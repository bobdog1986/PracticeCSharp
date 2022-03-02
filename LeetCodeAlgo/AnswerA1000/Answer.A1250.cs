using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {

        ///1254. Number of Closed Islands, #DFS
        ///Given a 2D grid consists of 0s (land) and 1s (water).
        ///An island is a maximal 4-directionally connected group of 0s and
        ///a closed island is an island totally (all left, top, right, bottom) surrounded by 1s.
        ///Return the number of closed islands.
        public int ClosedIsland(int[][] grid)
        {
            int ans = 0;
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            bool[,] visit = new bool[rowLen, colLen];
            int[][] dxy = new int[4][] { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 } };
            List<int[]> points = new List<int[]>();
            for (int i = 1; i < rowLen - 1; i++)
            {
                for (int j = 1; j < colLen - 1; j++)
                {
                    if (visit[i, j]) continue;
                    if (grid[i][j] == 1) continue;

                    bool isClose = true;
                    points = new List<int[]>();
                    points.Add(new int[] { i, j });
                    while (points.Count > 0)
                    {
                        List<int[]> nexts = new List<int[]>();
                        foreach (var p in points)
                        {
                            var row = p[0];
                            var col = p[1];
                            if (row == 0 || row == rowLen - 1 || col == 0 || col == colLen - 1)
                            {
                                isClose = false;
                            }
                            if (visit[row, col]) continue;
                            visit[row, col] = true;
                            if (grid[i][j] == 0)
                            {
                                foreach (var d in dxy)
                                {
                                    var r = row + d[0];
                                    var c = col + d[1];
                                    if (r < 0 || r >= rowLen || c < 0 || c >= colLen)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (grid[r][c] == 0 && !visit[r, c])
                                            nexts.Add(new int[] { r, c });
                                    }
                                }
                            }
                        }
                        points = nexts;
                    }
                    if (isClose)
                        ans++;
                }
            }
            return ans;
        }
        ///1288. Remove Covered Intervals
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

            //int ans = 0;
            //int seed = 1<<(30-1);
            //int count = 0;
            //while(head != null)
            //{
            //    if (head.val == 1)
            //    {
            //        ans += seed;
            //    }
            //    seed >>= 1;
            //    count++;
            //    head = head.next;
            //}
            //return ans>>=30-count;
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

    }
}
