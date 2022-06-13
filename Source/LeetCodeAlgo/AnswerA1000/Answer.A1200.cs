﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1200. Minimum Absolute Difference
        ///find all pairs of elements with the minimum absolute difference of any two elements.
        public IList<IList<int>> MinimumAbsDifference(int[] arr)
        {
            Array.Sort(arr);
            var res = new List<IList<int>>();
            int min = int.MaxValue;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                var diff = arr[i + 1] - arr[i];
                if (diff <= min)
                {
                    if (diff < min) res.Clear();
                    min = diff;
                    res.Add(new List<int>() { arr[i], arr[i + 1] });
                }
            }
            return res;
        }

        ///1201. Ugly Number III, #Binary Search
        //An ugly number is a positive integer that is divisible by a, b, or c.
        //Given four integers n, a, b, and c, return the nth ugly number.
        //1 <= n, a, b, c <= 10^9, 1 <= a* b * c <= 10^18
        ///It is guaranteed that the result will be in range[1, 2 * 109].
        public int NthUglyNumber(int n, int a, int b, int c)
        {
            int start = 0, end = int.MaxValue;
            long ab = LCM(a, b), bc = LCM(b, c), ca = LCM(c, a), abc = LCM(a, bc);
            while (start < end)
            {
                int mid = start + (end - start) / 2;
                int count = (int)(mid / a + mid / b + mid / c - mid / ab - mid / bc - mid / ca + mid / abc);
                if (count < n)
                    start = mid + 1;
                else
                    end = mid;
            }

            return start;
        }

        private long GCD(long x, long y)
        {
            return y == 0 ? x : GCD(y, x % y);
        }

        private long LCM(long x, long y)
        {
            return (x * y) / GCD(x, y);
        }
        /// 1202. Smallest String With Swaps, #Disjoint Set, #Union Find
        ///an array of pairs of indices where pairs[i] = [a, b] indicates 2 indices(0-indexed) of the string.
        ///You can swap the characters at any pair of indices in the given pairs any number of times.
        ///Return the lexicographically smallest string that s can be changed to after using the swaps.
        public string SmallestStringWithSwaps(string s, IList<IList<int>> pairs)
        {
            int n = s.Length;
            int[] parents=new int[n];
            //default root of each index is itself
            for(int i = 0; i < n; i++)
                parents[i] = i;

            //group index by the min index of connected-pairs
            foreach (var pair in pairs)
                SmallestStringWithSwaps_union(pair[0], pair[1], parents);

            return SmallestStringWithSwaps(s, pairs, parents);
        }

        private string SmallestStringWithSwaps(string s, IList<IList<int>> pairs, int[] parents)
        {
            var map = new Dictionary<int, PriorityQueue<char,char>>();
            for (int i = 0; i < s.Length; i++)
            {
                int root = SmallestStringWithSwaps_find(i,parents);
                //if no root index, create one using pq as value to auto sort s[i]
                if (!map.ContainsKey(root))
                    map.Add(root, new PriorityQueue<char, char>());

                map[root].Enqueue(s[i], s[i]);
            }

            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                //find the root , then dequeue a char from pq
                var root = SmallestStringWithSwaps_find(i, parents);
                sb.Append(map[root].Dequeue());
            }
            return sb.ToString();
        }

        private void SmallestStringWithSwaps_union(int a, int b, int[] parents)
        {
            //find root index of a and b
            int aParent = SmallestStringWithSwaps_find(a, parents);
            int bParent = SmallestStringWithSwaps_find(b, parents);
            //if a<b, set b's root = a
            if (aParent < bParent)
            {
                parents[bParent] = aParent;
            }
            else
            {
                parents[aParent] = bParent;
            }
        }

        private int SmallestStringWithSwaps_find(int index, int[] parents)
        {
            //if equal, this index is the root index
            while (parents[index] != index)
            {
                parents[index] = parents[parents[index]];
                index = parents[index];
            }
            return index;
        }

        ///1209. Remove All Adjacent Duplicates in String II
        public string RemoveDuplicates(string s, int k)
        {
            List<char> list = new List<char>();
            int count = 0;
            char prev = ' ';
            for(int i = 0; i < s.Length; i++)
            {
                if (prev == s[i]) count++;
                else count = 1;
                prev = s[i];
                list.Add(s[i]);
                if(count == k)
                {
                    while (count > 0)
                    {
                        list.RemoveAt(list.Count - 1);
                        count--;
                    }

                    if (list.Count == 0)
                    {
                        prev = ' ';
                    }
                    else
                    {
                        prev = list.Last();
                        for(int j = list.Count-1; j >=0; j--)
                        {
                            if (list[j] == prev)
                                count++;
                            else break;
                        }
                    }
                }
            }

            return new string(list.ToArray());
        }
        ///1218. Longest Arithmetic Subsequence of Given Difference
        public int LongestSubsequence(int[] arr, int difference)
        {
            var dict=new Dictionary<int, int>();
            foreach(var n in arr)
            {
                if (dict.ContainsKey(n - difference))
                {
                    if (dict.ContainsKey(n)) dict[n] = Math.Max(dict[n], dict[n - difference] + 1);
                    else dict.Add(n, dict[n - difference] + 1);
                }
                else
                {
                    if (dict.ContainsKey(n)) dict[n] = 1;
                    else dict.Add(n, 1);
                }
            }
            return dict.Values.Max();
        }
        /// 1219. Path with Maximum Gold, #DFS
        public int GetMaximumGold(int[][] grid)
        {
            int max = 0;
            int[][] dxy = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 }};
            for(int i = 0; i < grid.Length; i++)
            {
                for(int j = 0; j < grid[0].Length; j++)
                {
                    GetMaximumGold_DFS(grid, i, j, 0, dxy, ref max);
                }
            }
            return max;
        }

        private void GetMaximumGold_DFS(int[][] grid, int row , int col,int curr,int[][] dxy, ref int max)
        {
            if(row>=0 && row<grid.Length && col>=0 && col < grid[0].Length && grid[row][col]>0)
            {
                curr += grid[row][col];
                max = Math.Max(curr, max);
                int temp = grid[row][col];
                grid[row][col] = 0;
                foreach(var d in dxy)
                {
                    GetMaximumGold_DFS(grid, row + d[0], col + d[1], curr, dxy, ref max);
                }
                grid[row][col] = temp;
            }
        }
        /// 1227. Airplane Seat Assignment Probability
        public double NthPersonGetsNthSeat(int n)
        {
            return n == 1 ? 1.0 : 0.5;
        }
        /// 1232. Check If It Is a Straight Line
        ///coordinates[i] = [x, y], where [x, y] represents the coordinate of a point.
        ///Check if these points make a straight line in the XY plane.
        public bool CheckStraightLine(int[][] coordinates)
        {
            // base case:- there are only two points, return true
            // otherwise, check each point lies on line using above equation.
            for (int i = 2; i < coordinates.Length; i++)
            {
                if (!CheckStraightLine_onLine(coordinates[i], coordinates[0], coordinates[1]))
                    return false;
            }
            return true;
        }

        public bool CheckStraightLine_onLine(int[] p1, int[] p2, int[] p3)
        {
            int x = p1[0], y = p1[1], x1 = p2[0], y1 = p2[1], x2 = p3[0], y2 = p3[1];
            return ((y - y1) * (x2 - x1) == (y2 - y1) * (x - x1));
        }

        /// 1249. Minimum Remove to Make Valid Parentheses
        ///Given a string s of '(' , ')' and lowercase English characters.
        ///remove the minimum number of parentheses  '(' or ')', in any positions, to make it valid string
        ///valid string: 1.empty, only chars; 2, (A), 3. (A .... B)
        public string MinRemoveToMakeValid(string s)
        {
            Stack<int[]> stack = new Stack<int[]>();
            var arr = s.ToCharArray();
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '(')
                {
                    stack.Push(new int[] { arr[i], i });
                }
                else if (arr[i] == ')')
                {
                    if (stack.Count == 0)
                    {
                        arr[i] = ' ';
                    }
                    else
                    {
                        var peek = stack.Peek();
                        if (peek[0] == '(')
                        {
                            stack.Pop();
                        }
                        else
                        {
                            stack.Push(new int[] { arr[i], i });
                        }
                    }
                }
            }

            while (stack.Count > 0)
            {
                var pop = stack.Pop();
                arr[pop[1]] = ' ';
            }

            return string.Join("", arr.Where(x => x != ' '));
        }
    }
}