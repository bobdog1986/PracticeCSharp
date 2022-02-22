﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///300. Longest Increasing Subsequence, #DP
        /// Patient Sort, https://en.wikipedia.org/wiki/Longest_increasing_subsequence
        ///return the longest length , by deleting some or no elements without changing the order.
        ///eg. [0,3,1,6,4,4,7].=>[0,1,4,7] or [0,3,4,6] or [0,1,6,7], O(n log(n))
        public int LengthOfLIS(int[] nums)
        {
            var ans =new List<int>() { nums[0]};
            for(int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > ans.Last())
                {
                    ans.Add(nums[i]);
                }
                else
                {
                    //a litter same like =>Q 334. Increasing Triplet Subsequence
                    //replace the correct index, still get the correct ans
                    LengthOfLIS_BinaryReplace(ans, nums[i]);
                }
            }
            return ans.Count;
        }

        public void LengthOfLIS_BinaryReplace(IList<int> list, int num)
        {
            int left = 0;
            int right = list.Count - 1;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if(list[mid] < num)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            list[right] = num;
        }

        ///304. Range Sum Query 2D - Immutable, see NumMatrix

        /// 309. Best Time to Buy and Sell Stock with Cooldown
        ///After you sell your stock, you cannot buy stock on the next day (i.e., cooldown one day).
        ///0 <= prices[i] <= 1000, 1<=length<=5000
        public int MaxProfit(int[] prices)
        {
            int len = prices.Length;
            if (len <=1)
                return 0;

            //buy[i]: Max profit till index i. The series of transaction is ending with a buy.
            //sell[i]: Max profit till index i.The series of transaction is ending with a sell.
            int[] sell = new int[len];
            int[] buy = new int[len];
            //buy[i] = Math.Max(buy[i - 1], sell[i - 2] - prices[i]);
            //sell[i] = Math.Max(sell[i - 1], buy[i - 1] + prices[i]);

            buy[0] = -prices[0];
            buy[1] = Math.Max(buy[0], -prices[1]);
            sell[0] = 0;
            sell[1] = Math.Max(sell[0], buy[0] + prices[1]);

            for (int i = 2; i < len; i++)
            {
                buy[i] = Math.Max(buy[i - 1], sell[i - 2] - prices[i]);
                sell[i] = Math.Max(sell[i - 1], buy[i - 1] + prices[i]);
            }
            return sell[len - 1];
        }

        /// 322. Coin Change, #DP
        ///array coins representing coins of different denominations and an integer amount representing a total amount of money.
        ///Return the fewest number of coins that you need to make up that amount. Or return -1. can reuse each kind of coin.
        ///1 <= coins[i] <= 2^31 - 1, 0 <= amount <= 10000
        public int CoinChange(int[] coins, int amount)
        {
            if (amount == 0)
                return 0;
            int[] dp = new int[amount + 1];
            for (int i = 0; i < dp.Length; i++)
                dp[i] = 10001;
            Array.Sort(coins);
            dp[amount] = 0;
            for (int i = amount; i>=0; i--)
            {
                for (int j=coins.Length-1;j>=0;j--)
                {
                    if (i - coins[j] < 0)
                        continue;
                    dp[i - coins[j]] = Math.Min(dp[i - coins[j]], dp[i] + 1);
                }
            }
            return dp[0] == 10001 ? -1 : dp[0];
        }

        ///326. Power of Three
        ///Given an integer n, return true if it is a power of three. Otherwise, return false.
        ///An integer n is a power of three, if there exists an integer x such that n == 3x.
        public bool IsPowerOfThree(int n)
        {
            while (n >= 3)
            {
                if (n % 3 != 0)
                    return false;
                n /= 3;
            }
            return n == 1;
        }

        /// 327. Count of Range Sum --- not pass
        ///return the number of range sums that lie in [lower, upper] inclusive.
        public int CountRangeSum(int[] nums, int lower, int upper)
        {
            int ans = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                long sum = 0;
                int j = i;
                while (j++ < nums.Length)
                {
                    sum += nums[j];
                    if (sum >= lower && sum <= upper)
                        ans++;
                }
            }
            return ans;
        }

        ///328. Odd Even Linked List
        ///group all the nodes with odd indices together followed by the nodes with even indices, then return.
        //The first node is considered odd, and the second node is even, and so on.
        //You must solve the problem in O(1) extra space complexity and O(n) time complexity.
        ///head = [1,2,3,4,5] -> Output: [1,3,5,2,4], 0<=n<=10^4
        public ListNode OddEvenList(ListNode head)
        {
            ListNode oddsHead = null;
            ListNode evensHead = null;
            ListNode currOdd = null;
            ListNode currEven = null;

            bool isOdd = true;
            var node = head;
            while(node != null)
            {
                if (isOdd)
                {
                    if(currOdd == null)
                    {
                        oddsHead = node;
                        currOdd = node;
                    }
                    else
                    {
                        currOdd.next = node;
                        currOdd=node;
                    }
                }
                else
                {
                    if (currEven == null)
                    {
                        evensHead = node;
                        currEven = node;
                    }
                    else
                    {
                        currEven.next = node;
                        currEven = node;
                    }
                }
                isOdd = !isOdd;
                node = node.next;
            }
            if(currOdd != null)
                currOdd.next = evensHead;
            if(currEven!=null)
                currEven.next = null;

            return oddsHead;
        }
        ///329. Longest Increasing Path in a Matrix, #BFS
        ///Given an m x n integers matrix, return the length of the longest increasing path in matrix.
        ///From each cell, you can either move in four directions: left, right, up, or down.You may not move diagonally or move outside the boundary (i.e., wrap-around is not allowed).
        public int LongestIncreasingPath(int[][] matrix)
        {
            int ans = 1;

            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;
            int[][] dp = new int[rowLen][];
            for (int i = 0; i < rowLen; i++)
                dp[i] = new int[colLen];

            int[][] dxy = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            int len = 1;
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    int neighboor = 0;
                    foreach (var d in dxy)
                    {
                        int r = i + d[0];
                        int c = j + d[1];
                        if (r >= 0 && r < rowLen && c >= 0 && c < colLen)
                        {
                            if (matrix[r][c] < matrix[i][j])
                            {
                                neighboor++;
                                break;
                            }
                        }
                    }

                    if (neighboor == 0)
                    {
                        dp[i][j] = len;
                        list.Add(new int[] { i,j});
                    }
                }
            }

            while(list.Count > 0)
            {
                len++;
                List<int[]> next = new List<int[]>();
                Dictionary<int, int> dict = new Dictionary<int, int>();
                foreach(var cell in list)
                {
                    foreach (var d in dxy)
                    {
                        int r = cell[0] + d[0];
                        int c = cell[1] + d[1];
                        if (r >= 0 && r < rowLen && c >= 0 && c < colLen)
                        {
                            if (matrix[r][c] > matrix[cell[0]][cell[1]])
                            {
                                dp[r][c] = len;
                                ans = len;
                                if(!dict.ContainsKey(r * 1000 + c))
                                {
                                    next.Add(new int[] { r, c });
                                    dict.Add(r * 1000 + c, 1);
                                }
                            }
                        }
                    }
                }
                list = next;
            }
            return ans;
        }
        /// 334. Increasing Triplet Subsequence, #Greedy
        ///using greedy to find i<j<k, nums[i]<nums[j]<nums[k]
        public bool IncreasingTriplet(int[] nums)
        {
            // start with two largest values, update them until find a num > both of them
            int small = int.MaxValue;
            int big = int.MaxValue;
            foreach (var n in nums)
            {
                if (n > big)
                {
                    //find a num > than the big
                    return true;
                }
                else
                {
                    if(n<= small)
                    {
                        //even when big has valid value(<int.Max), we still need update small for updating the big in future
                        //this will not change the big,  so if we found n>big in future, this algo still work
                        small = n;
                    }
                    else
                    {
                        //find n <= big && >small, then update the big
                        big = n;
                    }
                }
            }
            return false;
        }

        ///343. Integer Break, #DP
        ///find 3 as many as possible, but no 1; 2 <= n <= 58
        public int IntegerBreak(int n)
        {
            int[] dp = new int[n + 1];
            dp[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                for (int j = 1; j <= i / 2; j++)
                {
                    dp[i] = Math.Max(dp[i], Math.Max(j, dp[j]) * Math.Max(i - j, dp[i - j]));
                }
            }
            return dp[n];
        }
        public int IntegerBreak_Math(int n)
        {
            if (n <= 3)
                return n - 1;//1*(n-1)
            int ans = 1;
            while (n > 4)
            {
                ans *= 3;
                n -= 3;
            }
            return ans * n;
        }

        /// 344. Reverse String
        /// You must do this by modifying the input array in-place with O(1) extra memory.
        public void ReverseString(char[] s)
        {
            for (int i = 0; i < s.Length / 2; i++)
            {
                char temp = s[i];
                s[i] = s[s.Length - 1 - i];
                s[s.Length - 1 - i] = temp;
            }
        }

        ///347. Top K Frequent Elements
        ///return the k most frequent elements. You may return the answer in any order.
        public int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (dict.ContainsKey(n))
                    dict[n]++;
                else
                    dict.Add(n, 1);
            }
            return dict.OrderBy(x => -x.Value).Take(k).Select(x => x.Key).ToArray();
        }
    }
}