using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1154. Day of the Year
        public int DayOfYear(string date)
        {
            var arr = date.Split('-');
            int year = int.Parse(arr[0]);
            int month = int.Parse(arr[1]);
            int day=int.Parse(arr[2]);
            var dt = new DateTime(year, month, day);
            int res = 0;
            Dictionary<int, int> map = new Dictionary<int, int>()
            {
                {1,31 },{3,31 },{5,31 },{7,31 },{8,31 },{10,31 },{12,31 },
                {4,30 },{6,30 },{9,30 },{11,30 },
            };
            for(int i = 1; i < month; i++)
            {
                if (i == 2)
                {
                    if (DateTime.IsLeapYear(dt.Year)) res += 29;
                    else res += 28;
                }
                else res += map[i];
            }
            res += day;
            return res;
        }

        ///1155. Number of Dice Rolls With Target Sum, #DP
        //You have n dice and each die has k faces numbered from 1 to k.
        //Given three integers n, k, and target, return the number of possible ways(out of the kn total ways)
        //to roll the dice so the sum of the face-up numbers equals target. return it modulo 109 + 7.
        //1 <= n, k <= 30, 1 <= target <= 1000
        public int NumRollsToTarget(int n, int k, int target)
        {
            long mod = 10_0000_0007;
            var dp = init2DLongMatrix(n + 1, target + 1);
            dp[0][0] = 1;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= target; j++)
                {
                    for (int x = Math.Max(0, j - k); x <= j - 1; x++)
                    {
                        dp[i][j] += dp[i - 1][x];
                        dp[i][j] %= mod;
                    }
                }
            }
            return (int)(dp[n][target] % mod);
        }

        /// 1161. Maximum Level Sum of a Binary Tree, #BTree
        ///the level of its root is 1, the level of its children is 2, and so on.
        ///Return the smallest level x such that the sum of all the values of nodes at level x is maximal.
        public int MaxLevelSum(TreeNode root)
        {
            int max = int.MinValue;
            int res = 0;
            int level = 0;
            var list=new List<TreeNode>() { root};
            while (list.Count > 0)
            {
                var next = new List<TreeNode>();
                level++;
                int sum = 0;
                foreach(var i in list)
                {
                    if (i == null) continue;
                    sum += i.val;
                    if (i.left != null) next.Add(i.left);
                    if (i.right != null) next.Add(i.right);
                }
                if (sum > max)
                {
                    max = sum;
                    res = level;
                }
                list = next;
            }
            return res;
        }

        /// 1162. As Far from Land as Possible, #Graph, #DP, #BFS
        ///0 represents water and 1 represents land,
        ///find a water cell such that its distance to the nearest land cell is maximized,
        ///and return the distance. If no land or water exists in the grid, return -1.
        public int MaxDistance1162_BFS(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            bool[,] visited = new bool[rowLen, colLen];
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };

            Queue<int[]> queue = new Queue<int[]>();
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        queue.Enqueue(new int[] { i, j });
                        visited[i, j] = true;
                    }
                }
            }
            int level = -1;
            while (queue.Count > 0)
            {
                int size = queue.Count;
                for (int i = 0; i < size; i++)
                {
                    int[] p = queue.Dequeue();
                    foreach (var d in dxy4)
                    {
                        int r = p[0] + d[0];
                        int c = p[1] + d[1];
                        if (r >= 0 && r < rowLen && c >= 0 && c < colLen
                           && !visited[r, c] && grid[r][c] == 0)
                        {
                            visited[r, c] = true;
                            queue.Enqueue(new int[] { r, c });
                        }
                    }
                }
                level++;
            }
            return level <= 0 ? -1 : level;
        }

        public int MaxDistance1162_DP(int[][] grid)
        {
            int n = grid.Length, m = grid[0].Length, res = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (grid[i][j] == 1) continue;
                    grid[i][j] = 201; //201 here cuz as the despription, the size won't exceed 100.
                    if (i > 0) grid[i][j] = Math.Min(grid[i][j], grid[i - 1][j] + 1);
                    if (j > 0) grid[i][j] = Math.Min(grid[i][j], grid[i][j - 1] + 1);
                }
            }

            for (int i = n - 1; i > -1; i--)
            {
                for (int j = m - 1; j > -1; j--)
                {
                    if (grid[i][j] == 1) continue;
                    if (i < n - 1) grid[i][j] = Math.Min(grid[i][j], grid[i + 1][j] + 1);
                    if (j < m - 1) grid[i][j] = Math.Min(grid[i][j], grid[i][j + 1] + 1);
                    res = Math.Max(res, grid[i][j]); //update the maximum
                }
            }

            return res == 201 ? -1 : res - 1;
        }

        ///1170. Compare Strings by Frequency of the Smallest Character
        public int[] NumSmallerByFrequency(string[] queries, string[] words)
        {
            int n = queries.Length;
            int[] res = new int[n];
            var mat = words.Select(x => NumSmallerByFrequency(x)).OrderBy(x => -x).ToList();
            var qArr = queries.Select(x => NumSmallerByFrequency(x)).ToList();
            for(int i = 0; i < n; i++)
            {
                int count = 0;
                foreach(var x in mat)
                {
                    if (qArr[i] < x) count++;
                    else break;
                }
                res[i]= count;
            }
            return res;
        }

        private int NumSmallerByFrequency(string word)
        {
            int[] arr = new int[26];
            foreach (var c in word)
                arr[c - 'a']++;
            for (int i = 0; i < 26; i++)
                if (arr[i] != 0) return arr[i];
            return -1;
        }
        /// 1171. Remove Zero Sum Consecutive Nodes from Linked List
        public ListNode RemoveZeroSumSublists(ListNode head)
        {
            var node = head;

            Dictionary<int, ListNode> dict = new Dictionary<int, ListNode>();
            int sum = 0;
            dict.Add(0, null);
            while (node != null)
            {
                sum += node.val;
                if (dict.ContainsKey(sum))
                {
                    var last = dict[sum];
                    if (last == null)
                    {
                        while (head != node.next)
                        {
                            if (dict.ContainsValue(head))
                            {
                                dict.Remove(dict.FirstOrDefault(x => x.Value == head).Key);
                            }
                            head = head.next;
                        }
                    }
                    else
                    {
                        var curr = last.next;
                        while (curr != node.next)
                        {
                            if (dict.ContainsValue(curr))
                            {
                                dict.Remove(dict.FirstOrDefault(x => x.Value == curr).Key);
                            }
                            curr = curr.next;
                        }
                        last.next = node.next;
                    }
                }
                else
                {
                    dict.Add(sum, node);
                }

                node = node.next;
            }

            return head;
        }

        ///1185. Day of the Week
        public string DayOfTheWeek(int day, int month, int year)
        {
            var dt = new DateTime(year, month, day);
            return dt.DayOfWeek.ToString();
        }

        ///1186. Maximum Subarray Sum with One Deletion, #Kadane
        public int MaximumSum(int[] arr)
        {
            int n = arr.Length;
            int[] dp1 = new int[n];
            int[] dp2 = new int[n];

            dp1[0] = arr[0];
            dp2[n - 1] = arr[n - 1];
            int max = arr[0];

            for (int i = 1; i < n; i++)
            {
                //contain at least one element
                dp1[i] = dp1[i - 1] > 0 ? dp1[i - 1] + arr[i] : arr[i];
                max = Math.Max(max, dp1[i]);
            }
            for (int i = n - 2; i >= 0; i--)
            {
                dp2[i] = dp2[i + 1] > 0 ? dp2[i + 1] + arr[i] : arr[i];
                max = Math.Max(max, dp2[i]);
            }

            for (int i = 1; i < n - 1; i++)
            {
                if (arr[i] < 0)
                {
                    max = Math.Max(max, dp1[i - 1] + dp2[i + 1]);
                }
            }
            return max;
        }

        /// 1189. Maximum Number of Balloons
        ///Given a string text, you want to use the characters of text to form as many instances of the word "balloon" as possible.
        ///You can use each character in text at most once.Return the maximum number of instances that can be formed.
        public int MaxNumberOfBalloons(string text)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            Dictionary<char, int> dict = new Dictionary<char, int>();
            string balloonStr = "balloon";
            foreach (var c in balloonStr)
            {
                if (dict.ContainsKey(c)) dict[c]++;
                else dict.Add(c, 1);
                if(!map.ContainsKey(c))map.Add(c,0);
            }
            foreach (var c in text)
            {
                if (map.ContainsKey(c)) map[c]++;
            }
            int res = dict.Keys.Select(x => map[x] / dict[x]).Min();
            return res;
        }

        ///1190. Reverse Substrings Between Each Pair of Parentheses
        public string ReverseParentheses(string s)
        {
            int n = s.Length;
            Stack<int> opened = new Stack<int>();
            int[] pair = new int[n];
            for (int i = 0; i < n; ++i)
            {
                if (s[i] == '(')
                    opened.Push(i);
                if (s[i] == ')')
                {
                    int j = opened.Pop();
                    pair[i] = j;
                    pair[j] = i;
                }
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0, d = 1; i < n; i += d)
            {
                if (s[i] == '(' || s[i] == ')')
                {
                    i = pair[i];
                    d = -d;
                }
                else
                {
                    sb.Append(s[i]);
                }
            }
            return sb.ToString();
        }

        /// 1192. Critical Connections in a Network, #Tarjan's
        ///A critical connection is a connection that, if removed, will make some servers unable to reach some other server.
        /// Return all critical connections in the network in any order.

        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            IList<IList<int>> res = new List<IList<int>>();

            Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
            foreach (var connection in connections)
            {
                int node1 = connection[0];
                int node2 = connection[1];

                if (!graph.ContainsKey(node1))
                    graph[node1] = new List<int>();
                if (!graph.ContainsKey(node2))
                    graph[node2] = new List<int>();

                graph[node1].Add(node2);
                graph[node2].Add(node1);
            }

            int[] disc = new int[n]; // node id generated by DFS search
            int[] low = new int[n];  // smallest node id that current node can be reached from
            Array.Fill(disc, -1);    // -1 denotes the node is not visted
            int time = 0;
            for (int i = 0; i < n; i++)
            {
                if (disc[i] == -1)
                    CriticalConnections_DFS(i, i, disc, low, graph, res, ref time);
            }

            return res;
        }

        private void CriticalConnections_DFS(int u, int parent, int[] disc, int[] low, Dictionary<int, List<int>> graph, IList<IList<int>> res, ref int time)
        {
            disc[u] = time;
            low[u] = time;
            time++;

            for (int i = 0; i < graph[u].Count; i++)
            {
                int v = graph[u][i];
                if (v == parent)
                    continue;
                if (disc[v] == -1)
                {
                    // node v is not visited, keep DFS traverse
                    CriticalConnections_DFS(v, u, disc, low, graph, res,ref time);

                    // update the low-link value for u
                    low[u] = Math.Min(low[u], low[v]);

                    if (low[v] > disc[u])
                        res.Add(new List<int> { u, v });
                }
                else
                {
                    // if v is already visited, then u-v is a back-edge in the DFS tree.
                    // low stores the min node id from the subtree of u, so use disv[v] here.
                    low[u] = Math.Min(low[u], disc[v]);
                }
            }
        }

    }
}