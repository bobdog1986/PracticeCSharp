﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1901. Find a Peak Element II, #Binary Search
        //A peak element in a 2D grid is an element that is > all of its 4-directions neighbors.
        //matrix mat no two adjacent cells are equal, find any peak element and return the length 2 array[i, j].
        public int[] FindPeakGrid(int[][] mat)
        {
            int rowLen = mat.Length;
            int colLen = mat[0].Length;
            int left = 0;
            int right = colLen - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int max_row = 0;
                for (int i = 0; i < rowLen; ++i)
                {
                    if (mat[max_row][mid] < mat[i][mid])
                        max_row = i;
                }
                if ((mid == 0 || mat[max_row][mid] > mat[max_row][mid - 1]) &&
                    (mid == colLen - 1 || mat[max_row][mid] > mat[max_row][mid + 1]))
                    return new int[] { max_row, mid };
                else if (mid > 0 && mat[max_row][mid - 1] > mat[max_row][mid])
                    right = mid - 1;
                else
                    left = mid + 1;
            }
            return new int[] { -1, -1 };
        }

        /// 1903. Largest Odd Number in String
        public string LargestOddNumber(string num)
        {
            for(int i=num.Length-1; i>=0; i--)
            {
                if ((num[i] - '0') % 2 == 1)
                {
                    return num.Substring(0, i + 1);
                }
            }
            return string.Empty;
        }
        /// 1905. Count Sub Islands, #Graph, #DFS
        ///Return the number of islands in grid2 that are considered sub-islands.
        public int CountSubIslands_My(int[][] grid1, int[][] grid2)
        {
            int rowLen = grid2.Length;
            int colLen = grid2[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            int ans = 0;
            bool[,] visit = new bool[rowLen, colLen];
            for (int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                {
                    if (grid2[i][j] == 0 || visit[i, j]) continue;
                    List<int[]> list = new List<int[]> { };
                    Queue<int[]> q = new Queue<int[]>();
                    var node = new int[] { i, j };
                    int subIsland = 1;//if not a subIsland, this will be 0
                    q.Enqueue(node);
                    list.Add(node);
                    visit[i, j] = true;
                    while (q.Count > 0)
                    {
                        var p = q.Dequeue();
                        if (grid1[p[0]][p[1]] == 0) subIsland = 0;//not a subIsland
                        foreach (var d in dxy4)
                        {
                            var r = p[0] + d[0];
                            var c = p[1] + d[1];
                            if (r >= 0 && r < rowLen && c >= 0 && c < colLen && grid2[r][c] == 1 && !visit[r, c])
                            {
                                visit[r, c] = true;
                                var next = new int[] { r, c };
                                q.Enqueue(next);
                                list.Add(next);
                            }
                        }
                    }
                    ans += subIsland;
                }
            return ans;
        }

        public int CountSubIslands(int[][] grid1, int[][] grid2)
        {
            int m = grid2.Length, n = grid2[0].Length, ans = 0;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    if (grid2[i][j] == 1)
                        ans += CountSubIslands_dfs(grid1, grid2, i, j);
            return ans;
        }

        private int CountSubIslands_dfs(int[][] grid1, int[][] grid2, int i, int j)
        {
            int m = grid2.Length, n = grid2[0].Length, ans = 1;
            if (i < 0 || i == m || j < 0 || j == n || grid2[i][j] == 0) return 1;
            grid2[i][j] = 0;
            ans &= CountSubIslands_dfs(grid1, grid2, i - 1, j);
            ans &= CountSubIslands_dfs(grid1, grid2, i + 1, j);
            ans &= CountSubIslands_dfs(grid1, grid2, i, j - 1);
            ans &= CountSubIslands_dfs(grid1, grid2, i, j + 1);
            return ans & grid1[i][j];
        }

        ///1909. Remove One Element to Make the Array Strictly Increasing
        public bool CanBeIncreasing(int[] nums)
        {
            int prev = int.MinValue;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] <= prev)
                {
                    return CanBeIncreasing(nums, i) || CanBeIncreasing(nums, i - 1);
                }
                prev = nums[i];
            }
            return true;
        }

        private bool CanBeIncreasing(int[] nums, int skip)
        {
            int prev = int.MinValue;
            for(int i = 0; i < nums.Length; i++)
            {
                if (i == skip) continue;
                if(nums[i]<=prev) return false;
                prev = nums[i];
            }
            return true;
        }

        /// 1910. Remove All Occurrences of a Substring
        // Find the leftmost occurrence of the substring part and remove it from s.
        // Return s after removing all occurrences of part.
        public string RemoveOccurrences(string s, string part)
        {
            var index=s.IndexOf(part);
            if (index != -1) return RemoveOccurrences(s.Substring(0, index) + s.Substring(index + part.Length), part);
            else return s;
        }
        ///1913. Maximum Product Difference Between Two Pairs
        public int MaxProductDifference(int[] nums)
        {
            Array.Sort(nums);
            int n = nums.Length;
            return nums[n - 1] * nums[n - 2] - nums[0] * nums[1];
        }
        /// 1920. Build Array from Permutation
        ///ans[i] = nums[nums[i]], 1 <= nums.length <= 1000, O(1) space
        public int[] BuildArray(int[] nums)
        {
            int mask = (1 << 10) - 1;
            for (int i = 0; i < nums.Length; i++)
                nums[i] |= (nums[nums[i]] & mask) << 10;
            for (int i = 0; i < nums.Length; i++)
                nums[i] = nums[i] >> 10;
            return nums;
        }
        /// 1922. Count Good Numbers- not pass,support to 10^8, but input is 10^15
        ///even indices are even and the digits at odd indices are prime (2, 3, 5, or 7).
        public int CountGoodNumbers(long n)
        {
            long Modulo = 10_0000_0007;

            long even = n / 2;

            long odd = n - even;

            var c = (power1922(5, odd) % Modulo) * (power1922(4, even) % Modulo) % Modulo;
            return (int)c;
        }

        public long power1922(long seed, long pow, long Modulo = 1000000007)
        {
            if (pow == 0)
                return 1;

            if (pow == 1)
                return seed;

            var half = pow / 2;

            if (pow % 2 == 0)
            {
                return (power1922(seed, half) % Modulo) * (power1922(seed, half) % Modulo) % Modulo;
            }
            else
            {
                return (seed * (power1922(seed, half) % Modulo) * (power1922(seed, half) % Modulo)) % Modulo;
            }
        }

        ///1925. Count Square Sum Triples
        //a2 + b2 = c2. 1 <= a, b, c <= n.
        public int CountTriples(int n)
        {
            int res = 0;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    var sqrt = Math.Sqrt(i * i + j * j);
                    if (sqrt % 1 == 0 && sqrt <= n)
                        res++;
                }
            }
            return res;
        }
        /// 1926. Nearest Exit from Entrance in Maze, #Graph, #BFS
        ///Return the number of steps in the shortest path from the entrance to the nearest exit, or -1 if no such path exists.
        public int NearestExit(char[][] maze, int[] entrance)
        {
            int rowLen = maze.Length;
            int colLen = maze[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            bool[,] visit = new bool[rowLen, colLen];

            List<int[]> list = new List<int[]>() { entrance };
            visit[entrance[0], entrance[1]] = true;
            int step = 1;
            while (list.Count > 0)
            {
                List<int[]> next = new List<int[]>();
                foreach (var p in list)
                {
                    if (maze[p[0]][p[1]] == '+') continue;
                    foreach (var d in dxy4)
                    {
                        var r = p[0] + d[0];
                        var c = p[1] + d[1];
                        if (r >= 0 && r < rowLen && c >= 0 && c < colLen && maze[r][c] == '.' && !visit[r, c])
                        {
                            if (r == 0 || r == rowLen - 1 || c == 0 || c == colLen - 1) return step;
                            visit[r, c] = true;
                            next.Add(new int[] { r, c });
                        }
                    }
                }
                step++;
                list = next;
            }
            return -1;
        }

        ///1927. Sum Game, #Greedy
        //a string num of even length consisting of digits and '?' characters.
        //each turn , player can pick a '?' to replace any [0,9] digit
        //alice first, return true if sum of first half != sum of second half
        public bool SumGame(string num)
        {
            int sum1 = 0;
            int count1 = 0;
            int sum2 = 0;
            int count2 = 0;
            for(int i = 0; i < num.Length; i++)
            {
                if (i < num.Length / 2)
                {
                    if (num[i] == '?') count1++;
                    else sum1 += num[i] - '0';
                }
                else
                {
                    if (num[i] == '?') count2++;
                    else sum2 += num[i] - '0';
                }
            }
            //alice must win
            if ((count1 + count2) % 2 == 1) return true;
            //alice must add 0 or 9
            if (sum1 + (count1 - count2) / 2 * 9 != sum2) return true;
            else return false;
        }

        ///1929. Concatenation of Array
        public int[] GetConcatenation(int[] nums)
        {
            int n = nums.Length;
            Array.Resize(ref nums, n * 2);
            for (int i = 0; i < n; i++)
                nums[i + n] = nums[i];
            return nums;
        }
        /// 1930. Unique Length-3 Palindromic Subsequences
        ///Given a string s, return the number of unique palindromes of length three that are a subsequence of s.
        public int CountPalindromicSubsequence(string s)
        {
            int res = 0;
            Dictionary<char, List<int>> dict = new Dictionary<char, List<int>>();
            for(int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i])) dict[s[i]].Add(i);
                else dict.Add(s[i], new List<int>() { i});
            }

            foreach(var key in dict.Keys)
            {
                if (dict[key].Count == 1) continue;
                int left = dict[key].First();
                int right = dict[key].Last();
                if (right - left <= 1) continue;
                var map = new HashSet<char>(s.Substring(left + 1, right - 1 - left));
                res += map.Count;
            }

            return res;
        }


        ///1935. Maximum Number of Words You Can Type
        public int CanBeTypedWords(string text, string brokenLetters)
        {
            var set = new HashSet<char>(brokenLetters);
            var words = text.Split(' ').ToList();
            int res = 0;
            foreach(var word in words)
            {
                bool type = true;
                foreach(var c in word)
                {
                    if (set.Contains(c))
                    {
                        type = false;
                        break;
                    }
                }
                if (type) res++;
            }
            return res;
        }

        ///1936. Add Minimum Number of Rungs
        //Return the minimum number of rungs that must be added to the ladder to climb to the last rung.
        public int AddRungs(int[] rungs, int dist)
        {
            int prev = 0;
            int res = 0;
            foreach(var r in rungs)
            {
                if (r <= prev) continue;
                if (r - prev > dist)
                {
                    int n = (int)Math.Ceiling((r - prev) * 1.0 / dist);
                    res += n - 1;
                }
                prev = r;
            }
            return res;
        }
        ///
        /// 1941. Check if All Characters Have Equal Number of Occurrences
        ///A string s is good if all the characters that appear in s have the same number of occurrences
        public bool AreOccurrencesEqual(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (dict.ContainsKey(c)) dict[c]++;
                else dict.Add(c, 1);
            }
            var keys = dict.Keys.ToList();
            for (int i = 0; i < keys.Count - 1; i++)
            {
                if (dict[keys[i]] != dict[keys[i + 1]]) return false;
            }
            return true;
        }

        ///1944. Number of Visible People in a Queue, #Monotonic Stack
        ///A person can see another person to their right if everybody in between is shorter than both of them.
        ///the ith person can see the jth person if i<j and min(heights[i], heights[j]) > max(heights[i + 1,...,j - 1]).
        ///Return an array answer where answer[i] is the number of person can see to i-th's right in the queue.
        public int[] CanSeePersonsCount(int[] heights)
        {
            int n = heights.Length;
            int[] res = new int[heights.Length];
            Stack<int> stack = new Stack<int>();
            //from right to left
            for (int i=n-1; i>=0; i--)
            {
                int count = 0;
                while(stack.Count>0 && stack.Peek()< heights[i])
                {
                    count++;
                    stack.Pop();
                }
                res[i] = stack.Count==0? count:count+1;
                stack.Push(heights[i]);
            }
            return res;
        }

        ///1945. Sum of Digits of String After Convert
        public int GetLucky(string s, int k)
        {
            var sb =new StringBuilder();
            foreach(var c in s)
                sb.Append($"{c - 'a' + 1}");
            var str = sb.ToString();
            while (k-- > 0 && str.Length>1)
                str = str.Sum(x => x - '0').ToString();

            return int.Parse(str);
        }
    }
}