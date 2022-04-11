﻿using System.Collections.Generic;
using System;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1805. Number of Different Integers in a String
        public int NumDifferentIntegers(string word)
        {
            HashSet<string> set = new HashSet<string>();
            List<char> list = new List<char>();
            foreach (var c in word)
            {
                if (char.IsDigit(c))
                {
                    list.Add(c);
                }
                else
                {
                    NumDifferentIntegers_Add(set, list);
                }
            }
            NumDifferentIntegers_Add(set, list);
            return set.Count;
        }

        private void NumDifferentIntegers_Add(HashSet<string> set, List<char> list)
        {
            if (list.Count != 0)
            {
                var sub = new List<char>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != list.Count - 1 && sub.Count == 0 && list[i] == '0') continue;
                    sub.Add(list[i]);
                }
                set.Add(new string(sub.ToArray()));
                list.Clear();
            }
        }


        /// 1814. Count Nice Pairs in an Array
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

        ///1818. Minimum Absolute Sum Difference
        ///You are given two positive integer arrays nums1 and nums2, both of length n.
        ///Return the minimum absolute sum of |nums1[i] - nums2[i]| after replacing at most one element in the array nums1.
        ///return it modulo 109 + 7. 1 <= nums1[i], nums2[i] <= 10&5, 1 <= n <= 105
        public int MinAbsoluteSumDiff(int[] nums1, int[] nums2)
        {
            int[] arr = new int[100001];
            int left = 100000;
            int right = 1;
            foreach (var i in nums1)
            {
                arr[i]++;
                left = Math.Min(left, i);
                right = Math.Max(right, i);
            }
            int max = 0;
            long sum = 0;
            for (int i = 0; i < nums1.Length; i++)
            {
                int abs = nums1[i] >= nums2[i] ? nums1[i] - nums2[i] : nums2[i] - nums1[i];
                sum += abs;

                int j = nums2[i];
                if (arr[j] > 0)
                {
                    //len=0, we can minus the whole abs
                    max = Math.Max(max, abs);
                }
                else
                {
                    //len is the closest nums1 element to nums2[i]
                    int len = 1;
                    while ((j - len >= left || j + len <= right) && len < abs && (abs - len > max))
                    {
                        if ((j - len >= left && arr[j - len] > 0)
                            || (j + len <= right && arr[j + len] > 0))
                        {
                            max = abs - len;
                            break;
                        }
                        len++;
                    }
                }
            }

            sum -= max;
            return (int)(sum % 1000000007);
        }

        ///1822. Sign of the Product of an Array
        ///Let product be the product of all values in the array nums. Return sign of (product).
        public int ArraySign(int[] nums)
        {
            int negCount = 0;
            foreach (var n in nums)
            {
                if (n == 0) return 0;
                if (n < 0) negCount++;
            }
            return negCount % 2 == 0 ? 1 : -1;
        }

        /// 1823. Find the Winner of the Circular Game
        public int FindTheWinner(int n, int k)
        {
            List<int> list = new List<int>();
            for (int i = 1; i <= n; i++)
                list.Add(i);

            int start = 0;

            while (list.Count > 1)
            {
                int steps = (k - 1) % list.Count;
                int loss = start + steps;
                if (loss >= list.Count)
                    loss -= list.Count;
                if (loss < list.Count - 1)
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

        ///1828. Queries on Number of Points Inside a Circle
        public int[] CountPoints(int[][] points, int[][] queries)
        {
            return queries.Select(q => points.Count(p => (p[0] - q[0]) * (p[0] - q[0]) + (p[1] - q[1]) * (p[1] - q[1]) <= q[2] * q[2])) .ToArray();
        }
        ///1838. Frequency of the Most Frequent Element, #Sliding Window
        ///In one operation, you can choose an index of nums and increment the element at that index by 1.
        ///Return the maximum possible frequency of an element after performing at most k operations.
        public int MaxFrequency(int[] nums, int k)
        {
            //the key is to find out the valid condition:
            //k + sum >= size * max which is k + sum >= (j - i + 1) * nums[j]
            int res = 1, i = 0, j;
            long sum = 0;
            Array.Sort(nums);
            for (j = 0; j < nums.Length; ++j)
            {
                sum += nums[j];
                while (sum + k < (long)nums[j] * (j - i + 1))
                {
                    sum -= nums[i];
                    i++;
                }
                res = Math.Max(res, j - i + 1);
            }
            return res;
        }

        /// 1844. Replace All Digits with Characters
        ///For every odd index i, you want to replace the digit s[i] with shift(s[i-1], s[i]).
        public string ReplaceDigits(string s)
        {
            var arr = s.ToCharArray();
            for (int i = 1; i < arr.Length; i += 2)
            {
                arr[i] = (char)(arr[i - 1] + (arr[i] - '0'));
            }
            return new string(arr);
        }

        ///1845. Seat Reservation Manager, see SeatManager
    }
}