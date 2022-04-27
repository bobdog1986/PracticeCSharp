using System;
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

        ///1202. Smallest String With Swaps, #Disjoint Set, #Union Find
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