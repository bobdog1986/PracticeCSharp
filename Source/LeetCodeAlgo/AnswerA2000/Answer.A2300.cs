﻿using System;
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
                            left = mid;
                        else
                            right = mid - 1;
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
            foreach (var bracket in brackets)
            {
                int curr = Math.Min(bracket[0], income) - prev;
                res += curr * 1.0 * bracket[1] / 100;
                prev = bracket[0];
                if (bracket[0] >= income) break;
            }
            return res;
        }

        ///2304. Minimum Path Cost in a Grid, #DP, #HashMap
        public int MinPathCost_Dict(int[][] grid, int[][] moveCost)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < grid[0].Length; i++)
                dict.Add(i, grid[0][i]);
            for (int i = 1; i < grid.Length; i++)
            {
                var next = new Dictionary<int, int>();
                foreach (var k in dict.Keys)
                {
                    for (int j = 0; j < grid[0].Length; j++)
                    {
                        var cost = dict[k] + moveCost[grid[i - 1][k]][j] + grid[i][j];
                        if (next.ContainsKey(j)) next[j] = Math.Min(cost, next[j]);
                        else next.Add(j, cost);
                    }
                }
                dict = next;
            }
            return dict.Values.Min();
        }

        public int MinPathCost(int[][] grid, int[][] moveCost)
        {
            int colLen = grid[0].Length;
            int[] arr = grid[0];
            for (int i = 1; i < grid.Length; i++)
            {
                var next = new int[colLen];
                Array.Fill(next, int.MaxValue);
                for (int j = 0; j < colLen; j++)
                {
                    for (int k = 0; k < colLen; k++)
                    {
                        var cost = arr[j] + moveCost[grid[i - 1][j]][k] + grid[i][k];
                        next[k] = Math.Min(cost, next[k]);
                    }
                }
                arr = next;
            }
            return arr.Min();
        }

        ///2305. Fair Distribution of Cookies, #Backtracking
        //k that denotes the number of children to distribute all the bags of cookies to.
        //Return the minimum unfairness(max-total-cookies) of all distributions.
        //2 <= cookies.length <= 8
        public int DistributeCookies(int[] cookies, int k)
        {
            int res = int.MaxValue;
            int[] arr = new int[k];
            DistributeCookies_BackTracking(cookies, 0, arr, k, ref res);
            return res;
        }

        private void DistributeCookies_BackTracking(int[] cookies, int index, int[] arr, int k, ref int res)
        {
            if (index == cookies.Length)
            {
                res = Math.Min(res, arr.Max());
                return;
            }
            else
            {
                for (int i = 0; i < k; i++)
                {
                    if (arr[i] + cookies[index] >= res) continue;
                    arr[i] += cookies[index];
                    DistributeCookies_BackTracking(cookies, index + 1, arr, k, ref res);
                    arr[i] -= cookies[index];
                }
            }
        }

        ///2306. Naming a Company, #HashMap
        //Choose 2 distinct names from ideas, call them ideaA and ideaB.
        //Swap the first letters of ideaA and ideaB with each other.
        //If both of the new names are not found in the original ideas, then the name ideaA ideaBis a valid company name.
        //Otherwise, it is not a valid name.
        //Return the number of distinct valid names for the company.
        public long DistinctNames(string[] ideas)
        {
            long res = 0;
            Dictionary<char, HashSet<string>> dict = new Dictionary<char, HashSet<string>>();
            foreach (var idea in ideas)
            {
                if (!dict.ContainsKey(idea[0])) dict.Add(idea[0], new HashSet<string>());
                dict[idea[0]].Add(idea);
            }
            Dictionary<char, Dictionary<char, long>> set = new Dictionary<char, Dictionary<char, long>>();
            foreach (var k1 in dict.Keys)
            {
                set.Add(k1, new Dictionary<char, long>());
                foreach (var k2 in dict.Keys)
                {
                    if (k1 == k2) continue;
                    set[k1].Add(k2, 0);
                    foreach (var s in dict[k2])
                    {
                        var s2 = $"{k1}" + s.Substring(1);
                        if (!dict[k1].Contains(s2)) set[k1][k2]++;
                    }
                }
            }
            foreach (var k1 in dict.Keys)
            {
                foreach (var k2 in dict.Keys)
                {
                    if (k1 == k2) continue;
                    res += set[k1][k2] * set[k2][k1];
                }
            }
            return res;
        }

        ///2309. Greatest English Letter in Upper and Lower Case
        public string GreatestLetter(string s)
        {
            var dict = new Dictionary<char, int[]>();
            foreach(var c in s)
            {
                if (!dict.ContainsKey(char.ToUpper(c)))
                    dict.Add(char.ToUpper(c),new int[] {0,0 });
                int index = char.IsUpper(c) ? 0 : 1;
                dict[char.ToUpper(c)][index] = 1;
            }
            var keys = dict.Keys.Where(x => dict[x].Sum() == 2).OrderBy(x=>-x).ToList();
            return keys.Count() == 0 ? String.Empty : keys[0].ToString();
        }

        ///2310. Sum of Numbers With Units Digit K
        //The units digit of each integer is k.
        //The sum of the integers is num.
        //Return the minimum possible size of such a set, or -1 if no such set exists.
        public int MinimumNumbers(int num, int k)
        {
            if (num == 0)
                return 0;
            if (k == 0)
                return num % 10 == 0 ? 1 : -1;
            if (num % 10 == k) return 1;
            //max of result is 10, because if exist valid result, we must fint it in [1,10]
            //start with 1 and look for the target i that make unit k
            for (int i = 1; i <= num / k && i<=10; i++)
            {
                //if unit equal to k, we can pick any number in set to add the cap to num
                if (num % 10 == ((i * k) % 10)) // Look for equal unit's digit
                    return i;
            }
            return -1;
        }

        ///2311. Longest Binary Subsequence Less Than or Equal to K, #Greedy
        //Return the length of the longest subsequence of s that makes up a binary number <= k.
        //The subsequence can contain leading zeroes.
        //The empty string is considered to be equal to 0.
        public int LongestSubsequence(string s, int k)
        {
            int totalZeros = s.Count(x => x == '0');
            int ones = 0;
            int zeros = 0;
            long curr = 0;
            for(int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '0')
                    zeros++;
                else
                {
                    long val = (long)1 << (zeros + ones);
                    if(curr+val> k)
                        break;
                    else if (curr + val == k)
                    {
                        ones++;
                        break;
                    }
                    else
                    {
                        ones++;
                        curr += val;
                    }
                }
            }
            return totalZeros + ones;
        }

        ///2312. Selling Pieces of Wood, #DP
        //You are given two integers m and n that represent the height and width of a rectangular piece of wood.
        //You are also given prices, where prices[i] = [hi, wi, pricei] that height hi and width wi for pricei dollars.
        //To cut a piece of wood, cut across the entire height or width of the piece to split it into two smaller pieces.
        //After cutting a piece of wood into some number of smaller pieces, you can sell pieces according to prices.
        //You may sell multiple pieces of the same shape, and you do not have to sell all the shapes.
        //The grain of the wood makes a difference, so you cannot rotate a piece to swap its height and width.
        //Return the maximum money you can earn after cutting an m x n piece of wood.
        //1 <= m, n <= 200
        public long SellingWood(int m, int n, int[][] prices)
        {
            int[,] priceMat = new int[201,201];
            foreach (var p in prices)
            {
                priceMat[p[0],p[1]] = Math.Max(priceMat[p[0], p[1]], p[2]);
            }
            long[,] dp = new long[m + 1,n + 1];
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    dp[i,j] = priceMat[i,j];
                    //because cut across the entire height or width of the piece
                    for (int k = 1; k < i; k++)
                    {
                        dp[i,j] = Math.Max(dp[i,j], dp[k,j] + dp[i - k,j]);
                    }
                    for (int k = 1; k < j; k++)
                    {
                        dp[i,j] = Math.Max(dp[i,j], dp[i,k] + dp[i,j - k]);
                    }
                }
            }
            return dp[m,n];
        }

        //2315. Count Asterisks
        //Return the number of '*' in s, excluding the '*' between each pair of '|'.
        public int CountAsterisks(string s)
        {
            bool open = false;
            int res = 0;
            foreach(var c in s)
            {
                if(c== '|')
                    open = !open;
                else if (c == '*' && !open)
                    res++;
            }
            return res;
        }

        ///2316. Count Unreachable Pairs of Nodes in an Undirected Graph, #DFS
        //undirected graph with n nodes from 0 to n - 1. edges where edges[i] = [ai, bi]
        //Return the number of pairs of different nodes that are unreachable from each other.
        public long CountPairs(int n, int[][] edges)
        {
            long res = 0;
            var graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();

            foreach(var edge in edges)
            {
                graph[edge[0]].Add(edge[1]);
                graph[edge[1]].Add(edge[0]);
            }

            var visit = new HashSet<int>();
            long left = 0;
            for (int i = 0; i < n; i++)
            {
                if (visit.Contains(i)) continue;
                var set = new HashSet<int>();
                CountPairs_DFS(graph, i, set);
                left = (n - visit.Count);
                res += (long)set.Count*(left- set.Count);
                foreach (var x in set)
                    visit.Add(x);
            }
            left = (n - visit.Count);
            if (left > 1)
                res += getFactorial((int)left, 2) / 2;
            return res;
        }

        private void CountPairs_DFS(List<int>[] graph,int i,HashSet<int> set)
        {
            foreach(var x in graph[i])
            {
                if (set.Contains(x)) continue;
                set.Add(x);
                CountPairs_DFS(graph, x, set);
            }
        }

        ///2317. Maximum XOR After Operations
        //update nums[i] to be equal to nums[i] AND (nums[i] XOR x).
        //Return the maximum possible bitwise XOR of all elements of nums after applying the operation any number of times.
        public int MaximumXOR(int[] nums)
        {
            return nums.Aggregate((x, y) => x | y);
        }

        ///2318. Number of Distinct Roll Sequences, #DP
        //You are given an integer n. You roll a fair 6-sided dice n times.
        //Determine the total number of distinct sequences of rolls possible such that the following rules:
        //The greatest common divisor of any adjacent values in the sequence is equal to 1.
        //There is at least a gap of 2 rolls between equal valued rolls. abs(i - j) > 2.
        //Return the total number of distinct sequences possible. return it modulo 109 + 7.
        public int DistinctSequences(int n)
        {
            long res = 0;
            long mod = 10_0000_0007;
            //init non-GCD dictionary either same dictionary
            //eg. for 1, all [1,6] meet the GCD<=1 rule , but 1 is same , so dict[1] = {2,3,4,5,6}
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            dict.Add(1, new List<int>() { 2, 3, 4, 5, 6 });
            dict.Add(2, new List<int>() { 1, 3, 5, });
            dict.Add(3, new List<int>() { 1, 2, 4, 5 });
            dict.Add(4, new List<int>() { 1, 3, 5 });
            dict.Add(5, new List<int>() { 1, 2, 3, 4, 6 });
            dict.Add(6, new List<int>() { 1, 5, });
            //must use 3D matrix array, init seed data
            long[,,] dp = new long[n + 1,7,7];
            for(int i=1;i<=6;i++)
                dp[1, i, i] = 1;// normally index1 cannot equal index2, this is a tricky
            //i is round
            for (int i = 2; i <= n; i++)
            {
                //j means current number
                for (int j = 1; j <= 6; j++)
                {
                    //k means that last row number, eg dp[i,j,k] came from dp[i-1,k,x], x cannot be j due to abs(i,j)>2 rule
                    for (int k=1;k<=6; k++)
                    {
                        if (!dict[j].Contains(k)) continue;
                        for (int l = 1; l <= 6; l++)
                        {
                            if (l == j) continue;//no need check l==k, this will make the tricky work
                            dp[i, j, k] += dp[i - 1, k, l];
                            dp[i, j, k] %= mod;
                        }
                    }
                }
            }
            for (int i=1;i<=6;i++)
            {
                for(int j = 1; j <= 6; j++)
                {
                    res = (res + dp[n,i,j]) % mod;
                }
            }
            return (int)((res + mod) % mod);
        }

        ///2319. Check if Matrix Is X-Matrix
        //All the elements in the diagonals of the matrix are non-zero. All other elements are 0.
        public bool CheckXMatrix(int[][] grid)
        {
            int n = grid.Length;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    bool drag = i == j || n - 1 - i == j;
                    if ((drag && grid[i][j] == 0)
                        ||(!drag && grid[i][j] != 0)) return false;
                }
            }
            return true;
        }

        ///2320. Count Number of Ways to Place Houses, #DP
        //There is a street with n * 2 plots, where there are n plots on each side of the street.
        //The plots on each side are numbered from 1 to n.On each plot, a house can be placed.
        //Return the number of ways houses can be placed such that no two houses are adjacent to
        //each other on the same side of the street. return modulo 109 + 7.
        //Note that if a house is placed on the ith plot on one side of the street,
        //a house can also be placed on the ith plot on the other side of the street.
        public int CountHousePlacements(int n)
        {
            //dp[i]=dp[i-1]+dp[i-2], fibnacci
            long mod = 10_0000_0007;
            long a = 1;
            long dp = 1;
            for(int i = 1; i <= n; i++)
            {
                long b = a + dp;
                a = dp;
                dp = b% mod;
            }
            return (int)(dp * dp % mod);
        }

        ///2321. Maximum Score Of Spliced Array, #Kadane
        ///1 <= nums1[i], nums2[i] <= 104
        public int MaximumsSplicedArray(int[] nums1, int[] nums2)
        {
            int n = nums1.Length;
            int[] diff1 = new int[n];
            int[] diff2 = new int[n];
            int sum1 = 0;
            int sum2 = 0;
            for (int i = 0; i < n; i++)
            {
                diff1[i] = nums1[i] - nums2[i];
                diff2[i] = nums2[i] - nums1[i];
                sum1 += nums1[i];
                sum2 += nums2[i];
            }
            int max1 = MaxSubArray(diff1);
            int max2 = MaxSubArray(diff2);
            return Math.Max(sum2+max1,sum1+max2);
        }

        ///2325. Decode the Message
        public string DecodeMessage(string key, string message)
        {
            var dict = new Dictionary<char, int>();
            foreach(var c in key)
            {
                if (dict.Count == 26) break;
                if (c == ' ') continue;
                if (dict.ContainsKey(c)) continue;
                dict.Add(c, dict.Count);
            }
            return new string(message.Select(x => x == ' ' ? ' ' : (char)(dict[x] + 'a')).ToArray());
        }

        ///2326. Spiral Matrix IV
        public int[][] SpiralMatrix(int m, int n, ListNode head)
        {
            var res=new int[m][];
            for(int i = 0; i < m; i++)
            {
                res[i] = new int[n];
                Array.Fill(res[i], -1);
            }
            int direct = n==1?1: 0;
            int r = 0;
            int c = 0;
            int row1 = 0;
            int row2 = m - 1;
            int col1 = 0;
            int col2 = n - 1;
            while (head != null)
            {
                res[r][c] = head.val;
                if (direct == 0)
                {
                    c++;
                    if (c == col2)
                    {
                        row1++;
                        direct = 1;
                    }
                }
                else if (direct == 1)
                {
                    r++;
                    if (r == row2)
                    {
                        direct = 2;
                        col2--;
                    }
                }
                else if(direct == 2)
                {
                    c--;
                    if (c == col1)
                    {
                        row2--;
                        direct = 3;
                    }
                }
                else
                {
                    r--;
                    if (r == row1)
                    {
                        col1++;
                        direct = 0;
                    }
                }
                head = head.next;
            }
            return res;
        }

        ///2327. Number of People Aware of a Secret, #DP
        //On day 1, one person discovers a secret.
        //each person will share the secret with a new person every day,starting from delay days after discovering the secret.
        //each person will forget the secret forget days after discovering it.
        //A person cannot share the secret on the same day they forgot it, or on any day afterwards.
        //Given an integer n, return the number of people who know the secret at the end of day n. modulo 109 + 7.
        //2 <= n <= 1000,1 <= delay < forget <= n
        public int PeopleAwareOfSecret(int n, int delay, int forget)
        {
            long mod = 10_0000_0007;
            long[] shares = new long[n + 1];
            long[] forgets = new long[n + 1];
            if (1+delay <= n)
                shares[delay + 1] = 1;
            if (1+forget <= n)
                forgets[forget + 1] = 1;
            long shareToday = 0;
            long peopleKnow = 1;
            for (int i = delay+1; i <= n; i++)
            {
                shareToday += shares[i] % mod;
                shareToday -= forgets[i] % mod;
                peopleKnow -= forgets[i] % mod;
                if (i + delay <= n)
                    shares[i + delay] += shareToday % mod;
                if (i + forget <= n)
                    forgets[i + forget] += shareToday % mod;
                peopleKnow = (peopleKnow + shareToday) % mod;
            }
            return (int)(peopleKnow % mod);
        }

        public int PeopleAwareOfSecret_Lee215(int n, int delay, int forget)
        {
            long[] dp = new long[n + 1];
            long mod=10_0000_0007, share = 0, res = 0;
            dp[1] = 1;
            for (int i = 2; i <= n; ++i)
            {
                share = share + dp[Math.Max(i - delay, 0)] - dp[Math.Max(i - forget, 0)];
                share %= mod;
                dp[i] = share;
            }
            for (int i = n - forget + 1; i <= n; ++i)
                res = (res + dp[i]) % mod;
            return (int)res;
        }

        ///2331. Evaluate Boolean Binary Tree
        //Leaf nodes have either the value 0 or 1, where 0 represents False and 1 represents True.
        //Non-leaf nodes have either the value 2 or 3, where 2 represents the boolean OR and 3 represents the boolean AND
        public bool EvaluateTree(TreeNode root)
        {
            if (root.val == 0)
                return false;
            else if (root.val == 1)
                return true;
            else if (root.val == 2)
                return EvaluateTree(root.left) || EvaluateTree(root.right);
            else
                return EvaluateTree(root.left) && EvaluateTree(root.right);
        }

        ///2332. The Latest Time to Catch a Bus, #Greedy
        //
        public int LatestTimeCatchTheBus(int[] buses, int[] passengers, int capacity)
        {
            Array.Sort(buses);
            var pq =new PriorityQueue<int, int>();
            foreach(var p in passengers)
                pq.Enqueue(p, p);

            HashSet<int> set = new HashSet<int>();
            for(int i = 0; i < buses.Length - 1; i++)
            {
                var curr = capacity;
                while (curr > 0)
                {
                    if (pq.Peek() > buses[i]) break;
                    else
                    {
                        curr--;
                        set.Add(pq.Dequeue());
                    }
                }
            }
            int cap = capacity;
            while (cap > 0)
            {
                if (pq.Count == 0 || pq.Peek() > buses.Last())
                {
                    var ans = buses.Last();
                    while (set.Contains(ans))
                    {
                        ans--;
                    }
                    return ans;
                }
                else
                {
                    cap--;
                    set.Add(pq.Dequeue());
                }
            }
            var last = set.Last();
            var res = last - 1;
            while (set.Contains(res))
            {
                res--;
            }
            return res;
        }

        ///2333. Minimum Sum of Squared Difference, #Binary Search
        //The sum of squared difference of arrays nums1 and nums2 is defined as the sum of (nums1[i] - nums2[i])2
        //k1 and k2. You can modify any of the elements of nums1 by +1 or -1 at most k1 times. same for k2 on nums2;
        //Return the minimum sum of squared difference after modifying atmost k1 times and k2 times.
        public long MinSumSquareDiff(int[] nums1, int[] nums2, int k1, int k2)
        {
            int n = nums1.Length;
            long[] arr = new long[n];// abs(nums1[i]-nums2[i])
            for (int i = 0; i < n; i++)
            {
                arr[i] = Math.Abs(nums1[i] - nums2[i]);
            }
            Array.Sort(arr);//sort ascending
            long left = 0;
            long right = arr[n - 1];
            //using binary search to find the left value that all arr[i]>left can be changed to left
            while (left < right)
            {
                long mid = (left + right) / 2;
                long k = 0;
                for (int i = n - 1; i >= 0; i--)
                {
                    if (k > k1 + k2) break;
                    if (arr[i] > mid)
                    {
                        k += arr[i] - mid;
                    }
                    else break;
                }
                if (k <= k1 + k2)
                    right = mid;//this mid is available
                else
                    left = mid + 1;//not available, then +1
            }
            long curr = k1 + k2;
            for (int i = n - 1; i >= 0 && curr > 0; i--)
            {
                if (arr[i] <= left) break;
                var subtract = Math.Min(arr[i] - left, curr);
                curr -= subtract;
                arr[i] -= subtract;
            }
            //it may still left some k, then do subtract 1 on as many elements as possible
            for (int i = n - 1; i >= 0 && curr > 0; i--)
            {
                if (arr[i] <= 0) break;
                curr -= 1;
                arr[i] -= 1;
            }
            return arr.Sum(x => x * x);
        }


        ///2335. Minimum Amount of Time to Fill Cups
        //Every second, you can either fill up 2 cups with different types of water, or 1 cup of any type of water.
        //Return the minimum number of seconds needed to fill up all the cups.
        public int FillCups(int[] amount)
        {
            Array.Sort(amount);
            if (amount[2] >= amount[0] + amount[1])
                return amount[2];
            return (amount.Sum() + 1) / 2;
        }

        public int FillCups_PQ(int[] amount)
        {
            int res = 0;
            PriorityQueue<int,int> pq = new PriorityQueue<int, int>();
            foreach (var a in amount)
            {
                if (a > 0)
                    pq.Enqueue(a, -a);
            }
            while (pq.Count > 0)
            {
                if (pq.Count == 1)
                {
                    res += pq.Dequeue();
                }
                else
                {
                    var max = pq.Dequeue();
                    var mid = pq.Dequeue();
                    res++;
                    max--;
                    mid--;
                    if(max>0)
                        pq.Enqueue(max, -max);
                    if (mid > 0)
                        pq.Enqueue(mid, -mid);
                }
            }
            return res;
        }
    }
}