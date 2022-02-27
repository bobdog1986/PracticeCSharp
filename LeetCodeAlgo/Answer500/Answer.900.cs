using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///918. Maximum Sum Circular Subarray
        ///Kadane algorithm, find max-of-positive and min-of-negtive, return max-of-positive or Sum()- min-of-neg
        public int MaxSubarraySumCircular(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];

            int sumOfPositive = nums[0];
            int max = nums[0];
            int allSum = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                sumOfPositive = Math.Max(sumOfPositive + nums[i], nums[i]);
                max = Math.Max(sumOfPositive, max);
                allSum += nums[i];
            }

            if (max < 0)
            {
                return max;
            }

            int sumOfNegative = nums[0];
            int min = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                sumOfNegative = Math.Min(sumOfNegative + nums[i], nums[i]);
                min = Math.Min(sumOfNegative, min);
            }

            return Math.Max(max, allSum - min);
        }


        /// 931. Minimum Falling Path Sum
        /// Given an n x n array of integers matrix, return the minimum sum of any falling path through matrix.
        public int MinFallingPathSum(int[][] matrix)
        {
            var len = matrix.Length;
            if (len == 1)
                return matrix[0][0];

            int[] dp = new int[len];

            for (int i = 0; i < len; i++)
                dp[i] = matrix[0][i];

            for (int i = 1; i < len; i++)
            {
                int[] dp2 = new int[len];
                for (int k = 0; k < len; k++)
                    dp2[k] = dp[k];

                for (int j = 0; j < len; j++)
                {
                    int a = dp2[j] + matrix[i][j];

                    if (j > 0)
                        a = Math.Min(a, dp2[j - 1] + matrix[i][j]);

                    if (j < len - 1)
                        a = Math.Min(a, dp2[j + 1] + matrix[i][j]);

                    dp[j] = a;
                }
            }

            return dp.Min();
        }

        ///934. Shortest Bridge, #DFS
        ///You are given an n x n binary matrix grid where 1 represents land and 0 represents water.
        ///An island is a 4-directionally connected group of 1's not connected to any other 1's. There are exactly two islands in grid.
        ///You may change 0's to 1's to connect the two islands to form one island. Return the smallest number of 0's to connect 2 islands
        public int ShortestBridge(int[][] grid)
        {
            List<int[]> list1 = new List<int[]>();
            List<int[]> list2 = new List<int[]>();
            int[][] dxy = new int[4][] { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 0) continue;
                    List<int[]> curr = new List<int[]>();
                    List<int[]> visits = new List<int[]>() { new int[] {i,j } };
                    grid[i][j] = 0;
                    while (visits.Count > 0)
                    {
                        curr.AddRange(visits);
                        List<int[]> nexts = new List<int[]>();
                        foreach(var v in visits)
                        {
                            foreach (var d in dxy)
                            {
                                int r = v[0] + d[0];
                                int c = v[1] + d[1];
                                if(r>=0&&r<grid.Length && c>=0&&c<grid[i].Length && grid[r][c] == 1)
                                {
                                    grid[r][c] = 0;
                                    nexts.Add(new int[] { r, c });
                                }
                            }
                        }
                        visits = nexts;
                    }
                    if (list1.Count == 0) { list1 = curr; }
                    else { list2 = curr; }
                }
                if (list2.Count > 0) break;
            }
            int min = int.MaxValue;
            foreach (var i in list1)
            {
                foreach (var j in list2)
                {
                    int len=int.MaxValue;
                    if (i[0] == j[0]) len = Math.Abs(i[1] - j[1])-1;
                    else if(i[1] == j[1]) len = Math.Abs(i[0] - j[0])-1;
                    else len = Math.Abs(i[1] - j[1]) - 1 + Math.Abs(i[0] - j[0]);
                    min = Math.Min(min, len);
                    if (min == 1) return min;
                }
            }
            return min;
        }

        /// 941. Valid Mountain Array
        ///len>=3, arr[i]> all [0,i-1],and [i+1,len-1]
        public bool ValidMountainArray(int[] arr)
        {
            if (arr.Length < 3)
                return false;

            bool isClimbing = true;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == arr[i - 1])
                    return false;

                if (isClimbing)
                {
                    if (arr[i] < arr[i - 1])
                    {
                        if (i == 1)
                            return false;

                        isClimbing = false;
                    }
                }
                else
                {
                    if (arr[i] > arr[i - 1])
                        return false;
                }
            }
            return !isClimbing;
        }
        /// 977. Squares of a Sorted Array

        public int[] SortedSquares(int[] nums)
        {
            var list1 = new List<int>();
            var list2 = new List<int>();

            var list3 = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    list3.Add(0);
                }
                else
                {
                    if (nums[i] > 0)
                    {
                        list2.Add(nums[i]);
                    }
                    else
                    {
                        list1.Insert(0, nums[i]);
                    }
                }
            }

            int j = 0;
            int k = 0;
            while (j < list1.Count || k < list2.Count)
            {
                if (j >= list1.Count)
                {
                    list3.Add(list2[k] * list2[k]);
                    k++;
                }
                else if (k >= list2.Count)
                {
                    list3.Add(list1[j] * list1[j]);
                    j++;
                }
                else
                {
                    if (list1[j] + list2[k] >= 0)
                    {
                        list3.Add(list1[j] * list1[j]);
                        j++;
                        //list3.Add(list2[k] * list2[k]);
                    }
                    else
                    {
                        list3.Add(list2[k] * list2[k]);
                        k++;
                        //list3.Add(list1[j] * list1[j]);
                    }
                }
            }

            return list3.ToArray();
        }

        ///986. Interval List Intersections
        ///The intersection of two closed intervals is a set of real numbers that are either empty or represented as a closed interval.
        ///For example, the intersection of [1, 3] and [2, 4] is [2, 3].
        public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
        {
            List<int[]> list = new List<int[]>();

            if (firstList.Length == 0 || secondList.Length == 0)
                return list.ToArray();

            foreach (var first in firstList)
            {
                if (first[1] < secondList[0][0])
                    continue;

                if (first[0] > secondList[secondList.Length - 1][1])
                    break;

                foreach (var second in secondList)
                {
                    if (first[1] < second[0] || first[0] > second[1])
                        continue;

                    list.Add(new int[] { Math.Max(first[0], second[0]), Math.Min(first[1], second[1]) });
                }
            }

            return list.ToArray();

        }
        /// 994. Rotting Oranges

        public int OrangesRotting(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;

            int totalCount = rowLen * colLen;

            int rottenCount = 0;
            int emptyCount = 0;

            bool isFirstLoop = true;

            Queue<int[]> queue = new Queue<int[]>();

            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == 0)
                    {
                        emptyCount++;
                    }
                    else if (grid[i][j] == 1)
                    {
                        queue.Enqueue(new int[] { i, j });
                    }
                    else
                    {
                        rottenCount++;
                    }
                }
            }

            if (rottenCount == totalCount)
                return 0;

            int lastRottenCount = rottenCount;

            int loop = 0;

            int lastCount = -1;
            Queue<int[]> q2;

            while (lastCount != queue.Count && queue.Count > 0)
            {
                lastCount = queue.Count;
                List<int[]> list = new List<int[]>();
                q2 = new Queue<int[]>();
                while (queue.Count > 0)
                {
                    var x = queue.Dequeue();
                    if ((x[0] > 0 && grid[x[0] - 1][x[1]] == 2)
                            || (x[0] < rowLen - 1 && grid[x[0] + 1][x[1]] == 2)
                            || (x[1] > 0 && grid[x[0]][x[1] - 1] == 2)
                            || (x[1] < colLen - 1 && grid[x[0]][x[1] + 1] == 2))
                    {
                        //grid[x[0]][x[1]] = 2;
                        list.Add(x);
                    }
                    else
                    {
                        q2.Enqueue(x);
                    }
                }

                if (list.Count == 0)
                {
                    return -1;
                }
                else
                {
                    queue = q2;
                    foreach (var i in list)
                    {
                        grid[i[0]][i[1]] = 2;
                    }
                }

                loop++;
            }

            return queue.Count == 0 ? loop : -1;
        }
    }
}