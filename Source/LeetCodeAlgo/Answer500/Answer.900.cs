﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///901. Online Stock Span, see StockSpanner

        ///904. Fruit Into Baskets, #Sliding Window
        //from left to right, at most two different fruits, each tree pick 1, return max count
        public int TotalFruit(int[] fruits)
        {
            int res = 0;
            int left = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < fruits.Length; i++)
            {
                if (dict.ContainsKey(fruits[i])) dict[fruits[i]]++;
                else dict.Add(fruits[i], 1);
                while (dict.Keys.Count > 2 && left <= i)
                {
                    dict[fruits[left]]--;
                    if (dict[fruits[left]] == 0)
                        dict.Remove(fruits[left]);
                    left++;
                }
                res = Math.Max(res, i - left + 1);
            }
            return res;
        }

        ///905. Sort Array By Parity
        ///move all the evens at the beginning then followed by all the odds.
        public int[] SortArrayByParity(int[] nums)
        {
            return nums.OrderBy(x => x % 2).ToArray();
        }

        /// 907. Sum of Subarray Minimums
        /// find the sum of min(b), where b ranges over every (contiguous) subarray of arr. return modulo 109 + 7.
        public int SumSubarrayMins(int[] arr)
        {
            int n = arr.Length;
            long res = 0;
            long mod = 1_000_000_007;
            for(int i = 0; i<n; i++)
            {
                int j = i-1;
                while (j>=0)
                {
                    if (arr[j]>arr[i])
                        j--;
                    else
                        break;
                }
                int k = i+1;
                while (k<n)
                {
                    if (arr[k]>arr[i])
                        k++;
                    else break;
                }

                res+= (i-j)*(k-i)*arr[i];
                res%=mod;
            }

            return (int)res;
        }

        ///908. Smallest Range I
        ///The score of nums is the difference between the maximum and minimum elements in nums.
        ///Return the minimum score of nums after applying the mentioned operation at most once for each index in it.
        public int SmallestRangeI(int[] nums, int k)
        {
            int max = nums[0];
            int min = nums[0];
            foreach (var n in nums)
            {
                max = Math.Max(max, n);
                min = Math.Min(min, n);
            }
            return Math.Max(0, max - min - 2 * k);
        }

        ///909. Snakes and Ladders, #DFS
        //Return the least number of moves required to reach the square n2.
        //If it is not possible to reach the square, return -1.
        public int SnakesAndLadders(int[][] board)
        {
            int n = board.Length;
            int[] dp = new int[n * n + 1];
            Array.Fill(dp, int.MaxValue);
            dp[1] = 0;
            SnakesAndLadders(1, board, dp);
            return dp[n * n] == int.MaxValue ? -1 : dp[n * n];
        }

        private void SnakesAndLadders(int index, int[][] board, int[] dp)
        {
            int n = board.Length;
            for (int i = index + 1; i <= index + 6 && i <= n * n; i++)
            {
                int[] arr = GetXY_SnakesAndLadders(i, n);
                int ladder = board[arr[0]][arr[1]];
                if (ladder == -1)
                {
                    if (dp[i] > dp[index] + 1)
                    {
                        dp[i] = dp[index] + 1;
                        SnakesAndLadders(i, board, dp);
                    }
                }
                else
                {
                    if (dp[ladder] > dp[index] + 1)
                    {
                        dp[ladder] = dp[index] + 1;
                        SnakesAndLadders(ladder, board, dp);
                    }
                }
            }
        }

        private int[] GetXY_SnakesAndLadders(int i, int n)
        {
            i = i - 1;
            int r = n - 1 - i / n;
            int c = (i / n % 2 == 0) ? i % n : n - 1 - i % n;
            return new int[] { r, c };
        }

        ///910. Smallest Range II
        ///For each index i where 0 <= i<nums.length, change nums[i] to be either nums[i] + k or nums[i] - k.
        ///Return the minimum difference between the max and min elements after changing the values at each index.
        public int SmallestRangeII(int[] nums, int k)
        {
            Array.Sort(nums);
            int n = nums.Length;
            int res = nums[n - 1] - nums[0];
            for (int i = 0; i < n - 1; ++i)
            {
                int max = Math.Max(nums[i] + k, nums[n - 1] - k);
                int min = Math.Min(nums[i + 1] - k, nums[0] + k);
                res = Math.Min(res, max - min);
            }
            return res;
        }

        ///

        ///911. Online Election, see TopVotedCandidate

        /// 912. Sort an Array
        public int[] SortArray(int[] nums)
        {
            Array.Sort(nums);
            return nums;
        }

        ///914. X of a Kind in a Deck of Cards
        public bool HasGroupsSizeX(int[] deck)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var d in deck)
            {
                if (!dict.ContainsKey(d)) dict.Add(d, 0);
                dict[d]++;
            }
            for (int x = 2; x <= deck.Length; x++)
            {
                if (deck.Length % x == 0 && !dict.Keys.Any(k => dict[k] % x != 0))
                    return true;
            }
            return false;
        }

        ///915. Partition Array into Disjoint Intervals, #Prefix Sum
        public int PartitionDisjoint(int[] nums)
        {
            int n = nums.Length;
            int[] minArr = new int[n];
            minArr[n - 1] = nums[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                minArr[i] = Math.Min(minArr[i + 1], nums[i]);
            }
            int max = int.MinValue;
            int res = 0;
            while (res < n)
            {
                max = Math.Max(max, nums[res]);
                if (res < n - 1 && max <= minArr[res + 1])
                {
                    break;
                }
                res++;
            }
            return res + 1;
        }

        ///916. Word Subsets
        //A string b is a subset of string a if every letter in b occurs in a including multiplicity.
        //A string a from words1 is universal if for every string b in words2, b is a subset of a.
        //Return an array of all the universal strings in words1. You may return the answer in any order.
        public IList<string> WordSubsets(string[] words1, string[] words2)
        {
            var res = new List<string>();

            int[] arr2 = new int[26];
            foreach (var w2 in words2)
            {
                int[] temp = new int[26];
                foreach (var c in w2)
                    temp[c - 'a']++;

                for (int i = 0; i < arr2.Length; i++)
                    arr2[i] = Math.Max(arr2[i], temp[i]);
            }

            foreach (var w1 in words1)
            {
                int[] arr1 = new int[26];
                foreach (var c1 in w1)
                    arr1[c1 - 'a']++;
                bool find = true;
                for (int i = 0; i < arr1.Length; i++)
                {
                    if (arr1[i] < arr2[i])
                    {
                        find = false;
                        break;
                    }
                }
                if (find) res.Add(w1);
            }
            return res;
        }

        /// 917. Reverse Only Letters
        ///not English letters remain in the same position.English letters(lowercase or uppercase) should be reversed.
        public string ReverseOnlyLetters(string s)
        {
            var arr = s.ToArray();
            List<int> list = new List<int>();
            List<char> letters = new List<char>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (char.IsLetter(s[i]))
                {
                    list.Add(i);
                    letters.Insert(0, s[i]);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                arr[list[i]] = letters[i];
            }
            return new string(arr);
        }

        /// 918. Maximum Sum Circular Subarray, #Kadane
        ///Kadane algorithm, find max-of-positive and min-of-negtive, return max-of-positive or Sum()- min-of-neg
        public int MaxSubarraySumCircular(int[] nums)
        {
            int sumOfPositive = 0;
            int max = int.MinValue;
            for (int i = 0; i < nums.Length; i++)
            {
                sumOfPositive += nums[i];
                max = Math.Max(sumOfPositive, max);
                if (sumOfPositive < 0)
                    sumOfPositive = 0;
            }

            //all elements are negative
            if (max < 0)
                return max;

            int sumOfNegative = 0;
            int min = int.MaxValue;
            for (int i = 0; i < nums.Length; i++)
            {
                sumOfNegative += nums[i];
                min = Math.Min(sumOfNegative, min);
                if (sumOfNegative > 0)
                    sumOfNegative = 0;
            }
            return Math.Max(max, nums.Sum() - min);
        }

        ///921. Minimum Add to Make Parentheses Valid
        ///Return the minimum number of moves required to make s valid. ()) to ()() or (())
        public int MinAddToMakeValid(string s)
        {
            int left = 0;
            int right = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    right++;
                }
                else
                {
                    if (right > 0) right--;
                    else left++;
                }
            }
            return left + right;
        }

        /// 923. 3Sum With Multiplicity
        ///Given an integer array arr, and an integer target, return the number of tuples i, j, k
        ///such that i < j < k and arr[i] + arr[j] + arr[k] == target.  return it modulo 10^9 + 7.
        ///0<= arr[i] <=100
        public int ThreeSumMulti(int[] arr, int target)
        {
            long[] nums = new long[101];
            foreach (int i in arr)
                nums[i]++;
            long res = 0;
            for (int i = 0; i <= 100; i++)
                for (int j = i; j <= 100; j++)
                {
                    int k = target - i - j;
                    if (k > 100 || k < 0) continue;
                    if (i == j && j == k)
                        res += nums[i] * (nums[i] - 1) * (nums[i] - 2) / 6;
                    else if (i == j && j != k)
                        res += nums[i] * (nums[i] - 1) / 2 * nums[k];
                    else if (j < k)
                        res += nums[i] * nums[j] * nums[k];
                }
            return (int)(res % 10_0000_0007);
        }

        ///924. Minimize Malware Spread, #Union Find
        //the ith node is directly connected to the jth node if graph[i][j] == 1.
        //Return the node that, if removed, would minimize M(initial).
        //If multiple nodes could be removed to minimize M(initial), return the smallest index.
        public int MinMalwareSpread_924(int[][] graph, int[] initial)
        {
            int n = graph.Length;
            var uf = new UnionFind(n);
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                    if (graph[i][j] == 1) uf.Union(i, j);//union all connected {i,j}
            }
            var groupCount = new Dictionary<int, int>();//{groupIndex, count},count of nodes connected with same groupIdx
            for (int i = 0; i < n; i++)
            {
                int j = uf.Find(i);
                if (!groupCount.ContainsKey(j)) groupCount.Add(j, 0);
                groupCount[j]++;
            }
            var malwareGroup = new Dictionary<int, int>();//{groupIndex, malwareCount},malwares shares same groupIndex
            foreach (var i in initial)
            {
                var j = uf.Find(i);
                if (!malwareGroup.ContainsKey(j)) malwareGroup.Add(j, 0);
                malwareGroup[j]++;
            }
            var arr = initial.OrderBy(x =>
            {
                var j = uf.Find(x);
                if (malwareGroup[j] >= 2) return 0;//>=2 malwares with same groupIndex will remove nothing
                else return -groupCount[j];//sort by removed descending
            }).ThenBy(x => x);
            return arr.First();
        }

        /// 925. Long Pressed Name
        ///Your friend is typing his name into a keyboard. Sometimes, when typing a character c,
        ///the key might get long pressed, and the character will be typed 1 or more times.
        ///You examine the typed characters of the keyboard.Return True if it is possible
        ///that it was your friends name, with some characters (possibly none) being long pressed
        public bool IsLongPressedName(string name, string typed)
        {
            int i = 0;
            int j = 0;
            for (; j < typed.Length && i < name.Length; j++)
            {
                if (typed[j] == name[i]) i++;
                else
                {
                    if (j == 0 || typed[j] != typed[j - 1]) return false;
                }
            }

            if (i != name.Length) return false;
            while (j < typed.Length)
            {
                if (typed[j] != typed[j - 1]) return false;
                j++;
            }
            return true;
        }

        ///926. Flip String to Monotone Increasing, #Prefix Sum
        //A binary string is monotone increasing if it consists of some number of 0's (possibly none),
        //followed by some number of 1's (also possibly none).
        //You are given a binary string s.You can flip s[i] changing it from 0 to 1 or from 1 to 0.
        //Return the minimum number of flips to make s monotone increasing
        public int MinFlipsMonoIncr(string s)
        {
            int n = s.Length;
            int ones = s.Count(x => x == '1');

            int res = Math.Min(ones, n - ones);
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (res == 0) break; ;
                count += s[i] - '0';
                res = Math.Min(res, count + n - (i + 1) - (ones - count));
            }
            return res;
        }

        /// 929. Unique Email Addresses
        ///If you add periods '.' between some characters in the local name part of an email address, ignored
        ///If you add a plus '+' in the local name, everything after the first plus sign will be ignored.
        public int NumUniqueEmails(string[] emails)
        {
            HashSet<string> map = new HashSet<string>();
            foreach (var email in emails)
            {
                var arr = email.Split("@");
                var str0 = arr[0].Split("+")[0].Replace(".", "");//first str before '@', then trim all '.'
                var str = str0 + "@" + arr[1];
                if (!map.Contains(str)) map.Add(str);
            }
            return map.Count;
        }

        ///930. Binary Subarrays With Sum, #Sliding Window, #Good
        //Given a binary array nums and an integer goal, return the number of non-empty subarrays with a sum goal.
        //A subarray is a contiguous part of the array.
        public int NumSubarraysWithSum(int[] nums, int goal)
        {
            int res = 0;
            int sum = 0;
            int left = 0;
            int one = -1;
            for (int i = 0; i<nums.Length; i++)
            {
                sum+=nums[i];
                while (sum>goal && left<=i)
                {
                    sum-=nums[left];
                    left++;
                }
                //if Sum of [left,i] == goal
                if (sum == goal)
                {
                    if (goal ==0)
                    {
                        res+= i-left+1;//all subarray from [left,i] to i
                    }
                    else
                    {
                        //find leftmost available index of 1
                        while (one<=i)
                        {
                            if (one<left) one++;
                            else if (nums[one]==0) one++;
                            else break;
                        }
                        res+= one-left+1;
                    }
                }
            }
            return res;
        }

        /// 931. Minimum Falling Path Sum
        /// Given an n x n array of integers matrix, return the minimum sum of any falling path through matrix.
        public int MinFallingPathSum(int[][] matrix)
        {
            var len = matrix.Length;
            if (len == 1)
                return matrix[0][0];

            int[] dp = new int[len];

            for (int i = 0; i < len; i++)
                dp[i] = matrix[0][i];

            for (int i = 1; i < len; i++)
            {
                int[] dp2 = new int[len];
                for (int k = 0; k < len; k++)
                    dp2[k] = dp[k];

                for (int j = 0; j < len; j++)
                {
                    int a = dp2[j] + matrix[i][j];

                    if (j > 0)
                        a = Math.Min(a, dp2[j - 1] + matrix[i][j]);

                    if (j < len - 1)
                        a = Math.Min(a, dp2[j + 1] + matrix[i][j]);

                    dp[j] = a;
                }
            }

            return dp.Min();
        }

        ///933. Number of Recent Calls, see RecentCounter

        ///934. Shortest Bridge, #Graph, #DFS
        ///You are given an n x n binary matrix grid where 1 represents land and 0 represents water.
        ///An island is a 4-directionally connected group of 1's not connected to any other 1's. There are exactly two islands in grid.
        ///You may change 0's to 1's to connect the two islands to form one island. Return the smallest number of 0's to connect 2 islands
        public int ShortestBridge(int[][] grid)
        {
            List<int[]> list1 = new List<int[]>();
            List<int[]> list2 = new List<int[]>();
            int[][] dxy4 = new int[4][] { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 0) continue;
                    List<int[]> curr = new List<int[]>();
                    List<int[]> visits = new List<int[]>() { new int[] { i, j } };
                    grid[i][j] = 0;
                    while (visits.Count > 0)
                    {
                        curr.AddRange(visits);
                        List<int[]> nexts = new List<int[]>();
                        foreach (var v in visits)
                        {
                            foreach (var d in dxy4)
                            {
                                int r = v[0] + d[0];
                                int c = v[1] + d[1];
                                if (r >= 0 && r < grid.Length && c >= 0 && c < grid[i].Length && grid[r][c] == 1)
                                {
                                    grid[r][c] = 0;
                                    nexts.Add(new int[] { r, c });
                                }
                            }
                        }
                        visits = nexts;
                    }
                    if (list1.Count == 0) { list1 = curr; }
                    else { list2 = curr; }
                }
                if (list2.Count > 0) break;
            }
            int min = int.MaxValue;
            foreach (var i in list1)
            {
                foreach (var j in list2)
                {
                    int len = int.MaxValue;
                    if (i[0] == j[0]) len = Math.Abs(i[1] - j[1]) - 1;
                    else if (i[1] == j[1]) len = Math.Abs(i[0] - j[0]) - 1;
                    else len = Math.Abs(i[1] - j[1]) - 1 + Math.Abs(i[0] - j[0]);
                    min = Math.Min(min, len);
                    if (min == 1) return min;
                }
            }
            return min;
        }

        ///935. Knight Dialer, #DP
        //The chess knight mvoe on a 4x3 a phone pad, cannot visit [3,0],[3,2]. start at any digit
        //Given an integer n, return module of how many distinct phone numbers of length n we can dial.
        public int KnightDialer(int n)
        {
            int mod = 1_000_000_007;
            int[][][] dp = new int[n][][];
            for (int i = 0; i < n; i++)
            {
                dp[i] = new int[4][];
                for (int j = 0; j < 4; j++)
                    dp[i][j] = new int[3];
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 3 && j != 1) continue;
                    dp[0][i][j] = 1;
                }
            }

            List<int[]> dxy8 = new List<int[]>()
            {
                new int[]{1,2 },new int[]{2,1 },new int[]{-1,-2 },new int[]{-2,-1 },
                new int[]{1,-2 },new int[]{2,-1 },new int[]{-1,2 },new int[]{-2,1 },
            };

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (j == 3 && k != 1) continue;
                        foreach (var d in dxy8)
                        {
                            int r = j + d[0];
                            int c = k + d[1];
                            if (r >= 0 && r < 4 && c >= 0 && c < 3 && !(r == 3 && c != 1))
                            {
                                dp[i + 1][r][c] = (dp[i + 1][r][c] + dp[i][j][k]) % mod;
                            }
                        }
                    }
                }
            }

            int res = 0;
            foreach (var r in dp[n - 1])//last round
            {
                foreach (var cell in r)
                {
                    res = (res + cell) % mod;
                }
            }
            return res;
        }

        ///938. Range Sum of BST, #BTree
        public int RangeSumBST(TreeNode root, int low, int high)
        {
            int res = 0;
            RangeSumBST(root, low, high, ref res);
            return res;
        }

        private void RangeSumBST(TreeNode root, int low, int high, ref int res)
        {
            if (root == null) return;
            else
            {
                if (root.val > high)
                {
                    RangeSumBST(root.left, low, high, ref res);
                }
                else if (root.val < low)
                {
                    RangeSumBST(root.right, low, high, ref res);
                }
                else
                {
                    res += root.val;
                    RangeSumBST(root.left, low, high, ref res);
                    RangeSumBST(root.right, low, high, ref res);
                }
            }
        }

        /// 941. Valid Mountain Array, #Two Pointers
        ///len>=3, arr[i]> all [0,i-1],and [i+1,len-1]
        public bool ValidMountainArray_TwoPointer(int[] arr)
        {
            int n = arr.Length;
            int i = 0;
            while (i < n - 1)
            {
                if (arr[i] < arr[i + 1]) i++;
                else if (arr[i] == arr[i + 1]) return false;
                else break;
            }
            if (i == 0 || i == n - 1) return false;

            int j = n - 1;
            while (j > 0)
            {
                if (arr[j] < arr[j - 1]) j--;
                else if (arr[j] == arr[j - 1]) return false;
                else break;
            }
            if (j == 0 || j == n - 1) return false;

            return i == j;
        }

        public bool ValidMountainArray(int[] arr)
        {
            if (arr.Length < 3)
                return false;

            bool isClimbing = true;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == arr[i - 1])
                    return false;

                if (isClimbing)
                {
                    if (arr[i] < arr[i - 1])
                    {
                        if (i == 1)
                            return false;

                        isClimbing = false;
                    }
                }
                else
                {
                    if (arr[i] > arr[i - 1])
                        return false;
                }
            }
            return !isClimbing;
        }

        ///942. DI String Match
        ///s[i] == 'I' if perm[i] < perm[i + 1], s[i] == 'D' if perm[i] > perm[i + 1].
        ///Given a string s, reconstruct the permutation perm and return it.
        public int[] DiStringMatch(string s)
        {
            var ans = new List<int>();
            var list = new List<int>();
            for (int i = 0; i <= s.Length; i++)
            {
                list.Add(i);
            }
            int increase = 0;
            int decrease = 0;
            foreach (var c in s)
            {
                if (c == 'I') increase++;
                else decrease++;
            }
            ans.Add(list[decrease]);
            foreach (var c in s)
            {
                if (c == 'I')
                {
                    ans.Add(list[list.Count - 1 - (increase - 1)]);
                    increase--;
                }
                else
                {
                    ans.Add(list[decrease - 1]);
                    decrease--;
                }
            }
            return ans.ToArray();
        }

        ///944. Delete Columns to Make Sorted
        public int MinDeletionSize(string[] strs)
        {
            int row = strs.Length;
            int col = strs[0].Length;
            int res = 0;
            for (int c = 0; c < col; c++)
            {
                char curr = 'a';
                for (int r = 0; r < row; r++)
                {
                    if (strs[r][c] < curr)
                    {
                        res++;
                        break;
                    }
                    curr = strs[r][c];
                }
            }
            return res;
        }

        ///945. Minimum Increment to Make Array Unique, #Greedy
        //In one move, you can pick an index i where 0 <= i < nums.length and increment nums[i] by 1.
        //Return the minimum number of moves to make every value in nums unique.
        public int MinIncrementForUnique(int[] nums)
        {
            Array.Sort(nums);
            int res = 0;
            int prev = int.MinValue;
            foreach (var n in nums)
            {
                if (n <= prev)
                {
                    res += prev + 1 - n;
                    prev++;
                }
                else
                {
                    prev = n;
                }
            }
            return res;
        }

        /// 946. Validate Stack Sequences
        ///Given two integer arrays pushed and popped each with distinct values,
        ///return true if this could have been the result of a sequence of push and pop operations on an initially empty stack, or false otherwise.
        public bool ValidateStackSequences(int[] pushed, int[] popped)
        {
            Stack<int> stack = new Stack<int>();
            var list1 = pushed.ToList();
            var list2 = popped.ToList();
            int count = 0;//how many nums in popped array have been handled
            while (count++ < pushed.Length)
            {
                var index = list1.IndexOf(list2[0]);
                if (index == -1)
                {
                    if (stack.Count == 0) return false;
                }
                else
                {
                    for (int i = 0; i <= index; i++)
                    {
                        stack.Push(list1[0]);//push all nums before popped[0] to stack
                        list1.RemoveAt(0);//then remove
                    }
                }
                var pop = stack.Pop();
                if (pop != list2[0]) return false;
                list2.RemoveAt(0);//if equal, remove popped[0]
            }
            return true;
        }

        ///947. Most Stones Removed with Same Row or Column, #Union Find
        //A stone can be removed if either the same row or the same column as another stone that has not been removed.
        //Given an array stones of length n where stones[i] = [xi, yi] represents the location of the ith stone,
        //return the largest possible number of stones that can be removed.
        public int RemoveStones(int[][] stones)
        {
            int n = stones.Length;
            var uf = new UnionFind(n);
            Dictionary<int, int> rowDict = new Dictionary<int, int>();//store {rowIndex, indexOfFirstStone}
            Dictionary<int, int> colDict = new Dictionary<int, int>();//store {colIndex, indexOfFirstStone}
            for (int i = 0; i < n; i++)
            {
                int[] curr = stones[i];
                if (rowDict.ContainsKey(curr[0]))
                    uf.Union(i, rowDict[curr[0]]);//union current stone with first stone contain same row
                else
                    rowDict.Add(curr[0], i);//or mark current stone as the first one has this rowIndex
                if (colDict.ContainsKey(curr[1]))
                    uf.Union(i, colDict[curr[1]]);//union current stone with first stone contain same col
                else
                    colDict.Add(curr[1], i);//or mark current stone as the first one has this colIndex
            }
            return n - uf.GroupCount;//every group will finally left 1 stone that cannot be removed!
        }

        ///948. Bag of Tokens, #Two Pointer, #Greedy
        //power - tokens[i], score+1; else power + tokens[i], , score-1;
        //return max score, use any number tokens each atmost 1 time
        public int BagOfTokensScore(int[] tokens, int power)
        {
            int res = 0;
            Array.Sort(tokens);
            int left = 0;
            int right = tokens.Length - 1;
            while (left <= right)
            {
                if (power >= tokens[left])
                {
                    power -= tokens[left++];
                    res++;
                }
                else
                {
                    if (left == right) break;
                    if (res == 0) break;
                    power += tokens[right--];
                    res--;
                }
            }
            return res;
        }
    }
}