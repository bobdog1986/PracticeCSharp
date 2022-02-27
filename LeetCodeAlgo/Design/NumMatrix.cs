using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    /// 304. Range Sum Query 2D - Immutable
    /// Calculate the sum of the elements of matrix inside the rectangle defined by
    /// its upper left corner (row1, col1) and lower right corner (row2, col2).
    public class NumMatrix
    {
        private readonly int[][] cache;
        public NumMatrix(int[][] matrix)
        {
            cache = new int[matrix.Length+1][];
            for(int i = 0; i < matrix.Length + 1; i++)
                cache[i] = new int[matrix[0].Length + 1];

            for (int i=1;i<cache.Length;i++)
                for(int j=1;j<cache[i].Length;j++)
                    cache[i][j] = matrix[i-1][j-1]+ cache[i-1][j]+cache[i][j-1]-cache[i-1][j-1];
        }

        public int SumRegion(int row1, int col1, int row2, int col2)
        {
            return cache[row2 + 1][col2 + 1] - cache[row2 + 1][col1] - cache[row1][col2 + 1] + cache[row1][col1];
        }
    }
}
