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

        ///2562. Find the Array Concatenation Value
        //public long FindTheArrayConcVal(int[] nums)
        //{
        //    long res = 0;
        //    int n = nums.Length;
        //    for (int i = 0; i<=n-1-i; i++)
        //    {
        //        if (i==n-1-i)
        //        {
        //            res+=nums[i];
        //        }
        //        else
        //        {
        //            res+= int.Parse($"{nums[i]}{nums[n-1-i]}");
        //        }
        //    }
        //    return res;
        //}

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

        ///2564. Substring XOR Queries
        public int[][] SubstringXorQueries(string s, int[][] queries)
        {
            int n = queries.Length;
            var dict = new Dictionary<int, int[]>();
            for (int i = 0; i < s.Length; i++)
            {
                int len = 30;
                if (s[i] == '0') len++;//value must <= 10^9,if start at '0' can shift left 31 bits
                int curr = 0;
                for (int j = 0; j < len && j + i < s.Length; j++)
                {
                    curr <<= 1;
                    curr += s[i + j] - '0';
                    if (!dict.ContainsKey(curr))
                    {
                        dict.Add(curr, new int[] { i, i + j });
                    }
                    else if (dict[curr][1] - dict[curr][0] > j)//shorter than current
                    {
                        dict[curr] = new int[] { i, i + j };
                    }
                }
            }
            int[][] res = new int[n][];
            for (int i = 0; i < n; i++)
            {
                int q = queries[i][0] ^ queries[i][1];
                if (dict.ContainsKey(q)) res[i] = dict[q];
                else res[i] = new int[] { -1, -1 };
            }
            return res;
        }

        ///2567. Minimum Score by Changing Two Elements
        //public int MinimizeSum(int[] nums)
        //{
        //    int n = nums.Length;
        //    Array.Sort(nums);
        //    int res = int.MaxValue;
        //    res = Math.Min(res, nums[n-1-1-1]-nums[0]);//two largest
        //    res = Math.Min(res, nums[n-1-1]-nums[1]);//one largest, one smallest
        //    res = Math.Min(res, nums[n-1]-nums[2]);//two smallest
        //    return res;
        //}

        ///2566. Maximum Difference by Remapping a Digit
        //public int MinMaxDifference(int num)
        //{
        //    string s= num.ToString();
        //    int n = s.Length;
        //    Dictionary<char,List<int>> dict=new Dictionary<char, List<int>>();
        //    for(int i = 0; i<n; i++)
        //    {
        //        if (!dict.ContainsKey(s[i])) dict.Add(s[i], new List<int>());
        //        dict[s[i]].Add(i);
        //    }

        //    int max = num;
        //    for(int i = 0; i<n; i++)
        //    {
        //        if (s[i]!='9')
        //        {
        //            char[] arr1 = s.ToArray();
        //            foreach(var j in dict[s[i]])
        //            {
        //                arr1[j]='9';
        //            }
        //            max = int.Parse(new string(arr1));
        //            break;
        //        }
        //    }

        //    char[] arr2 = s.ToArray();
        //    foreach (var i in dict[s[0]])
        //    {
        //        arr2[i]='0';
        //    }
        //    int min = int.Parse(new string(arr2));
        //    return max-min;
        //}

    }
}