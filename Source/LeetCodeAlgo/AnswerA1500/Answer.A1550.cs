using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1550. Three Consecutive Odds
        public bool ThreeConsecutiveOdds(int[] arr)
        {
            int count = 0;
            foreach (var n in arr)
            {
                if (n % 2 == 1)
                {
                    count++;
                    if (count == 3) return true;
                }
                else count = 0;
            }
            return false;
        }

        ///1551. Minimum Operations to Make Array Equal
        public int MinOperations_1551(int n)
        {
            int res = 0;
            int left = 0;
            int right = n - 1;
            while (left < right)
                res += right-- - left++;
            return res;
        }
        ///1552. Magnetic Force Between Two Balls, #Binary Search
        //that magnetic force between two different balls at positions x and y is |x - y|
        //Given the integer array position and the integer m.Return the max force.
        public int MaxDistance(int[] position, int m)
        {
            Array.Sort(position);
            int n = position.Length;
            int left = 1;
            int right = position[n - 1] - position[0];
            while (left < right)
            {
                int mid = (left + right + 1) / 2;//must select right side
                if (MaxDistance_Count(mid, position) >= m)
                    left = mid;
                else
                    right = mid - 1;
            }
            return left;
        }

        private int MaxDistance_Count(int minDist, int[] position)
        {
            int res = 1;
            int curr = position[0];
            for (int i = 1; i < position.Length; i++)
            {
                if (position[i] - curr >= minDist)
                {
                    res++;
                    curr = position[i];
                }
            }
            return res;
        }


        /// 1557. Minimum Number of Vertices to Reach All Nodes, #Graph
        ///Given a directed acyclic graph, with n vertices numbered from 0 to n-1,
        ///and an array edges where edges[i] = [fromi, toi] represents a directed edge from node fromi to node toi.
        ///Find the smallest set of vertices from which all nodes in the graph are reachable.
        public IList<int> FindSmallestSetOfVertices(int n, IList<IList<int>> edges)
        {
            var ans = new List<int>();
            int[] reachableArr = new int[n];
            for (int i = 0; i < edges.Count; i++)
            {
                reachableArr[edges[i][1]]++;
            }
            for (int i = 0; i < reachableArr.Length; i++)
            {
                if (reachableArr[i] == 0)
                    ans.Add(i);
            }
            return ans;
        }
        ///1561. Maximum Number of Coins You Can Get
        public int MaxCoins_1561(int[] piles)
        {
            int res = 0;
            Array.Sort(piles);
            int left = 0, right = piles.Length - 1;
            while (left++ < right--)
                res += piles[right--];
            return res;
        }
        ///1562. Find Latest Group of Size M
        //Given an array arr(indexed-1) that represents a permutation of numbers from 1 to n.
        //binary string of size n that initially its bits set to zero.
        //At each step (indexed-1) from 1 to n, the bit at position arr[i] is set to 1.
        //Find the latest step at which there exists a group of ones of length m. Or return -1.
        public int FindLatestStep(int[] arr, int m)
        {
            int res = -1;
            int n = arr.Length;
            if (m == n) return n;
            Dictionary<int, int> head = new Dictionary<int, int>();
            Dictionary<int, int> tail = new Dictionary<int, int>();
            for (int i = 0; i < n; ++i)
            {
                if (head.ContainsKey(arr[i] + 1) && tail.ContainsKey(arr[i] - 1))
                {
                    var start1 = tail[arr[i] - 1];
                    var end1 = arr[i] - 1;
                    var start2 = arr[i] + 1;
                    var end2 = head[arr[i] + 1];

                    tail.Remove(head[arr[i] + 1]);
                    head.Remove(tail[arr[i] - 1]);
                    head.Remove(arr[i] + 1);
                    tail.Remove(arr[i] - 1);

                    head.Add(start1, end2);
                    tail.Add(end2, start1);

                    if (end1 - start1 + 1 == m) res = i;
                    if (end2 - start2 + 1 == m) res = i;
                }
                else if (head.ContainsKey(arr[i] + 1))
                {
                    var start2 = arr[i] + 1;
                    var end2 = head[arr[i] + 1];

                    tail.Remove(head[arr[i] + 1]);
                    head.Remove(arr[i] + 1);

                    head.Add(arr[i], end2);
                    tail.Add(end2, arr[i]);

                    if (end2 - start2 + 1 == m) res = i;
                }
                else if (tail.ContainsKey(arr[i] - 1))
                {
                    var start1 = tail[arr[i] - 1];
                    var end1 = arr[i] - 1;

                    head.Remove(tail[arr[i] - 1]);
                    tail.Remove(arr[i] - 1);

                    tail.Add(arr[i], start1);
                    head.Add(start1, arr[i]);

                    if (end1 - start1 + 1 == m) res = i;
                }
                else
                {
                    head.Add(arr[i], arr[i]);
                    tail.Add(arr[i], arr[i]);
                }
            }
            return res;
        }

        public int FindLatestStep_Lee215(int[] arr, int m)
        {
            int res = -1;
            int n = arr.Length;
            int[] length = new int[n + 2];
            int[] count = new int[n + 1];
            for (int i = 0; i < n; ++i)
            {
                int a = arr[i];
                int left = length[a - 1];
                int right = length[a + 1];
                length[a] = length[a - left] = length[a + right] = left + right + 1;
                count[left]--;
                count[right]--;
                count[length[a]]++;
                if (count[m] > 0)
                    res = i + 1;
            }
            return res;
        }
        /// 1567. Maximum Length of Subarray With Positive Product
        public int GetMaxLen(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0] > 0 ? 1 : 0;

            int max = 0;

            int count = 0;
            int negCount = 0;
            int negStart = -1;
            int negEnd = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    if (count <= max)
                    {
                    }
                    else
                    {
                        if (negCount % 2 == 0)
                        {
                            max = count;
                        }
                        else
                        {
                            var temp = Math.Max(Math.Max(count - negStart - 1, negStart), Math.Max(count - negEnd - 1, negEnd));
                            max = Math.Max(max, temp);
                        }
                    }

                    count = 0;
                    negCount = 0;
                    negStart = -1;
                    negEnd = 0;
                }
                else
                {
                    if (nums[i] > 0)
                    {
                    }
                    else
                    {
                        if (negStart == -1)
                            negStart = count;

                        negEnd = count;

                        negCount++;
                    }
                    count++;
                }
            }

            if (negCount % 2 == 0)
            {
                max = Math.Max(max, count);
            }
            else
            {
                var temp = Math.Max(Math.Max(count - negStart - 1, negStart), Math.Max(count - negEnd - 1, negEnd));

                max = Math.Max(max, temp);
            }
            return max;
        }

        ///1572. Matrix Diagonal Sum
        ///Given a square matrix mat, return the sum of the matrix diagonals.
        public int DiagonalSum(int[][] mat)
        {
            int res = 0;
            for (int i = 0; i < mat.Length; i++)
            {
                res += mat[i][i];
                res += mat[i][mat.Length - 1 - i];
            }
            if (mat.Length % 2 == 1)
                res -= mat[mat.Length / 2][mat.Length / 2];
            return res;
        }

        ///1573. Number of Ways to Split a String
        //Given a binary string s, you can split s into 3 non-empty strings s1, s2, and s3 where s1 + s2 + s3 = s.
        //Return the number of ways s can be split such that the number of ones is the same in s1, s2, and s3. return it modulo 109 + 7.
        public int NumWays(string s)
        {
            var list = new List<int>();//get all indexes of '1'
            int n = s.Length;
            for (int i = 0; i < n; i++)
            {
                if (s[i] == '1')
                    list.Add(i);
            }
            long res = 0;
            long mod = 1_000_000_007;
            if (list.Count % 3 != 0) return 0;//no possible
            if (list.Count == 0)
            {
                //there will be n-2 position that we can pick any 2 of them to split s to 3 segments
                //eg, "0000", "0|0|0|0" , all position marked as |
                //eg, "00000", "0|0|0|0|0" , all position marked as |
                //=> calculate Combine(n-1,2) = Factoral(n-1,2)/Factoral(2);
                long x = n - 1;
                long y = n - 2;
                res = x * y / 2 % mod;
            }
            else
            {
                //every segment must contain list.Count / 3 ones
                //First split point : must start from list[list.Count / 3 - 1], then end before list[list.Count / 3],
                //Second split point : must start from list[list.Count / 3 *2- 1], then end before list[list.Count / 3*2],
                long x = list[list.Count / 3] - list[list.Count / 3 - 1];//all possible indexes for point1
                long y = list[list.Count / 3 * 2] - list[list.Count / 3 * 2 - 1];//all possible indexes for point2
                res = x * y % mod;
            }
            return (int)res;
        }

        ///1574. Shortest Subarray to be Removed to Make Array Sorted, #Two Pointers
        //remove a subarray(can be empty) from arr such that the remaining elements in arr are non-decreasing.
        //Return the length of the shortest subarray(contiguous) to remove.
        public int FindLengthOfShortestSubarray(int[] arr)
        {
            int n = arr.Length;
            int right = n - 1;
            while (right > 0 && arr[right] >= arr[right - 1])
                right--;

            int res = right; //[0,right-1]
            for (int left = 0; left < right && (left == 0 || arr[left - 1] <= arr[left]); left++)
            {
                while (right < n && arr[right] < arr[left])
                    right++;
                res = Math.Min(res, right - left - 1);
            }
            return res;
        }

        public int FindLengthOfShortestSubarray_My(int[] arr)
        {
            int n = arr.Length;
            if (n == 1) return 0;

            int right = n - 1;
            while (right > 0 && arr[right] >= arr[right - 1])
                right--;

            if (right == 0) return 0;

            int res = right;//[0,right-1]

            for (int left = 0; left < n && left < right; left++)
            {
                while (right < n && arr[right] < arr[left])
                    right++;

                if (left == n - 1) break;
                res = Math.Min(res, right - left - 1); //remove all [left+1,right-1]
                if (arr[left] > arr[left + 1])
                    break;
            }
            return res;
        }

        ///1575. Count All Possible Routes, #Graph, #DP
        //distinct positive integers array locations where locations[i] represents the position of city i.
        //integers start, finish and fuel representing the starting city, ending city, and the initial amount of fuel.
        //each step, you can move from city i to city j such that j != i and 0 <= j < locations.length.
        //Moving from city i to city j reduces the amount of fuel you have by |locations[i] - locations[j]|
        //Notice that fuel cannot become negative at any point in time,
        //and that you are allowed to visit any city more than once (including start and finish).
        //Return the count of all possible routes from start to finish. return it modulo 109 + 7.
        public int CountRoutes(int[] locations, int start, int finish, int fuel)
        {
            int n = locations.Length;
            long[][] memo = new long[n][];
            for (int i = 0; i < n; i++)
            {
                memo[i] = new long[fuel + 1];
                Array.Fill(memo[i], -1l);
            }
            return (int)CountRoutes_DFS(locations, start, finish, fuel, memo);
        }

        private long CountRoutes_DFS(int[] locations, int city, int finish, int fuel, long[][] memo)
        {
            long mod = 1_000_000_007;
            if (fuel < 0) return 0;
            if (memo[city][fuel] != -1) return memo[city][fuel];
            long res = 0;
            if (city == finish) res += 1;
            for (int i = 0; i < locations.Length; i++)
            {
                if (i == city) continue;
                int fuelLeft = fuel - Math.Abs(locations[i] - locations[city]);
                if (fuelLeft >= 0)
                    res = (res + CountRoutes_DFS(locations, i, finish, fuelLeft, memo)) % mod;
            }
            memo[city][fuel] = res;
            return res;
        }

        public int CountRoutes_DP(int[] locations, int start, int finish, int fuel)
        {
            int n = locations.Length;
            long[][] dp = new long[n][];
            for (int i = 0; i < n; i++)
            {
                dp[i] = new long[fuel + 1];
            }
            Array.Fill(dp[finish], 1);
            long mod = 1_000_000_007;
            for (int j = 0; j <= fuel; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (k == i) continue;
                        int cost = Math.Abs(locations[i] - locations[k]);
                        if (cost <= j)
                        {
                            dp[i][j] = (dp[i][j] + dp[k][j - cost]) % mod;
                        }
                    }
                }
            }
            return (int)dp[start][fuel];
        }
        /// 1576. Replace All ?'s to Avoid Consecutive Repeating Characters
        /// replace ? to not same as previous or next char
        public string ModifyString(string s)
        {
            var arr = s.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '?')
                {
                    var c = 'a';
                    while (c <= 'z')
                    {
                        //if not same as previous or next char, update it
                        if ((i == 0 || arr[i - 1] != c)
                            && (i == arr.Length - 1 || arr[i + 1] == '?' || arr[i + 1] != c))
                        {
                            arr[i] = c;
                            break;
                        }
                        c++;
                    }
                }
            }
            return String.Join("", arr);
        }

        ///1577. Number of Ways Where Square of Number Is Equal to Product of Two Numbers
        // public int NumTriplets(int[] nums1, int[] nums2)
        // {
        //     int res = 0;
        //     var square1 = new Dictionary<long, int>();
        //     foreach (var i in nums1)
        //     {
        //         long x = (long)i * i;
        //         if (square1.ContainsKey(x)) square1[x]++;
        //         else square1.Add(x, 1);
        //     }

        //     var multiple1 = new Dictionary<long, int>();
        //     for (int i = 0; i < nums2.Length; i++)
        //     {
        //         for (int j = i + 1; j < nums2.Length; j++)
        //         {
        //             long x = (long)nums2[i] * nums2[j];
        //             if (multiple1.ContainsKey(x)) multiple1[x]++;
        //             else multiple1.Add(x, 1);
        //         }
        //     }

        //     foreach (var k1 in square1.Keys)
        //     {
        //         foreach (var k2 in multiple1.Keys)
        //         {
        //             if (k1 == k2)
        //             {
        //                 res += square1[k1] * multiple1[k2];
        //             }
        //         }
        //     }
        //     //
        //     var square2 = new Dictionary<long, int>();
        //     foreach (var i in nums2)
        //     {
        //         long x = (long)i * i;
        //         if (square2.ContainsKey(x)) square2[x]++;
        //         else square2.Add(x, 1);
        //     }

        //     var multiple2 = new Dictionary<long, int>();
        //     for (int i = 0; i < nums1.Length; i++)
        //     {
        //         for (int j = i + 1; j < nums1.Length; j++)
        //         {
        //             long x = (long)nums1[i] * nums1[j];
        //             if (multiple2.ContainsKey(x)) multiple2[x]++;
        //             else multiple2.Add(x, 1);
        //         }
        //     }

        //     foreach (var k1 in square2.Keys)
        //     {
        //         foreach (var k2 in multiple2.Keys)
        //         {
        //             if (k1 == k2)
        //             {
        //                 res += square2[k1] * multiple2[k2];
        //             }
        //         }
        //     }
        //     return res;
        // }

        ///1578. Minimum Time to Make Rope Colorful, #Greedy
        public int MinCost(string colors, int[] neededTime)
        {
            int res = 0;
            int sum = 0;
            int max = 0;
            int n = colors.Length;
            for (int i = 0; i < n; i++)
            {
                if (i > 0 && colors[i] != colors[i - 1])
                {
                    //remove all but exclude the max of consecutive same color balloons
                    res += sum - max;
                    sum = 0;
                    max = 0;
                }
                sum += neededTime[i];
                max = Math.Max(max, neededTime[i]);
            }
            res += sum - max;
            return res;
        }

        ///1582. Special Positions in a Binary Matrix
        //public int NumSpecial(int[][] mat)
        //{
        //    int res = 0;
        //    int[] rows = mat.Select(x => x.Sum()).ToArray();
        //    int[] cols = new int[ mat[0].Length];
        //    for(int j = 0; j<mat[0].Length; j++)
        //    {
        //        int count = 0;
        //        for (int i = 0; i<mat.Length; i++)
        //            count+=mat[i][j];
        //        cols[j] = count;
        //    }

        //    for(int i = 0; i<mat.Length; i++)
        //    {
        //        for(int j = 0; j<mat[0].Length; j++)
        //        {
        //            if (rows[i]==1 && cols[j]==1 && mat[i][j]==1)
        //                res++;
        //        }
        //    }
        //    return res;
        //}

        ///1584. Min Cost to Connect All Points, #Greedy, #Prim's algorithm,
        ///Return the minimum cost to make all points connected.
        public int MinCostConnectPoints(int[][] points)
        {
            int n = points.Length;
            int ans = 0;
            HashSet<int> set = new HashSet<int>();
            set.Add(0);
            int[] dist = new int[n];
            for (int i = 1; i < n; i++)
                dist[i] = MinCostConnectPoints_ManhattanDist(points, 0, i);
            while (set.Count != n)
            {
                // Find the node that has shortest distance
                int next = -1;
                for (int i = 0; i < n; i++)
                {
                    if (set.Contains(i)) continue;
                    if (next == -1 || dist[next] > dist[i])
                        next = i;
                }
                // Put the node into the Minning Spanning Tree
                set.Add(next);
                ans += dist[next];
                // Update distance array
                for (int i = 0; i < n; i++)
                {
                    if (!set.Contains(i))
                    {
                        dist[i] = Math.Min(dist[i], MinCostConnectPoints_ManhattanDist(points, i, next));
                    }
                }
            }
            return ans;
        }
        private int MinCostConnectPoints_ManhattanDist(int[][] points, int source, int dest)
        {
            return Math.Abs(points[source][0] - points[dest][0]) + Math.Abs(points[source][1] - points[dest][1]);
        }

        ///1588. Sum of All Odd Length Subarrays
        public int SumOddLengthSubarrays(int[] arr)
        {
            int res = 0, n = arr.Length;
            for (int i = 0; i < n; ++i)
            {
                var count = ((i + 1) * (n - i) + 1) / 2;
                res += count * arr[i];
            }
            return res;
        }

        ///1592. Rearrange Spaces Between Words
        public string ReorderSpaces(string text)
        {
            int n = text.Length;
            var words = text.Split(' ').Where(x => x.Length > 0).ToArray();
            if (words.Length == 1)
                return words[0].PadRight(n, ' ');
            int len = words.Sum(x => x.Length);
            int spaces = n - len;
            int interval = spaces / (words.Length - 1);
            string str = "";
            while (interval-- > 0)
                str += " ";
            return string.Join(str, words).PadRight(n, ' ');
        }
    }
}
