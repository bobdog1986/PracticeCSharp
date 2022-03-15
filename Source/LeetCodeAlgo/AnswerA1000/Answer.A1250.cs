using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {

        ///1254. Number of Closed Islands, #DFS, #Graph
        ///Given a 2D grid consists of 0s (land) and 1s (water).
        ///An island is a maximal 4-directionally connected group of 0s and
        ///Return the number of islands.
        public int ClosedIsland(int[][] grid)
        {
            int ans = 0;
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            bool[,] visit = new bool[rowLen, colLen];
            int[][] dxy = new int[4][] { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 } };
            for (int i = 1; i < rowLen - 1; i++)
            {
                for (int j = 1; j < colLen - 1; j++)
                {
                    if (grid[i][j] == 0 && !visit[i, j])
                    {
                        bool isClose = true;
                        visit[i, j] = true;
                        Queue<int[]> q = new Queue<int[]>();
                        q.Enqueue(new int[] { i, j });
                        while (q.Count > 0)
                        {
                            var p = q.Dequeue();
                            var row = p[0];
                            var col = p[1];
                            if (row == 0 || row == rowLen - 1 || col == 0 || col == colLen - 1)
                            {
                                isClose = false;
                            }
                            if (grid[i][j] == 0)
                            {
                                foreach (var d in dxy)
                                {
                                    var r = row + d[0];
                                    var c = col + d[1];
                                    if (r >= 0 && r < rowLen && c >= 0 && c < colLen && grid[r][c] == 0 && !visit[r, c])
                                    {
                                        visit[r, c] = true;
                                        q.Enqueue(new int[] { r, c });
                                    }
                                }
                            }
                        }
                        if (isClose)
                            ans++;
                    }
                }
            }
            return ans;
        }
        ///1260. Shift 2D Grid
        ///Return the 2D grid after applying shift operation k times.
        public IList<IList<int>> ShiftGrid(int[][] grid, int k)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            var res =new List<IList<int>>();
            foreach(var r in grid)
                res.Add(r.ToList());
            k %= rowLen * colLen;
            while(k-- > 0)
            {
                int temp = res[rowLen-1][colLen-1];
                for(int i = 0; i < rowLen; i++)
                {
                    res[i].Insert(0,temp);
                    temp = res[i].Last();
                    res[i].RemoveAt(res[i].Count-1);
                }
            }
            return res;
        }
        /// 1281. Subtract the Product and Sum of Digits of an Integer
        ///return the difference between the product of its digits and the sum of its digits.
        public int SubtractProductAndSum(int n)
        {
            List<int> list=new List<int>();
            while (n > 0)
            {
                list.Add(n % 10);
                n /= 10;
            }
            return list.Aggregate((x, y) => x * y) - list.Aggregate((x, y) => x + y);
        }

        /// 1288. Remove Covered Intervals
        ///Given an array intervals where intervals[i] = [li, ri] represent the interval [li, ri),
        ///remove all intervals that are covered by another interval in the list.
        ///The interval [a, b) is covered by the interval [c, d) if and only if c <= a and b <= d.
        ///Return the number of remaining intervals.
        ///0 <= li <= ri <= 10^5
        public int RemoveCoveredIntervals(int[][] intervals)
        {
            int ans = 0, left = -1, right = -1;
            var list = intervals.OrderBy(x => x[0]).ToList();
            foreach (var v in list)
            {
                if (v[0] == left)
                {
                    //update the right
                    right = Math.Max(right, v[1]);
                }
                else//v[0] > left
                {
                    if(v[1] > right)
                    {
                        ans++;
                        left = v[0];
                        right = v[1];
                    }
                    else
                    {
                        //merged
                    }
                }
                //if (v[0] > left && v[1] > right)
                //{
                //    left = v[0];
                //    ans++;
                //}
                //right = Math.Max(right, v[1]);
            }
            return ans;
        }
        /// 1290. Convert Binary Number in a Linked List to Integer
        ///The value of each node in the linked list is either 0 or 1. The linked list holds the binary representation of a number.
        ///Return the decimal value of the number in the linked list.
        ///Number of nodes will not exceed 30.
        public int GetDecimalValue(ListNode head)
        {
            int sum = 0;
            while (head != null)
            {
                sum <<=1;
                sum += head.val;
                head = head.next;
            }
            return sum;
        }


        /// 1291. Sequential Digits
        ///An integer has sequential digits if and only if each digit in the number is one more than the previous digit.
        ///eg. 123, 234, 3456,
        public IList<int> SequentialDigits(int low, int high)
        {
            var ans = new List<int>();
            int len1 = 1;
            int len2 = 1;
            int a = low;
            int b = high;

            while (a >= 10)
            {
                len1++;
                a /= 10;
            }

            while (b >= 10)
            {
                len2++;
                b /= 10;
            }

            for (int i = len1; i <= len2 && i <= 9; i++)
            {
                int start = 1;
                int end = 9;

                if (i == len1)
                    start = a;
                if (i == len2)
                    end = b;

                for (int j = start; j <= 9 - i + 1 && j <= end; j++)
                {
                    int val = 0;
                    int m = i;
                    int n = j;
                    while (m > 0)
                    {
                        val *= 10;
                        val += n;

                        m--;
                        n++;
                    }

                    if (val >= low && val <= high)
                        ans.Add(val);
                }
            }

            return ans;
        }

        ///1295. Find Numbers with Even Number of Digits
        ///Given an array nums of integers, return how many of them contain an even number of digits.
        public int FindNumbers(int[] nums)
        {
            int res = 0;
            foreach(var n in nums)
                if (FindNumbers_isEvenDigit(n)) res++;
            return res;
        }

        public bool FindNumbers_isEvenDigit(int n)
        {
            while (n > 0)
            {
                if (n >= 100)
                {
                    n /= 100;
                }
                else
                {
                    if (n < 10) return false;
                    else return true;
                }
            }
            return true;
        }
        ///1299. Replace Elements with Greatest Element on Right Side
        ///replace every element with the greatest element among the elements to its right, and replace the last element with -1.
        public int[] ReplaceElements(int[] arr)
        {
            int max = -1;
            for(int i = arr.Length - 1; i >= 0; i--)
            {
                var temp = arr[i];
                arr[i] = max;
                max = Math.Max(max, temp);
            }
            return arr;
        }
    }
}
