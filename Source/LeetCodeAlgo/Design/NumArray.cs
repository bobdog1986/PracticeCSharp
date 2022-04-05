using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///307. Range Sum Query - Mutable, #Segment Tree
    ///Given an integer array nums, handle multiple queries of the following types:
    ///Update the value of an element in nums.
    ///Calculate the sum of the elements of nums between indices left and right inclusive where left <= right.

    public class NumArray
    {
        private SegmentTreeNode root = null;

        public NumArray(int[] nums)
        {
            root = BuildSegmentTree(nums, 0, nums.Length - 1);
        }

        private SegmentTreeNode BuildSegmentTree(int[] nums, int start, int end)
        {
            if (start > end)
            {
                return null;
            }
            else
            {
                SegmentTreeNode res = new SegmentTreeNode(start, end);
                if (start == end)
                {
                    res.sum = nums[start];
                }
                else
                {
                    int mid = (start + end ) / 2;
                    res.left = BuildSegmentTree(nums, start, mid);
                    res.right = BuildSegmentTree(nums, mid + 1, end);
                    res.sum = res.left.sum + res.right.sum;
                }
                return res;
            }
        }

        public void Update(int index, int val)
        {
            Update(root, index, val);
        }

        private void Update(SegmentTreeNode node,int index, int val)
        {
            if (node.start == node.end)
            {
                node.sum = val;
            }
            else
            {
                int mid = (node.start+ node.end) / 2;
                if (index <= mid)
                {
                    Update(node.left, index, val);
                }
                else
                {
                    Update(node.right, index, val);
                }
                node.sum = node.left.sum + node.right.sum;
            }
        }

        public int SumRange(int left, int right)
        {
            return SumRange(root, left, right);
        }

        private int SumRange(SegmentTreeNode node, int start, int end)
        {
            if (node.end == end && node.start == start)
            {
                return node.sum;
            }
            else
            {
                int mid = (node.start +node.end ) / 2;
                if (end <= mid)
                {
                    return SumRange(node.left, start, end);
                }
                else if (start >= mid + 1)
                {
                    return SumRange(node.right, start, end);
                }
                else
                {
                    return SumRange(node.right, mid + 1, end) + SumRange(node.left, start, mid);
                }
            }
        }
    }

    public class SegmentTreeNode
    {
        public int start;
        public int end;
        public SegmentTreeNode left;
        public SegmentTreeNode right;
        public int sum;

        public SegmentTreeNode(int start, int end)
        {
            this.start = start;
            this.end = end;
            this.left = null;
            this.right = null;
            this.sum = 0;
        }
    }
}
