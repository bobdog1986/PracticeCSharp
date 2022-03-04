using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1905. Count Sub Islands, #Graph, #DFS
        ///Return the number of islands in grid2 that are considered sub-islands.
        public int CountSubIslands_My(int[][] grid1, int[][] grid2)
        {
            int rowLen = grid2.Length;
            int colLen = grid2[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            int ans = 0;
            bool[,] visit = new bool[rowLen, colLen];
            for (int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                {
                    if (grid2[i][j] == 0 || visit[i, j]) continue;
                    List<int[]> list = new List<int[]> { };
                    Queue<int[]> q = new Queue<int[]>();
                    var node = new int[] { i, j };
                    int subIsland = 1;//if not a subIsland, this will be 0
                    q.Enqueue(node);
                    list.Add(node);
                    visit[i, j] = true;
                    while (q.Count > 0)
                    {
                        var p = q.Dequeue();
                        if (grid1[p[0]][p[1]] == 0) subIsland = 0;//not a subIsland
                        foreach (var d in dxy4)
                        {
                            var r = p[0] + d[0];
                            var c = p[1] + d[1];
                            if (r >= 0 && r < rowLen && c >= 0 && c < colLen && grid2[r][c] == 1 && !visit[r, c])
                            {
                                visit[r, c] = true;
                                var next = new int[] { r, c };
                                q.Enqueue(next);
                                list.Add(next);
                            }
                        }
                    }
                    ans += subIsland;
                }
            return ans;
        }

        public int CountSubIslands(int[][] grid1, int[][] grid2)
        {
            int m = grid2.Length, n = grid2[0].Length, ans = 0;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (grid2[i][j] == 1)
                        ans += CountSubIslands_dfs(grid1, grid2, i, j);
            return ans;
        }


        private int CountSubIslands_dfs(int[][] grid1, int[][] grid2, int i, int j)
        {
            int m = grid2.Length, n = grid2[0].Length, ans = 1;
            if (i < 0 || i == m || j < 0 || j == n || grid2[i][j] == 0) return 1;
            grid2[i][j] = 0;
            ans &= CountSubIslands_dfs(grid1, grid2, i - 1, j);
            ans &= CountSubIslands_dfs(grid1, grid2, i + 1, j);
            ans &= CountSubIslands_dfs(grid1, grid2, i, j - 1);
            ans &= CountSubIslands_dfs(grid1, grid2, i, j + 1);
            return ans & grid1[i][j];
        }

        /// 1922. Count Good Numbers- not pass,support to 10^8, but input is 10^15
        ///even indices are even and the digits at odd indices are prime (2, 3, 5, or 7).
        public int CountGoodNumbers(long n)
        {
            long even = n / 2;

            long odd = n - even;

            var c = (power1922(5, odd)%Modulo) * (power1922(4, even) % Modulo) % Modulo;
            return (int)c;
        }

        public long Modulo = 1000000007;

        public long power1922(long seed, long pow)
        {
            if (pow == 0)
                return 1;

            if (pow == 1)
                return seed;

            var half = pow / 2;

            if (pow % 2 == 0)
            {
                return (power1922(seed, half) % Modulo) * (power1922(seed, half) % Modulo)% Modulo;
            }
            else
            {
                return (seed * (power1922(seed, half) % Modulo) * (power1922(seed, half) % Modulo)) % Modulo;
            }

        }
    }
}
