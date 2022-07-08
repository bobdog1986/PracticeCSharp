using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1455. Check If a Word Occurs As a Prefix of Any Word in a Sentence
        public int IsPrefixOfWord(string sentence, string searchWord)
        {
            var arr = sentence.Split(' ');
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].StartsWith(searchWord)) return i + 1;
            }
            return -1;
        }

        ///1456. Maximum Number of Vowels in a Substring of Given Length, #Sliding Window
        public int MaxVowels(string s, int k)
        {
            int len = 0;
            int count = 0;
            int max = 0;
            HashSet<char> set = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
            for (int i = 0; i < s.Length; i++)
            {
                len++;
                if (set.Contains(s[i])) count++;
                max = Math.Max(max, count);
                if (len == k)
                {
                    if (set.Contains(s[i + 1 - k])) count--;
                    len--;
                }
            }
            return max;
        }

        /// 1461. Check If a String Contains All Binary Codes of Size K
        ///return true if every binary code of length k is a substring of s. Otherwise, return false.
        public bool HasAllCodes(string s, int k)
        {
            var set = new HashSet<string>();
            int count = 1 << k;
            for (int i = 0; i < s.Length - k + 1; i++)
            {
                var curr = s.Substring(i, k);
                if (!set.Contains(curr))
                {
                    set.Add(curr);
                    if (set.Count == count) break;
                }
            }
            return set.Count == count;
        }

        /// 1464. Maximum Product of Two Elements in an Array
        ///Return the maximum value of (nums[i]-1)*(nums[j]-1).
        ///2 <= nums.length <= 500, 1 <= nums[i] <= 10^3
        public int MaxProduct(int[] nums)
        {
            int max1 = Math.Max(nums[0], nums[1]);
            int max2 = Math.Min(nums[0], nums[1]);
            for (int i = 2; i < nums.Length; i++)
            {
                if (nums[i] >= max1)
                {
                    max2 = max1;
                    max1 = nums[i];
                }
                else if (nums[i] > max2)
                {
                    max2 = nums[i];
                }
            }
            return (max1 - 1) * (max2 - 1);
        }

        ///1465. Maximum Area of a Piece of Cake After Horizontal and Vertical Cuts
        public int MaxArea(int h, int w, int[] horizontalCuts, int[] verticalCuts)
        {
            Array.Sort(horizontalCuts);
            Array.Sort(verticalCuts);
            long mod = 10_0000_0007;
            long maxH = Math.Max(horizontalCuts[0], h- horizontalCuts.Last());
            for(int i = 1; i < horizontalCuts.Length; i++)
                maxH = Math.Max(maxH, horizontalCuts[i] - horizontalCuts[i - 1]);

            long maxW = Math.Max(verticalCuts[0], w - verticalCuts.Last());
            for (int i = 1; i < verticalCuts.Length; i++)
                maxW = Math.Max(maxW, verticalCuts[i] - verticalCuts[i - 1]);

            return (int)(maxW * maxH % mod);
        }

        ///1466. Reorder Routes to Make All Paths Lead to the City Zero, #Graph, #DFS
        public int MinReorder(int n, int[][] connections)
        {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }
            foreach (var conn in connections)
            {
                graph[conn[0]].Add(conn[1]);
                graph[conn[1]].Add(-conn[0]);
            }
            var res = MinReorder_dfs(graph, new bool[n], 0);
            return res;
        }

        public int MinReorder_dfs(List<int>[] graph, bool[] visited, int from)
        {
            int change = 0;
            visited[from] = true;
            foreach (var to in graph[from])
                if (!visited[Math.Abs(to)])
                    change += MinReorder_dfs(graph, visited, Math.Abs(to)) + (to > 0 ? 1 : 0);
            return change;
        }

        ///1470. Shuffle the Array
        ///Given the array nums consisting of 2n elements in the form [x1,x2,...,xn,y1,y2,...,yn].
        ///Return the array in the form[x1, y1, x2, y2, ..., xn, yn].
        public int[] Shuffle(int[] nums, int n)
        {
            int[] res = new int[n * 2];
            for (int i = 0; i < n; i++)
            {
                res[i * 2] = nums[i];
                res[i * 2 + 1] = nums[i + n];
            }
            return res;
        }

        ///1472. Design Browser History, see BrowserHistory

        ///1473. Paint House III, #DP
        //1 <= m <= 100, 1 <= n <= 20, 0 <= houses[i] <= n, 1 <= cost[i][j] <= 104
        public int MinCost(int[] houses, int[][] cost, int m, int n, int target)
        {
            int MAX_COST = 1000001;
            int[][][] dp = init3DMatrix(m, n + 1, target + 1,MAX_COST);

            for (int i = 1; i <= n; i++)
            {
                if (houses[0] == 0) dp[0][i][1] = cost[0][i - 1];
                else if (houses[0] == i) dp[0][i][1] = 0;
            }

            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    // if I only have two houses, I can't make 4 neighbour hoods so no point in looking at them hence the min in the next expression
                    for (int k = 1; k <= Math.Min(target, i + 1); k++)
                    {

                        if (houses[i] != 0 && houses[i] != j) continue;
                        int currCost = (houses[i] == j) ? 0 : cost[i][j - 1];

                        for (int l = 1; l <= n; l++)
                        {
                            int newNeighour = (j == l) ? k : k - 1;
                            dp[i][j][k] = Math.Min(dp[i][j][k], currCost + dp[i - 1][l][newNeighour]);
                        }
                    }
                }
            }

            int minCostRes = MAX_COST;
            for (int l = 1; l <= n; l++)
            {
                minCostRes = Math.Min(minCostRes, dp[m - 1][l][target]);
            }

            return (minCostRes == MAX_COST) ? -1 : minCostRes;
        }

        /// 1476. Subrectangle Queries, see SubrectangleQueries

        ///1480. Running Sum of 1d Array
        public int[] RunningSum(int[] nums)
        {
            int sum = 0;
            for (int i = 0; i < nums.Length; ++i)
            {
                sum += nums[i];
                nums[i] = sum;
            }
            return nums;
        }

        /// 1482. Minimum Number of Days to Make m Bouquets , # Binary Search
        ///You want to make m bouquets. To make a bouquet, you need to use k adjacent flowers from the garden.
        ///The garden consists of n flowers, the ith flower will bloom in the bloomDay[i] and then can be used in exactly one bouquet.
        ///Return the minimum number of days you need to wait to be able to make m bouquets from the garden.
        ///If it is impossible to make m bouquets return -1.
        ///1 <= bloomDay.length <= 10^5, 1 <= bloomDay[i] <= 10^9, 1 <= m <= 10^6, 1 <= k <= bloomDay.length
        public int MinDays(int[] bloomDay, int m, int k)
        {
            if (m * k > bloomDay.Length)
                return -1;
            if (m * k == bloomDay.Length)
                return bloomDay.Max();
            int left = 1;
            int right = 1_000_000_000;
            while (left < right)
            {
                int mid = (left + right) / 2;
                int flowers = 0;
                int bouquet = 0;
                for (int i = 0; i < bloomDay.Length; i++)
                {
                    if (bloomDay[i] > mid)
                    {
                        flowers = 0;
                    }
                    else
                    {
                        flowers++;
                        if (flowers >= k)
                        {
                            bouquet++;
                            flowers = 0;
                        }
                    }
                }
                if (bouquet < m)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            return left;
        }

        ///1488. Avoid Flood in The City
        //rains[i] > 0 means there will be rains over the rains[i] lake.
        //rains[i] == 0 means there are no rains this day and you can choose one lake this day and dry it.
        //Return an array ans where:
        //ans[i] == -1 if rains[i] > 0.
        //ans[i] is the lake you choose to dry in the ith day if rains[i] == 0.
        public int[] AvoidFlood(int[] rains)
        {
            List<int> dryDays = new List<int>();
            int[] res = new int[rains.Length];
            Dictionary<int, int> lastRained = new Dictionary<int, int>();

            for (int i = 0; i < rains.Length; i++)
            {
                int lake = rains[i];
                if (lake == 0)
                {
                    dryDays.Add(i);
                    res[i] = 1;
                }
                else
                {
                    // There is already rain on this lake
                    if (lastRained.ContainsKey(lake))
                    {
                        int low = lastRained[lake];
                        int index = -1;

                        foreach (var dryDay in dryDays)
                        {
                            if (dryDay > low)
                            {
                                index = dryDay;
                                break;
                            }
                        }

                        if (index >= 0)
                        {
                            res[index] = lake;
                            dryDays.Remove(index);
                        }
                        else
                        {
                            return Array.Empty<int>();
                        }
                    }
                    //remember to update lastRained day of lake
                    //equal to dict.Add or update
                    lastRained[lake] = i;
                    res[i] = -1;
                }
            }

            return res;

        }

        /// 1491. Average Salary Excluding the Minimum and Maximum Salary
        ///1000 <= salary[i] <= 10^6,
        public double Average(int[] salary)
        {
            int sum = 0;
            int max = 1000;
            int min = 1000000;
            foreach (var n in salary)
            {
                max = Math.Max(max, n);
                min = Math.Min(min, n);
                sum += n;
            }

            sum = sum - max - min;

            return sum * 1.0 / (salary.Length - 2);
        }

        ///1492. The kth Factor of n
        ///You are given two positive integers n and k. A factor of an integer n is defined as an integer i where n % i == 0.
        ///Consider a list of all factors of n sorted in ascending order, return the kth factor in this list or return -1 if n has less than k factors.
        public int KthFactor(int n, int k)
        {
            var list = new List<int>();
            for (int i = 1; i <= n; i++)
                if (n % i == 0) list.Add(i);
            return list.Count >= k ? list[k - 1] : -1;
        }

        ///1496. Path Crossing
        public bool IsPathCrossing(string path)
        {
            int x=0, y = 0;
            HashSet<int> set = new HashSet<int>();
            set.Add(0);
            foreach(var c in path)
            {
                if (c == 'N') y++;
                else if (c == 'S') y--;
                else if (c == 'E') x++;
                else if (c == 'W') x--;
                if (set.Contains(x * 10000 + y)) return true;
                set.Add(x * 10000 + y);
            }
            return false;
        }

        ///1498. Number of Subsequences That Satisfy the Given Sum Condition, #Two Pointers
        //Return the number of non-empty subsequences of nums such that minimum + maximum <= target. modulo 109 + 7
        public int NumSubseq(int[] nums, int target)
        {
            long res = 0;
            long mod = 10_0000_0007;
            Array.Sort(nums);
            int n = nums.Length;
            int left = 0;
            int right = n - 1;

            //Math.Pow(2, right-left) will overflow, using dp
            long[] dp = new long[n];
            dp[0] = 1;
            for (int i = 1; i < n; i++)
                dp[i] = dp[i - 1] * 2 % mod;

            while (left <= right)
            {
                int sum = nums[left] + nums[right];
                if (sum > target)
                {
                    right--;
                }
                else
                {
                    res += dp[right - left];
                    left++;
                    res %= mod;
                }
            }
            return (int)(res % mod);
        }
    }
}