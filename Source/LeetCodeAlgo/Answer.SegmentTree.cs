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
        public int sum;
        public SegmentNode(int start, int end, int sum = 0, SegmentNode left = null, SegmentNode right = null)
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
            }
            else
            {
                int mid = start + (end - start)/2;
                node.left = buildInternal(nums, start, mid);
                node.right = buildInternal(nums, mid+1, end);
                node.sum = node.left.sum + node.right.sum;
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
                }
            }
        }

        public int SumRange(int left,int right)
        {
            return sumRangeInternal(root, left, right);
        }

        private int sumRangeInternal(SegmentNode node, int left,int right)
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
    }


}
