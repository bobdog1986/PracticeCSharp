using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1512. Number of Good Pairs
        ///Given an array of integers nums, return the number of good pairs.
        ///A pair(i, j) is called good if nums[i] == nums[j] and i<j.
        ///nums = [1,1,1,1], result =6;
        ///1 <= nums.length <= 100
        ///1 <= nums[i] <= 100
        public int NumIdenticalPairs(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            int result = 0;

            int[] arr = new int[100 + 1];
            foreach (var i in nums)
                arr[i]++;

            foreach (var i in arr)
            {
                if (i > 1)
                {
                    result += i * (i - 1) / 2;
                }
            }

            return result;
        }

        ///1513. Number of Substrings With Only 1s
        ///Given a binary string s, return the number of substrings with all characters 1's.
        ///Since the answer may be too large, return it modulo 109 + 7.
        public int NumSub(string s)
        {
            long ans = 0;
            long mod = 10_0000_0007;
            Dictionary<long, long> dict = new Dictionary<long, long>();
            long count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if ('1' == s[i]) { count++; }
                else
                {
                    if (count == 0) continue;
                    if (!dict.ContainsKey(count)) { NumSub(count, dict); }
                    ans += dict[count];
                    ans %= mod;
                    count = 0;
                }
            }
            if (!dict.ContainsKey(count)) { NumSub(count, dict); }
            ans += dict[count];
            ans %= mod;
            return (int)(ans % mod);
        }
        public void NumSub(long count, Dictionary<long, long> dict)
        {
            long ans = 0;
            long seed = 0;
            int i = 0;
            while (i <= count)
            {
                ans += seed;
                if (!dict.ContainsKey(i)) dict.Add(i, ans);
                i++;
                seed++;
            }
        }
    }
}