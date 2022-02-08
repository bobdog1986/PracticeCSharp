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

        ///1818. Minimum Absolute Sum Difference
        ///You are given two positive integer arrays nums1 and nums2, both of length n.
        ///Return the minimum absolute sum of |nums1[i] - nums2[i]| after replacing at most one element in the array nums1.
        ///return it modulo 109 + 7. 1 <= nums1[i], nums2[i] <= 10&5, 1 <= n <= 105
        public int MinAbsoluteSumDiff(int[] nums1, int[] nums2)
        {
            int[] arr = new int[100001];
            int left = 100000;
            int right = 1;
            foreach(var i in nums1)
            {
                arr[i]++;
                left = Math.Min(left, i);
                right = Math.Max(right, i);
            }
            int max = 0;
            long sum = 0;
            for(int i=0;i<nums1.Length; i++)
            {
                int abs=nums1[i]>= nums2[i]? nums1[i] - nums2[i]: nums2[i] - nums1[i];
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
                    while ((j - len >= left || j + len <= right) && len<abs && (abs-len>max))
                    {
                        if((j - len >= left && arr[j - len]>0 )
                            ||(j + len <= right && arr[j + len] > 0))
                        {
                            max = abs - len;
                            break;
                        }
                        len++;
                    }
                }
            }

            sum -= max;
            return (int)(sum%1000000007);
        }
        /// 1823. Find the Winner of the Circular Game
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