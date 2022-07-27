using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2200. Find All K-Distant Indices in an Array, in Easy


        /// 2201. Count Artifacts That Can Be Extracted
        ///Given a 0-indexed 2D integer array dig where dig[i] = [ri, ci] indicates that
        ///you will excavate the cell (ri, ci), return the number of artifacts that you can extract.
        public int DigArtifacts(int n, int[][] artifacts, int[][] dig)
        {
            int res = 0;

            bool[,] matrix = new bool[n, n];
            foreach (var d in dig)
            {
                matrix[d[0], d[1]] = true;
            }
            foreach (var arti in artifacts)
            {
                bool canDig = true;
                for (int i = arti[0]; i <= arti[2]; i++)
                {
                    for (int j = arti[1]; j <= arti[3]; j++)
                    {
                        if (!matrix[i, j])
                        {
                            canDig = false;
                            break;
                        }
                    }
                }
                if (canDig) res++;
            }
            return res;
        }

        ///2202. Maximize the Topmost Element After K Moves
        public int MaximumTop(int[] nums, int k)
        {
            int n = nums.Length;
            if (k == 0) return nums[0];
            if (k == 1) return n == 1 ? -1 : nums[1];
            if (n == 1) return k % 2 == 0 ? nums[0] : -1;

            int max = 0;
            for (int i = 0; i < Math.Min(k - 1, n); i++)
                //finding the max element from first k-1 elelment or len -1 if len is less than k
                max = Math.Max(max, nums[i]);

            if (k < n)  // check for scenario where we dont have to put back Max out of k-1 element
                max = Math.Max(max, nums[k]);
            return max;
        }

        ///2203. Minimum Weighted Subgraph With the Required Paths, #Graph, #Dijkstra
        //weighted directed graph, n nodes, edges[i] = [fromi, toi, weighti]
        //Return the minimum weight of a subgraph that it is possible to reach dest
        //from both src1 and src2 via a set of edges of this subgraph. If not exist, return -1.
        public long MinimumWeight(int n, int[][] edges, int src1, int src2, int dest)
        {
            List<int[]>[] graph = new List<int[]>[n];
            List<int[]>[] graphRev = new List<int[]>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int[]>();
                graphRev[i] = new List<int[]>();//reverse graph will be used to find path from dest to other nodes
            }
            foreach (var e in edges)
            {
                graph[e[0]].Add(new int[] { e[1], e[2] });
                graphRev[e[1]].Add(new int[] { e[0], e[2] });
            }

            var srcArr1 = getDijkstraCostArray(graph, src1);
            if (srcArr1[dest] == long.MaxValue) return -1;//no possible from src1 to dest

            var srcArr2 = getDijkstraCostArray(graph, src2);
            if (srcArr2[dest] == long.MaxValue) return -1;//no possible from src2 to dest
            long res = long.MaxValue;
            var destArr = getDijkstraCostArray(graphRev, dest);
            for(int i = 0; i < n; i++)
            {
                if (destArr[i] == long.MaxValue || srcArr1[i] == long.MaxValue || srcArr2[i] == long.MaxValue) continue;
                //try every valid node as common node, total path = src1->node + src2->node + dest->node(reversed)
                res = Math.Min(res, destArr[i] + srcArr1[i] + srcArr2[i]);
            }
            return res;
        }

        private long[] getDijkstraCostArray(List<int[]>[] graph, int src = 0)
        {
            int n = graph.Length;
            long[] distance = new long[n];
            Array.Fill(distance, long.MaxValue);
            distance[src] = 0;
            PriorityQueue<long[], long> pq = new PriorityQueue<long[], long>();
            //{index, cost} sort by cost-asc, visit shortest path first, it helps to skips longer paths later
            pq.Enqueue(new long[] { src, 0 }, 0);
            while (pq.Count > 0)
            {
                long[] top = pq.Dequeue();
                long u = top[0];
                long cost = top[1];
                if (cost > distance[u]) continue;//not shortest, skip it
                foreach (var v in graph[u])
                {
                    if (distance[v[0]] > cost + v[1])
                    {
                        distance[v[0]] = cost + v[1];//shorter path found, re-visit again
                        pq.Enqueue(new long[] { v[0], distance[v[0]] }, distance[v[0]]);
                    }
                }
            }
            return distance;
        }


        /// 2206. Divide Array Into Equal Pairs
        public bool DivideArray(int[] nums)
        {
            HashSet<int> set = new HashSet<int>();
            foreach (var n in nums)
            {
                if (set.Contains(n)) set.Remove(n);
                else set.Add(n);
            }
            return set.Count == 0;
        }

        ///2207. Maximize Number of Subsequences in a String
        ///pattern is 2 length, eg "ab"
        ///You can add either pattern[0] or pattern[1] anywhere in text exactly once.
        ///Note that the character can be added even at the beginning or at the end of text.
        ///Return the maximum number of times pattern can occur as a subsequence of the modified text.
        public long MaximumSubsequenceCount(string text, string pattern)
        {
            ///If we add pattern[0], the best option is to add at the begin.
            ///If we add pattern[1], the best option is to add at the end.
            long res = 0, cnt1 = 0, cnt2 = 0;
            for (int i = 0; i < text.Length; ++i)
            {
                if (text[i] == pattern[1])
                {
                    res += cnt1;//add all exist patterns
                    cnt2++;
                }
                if (text[i] == pattern[0])
                {
                    cnt1++;
                }
            }
            //res + max possible of p0 or p1
            return res + Math.Max(cnt1, cnt2);
        }

        /// 2208. Minimum Operations to Halve Array Sum, #PriorityQueue, 
        ///In one operation, you can choose any number from nums and reduce it to exactly half the number.
        ///(Note that you may choose this reduced number in future operations.)
        ///Return the minimum number of operations to reduce the sum of nums by at least half.
        public int HalveArray(int[] nums)
        {
            int res = 0;
            PriorityQueue<double, double> pq = new PriorityQueue<double, double>();
            double sum = 0;
            foreach (var n in nums)
            {
                pq.Enqueue(n, -n);
                sum += n;
            }
            double diff = 0;
            while (diff < sum / 2)
            {
                res++;
                var val = pq.Dequeue();
                diff += val / 2;
                pq.Enqueue(val / 2, -val / 2);
            }
            return res;
        }

        /// 2210. Count Hills and Valleys in an Array, #Two Pointers, #Greedy
        ///An index i is part of a hill in nums if the closest non-equal neighbors of i are smaller than nums[i].
        ///Similarly, an index i is part of a valley in nums if the closest non-equal neighbors of i are larger than nums[i].
        ///Adjacent indices i and j are part of the same hill or valley if nums[i] == nums[j].
        ///Note that for an index to be part of a hill or valley, it must have a non-equal neighbor on both the left and right sides.
        ///Return the number of hills and valleys in nums.
        public int CountHillValley(int[] nums)
        {
            int res = 0;
            int left = -1;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == nums[i + 1]) continue;
                int right = nums[i + 1];
                if (left != -1)
                {
                    if (nums[i] > left && nums[i] > right) res++;//hill
                    else if (nums[i] < left && nums[i] < right) res++;//valley
                }
                left = nums[i];
            }
            return res;
        }

        ///2211. Count Collisions on a Road
        public int CountCollisions(string directions)
        {
            int res = 0;
            char prev = 'L';
            int rights = 0;
            for (int i = 0; i < directions.Length; i++)
            {
                if (directions[i] == 'L')
                {
                    if (prev == 'R')
                    {
                        res += rights;
                        res++;
                        rights = 0;
                        prev = 'R';
                    }
                    else if (prev == 'S')
                    {
                        res++;
                        prev = 'S';
                    }
                    else
                    {
                        prev = 'L';
                    }
                }
                else if (directions[i] == 'S')
                {
                    if (prev == 'R')
                    {
                        res += rights;
                        rights = 0;
                    }
                    prev = directions[i];
                }
                else if (directions[i] == 'R')
                {
                    rights++;
                    prev = directions[i];
                }
            }
            return res;
        }

        /// 2212. Maximum Points in an Archery Competition, #Backtracking
        ///Return the array bobArrows which represents the number of arrows Bob shot on each scoring section from 0 to 11.
        ///The sum of the values in bobArrows should equal numArrows.
        ///If there are multiple ways for Bob to earn the maximum total points, return any one of them.
        public int[] MaximumBobPoints(int numArrows, int[] aliceArrows)
        {
            int[] res = new int[aliceArrows.Length];
            int max = int.MinValue;
            MaximumBobPoints_BackTracking(numArrows, aliceArrows, new int[aliceArrows.Length], aliceArrows.Length - 1, 0, ref max, ref res);
            return res;
        }

        private void MaximumBobPoints_BackTracking(int numArrows, int[] aliceArrows, int[] bobArrows, int index, int total, ref int max, ref int[] res)
        {
            if (index == 0)
            {
                bobArrows[0] = numArrows;
                if (total > max)
                {
                    max = total;
                    res = bobArrows;
                }
            }
            else
            {
                MaximumBobPoints_BackTracking(numArrows, aliceArrows, bobArrows, index - 1, total, ref max, ref res);
                if (numArrows > aliceArrows[index])
                {
                    var nextArr = new int[bobArrows.Length];
                    Array.Copy(bobArrows, nextArr, nextArr.Length);
                    nextArr[index] = aliceArrows[index] + 1;
                    MaximumBobPoints_BackTracking(numArrows - aliceArrows[index] - 1, aliceArrows, nextArr, index - 1, total + index, ref max, ref res);
                }
            }
        }

        ///2213. Longest Substring of One Repeating Character , #Segment Tree
        public int[] LongestRepeating(string s, string queryCharacters, int[] queryIndices)
        {
            int n = queryCharacters.Length;
            var root = new SegmentLongestSubstrTree(s);
            char[] arr = s.ToArray();
            int[] res = new int[n];
            for(int i = 0; i < n; i++)
            {
                int j = queryIndices[i];
                char c = queryCharacters[i];
                if ( c != arr[j])
                {
                    root.Update(j, c);
                    arr[j] = c;
                }
                res[i] = root.Max();
            }
            return res;
        }


        /// 2215. Find the Difference of Two Arrays, in Easy


        ///2216. Minimum Deletions to Make Array Beautiful, #Two Pointers
        ///The array nums is beautiful if: nums.length is even. nums[i] != nums[i + 1] for all i % 2 == 0.
        ///When you delete an element,all the elements to the right of the deleted element will be shifted one unit
        ///Return the minimum number of elements to delete from nums to make it beautiful.
        public int MinDeletion(int[] nums)
        {
            int res = 0;
            int i = 0;
            int j = 1;
            while (j < nums.Length)
            {
                if (nums[i] == nums[j])
                {
                    //if left == right , delete right, and move right-index j to next
                    res++;
                    j++;
                }
                else
                {
                    //update left to next of current right, then update right to next of left
                    i = j + 1;
                    j = i + 1;
                }
            }
            if ((nums.Length - res) % 2 == 1) res++;//if odd count of elements still in nums, delete the last one
            return res;
        }

        /// 2217. Find Palindrome With Fixed Length
        ///Given an integer array queries and a positive integer intLength, return an array answer where answer[i]
        ///is either the queries[i]th smallest positive palindrome of length intLength or -1 if no such palindrome exists.
        ///A palindrome is a number that reads the same backwards and forwards.Palindromes cannot have leading zeros.
        public long[] KthPalindrome(int[] queries, int intLength)
        {
            long[] res = new long[queries.Length];
            //maxCount for each intLength from 1 to 15
            Dictionary<int, long> maxCountDict = new Dictionary<int, long>();
            for (int i = 1; i <= 15; i++)
            {
                long count = 9;
                int j = i - 1;
                while (j-- > 0)
                    count *= 10;
                maxCountDict.Add(i, count);
            }
            //how many palindrome pairs
            int pair = (intLength + 1) / 2;
            for (int i = 0; i < queries.Length; i++)
            {
                //out of range, assign -1
                if (queries[i] > maxCountDict[pair]) res[i] = -1;
                else
                {
                    //from left/right to center
                    long[] list = new long[intLength];
                    long curr = queries[i];
                    for (int j = 0; j < pair; j++)
                    {
                        //get current index by divide to next total count of 10^(pair - j - 1)
                        long count = (long)Math.Pow(10, pair - j - 1);
                        long k = curr / count;
                        if (curr % count == 0) k--;//if mod is 0, need k--
                        curr -= k * count;
                        list[j] = k;
                        list[intLength - 1 - j] = k;//work for both odd and even intLength
                    }

                    //remove leading zero
                    list[0] = list[0] + 1;
                    list[intLength - 1] = list[0];//this will work for intLength=1, avoid duplicate +1
                    res[i] = long.Parse(String.Join("", list));
                }
            }
            return res;
        }

        ///2218. Maximum Value of K Coins From Piles, #DP, #Memo
        ///There are n piles of coins on a table. Each pile consists of a positive number of coins of assorted denominations.
        ///In one move, you can choose any coin on top of any pile, remove it, and add it to your wallet.
        ///Given a list piles, where piles[i] is a list of integers denoting the composition of the ith pile from top to bottom,
        ///and a positive integer k, return the maximum total value of coins you can have in your wallet
        ///if you choose exactly k coins optimally.
        public int MaxValueOfCoins(IList<IList<int>> piles, int k)
        {
            //dp[i,k] means picking k elements from pile[i] to pile[n-1].
            //We can pick 0,1,2,3... elements from the current pile[i] one by one.
            //It asks for the maximum total value of coins we can have,
            //so we need to return max of all the options.
            //Complexity:Time O(nm),Space O(nk)
            int[,] memo = new int[piles.Count + 1, k + 1];
            return MaxValueOfCoins_DP(piles, memo, 0, k);
        }

        private int MaxValueOfCoins_DP(IList<IList<int>> piles, int[,] memo, int i, int k)
        {
            if (k == 0 || i == piles.Count) return 0;
            if (memo[i, k] != 0) return memo[i, k];

            int res = MaxValueOfCoins_DP(piles, memo, i + 1, k);
            int sum = 0;

            for (int j = 0; j < Math.Min(piles[i].Count, k); j++)
            {
                sum += piles[i][j];
                res = Math.Max(res, sum + MaxValueOfCoins_DP(piles, memo, i + 1, k - j - 1));
            }
            memo[i, k] = res;
            return res;
        }

        ///2220. Minimum Bit Flips to Convert Number, in Easy

        ///2221. Find Triangular Sum of an Array, in Easy


        ///2222. Number of Ways to Select Buildings
        ///Find all combine of 010 or 101
        public long NumberOfWays(string s)
        {
            long ans = 0;
            int len = s.Length;

            long totOnes = 0;
            for (int i = 0; i < len; i++)
                totOnes += s[i] - '0';

            long totZeros = len - totOnes;
            long currZeros = s[0] == '0' ? 1 : 0;
            long currOnes = s[0] == '1' ? 1 : 0;

            for (int i = 1; i < len; i++)
            {
                if (s[i] == '0')
                {
                    ans += (currOnes * (totOnes - currOnes));
                    currZeros++;
                }
                else
                {
                    ans += (currZeros * (totZeros - currZeros));
                    currOnes++;
                }
            }
            return ans;
        }

        ///2223. Sum of Scores of Built Strings, #Z-Algorithm
        //Si->Sn, i is length from tail to head, sum the length of prefix of [S1...Sn] of s
        public long SumScores(string s)
        {
            long res = 0;
            var arr = getZArray(s.ToArray());
            foreach (var i in arr)
                res += i;//z-algorithm only calculate prefix
            res += s.Length;//we need add final whole s
            return res;
        }

        private int[] getZArray(char[] input)
        {
            int n = input.Length;
            int[] Z = new int[n];
            int left = 0, right = 0;
            for (int i = 1; i < n; i++)
            {
                if (i > right)
                {
                    left = right = i;
                    while (right < n && input[right] == input[right - left])
                    {
                        right++;
                    }
                    Z[i] = right - left;
                    right--;
                }
                else
                {
                    int k = i - left;
                    if (Z[k] < right - i + 1)
                    {
                        Z[i] = Z[k];
                    }
                    else
                    {
                        left = i;
                        while (right < n && input[right] == input[right - left])
                        {
                            right++;
                        }
                        Z[i] = right - left;
                        right--;
                    }
                }
            }
            return Z;
        }

        ///2224. Minimum Number of Operations to Convert Time
        //In one operation you can increase the time current by 1, 5, 15, or 60 minutes.
        //Return the minimum number of operations to convert current to correct.
        public int ConvertTime(string current, string correct)
        {
            DateTime start = DateTime.ParseExact(current, "HH:mm", null);
            DateTime end = DateTime.ParseExact(correct, "HH:mm", null);
            int diff = (int)((end - start).TotalMinutes);
            int res = 0;
            while (diff > 0)
            {
                res++;
                if (diff >= 60) diff -= 60;
                else if (diff >= 15) diff -= 15;
                else if (diff >= 5) diff -= 5;
                else if (diff >= 1) diff -= 1;
            }
            return res;
        }

        ///2225. Find Players With Zero or One Losses
        ///You are given an integer array matches where matches[i] = [winneri, loseri] indicates that
        ///the player winneri defeated player loseri in a match. Return a list answer of size 2 where:
        ///answer[0] is a list of all players that have not lost any matches.
        ///answer[1] is a list of all players that have lost exactly one match.
        ///The values in the two lists should be returned in increasing order.
        public IList<IList<int>> FindWinners(int[][] matches)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var match in matches)
            {
                if (!dict.ContainsKey(match[0])) dict.Add(match[0], 0);
                if (!dict.ContainsKey(match[1])) dict.Add(match[1], 0);
                dict[match[1]]++;
            }

            var res = new List<IList<int>>();
            var list1 = dict.Keys.Where(x => dict[x] == 0).OrderBy(x => x).ToList();
            var list2 = dict.Keys.Where(x => dict[x] == 1).OrderBy(x => x).ToList();
            res.Add(list1);
            res.Add(list2);
            return res;
        }

        ///2226. Maximum Candies Allocated to K Children, #Binary Search
        ///Each candy can split to any parts, return the maximum number of candies each child can get.
        public int MaximumCandies(int[] candies, long k)
        {
            //left inited as 0, if invalid then return 0
            int left = 0, right = 10_000_000;
            ///Tip1.left < right Vs left <= right
            ///keep using left < right ,never bother thinking it anymore.
            while (left < right)
            {
                long sum = 0;
                ///Tip2.mid = (left + right + 1) / 2 Vs mid = (left + right) / 2
                ///mid = (left + right) / 2 to find first element valid
                ///mid = (left + right + 1) / 2 to find last element valid
                int mid = (left + right + 1) / 2;
                foreach (int candy in candies)
                {
                    sum += candy / mid;
                }
                if (k > sum)
                    right = mid - 1;
                else
                    left = mid;//make left always valid
            }
            return left;
        }

        ///2227. Encrypt and Decrypt Strings, see Encrypter

        ///2231. Largest Number After Digit Swaps by Parity
        ///You are given a positive integer num. You may swap any two digits of num that have the same parity
        ///(i.e. both odd digits or both even digits).
        ///Return the largest possible value of num after any number of swaps.
        public int LargestInteger(int num)
        {
            List<int> evens = new List<int>();
            List<int> odds = new List<int>();
            List<bool> parities = new List<bool>();

            while (num > 0)
            {
                int mod = num % 10;
                if (mod % 2 == 0)
                    evens.Add(mod);
                else
                    odds.Add(mod);
                parities.Insert(0, mod % 2 == 0);
                num /= 10;
            }
            evens.Sort();
            odds.Sort();
            int i = evens.Count - 1;
            int j = odds.Count - 1;
            int res = 0;
            foreach (var parity in parities)
            {
                res *= 10;
                if (parity)
                    res += evens[i--];
                else
                    res += odds[j--];
            }
            return res;
        }

        ///2232. Minimize Result by Adding Parentheses to Expression
        ///You are given a 0-indexed string expression of the form "<num1>+<num2>" where <num1> and <num2> represent positive integers.
        ///Insert "()" to get the minimium value, return the final expression string
        public string MinimizeResult(string expression)
        {
            var arr = expression.Split("+");
            int res = int.MaxValue;
            int x = 0;
            int y = 0;
            for (int i = 0; i < arr[0].Length; i++)
            {
                for (int j = 0; j < arr[1].Length; j++)
                {
                    int val1 = i == 0 ? 1 : int.Parse(arr[0].Substring(0, i));
                    int val2 = int.Parse(arr[0].Substring(i)) + int.Parse(arr[1].Substring(0, j + 1));
                    int val3 = j == arr[1].Length - 1 ? 1 : int.Parse(arr[1].Substring(j + 1));
                    if (val1 * val2 * val3 < res)
                    {
                        res = val1 * val2 * val3;
                        x = i;
                        y = j;
                    }
                }
            }
            return arr[0].Insert(x, "(") + "+" + arr[1].Insert(y + 1, ")");
        }

        ///2233. Maximum Product After K Increments, #PriorityQueue, 
        ///You are given an array of non-negative integers nums and an integer k.
        ///In one operation, you may choose any element from nums and increment it by 1.
        ///Return the maximum product of nums after at most k operations.Since the answer may be very large, return it modulo 109 + 7
        public int MaximumProduct(int[] nums, int k)
        {
            if (nums.Length == 1) return nums[0] + k;
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            foreach (var n in nums)
                pq.Enqueue(n, n);

            while (k-- > 0)
            {
                var min = pq.Dequeue();
                min++;
                pq.Enqueue(min, min);
            }
            long res = 1;
            while (pq.Count > 0)
            {
                res *= pq.Dequeue();
                res %= 10_0000_0007;
            }
            return (int)res;
        }

        ///2235. Add Two Integers
        //public int Sum(int num1, int num2)
        //{
        //    return num1 + num2;
        //}

        ///2236. Root Equals Sum of Children
        //public bool CheckTree(TreeNode root)
        //{
        //    return root.val == root.left.val + root.right.val;
        //}

        ///2239. Find Closest Number to Zero
        public int FindClosestNumber(int[] nums)
        {
            int abs = Math.Abs(nums[0]);
            int res = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (Math.Abs(nums[i]) < abs || (Math.Abs(nums[i]) == abs && nums[i] > res))
                {
                    abs = Math.Abs(nums[i]);
                    res = nums[i];
                }
            }
            return res;
        }

        ///2240. Number of Ways to Buy Pens and Pencils
        ///Return the number of distinct ways you can buy some number of pens and pencils.
        public long WaysToBuyPensPencils(int total, int cost1, int cost2)
        {
            long res = 0;
            //including 0 combines
            for (int i = 0; i <= total / cost1; i++)
            {
                var curr = total - i * cost1;
                res += curr / cost2 + 1;
            }
            return res;
        }

        ///2241. Design an ATM Machine, see ATM

        ///2243. Calculate Digit Sum of a String
        public string DigitSum(string s, int k)
        {
            while (s.Length > k)
            {
                string temp = string.Empty;
                for (int i = 0; i < s.Length; i += k)
                {
                    int sum = 0;
                    for (int j = i; j < k + i && j < s.Length; j++)
                        sum += s[j] - '0';
                    temp += sum.ToString();
                }
                s = temp;
            }
            return s;
        }

        /// 2244. Minimum Rounds to Complete All Tasks
        ///Everytime you can complete 2 or 3 same level tasks, if task count of level is 1, return -1
        public int MinimumRounds(int[] tasks)
        {
            int res = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var task in tasks)
            {
                if (dict.ContainsKey(task)) dict[task]++;
                else dict.Add(task, 1);
            }

            foreach (var key in dict.Keys)
            {
                if (dict[key] == 1) return -1;
                if (dict[key] % 3 == 0) res += dict[key] / 3;
                else res += dict[key] / 3 + 1;
            }
            return res;
        }

        ///2245. Maximum Trailing Zeros in a Cornered Path, #DP, #Prefix Sum
        //max count of trailing zeros with at most 1 turn, Max of Min(count2,count5), grid[i][j]>=1
        public int MaxTrailingZeros(int[][] grid)
        {
            int ans = 0;

            int m = grid.Length;
            int n = grid[0].Length;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int count2 = CountOfVal(grid[i][j], 2);
                    int count5 = CountOfVal(grid[i][j], 5);
                    grid[i][j] = count2 * 100 + count5;
                }
            }

            //matrix count of 2, in row i, from j to k (j<=k) matrix2[i+1][k+1] - matrix2[i+1][j]
            int[][] matrix2row = new int[m + 1][];
            for (int i = 0; i <=m; i++)
                matrix2row[i] = new int[n + 1];
            int[][] matrix5row = new int[m + 1][];
            for (int i = 0; i <= m; i++)
                matrix5row[i] = new int[n + 1];

            //matrix count of 2, in col i, from j to k (j<=k) matrix[k+1][i+1] - matrix[j][i+1]
            int[][] matrix2col = new int[m + 1][];
            for (int i = 0; i <= m; i++)
                matrix2col[i] = new int[n + 1];
            int[][] matrix5col = new int[m + 1][];
            for (int i = 0; i <= m; i++)
                matrix5col[i] = new int[n + 1];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix2row[i + 1][j + 1] = matrix2row[i + 1][j] + grid[i][j]/100;
                    matrix5row[i + 1][j + 1] = matrix5row[i + 1][j] + grid[i][j]% 100;
                }
            }

            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < m; i++)
                {
                    matrix2col[i + 1][j + 1] = matrix2col[i][j + 1] + grid[i][j] / 100;
                    matrix5col[i + 1][j + 1] = matrix5col[i][j + 1] + grid[i][j] % 100;
                }
            }

            //move only horizontal, max of each row's Min(count2,count5)
            for (int i = 0; i < m; i++)
                ans = Math.Max(ans, Math.Min(matrix2row[i + 1][n], matrix5row[i + 1][n]));

            //move only vertically, max of each col's Min(count2,count5)
            for (int j = 0; j < n; j++)
                ans = Math.Max(ans, Math.Min(matrix2col[m][j + 1], matrix5col[m][j + 1]));

            //Find center of + then there are 4 directions
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //up (i,j) to (0,j)
                    int count2Up = matrix2col[i + 1][j + 1] - matrix2col[0][j + 1];
                    int count5Up = matrix5col[i + 1][j + 1] - matrix5col[0][j + 1];
                    //down (i,j) to (m-1,j)
                    int count2Down = matrix2col[m][j + 1] - matrix2col[i][j + 1];
                    int count5Down = matrix5col[m][j + 1] - matrix5col[i][j + 1];
                    //left (i,0) to (i,j)
                    int count2Left = matrix2row[i + 1][j + 1] - matrix2row[i + 1][0];
                    int count5Left = matrix5row[i + 1][j + 1] - matrix5row[i + 1][0];
                    //right (i,j) to (i,n-1)
                    int count2Right = matrix2row[i + 1][n] - matrix2row[i + 1][j];
                    int count5Right = matrix5row[i + 1][n] - matrix5row[i + 1][j];
                    //3.1 L turn
                    ans = Math.Max(ans, Math.Min((count2Up + count2Right - grid[i][j]/100), count5Up + count5Right - grid[i][j]%100));
                    //3.2 7 turn
                    ans = Math.Max(ans, Math.Min((count2Up + count2Left - grid[i][j] / 100), count5Up + count5Left - grid[i][j] % 100));
                    //3.3 |` turn
                    ans = Math.Max(ans, Math.Min((count2Down + count2Right - grid[i][j] / 100), count5Down + count5Right - grid[i][j] % 100));
                    //3.4 J turn
                    ans = Math.Max(ans, Math.Min((count2Down + count2Left - grid[i][j] / 100), count5Down + count5Left - grid[i][j] % 100));
                }
            }
            return ans;
        }

        private int CountOfVal(int x,int val)
        {
            int res = 0;
            while (x > 0)
            {
                if (x % val != 0) break;
                else
                {
                    res++;
                    x /= val;
                }
            }
            return res;
        }

        ///2246. Longest Path With Different Adjacent Characters,#Graph, #DP, #DFS
        //tree(connected, undirected graph that has no cycles) rooted at node 0 with n nodes indexed [0, n - 1].
        //array parent of size n, where parent[i] is the parent of node i. node 0 is the root, parent[0] == -1.
        //You are also given a string s of length n, where s[i] is the character assigned to node i.
        //Return the length of the longest path in the tree such that no adjacent nodes have the same character.
        public int LongestPath(int[] parent, string s)
        {
            int res = 0;
            int n = s.Length;
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();
            for (int i = 1; i < n; i++)
                graph[parent[i]].Add(i);//build the graph
            int[] memo = new int[n];//store the max-path from i-th node down to its childs
            LongestPath_DFS(graph, 0, '-', s, memo);//traversal from root-0
            res = memo.Max();//max path that from a node  down to its childs
            for (int i = 0; i < n; i++)
            {
                //because this graph is undirect, so path can also climb up
                //so we need find out 1st and 2nd max-path of everynode's childs that with diffierent char
                //then res=Math.Max(res, 1 + 1st + 2nd )， 1 is current node
                var diffNodes = graph[i].Where(x => s[x] != s[i]).ToList();
                foreach (var k1 in diffNodes)
                    foreach (var k2 in diffNodes)
                        if (k1 != k2)
                            res = Math.Max(res, 1 + memo[k1] + memo[k2]);
            }
            return res;
        }

        private int LongestPath_DFS(List<int>[] graph, int i, char prev, string s, int[] memo)
        {
            if (memo[i] != 0) return memo[i];
            int res = 1;
            foreach (var j in graph[i])
                res = Math.Max(res, 1 + LongestPath_DFS(graph, j, s[i], s, memo));
            memo[i] = res;
            if (s[i] == prev) return 0;
            else return memo[i];
        }

        /// 2248. Intersection of Multiple Arrays
        ///Given a 2D integer array nums where nums[i] is a non-empty array of distinct positive integers,
        ///return the list of integers that are present in each array of nums sorted in ascending order.
        public IList<int> Intersection_2248(int[][] nums)
        {
            HashSet<int> set = new HashSet<int>(nums[0]);
            for (int i = 1; i < nums.Length; i++)
            {
                HashSet<int> currSet = new HashSet<int>(nums[i]);
                foreach (var j in set)
                    if (!currSet.Contains(j)) set.Remove(j);
                if (set.Count == 0) break;
            }
            return set.OrderBy(x => x).ToList();
        }

        /// 2249. Count Lattice Points Inside a Circle
        ///circles[i] = [xi, yi, ri] ,return the number of lattice points that are present inside at least one circle.
        ///1 <= xi, yi <= 100
        public int CountLatticePoints(int[][] circles)
        {
            HashSet<int> set = new HashSet<int>();
            foreach (var c in circles)
            {
                int x = c[0];
                int y = c[1];
                int r = c[2];
                for (int i = x - r; i <= x + r; i++)
                {
                    for (int j = y; j <= y + r; j++)
                    {
                        if (set.Contains(i * 1000 + j)) continue;
                        if ((i - x) * (i - x) + (j - y) * (j - y) > r * r) break;
                        set.Add(i * 1000 + j);
                    }
                    for (int j = y - 1; j >= y - r; j--)
                    {
                        if (set.Contains(i * 1000 + j)) continue;
                        if ((i - x) * (i - x) + (j - y) * (j - y) > r * r) break;
                        set.Add(i * 1000 + j);
                    }
                }
            }
            return set.Count;
        }
    }
}