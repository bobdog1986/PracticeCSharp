﻿using System;
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

        ///1255. Maximum Score Words Formed by Letters, #Backtracking, #DFS
        ///Return the maximum score of any valid set of words formed by given letters(words[i] use only 1 time).
        ///each letter can only be used once.Score of letters 'a', 'b',... 'z' is given by score[0], score[25] respectively.
        public int MaxScoreWords(string[] words, char[] letters, int[] score)
        {
            int res = 0;
            int[] arr = new int[26];
            foreach (var l in letters)
                arr[l - 'a']++;
            MaxScoreWords_DFS(words, arr, 0, 0, score, ref res);
            return res;
        }

        private void MaxScoreWords_DFS(string[] words, int[] arr, int index, int currScore, int[] score, ref int res)
        {
            res = Math.Max(res, currScore);
            for (int i = index; i < words.Length; i++)
            {
                int[] map = new int[26];
                bool valid = true;
                foreach (var c in words[i])
                {
                    map[c - 'a']++;
                    if (map[c - 'a'] > arr[c - 'a'])
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    int sum = 0;
                    foreach (var c in words[i])
                    {
                        sum += score[c - 'a'];
                        arr[c - 'a']--;
                    }
                    MaxScoreWords_DFS(words, arr, i + 1, currScore + sum, score, ref res);
                    foreach (var c in words[i])
                    {
                        arr[c - 'a']++;
                    }
                }
            }
        }

        /// 1260. Shift 2D Grid
        ///Return the 2D grid after applying shift operation k times.
        public IList<IList<int>> ShiftGrid(int[][] grid, int k)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            var res = new List<IList<int>>();
            foreach (var r in grid)
                res.Add(r.ToList());
            k %= rowLen * colLen;
            while (k-- > 0)
            {
                int temp = res[rowLen - 1][colLen - 1];
                for (int i = 0; i < rowLen; i++)
                {
                    res[i].Insert(0, temp);
                    temp = res[i].Last();
                    res[i].RemoveAt(res[i].Count - 1);
                }
            }
            return res;
        }

        ///1261. Find Elements in a Contaminated Binary Tree. see FindElements

        ///1266. Minimum Time Visiting All Points
        public int MinTimeToVisitAllPoints(int[][] points)
        {
            int res = 0;
            var prev = points[0];
            foreach (var curr in points)
            {
                res += Math.Max(Math.Abs(curr[0] - prev[0]), Math.Abs(curr[1] - prev[1]));
                prev = curr;
            }
            return res;
        }

        ///1267. Count Servers that Communicate
        public int CountServers(int[][] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            int[] rows = new int[m];
            int[] cols = new int[n];
            int res = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        res++;
                        rows[i]++;
                        cols[j]++;
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        if (rows[i] == 1 && cols[j] == 1)
                            res--;
                    }
                }
            }
            return res;
        }

        ///1268. Search Suggestions System, #Trie
        //Design a system that suggests at most three product names after each character of searchWord is typed.
        //Suggested products should have common prefix with searchWord. return lexicographically minimums products.
        public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
        {
            var res = new IList<string>[searchWord.Length];
            for (int i = 0; i < searchWord.Length; i++)
                res[i] = new List<string>();
            products = products.OrderBy(x => x).ToArray();
            var root = new TrieItem();
            foreach (var product in products)
            {
                SuggestedProducts_build(product, root);
            }
            var curr = root;
            for (int i = 0; i < searchWord.Length; i++)
            {
                var c = searchWord[i];
                if (!curr.dict.ContainsKey(c))
                    break;
                res[i] = curr.dict[c].list.Take(3).ToList();
                curr = curr.dict[c];
            }
            return res;
        }

        private void SuggestedProducts_build(string product, TrieItem root)
        {
            var curr = root;
            foreach (var c in product)
            {
                if (!curr.dict.ContainsKey(c))
                    curr.dict.Add(c, new TrieItem());
                curr.dict[c].list.Add(product);
                curr = curr.dict[c];
            }
        }

        ///1275. Find Winner on a Tic Tac Toe Game
        // public string Tictactoe(int[][] moves)
        // {
        //     int[][] mat = new int[3][];
        //     for (int i = 0; i < 3; i++)
        //     {
        //         mat[i] = new int[3];
        //     }
        //     int sign = 1;
        //     foreach (var move in moves)
        //     {
        //         mat[move[0]][move[1]] = sign;
        //         sign = -sign;
        //     }
        //     for (int i = 0; i < 3; i++)
        //     {
        //         if (mat[i].All(x => x == 1)) return "A";
        //         if (mat[0][i] == 1 && mat[1][i] == 1 && mat[2][i] == 1)
        //             return "A";
        //         if (mat[i].All(x => x == -1)) return "B";
        //         if (mat[0][i] == -1 && mat[1][i] == -1 && mat[2][i] == -1)
        //             return "B";
        //     }

        //     if (mat[0][0] == 1 && mat[1][1] == 1 && mat[2][2] == 1) return "A";
        //     if (mat[0][0] == -1 && mat[1][1] == -1 && mat[2][2] == -1) return "B";

        //     if (mat[0][2] == 1 && mat[1][1] == 1 && mat[2][0] == 1) return "A";
        //     if (mat[0][2] == -1 && mat[1][1] == -1 && mat[2][0] == -1) return "B";
        //     return moves.Length == 9 ? "Draw" : "Pending";
        // }
        
        ///1277. Count Square Submatrices with All Ones, #DP
        ///Given a m * n matrix of ones and zeros, return how many square submatrices have all ones.
        public int CountSquares(int[][] matrix)
        {
            int res = 0;
            for (int i = 0; i < matrix.Length; ++i)
            {
                for (int j = 0; j < matrix[0].Length; ++j)
                {
                    if (matrix[i][j] > 0 && i > 0 && j > 0)
                    {
                        matrix[i][j] = Math.Max(matrix[i - 1][j - 1], Math.Max(matrix[i - 1][j], matrix[i][j - 1])) + 1;
                    }
                    res += matrix[i][j];
                }
            }
            return res;
        }
        /// 1281. Subtract the Product and Sum of Digits of an Integer
        ///return the difference between the product of its digits and the sum of its digits.
        public int SubtractProductAndSum(int n)
        {
            List<int> list = new List<int>();
            while (n > 0)
            {
                list.Add(n % 10);
                n /= 10;
            }
            return list.Aggregate((x, y) => x * y) - list.Aggregate((x, y) => x + y);
        }

        ///1282. Group the People Given the Group Size They Belong To
        public IList<IList<int>> GroupThePeople(int[] groupSizes)
        {
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < groupSizes.Length; i++)
            {
                if (!dict.ContainsKey(groupSizes[i])) dict.Add(groupSizes[i], new List<int>());
                dict[groupSizes[i]].Add(i);
            }
            var keys = dict.Keys.OrderBy(x => x).ToList();
            var res = new List<IList<int>>();
            foreach (var key in keys)
            {
                for (int i = 0; i < dict[key].Count; i += key)
                {
                    res.Add(dict[key].GetRange(i, key));
                }
            }
            return res;
        }

        ///1283. Find the Smallest Divisor Given a Threshold, #Binary Search
        //a positive integer divisor, divide all the array by it, and sum the division's result.
        //Find the smallest divisor such that the result mentioned above is less than or equal to threshold.
        public int SmallestDivisor(int[] nums, int threshold)
        {
            int left = 1;
            int right = 1_000_000;
            while (left < right)
            {
                int mid = (left + right) / 2;
                int sum = 0;
                foreach (var n in nums)
                    sum += (int)Math.Ceiling(1.0 * n / mid);
                if (sum <= threshold)
                    right = mid;
                else
                    left = mid + 1;
            }
            return left;
        }
        /// 1284. Minimum Number of Flips to Convert Binary Matrix to Zero Matrix, #DFS
        ///Given a m x n binary matrix mat. In one step, you can choose one cell and
        ///flip it and all the four neighbors of it if they exist (Flip is changing 1 to 0 and 0 to 1).
        ///Return the minimum number of steps required to convert mat to a zero matrix or -1 if you cannot.
        public int MinFlips(int[][] mat)
        {
            int res = -1;
            int[][] dxy = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            int count = mat.Sum(x => x.Sum());
            bool[] visit = new bool[mat.Length * mat[0].Length];
            MinFlips_DFS(mat, visit, count, 0, dxy, ref res);
            return res;
        }

        private void MinFlips_DFS(int[][] mat, bool[] visit, int count, int step, int[][] dxy, ref int res)
        {
            if (count == 0)
            {
                if (res == -1) res = step;
                else res = Math.Min(res, step);
                return;
            }

            for (int i = 0; i < mat.Length * mat[0].Length; i++)
            {
                if (visit[i]) continue;
                visit[i] = true;

                int row = i / mat[0].Length;
                int col = i % mat[0].Length;
                count += mat[row][col] == 0 ? 1 : -1;
                mat[row][col] ^= 1;
                foreach (var d in dxy)
                {
                    int r = row + d[0];
                    int c = col + d[1];
                    if (r < 0 || r >= mat.Length || c < 0 || c >= mat[0].Length) continue;
                    count += mat[r][c] == 0 ? 1 : -1;
                    mat[r][c] ^= 1;
                }

                MinFlips_DFS(mat, visit, count, step + 1, dxy, ref res);

                count += mat[row][col] == 0 ? 1 : -1;
                mat[row][col] ^= 1;

                foreach (var d in dxy)
                {
                    int r = row + d[0];
                    int c = col + d[1];
                    if (r < 0 || r >= mat.Length || c < 0 || c >= mat[0].Length) continue;
                    count += mat[r][c] == 0 ? 1 : -1;
                    mat[r][c] ^= 1;
                }

                visit[i] = false;

            }
        }

        ///1287. Element Appearing More Than 25% In Sorted Array
        //public int FindSpecialInteger(int[] arr)
        //{
        //    Dictionary<int,int> dict = new Dictionary<int,int>();
        //    int n = arr.Length;
        //    foreach(var i in arr)
        //    {
        //        if (!dict.ContainsKey(i)) dict.Add(i, 0);
        //        dict[i]++;
        //        if (dict[i] > 0.25*n) return i;
        //    }
        //    return -1;
        //}


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
                    if (v[1] > right)
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
                sum <<= 1;
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

        ///1292. Maximum Side Length of a Square with Sum Less than or Equal to Threshold, #Prefix Sum
        //return the maximum side-length of a square with a sum <= threshold or return 0 if there is no such square.
        public int MaxSideLength(int[][] mat, int threshold)
        {
            int m = mat.Length;
            int n = mat[0].Length;
            int[][] prefixSum = new int[m + 1][];
            for (int i = 0; i <= m; i++)
                prefixSum[i] = new int[n + 1];

            int len = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    prefixSum[i + 1][j + 1] = prefixSum[i + 1][j] + prefixSum[i][j + 1] - prefixSum[i][j] + mat[i][j];
                    if (i - len >= 0 && j - len >= 0 &&
                        prefixSum[i + 1][j + 1] - prefixSum[i - len][j + 1] - prefixSum[i + 1][j - len] + prefixSum[i - len][j - len] <= threshold
                       )
                    {
                        len++;
                    }
                }
            }
            return len;
        }

        /// 1295. Find Numbers with Even Number of Digits
        ///Given an array nums of integers, return how many of them contain an even number of digits.
        public int FindNumbers(int[] nums)
        {
            //return nums.Count(x=> (x>=10&&x<100)||(x>=1000&&x<10000) ||x==100000 );
            return nums.Count(x => FindNumbers_isEvenDigit(x));
        }

        private bool FindNumbers_isEvenDigit(int n)
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
        ///replace every element with the greatest element on its right, and replace the last element with -1.
        public int[] ReplaceElements(int[] arr)
        {
            int max = -1;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                var temp = arr[i];
                arr[i] = max;
                max = Math.Max(max, temp);
            }
            return arr;
        }
    }
}