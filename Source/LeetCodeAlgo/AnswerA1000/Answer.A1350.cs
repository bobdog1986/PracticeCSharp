using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1356. Sort Integers by The Number of 1 Bits
        ///1 <= arr.length <= 500, 0 <= arr[i] <= 10^4
        public int[] SortByBits(int[] arr)
        {
            return arr.OrderBy(x=> SortByBits_BitsCount(x)).ThenBy(x => x).ToArray();
        }

        public int SortByBits_BitsCount(int n)
        {
            int res = 0;
            while (n > 0)
            {
                if ((n & 1) == 1) res++;
                n >>= 1;
            }
            return res;
        }

        /// 1359. Count All Valid Pickup and Delivery Options, #DP
        ///Given n orders, each order consist in pickup and delivery services.
        ///Count all valid pickup/delivery possible sequences such that delivery(i) is always after of pickup(i).
        ///Since the answer may be too large, return it modulo 10^9 + 7.
        public int CountOrders(int n)
        {
            if (n == 1) return 1;
            long dp = 1;
            long mod = 10_0000_0007;
            int i = 2;
            while (i <= n)
            {
                dp *= (i * (2 * i - 1));
                dp %= mod;
                i++;
            }
            return (int)dp;
        }


        /// 1365. How Many Numbers Are Smaller Than the Current Number
        ///for each nums[i] find out how many numbers in the array are smaller than it.
        ///0 <= nums[i] <= 100, 2 <= nums.length <= 500
        public int[] SmallerNumbersThanCurrent(int[] nums)
        {
            int[] arr=new int[101];
            Dictionary<int, List<int>> map=new Dictionary<int, List<int>>();
            for(int i = 0; i < nums.Length; i++)
            {
                arr[nums[i]]++;
                if(!map.ContainsKey(nums[i]))
                    map[nums[i]] = new List<int>();
                map[nums[i]].Add(i);
            }

            int count=nums.Length;
            int[] ans = new int[nums.Length];
            for(int i=arr.Length-1; i>=0 && count>0; i--)
            {
                if (arr[i] == 0) continue;
                count-=arr[i];
                foreach(var index in map[i])
                    ans[index] = count;
            }
            return ans;
        }
        /// 1366. Rank Teams by Votes
        ///Return a string of all teams sorted by the ranking system.
        public string RankTeams(string[] votes)
        {
            int[,] matrix = new int[26, votes[0].Length];
            foreach(var vote in votes)
            {
                for(int i=0;i<vote.Length;i++)
                {
                    matrix[vote[i] - 'A', i]++;
                }
            }
            var teams= votes[0].ToList();
            teams.Sort((x, y) =>
            {
                int i = 0;
                while (i < votes[0].Length)
                {
                    if(matrix[x-'A',i]> matrix[y-'A', i])
                    {
                        return -1;
                    }
                    else if(matrix[x - 'A', i]< matrix[y - 'A', i])
                    {
                        return 1;
                    }
                    i++;
                }
                //if all same , sort by alphabetically
                return x -y;
            });
            return new string(teams.ToArray());
        }

        ///1375. Number of Times Binary String Is Prefix-Aligned
        ///Return the number of times the binary string is prefix-aligned during the flipping process.
        public int NumTimesAllBlue(int[] flips)
        {
            int right = 0;
            int ans = 0;
            int index = 1;
            foreach(var n in flips)
            {
                right = Math.Max(right, n);
                if (right == index) ans++;
                index++;
            }
            return ans;
        }

        ///1376. Time Needed to Inform All Employees, #Graph, #BFS, #DFS

        public int NumOfMinutes(int n, int headID, int[] manager, int[] informTime)
        {
            int res = 0;
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < n; i++)
                set.Add(i);
            set.Remove(headID);

            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(headID, 0);
            while (map.Count > 0)
            {
                Dictionary<int, int> next = new Dictionary<int, int>();

                foreach(var i in set)
                {
                    if (map.ContainsKey(manager[i]))
                    {
                        set.Remove(i);
                        next.Add(i, map[manager[i]] + informTime[manager[i]]);
                    }
                }

                if (next.Count > 0)
                    res = Math.Max(res, next.Values.Max());
                map = next;
            }
            return res;
        }
        public int NumOfMinutes_BFS(int n, int headID, int[] manager, int[] informTime)
        {
            List<int>[] graph =new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();

            for (int i = 0; i < n; i++)
                if (manager[i] != -1) graph[manager[i]].Add(i);

            Queue<int[]> q = new Queue<int[]>(); // Since it's a tree, we don't need `visited` array
            q.Enqueue(new int[] { headID, 0 });
            int ans = 0;
            while (q.Count>0)
            {
                int[] top = q.Dequeue();
                int u = top[0], w = top[1];
                ans = Math.Max(w, ans);
                foreach (int v in graph[u])
                    q.Enqueue(new int[] { v, w + informTime[u] });
            }
            return ans;
        }
        public int NumOfMinutes_DFS(int n, int headID, int[] manager, int[] informTime)
        {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();
            for (int i = 0; i < n; i++)
                if (manager[i] != -1) graph[manager[i]].Add(i);

            return NumOfMinutes_DFS(graph, headID, informTime);
        }
        public int NumOfMinutes_DFS(List<int>[] graph, int u, int[] informTime)
        {
            int ans = 0;
            foreach (int v in graph[u])
                ans = Math.Max(ans, NumOfMinutes_DFS(graph, v, informTime));
            return ans + informTime[u];
        }

        /// 1380. Lucky Numbers in a Matrix
        ///A lucky number is an element of the matrix such that it is the minimum element in its row and maximum in its column.
        ///1 <= n, m <= 50 , 1 <= matrix[i][j] <= 10^5.
        public IList<int> LuckyNumbers(int[][] matrix)
        {
            var rowLen = matrix.Length;
            var colLen=matrix[0].Length;
            int[] minOfRows=new int[rowLen];
            for(int i =0;i<rowLen;i++)
                minOfRows[i]=int.MaxValue;
            int[] maxOfCols=new int[colLen];
            for(int i=0;i<rowLen;i++)
                for(int j = 0; j < colLen; j++)
                {
                    minOfRows[i] = Math.Min(minOfRows[i], matrix[i][j]);
                    maxOfCols[j] = Math.Max(maxOfCols[j], matrix[i][j]);
                }
            var ans=new List<int>();
            for (int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                {
                    if(matrix[i][j] == minOfRows[i] && matrix[i][j] == maxOfCols[j])
                        ans.Add(matrix[i][j]);
                }
            return ans;
        }


        ///1394. Find Lucky Integer in an Array
        ///Given an array of integers arr, a lucky integer is an integer that has a frequency in the array equal to its value.
        ///Return the largest lucky integer in the array.If there is no lucky integer return -1.
        public int FindLucky(int[] arr)
        {
            Dictionary<int,int> map = new Dictionary<int,int>();
            foreach(var n in arr)
            {
                if (map.ContainsKey(n)) map[n]++;
                else map.Add(n, 1);
            }
            var keys=map.Keys.OrderBy(x=>-x);
            foreach(var key in keys)
                if (map[key] == key) return key;
            return -1;
        }

    }
}
