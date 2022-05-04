using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1502. Can Make Arithmetic Progression From Sequence
        ///A sequence of numbers is an arithmetic progression if the difference between any two consecutive elements is same.
        public bool CanMakeArithmeticProgression(int[] arr)
        {
            Array.Sort(arr);
            int diff = arr[1] - arr[0];
            for (int i = 2; i < arr.Length; i++)
                if (arr[i] - arr[i - 1] != diff) return false;
            return true;
        }
        /// 1512. Number of Good Pairs
        ///Given an array of integers nums, return the number of good pairs.
        ///A pair(i, j) is called good if nums[i] == nums[j] and i<j.
        ///nums = [1,1,1,1], result =6;
        ///1 <= nums.length <= 100, 1 <= nums[i] <= 100
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

        ///1523. Count Odd Numbers in an Interval Range
        ///Given two non-negative integers low and high. Return the count of odd numbers between low and high (inclusive).
        public int CountOdds(int low, int high)
        {
            var diff = high - low;
            if (diff % 2 == 0) return diff / 2 + (low % 2 == 1 ? 1 : 0);
            else return diff / 2 + 1;
        }

        ///1525. Number of Good Ways to Split a String
        public int NumSplits(string s)
        {
            int res = 0;
            HashSet<char> set1 = new HashSet<char>();
            HashSet<char> set2 = new HashSet<char>();
            int[] arr1 = new int[s.Length];
            int[] arr2 = new int[s.Length];
            for (int i=0; i<s.Length; i++)
            {
                set1.Add(s[i]);
                arr1[i] = set1.Count;
            }
            for(int i=s.Length-1; i>=0; i--)
            {
                set2.Add(s[i]);
                arr2[i] = set2.Count;
            }
            for(int i=0; i<s.Length-1; i++)
            {
                if (arr1[i] == arr2[i + 1])
                    res++;
            }
            return res;
        }
        /// 1526. Minimum Number of Increments on Subarrays to Form a Target Array
        /// In one operation you can choose any subarray from initial and increment each value by one.
        ///Return the minimum number of operations to form a target array from initial.
        public int MinNumberOperations(int[] target)
        {
            int res = 0;
            int pre = 0;
            foreach (var n in target)
            {
                if (n > pre)
                    res += n - pre;
                pre = n;
            }
            return res;
        }

        /// 1528. Shuffle String
        public string RestoreString(string s, int[] indices)
        {
            char[] arr = new char[indices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                arr[indices[i]] = s[i];
            }
            return new string(arr);
        }

        ///1539. Kth Missing Positive Number, #Binary Search
        ///Given an array arr of positive integers sorted in a strictly increasing order, and an integer k.
        ///Find the kth positive integer that is missing from this array.
        public int FindKthPositive(int[] arr, int k)
        {
            int left = 0;
            int right = arr.Length;
            int mid;
            while (left < right)
            {
                mid = (left + right) / 2;
                if (arr[mid] - 1 - mid < k)
                    left = mid + 1;
                else
                    right = mid;
            }
            return left + k;
        }
    }
}