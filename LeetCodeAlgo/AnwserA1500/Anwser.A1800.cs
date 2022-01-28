using System.Collections.Generic;
using System;
using System.Linq;

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

        ///1823. Find the Winner of the Circular Game
        public int FindTheWinner(int n, int k)
        {
            List<int> list = new List<int>();
            for(int i=1;i<=n;i++)
                list.Add(i);

            int start = 0;

            while (list.Count > 1)
            {
                int steps = (k-1) % list.Count;
                int loss = start + steps;
                if(loss>=list.Count)
                    loss-=list.Count;
                if (loss <list.Count-1)
                {
                    list.RemoveAt(loss);
                    start = loss;
                }
                else
                {
                    list.RemoveAt(loss);
                    start = 0;
                }
            }

            return list[0];
        }
    }
}