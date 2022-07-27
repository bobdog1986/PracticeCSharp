using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        /// <summary>
        /// get shortest ways from src to all nodes
        /// </summary>
        private long[] getDijkstraWaysArray(List<int[]>[] graph, int src=0)
        {
            int n = graph.Length;
            long[] ways = new long[n];
            long[] dp = new long[n];//store total cost from src to i
            Array.Fill(dp, long.MaxValue);
            dp[src] = 0;
            ways[src] = 1;
            long mod = 1_000_000_007;
            PriorityQueue<long[], long> pq = new PriorityQueue<long[], long>();
            //store {index, cost} by cost asc, visit shortest path first, it helps to skips longer paths later
            pq.Enqueue(new long[] { src, 0 }, 0);
            while (pq.Count > 0)
            {
                long[] top = pq.Dequeue();
                long u = top[0];
                long cost = top[1];
                if (cost > dp[u]) continue;//not shortest, skip it, donot use >= , this will skip first time call
                foreach (var v in graph[u])
                {
                    long nextCost = cost + v[1];
                    if (nextCost < dp[v[0]])
                    {
                        dp[v[0]] = nextCost;//shorter path found, update
                        ways[v[0]] = ways[u];//update
                        pq.Enqueue(new long[] { v[0], nextCost }, nextCost);//re-visit again
                    }
                    else if (dp[v[0]] == cost + v[1])
                    {
                        ways[v[0]] = (ways[v[0]] + ways[u]) % mod;//Dijkstra is BFS,no need to Enqueue,just update
                    }
                }
            }
            return ways;
        }

        private long[] getDijkstraWaysArray(List<int[]>[] graph, out long[] dist, int src=0)
        {
            int n = graph.Length;
            long[] ways = new long[n];
            long[] dp = new long[n];//store total cost from src to i
            Array.Fill(dp, long.MaxValue);
            dp[src] = 0;
            ways[src] = 1;
            long mod = 1_000_000_007;
            PriorityQueue<long[], long> pq = new PriorityQueue<long[], long>();
            //store {index, cost} by cost asc, visit shortest path first, it helps to skips longer paths later
            pq.Enqueue(new long[] { src, 0 }, 0);
            while (pq.Count > 0)
            {
                long[] top = pq.Dequeue();
                long u = top[0];
                long cost = top[1];
                if (cost > dp[u]) continue;//not shortest, skip it, donot use >= , this will skip first time call
                foreach (var v in graph[u])
                {
                    long nextCost = cost + v[1];
                    if (nextCost < dp[v[0]])
                    {
                        dp[v[0]] = nextCost;//shorter path found, update
                        ways[v[0]] = ways[u];//update
                        pq.Enqueue(new long[] { v[0], nextCost }, nextCost);//re-visit again
                    }
                    else if (dp[v[0]] == cost + v[1])
                    {
                        ways[v[0]] = (ways[v[0]] + ways[u]) % mod;//Dijkstra is BFS,no need to Enqueue,just update
                    }
                }
            }
            dist = dp;//set the output cost array
            return ways;
        }

        private long[] getDijkstraCostArray(List<int[]>[] graph, int src = 0)
        {
            int n = graph.Length;
            long[] dp = new long[n];//store cost
            Array.Fill(dp, long.MaxValue);
            dp[src] = 0;
            PriorityQueue<long[], long> pq = new PriorityQueue<long[], long>();
            //{index, cost} sort by cost-asc, visit shortest path first, it helps to skips longer paths later
            pq.Enqueue(new long[] { src, 0 }, 0);
            while (pq.Count > 0)
            {
                long[] top = pq.Dequeue();
                long u = top[0];
                long cost = top[1];
                if (cost > dp[u]) continue;//not shortest, skip it, donot use >= , this will skip first time call
                foreach (var v in graph[u])
                {
                    long nextCost = cost + v[1];
                    if (nextCost < dp[v[0]])
                    {
                        dp[v[0]] = nextCost;//shorter path found
                        pq.Enqueue(new long[] { v[0], nextCost }, nextCost);// re-visit again
                    }
                }
            }
            return dp;
        }

        /// <summary>
        /// find the max abs edge from src to any cell, abs edge is abs(grid[i,j] - grid[r,c]) of all neighbors
        /// </summary>
        private int[][] getDijkstraMaxAbsEdgeMatrix(int[][] grid, int srcX = 0, int srcY = 0)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            int[][] dxy = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            int[][] dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n];
                Array.Fill(dp[i], int.MaxValue);
            }
            dp[srcX][srcY] = 0;
            PriorityQueue<int[], int> pq = new PriorityQueue<int[], int>();
            pq.Enqueue(new int[3] { srcX, srcY, 0 }, 0);//{i,j, cost}, sort by cost asc
            while (pq.Count > 0)
            {
                int[] top = pq.Dequeue();
                int i = top[0],j = top[1], cost = top[2];
                foreach (var d in dxy)
                {
                    int r = i + d[0], c = j + d[1];
                    if (0 <= r && r < m && 0 <= c && c < n)
                    {
                        //this only find the MaxCost , not SumOfAll
                        int nextCost = Math.Max(cost, Math.Abs(grid[r][c] - grid[i][j]));
                        if (nextCost < dp[r][c])//shorter path found
                        {
                            dp[r][c] = nextCost;
                            pq.Enqueue(new int[] { r, c, nextCost }, nextCost);
                        }
                    }
                }
            }
            return dp;
        }
        /// <summary>
        /// find the max abs edge from src to dest , abs edge is abs(grid[i,j] - grid[r,c]) of all neighbors
        /// </summary>
        private int getDijkstraMaxAbsEdge(int[][] grid, int srcX = 0, int srcY = 0, int destX=-1, int destY=-1)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            if (destX == -1) destX = m - 1;
            if (destY == -1) destY = n - 1;
            int[][] dxy = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            int[][] dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n];
                Array.Fill(dp[i], int.MaxValue);
            }
            dp[srcX][srcY] = 0;
            PriorityQueue<int[], int> pq = new PriorityQueue<int[], int>();
            pq.Enqueue(new int[3] { srcX, srcY, 0 }, 0);//{i,j, cost}, sort by cost asc
            while (pq.Count > 0)
            {
                int[] top = pq.Dequeue();
                int i = top[0],j = top[1], cost = top[2];
                if (i == destX && j == destY)//first time found , then return
                    return cost;
                foreach (var d in dxy)
                {
                    int r = i + d[0], c = j + d[1];
                    if (0 <= r && r < m && 0 <= c && c < n)
                    {
                        //this only find the MaxCost , not SumOfAll
                        int nextCost = Math.Max(cost, Math.Abs(grid[r][c] - grid[i][j]));
                        if (nextCost < dp[r][c] ) //shorter abs cost found
                        {
                            dp[r][c] = nextCost;
                            pq.Enqueue(new int[] { r, c, nextCost }, nextCost);
                        }
                    }
                }
            }
            return dp[destX][destY];//never go here
        }

        /// <summary>
        /// shorest SumOfAbs from src to any grid cell
        /// </summary>
        private long[][] getDijkstraAbsCostMatrix(int[][] grid, int srcX = 0, int srcY = 0)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            int[][] dxy = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            long[][] dp = new long[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new long[n];
                Array.Fill(dp[i], long.MaxValue);
            }
            dp[srcX][srcY] = 0;
            PriorityQueue<long[], long> pq = new PriorityQueue<long[], long>();
            pq.Enqueue(new long[3] { srcX, srcY, 0 }, 0);//{i,j, cost}, sort by cost asc
            while (pq.Count > 0)
            {
                long[] top = pq.Dequeue();
                long i = top[0],j= top[1], cost = top[2];
                if (cost > dp[i][j]) continue;//donot use >= , this will skip first time call
                foreach (int[] d in dxy)
                {
                    long r = i + d[0], c = j + d[1];
                    if (0 <= r && r < m && 0 <= c && c < n)
                    {
                        long nextCost = cost + Math.Abs(grid[r][c] - grid[i][j]);
                        if (nextCost < dp[r][c])//shorter than current cost
                        {
                            dp[r][c] = nextCost;
                            pq.Enqueue(new long[] { r, c, nextCost }, nextCost);//re-visit
                        }
                    }
                }
            }
            return dp;
        }

        /// <summary>
        /// Ways (shortest sum of abs) from src to all grid cells
        /// </summary>
        private long[][] getDijkstraAbsWaysMatrix(int[][] grid, int srcX = 0, int srcY = 0)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            long mod = 1_000_000_007;
            int[][] dxy = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            long[][] ways = new long[m][];
            long[][] dp = new long[m][];
            for (int i = 0; i < m; i++)
            {
                ways[i] = new long[n];
                dp[i] = new long[n];
                Array.Fill(dp[i], long.MaxValue);
            }
            ways[srcX][srcY] = 1;
            dp[srcX][srcY] = 0;
            PriorityQueue<long[], long> pq = new PriorityQueue<long[], long>();
            pq.Enqueue(new long[3] { srcX, srcY, 0 }, 0);//{i,j, cost}, sort by cost asc
            while (pq.Count > 0)
            {
                long[] top = pq.Dequeue();
                long i = top[0], j = top[1], cost = top[2];
                if (cost > dp[i][j]) continue;//donot use >= , this will skip first time call
                foreach (int[] d in dxy)
                {
                    long r = i + d[0], c = j + d[1];
                    if (0 <= r && r < m && 0 <= c && c < n)
                    {
                        long nextCost = cost + Math.Abs(grid[r][c] - grid[i][j]);
                        if (nextCost < dp[r][c])
                        {
                            dp[r][c] = nextCost;//shorter path found, update
                            ways[r][c] = ways[i][j];//update
                            pq.Enqueue(new long[] { r, c, nextCost }, nextCost);//re-visit
                        }
                        else if(nextCost == dp[r][c])
                        {
                            ways[r][c] = (ways[r][c] + ways[i][j])% mod;//Dijkstra is BFS,no need to Enqueue,just update
                        }
                    }
                }
            }
            return ways;
        }

        private long[][] getDijkstraAbsWaysMatrix(int[][] grid, out long[][] dist, int srcX = 0, int srcY = 0)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            long mod = 1_000_000_007;
            int[][] dxy = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            long[][] ways = new long[m][];
            long[][] dp = new long[m][];
            for (int i = 0; i < m; i++)
            {
                ways[i] = new long[n];
                dp[i] = new long[n];
                Array.Fill(dp[i], long.MaxValue);
            }
            ways[srcX][srcY] = 1;
            dp[srcX][srcY] = 0;
            PriorityQueue<long[], long> pq = new PriorityQueue<long[], long>();
            pq.Enqueue(new long[3] { srcX, srcY, 0 }, 0);//{i,j, cost}, sort by cost asc
            while (pq.Count > 0)
            {
                long[] top = pq.Dequeue();
                long i = top[0], j = top[1], cost = top[2];
                if (cost > dp[i][j]) continue;//donot use >= , this will skip first time call
                foreach (int[] d in dxy)
                {
                    long r = i + d[0], c = j + d[1];
                    if (0 <= r && r < m && 0 <= c && c < n)
                    {
                        long nextCost = cost + Math.Abs(grid[r][c] - grid[i][j]);
                        if (nextCost < dp[r][c])
                        {
                            dp[r][c] = nextCost;//shorter path found, update
                            ways[r][c] = ways[i][j];//update
                            pq.Enqueue(new long[] { r, c, nextCost }, nextCost);//re-visit
                        }
                        else if (nextCost == dp[r][c])
                        {
                            ways[r][c] = (ways[r][c] + ways[i][j]) % mod;//Dijkstra is BFS,no need to Enqueue,just update
                        }
                    }
                }
            }
            dist = dp;
            return ways;
        }

    }
}
