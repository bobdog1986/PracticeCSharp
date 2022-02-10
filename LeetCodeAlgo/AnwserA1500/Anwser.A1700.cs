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
        /// 1752. Check if Array Is Sorted and Rotated
        ///Given an array nums, return true if the array sorted in non-decreasing order, then rotated some
        ///[1,2,3,3,4],[2,3,4,1],[3,4,5,1,2]=>true, [2,1,3,4]=>false
        public bool Check_1752(int[] nums)
        {
            bool ans = true;
            bool isRotate = false;
            for(int i = 1; i < nums.Length; i++)
            {
                if (nums[i] < nums[i - 1])
                {
                    if (isRotate)
                        return false;
                    isRotate = true;
                }
            }

            if (isRotate)
            {
                if (nums.Last() > nums.FirstOrDefault())
                {
                    ans = false;
                }
            }
            else
            {
                if (nums.Last() < nums.FirstOrDefault())
                {
                    ans = false;
                }
            }

            return ans;
        }

    }
}
