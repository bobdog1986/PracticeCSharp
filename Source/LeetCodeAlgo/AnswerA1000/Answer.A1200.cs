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

        ///1201. Ugly Number III, #Binary Search
        //An ugly number is a positive integer that is divisible by a, b, or c.
        //Given four integers n, a, b, and c, return the nth ugly number.
        //1 <= n, a, b, c <= 10^9, 1 <= a* b * c <= 10^18
        ///It is guaranteed that the result will be in range[1, 2 * 109].
        public int NthUglyNumber(int n, int a, int b, int c)
        {
            int start = 0, end = int.MaxValue;
            long ab = getLCM_Long(a, b), bc = getLCM_Long(b, c), ca = getLCM_Long(c, a), abc = getLCM_Long(a, bc);
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

        ///1208. Get Equal Substrings Within Budget, #Sliding Window
        public int EqualSubstring(string s, string t, int maxCost)
        {
            int n = s.Length;
            int left = 0;
            int res = 0;
            for(int i = 0; i < n; i++)
            {
                maxCost -= Math.Abs( s[i] - t[i]);
                while(maxCost < 0 && left <= i)
                {
                    maxCost += Math.Abs(s[left] - t[left]);
                    left++;
                }
                res = Math.Max(res, i - left + 1);
            }
            return res;
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

        ///1222. Queens That Can Attack the King
        public IList<IList<int>> QueensAttacktheKing(int[][] queens, int[] king)
        {
            var res=new List<IList<int>>();

            var rights = queens.Where(q => q[0] == king[0] && q[1] > king[1]).OrderBy(q => q[1] - king[1]);
            if (rights.Any()) res.Add(rights.First().ToList());

            var lefts = queens.Where(q => q[0] == king[0] && q[1] < king[1]).OrderBy(q => king[1] - q[1] );
            if (lefts.Any()) res.Add(lefts.First().ToList());

            var downs = queens.Where(q => q[1] == king[1] && q[0] > king[0]).OrderBy(q => q[0] - king[0]);
            if (downs.Any()) res.Add(downs.First().ToList());

            var ups = queens.Where(q => q[1] == king[1] && q[0] < king[0]).OrderBy(q => king[0] - q[0] );
            if (ups.Any()) res.Add(ups.First().ToList());

            var rightDowns = queens.Where(q => q[0] > king[0] && q[0]- king[0] == q[1] - king[1])
                                    .OrderBy(q => q[0] - king[0]);
            if (rightDowns.Any()) res.Add(rightDowns.First().ToList());

            var rightUps = queens.Where(q => q[0] > king[0] && q[0] - king[0] == king[1] -q[1])
                                    .OrderBy(q => q[0] - king[0]);
            if (rightUps.Any()) res.Add(rightUps.First().ToList());

            var leftDowns = queens.Where(q => q[0] < king[0] && king[0] - q[0]  == q[1] - king[1])
                                    .OrderBy(q => king[0] -q[0] );
            if (leftDowns.Any()) res.Add(leftDowns.First().ToList());

            var leftUps = queens.Where(q => q[0] <king[0] && king[0] - q[0] == king[1]- q[1])
                                    .OrderBy(q => king[0] - q[0]);
            if (leftUps.Any()) res.Add(leftUps.First().ToList());

            return res;
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

        private bool CheckStraightLine_onLine(int[] p1, int[] p2, int[] p3)
        {
            int x = p1[0], y = p1[1], x1 = p2[0], y1 = p2[1], x2 = p3[0], y2 = p3[1];
            return ((y - y1) * (x2 - x1) == (y2 - y1) * (x - x1));
        }

        ///1233. Remove Sub-Folders from the Filesystem, #Trie
        public IList<string> RemoveSubfolders(string[] folder)
        {
            var root = new TrieItem();
            foreach(var word in folder)
            {
                var strs = word.Split('/').Where(x => x.Length > 0).ToList();
                var curr = root;
                foreach(var s in strs)
                {
                    if (!string.IsNullOrEmpty(curr.word)) break;
                    if (!curr.stringDict.ContainsKey(s)) curr.stringDict.Add(s, new TrieItem());
                    curr = curr.stringDict[s];
                }
                if (!string.IsNullOrEmpty(curr.word)) continue;
                curr.word = word;
            }
            List<string> res = new List<string>();
            RemoveSubfolders_Search(root, res);
            return res;
        }

        private void RemoveSubfolders_Search(TrieItem root, List<string> res)
        {
            if (root == null) return;
            if (!string.IsNullOrEmpty(root.word))
            {
                res.Add(root.word);
            }
            else
            {
                foreach(var pair in root.stringDict)
                    RemoveSubfolders_Search(pair.Value, res);
            }
        }

        public IList<string> RemoveSubfolders_Sort(string[] folder)
        {
            Array.Sort(folder);
            string prev = folder[0];
            var res = new List<string>() { prev };
            for (int i = 1; i < folder.Length; i++)
            {
                // Check if the string StartsWith or not. If not, add to the result list
                if (! folder[i].StartsWith(prev + "/"))
                {
                    res.Add(folder[i]);
                    prev = folder[i];
                }
            }
            return res;
        }

        ///1247. Minimum Swaps to Make Strings Equal
        //s1 and s2 of equal length consisting of letters "x" and "y" only.
        //You can swap any two characters that belong to different strings, which means: swap s1[i] and s2[j].
        //Return the minimum number of swaps required to make s1 and s2 equal, or return -1 if impossible.
        public int MinimumSwap(string s1, string s2)
        {
            int res = 0;
            int n = s1.Length;
            Dictionary<string, int> dict1 = new Dictionary<string, int>();
            Dictionary<char, int> map = new Dictionary<char, int>();
            for(int i = 0; i < n; i++)
            {
                if (s1[i] != s2[i])
                {
                    var k = $"{s1[i]}{s2[i]}";
                    if (dict1.ContainsKey(k)) dict1[k]++;
                    else dict1.Add(k, 1);

                    //all chars on these index
                    if (map.ContainsKey(s1[i])) map[s1[i]]++;
                    else map.Add(s1[i], 1);
                    if (map.ContainsKey(s2[i])) map[s2[i]]++;
                    else map.Add(s2[i], 1);
                }
            }
            if (map.Keys.Any(x =>map[x]  % 2 != 0)) return -1;//case3, invalid, eg. s1 = "xx", s2 = "xy"
            res += dict1.Keys.Select(x => dict1[x] / 2).Sum();//case1, eg. s1 = "xx", s2 = "yy"
            res += dict1.Keys.Select(x => dict1[x] % 2).Sum();//case2, eg. s1 = "xy", s2 = "yx"
            return res;
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