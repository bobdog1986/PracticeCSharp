using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1658. Minimum Operations to Reduce X to Zero
        ///Return the minimum number of operations to reduce x to exactly 0 if it is possible, otherwise, return -1.
        ///This problem is equivalent to finding the longest subarray whose sum is == totalSum - x
        public int MinOperations(int[] nums, int x)
        {
            int sum = nums.Sum() - x;
            if (sum < 0) return -1;
            if (sum == 0) return nums.Length;

            int start = 0, windowSum = 0, len = -1;
            for (int end = 0; end < nums.Length; end++)
            {
                if (windowSum < sum)
                    windowSum += nums[end];
                while (windowSum >= sum)
                {
                    if (windowSum == sum)
                        len = Math.Max(len, end - start + 1);
                    windowSum -= nums[start];
                    start++;
                }
            }

            return len == -1 ? -1 : nums.Length - len;
        }
    }
}
