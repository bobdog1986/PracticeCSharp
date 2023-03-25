using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    /// <summary>
    /// standard segment tree for update and range query of sum/min/max
    /// </summary>
    public class SegmentTree
    {
        protected class SegmentNode
        {
            public readonly int start, end;
            public SegmentNode left, right;
            public long sum = 0;
            public int max = int.MinValue, min = int.MaxValue;
            public long m = 1, inc = 0;
            public SegmentNode(int start, int end)
            {
                this.start = start;
                this.end = end;
            }
        }

        protected readonly SegmentNode root;
        public int[] arr;

        public SegmentTree(int[] nums)
        {
            this.arr=nums;
            this.root = buildInternal(nums, 0, nums.Length-1);
        }

        protected SegmentNode buildInternal(int[] nums, int start, int end)
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

        protected void updateInternal(SegmentNode node, int index ,int val)
        {
            if (node == null) return;
            if(index>=node.start && index <= node.end)
            {
                if(node.start == node.end)
                {
                    node.sum = val;
                    node.min = val;
                    node.max = val;
                    this.arr[node.start]=val;
                }
                else
                {
                    int mid = node.start + ( node.end - node.start) / 2;
                    if (node.left == null)//lazy build
                    {
                        node.left = new SegmentNode(node.start, mid);
                        node.right = new SegmentNode(mid + 1, node.end);
                    }
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

        public long SumOfRange(int left,int right)
        {
            return sumOfRangeInternal(root, Math.Max(root.start, left), Math.Min(root.end, right));
        }

        protected long sumOfRangeInternal(SegmentNode node, int left,int right)
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
                if (node.left == null)//lazy build
                {
                    node.left = new SegmentNode(node.start, mid);
                    node.right = new SegmentNode(mid + 1, node.end);
                }
                if (mid < left)
                {
                    return sumOfRangeInternal(node.right, left, right);
                }
                else if(mid>= right)
                {
                    return sumOfRangeInternal(node.left, left, right);
                }
                else
                {
                    return sumOfRangeInternal(node.left, left, mid) + sumOfRangeInternal(node.right, mid+1, right);
                }
            }
        }

        public int MinOfRange(int left, int right)
        {
            return minRangeInternal(root, Math.Max(root.start, left), Math.Min(root.end, right));
        }

        protected int minRangeInternal(SegmentNode node, int left, int right)
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
                if (node.left == null)//lazy build
                {
                    node.left = new SegmentNode(node.start, mid);
                    node.right = new SegmentNode(mid + 1, node.end);
                }
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

        public int MaxOfRange(int left, int right)
        {
            return maxOfRangeInternal(root, Math.Max(root.start, left), Math.Min(root.end, right));
        }

        private int maxOfRangeInternal(SegmentNode node, int left, int right)
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
                if (node.left == null)//lazy build
                {
                    node.left = new SegmentNode(node.start, mid);
                    node.right = new SegmentNode(mid + 1, node.end);
                }
                if (mid < left)
                {
                    return maxOfRangeInternal(node.right, left, right);
                }
                else if (mid >= right)
                {
                    return maxOfRangeInternal(node.left, left, right);
                }
                else
                {
                    return Math.Max(maxOfRangeInternal(node.left, left, mid), maxOfRangeInternal(node.right, mid + 1, right));
                }
            }
        }

    }
}
