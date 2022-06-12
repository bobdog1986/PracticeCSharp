using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2300. Successful Pairs of Spells and Potions, #Binary Search
        //A spell and potion pair is considered successful if the product of their strengths >= success.
        //Return an integer array pairs of length n where pairs[i] is the number of potions that will form a successful pair with the ith spell.
        public int[] SuccessfulPairs(int[] spells, int[] potions, long success)
        {
            potions = potions.OrderBy(x => -x).ToArray();
            int m = potions.Length;
            long[] arr = new long[m];//must long, or overflow 10^10/1
            for (int i = 0; i < m; i++)
            {
                arr[i] = (long)Math.Ceiling(success * 1.0 / potions[i]);
            }

            int n = spells.Length;
            int[] res = new int[n];
            for (int i = 0; i < n; i++)
            {
                if (spells[i] < arr[0]) res[i] = 0;
                else if (spells[i] >= arr[m - 1]) res[i] = m;
                else
                {
                    int left = 0;
                    int right = m - 1;
                    while (left < right)
                    {
                        int mid = (left + right + 1) / 2;
                        if (spells[i] >= arr[mid])
                        {
                            left = mid;
                        }
                        else
                        {
                            right = mid - 1;
                        }
                    }
                    res[i] = left + 1;
                }
            }
            return res;
        }

        ///2301. Match Substring After Replacement, #DP
        //mappings[i] = [oldi, newi],you may replace any number of oldi characters of sub with newi any times.
        //Return true if it is possible to make sub a substring of s.Otherwise, return false.
        //A substring is a contiguous non-empty sequence of characters within a string.
        public bool MatchReplacement(string s, string sub, char[][] mappings)
        {
            var dict = new Dictionary<char, HashSet<char>>();
            foreach (var map in mappings)
            {
                if (!dict.ContainsKey(map[0])) dict.Add(map[0], new HashSet<char>());
                dict[map[0]].Add(map[1]);
            }

            int m = s.Length;
            int n = sub.Length;
            bool[][] dp = new bool[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new bool[n + 1];
                dp[i][0] = true;
            }
            //we donot need to check all index of s as start of substring, just i<m-n+1
            for (int i = 0; i < m - n + 1; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (dp[i][j - 1])
                    {
                        // if two chars are equal or could be mapped
                        if (s[i + j - 1] == sub[j - 1] ||
                            (dict.ContainsKey(sub[j - 1]) && dict[sub[j - 1]].Contains(s[i + j - 1])))
                        {
                            dp[i][j] = true;
                        }
                        else break;
                    }
                    else break;
                }
                if (dp[i][n]) return true;
            }
            return false;
        }


        ///2302. Count Subarrays With Score Less Than K, #Sliding Window
        //The score of an array is defined as the product of its sum and its length.
        //For example, the score of[1, 2, 3, 4, 5] is (1 + 2 + 3 + 4 + 5) * 5 = 75.
        //Given a positive integer array nums and an integer k,
        //return the number of non-empty subarrays of nums whose score is strictly less than k.
        //A subarray is a contiguous sequence of elements within an array.
        public long CountSubarrays(int[] nums, long k)
        {
            long res = 0;
            long sum = 0;
            int left = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                while (left <= i && sum * (i - left + 1) >= k)
                {
                    sum -= nums[left++];
                }
                res += (i - left + 1);//count all subarrays which end at i , and start in [left,i]
            }
            return res;
        }

        ///2303. Calculate Amount Paid in Taxes
        //brackets[i] = [upperi, percenti] means that the ith tax bracket has an upper bound of upperi and
        //is taxed at a rate of percenti.
        //The brackets are sorted by upper bound (i.e. upperi-1 < upperi for 0 < i < brackets.length).
        public double CalculateTax(int[][] brackets, int income)
        {
            double res = 0;
            int prev = 0;
            foreach(var bracket in brackets)
            {
                int curr =Math.Min(bracket[0],income) - prev;
                res += curr * 1.0 * bracket[1] / 100;
                prev = bracket[0];
                if (bracket[0] >= income) break;
            }
            return res;
        }

        ///2304. Minimum Path Cost in a Grid, #DP, #HashMap
        public int MinPathCost(int[][] grid, int[][] moveCost)
        {
            Dictionary<int,int> dict = new Dictionary<int,int>();
            for(int i = 0; i < grid[0].Length; i++)
                dict.Add(i, grid[0][i]);

            for (int i = 1; i < grid.Length; i++)
            {
                var next = new Dictionary<int, int>();
                foreach(var k in dict.Keys)
                {
                    for(int j = 0; j < grid[0].Length; j++)
                    {
                        var cost = dict[k] + moveCost[grid[i-1][k]][j] + grid[i][j];
                        if (next.ContainsKey(j)) next[j] = Math.Min(cost, next[j]);
                        else next.Add(j, cost);
                    }
                }
                dict = next;
            }
            return dict.Values.Min();
        }

        ///2305. Fair Distribution of Cookies, #Backtracking
        //k that denotes the number of children to distribute all the bags of cookies to.
        //Return the minimum unfairness(max-total-cookies) of all distributions.
        //2 <= cookies.length <= 8
        public int DistributeCookies(int[] cookies, int k)
        {
            int res = int.MaxValue;
            int[] arr = new int[k];
            DistributeCookies(cookies, 0, arr, k, ref res);
            return res;
        }

        private void DistributeCookies(int[] cookies,int index, int[] arr,int k, ref int res)
        {
            if(index == cookies.Length)
            {
                res = Math.Min(res, arr.Max());
                return;
            }
            else
            {
                for(int i = 0; i < k; i++)
                {
                    var next = new int[k];
                    Array.Copy(arr, next,k);
                    next[i]+= cookies[index];
                    if (next[i] >= res) continue;
                    DistributeCookies(cookies, index + 1, next, k, ref res);
                }
            }
        }

    }
}
