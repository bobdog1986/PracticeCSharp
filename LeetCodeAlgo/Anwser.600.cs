using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //611
        public int TriangleNumber(int[] nums)
        {
            Array.Sort(nums);
            int count = 0;
            int i, j, k;
            for (i = 0; i < nums.Length-2; i++)
            {
                for (j = i + 1; j < nums.Length - 1; j++)
                {
                    for (k = j + 1; k < nums.Length; k++)
                    {
                        if (IsTriangle(nums[i], nums[j], nums[k]))
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return count;
        }

        public bool IsTriangle(int num1,int num2,int num3)
        {
            return num3 < num1 + num2;
        }

        //695. Max Area of Island

        public int MaxAreaOfIsland(int[][] grid)
        {
            int row = grid.Length;
            int col = grid[0].Length;

            List<List<int>> list = new List<List<int>>();
            for(int i = 0;i < row; i++)
            {
                List<int> list2 = new List<int>();
                for(int j = 0;j < col; j++)
                {
                    list2.Add(0);
                }
                list.Add(list2);
            }

            int max = 0;
            for(int i = 0; i < row; i++)
            {
                for(int j = 0; j < col; j++)
                {
                    if(grid[i][j] == 1 && list[i][j]==0)
                    {
                        Queue<int[]> queue = new Queue<int[]>();

                        list[i][j] = 1;
                        int count = 1;
                        queue.Enqueue(new int[] {i,j});

                        while(queue.Count > 0)
                        {
                            var point=queue.Dequeue();

                            if ((point[0] > 0) && grid[point[0] - 1][point[1]] == 1 && list[point[0] - 1][point[1]] == 0)
                            {
                                list[point[0] - 1][point[1]] = 1;
                                queue.Enqueue(new int[] { point[0] - 1, point[1] });
                                count++;
                            }

                            if ((point[0] < row - 1) && (grid[point[0] + 1][point[1]] == 1) && list[point[0] + 1][point[1]] == 0)
                            {
                                list[point[0] + 1][point[1]] = 1;
                                queue.Enqueue(new int[] { point[0] + 1, point[1] });
                                count++;

                            }

                            if ((point[1] > 0) && (grid[point[0]][point[1] - 1] == 1) && list[point[0]][point[1]-1] == 0)
                            {
                                list[point[0]][point[1] - 1] = 1;
                                queue.Enqueue(new int[] { point[0], point[1] - 1 });
                                count++;

                            }

                            if ((point[1] < col - 1) && (grid[point[0]][point[1] + 1] == 1) && list[point[0]][point[1]+1] == 0)
                            {
                                list[point[0]][point[1] + 1] = 1;
                                queue.Enqueue(new int[] { point[0], point[1] + 1 });
                                count++;

                            }

                        }

                        max = Math.Max(max, count);
                    }


                }
            }

            return max;
        }
        //697
        public int FindShortestSubArray(int[] nums)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dictionary.ContainsKey(nums[i]))
                {
                    dictionary[nums[i]]++;
                }
                else
                {
                    dictionary.Add(nums[i], 1);
                }
            }

            int frequency = dictionary.Values.Max();
            var candidate = dictionary.Where(o => o.Value == frequency).Select(o=>o.Key);
            int minLength = nums.Length;
            foreach(var i in candidate)
            {
                int length = GetLastIndex(nums, i)- GetFirstIndex(nums, i)+1;
                minLength = length < minLength ? length : minLength;
            }
            return minLength;
        }
        public int GetFirstIndex(int[] nums,int key)
        {
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == key) return i;
            }
            throw new ArgumentOutOfRangeException();
        }
        public int GetLastIndex(int[] nums, int key)
        {
            for (int i = nums.Length-1; i >=0; i--)
            {
                if (nums[i] == key) return i;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
