using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    //common data structure of SegmentNode
    public class SegmentNode
    {
        public int start;
        public int end;
        public SegmentNode left;
        public SegmentNode right;
        public long sum;
        public int max;
        public int min;
        public SegmentNode(int start, int end, long sum = 0, SegmentNode left = null, SegmentNode right = null)
        {
            this.start = start;
            this.end = end;
            this.sum = sum;
            this.left = left;
            this.right = right;
        }
    }

    public class SegmentTree
    {
        public SegmentNode root=null;

        public SegmentTree()
        {

        }

        public SegmentTree(int[] nums)
        {
            Build(nums);
        }

        public SegmentTree(int[] nums, int start, int end)
        {
            Build(nums, start, end);
        }

        public void Build(int[] nums)
        {
            Build(nums, 0, nums.Length - 1);
        }

        public void Build(int[] nums,int start,int end)
        {
            this.root = buildInternal(nums, start, end);
        }

        private SegmentNode buildInternal(int[] nums, int start, int end)
        {
            if (start > end)
                return null;

            var node = new SegmentNode(start, end);
            if(start == end)
            {
                node.sum = nums[start];
                node.max= nums[start];
                node.min = nums[start];
            }
            else
            {
                int mid = start + (end - start)/2;
                node.left = buildInternal(nums, start, mid);
                node.right = buildInternal(nums, mid+1, end);
                node.sum = node.left.sum + node.right.sum;

                node.max = Math.Max(node.left.max, node.right.max);
                node.min = Math.Min(node.left.min, node.right.min);
            }
            return node;
        }

        public void Update(int index,int val)
        {
            updateInternal(root, index, val);
        }

        private void updateInternal(SegmentNode node, int index ,int val)
        {
            if (node == null) return;
            if(index>=node.start && index <= node.end)
            {
                if(node.start == node.end)
                {
                    node.sum = val;
                    node.min = val;
                    node.max = val;
                }
                else
                {
                    int mid = node.start + ( node.end - node.start) / 2;
                    if (index <= mid)
                    {
                        updateInternal(node.left, index, val);
                    }
                    else
                    {
                        updateInternal(node.right, index, val);
                    }
                    node.sum = node.left.sum + node.right.sum;

                    node.max = Math.Max(node.left.max, node.right.max);
                    node.min = Math.Min(node.left.min, node.right.min);
                }
            }
        }

        public long SumRange(int left,int right)
        {
            return sumRangeInternal(root, left, right);
        }

        private long sumRangeInternal(SegmentNode node, int left,int right)
        {
            if (node == null) return 0;
            if (node.start > right || node.end < left) return 0;
            if(node.start == left && node.end == right)
            {
                return node.sum;
            }
            else
            {
                int mid = node.start + (node.end - node.start) / 2;
                if (mid < left)
                {
                    return sumRangeInternal(node.right, left, right);
                }
                else if(mid>= right)
                {
                    return sumRangeInternal(node.left, left, right);
                }
                else
                {
                    return sumRangeInternal(node.left, left, mid) + sumRangeInternal(node.right, mid+1, right);
                }
            }
        }

        public void Scatter(int left,int right, int k,int m)
        {
            scatterInternal(root, left, right, k, m);
        }

        private void scatterInternal(SegmentNode node, int left, int right, int k,int m)
        {
            if (node == null) return;
            if (node.start > right || node.end < left) return;
            if (node.start == node.end)
            {
                int diff = Math.Min(k, m - (int)node.sum);
                node.sum = node.sum + diff;
                node.min = (int)node.sum;
                node.max = (int)node.sum;
            }
            else
            {
                long leftSum = node.left.sum;
                long leftDiff = (long)m * (node.left.end - node.left.start + 1) - leftSum;
                if(leftDiff >= k)
                {
                    scatterInternal(node.left, left, right, k, m);
                }
                else
                {
                    if(leftDiff >0)
                        scatterInternal(node.left, left, right, (int)leftDiff, m);
                    scatterInternal(node.right, left, right, k - (int)leftDiff, m);
                }
                node.sum = node.left.sum + node.right.sum;
                node.min = Math.Min(node.left.min, node.right.min);
                node.max = Math.Max(node.left.max, node.right.max);
            }

        }

        public int MinRange(int left, int right)
        {
            return minRangeInternal(root, left, right);
        }

        private int minRangeInternal(SegmentNode node, int left, int right)
        {
            if (node == null) return int.MaxValue;
            if (node.start > right || node.end < left) return int.MaxValue;
            if (node.start == left && node.end == right)
            {
                return node.min;
            }
            else
            {
                int mid = node.start + (node.end - node.start) / 2;
                if (mid < left)
                {
                    return minRangeInternal(node.right, left, right);
                }
                else if (mid >= right)
                {
                    return minRangeInternal(node.left, left, right);
                }
                else
                {
                    return Math.Min(minRangeInternal(node.left, left, mid), minRangeInternal(node.right, mid + 1, right));
                }
            }
        }

        public int[] Gather(int left, int right, int val, int m)
        {
            return gatherInternal(root, left, right, val, m);
        }

        private int[] gatherInternal(SegmentNode node, int left, int right, int val,int m )
        {
            if (node == null) return new int[] { };
            if (node.start > right || node.end < left) return new int[] { };
            if (node.min + val > m) return new int[] { };
            if (node.start == node.end)
            {
                int[] res = new int[] { node.start, (int)node.sum };
                node.sum = node.sum + val;
                node.min = (int)node.sum;
                node.max = (int)node.sum;
                return res;
            }
            else
            {
                int[] res = new int[] {};
                if (node.left != null && node.left.min + val <= m)
                {
                    res = gatherInternal(node.left, left, right,val,m);
                }
                else if (node.right != null && node.right.min + val <= m)
                {
                    res = gatherInternal(node.right, left, right, val,m);
                }
                node.sum = node.left.sum + node.right.sum;
                node.min = Math.Min(node.left.min, node.right.min);
                node.max = Math.Max(node.left.max, node.right.max);
                return res;
            }
        }
    }


}
