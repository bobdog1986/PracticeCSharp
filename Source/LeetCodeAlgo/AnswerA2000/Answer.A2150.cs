﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2150. Find All Lonely Numbers in the Array
        ///You are given an integer array nums. A number x is lonely when it appears only once,
        ///and no adjacent numbers (i.e. x + 1 and x - 1) appear in the array.
        ///Return all lonely numbers in nums.You may return the answer in any order.
        ///0 <= nums[i] <= 10^6
        public IList<int> FindLonely(int[] nums)
        {
            List<int> res = new List<int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (dict.ContainsKey(n)) dict[n]++;
                else dict.Add(n, 1);
            }
            foreach (var key in dict.Keys)
            {
                if (dict[key] == 1 && !dict.ContainsKey(key - 1) && !dict.ContainsKey(key + 1))
                    res.Add(key);
            }
            return res;
        }

        ///2154. Keep Multiplying Found Values by Two
        ///If original is found in nums, multiply it by two (i.e., set original = 2 * original).
        ///Otherwise, stop the process.Repeat this process with the new number as long as you keep finding the number.
        ///Return the final value of original.
        public int FindFinalValue(int[] nums, int original)
        {
            HashSet<int> set = new HashSet<int>(nums);
            while (set.Contains(original))
                original += original;
            return original;
        }

        ///2155. All Divisions With the Highest Score of a Binary Array
        ///The division score of an index i is the sum of the number of 0's in numsleft and the number of 1's in numsright.
        ///Return all distinct indices that have the highest possible division score.
        public IList<int> MaxScoreIndices(int[] nums)
        {
            int[] arr = new int[nums.Length + 1];
            int zeros = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                arr[i] = zeros;
                if (nums[i] == 0) zeros++;
            }
            arr[nums.Length] = zeros;
            int ones = nums.Length - zeros;
            int max = 0;
            var res = new List<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                int divide = arr[i] + ones - (i - arr[i]);
                if (divide == max) res.Add(i);
                else if (divide > max)
                {
                    res.Clear();
                    res.Add(i);
                    max = divide;
                }
            }

            return res;
        }

        ///2157. Groups of Strings, #Union Find, #Bit Manipulation
        //Two strings connected if the set of letters of s2 can be obtained by: Add 1 /Delete /Replace 1 char
        //Return array ans where:ans[0] is the maximum number of groups, ans[1] is the size of the largest group.
        public int[] GroupStrings(string[] words)
        {
            int n = words.Length;
            var uf = new UnionFind(n);

            var dict = new Dictionary<int, Dictionary<int, int>>();//store {len, {bit, index}} pairs
            for (int i = 0; i < n; i++)
            {
                int len = words[i].Length;
                int bit = 0;
                foreach (var c in words[i])
                    bit |= 1 << (c - 'a');
                if (!dict.ContainsKey(len))
                    dict.Add(len, new Dictionary<int, int>());
                if (dict[len].ContainsKey(bit))
                    uf.Union(dict[len][bit], i);//union duplicates
                else dict[len].Add(bit, i);
            }
            //union all replace pairs of same length
            foreach (var i in dict.Keys)
            {
                var keys = dict[i].Keys.ToList();
                for (int k1 = 0; k1 < keys.Count - 1; k1++)
                {
                    for (int k2 = k1 + 1; k2 < keys.Count; k2++)
                    {
                        if (GroupStrings_Ones(keys[k1] ^ keys[k2]) == 2)
                            uf.Union(dict[i][keys[k1]], dict[i][keys[k2]]);
                    }
                }
            }
            //union all delete pairs of 1 length diff
            //this is very important to reduce O(n1*n2) to O(n1*26), n1,n2 is count of same length words of i,i-1
            for (int i = 1; i <= 26; i++)
            {
                if (!dict.ContainsKey(i) || !dict.ContainsKey(i - 1)) continue;
                foreach (var num in dict[i].Keys)
                {
                    for (int j = 0; j < 26; j++)
                    {
                        if ((num & (1 << j)) != 0)// if current bit is 1
                        {
                            int delete = num & (~(1 << j));//try to find if delete exist
                            if (dict[i - 1].ContainsKey(delete))
                                uf.Union(dict[i][num], dict[i - 1][delete]);
                        }
                    }
                }
            }
            int maxCount = 0;
            HashSet<int> indexSet = new HashSet<int>();
            int[] arr = new int[n];
            for (int i = 0; i < n; i++)
            {
                int k = uf.Find(i);
                indexSet.Add(k);
                maxCount = Math.Max(maxCount, ++arr[k]);
            }
            return new int[] { indexSet.Count, maxCount };
        }

        private int GroupStrings_Ones(int n)
        {
            int count = 0;
            int seed = 1;
            for (int i = 0; i < 26 && count <= 2; i++)
            {
                if ((n & (seed)) != 0) count++;
                seed <<= 1;
            }
            return count;
        }

        ///2160. Minimum Sum of Four Digit Number After Splitting Digits
        ///You are given a positive integer num consisting of exactly four digits.
        ///Split num into two new integers new1 and new2 by using the digits found in num.
        ///Leading zeros are allowed in new1 and new2, and all the digits found in num must be used.
        ///Return the minimum possible sum of new1 and new2.
        public int MinimumSum(int num)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                var curr = num % 10;
                if (curr > 0)
                    list.Add(curr);
                num /= 10;
            }
            list.Sort();
            int sum1 = 0;
            int sum2 = 0;
            for (var i = 0; i < list.Count; i++)
            {
                sum1 = sum1 * 10 + list[i++];
                if (i >= list.Count)
                    break;
                sum2 = sum2 * 10 + list[i++];
            }
            return sum1 + sum2;
        }

        /// 2161. Partition Array According to Given Pivot
        ///smaller than pivot on left, same as pivot on mid, larger on right
        public int[] PivotArray(int[] nums, int pivot)
        {
            int left = 0;
            int mid = 0;
            int right = 0;
            int[] arr = new int[nums.Length];
            int[] res = new int[nums.Length];
            foreach (var n in nums)
            {
                if (n < pivot) res[left++] = n;
                else if (n == pivot) mid++;
                else arr[right++] = n;
            }
            while (mid-- > 0)
            {
                res[left++] = pivot;
            }
            int j = 0;
            while (j < right)
            {
                res[left + j] = arr[j];
                j++;
            }
            return res;
        }

        ///2162. Minimum Cost to Set Cooking Time
        public int MinCostSetTime(int startAt, int moveCost, int pushCost, int targetSeconds)
        {
            int minutes = targetSeconds / 60;
            int second = targetSeconds % 60;

            //if targetSeconds>=6000, should be 99:60 etc.
            if (minutes >= 100)
            {
                minutes--;
                second += 60;
            }

            int res = MinCostSetTime(startAt, moveCost, pushCost, minutes, second);
            //if exist another minutes:seconds combine
            if (minutes > 0 && second < 40)
                res = Math.Min(res, MinCostSetTime(startAt, moveCost, pushCost, minutes - 1, second + 60));
            return res;
        }

        private int MinCostSetTime(int startAt, int moveCost, int pushCost, int minutes, int seconds)
        {
            //if over flow ,return int.MaxValue
            if (minutes >= 100 || seconds >= 100) return int.MaxValue;
            int res = 0;
            if (minutes > 0)
            {
                if (minutes >= 10)
                {
                    if (startAt != minutes / 10) res += moveCost;
                    res += pushCost;
                    startAt = minutes / 10;
                    minutes %= 10;
                }

                if (startAt != minutes) res += moveCost;
                res += pushCost;
                startAt = minutes;

                if (startAt != seconds / 10) res += moveCost;
                res += pushCost;
                startAt = seconds / 10;
                seconds %= 10;

                if (startAt != seconds) res += moveCost;
                res += pushCost;
            }
            else
            {
                if (seconds > 0)
                {
                    if (seconds >= 10)
                    {
                        if (startAt != seconds / 10) res += moveCost;
                        res += pushCost;
                        startAt = seconds / 10;
                        seconds %= 10;
                    }
                    if (startAt != seconds) res += moveCost;
                    res += pushCost;
                }
                else
                {
                    if (startAt != seconds) res += moveCost;
                    res += pushCost;
                }
            }
            return res;
        }

        ///2163. Minimum Difference in Sums After Removal of Elements, #PriorityQueue, #Prefix Sum
        ///Return the minimum difference possible between the sums of the two parts after the removal of n elements.
        ///nums.length == 3 * n,1 <= n <= 10^5 ,1 <= nums[i] <= 10^5
        public long MinimumDifference(int[] nums)
        {
            long res = long.MaxValue;
            int m = nums.Length;
            int n = m / 3;

            long maxSum = 0;
            long sum1 = 0;
            long[] prefixSum1 = new long[m];//max removed sum from 0 to i (inclusive), [0,i]
            PriorityQueue<long, long> maxHeap = new PriorityQueue<long, long>();
            for (int i = 0; i < n; i++)
            {
                sum1 += nums[i];
                maxHeap.Enqueue(nums[i], -nums[i]);
            }
            prefixSum1[n - 1] = sum1;//sum1-0
            //i must <m-1 due to right side [m-n,m-1] contain n elements
            for (int i = n; i < m - n; i++)
            {
                sum1 += nums[i];
                maxHeap.Enqueue(nums[i], -nums[i]);
                maxSum += maxHeap.Dequeue();
                prefixSum1[i] = sum1 - maxSum;
            }

            long sum2 = 0;
            long minSum = 0;
            long[] prefixSum2 = new long[m];//min removed sum from i(exclusive) to m-1 , (i,m-1]
            PriorityQueue<long, long> minHeap = new PriorityQueue<long, long>();
            for (int i = m - 1; i >= m - n; i--)
            {
                sum2 += nums[i];
                minHeap.Enqueue(nums[i], nums[i]);
            }
            //i must >=n-1 ,due to left side [0,n-1] contains n elements
            for (int i = m - n - 1; i >= n - 1; i--)
            {
                prefixSum2[i] = sum2 - minSum;
                sum2 += nums[i];
                minHeap.Enqueue(nums[i], nums[i]);
                minSum += minHeap.Dequeue();
            }
            //i must >=n-1 and i<m-n to ensure left and right both atleast contain n elements
            for (int i = n - 1; i < m - n; i++)
            {
                res = Math.Min(res, prefixSum1[i] - prefixSum2[i]);
            }
            return res;
        }

        ///2164. Sort Even and Odd Indices Independently, #PriorityQueue,
        ///Sort the values at odd indices of nums in non-increasing order., Even indices in non-decreasing
        public int[] SortEvenOdd(int[] nums)
        {
            int[] res = new int[nums.Length];
            PriorityQueue<int, int> q1 = new PriorityQueue<int, int>();
            PriorityQueue<int, int> q2 = new PriorityQueue<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i % 2 == 0)
                    q2.Enqueue(nums[i], nums[i]);
                else
                    q1.Enqueue(nums[i], -nums[i]);
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (i % 2 == 0)
                    res[i] = q2.Dequeue();
                else
                    res[i] = q1.Dequeue();
            }

            return res;
        }

        /// 2165. Smallest Value of the Rearranged Number
        ///Rearrange the digits of num such that its value is minimized and it does not contain any leading zeros.
        ///Return the rearranged number with minimal value. the sign does not change after rearranging the digits.
        ///-10^15 <=x <= 10^15
        public long SmallestNumber(long num)
        {
            if (num <= 10 && num >= -10) return num;
            long res = 0;
            long sign = num < 0 ? -1 : 1;

            var str = Math.Abs(num).ToString();
            var arr = str.OrderBy(x => sign * x).ToArray();

            if (sign == 1)
            {
                int i = 0;
                while (i < arr.Length)
                {
                    if (arr[i] != '0') break;
                    i++;
                }

                var temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
            }
            res = long.Parse(new string(arr));
            return sign * res;
        }

        ///2166. Design Bitset, see Bitset

        ///2168. Unique Substrings With Equal Digit Frequency
        //Given a digit string s, return the number of unique substrings of s where every digit appears the same number of times
        public int EqualDigitFrequency(string s)
        {
            HashSet<string> set = new HashSet<string>();
            int n = s.Length;
            for (int i = 0; i<n; i++)
            {
                int[] arr = new int[10];
                for (int j = i; j<n; j++)
                {
                    arr[s[j]-'0']++;
                    bool valid = true;
                    int prev = -1;
                    foreach (var k in arr)
                    {
                        if (k>0)
                        {
                            if (prev==-1) prev = k;
                            else
                            {
                                if (k!=prev)
                                {
                                    valid = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (valid) set.Add(s.Substring(i, j-i+1));
                }
            }
            return set.Count();
        }

        /// 2169. Count Operations to Obtain Zero
        public int CountOperations(int num1, int num2)
        {
            int res = 0;
            while (num1 != 0 && num2 != 0)
            {
                if (num1 >= num2) num1 -= num2;
                else num2 -= num1;
                res++;
            }
            return res;
        }

        ///2170. Minimum Operations to Make the Array Alternating
        public int MinimumOperations(int[] nums)
        {
            if (nums.Length <= 1) return 0;
            Dictionary<int, int> evenDict = new Dictionary<int, int>();
            Dictionary<int, int> oddDict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    if (evenDict.ContainsKey(nums[i])) evenDict[nums[i]]++;
                    else evenDict.Add(nums[i], 1);
                }
                else
                {
                    if (oddDict.ContainsKey(nums[i])) oddDict[nums[i]]++;
                    else oddDict.Add(nums[i], 1);
                }
            }
            int lenOfEven = (nums.Length + 1) / 2;
            int lenOfOdd = nums.Length - lenOfEven;
            var evenKeys = evenDict.Keys.OrderBy(x => -evenDict[x]).ToList();
            var oddKeys = oddDict.Keys.OrderBy(x => -oddDict[x]).ToList();
            int res = int.MaxValue;
            if (evenKeys.Count == 1 && oddKeys.Count == 1)
            {
                if (evenKeys[0] == oddKeys[0]) res = oddDict[oddKeys[0]];
                else res = 0;
            }
            else if (evenKeys.Count == 1 || oddKeys.Count == 1)
            {
                if (evenKeys.Count == 1)
                {
                    if (evenKeys[0] == oddKeys[0]) res = lenOfOdd - oddDict[oddKeys[1]];
                    else res = lenOfOdd - oddDict[oddKeys[0]];
                }
                else
                {
                    if (evenKeys[0] == oddKeys[0]) res = Math.Min(lenOfEven - evenDict[evenKeys[0]] + lenOfOdd, lenOfEven - evenDict[evenKeys[1]]);
                    else res = lenOfEven - evenDict[evenKeys[0]];
                }
            }
            else
            {
                if (evenKeys[0] == oddKeys[0])
                {
                    res = Math.Min(lenOfEven - evenDict[evenKeys[0]] + lenOfOdd - oddDict[oddKeys[1]],
                                    lenOfEven - evenDict[evenKeys[1]] + lenOfOdd - oddDict[oddKeys[0]]);
                }
                else
                {
                    res = lenOfEven - evenDict[evenKeys[0]] + lenOfOdd - oddDict[oddKeys[0]];
                }
            }
            return res;
        }

        /// 2171. Removing Minimum Number of Magic Beans, #Prefix Sum
        public long MinimumRemoval(int[] beans)
        {
            Array.Sort(beans);
            long sum = 0;
            foreach (var bean in beans)
                sum += bean;

            long res = long.MaxValue;
            long m = beans.Length;
            for (int i = 0; i < beans.Length; i++, m--)
            {
                res = Math.Min(res, sum - m * beans[i]);
            }
            return res;
        }
        ///2172. Maximum AND Sum of Array
        public int MaximumANDSum(int[] nums, int numSlots)
        {
            int n = nums.Length;
            int res = 0;
            int[][][] dp = new int[n][][];
            for (int i = 0; i < n; i++)
            {
                dp[i] = new int[numSlots * 2 + 2][];
                for (int j = 0; j < dp[i].Length; j++)
                    dp[i][j] = new int[2];
            }

            for (int i = 2; i < numSlots * 2 + 2; i++)
            {
                dp[0][i][1] = (i / 2 & nums[0]);
            }

            for (int i = 1; i < n; i++)
            {
                for (int j = 2; j < dp[i].Length; j++)
                {
                    for (int k = 2; k < dp[i].Length; k++)
                    {
                        if (j == k)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
            return dp[n - 1].Max(x => x.Max());
        }


        /// 2176. Count Equal and Divisible Pairs in an Array
        ///return the number of pairs (i, j) where 0 <= i < j < n, such that nums[i] == nums[j] and (i * j) is divisible by k.
        public int CountPairs_2176(int[] nums, int k)
        {
            int res = 0;
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i])) dict.Add(nums[i], new List<int>());
                dict[nums[i]].Add(i);
            }
            foreach (var key in dict.Keys)
            {
                if (dict[key].Count >= 2)
                {
                    for (int i = 0; i < dict[key].Count - 1; i++)
                    {
                        for (int j = i + 1; j < dict[key].Count; j++)
                        {
                            if (dict[key][i] * dict[key][j] % k == 0) res++;
                        }
                    }
                }
            }
            return res;
        }

        ///2177. Find Three Consecutive Integers That Sum to a Given Number
        public long[] SumOfThree(long num)
        {
            return num % 3 == 0 ? new long[] { num / 3 - 1, num / 3, num / 3 + 1 } : new long[] { };
        }

        ///2178. Maximum Split of Positive Even Integers
        public IList<long> MaximumEvenSplit(long finalSum)
        {
            if (finalSum % 2 == 1) return new List<long>();

            var res = new HashSet<long>();
            int seed = 2;
            while (finalSum > 0)
            {
                if (finalSum >= seed)
                {
                    res.Add(seed);
                    finalSum -= seed;
                    seed += 2;
                }
                else
                {
                    res.Remove(seed - 2);
                    res.Add(finalSum + seed - 2);
                    break;
                }
            }
            return res.ToList();
        }

        /// 2180. Count Integers With Even Digit Sum, nums<=1000
        public int CountEven(int num)
        {
            ///10 = 2, 4, 6, 8(4)
            ///20 = 2, 4, 6, 8, 11, 13, 15, 17, 19, 20(10)
            ///30 = 2, 4, 6, 8, 11, 13, 15, 17, 19, 20, 22, 24, 26, and 28. (14)
            int temp = num, sum = 0;
            while (num > 0)
            {
                sum += num % 10;
                num /= 10;
            }
            return sum % 2 == 0 ? temp / 2 : (temp - 1) / 2;
        }

        ///2181. Merge Nodes in Between Zeros
        public ListNode MergeNodes(ListNode head)
        {
            if (head == null || head.next == null) return null;
            var node = head.next;
            int sum = 0;
            while (node != null && node.val != 0)
            {
                sum += node.val;
                node = node.next;
            }
            var res = new ListNode(sum);
            res.next = MergeNodes(node);
            return res;
        }

        ///2182. Construct String With Repeat Limit
        ///Return the lexicographically largest repeatLimitedString possible.
        public string RepeatLimitedString(string s, int repeatLimit)
        {
            int[] arr = new int[26];
            foreach (var c in s)
                arr[c - 'a']++;
            List<char> list = new List<char>();
            bool isStop = false;
            int maxIndex = 25;
            int minIndex = 0;
            while (!isStop)
            {
                bool existHigh = false;
                while (arr[maxIndex] == 0)
                    maxIndex--;
                while (arr[minIndex] == 0)
                    minIndex++;

                for (int i = maxIndex; i >= minIndex; i--)
                {
                    if (arr[i] == 0) continue;
                    if (existHigh)
                    {
                        list.Add((char)('a' + i));
                        arr[i]--;
                        break;
                    }
                    else
                    {
                        int j = Math.Min(repeatLimit, arr[i]);
                        arr[i] -= j;
                        while (j-- > 0)
                            list.Add((char)('a' + i));

                        existHigh = arr[i] > 0;
                        if (i == minIndex)
                        {
                            isStop = true;
                            break;
                        }
                    }
                }
                if (list.Count == s.Length)
                    isStop = true;
            }
            return new string(list.ToArray());
        }

        ///2183. Count Array Pairs Divisible by K, #GCD
        //  return the number of pairs(i, j) such that:
        //0 <= i<j <= n - 1 and nums[i] * nums[j] is divisible by k.
        public long CountPairs(int[] nums, int k)
        {
            // nums[i] * nums[j] % k==0 => gcd(nums[i] ,k) * gcd(nums[j] ,k) %k==0
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (dict.ContainsKey(n)) dict[n]++;
                else dict.Add(n, 1);
            }

            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach (var n in dict.Keys)
            {
                var g = getGCD(n, k);
                if (map.ContainsKey(g)) map[g] += dict[n];
                else map.Add(g, dict[n]);
            }
            long res = 0;
            int[] keys = map.Keys.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                if ((long)keys[i] * keys[i] % k == 0)
                {
                    res += (long)map[keys[i]] * (map[keys[i]] - 1) / 2;
                }

                for (int j = i + 1; j < keys.Length; j++)
                {
                    if ((long)keys[i] * keys[j] % k == 0)
                    {
                        res += (long)map[keys[i]] * map[keys[j]];
                    }
                }
            }
            return res;
        }

        /// 2185. Counting Words With a Given Prefix
        public int PrefixCount(string[] words, string pref)
        {
            return words.Where(word => word.StartsWith(pref)).Count();
        }

        ///2186. Minimum Number of Steps to Make Two Strings Anagram II
        ///You are given two strings s and t. In one step, you can append any character to either s or t.
        ///Return the minimum number of steps to make s and t anagrams of each other.
        ///An anagram of a string is a string that contains the same characters with a different(or the same) ordering.
        public int MinSteps(string s, string t)
        {
            int[] arr = new int[26];
            foreach (var c in s)
                arr[c - 'a']++;
            foreach (var c in t)
                arr[c - 'a']--;
            return arr.Sum(x => Math.Abs(x));
        }

        ///2187. Minimum Time to Complete Trips, #Binary Search
        // Return the minimum time required for all buses to complete at least totalTrips trips.
        // public long MinimumTime(int[] time, int totalTrips)
        // {
        //     int n = time.Length;
        //     Array.Sort(time);
        //     long left = 1;
        //     long right = long.MaxValue;
        //     while (left < right)
        //     {
        //         long mid = left + (right - left) / 2;
        //         long sum = 0;
        //         for (int i = 0; i < n; i++)
        //         {
        //             sum += mid / time[i];
        //             if (sum >= totalTrips) break;
        //         }
        //         if (sum >= totalTrips)
        //             right = mid;
        //         else left = mid + 1;

        //     }
        //     return left;
        // }

        ///2188. Minimum Time to Finish the Race, #DP
        //xth successive lap in fi * ri(x-1) seconds.
        //1 <= tires.length <= 105, 1 <= fi, changeTime <= 105,2 <= ri <= 105, 1 <= numLaps <= 1000
        public int MinimumFinishTime(int[][] tires, int changeTime, int numLaps)
        {
            tires = tires.OrderBy(x => x[0]).ThenBy(x => x[1]).ToArray();//sort by fi then by ri
            List<int[]> goodTires = new List<int[]>();//only select good tires to avoid TLE
            int[] prev = tires[0];
            goodTires.Add(tires[0]);
            for (int i = 1; i < tires.Length; i++)
            {
                if (tires[i][1] >= prev[1]) continue;
                else
                {
                    goodTires.Add(tires[i]);
                    prev = tires[i];
                }
            }
            int n = goodTires.Count;
            int minTire = goodTires.Min(x => x[0]);
            int maxUsed = 0;
            int LEN = 20;//1*2^20 >=1_000_000 , we should never use a tire continuous 20 laps
            int[][] timeMat = new int[n][];//create a tire cost table for future query
            for (int i = 0; i < n; i++)
            {
                timeMat[i] = new int[LEN];
                Array.Fill(timeMat[i], int.MaxValue);
                timeMat[i][0] = goodTires[i][0];
                for (int j = 1; j < LEN; j++)
                {
                    int curr = timeMat[i][j - 1] * goodTires[i][1];
                    if (curr >= changeTime + minTire)//we should better replace a new tire with same tireID
                        break;
                    maxUsed = Math.Max(maxUsed, j);
                    timeMat[i][j] = curr;
                }
            }
            int[][][] dp = new int[numLaps][][];
            for (int i = 0; i < numLaps; i++)
            {
                dp[i] = new int[n][];
                for (int j = 0; j < n; j++)
                {
                    dp[i][j] = new int[LEN];
                    Array.Fill(dp[i][j], int.MaxValue);
                    if (i == 0)
                        dp[i][j][0] = goodTires[j][0];//seed data
                }
            }
            for (int i = 0; i < numLaps - 1; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //why k<=i ? because in lap-i, every time will be used at most i times
                    for (int k = 0; k <= i && k < LEN && k <= maxUsed; k++)
                    {
                        //many data in dp[][][] is int.MaxValue, means it no need to handle
                        if (dp[i][j][k] == int.MaxValue) continue;
                        for (int m = 0; m < n; m++)
                        {
                            //try change to a new tire with ID=m, so it's dp[i + 1][m][0]
                            //current cost is dp[i][j][k] , we should add  changeTime + timeMat[m][0]
                            dp[i + 1][m][0] = Math.Min(dp[i + 1][m][0], dp[i][j][k] + changeTime + timeMat[m][0]);
                        }
                        if (timeMat[j][k + 1] == int.MaxValue) continue;//no need to continue , we should better change a new tire
                        dp[i + 1][j][k + 1] = Math.Min(dp[i + 1][j][k + 1], dp[i][j][k] + timeMat[j][k + 1]);
                    }
                }
            }
            return dp.Last().Min(x => x.Min());
        }

        /// 2190. Most Frequent Number Following Key In an Array
        ///0 <= i <= n - 2, nums[i] == key and, nums[i + 1] == target.
        ///Return the target with the maximum count.
        public int MostFrequent(int[] nums, int key)
        {
            int max = 0;
            int res = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == key)
                {
                    if (map.ContainsKey(nums[i + 1])) map[nums[i + 1]]++;
                    else map.Add(nums[i + 1], 1);
                    if (map[nums[i + 1]] > max)
                    {
                        max = map[nums[i + 1]];
                        res = nums[i + 1];
                    }
                }
            }
            return res;
        }

        ///2191. Sort the Jumbled Numbers
        ///Transfer digits according to mapping array, then sort
        public int[] SortJumbled(int[] mapping, int[] nums)
        {
            return nums.OrderBy(x => SortJumbled(x, mapping)).ToArray();
        }

        public int SortJumbled(int n, int[] mapping)
        {
            if (n < 10) return mapping[n];//avoid n==0 issue
            int res = 0;
            int x = 1;
            while (n > 0)
            {
                res += mapping[n % 10] * x;
                n /= 10;
                x *= 10;
            }
            return res;
        }

        ///2192. All Ancestors of a Node in a Directed Acyclic Graph, #Graph, #DFS
        ///edges[i] = [fromi, toi] denotes that there is a unidirectional edge from fromi to toi in the graph.
        ///Return a list answer, where answer[i] is the list of ancestors of the ith node, sorted in ascending order.
        public IList<IList<int>> GetAncestors(int n, int[][] edges)
        {
            IList<int>[] res = new List<int>[n];
            bool[][] graph = new bool[n][];
            for (int i = 0; i < n; i++)
                graph[i] = new bool[n];

            foreach (var edge in edges)
                graph[edge[1]][edge[0]] = true;

            for (int i = 0; i < n; i++)
            {
                bool[] parent = new bool[n];
                GetAncestors_DFS(graph, i, parent, res);
                List<int> list = new List<int>();
                for (int j = 0; j < n; j++)
                {
                    if (parent[j])
                        list.Add(j);
                }
                res[i] = list;
            }
            return res.ToList();
        }

        private void GetAncestors_DFS(bool[][] graph, int index, bool[] parent, IList<int>[] res)
        {
            for (int i = 0; i < graph[index].Length; i++)
            {
                if (graph[index][i] && !parent[i])
                {
                    if (res[i] != null)
                    {
                        parent[i] = true;
                        foreach (var j in res[i])
                            parent[j] = true;
                    }
                    else
                    {
                        parent[i] = true;
                        GetAncestors_DFS(graph, i, parent, res);
                    }
                }
            }
        }

        ///2194. Cells in a Range on an Excel Sheet
        public IList<string> CellsInRange(string s)
        {
            var res = new List<string>();
            for (char c = s[0]; c <= s[3]; c++)
            {
                for (char i = s[1]; i <= s[4]; i++)
                {
                    res.Add($"{c}{i}");
                }
            }
            return res;
        }

        /// 2195. Append K Integers With Minimal Sum, #PriorityQueue,
        ///You are given an integer array nums and an integer k.
        ///Append k unique positive integers that do not appear in nums to nums such that the resulting total sum is minimum.
        ///Return the sum of the k integers appended to nums.
        public long MinimalKSum(int[] nums, int k)
        {
            long res = 0;
            PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();
            foreach (var n in nums)
                priorityQueue.Enqueue(n, n);//min heap

            int lastIndex = 0;
            while (priorityQueue.Count > 0)
            {
                int currIndex = priorityQueue.Dequeue();
                if (lastIndex == currIndex) continue;
                else
                {
                    //how many nums in range [lastIndex + 1,currIndex - 1], inclusive
                    int count = currIndex - 1 - (lastIndex + 1) + 1;
                    if (count >= k) count = k;//if exceed k, only append k nums
                    res += (lastIndex + 1 + lastIndex + count) * (long)count / 2;
                    k -= count;
                    lastIndex = currIndex;//update lastIndex
                }
            }

            if (k > 0)// if still k>0
            {
                res += (lastIndex + 1 + lastIndex + k) * (long)k / 2;
            }

            return res;
        }

        /// 2196. Create Binary Tree From Descriptions
        ///descriptions[i] = [parenti, childi, isLefti] indicates that parenti is the parent of childi in a binary tree of unique values. Furthermore,
        ///If isLefti == 1, then childi is the left child of parenti.
        ///If isLefti == 0, then childi is the right child of parenti.
        ///Construct the binary tree described by descriptions and return its root.
        public TreeNode CreateBinaryTree(int[][] descriptions)
        {
            //set contains all nodes,but only the root's value is true
            Dictionary<TreeNode, bool> set = new Dictionary<TreeNode, bool>();
            Dictionary<int, TreeNode> dict = new Dictionary<int, TreeNode>();
            foreach (var desc in descriptions)
            {
                TreeNode parent = null;
                if (!dict.ContainsKey(desc[0])) dict.Add(desc[0], new TreeNode(desc[0]));
                parent = dict[desc[0]];
                if (!set.ContainsKey(parent)) set.Add(parent, true);

                TreeNode child = null;
                if (!dict.ContainsKey(desc[1])) dict.Add(desc[1], new TreeNode(desc[1]));
                child = dict[desc[1]];
                if (!set.ContainsKey(child)) set.Add(child, false);
                else set[child] = false;

                if (desc[2] == 1) parent.left = child;
                else parent.right = child;
            }

            return set.FirstOrDefault(x => x.Value).Key;
        }

        ///2197. Replace Non-Coprime Numbers in Array, #Monotonic Stack
        //If any two adjacent numbers in nums that are non-coprime( aka GCD is 1)
        //combine this two numbers to their LCM
        //return the final elements in nums
        public IList<int> ReplaceNonCoprimes(int[] nums)
        {
            int n = nums.Length;
            int[] arr = new int[n];//donot use stack , it may TLE
            int j = -1;
            for (int i = 0; i < n; i++)
            {
                int curr = nums[i];//we need a local var, it may be updated to the LCM later
                while (j >= 0 && getGCD(arr[j], curr) > 1)//if current number is non-coprime with last one
                {
                    curr = getLCM(arr[j--], curr);//delete them and create their LCM
                }
                arr[++j] = curr;
            }
            return arr.Take(j + 1).ToList();//j+1 = final count of elements in nums
        }
    }
}