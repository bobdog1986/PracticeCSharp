using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy
{
    public partial class Easy
    {
        /// 1. Two Sum
        // return indices of the two numbers such that they add up to target.
        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(target - nums[i]))
                    return new int[2] { dict[target - nums[i]], i };
                if (!dict.ContainsKey(nums[i]))
                    dict.Add(nums[i], i);
            }
            return new int[2];
        }


    }
}
