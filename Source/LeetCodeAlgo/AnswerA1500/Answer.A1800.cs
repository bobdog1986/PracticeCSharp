﻿using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;
using System.Text;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1800. Maximum Ascending Subarray Sum
        //return the maximum possible sum of an ascending subarray in nums.
        public int MaxAscendingSum(int[] nums)
        {
            int res = 0;
            int sum = 0;
            int prev = int.MinValue;
            for(int i = 0; i < nums.Length; i++)
            {
                if(nums[i] <= prev) sum = 0;
                sum += nums[i];
                res = Math.Max(res, sum);
                prev = nums[i];
            }
            return res;
        }
        /// 1802. Maximum Value at a Given Index in a Bounded Array, #Binary Search
        //You are given three positive integers: n, index, and maxSum.
        //You want to construct an array nums (0-indexed) that satisfies the following conditions:
        //nums.length == n, nums[i] >0. abs(nums[i] - nums[i + 1]) <= 1.
        //The sum of all the elements of nums does not exceed maxSum.
        //Return nums[index] is maximized.
        //1 <= n <= maxSum <= 10^9 ,0 <= index<n
        public int MaxValue(int n, int index, int maxSum)
        {
            int left = 1;
            int right = maxSum - (n - 1);
            int count1 = index + 1;// [0,index]]
            int count2 = n - index - 1;//[index+1,n-1]
            while (left < right)
            {
                long mid = (left + right + 1) / 2;
                long sum1 = 0;
                if (mid - (count1 - 1) >= 1)
                {
                    sum1 = (mid - (count1 - 1) + mid) * count1 / 2;
                }
                else
                {
                    sum1 = (1 + mid) * mid / 2 + (count1 - mid);
                }
                long sum2 = 0;
                if (mid - count2 >= 1)
                {
                    sum2 = (mid - 1 + mid - count2) * count2 / 2;
                }
                else
                {
                    sum2 = (mid - 1 + 1) * (mid - 1) / 2 + count2 - (mid - 1);
                }

                long sum = sum1 + sum2;
                if (sum == maxSum) return (int)mid;
                else if (sum > maxSum)
                {
                    right = (int)mid - 1;
                }
                else
                {
                    left = (int)mid;
                }
            }
            return left;
        }

        /// 1805. Number of Different Integers in a String
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

        ///1806. Minimum Number of Operations to Reinitialize a Permutation
        public int ReinitializePermutation(int n)
        {
            int res = 0, i = 1;
            while (res == 0 || i > 1)
            {
                i = i * 2 % (n - 1);
                res++;
            }
            return res;
        }

        ///1807. Evaluate the Bracket Pairs of a String
        public string Evaluate(string s, IList<IList<string>> knowledge)
        {
            var sb =new StringBuilder();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var i in knowledge)
                dict.Add(i[0], i[1]);
            int left = 0;
            for(int i = 0; i < s.Length; )
            {
                if (s[i] == '(')
                {
                    if (i != left)
                    {
                        sb.Append(s.Substring(left, i - left));
                    }

                    int j = i + 1;
                    while (s[j] != ')')
                    {
                        j++;
                    }

                    var str = s.Substring(i + 1, j - i - 1);
                    if (dict.ContainsKey(str)) sb.Append(dict[str]);
                    else sb.Append("?");
                    i = j + 1;
                    left = j + 1;
                }
                else
                {
                    i++;
                }
            }

            if (left != s.Length)
                sb.Append(s.Substring(left));

            return sb.ToString();
        }
        ///1812. Determine Color of a Chessboard Square
        public bool SquareIsWhite(string coordinates)
        {
            int row = coordinates[1] - '1';
            int col = coordinates[0] - 'a';
            return row % 2 != col % 2;
        }

        /// 1814. Count Nice Pairs in an Array
        // You are given an array nums that consists of non-negative integers.
        // For example, rev(123) = 321, and rev(120) = 21.
        // A pair of indices(i, j) is nice if it satisfies all of the following conditions:
        // 0 <= i<j<nums.length
        // nums[i] + rev(nums[j]) == nums[j] + rev(nums[i])
        // Return the number of nice pairs of indices.Since that number can be too large, return it modulo 109 + 7.

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

        ///1816. Truncate Sentence
        public string TruncateSentence(string s, int k)
        {
            return string.Join(' ', s.Split(' ').Take(k));
        }
        /// 1817. Finding the Users Active Minutes
        public int[] FindingUsersActiveMinutes(int[][] logs, int k)
        {
            int[] res = new int[k];

            Dictionary<int, HashSet<int>> dict= new Dictionary<int, HashSet<int>>();
            foreach(var log in logs)
            {
                if (dict.ContainsKey(log[0])) dict[log[0]].Add(log[1]);
                else dict.Add(log[0], new HashSet<int>() { log[1] });
            }

            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach(var d in dict)
            {
                if (map.ContainsKey(d.Value.Count)) map[d.Value.Count]++;
                else map.Add(d.Value.Count, 1);
            }

            for(int i = 0; i < k; i++)
            {
                res[i] = map.ContainsKey(i+1)? map[i+1]:0;
            }
            return res;
        }
        /// 1818. Minimum Absolute Sum Difference
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

        ///1824. Minimum Sideway Jumps, #DP
        public int MinSideJumps(int[] obstacles)
        {
            int[] dp = new int[] { 1, 0, 1 };
            foreach (var a in obstacles)
            {
                if (a > 0)
                    dp[a-1] = 1000000;
                for (int i = 0; i < 3; ++i)
                {
                    if (a-1 != i)
                    {
                        dp[i] = Math.Min(dp[i], Math.Min(dp[(i+1)% dp.Length], dp[(i+2)% dp.Length]) + 1);
                    }
                }
            }
            return dp.Min();
        }

        ///1827. Minimum Operations to Make the Array Increasing
        public int MinOperations(int[] nums)
        {
            int res = 0;
            int prev = 0;
            foreach(var n in nums)
            {
                if (n > prev) prev = n;
                else
                {
                    res += prev + 1 - n;
                    prev++;
                }
            }
            return res;
        }
        /// 1828. Queries on Number of Points Inside a Circle
        public int[] CountPoints(int[][] points, int[][] queries)
        {
            return queries.Select(q => points.Count(p => (p[0] - q[0]) * (p[0] - q[0]) + (p[1] - q[1]) * (p[1] - q[1]) <= q[2] * q[2])) .ToArray();
        }
        ///1829. Maximum XOR for Each Query
        ///You are given a sorted array nums of n non-negative integers and an integer maximumBit.
        ///You want to perform the following query n times:
        ///Find a non-negative integer k< 2maximumBit such that nums[0] XOR nums[1] XOR...XOR nums[nums.length - 1] XOR k is maximized.k is the answer to the ith query.
        ///Remove the last element from the current array nums.
        ///Return an array answer, where answer[i] is the answer to the ith query.
        public int[] GetMaximumXor(int[] nums, int maximumBit)
        {
            int n=nums.Length;
            int sum = 0;
            int[] res = new int[n];
            for(int i=0; i<n; i++)
            {
                sum^=nums[i];
                int x = 0;
                int m = 1;
                while (m < (1 << maximumBit))
                {
                    if((m & sum) == 0)
                    {
                        x += m;
                    }
                    m <<= 1;
                }
                res[n - 1 - i] = x;
            }
            return res;
        }
        ///1832. Check if the Sentence Is Pangram
        public bool CheckIfPangram(string sentence)
        {
            var set = new HashSet<char>();
            foreach(var c in sentence)
            {
                set.Add(c);
                if (set.Count == 26) return true;
            }
            return false;
        }
        /// 1833. Maximum Ice Cream Bars
        public int MaxIceCream(int[] costs, int coins)
        {
            int res = 0;
            Array.Sort(costs);
            foreach(var cost in costs)
            {
                if (coins < cost) break;
                else
                {
                    coins -= cost;
                    res++;
                }
            }
            return res;
        }

        ///1834. Single-Threaded CPU, #PriorityQueue
        //tasks[i] = [enqueueTimei, processingTimei]
        // CPU execute shortest processing time, if multiple exist then execute smallest index one
        public int[] GetOrder(int[][] tasks)
        {
            int n = tasks.Length;
            int[] res = new int[n];
            int index = 0;

            var pq1 = new PriorityQueue<int[], int[]>(Comparer<int[]>.Create((x, y) =>
            {
                if (x[0]==y[0])
                    return x[2]-y[2];
                else return x[0]-y[0];
            }));

            for(int i=0;i<n; i++)
            {
                int[] curr = new int[] { tasks[i][0], tasks[i][1], i };
                pq1.Enqueue(curr, curr);
            }

            var pq2 = new PriorityQueue<int[], int[]>(Comparer<int[]>.Create((x, y) =>
            {
                if (x[1]==y[1])
                    return x[2]-y[2];
                else return x[1]-y[1];
            }));

            int time = 0;
            while(index<n)
            {
                while (pq1.Count>0 && pq1.Peek()[0]<=time)
                {
                    var top1 = pq1.Dequeue();
                    pq2.Enqueue(top1, top1);
                }

                if(pq2.Count ==0)
                {
                    time = pq1.Peek()[0];
                    while(pq1.Count>0 && pq1.Peek()[0]<=time)
                    {
                        var top2 = pq1.Dequeue();
                        pq2.Enqueue(top2, top2);
                    }
                }

                var top=pq2.Dequeue();
                time += top[1];
                res[index++] = top[2];
            }
            return res;
        }
        ///1837. Sum of Digits in Base K
        public int SumBase(int n, int k)
        {
            int res = 0;
            while (n > 0)
            {
                res += n % k;
                n /= k;
            }
            return res;
        }
        /// 1838. Frequency of the Most Frequent Element, #Sliding Window
        ///In one operation, you can choose an index of nums and increment the element at that index by 1.
        ///Return the maximum possible frequency of an element after performing at most k operations.
        public int MaxFrequency(int[] nums, int k)
        {
            //the key is to find out the valid condition:
            //k + sum >= size * max which is k + sum >= (j - i + 1) * nums[j]
            Array.Sort(nums);
            int res = 1;
            int left = 0;
            long sum = 0;
            for (int right = 0; right < nums.Length; ++right)
            {
                sum += nums[right];
                while (sum + k < (long)nums[right] * (right - left + 1))
                {
                    sum -= nums[left++];
                }
                res = Math.Max(res, right - left + 1);
            }
            return res;
        }

        ///1839. Longest Substring Of All Vowels in Order, #Sliding Window
        //Each of the 5 English vowels('a', 'e', 'i', 'o', 'u') must appear at least once in it.
        //The letters must be sorted in alphabetical order(i.e.all 'a's before 'e's, all 'e's before 'i's, etc.).
        //return the length of the longest beautiful substring of word.If no such substring exists, return 0.
        public int LongestBeautifulSubstring(string word)
        {
            int res = 0;
            int len = 0;
            char prev = 'a';
            Dictionary<char, int> map = new Dictionary<char, int>()
            {
                { 'a',1},{ 'e',2},{ 'i',3},{ 'o',4},{ 'u',5},
            };

            foreach(var c in word)
            {
                if(len > 0)
                {
                    if (map[c] == map[prev] || map[c]== map[prev] + 1)
                    {
                        len++;
                        prev = c;
                        if (c == 'u')
                            res = Math.Max(res, len);
                    }
                    else
                    {
                        len = c == 'a' ? 1 : 0;
                        prev = 'a';
                    }
                }
                else
                {
                    if (c == 'a') len++;
                }
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

        ///1846. Maximum Element After Decreasing and Rearranging
        //any times of rearrange and decrease, |arr[n]- arr[n-1]|<= 1, return the max
        public int MaximumElementAfterDecrementingAndRearranging(int[] arr)
        {
            Array.Sort(arr);
            arr[0] = 1;
            int n = arr.Length;
            int curr = 1;
            for(int i = 1; i < n; i++)
            {
                if (arr[i] - curr <= 1) curr = arr[i];
                else curr++;
            }
            return curr;
        }

        ///1848. Minimum Distance to the Target Element
        //min of abs(i-start), nums[i]==target
        public int GetMinDistance(int[] nums, int target, int start)
        {
            int res = int.MaxValue;
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == target)
                    res = Math.Min(res, Math.Abs(i - start));
            }
            return res;
        }
    }
}