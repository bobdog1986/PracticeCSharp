using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///918. Maximum Sum Circular Subarray
        public int MaxSubarraySumCircular(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];

            int sum = nums[0];
            int max = nums[0];
            int allSum = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                sum = Math.Max(sum + nums[i], nums[i]);
                max = Math.Max(sum, max);
                allSum += nums[i];
            }

            if (max < 0)
            {
                return max;
            }

            int sum_n = nums[0];
            int max_n = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                sum_n = Math.Min(sum_n + nums[i], nums[i]);
                max_n = Math.Min(sum_n, max_n);
            }

            return Math.Max(max, allSum - max_n);
        }


        /// 931. Minimum Falling Path Sum
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

        //977. Squares of a Sorted Array

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

            foreach(var first in firstList)
            {
                if (first[1] < secondList[0][0])
                    continue;

                if (first[0] > secondList[secondList.Length - 1][1])
                    break;

                foreach(var second in secondList)
                {
                    if (first[1] < second[0] || first[0] > second[1])
                        continue;

                    list.Add(new int[] { Math.Max(first[0],second[0]),Math.Min(first[1],second[1])});
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