using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///303. Range Sum Query - Immutable, #Segment Tree
    ///Given an integer array nums, handle multiple queries of the following type:
    ///Calculate the sum of the elements of nums between indices [left,right] inclusive where left <= right.
    public class NumArray
    {
        private int[] dp;
        public NumArray(int[] nums)
        {
            dp = new int[nums.Length];
            int sum = 0;
            for(int i = 0; i < dp.Length; i++)
            {
                sum += nums[i];
                dp[i]=sum;
            }
        }

        public int SumRange(int left, int right)
        {
            if (left == 0) return dp[right];
            return dp[right] - dp[left-1];
        }
    }

    public class NumArray_SegmentTree
    {
        private int[] tree;
        private int n;

        public NumArray_SegmentTree(int[] nums)
        {
            n = nums.Length;
            if (n == 0) return;
            //why?
            if ((n & (n - 1)) == 0)
            {
                tree = new int[2 * n];
            }
            else
            {
                int count = 0;
                int n1 = n;
                while (n1 > 0)
                {
                    count++;
                    n1 >>= 1;
                }
                tree = new int[2 * (1 << count)];
            }
            buildSegmentTree(0, n - 1, 1, nums);
        }
        private int buildSegmentTree(int left, int right, int pos, int[] nums)
        {
            if (left == right)
            {
                tree[pos] = nums[left];
                return tree[pos];
            }
            int mid = (left + right) / 2;
            tree[pos] = buildSegmentTree(left, mid, pos * 2, nums) + buildSegmentTree(mid + 1, right, pos * 2 + 1, nums);
            return tree[pos];

        }
        private int find(int start, int end, int left, int right, int pos)
        {
            if (end < left || right < start)
                return 0;
            if (start <= left && right <= end)
                return tree[pos];
            int mid = (left + right) / 2;
            return find(start, end, left, mid, pos * 2) + find(start, end, mid + 1, right, pos * 2 + 1);
        }
        public int SumRange(int left, int right)
        {
            return find(left, right, 0, n - 1, 1);
        }
    }
}
