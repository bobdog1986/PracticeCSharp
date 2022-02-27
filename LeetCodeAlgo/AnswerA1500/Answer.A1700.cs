using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1706. Where Will the Ball Fall
        ///You have a 2-D grid of size m x n representing a box, and you have n balls.
        ///The box is open on the top and bottom sides.
        ///A board = 1, that redirects the ball to the right spans the top-left to the bottom-right
        ///A board =-1, that redirects the ball to the left spans the top-right to the bottom-left
        ///Return an array answer of size n where answer[i] is the column that the ball falls out of
        ///at the bottom after dropping the ball from the ith column at the top, or -1 if the ball gets stuck in the box.
        public int[] FindBall(int[][] grid)
        {
            var rowLen = grid.Length;
            var colLen = grid[0].Length;
            var ans=new int[colLen];
            for(int i = 0; i < colLen; i++)
            {
                int col = i;
                int row = 0;
                while (row < rowLen)
                {
                    int adjacentCol = col + grid[row][col];
                    if (adjacentCol < 0 || adjacentCol >= colLen)
                    {
                        col = -1;
                        break;
                    }
                    else if (adjacentCol+ grid[row][adjacentCol]== col)
                    {
                        col = -1;
                        break;
                    }
                    else
                    {
                        col=adjacentCol;
                        row++;
                    }
                }
                ans[i] = col;
            }
            return ans;
        }
        ///1742. Maximum Number of Balls in a Box
        ///For example, the ball number 321 will be put in the box number 3 + 2 + 1 = 6 and
        ///the ball number 10 will be put in the box number 1 + 0 = 1.
        ///Given two integers lowLimit and highLimit, return the number of balls in the box with the most balls.
        public int CountBalls(int lowLimit, int highLimit)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int ans = 0;
            for(int i = lowLimit; i <= highLimit; i++)
            {
                var boxIndex = CountBalls_GetBoxIndex(i);
                if (dict.ContainsKey(boxIndex))
                    dict[boxIndex]++;
                else
                    dict.Add(boxIndex, 1);
                ans = Math.Max(ans, dict[boxIndex]);
            }
            return ans;
        }
        public int CountBalls_GetBoxIndex(int ball)
        {
            int ans = 0;
            while (ball > 0)
            {
                ans += ball % 10;
                ball /= 10;
            }
            return ans;
        }
    }
}
