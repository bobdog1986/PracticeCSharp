using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///509. Fibonacci Number
        ///0 <= n <= 30, F(0) = 0, F(1) = 1, F(n) = F(n - 1) + F(n - 2), for n > 1.
        public int Fib(int n)
        {
            if (n <= 1)
                return n;
            int dp = 0;
            int a1 = 0;
            int a2 = 1;
            int i = 2;
            while (i <= n)
            {
                dp = a1 + a2;
                a1 = a2;
                a2 = dp;
                i++;
            }
            return dp;
        }
        public int Fib_Recursion(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            return Fib_Recursion(n - 1) + Fib_Recursion(n - 2);
        }

        ///516. Longest Palindromic Subsequence
        ///Given a string s, find the longest palindromic subsequence's length in s.
        ///A subsequence is a sequence that can be derived from another sequence
        ///by deleting some or no elements without changing the order of the remaining elements.
        public int LongestPalindromeSubseq(string s)
        {
            int[,] dp = new int[s.Length,s.Length];

            for (int i = s.Length - 1; i >= 0; i--)
            {
                dp[i,i] = 1;
                for (int j = i + 1; j < s.Length; j++)
                {
                    if (s[i] == s[j])
                    {
                        dp[i,j] = dp[i + 1,j - 1] + 2;
                    }
                    else
                    {
                        dp[i,j] = Math.Max(dp[i + 1,j], dp[i,j - 1]);
                    }
                }
            }
            return dp[0,s.Length - 1];
        }

        ///518. Coin Change 2, #DP
        ///Return the number of combinations that make up that amount.
        ///If that amount of money cannot be made up by any combination of the coins, return 0.
        public int Change(int amount, int[] coins)
        {
            int[] dp = new int[amount + 1];
            dp[0] = 1;
            foreach (var coin in coins)
            {
                for (int i = coin; i <= amount; i++)
                {
                    dp[i] += dp[i - coin];
                }
            }
            return dp[amount];
        }


        /// 520. Detect Capital
        ///3 pattern: all UpCase, all LowerCase, only first char UpCase others lower
        ///Given a string word, return true if the usage of capitals in it is right.
        public bool DetectCapitalUse(string word)
        {
            if (word.Length == 1)
                return true;

            bool lastIsUpper = char.IsUpper(word[word.Length - 1]);

            bool ans = true;

            if (lastIsUpper)
            {
                for (int i = 0; i < word.Length - 1; i++)
                {
                    if (!char.IsUpper(word[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 1; i < word.Length - 1; i++)
                {
                    if (char.IsUpper(word[i]))
                    {
                        return false;
                    }
                }
            }
            return ans;

        }
        ///525. Contiguous Array
        ///Given a binary array nums, return the maximum length of a contiguous subarray with an equal number of 0 and 1.
        ///1 <= nums.length <= 105
        ///nums[i] is either 0 or 1.
        public int FindMaxLength(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int sum = 0;
            //assume 0 is -1, then find diff of index between two dict[sum]
            //dict[0] should inited as -1, because [0,1] will add dict[0]-index1,
            dict.Add(0, -1);
            int max = 0;
            for (int i=0; i<nums.Length; i++)
            {
                //assume 0 is -1, then find diff of index between two dict[sum]
                if (nums[i] == 0)
                {
                    sum -= 1;
                }
                else
                {
                    sum += 1;
                }

                if (dict.ContainsKey(sum))
                {
                    max = Math.Max(max, i - dict[sum]);
                }
                else
                {
                    dict.Add(sum, i);
                }
            }
            return max;
        }

        ///532. K-diff Pairs in an Array, #HashMap
        ///Given an array of integers nums and an integer k, return the number of unique k-diff pairs in the array.
        /// |nums[i] - nums[j]| == k, -10^7 <= nums[i] <= 10^7, 0 <= k <= 10^7
        public int FindPairs(int[] nums, int k)
        {
            Dictionary<int,int> dict= new Dictionary<int,int>();
            foreach(var n in nums)
            {
                if (dict.ContainsKey(n))
                    dict[n]++;
                else
                    dict.Add(n, 1);
            }
            int ans = 0;
            if (k == 0)
            {
                foreach(var key in dict.Keys)
                {
                    if (dict[key] >= 2)
                        ans++;
                }
            }
            else
            {
                var keys = dict.Keys.OrderBy(x => x).ToList();
                foreach(var key in keys)
                {
                    if (key + k > keys.Last())
                        break;
                    if (dict.ContainsKey(key + k))
                        ans++;
                }
            }
            return ans;
        }

        /// 542. 01 Matrix, #Graph, #BFS
        /// Given an m x n binary matrix mat, return the distance of the nearest 0 for each cell.
        /// The distance between two adjacent cells is 1.
        public int[][] UpdateMatrix(int[][] mat)
        {
            int rowLen = mat.Length;
            int colLen = mat[0].Length;
            int[][] res = new int[rowLen][];
            for (int i = 0; i < rowLen; i++)
                res[i] = new int[colLen];
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            Queue<int[]> q = new Queue<int[]>();
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (mat[i][j] == 0)
                    {
                        res[i][j] = 0;
                        q.Enqueue(new int[] { i, j });
                    }
                    else
                    {
                        res[i][j] = Math.Max(i, rowLen - i - 1) + Math.Max(j, colLen - j - 1);
                    }
                }
            }
            int step = 1;
            bool[,] visit = new bool[rowLen, colLen];
            while (q.Count > 0)
            {
                int size = q.Count;
                while (size-- > 0)
                {
                    var p = q.Dequeue();
                    foreach(var d in dxy4)
                    {
                        var r=p[0] + d[0];
                        var c=p[1] + d[1];
                        if(r>=0 && r<rowLen && c>=0 && c<colLen && !visit[r,c] && mat[r][c] == 1)
                        {
                            visit[r, c] = true;
                            res[r][c] = step;
                            q.Enqueue(new int[] { r, c });
                        }
                    }
                }
                step++;
            }
            return res;
        }

        ///547. Number of Provinces, #DFS
        ///BFS/DFS, same to 200
        public int FindCircleNum(int[][] isConnected)
        {
            int[] arr = new int[isConnected.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 1;
            }
            int ans = 0;
            for (int i = 0; i < isConnected.Length; i++)
            {
                if (arr[i] == 0)
                {
                    continue;
                }
                else
                {
                    ans++;
                    arr[i] = 0;
                }

                for (int j = 0; j < isConnected[i].Length; j++)
                {
                    if (i != j && isConnected[i][j] == 1)
                    {
                        FindCircleNum_RemoveAllConnected(isConnected, arr, i, j);
                    }
                }
            }
            return ans;
        }

        public void FindCircleNum_RemoveAllConnected(int[][] isConnected, int[] arr, int r, int c)
        {
            if (arr[c] == 0)
                return;
            arr[c] = 0;
            for (int j = 0; j < isConnected[c].Length; j++)
            {
                if (c != j && isConnected[c][j] == 1)
                {
                    FindCircleNum_RemoveAllConnected(isConnected, arr, c, j);
                }
            }
        }

    }
}