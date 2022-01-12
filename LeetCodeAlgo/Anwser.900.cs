using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //918. Maximum Sum Circular Subarray


        //918 failed, time exceed
        //public int MaxSubarraySumCircular(int[] nums)
        //{
        //    if(nums.Length == 1)
        //        return nums[0];

        //    if (nums.Length == 2)
        //        return nums[0] > 0 && nums[1] > 0 ? nums[0] + nums[1] : Math.Max(nums[0], nums[1]);

        //    int[] arr=new int[nums.Length*2-1];
        //    for(int i=0; i<arr.Length;i++)
        //        arr[i]=nums[i%nums.Length];

        //    int[] dp=new int[nums.Length];
        //    for(int i = 0; i < dp.Length; i++)
        //    {
        //        dp[i] = GetMaxSumOfArray(arr, i, dp.Length);
        //    }

        //    return dp.Max();
        //}

        //public int GetMaxSumOfArray(int[] nums, int start, int length)
        //{
        //    int max = nums[start];

        //    for(int i=start; i < length + start;i++)
        //    {
        //        if (nums[i]<=0)
        //        {
        //            max=Math.Max(max,nums[1]);
        //        }
        //        else
        //        {
        //            int sum=nums[i];
        //            for(int j=i+1; j<length+start; j++)
        //            {
        //                if (sum <= 0)
        //                    break;
        //                sum+=nums[j];

        //                max = Math.Max(max, sum);
        //            }
        //        }
        //    }

        //    return max;
        //}

        //977. Squares of a Sorted Array

        public int[] SortedSquares(int[] nums)
        {

            var list1=new List<int>();
            var list2=new List<int>();

            var list3 = new List<int>();


            for (int i = 0; i < nums.Length; i++)
            {
                if(nums[i] == 0)
                {
                    list3.Add(0);
                }
                else
                {
                    if (nums[i]>0)
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

        //994. Rotting Oranges

        public int OrangesRotting(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen=grid[0].Length;


            int totalCount = rowLen * colLen;

            int rottenCount = 0;
            int emptyCount = 0;

            bool isFirstLoop=true;

            Queue<int[]> queue = new Queue<int[]>();

            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == 0)
                    {
                        emptyCount++;
                    }
                    else if(grid[i][j]== 1)
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

            int lastCount =-1;
            Queue<int[]> q2;

            while (lastCount != queue.Count && queue.Count>0)
            {
                lastCount = queue.Count;
                List<int[]> list = new List<int[]>();
                q2 = new Queue<int[]>();
                while (queue.Count > 0)
                {
                    var x = queue.Dequeue();
                    if ((x[0] > 0 && grid[x[0] - 1][x[1]] == 2)
                            ||(x[0] < rowLen - 1 && grid[x[0] + 1][x[1]] == 2)
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
                    foreach(var i in list)
                    {
                        grid[i[0]][i[1]] = 2;
                    }
                }

                loop++;

            }


            return queue.Count == 0?loop:-1;
        }
    }
}
