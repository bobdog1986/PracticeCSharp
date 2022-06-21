using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1603. Design Parking System

        ///1605. Find Valid Matrix Given Row and Column Sums, #Greedy
        ///Find any matrix of non-negative integers of size rowSum.length x colSum.length satisfies the Sum.
        ///0 <= rowSum[i], colSum[i] <= 10^8
        public int[][] RestoreMatrix(int[] rowSum, int[] colSum)
        {
            int rowLen= rowSum.Length;
            int colLen= colSum.Length;

            int[][] res= new int[rowLen][];
            for(int i=0; i<rowLen; i++)
                res[i]=new int[colLen];

            for(int i=0;i<rowLen; i++)
            {
                for(int j=0;j<colLen; j++)
                {
                    var val = Math.Min(rowSum[i], colSum[j]);
                    res[i][j]=val;
                    rowSum[i] -= val;
                    colSum[j] -= val;
                }
            }
            return res;
        }
        /// 1608. Special Array With X Elements Greater Than or Equal X
        ///Return x if the array is special, otherwise, return -1.
        ///0 <= nums[i] <= 1000
        public int SpecialArray(int[] nums)
        {
            int[] arr = new int[1001];
            foreach(var n in nums)
                arr[n]++;

            int count = 0;
            for(int i = 1000; i >= 0; i--)
            {
                count += arr[i];
                if (count == i) return i;
                else if (count > i) break;
            }
            return -1;
        }

        public int SpecialArray_BinarySearch(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums);
            //<= n
            for (int i = 0; i <= n; i++)
            {
                int left = 0, right = n;
                while (left < right)
                {
                    int mid = left + (right - left) / 2;
                    if (nums[mid] >= i) right = mid;
                    else left = mid + 1;
                }
                if ((n - left) == i) return i;//find how many num >=i
            }
            return -1;
        }
        /// 1609. Even Odd Tree, #BFS, #BTree
        ///A binary tree is named Even-Odd if it meets the following conditions:
        ///odd-indexed level, all nodes at the level have even integer values in strictly decreasing order(from left to right).
        ///even-indexed level, all nodes at the level have odd integer values in strictly increasing order (from left to right).
        public bool IsEvenOddTree(TreeNode root)
        {
            List<TreeNode> list =new List<TreeNode>() { root};
            bool odd = false;
            while(list.Count > 0)
            {
                List<TreeNode> next = new List<TreeNode>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (odd)
                    {
                        if (list[i].val % 2 == 1) return false;
                        if (i < list.Count - 1 && list[i].val <= list[i + 1].val) return false;
                    }
                    else
                    {
                        if (list[i].val % 2 == 0) return false;
                        if (i < list.Count - 1 && list[i].val >= list[i + 1].val) return false;
                    }
                    if (list[i].left != null) next.Add(list[i].left);
                    if (list[i].right != null) next.Add(list[i].right);
                }
                odd = !odd;
                list = next;
            }
            return true;
        }

        ///1615. Maximal Network Rank, #Graph
        ///Given the integer n and the array roads, return the maximal network rank of the entire infrastructure.
        ///two cities donot need connected
        public int MaximalNetworkRank(int n, int[][] roads)
        {
            int max = 0;
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();

            foreach (var road in roads)
            {
                graph[road[0]].Add(road[1]);
                graph[road[1]].Add(road[0]);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (graph[i].Contains(j))
                        max = Math.Max(max, graph[i].Count + graph[j].Count - 1);
                    else
                        max = Math.Max(max, graph[i].Count + graph[j].Count);
                }

            }
            return max;
        }

        ///1619. Mean of Array After Removing Some Elements
        // return the mean of the remaining integers after removing the smallest 5% and the largest 5% of the elements.
        public double TrimMean(int[] arr)
        {
            Array.Sort(arr);
            int sum = arr.Sum();
            int skips = arr.Length / 20;
            for(int i = 0; i < skips; i++)
            {
                sum -= arr[i];
                sum -= arr[arr.Length - 1 - i];
            }
            return sum * 1.0 / (arr.Length - 2 * skips);
        }

        ///1630. Arithmetic Subarrays
        ///Return a list of boolean elements answer, where answer[i] is true if the subarray
        ///nums[l[i]], nums[l[i]+1], ... , nums[r[i]] can be rearranged to form an arithmetic sequence or false.
        public IList<bool> CheckArithmeticSubarrays_QuickSort(int[] nums, int[] l, int[] r)
        {
            bool[] res=new bool[l.Length];
            for(int i= 0; i < l.Length; i++)
            {
                int len = r[i] -l[i]+1;
                int[] arr=new int[len];
                for(int j = l[i]; j <= r[i]; j++)
                {
                    arr[j - l[i]] = nums[j];
                }

                res[i] = CheckArithmeticSubarrays_QuickSort(arr);
            }
            return res.ToList();
        }
        private bool CheckArithmeticSubarrays_QuickSort(int[] arr)
        {
            if (arr.Length <= 2) return true;

            Array.Sort(arr);
            int diff = arr[1] - arr[0];
            for(int i = 2; i < arr.Length; i++)
            {
                if(arr[i] - arr[i-1]!= diff) return false;
            }
            return true;
        }

        public IList<bool> CheckArithmeticSubarrays(int[] nums, int[] l, int[] r)
        {
            bool[] res = new bool[l.Length];
            for (int i = 0; i < l.Length; i++)
            {
                res[i] = CheckArithmeticSubarrays_IsArithmeticSeq(nums, l[i], r[i]);
            }
            return res.ToList();
        }

        private bool CheckArithmeticSubarrays_IsArithmeticSeq(int[] nums, int start, int end)
        {
            if (end - start < 2) return true;

            int min = int.MaxValue, max = int.MinValue;
            HashSet<int> set = new HashSet<int>();
            for (int i = start; i <= end; i++)
            {
                min = Math.Min(min, nums[i]);
                max = Math.Max(max, nums[i]);
                set.Add(nums[i]);
            }

            if ((max - min) % (end - start) != 0) return false;
            int interval = (max - min) / (end - start);
            for (int i = 1; i <= end - start; i++)
            {
                if (!set.Contains(min + i * interval)) return false;
            }
            return true;
        }
        ///1631. Path With Minimum Effort, #Dijkstra
        /// A route's effort is the maximum absolute difference in heights between two consecutive cells of the route.
        ///Return the minimum effort required to travel from the top-left cell to the bottom-right cell.
        public int MinimumEffortPath(int[][] heights)
        {
            int rowLen = heights.Length;
            int colLen = heights[0].Length;

            int[][] dxy = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            int[][] efforts = new int[rowLen][];
            for(int i = 0; i < rowLen; i++)
            {
                efforts[i] = new int[colLen];
                for (int j = 0; j < colLen; j++)
                    efforts[i][j] = int.MaxValue;
            }

            efforts[0][0] = 0;
            PriorityQueue<int[], int> pq = new PriorityQueue<int[], int>();
            pq.Enqueue(new int[3],0);
            while (pq.Count>0)
            {
                int[] cur = pq.Dequeue();
                int effort = cur[0], x = cur[1], y = cur[2];
                if (x == rowLen - 1 && y == colLen - 1)
                {
                    return effort;
                }
                foreach(var d in dxy)
                {
                    int r = x + d[0], c = y + d[1];
                    if (0 <= r && r < rowLen && 0 <= c && c < colLen)
                    {
                        int nextEffort = Math.Max(effort, Math.Abs(heights[r][c] - heights[x][y]));
                        if (efforts[r][c] > nextEffort)
                        {
                            efforts[r][c] = nextEffort;
                            pq.Enqueue(new int[] { nextEffort, r, c }, nextEffort);
                        }
                    }
                }
            }
            return -1;
        }
        /// 1636. Sort Array by Increasing Frequency
        ///sort the array in increasing order based on the frequency of the values.
        ///If multiple values have the same frequency, sort them in decreasing order.
        public int[] FrequencySort(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var n in nums)
            {
                if(dict.ContainsKey(n))dict[n]++;
                else dict.Add(n, 1);
            }
            var keys=dict.Keys.OrderBy(x=>dict[x]).ThenBy(x=>-x);
            var res = new int[nums.Length];
            int i = 0;
            foreach(var key in keys)
            {
                while (dict[key]-- > 0)
                {
                    res[i++] = key;
                }
            }
            return res;
        }

        ///1637. Widest Vertical Area Between Two Points Containing No Points
        public int MaxWidthOfVerticalArea(int[][] points)
        {
            int res = 0;
            var list = points.Select(x=>x[0]).OrderBy(x =>x).ToList();
            for (int i = 0; i < list.Count - 1; i++)
                res = Math.Max(res, list[i + 1] - list[i]);
            return res;
        }

        ///1641. Count Sorted Vowel Strings, #DP
        ///return the number of strings of length n that consist only of vowels (a, e, i, o, u) and are lexicographically sorted.
        public int CountVowelStrings(int n)
        {
            int[] dp = new int[] { 0, 1, 1, 1, 1, 1 };
            for (int i = 1; i <= n; ++i)
                for (int k = 1; k <= 5; ++k)
                    dp[k] += dp[k - 1];
            return dp[5];
        }

        public int CountVowelStrings_DP(int n)
        {
            int a = 1, e = 1, i = 1, o = 1, u = 1;
            while (n-- > 1)
            {
                // add new char before prev string
                a = a + e + i + o + u; // a, e, i, o, u -> aa, ae, ai, ao, au
                e = e + i + o + u; // e, i, o, u -> ee, ei, eo, eu
                i = i + o + u; // i, o, u -> ii, io, iu
                o = o + u; // o, u -> oo, ou
                u = u; ; // u -> uu
            }
            return a + e + i + o + u;
        }

        public int CountVowelStrings_Math(int n)
        {
            return (n + 1) * (n + 2) * (n + 3) * (n + 4) / 24;
        }

        ///1642. Furthest Building You Can Reach, #PriorityQueue
        //Start from building index-0, If h[i + 1] > h[i], you can either use one ladder or(h[i + 1] - h[i]) bricks.
        //Return the furthest building index(0-indexed) you can reach if you use the given ladders and bricks optimally.
        public int FurthestBuilding(int[] heights, int bricks, int ladders)
        {
            int n = heights.Length;
            var minHeap = new PriorityQueue<int, int>();
            int total = 0;
            for(int i = 0; i < n - 1; i++)
            {
                int gap = heights[i + 1] - heights[i];
                if (gap > 0)
                {
                    minHeap.Enqueue(gap, gap);
                    if (minHeap.Count > ladders)
                    {
                        var top=minHeap.Dequeue();
                        total += top;
                        if (total > bricks) return i;
                    }
                }
            }
            return n - 1;
        }

        ///1648. Sell Diminishing-Valued Colored Balls, #Binary Search
        // if you own 6 yellow balls, the customer would pay 6 for the first yellow ball.
        // After the transaction, there are only 5 yellow balls left, so the next yellow ball is then valued at 5
        //1 <= inventory.length <= 10^5, 1 <= inventory[i] <= 10^9 ,1 <= orders <= min(sum(inventory[i]), 10^9)
        public int MaxProfit_1648(int[] inventory, int orders)
        {
            long res = 0;
            long mod = 10_0000_0007;
            inventory = inventory.OrderBy(x => -x).ToArray();//sort desc
            int n = inventory.Length;

            int left = 1;
            int right = 10_0000_0000;
            while (left < right)
            {
                int mid = (left + right + 1) / 2;//select the right center
                long sum = 0;
                foreach (var i in inventory)
                {
                    if (i < mid) break;//select all balls which count >=mid
                    else sum += i - mid + 1;//all balls [mid,i]
                }

                if (sum == orders)
                {
                    left = mid;
                    break;
                }
                else if (sum > orders)
                {
                    left = mid;
                }
                else
                {
                    right = mid - 1;//this cause we choose the right center
                }
            }
            //now we have the left-value, so we must :
            //First, select all >=left+1, if these balls not enough...
            //Then ,orders still >0, select orders count of left
            for (int i = 0; i < n && orders > 0; i++)
            {
                long a = inventory[i];
                if (a <= left) break;
                int count = Math.Min(inventory[i] - left, orders);//count is of range [left+1,inventory[i]]
                res += (a + (a - count + 1)) * count / 2;
                res %= mod;
                orders -= count;
            }

            res += orders % mod * left;//must % mod, or cause overflow
            return (int)(res % mod);
        }
    }
}
