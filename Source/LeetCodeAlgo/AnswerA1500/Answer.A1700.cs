using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1701. Average Waiting Time
        ///Only 1 chef, customers[i] = [arrivali, timei], Return the average waiting time of all customers.
        public double AverageWaitingTime(int[][] customers)
        {
            double total = 0;
            int n = customers.Length;
            double start = 0;
            for(int i = 0; i < n; ++i)
            {
                start = Math.Max(start, customers[i][0]);
                double end = start + customers[i][1];
                total += end - customers[i][0];
                start = end;
            }
            return total / n;
        }
        /// 1706. Where Will the Ball Fall
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
        ///1710. Maximum Units on a Truck
        public int MaximumUnits(int[][] boxTypes, int truckSize)
        {
            int res = 0;
            boxTypes = boxTypes.OrderBy(x => -x[1]).ToArray();
            foreach(var i in boxTypes)
            {
                int count = Math.Min(truckSize, i[0]);
                res += count * i[1];
                truckSize -= count;
                if (truckSize <= 0) break;
            }
            return res;
        }
        /// 1721. Swapping Nodes in a Linked List
        ///You are given the head of a linked list, and an integer k.
        ///Return the head of the linked list after swapping the values of the kth node
        ///from the beginning and the kth node from the end(the list is 1-indexed).
        public ListNode SwapNodes(ListNode head, int k)
        {
            List<ListNode> list = new List<ListNode>();
            var node = head;
            while(node != null)
            {
                list.Add(node);
                node = node.next;
            }
            if (k > list.Count / 2)
                k = list.Count + 1 - k;
            if (list.Count % 2 == 1 && k == list.Count / 2 + 1)
                return head;

            var temp = list[k - 1];
            list[k - 1] = list[list.Count - k];
            list[list.Count - k] = temp;
            if (k == 1 || k == list.Count)
            {
                list[0].next = list[1];

                list[list.Count - 2].next = list[list.Count - 1];
                list[list.Count - 1].next = null;
            }
            else
            {
                list[k - 2].next = list[k - 1];
                list[k - 1].next = list[k];

                list[list.Count - k -1].next = list[list.Count - k];
                list[list.Count - k].next = list[list.Count - k + 1];
            }
            return list[0];
        }

        /// 1742. Maximum Number of Balls in a Box
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

        ///1745. Palindrome Partitioning IV, #DP
        ///return true if split the string s into three non-empty palindromic substrings.

        public bool CheckPartitioning(string s)
        {
            int n = s.Length;
            bool[,] dp = new bool[n,n];
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i >= j)
                        dp[i,j] = true;
                    else if (s[i] == s[j])
                        dp[i,j] = dp[i + 1,j - 1];
                }
            }
            for (int i = 1; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (dp[0,i - 1] && dp[i,j - 1] && dp[j,n - 1])
                        return true;
            return false;
        }
        public bool CheckPartitioning_MyOn3(string s)
        {
            bool ans = false;
            for (int i = 0; i < s.Length - 2; i++)
            {
                if (CheckPartitioning(s, 0, i))
                {
                    for (int j = i + 1; j < s.Length - 1; j++)
                    {
                        if (CheckPartitioning(s, i + 1, j) && CheckPartitioning(s, j + 1, s.Length - 1))
                        {
                            return true;
                        }
                    }
                }
            }
            return ans;
        }

        public bool CheckPartitioning(string s, int start,int end)
        {
            while (start < end)
            {
                if (s[start++] != s[end--]) return false;
            }
            return true;
        }

        ///1748. Sum of Unique Elements
        public int SumOfUnique(int[] nums)
        {
            var dict = new Dictionary<int, int>();
            int sum = 0;
            foreach(var n in nums)
            {
                if (dict.ContainsKey(n))
                {
                    if (dict[n] == 1) sum -= n;
                    dict[n]++;
                }
                else
                {
                    sum += n;
                    dict.Add(n, 1);
                }
            }
            return sum;
        }
    }
}
