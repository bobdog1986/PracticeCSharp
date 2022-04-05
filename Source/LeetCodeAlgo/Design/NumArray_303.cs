using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///303. Range Sum Query - Immutable
    ///Given an integer array nums, handle multiple queries of the following type:
    ///Calculate the sum of the elements of nums between indices [left,right] inclusive where left <= right.
    public class NumArray_303
    {
        private int[] dp;
        public NumArray_303(int[] nums)
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
}
