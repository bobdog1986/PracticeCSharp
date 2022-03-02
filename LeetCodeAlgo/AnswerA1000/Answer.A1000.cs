using System;
using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1004. Max Consecutive Ones III, #Sliding Window
        //return the maximum number of consecutive 1's in the array if you can flip at most k 0's.
        public int LongestOnes(int[] nums, int k)
        {
            int max = 0;
            int left = 0;//left index, i is right index
            int zeroCount = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if(nums[i] == 0)
                {
                    zeroCount++;
                    while (zeroCount > k && left <= i)
                    {
                        if (nums[left] == 0) { zeroCount--; }
                        left++;
                    }
                }
                max = Math.Max(max, i - left + 1);
            }
            return max;
        }

        /// 1014. Best Sightseeing Pair
        ///find max = values[i] + values[j] + i - j, 1 <= values[i] <= 1000
        public int MaxScoreSightseeingPair(int[] values)
        {
            //bestPoint =Max( value[i] + i)
            int maxv = Math.Max(values[0], values[1] + 1);
            int ans = values[1] + values[0] - 1;
            for (int i = 2; i < values.Length; i++)
            {
                ans = Math.Max(ans, maxv + values[i] - i);
                maxv = Math.Max(maxv, values[i] + i);
            }
            return ans;
        }

        ///1091. Shortest Path in Binary Matrix
        ///Given an n x n binary matrix grid, return the length of the shortest clear path in the matrix.
        ///If there is no clear path, return -1.
        ///8 direction
        public int ShortestPathBinaryMatrix(int[][] grid)
        {
            int len = grid.Length;

            if (len == 1)
                return grid[0][0] == 0 ? 1 : -1;

            if (grid[0][0] == 1 || grid[len - 1][len - 1] == 1)
                return -1;

            int[][] visit = new int[len][];

            for (int i = 0; i < len; i++)
            {
                visit[i] = new int[len];
            }

            int[][] dxy = new int[8][] {
                new int[]{0,1}, new int[] {1, 0 },
                new int[]{ 0,-1}, new int[] { -1, 0 },
                new int[]{ 1,1 }, new int[]{ -1, -1 },
                new int[]{ -1, 1 },new int[] { 1, -1 } };

            List<int[]> list = new List<int[]>();

            int step = 0;

            list.Add(new int[] { 0, 0 });
            visit[0][0] = 1;
            step++;

            while (list.Count > 0)
            {
                List<int[]> sub = new List<int[]>();

                foreach (var cell in list)
                {
                    int r = cell[0];
                    int c = cell[1];

                    foreach (var d in dxy)
                    {
                        int r1 = r + d[0];
                        int c1 = c + d[1];

                        if (r1 >= 0 && r1 <= len - 1 && c1 >= 0 && c1 <= len - 1)
                        {
                            if (r1 == len - 1 && c1 == len - 1)
                                return step + 1;

                            if (visit[r1][c1] == 1)
                                continue;

                            visit[r1][c1] = 1;

                            if (grid[r1][c1] == 0)
                            {
                                sub.Add(new int[] { r1, c1, });
                            }

                        }


                    }
                }

                list = sub;


                step++;
            }





            return -1;
        }
    }
}
