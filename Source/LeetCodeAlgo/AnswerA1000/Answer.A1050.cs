using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1051. Height Checker
        public int HeightChecker(int[] heights)
        {
            var expected = heights.OrderBy(x => x).ToArray();
            int res = 0;
            for (int i = 0; i < expected.Length; i++)
                if (heights[i] != expected[i]) res++;
            return res;
        }
        /// 1052. Grumpy Bookstore Owner, #Sliding Window
        public int MaxSatisfied(int[] customers, int[] grumpy, int minutes)
        {
            int n = grumpy.Length;
            int satisfied = 0;
            int notSatisfied = 0;
            int count = 0;
            int max = 0;
            for (int i = 0; i < n; i++)
            {
                if (grumpy[i] == 0) satisfied += customers[i];
                else
                {
                    notSatisfied += customers[i];
                    max = Math.Max(max, notSatisfied);
                }
                count++;
                if (count == minutes)
                {
                    if (grumpy[i + 1 - minutes] == 1) notSatisfied -= customers[i + 1 - minutes];
                    count--;
                }
            }
            return max + satisfied;
        }

        /// 1054. Distant Barcodes, #PriorityQueue, 
        ///Rearrange the barcodes so that no two adjacent barcodes are equal.
        public int[] RearrangeBarcodes(int[] barcodes)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var bar in barcodes)
            {
                if (dict.ContainsKey(bar)) dict[bar]++;
                else dict.Add(bar, 1);
            }

            //sort by occurrence - max heap
            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();

            foreach (var k in dict.Keys)
            {
                queue.Enqueue(k, dict[k]);
            }

            // poll from queue - put into res array
            int[] res = new int[barcodes.Length];
            int i = 0;//start from index-0, traversal all even indexes
            while (queue.Count > 0)
            {
                var key = queue.Dequeue();
                while (dict[key]-- > 0)
                {
                    res[i] = key;
                    i += 2;
                    //if exceed length,aka all even index already done, reset to index 1 for all odd indexes
                    if (i >= barcodes.Length) i = 1;
                }
            }
            return res;
        }

        /// 1071. Greatest Common Divisor of Strings
        ///Given two strings str1 and str2, return the largest string x such that x divides both str1 and str2.
        public string GcdOfStrings(string str1, string str2)
        {
            while (str1.Length > 0 && str2.Length > 0 && str1 + str2 == str2 + str1)
            {
                if (str1 == str2) return str1;
                if (str1.Length > str2.Length)
                {
                    var mod = str1.Length % str2.Length;
                    if (mod == 0) mod = str2.Length;
                    str1 = str1.Substring(0, mod);
                }
                else
                {
                    var mod = str2.Length % str1.Length;
                    if (mod == 0) mod = str1.Length;
                    str2 = str2.Substring(0, mod);
                }
            }
            return "";
        }

        public string GcdOfStrings_My(string str1, string str2)
        {
            if (str1.Length < str2.Length)
            {
                var temp = str2;
                str2 = str1;
                str1 = temp;
            }
            int gcb = (int)getGCD_Long(str1.Length, str2.Length);
            for (int i = gcb; i >= 1; i--)
            {
                if (str1.Length % gcb == 0 && str2.Length % gcb == 0)
                {
                    var s = str2.Substring(0, i);
                    if (GcdOfStrings_Check(str1, s) && GcdOfStrings_Check(str2, s))
                    {
                        return s;
                    }
                }
            }
            return "";
        }

        public bool GcdOfStrings_Check(string str1, string str2)
        {
            if (str1.Length % str2.Length != 0) return false;
            for (int i = 0; i < str1.Length; i += str2.Length)
            {
                if (str1.Substring(i, str2.Length) != str2) return false;
            }
            return true;
        }

        ///1074. Number of Submatrices That Sum to Target, #Prefix Sum
        ///Given a matrix and a target, return the number of non-empty submatrices that sum to target.
        public int NumSubmatrixSumTarget(int[][] matrix, int target)
        {
            int res = 0;
            int m = matrix.Length;
            int n = matrix[0].Length;
            //each row is PrefixSum
            for (int i = 0; i < m; i++)
                for (int j = 1; j < n; j++)
                    matrix[i][j] += matrix[i][j - 1];
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    //loop all column pairs
                    dict.Clear();
                    dict.Add(0, 1);//init, add{0,1}
                    int cur = 0;
                    for (int k = 0; k < m; k++)
                    {
                        //sum of current row's cols [i,j]
                        cur += matrix[k][j] - (i > 0 ? matrix[k][i - 1] : 0);
                        if (dict.ContainsKey(cur - target))
                        {
                            res += dict[cur - target];
                        }
                        if(dict.ContainsKey(cur))dict[cur]++;
                        else dict.Add(cur,1);
                    }
                }
            }
            return res;
        }

        ///1078. Occurrences After Bigram
        public string[] FindOcurrences(string text, string first, string second)
        {
            var res = new List<string>();
            var words = text.Split(' ').ToArray();
            for(int i = 2; i < words.Length; i++)
            {
                if (words[i - 2] == first && words[i - 1] == second)
                    res.Add(words[i]);
            }
            return res.ToArray();
        }
        /// 1079. Letter Tile Possibilities, #Backtracking, #DFS
        ///You have n  tiles, where each tile has one letter tiles[i] printed on it.
        ///Return the number of possible non-empty sequences can make using the letters printed on those tiles.
        public int NumTilePossibilities_DFS(string tiles)
        {
            int[] arr = new int[26];
            foreach (var c in tiles)
                arr[c - 'A']++;
            return NumTilePossibilities_DFS(arr);
        }

        private int NumTilePossibilities_DFS(int[] arr)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 0) continue;
                sum++;
                arr[i]--;
                sum += NumTilePossibilities_DFS(arr);
                arr[i]++;
            }
            return sum;
        }

        public int NumTilePossibilities_BackTracking(string tiles)
        {
            HashSet<string> set = new HashSet<string>();
            for (int i = 1; i <= tiles.Length; i++)
            {
                NumTilePossibilities_BackTracking(i, "", tiles, new HashSet<int>(), set);
            }
            return set.Count;
        }

        private void NumTilePossibilities_BackTracking(int count, string curr, string tiles, HashSet<int> visit, HashSet<string> set)
        {
            if (count == 0)
            {
                if (curr.Length > 0)
                    set.Add(curr);
                return;
            }

            for (int i = 0; i < tiles.Length; i++)
            {
                if (visit.Contains(i)) continue;
                var nextVisit = new HashSet<int>(visit) { i };
                NumTilePossibilities_BackTracking(count - 1, curr + tiles[i].ToString(), tiles, nextVisit, set);
            }
        }

        /// 1089. Duplicate Zeros
        ///Given a fixed-length integer array arr, duplicate each occurrence of zero, shifting the remaining elements to the right.
        public void DuplicateZeros(int[] arr)
        {
            int n = arr.Length;
            int[] temp = new int[n];
            for (int i = 0, j = 0; i < n && j < n; i++)
            {
                temp[j++] = arr[i];
                if (arr[i] == 0 && j < n)
                    temp[j++] = arr[i];
            }
            for (int i = 0; i < n; i++)
                arr[i] = temp[i];
        }

        /// 1091. Shortest Path in Binary Matrix, #Graph, #BFS
        ///Given an n x n binary matrix grid, return the length of the shortest clear path in the matrix.
        ///If there is no clear path, return -1. 8 direction
        public int ShortestPathBinaryMatrix(int[][] grid)
        {
            int len = grid.Length;
            if (len == 1)
                return grid[0][0] == 0 ? 1 : -1;
            if (grid[0][0] == 1 || grid[len - 1][len - 1] == 1)
                return -1;
            bool[,] visit = new bool[len, len];
            int[][] dxy8 = new int[8][] {
                new int[]{0,1}, new int[] {1, 0 },
                new int[]{ 0,-1}, new int[] { -1, 0 },
                new int[]{ 1,1 }, new int[]{ -1, -1 },
                new int[]{ -1, 1 },new int[] { 1, -1 } };
            List<int[]> list = new List<int[]>();
            int step = 0;
            list.Add(new int[] { 0, 0 });
            visit[0, 0] = true;
            step++;
            while (list.Count > 0)
            {
                List<int[]> next = new List<int[]>();
                foreach (var cell in list)
                {
                    foreach (var d in dxy8)
                    {
                        int r = cell[0] + d[0];
                        int c = cell[1] + d[1];
                        if (r >= 0 && r <= len - 1 && c >= 0 && c <= len - 1 && !visit[r, c])
                        {
                            if (r == len - 1 && c == len - 1)
                                return step + 1;
                            visit[r, c] = true;
                            if (grid[r][c] == 0)
                            {
                                next.Add(new int[] { r, c, });
                            }
                        }
                    }
                }
                list = next;
                step++;
            }
            return -1;
        }

        ///1094. Car Pooling
        public bool CarPooling(int[][] trips, int capacity)
        {
            int[] arr = new int[1001];
            foreach(var trip in trips)
            {
                arr[trip[1]] -= trip[0];
                arr[trip[2]] += trip[0];
            }

            foreach(var n in arr)
            {
                capacity += n;
                if (capacity < 0) return false;
            }

            return true;
        }

    }
}