using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1814. Count Nice Pairs in an Array
        /// You are given an array nums that consists of non-negative integers.
        /// For example, rev(123) = 321, and rev(120) = 21.
        /// A pair of indices(i, j) is nice if it satisfies all of the following conditions:
        ///0 <= i<j<nums.length
        ///nums[i] + rev(nums[j]) == nums[j] + rev(nums[i])
        ///Return the number of nice pairs of indices.Since that number can be too large, return it modulo 109 + 7.

        public int CountNicePairs(int[] nums)
        {
            long modulo = 1000000007;
            long count = 0;

            Dictionary<int, long> diffPairs = new Dictionary<int, long>();

            for (int i = 0; i < nums.Length; i++)
            {
                var rev1 = rev10(nums[i]);
                //var rev2 = rev10(rev1);
                var diff = nums[i] - rev1;

                if (diffPairs.ContainsKey(diff))
                {
                    diffPairs[diff]++;
                }
                else
                {
                    diffPairs.Add(diff, 1);
                }
            }

            foreach (var pair in diffPairs)
            {
                count += pair.Value * (pair.Value - 1) / 2;
            }

            return (int)(count % modulo);
        }

        public int rev10(int n)
        {
            if (n < 10) return n;

            int result = 0;
            int m = 10;
            while (n > 0)
            {
                result = result * 10;

                var a = n % m;
                result += a;
                n = n / m;
            }

            return result;
        }
    }
}