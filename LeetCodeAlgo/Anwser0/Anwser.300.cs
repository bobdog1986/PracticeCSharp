﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///300. Longest Increasing Subsequence
        /// https://en.wikipedia.org/wiki/Longest_increasing_subsequence
        ///Patient Sort
        ///by deleting some or no elements without changing the order of the remaining elements.
        ///eg. [0,3,1,6,2,2,7].=>[0,1,2,7] , 1<=n<=2500, try Time Complexity O(n log(n))
        ///-10000 <= nums[i] <= 10000
        public int LengthOfLIS(int[] nums)
        {
            int[] tails = new int[nums.Length];
            int size = 0;
            foreach (var x in nums)
            {
                int i = 0, j = size;
                while (i != j)
                {
                    int m = (i + j) / 2;
                    if (tails[m] < x)
                        i = m + 1;
                    else
                        j = m;
                }
                tails[i] = x;
                if (i == size)
                    size++;
            }
            return size;
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

        ///327. Count of Range Sum --- not pass
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

        /// 334. Increasing Triplet Subsequence
        ///using greedy to find i<j<k, nums[i]<nums[j]<nums[k]
        public bool IncreasingTriplet(int[] nums)
        {
            if (nums.Length <= 2)
                return false;

            int start = 0;
            while (start < nums.Length - 1)
            {
                if (nums[start + 1] <= nums[start])
                    start++;
                else
                    break;
            }

            if (start > nums.Length - 3)
                return false;

            int end = nums.Length - 1;
            while (end > 0)
            {
                if (nums[end - 1] >= nums[end])
                    end--;
                else
                    break;
            }

            if (end - start + 1 < 3)
                return false;

            List<int> listX = new List<int>();

            for (int i = start; i <= end - 2; i++)
            {
                if (nums[i + 1] <= nums[i])
                    continue;

                for (int j = i + 1; j <= end - 1; j++)
                {
                    if (nums[j] <= nums[i])
                    {
                        i = j;
                        continue;
                    }

                    for (int k = j + 1; k <= end; k++)
                    {
                        if (k == end)
                        {
                            return nums[k] > nums[j] && nums[j] > nums[i];
                        }

                        if (nums[k] <= nums[i])
                        {
                            if (listX.Count > 0)
                            {
                                int nextXIndex = -1;
                                int minOfNextX = nums[k];
                                for (int m = 0; m < listX.Count; m++)
                                {
                                    if (nums[listX[m]] < minOfNextX)
                                    {
                                        nextXIndex = listX[m];
                                        minOfNextX = nums[nextXIndex];
                                    }
                                }

                                if (nextXIndex != -1)
                                {
                                    i = nextXIndex;
                                    j = k;
                                    listX.Clear();
                                    continue;
                                }
                            }

                            listX.Add(k);

                            continue;
                        }
                        else if (nums[k] <= nums[j])
                        {
                            //if (nums[k] > nums[i])
                            //{
                            //    j = k;
                            //}

                            if (listX.Count > 0)
                            {
                                int nextXIndex = -1;
                                int minOfNextX = nums[i];
                                for (int m = 0; m < listX.Count; m++)
                                {
                                    if (nums[listX[m]] <= minOfNextX && nums[listX[m]] < nums[k])
                                    {
                                        nextXIndex = listX[m];
                                        minOfNextX = nums[nextXIndex];
                                    }
                                }

                                if (nextXIndex != -1)
                                {
                                    i = nextXIndex;
                                    j = k;
                                    listX.Clear();
                                    continue;
                                }
                            }

                            //listX.Add(k);
                            if (nums[k] > nums[i])
                            {
                                j = k;
                            }
                            continue;
                        }

                        return true;
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
                {
                    dict[n]++;
                }
                else
                {
                    dict.Add(n, 1);
                }
            }

            return dict.OrderBy(x => -x.Value).Take(k).Select(x => x.Key).ToArray();
        }
    }
}