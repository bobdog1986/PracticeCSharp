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
        public int start,end;
        public SegmentNode left,right;
        public long sum;
        public int max,min;
        public SegmentNode(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
    }

    public class SegmentTree
    {
        public SegmentNode root = null;

        public SegmentTree() { }

        public SegmentTree(int[] nums)
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

        public int MaxRange(int left, int right)
        {
            return maxRangeInternal(root, left, right);
        }

        private int maxRangeInternal(SegmentNode node, int left, int right)
        {
            if (node == null) return int.MinValue;
            if (node.start > right || node.end < left) return int.MinValue;
            if (node.start == left && node.end == right)
            {
                return node.max;
            }
            else
            {
                int mid = node.start + (node.end - node.start) / 2;
                if (mid < left)
                {
                    return maxRangeInternal(node.right, left, right);
                }
                else if (mid >= right)
                {
                    return maxRangeInternal(node.left, left, right);
                }
                else
                {
                    return Math.Max(maxRangeInternal(node.left, left, mid), maxRangeInternal(node.right, mid + 1, right));
                }
            }
        }

    }

    //for merge intervals, 1_000_000_000 is too big for normal segment tree
    public class SegmentIntervalNode
    {
        private int start, end;
        private SegmentIntervalNode left, right;

        public int Count { get; private set; }

        public SegmentIntervalNode(int start, int end)
        {
            this.start = start;
            this.end = end;
        }

        public int Add(int left, int right)
        {
            if (left > right || left > end || right < start)
                return Count;
            if ((left == start && right == end) || Count == end - start + 1)
            {
                Count = end - start + 1;
                return Count;
            }

            int mid = start + (end - start) / 2;
            if (this.left == null)
                this.left = new SegmentIntervalNode(start, mid);
            if (this.right == null)
                this.right = new SegmentIntervalNode(mid + 1, end);

            var leftCount = this.left.Add(left, Math.Min(mid, right));
            var rightCount = this.right.Add(Math.Max(left, mid + 1), right);
            Count = leftCount + rightCount;
            return Count;
        }
    }
}
