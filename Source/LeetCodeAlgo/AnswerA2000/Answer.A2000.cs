﻿using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;
using System.Text;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2000. Reverse Prefix of Word
        ///reverse the segment of word that starts at index 0 and ends at the index of the first occurrence of ch (inclusive).
        ///If the character ch does not exist in word, do nothing.
        public string ReversePrefix(string word, char ch)
        {
            List<char> list = new List<char>();
            for (int i = 0; i < word.Length; i++)
            {
                list.Add(word[i]);
                if (ch == word[i])
                {
                    list.Reverse();
                    return new string(list.ToArray()) + word.Substring(i + 1);
                }
            }
            return word;
        }

        /// 2001. Number of Pairs of Interchangeable Rectangles
        public long InterchangeableRectangles(int[][] rectangles)
        {
            long sum = 0;
            Dictionary<string, long> pairs = new Dictionary<string, long>();
            foreach (var rect in rectangles)
            {
                var gcb = getGCD(rect[0], rect[1]);
                var key = rect[0] / gcb + ":" + rect[1] / gcb;
                if (pairs.ContainsKey(key))
                    pairs[key]++;
                else
                    pairs.Add(key, 1);
            }
            foreach (var pair in pairs)
            {
                if (pair.Value > 1)
                {
                    sum += pair.Value * (pair.Value - 1) / 2;
                }
            }
            return sum;
        }

        ///2002. Maximum Product of the Length of Two Palindromic Subsequences, #Backtracking, #DP
        //2 <= s.length <= 12
        public int MaxProduct(string s)
        {
            int res = 0;
            MaxProduct_DFS(s, 0, new bool[s.Length], ref res);
            return res;
        }

        private void MaxProduct_DFS(string s, int index, bool[] visit, ref int res)
        {
            if (index == s.Length)
            {
                var list1 = new List<char>();
                var list2 = new List<char>();
                for (int i = 0; i < s.Length; i++)
                {
                    if (visit[i]) list1.Add(s[i]);
                    else list2.Add(s[i]);
                }
                //using existed other question
                var len1 = LongestPalindromeSubseq(new string(list1.ToArray()));
                var len2 = LongestPalindromeSubseq(new string(list2.ToArray()));
                res = Math.Max(res, len1 * len2);
            }
            else
            {
                visit[index] = false;
                MaxProduct_DFS(s, index + 1, visit, ref res);
                visit[index] = true;
                MaxProduct_DFS(s, index + 1, visit, ref res);
            }
        }

        ///2006. Count Number of Pairs With Absolute Difference K
        ///return the number of pairs (i, j) where i < j such that |nums[i] - nums[j]| == k.
        ///1 <= k <= 99, 1 <= nums[i] <= 100, 1 <= nums.length <= 200
        public int CountKDifference(int[] nums, int k)
        {
            if (nums.Length <= 1)
                return 0;
            int[] arr = new int[101];
            int start = 100;
            int end = 1;
            foreach (var num in nums)
            {
                arr[num]++;
                start = Math.Min(start, num);
                end = Math.Max(end, num);
            }
            int ans = 0;
            for (int i = start; i <= end - k; i++)
            {
                if (arr[i] > 0 && arr[i + k] > 0)
                {
                    ans += arr[i] * arr[i + k];
                }
            }
            return ans;
        }

        ///2007. Find Original Array From Doubled Array
        public int[] FindOriginalArray(int[] changed)
        {
            int n = changed.Length;
            var res = new List<int>();
            if ((n & 1) == 1) return Array.Empty<int>();
            var dict = new Dictionary<int, int>();
            foreach (var i in changed)
            {
                if (dict.ContainsKey(i)) dict[i]++;
                else dict.Add(i, 1);
            }

            var keys = dict.Keys.OrderBy(x => x).ToArray();
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == 0 || dict[keys[i]] == 0) continue;
                if (dict.ContainsKey(2 * keys[i]))
                {
                    if (dict[2 * keys[i]] < dict[keys[i]]) return Array.Empty<int>();
                    else
                    {
                        res.AddRange(Enumerable.Repeat(keys[i], dict[keys[i]]));
                        dict[2 * keys[i]] -= dict[keys[i]];
                        //dict[keys[i]]=0;
                    }
                }
                else return Array.Empty<int>();
            }
            if (res.Count < n / 2)
            {
                res.AddRange(Enumerable.Repeat(0, n / 2 - res.Count));
            }
            return res.ToArray();
        }

        ///2008. Maximum Earnings From Taxi, #DP
        // The n points on the road are labeled from 1 to n , there are rides, rides[i] = [starti, endi, tipi]
        //For each passenger i you pick up, earn endi - starti + tipi . only drive at most one passenger at a time.
        //Given n and rides, return the maximum number of dollars you can earn by picking up the passengers optimally.
        public long MaxTaxiEarnings(int n, int[][] rides)
        {
            rides = rides.OrderBy(x => x[0]).ToArray();
            long[] dp = new long[n + 1];
            int j = 0;
            for (int i = 1; i <= n; ++i)
            {
                dp[i] = Math.Max(dp[i], dp[i - 1]);
                while (j < rides.Length && rides[j][0] <= i)
                {
                    dp[rides[j][1]] = Math.Max(dp[rides[j][1]], dp[i] + rides[j][1] - rides[j][0] + rides[j][2]);
                    j++;
                }
            }
            return dp[n];
        }

        ///2011. Final Value of Variable After Performing Operations
        public int FinalValueAfterOperations(string[] operations)
        {
            return operations.Sum(x => x.Contains("++") ? 1 : -1);
        }

        ///2012. Sum of Beauty in the Array, #Prefix Sum
        public int SumOfBeauties(int[] nums)
        {
            int n = nums.Length;
            int[] prefixMax = new int[n];
            int[] suffixMin = new int[n];
            int max = int.MinValue;
            int min = int.MaxValue;
            for (int i = 0; i < n; i++)
            {
                max = Math.Max(max, nums[i]);
                min = Math.Min(min, nums[n - 1 - i]);
                prefixMax[i] = max;
                suffixMin[n - 1 - i] = min;
            }
            int res = 0;
            for (int i = 1; i < n - 1; i++)
            {
                if (nums[i] > prefixMax[i - 1] && nums[i] < suffixMin[i + 1]) res += 2;
                else if (nums[i] > nums[i - 1] && nums[i] < nums[i + 1]) res += 1;
            }
            return res;
        }

        ///2016. Maximum Difference Between Increasing Elements
        // find the maximum of ( nums[j] - nums[i]), such that 0 <= i < j < n and nums[i] < nums[j].
        // Return the maximum difference.If no such i and j exists, return -1
        public int MaximumDifference(int[] nums)
        {
            int res = -1;
            int n = nums.Length;
            int max = nums[n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                if (nums[i] < max && max - nums[i] > res)
                {
                    res = max - nums[i];
                }
                max = Math.Max(max, nums[i]);
            }
            return res;
        }

        ///2017. Grid Game, #Prefix Sum
        //2-row matrix, robot1 take all visited cell to 0, robot2 take the max path of left cells
        //only go right or down, return minimium of robot2's score
        public long GridGame(int[][] grid)
        {
            long res = long.MaxValue;
            int n = grid[0].Length;
            long[] prefixSum0 = new long[n];
            long[] prefixSum1 = new long[n];
            long sum0 = 0;
            long sum1 = 0;
            for (int i = 0; i < n; i++)
            {
                sum0 += grid[0][i];
                prefixSum0[i] = sum0;
                sum1 += grid[1][i];
                prefixSum1[i] = sum1;
            }
            for (int i = 0; i < n; i++)
            {
                long max = Math.Max(sum0 - prefixSum0[i], i == 0 ? 0 : prefixSum1[i - 1]);
                res = Math.Min(res, max);
            }
            return res;
        }

        /// 2018. Check if Word Can Be Placed In Crossword
        public bool PlaceWordInCrossword(char[][] board, string word)
        {
            int m = board.Length;
            int n = board[0].Length;
            int len = word.Length;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i][j] == '#') continue;
                    if (i == 0 || board[i - 1][j] == '#')
                    {
                        int l = 0;
                        for (int k = i; k < m && l < len; k++)
                        {
                            if (board[k][j] == '#') break;
                            if (board[k][j] != word[l] && board[k][j] != ' ') break;
                            l++;
                            if (l == len && k != m - 1 && board[k + 1][j] != '#')
                            {
                                l = -1;
                                break;
                            }
                        }
                        if (l == len) return true;
                    }

                    if (i == m - 1 || board[i + 1][j] == '#')
                    {
                        int l = 0;
                        for (int k = i; k >= 0 && l < len; k--)
                        {
                            if (board[k][j] == '#') break;
                            if (board[k][j] != word[l] && board[k][j] != ' ') break;
                            l++;
                            if (l == len && k != 0 && board[k - 1][j] != '#')
                            {
                                l = -1;
                                break;
                            }
                        }
                        if (l == len) return true;
                    }

                    if (j == 0 || board[i][j - 1] == '#')
                    {
                        int l = 0;
                        for (int k = j; k < n && l < len; k++)
                        {
                            if (board[i][k] == '#') break;
                            if (board[i][k] != word[l] && board[i][k] != ' ') break;
                            l++;
                            if (l == len && k != n - 1 && board[i][k + 1] != '#')
                            {
                                l = -1;
                                break;
                            }
                        }
                        if (l == len) return true;
                    }

                    if (j == n - 1 || board[i][j + 1] == '#')
                    {
                        int l = 0;
                        for (int k = j; k >= 0 && l < len; k--)
                        {
                            if (board[i][k] == '#') break;
                            if (board[i][k] != word[l] && board[i][k] != ' ') break;
                            l++;
                            if (l == len && k != 0 && board[i][k - 1] != '#')
                            {
                                l = -1;
                                break;
                            }
                        }
                        if (l == len) return true;
                    }
                }
            }

            return false;
        }

        /// 2022. Convert 1D Array Into 2D Array
        public int[][] Construct2DArray(int[] original, int m, int n)
        {
            if (original.Length != m * n)
                return new int[0][] { };
            var ans = new int[m][];
            for (int i = 0; i < m * n; i++)
            {
                if (i % n == 0)
                    ans[i / n] = new int[n];
                ans[i / n][i % n] = original[i];
            }
            return ans;
        }

        ///2023. Number of Pairs of Strings With Concatenation Equal to Target
        ///Given an array of digit strings nums and a digit string target,
        ///return the number of pairs of indices (i, j) (where i != j) such that the concatenation of nums[i] + nums[j] equals target.
        public int NumOfPairs(string[] nums, string target)
        {
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i])) dict.Add(nums[i], new List<int>());
                dict[nums[i]].Add(i);
            }
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (target.StartsWith(nums[i]))
                {
                    var str = target.Substring(nums[i].Length, target.Length - nums[i].Length);
                    if (dict.ContainsKey(str))
                    {
                        res += dict[str].Where(x => x != i).Count();
                    }
                }
            }
            return res;
        }

        /// 2024. Maximize the Confusion of an Exam, #Sliding Window ,#Binary Search
        ///See 424. Longest Repeating Character Replacement
        ///Change the answer key for any question to 'T' or 'F' (i.e., set answerKey[i] to 'T' or 'F').
        ///Return the maximum number of consecutive 'T's or 'F's in the answer key after performing the operation at most k times.
        ///n == answerKey.length, 1 <= n <= 5 * 10^4, answerKey[i] is either 'T' or 'F', 1 <= k <= n
        public int MaxConsecutiveAnswers(string answerKey, int k)
        {
            int maxfreq = 0;
            int left = 0;
            int[] arr = new int[26];
            for (int i = 0; i < answerKey.Length; i++)
            {
                maxfreq = Math.Max(maxfreq, ++arr[answerKey[i] - 'A']);
                if (i - left + 1 > maxfreq + k)
                {
                    arr[answerKey[left] - 'A']--;
                    left++;
                }
            }
            return answerKey.Length - left;
        }

        public int MaxConsecutiveAnswers_BinarySearch(string answerKey, int k)
        {
            int n = answerKey.Length;
            if (n == k)
                return n;

            List<int[]> list = new List<int[]>();
            int countT = 0;
            int countF = 0;
            list.Add(new int[] { 0, 0 });
            for (int i = 0; i < n; i++)
            {
                if (answerKey[i] == 'T')
                {
                    countT++;
                }
                else
                {
                    countF++;
                }
                list.Add(new int[] { countT, countF });
            }

            int left = k + 1;
            int right = n;

            int mid = (left + right + 1) / 2;

            while (left < right)
            {
                bool exist = false;
                for (int i = 0; i < n - mid + 1; i++)
                {
                    var a = list[i];
                    var b = list[i + mid];

                    int count1 = b[0] - a[0];
                    int count2 = b[1] - a[1];
                    if (count1 + k >= mid || count2 + k >= mid)
                    {
                        exist = true;
                        break;
                    }
                }

                if (exist)
                {
                    left = mid;
                    mid = (left + right + 1) / 2;
                }
                else
                {
                    right = mid - 1;
                    mid = (left + right + 1) / 2;
                }
            }

            return left;
        }

        ///2027. Minimum Moves to Convert String
        public int MinimumMoves(string s)
        {
            int res = 0;
            for (int i = 0; i < s.Length;)
            {
                if (s[i] == 'X')
                {
                    res++;
                    i += 3;
                }
                else i++;
            }
            return res;
        }

        ///2028. Find Missing Observations
        public int[] MissingRolls(int[] rolls, int mean, int n)
        {
            int diff = (rolls.Length + n) * mean - rolls.Sum();
            if (diff > 6 * n || diff < n) return new int[0];
            int mod = diff % n;
            int[] res = Enumerable.Repeat(diff / n, n).ToArray();
            for (int i = 0; i < mod; i++)
                res[i]++;
            return res;
        }

        ///2030. Smallest K-Length Subsequence With Occurrences of a Letter
        public string SmallestSubsequence(string s, int k, char letter, int repetition)
        {
            int n = s.Length;
            List<int> list = new List<int>();
            int[] arr = new int[n];
            int index = -1;
            int i = 0;
            for (; i < n; i++)
            {
                if (s[i] == letter)
                {
                    list.Add(i);
                    continue;
                }
                while (index >= 0 && index + 1 + (n - 1) - i >= k && s[arr[index]] > s[i])
                {
                    index--;
                }
                arr[++index] = i;
            }

            char[] res = new char[k];
            int x = 0, y = 0;
            int m = 0;
            while (m < k)
            {
                //if e is just enough, add as many normal as possible
                //<letter first
            }
            return new string(res);
        }

        /// 2032. Two Out of Three
        ///return a distinct array containing all the values that in at least two out of the three arrays.
        public IList<int> TwoOutOfThree(int[] nums1, int[] nums2, int[] nums3)
        {
            Dictionary<int, HashSet<int>> dict = new Dictionary<int, HashSet<int>>();
            foreach (var n in nums1)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, new HashSet<int>() { 0 });
            }
            foreach (var n in nums2)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, new HashSet<int>() { 1 });
                else dict[n].Add(1);
            }

            foreach (var n in nums3)
            {
                if (dict.ContainsKey(n))
                    dict[n].Add(2);
            }

            return dict.Keys.Where(x => dict[x].Count >= 2).ToList();
        }

        ///2033. Minimum Operations to Make a Uni-Value Grid
        //In one operation, you can add x to or subtract x from any element in the grid.
        //A uni-value grid is a grid where all the elements of it are equal.
        //Return the minimum number of operations to make the grid uni-value. If it is not possible, return -1.
        public int MinOperations(int[][] grid, int x)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            var arr = new int[m * n];//convert 2D array to 1D
            for (int i = 0; i < m; i++)
                Array.Copy(grid[i], 0, arr, i * n, n);
            Array.Sort(arr);
            if (arr.Any(i => (i - arr[0]) % x != 0)) return -1;
            //why median works? when move to right, all left-elements add and right-elements subtract
            //so when left-elements count = right-elements, stop move
            //https://asvrada.github.io/blog/median-shortest-distance-sum/
            if (m * n % 2 == 1)
                return arr.Sum(i => Math.Abs(i - arr[m * n / 2]) / x);
            else
                return Math.Min(arr.Sum(i => Math.Abs(i - arr[m * n / 2]) / x),
                    arr.Sum(i => Math.Abs(i - arr[m * n / 2 - 1]) / x));
        }

        ///2034. Stock Price Fluctuation, see StockPrice

        /// 2037. Minimum Number of Moves to Seat Everyone
        /// Return the minimum number of moves required to move each student to a seat
        /// such that no two students are in the same seat.
        /// Note that there may be multiple seats or students in the same position at the beginning.
        public int MinMovesToSeat(int[] seats, int[] students)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var seat in seats)
            {
                if (dict.ContainsKey(seat)) dict[seat]++;
                else dict.Add(seat, 1);
            }

            int res = 0;

            var keys = dict.Keys.OrderBy(x => x).ToList();
            Array.Sort(students);

            var i = 0;
            foreach (var student in students)
            {
                if (student == keys[i])
                {
                }
                else
                {
                    res += Math.Abs(student - keys[i]);
                }

                dict[keys[i]]--;
                if (dict[keys[i]] == 0) i++;
            }

            return res;
        }

        ///2040. Kth Smallest Product of Two Sorted Arrays, #Binary Search
        //Given two sorted 0-indexed integer arrays nums1 and nums2 as well as an integer k,
        //return the kth (1-based) smallest product of nums1[i] * nums2[j],-10^5 <= nums1[i], nums2[j] <= 10^5
        public long KthSmallestProduct(int[] nums1, int[] nums2, long k)
        {
            int n1 = nums1.Length;
            int n2 = nums2.Length;
            int neg1 = 0, zero1 = 0, neg2 = 0, zero2 = 0;
            foreach (var x in nums1)
            {
                if (x < 0) neg1++;
                else if (x == 0) zero1++;
                else break;
            }
            foreach (var x in nums2)
            {
                if (x < 0) neg2++;
                else if (x == 0) zero2++;
                else break;
            }

            long totalNeg = (long)neg1 * (n2 - neg2 - zero2) + (long)neg2 * (n1 - neg1 - zero1);
            long totalZero = (long)zero1 * n2 + (long)zero2 * n1 - (long)zero1 * zero2;
            if (k <= totalNeg)
            {
                long left = -10_000_000_000;
                long right = 0;
                while (left < right)
                {
                    long mid = left + (right - left) / 2;
                    long count = 0;
                    if (neg1 > 0 && n2 - neg2 - zero2 > 0)
                    {
                        for (int i = 0; i < neg1 && count < k; i++)
                        {
                            if ((long)nums1[i] * nums2[n2 - 1] > mid) break;//min out of range
                            if ((long)nums1[i] * nums2[neg2 + zero2] <= mid)
                            {
                                count += n2 - neg2 - zero2;
                            }
                            else
                            {
                                int low = neg2 + zero2;
                                int high = n2 - 1;
                                while (low < high)
                                {
                                    int center = (low + high) / 2;
                                    if ((long)nums1[i] * nums2[center] <= mid)
                                    {
                                        high = center;
                                    }
                                    else
                                    {
                                        low = center + 1;
                                    }
                                }
                                count += n2 - low; //[low,n2-1]
                            }
                        }
                    }

                    if (neg2 > 0 && n1 - neg1 - zero1 > 0)
                    {
                        for (int i = 0; i < neg2 && count < k; i++)
                        {
                            if ((long)nums2[i] * nums1[n1 - 1] > mid) break;//min out of range
                            if ((long)nums2[i] * nums1[neg1 + zero1] <= mid)
                            {
                                count += n1 - neg1 - zero1;
                            }
                            else
                            {
                                int low = neg1 + zero1;
                                int high = n1 - 1;
                                while (low < high)
                                {
                                    int center = (low + high) / 2;
                                    if ((long)nums2[i] * nums1[center] <= mid)
                                    {
                                        high = center;
                                    }
                                    else
                                    {
                                        low = center + 1;
                                    }
                                }
                                count += n1 - low; //[low,n1-1]
                            }
                        }
                    }

                    if (count >= k)
                        right = mid;
                    else
                        left = mid + 1;
                }
                return left;
            }
            else
            {
                k -= totalNeg;
                if (k <= totalZero) return 0;
                k -= totalZero;

                long left = 0;
                long right = 10_000_000_000;
                while (left < right)
                {
                    long mid = left + (right - left) / 2;
                    long count = 0;
                    if (neg1 > 0 && neg2 > 0)
                    {
                        for (int i = neg1 - 1; i >= 0 && count < k; i--)
                        {
                            if ((long)nums1[i] * nums2[neg2 - 1] > mid) break;//min out of range
                            if ((long)nums1[i] * nums2[0] <= mid)
                            {
                                count += neg2;
                            }
                            else
                            {
                                int low = 0;
                                int high = neg2 - 1;
                                while (low < high)
                                {
                                    int center = (low + high) / 2;
                                    if ((long)nums1[i] * nums2[center] <= mid)
                                    {
                                        high = center;
                                    }
                                    else
                                    {
                                        low = center + 1;
                                    }
                                }
                                count += neg2 - low; //[low,neg2-1]
                            }
                        }
                    }

                    if (n1 - neg1 - zero1 > 0 && n2 - neg2 - zero2 > 0)
                    {
                        for (int i = neg1 + zero1; i < n1 && count < k; i++)
                        {
                            if ((long)nums1[i] * nums2[neg2 + zero2] > mid) break;//min out of range
                            if ((long)nums1[i] * nums2[n2 - 1] <= mid)
                            {
                                count += n2 - neg2 - zero2;
                            }
                            else
                            {
                                int low = neg2 + zero2;
                                int high = n2 - 1;
                                while (low < high)
                                {
                                    int center = (low + high + 1) / 2;
                                    if ((long)nums1[i] * nums2[center] <= mid)
                                    {
                                        low = center;
                                    }
                                    else
                                    {
                                        high = center - 1;
                                    }
                                }
                                count += low - neg2 - zero2 + 1; //[neg2+zero2,low]
                            }
                        }
                    }

                    if (count >= k)
                        right = mid;
                    else
                        left = mid + 1;
                }
                return left;
            }
        }

        /// 2042. Check if Numbers Are Ascending in a Sentence
        public bool AreNumbersAscending(string s)
        {
            var arr = s.Split(" ").Where(x => char.IsDigit(x[0])).Select(x => int.Parse(x)).ToList();
            for (int i = 0; i < arr.Count - 1; i++)
                if (arr[i] >= arr[i + 1]) return false;
            return true;
        }

        ///2043. Simple Bank System, see Bank

        /// 2044. Count Number of Maximum Bitwise-OR Subsets,#Backtracking
        ///find the maximum possible bitwise OR of a subset of nums and return the number of different non-empty subsets with the maximum bitwise OR.
        ///An array a is a subset of an array b if a can be obtained from b by deleting some(possibly zero) elements of b.
        public int CountMaxOrSubsets(int[] nums)
        {
            int res = 0;
            int maxOr = 0;
            foreach (var n in nums)
                maxOr |= n;
            CountMaxOrSubsets_BackTracking(nums, 0, 0, maxOr, ref res);
            return res;
        }

        private void CountMaxOrSubsets_BackTracking(int[] nums, int i, int currOr, int maxOr, ref int res)
        {
            if (currOr == maxOr && i == nums.Length)
            {
                res++;
                return;
            }

            if (i >= nums.Length) return;
            CountMaxOrSubsets_BackTracking(nums, i + 1, currOr, maxOr, ref res);
            CountMaxOrSubsets_BackTracking(nums, i + 1, currOr | nums[i], maxOr, ref res);
        }

        ///2045. Second Minimum Time to Reach Destination, #Graph, #BFS
        // vertex 1 to n, traffic light switching green/red by change interval, only leave any vertex at green
        // cost from any 2 connected vertex u to v is time
        // find 2nd minimum result from 1 to n
        public int SecondMinimum(int n, int[][] edges, int time, int change)
        {
            List<int>[] graph = new List<int>[n + 1];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }
            foreach (var e in edges)
            {
                graph[e[0]].Add(e[1]);//build the graph
                graph[e[1]].Add(e[0]);
            }
            int[][] memo = new int[n + 1][];//build the memo
            for (int i = 0; i < memo.Length; i++)
            {
                memo[i] = new int[] { int.MaxValue, int.MaxValue };
            }
            //using queue to BFS, every element in q is [index,costTime] pair
            var q = new Queue<int[]>();
            int[] seed = new int[] { 1, 0 };
            q.Enqueue(seed);
            while (q.Count > 0)
            {
                int size = q.Count;
                while (size-- > 0)
                {
                    var top = q.Dequeue();
                    //we can only leave vertex on green light
                    int offset = top[1] % (change * 2);
                    if (offset < change)//green light
                    {
                        foreach (var i in graph[top[0]])
                        {
                            int cost = top[1] + time;
                            if (cost > memo[i][1])
                                continue;//slower than 2nd, discard
                            else if (cost == memo[i][0] || cost == memo[i][1])
                                continue;//want unique result, discard duplicate
                            else
                            {
                                if (cost < memo[i][0])//better than 1st
                                {
                                    memo[i][1] = memo[i][0];
                                    memo[i][0] = cost;
                                }
                                else
                                {
                                    memo[i][1] = cost;
                                }
                                q.Enqueue(new int[] { i, cost });
                            }
                        }
                    }
                    else
                    {
                        //wait unitl next green light
                        top[1] += 2 * change - offset;
                        q.Enqueue(top);
                    }
                }
            }
            return memo[n][1];
        }

        ///2047. Number of Valid Words in a Sentence
        public int CountValidWords(string sentence)
        {
            int res = 0;
            var arr = sentence.Split(' ').ToArray();
            foreach (var str in arr)
            {
                if (string.IsNullOrEmpty(str)) continue;
                bool find = true;
                int hyphen = 0;
                int punctuation = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ' ' || char.IsDigit(str[i]))
                    {
                        find = false;
                        break;
                    }
                    else
                    {
                        if (str[i] == '-')
                        {
                            hyphen++;
                            if (hyphen > 1)
                            {
                                find = false;
                                break;
                            }
                            if (i == 0 || i == str.Length - 1 || !char.IsLetter(str[i - 1]) || !char.IsLetter(str[i + 1]))
                            {
                                find = false;
                                break;
                            }
                        }
                        else if (str[i] == '!' || str[i] == ',' || str[i] == '.')
                        {
                            if (i != str.Length - 1)
                            {
                                find = false;
                                break;
                            }
                        }
                    }
                }

                if (find)
                    res++;
            }

            return res;
        }

        ///2049. Count Nodes With the Highest Score, #DFS
        //The score of the node is the product of the sizes of all those subtrees created by removing this node
        //Return the number of nodes that have the highest score.
        public int CountHighestScoreNodes(int[] parents)
        {
            int count =0;
            double max = 0;
            int n = parents.Length;
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 1; i < n; i++)
            {
                graph[parents[i]].Add(i);
            }
            CountHighestScoreNodes(0,graph,ref max,ref count);
            return count;
        }
        private int CountHighestScoreNodes(int i, List<int>[] graph, ref double max, ref int count)
        {
            int n = graph.Length;
            double product = 1.0;
            int childs = 0;
            foreach (var j in graph[i])
            {
                int k = CountHighestScoreNodes(j, graph, ref max,ref count);
                product *= k;
                childs+=k;
            }

            if(n - childs - 1>0)
                product *= n - childs - 1;
            if(product>max){
                max=product;
                count=1;
            }else if(product==max){
                count++;
            }
            return childs + 1;
        }

    }
}