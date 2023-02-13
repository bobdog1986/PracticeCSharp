using LeetCodeAlgo.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2550. Count Collisions of Monkeys on a Polygon
        public int MonkeyMove(int n)
        {
            //res = 2^n - 2;
            long res = 1;
            long powBase = 2;
            long mod = 1_000_000_007;
            while (n > 0)
            {
                if (n % 2 == 1)
                    res = res * powBase % mod;
                powBase = powBase * powBase % mod;
                n /= 2;
            }
            return (int)((res - 2 + mod) % mod);
        }

        ///2553. Separate the Digits in an Array
        // public int[] SeparateDigits(int[] nums)
        // {
        //     List<int> res = new List<int>();
        //     foreach (var i in nums)
        //     {
        //         res.AddRange(i.ToString().Select(x => x - '0'));
        //     }
        //     return res.ToArray();
        // }

        ///2554. Maximum Number of Integers to Choose From a Range I
        // public int MaxCount(int[] banned, int n, int maxSum)
        // {
        //     int res = 0;
        //     int i = 1;
        //     var set = banned.ToHashSet();
        //     int sum = 0;
        //     while (i <= n && sum <= maxSum)
        //     {
        //         if (i + sum > maxSum) break;
        //         if (set.Contains(i))
        //         {
        //             i++;
        //             continue;
        //         }
        //         else
        //         {
        //             res++;
        //             sum += i;
        //             i++;
        //         }
        //     }
        //     return res;
        // }

        ///2558. Take Gifts From the Richest Pile
        // public long PickGifts(int[] gifts, int k)
        // {
        //     long res = 0;
        //     var pq = new PriorityQueue<int, int>();
        //     foreach (var i in gifts)
        //     {
        //         pq.Enqueue(i, -i);
        //     }
        //     while (k-- > 0)
        //     {
        //         int top = pq.Dequeue();
        //         int next = (int)Math.Sqrt(top);
        //         pq.Enqueue(next, -next);
        //     }
        //     while (pq.Count > 0)
        //     {
        //         res += pq.Dequeue();
        //     }
        //     return res;
        // }

        ///2559. Count Vowel Strings in Ranges
        // public int[] VowelStrings(string[] words, int[][] queries)
        // {
        //     int m = words.Length;
        //     int[] prefix = new int[m];
        //     HashSet<char> set = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
        //     for (int i = 0; i < m; i++)
        //     {
        //         if (i > 0)
        //             prefix[i] = prefix[i - 1];
        //         if (set.Contains(words[i].First()) && set.Contains(words[i].Last()))
        //             prefix[i]++;
        //     }
        //     int n = queries.Length;
        //     int[] res = new int[n];
        //     for (int i = 0; i < n; i++)
        //     {
        //         int curr = prefix[queries[i][1]];
        //         if (queries[i][0] > 0)
        //             curr -= prefix[queries[i][0] - 1];
        //         res[i] = curr;
        //     }
        //     return res;
        // }

        ///2563. Count the Number of Fair Pairs, #Binary Search
        //Given a 0-indexed integer array nums of size n and two integers lower and upper,
        // return the number of fair pairs.
        // A pair (i, j) is fair if:
        // 0 <= i < j < n, and lower <= nums[i] + nums[j] <= upper
        public long CountFairPairs(int[] nums, int lower, int upper)
        {
            long res = 0;
            Array.Sort(nums);
            int n = nums.Length;
            for (int i = 0; i < n - 1; i++)
            {
                if (nums[i] + nums[i + 1] > upper) break;
                if (nums[i] + nums[n - 1] < lower) continue;
                int left = i + 1;
                int right = n - 1;
                while (left < right)
                {
                    int mid = (left + right) / 2;
                    if (nums[i] + nums[mid] < lower) left = mid + 1;
                    else right = mid;
                }
                int low = left;
                left = i + 1;
                right = n - 1;
                while (left < right)
                {
                    int mid = (left + right + 1) / 2;
                    if (nums[i] + nums[mid] > upper) right = mid - 1;
                    else left = mid;
                }
                int high = left;
                res += high - low + 1;
            }
            return res;
        }
    }
}

