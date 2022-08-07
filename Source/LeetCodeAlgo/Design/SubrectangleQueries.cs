using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    /// 1476. Subrectangle Queries
    /// Implement the class SubrectangleQueries which receives a rows x cols rectangle
    /// as a matrix of integers in the constructor and supports two methods:
    public class SubrectangleQueries
    {
        private readonly int[][] _matrix;
        public SubrectangleQueries(int[][] rectangle)
        {
            _matrix = rectangle;
        }

        public void UpdateSubrectangle(int row1, int col1, int row2, int col2, int newValue)
        {
            for (int i = row1; i <= row2; i++)
                for (int j = col1; j <= col2; j++)
                    _matrix[i][j] = newValue;
        }

        public int GetValue(int row, int col)
        {
            return _matrix[row][col];
        }
    }
}
