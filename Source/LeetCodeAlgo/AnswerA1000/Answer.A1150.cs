using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {

        ///1162. As Far from Land as Possible, #Graph, #DP, #BFS
        ///0 represents water and 1 represents land,
        ///find a water cell such that its distance to the nearest land cell is maximized,
        ///and return the distance. If no land or water exists in the grid, return -1.
        public int MaxDistance1162_BFS(int[][] grid)
        {
            int rowLen=grid.Length;
            int colLen=grid[0].Length;
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
                        visited[i,j] = true;
                    }
                }
            }
            int level = -1;
            while (queue.Count>0)
            {
                int size = queue.Count;
                for (int i = 0; i < size; i++)
                {
                    int[] p = queue.Dequeue();
                    foreach (var  d in dxy4)
                    {
                        int r = p[0] + d[0];
                        int c = p[1] + d[1];
                        if (r >= 0 && r < rowLen && c >= 0 && c < colLen
                           && !visited[r,c] && grid[r][c] == 0)
                        {
                            visited[r,c] = true;
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
    }
}
