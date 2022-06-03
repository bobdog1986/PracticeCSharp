using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1905. Count Sub Islands, #Graph, #DFS
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

        ///1910. Remove All Occurrences of a Substring
        ///Find the leftmost occurrence of the substring part and remove it from s.
        ///Return s after removing all occurrences of part.
        public string RemoveOccurrences(string s, string part)
        {
            var index=s.IndexOf(part);
            if (index != -1) return RemoveOccurrences(s.Substring(0, index) + s.Substring(index + part.Length), part);
            else return s;
        }
        ///1920. Build Array from Permutation
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
            long Modulo = 1000000007;

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

        ///1926. Nearest Exit from Entrance in Maze, #Graph, #BFS
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

        ///1944. Number of Visible People in a Queue, #Monotonic
        ///A person can see another person to their right in the queue if everybody in between is shorter than both of them.
        ///More formally, the ith person can see the jth person if i<j and min(heights[i], heights[j]) > max(heights[i + 1], heights[i + 2], ..., heights[j - 1]).
        ///Return an array answer of length n where answer[i] is the number of people the ith person can see to their right in the queue.
        public int[] CanSeePersonsCount(int[] heights)
        {
            int[] res = new int[heights.Length];
            int[] arr= new int[heights.Length];//all possible persons which current can see, monotonic, desc, you can also using stack
            int count = 0;//how many persons in arr
            //from tail to head
            for (int i=heights.Length-1; i>=0; i--)
            {
                if(count == 0)
                {
                    res[i] = 0;
                }
                else
                {
                    int j = count - 1;//closest person to current
                    while (j > 0)//person at index-0, is the last one, will block no one. so just skip it.
                    {
                        if (arr[j] >= heights[i]) break;//if this person is taller than current, he/she will block all others behind
                        else j--;
                    }
                    res[i] = count - j;
                }

                //remove all persons who is lower than current
                while (count > 0 && arr[count-1] <= heights[i])
                    arr[count-- -1]=0;
                arr[count++]=heights[i];
            }
            return res;
        }
    }
}