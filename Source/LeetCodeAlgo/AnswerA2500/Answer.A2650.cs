using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2658. Maximum Number of Fish in a Grid
        //public int FindMaxFish(int[][] grid)
        //{
        //    int max = 0;
        //    int m = grid.Length;
        //    int n = grid[0].Length;
        //    for (int i = 0; i<m; i++)
        //    {
        //        for (int j = 0; j<n; j++)
        //        {
        //            if (grid[i][j]>0)
        //            {
        //                int sum = 0;
        //                FindMaxFish(grid, i, j, ref sum);
        //                max=Math.Max(max, sum);
        //            }
        //        }
        //    }

        //    return max;
        //}

        //private void FindMaxFish(int[][] grid, int i, int j, ref int sum)
        //{
        //    int m = grid.Length;
        //    int n = grid[0].Length;
        //    if (grid[i][j]==0) return;
        //    sum = sum+grid[i][j];
        //    grid[i][j] = 0;
        //    int[][] dxy4 = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
        //    foreach (var x in dxy4)
        //    {
        //        int r = i+x[0];
        //        int c = j+x[1];
        //        if (r>=0&&r<m && c>=0 && c<n && grid[r][c]>0)
        //        {
        //            FindMaxFish(grid, r, c, ref sum);
        //        }
        //    }
        //}
    }
}
