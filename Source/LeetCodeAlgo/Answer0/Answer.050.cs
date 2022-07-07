using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///50. Pow(x, n)
        ///Implement pow(x, n), which calculates x raised to the power n (i.e., x^n).
        ///-100.0 < x < 100.0, -2^31 <= n <= 2^31-1, -10^4 <= x^n <= 10^4
        public double MyPow(double x, int n)
        {
            if (n == 0) return 1;
            long m = n;
            if (m < 0)
            {
                m = -m;
                x = 1 / x;
            }
            if (m == 1) return x;
            return (m % 2 == 0) ? MyPow(x * x, (int)(m / 2)) : x * MyPow(x * x, (int)(m / 2));
        }

        ///51. N-Queens, #Backtracking
        ///The n-queens puzzle is the problem of placing n queens on an n x n chessboard such that no two queens attack each other.
        ///Given an integer n, return all distinct solutions to the n-queens puzzle. You may return the answer in any order.
        public IList<IList<string>> SolveNQueens(int n)
        {
            var ans = new List<IList<string>>();
            var list = new List<int>();
            SolveNQueens_BackTracking(list, n, n, ans);
            return ans;
        }

        private void SolveNQueens_BackTracking(IList<int> list, int n, int len, IList<IList<string>> ans)
        {
            if (n == 0)
            {
                List<string> seq = new List<string>();
                foreach (var i in list)
                {
                    string s = "";
                    int j = 0;
                    while (j < len)
                    {
                        s += j == i ? "Q" : ".";
                        j++;
                    }
                    seq.Add(s);
                }
                ans.Add(seq);
                return;
            }

            for (int i = 0; i < len; i++)
            {
                bool canAttack = false; ;
                for (int j = 0; j < list.Count; j++)
                {
                    var queen = list[j];
                    if (queen == i || Math.Abs(queen - i) == Math.Abs(list.Count - j))
                    {
                        canAttack = true;
                        break;
                    }
                }
                if (canAttack) continue;
                var next = new List<int>(list) { i };
                SolveNQueens_BackTracking(next, n - 1, len, ans);
            }
        }

        ///52. N-Queens II,  #Backtracking
        ///The n-queens puzzle is the problem of placing n queens on an n x n chessboard such that no two queens attack each other.
        ///Given an integer n, return the number of distinct solutions to the n-queens puzzle.
        public int TotalNQueens(int n)
        {
            int ans = 0;
            var list = new List<int>();
            TotalNQueens_Backtracking(list, n, n, ref ans);
            return ans;
        }

        private void TotalNQueens_Backtracking(IList<int> list, int n, int len, ref int ans)
        {
            if (n == 0)
            {
                ans++;
                return;
            }
            for (int i = 0; i < len; i++)
            {
                bool canAttack = false; ;
                for (int j = 0; j < list.Count; j++)
                {
                    var queen = list[j];
                    if (queen == i || Math.Abs(queen - i) == Math.Abs(list.Count - j))
                    {
                        canAttack = true;
                        break;
                    }
                }
                if (canAttack) continue;
                var next = new List<int>(list) { i };
                TotalNQueens_Backtracking(next, n - 1, len, ref ans);
            }
        }

        /// 53. Maximum Subarray, #DP,#Kadane
        // find the contiguous subarray which has the largest sum and return its sum.
        public int MaxSubArray(int[] nums)
        {
            int sum = 0;
            int max = int.MinValue; //if all negtive num ,return nums.Max()
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                max = Math.Max(max, sum);
                if (sum <= 0) sum = 0;
            }
            return max;
        }

        public int MaxSubArray_DP(int[] nums)
        {
            int n = nums.Length;
            int[] dp = new int[n];
            dp[0] = nums[0];
            int max = nums[0];
            for (int i = 1; i <n; i++)
            {
                if (dp[i - 1] > 0) dp[i] = dp[i - 1] + nums[i];
                else dp[i] = nums[i];
                max = Math.Max(max, dp[i]);
            }
            //return dp.Max();
            return max;
        }

        ///54. Spiral Matrix
        ///Given an m x n matrix, return all elements of the matrix in spiral order.
        public IList<int> SpiralOrder(int[][] matrix)
        {
            var ans = new List<int>();
            var rLen = matrix.Length;
            var cLen = matrix[0].Length;
            if (rLen == 1)
                return matrix[0].ToList();
            if (cLen == 1)
            {
                foreach (var x in matrix)
                    ans.Add(x[0]);
                return ans;
            }

            int direct = 0;
            int r = 0;
            int c = 0;
            int row1 = 0;
            int row2 = rLen - 1;
            int col1 = 0;
            int col2 = cLen - 1;
            for (int i = 0; i < rLen * cLen; i++)
            {
                if (direct == 0)
                {
                    ans.Add(matrix[r][c]);
                    c++;
                    if (c > col2)
                    {
                        direct = 1;
                        row1++;
                        r++;
                        c--;
                    }
                }
                else if (direct == 1)
                {
                    ans.Add(matrix[r][c]);
                    r++;
                    if (r > row2)
                    {
                        direct = 2;
                        col2--;
                        r--;
                        c--;
                    }
                }
                else if (direct == 2)
                {
                    ans.Add(matrix[r][c]);
                    c--;
                    if (c < col1)
                    {
                        direct = 3;
                        row2--;
                        r--;
                        c++;
                    }
                }
                else
                {
                    ans.Add(matrix[r][c]);
                    r--;
                    if (r < row1)
                    {
                        direct = 0;
                        col1++;
                        r++;
                        c++;
                    }
                }
            }

            return ans;
        }

        ///55. Jump Game, #DP
        ///start at 0-index,nums[i] maximum jump length at that position.
        //Return true if you can reach the last index, or false otherwise.
        public bool CanJump(int[] nums)
        {
            bool[] dp = new bool[nums.Length];
            dp[0] = true;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (dp[i] && nums[i] > 0)
                {
                    int j = 1;
                    if (i + nums[i] >= nums.Length - 1)
                        return true;
                    while (j <= nums[i])
                    {
                        dp[i + j] = true;
                        j++;
                    }
                }
            }
            return dp.Last();
        }

        ///56. Merge Intervals
        ///Given an array of intervals where intervals[i] = [starti, endi], merge all overlapping intervals,
        ///and return an array of the non-overlapping intervals that cover all the intervals in the input.
        ///Input: intervals =  [[1,3],[2,6],[8,10],[15,18]]
        ///Output: [[1,6],[8,10],[15,18]]
        public int[][] Merge(int[][] intervals)
        {
            if (intervals.Length == 1)
                return intervals;
            List<int[]> list = new List<int[]>();
            var mat = intervals.OrderBy(x => x[0]).ToList();
            int[] last = null;
            for (int i = 0; i < mat.Count; i++)
            {
                var current = mat[i];
                if (last == null)
                {
                    last = current;
                }
                else
                {
                    if (last[1] < current[0])
                    {
                        list.Add(last);
                        last = current;
                    }
                    else
                    {
                        last[0] = Math.Min(last[0], current[0]);
                        last[1] = Math.Max(last[1], current[1]);
                    }
                }
            }
            if (last != null)
                list.Add(last);
            return list.ToArray();
        }

        ///57. Insert Interval
        ///non-overlapping intervals intervals represent the start and the end of the ith interval sorted in ascending order by start
        ///0 <= start <= end <= 10^5
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            var ans = new List<int[]>();
            int start = -1, end = -1;
            bool hasAdd = false;
            foreach (var curr in intervals)
            {
                if (hasAdd)
                {
                    ans.Add(curr);
                }
                else
                {
                    int left = start == -1 ? newInterval[0] : start;
                    int right = end == -1 ? newInterval[1] : end;

                    if (curr[1] < newInterval[0])
                    {
                        //add left no joint
                        ans.Add(curr);
                    }
                    else if (curr[0] > right)
                    {
                        ans.Add(new int[] { left, right });
                        hasAdd = true;
                        ans.Add(curr);
                    }
                    else
                    {
                        start = Math.Min(left, curr[0]);
                        end = Math.Max(right, curr[1]);
                    }
                }
            }

            if (!hasAdd)
            {
                int left = start == -1 ? newInterval[0] : start;
                int right = end == -1 ? newInterval[1] : end;
                if (intervals.Length == 0 || intervals[0][0] > right)
                {
                    ans.Insert(0, new int[] { left, right });
                }
                else
                {
                    ans.Add(new int[] { left, right });
                }
            }

            return ans.ToArray();
        }

        ///58. Length of Last Word
        public int LengthOfLastWord(string s)
        {
            int ans = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == ' ')
                {
                    if (ans == 0) continue;
                    else break;
                }
                else
                {
                    ans++;
                }
            }
            return ans;
        }

        /// 59. Spiral Matrix II
        ///Given a positive integer n, generate an n x n matrix filled with elements from 1 to n2 in spiral order.
        public int[][] GenerateMatrix(int n)
        {
            int[][] ans = new int[n][];
            for (int i = 0; i < n; i++)
                ans[i] = new int[n];

            int j = 1;
            int r = 0;
            int c = 0;
            int direct = 0;
            int row1 = 0;
            int row2 = n - 1;
            int col1 = 0;
            int col2 = n - 1;

            while (j <= n * n)
            {
                if (direct == 0)
                {
                    ans[r][c] = j;

                    if (c >= col2)
                    {
                        r++;
                        row1++;
                        direct++;
                    }
                    else
                    {
                        c++;
                    }
                }
                else if (direct == 1)
                {
                    ans[r][c] = j;

                    if (r >= row2)
                    {
                        c--;
                        col2--;
                        direct++;
                    }
                    else
                    {
                        r++;
                    }
                }
                else if (direct == 2)
                {
                    ans[r][c] = j;

                    if (c <= col1)
                    {
                        r--;
                        row2--;
                        direct++;
                    }
                    else
                    {
                        c--;
                    }
                }
                else if (direct == 3)
                {
                    ans[r][c] = j;

                    if (r <= row1)
                    {
                        c++;
                        col1++;
                        direct = 0;
                    }
                    else
                    {
                        r--;
                    }
                }

                j++;
            }
            return ans;
        }

        ///60. Permutation Sequence
        ///The set [1, 2, 3, ..., n] contains a total of n! unique permutations.
        ///Given n and k, return the kth permutation sequence.
        ///1 <= n <= 9, 1 <= k <= n!
        public string GetPermutation(int n, int k)
        {
            List<int> digits = new List<int>();
            for (int i = 1; i <= n; i++)
                digits.Add(i);
            List<int> ans = new List<int>();
            int x = n;
            while (x >= 1)
            {
                if (k == 1)
                {
                    //k==1, add all digits as current sequence
                    ans.AddRange(digits);
                    break;
                }
                else if (k == getFactorial(x))
                {
                    //k==n!, add all digits as reversed sequence
                    digits.Reverse();
                    ans.AddRange(digits);
                    break;
                }

                var nextFactor = (int)getFactorial(x - 1);
                if (k <= nextFactor)
                {
                    //add digits[0], then call next loop
                    ans.Add(digits[0]);
                    digits.RemoveAt(0);
                }
                else
                {
                    var index = k / nextFactor;
                    var mod = k % nextFactor;
                    if (mod == 0) index--;
                    ans.Add(digits[index]);
                    digits.RemoveAt(index);
                    k -= nextFactor * index;
                }
                x--;
            }

            return String.Join("", ans);
        }

        public string TestGetPermutation()
        {
            int n = 4;
            var k = getFactorial(n);
            for (int i = 1; i <= k; i++)
            {
                var r = GetPermutation(n, i);
                Console.WriteLine($"n={n},k={i}, seq = {r}");
            }
            return "";
        }

        ///61. Rotate List
        ///Given the head of a linked list, rotate the list to the right by k places.
        public ListNode RotateRight(ListNode head, int k)
        {
            if (head == null) return head;
            ListNode ans = null;
            ListNode tail = null;
            ListNode rotateTail = null;
            var node = head;
            int count = 0;
            while (node != null)
            {
                if (node.next == null)
                    tail = node;
                node = node.next;
                count++;
            }
            k %= count;
            if (k == 0) return head;

            node = head;
            int i = count - k;//if rotate left,set i=k;
            while (i-- > 0)
            {
                if (i == 0) rotateTail = node;
                node = node.next;
            }

            ans = node;
            tail.next = head;
            rotateTail.next = null;
            return ans;
        }

        /// 62. Unique Paths, #DP
        ///Move from grid[0][0] to grid[m - 1][n - 1], each step can only move down or right.
        ///A(m-1+n-1)/A(m-1)/A(n-1)
        public int UniquePaths(int m, int n)
        {
            var dp = init2DMatrix(m, n, 1);
            for (int i = 1; i < m; i++)
                for (int j = 1; j < n; j++)
                    dp[i] [j] = dp[i - 1][ j] + dp[i][ j - 1];
            return dp[m - 1][ n - 1];
        }

        ///63. Unique Paths II, #DP
        /// Move from grid[0][0] to grid[m - 1][n - 1], each step can only move down or right.
        /// An obstacle and space is marked as 1 and 0 respectively in the grid.
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            var grid = obstacleGrid;

            int r = grid.Length;
            int c = grid[0].Length;

            int[,] dp = new int[r, c];
            dp[0, 0] = grid[0][0] == 0 ? 1 : 0;
            for (int i = 1; i < r; i++)
                dp[i, 0] = grid[i][0] == 0 ? dp[i - 1, 0] : 0;

            for (int j = 1; j < c; j++)
                dp[0, j] = grid[0][j] == 0 ? dp[0, j - 1] : 0;

            for (int i = 1; i < r; i++)
                for (int j = 1; j < c; j++)
                    dp[i, j] = grid[i][j] == 0 ? dp[i - 1, j] + dp[i, j - 1] : 0;

            return dp[r - 1, c - 1];
        }

        ///64. Minimum Path Sum, #DP
        ///Given a m x n grid filled with non-negative numbers,find a path from top left to bottom right,
        ///which minimizes the sum of all numbers along its path. Only move either down or right at any point in time.
        public int MinPathSum(int[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;
            var dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++)
                dp[i, 0] = int.MaxValue;
            for (int i = 0; i <= n; i++)
                dp[0, i] = int.MaxValue;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (i == 1 && j == 1)
                    {
                        dp[i, j] = grid[i - 1][j - 1];
                    }
                    else
                    {
                        dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + grid[i - 1][j - 1];
                    }
                }
            }
            return dp[m, n];
        }

        /// 65. Valid Number
        ///Given a string s, return true if s is a valid number.
        public bool IsNumber(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            s = s.TrimStart();
            s = s.TrimEnd();
            if (string.IsNullOrEmpty(s)) return false;

            try
            {
                double d = double.Parse(s);
                return true;
            }
            catch
            {
                if (s.Contains(" ")) return false;

                if (s.Contains("e"))
                {
                    int idx = s.IndexOf('e');
                    if (idx == s.LastIndexOf('e') && (idx > 0 && idx < s.Length - 1))
                    {
                        var arr = s.Split('e');
                        if (arr.Length != 2) return false;
                        double part;
                        long exp;
                        try
                        {
                            part = double.Parse(arr[0]);
                            exp = long.Parse(arr[1]);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
            }

            return false;
        }

        ///66. Plus One
        /// 0-index is the highest bit, Increment the large integer by one and return the resulting array of digits.
        ///Input: digits = [1,2,3]
        ///Output: [1,2,4]
        public int[] PlusOne(int[] digits)
        {
            int i = digits.Length - 1;
            while (i >= 0)
            {
                if (digits[i] < 9 || i == 0)
                {
                    digits[i]++;
                    break;
                }
                else
                {
                    digits[i] = 0;
                }
                i--;
            }
            if (digits[0] == 10)
            {
                var list = new List<int>(digits);
                list[0] %= 10;
                list.Insert(0, 1);
                digits = list.ToArray();
            }
            return digits;
        }

        /// 67. Add Binary
        /// Given two binary strings a and b, return their sum as a binary string.
        ///1 <= a.Length, b.Length <= 104, no leading zero
        public string AddBinary(string a, string b)
        {
            List<char> list = new List<char>();
            int i = a.Length - 1;
            int j = b.Length - 1;
            int carry = 0;
            while(i>=0 || j >= 0)
            {
                int c1 = i >= 0 ? a[i--] - '0' : 0;
                int c2 = j >= 0 ? b[j--] - '0' : 0;
                int c = c1 + c2 + carry;
                if (c %2==0) list.Insert(0, '0');
                else list.Insert(0, '1');
                carry = c / 2;
            }
            if (carry==1)
                list.Insert(0, '1');
            return new string(list.ToArray());
        }

        ///68. Text Justification
        ///Given an array of strings words and a width maxWidth, format the text such that each line has exactly maxWidth characters and is fully (left and right) justified.
        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            var res = new List<string>();

            var list = new List<string>();

            foreach (var word in words)
            {
                int sum = list.Sum(x => x.Length);
                if (sum + list.Count + word.Length > maxWidth)
                {
                    FullJustify_Add(list, maxWidth, res);
                }
                list.Add(word);
            }
            FullJustify_Add(list, maxWidth, res, true);
            return res;
        }

        private void FullJustify_Add(List<string> list, int maxWidth, List<string> res, bool leftJustify = false)
        {
            if (list.Count == 0) return;
            else if (list.Count == 1 || leftJustify)
            {
                string str = string.Join(" ", list).PadRight(maxWidth, ' ');
                res.Add(str);
            }
            else
            {
                int spaces = maxWidth - list.Sum(x => x.Length);
                int mod = spaces % (list.Count - 1);//additional space after first mod count strings
                int j = spaces / (list.Count - 1);//base spaces count between strings
                string str = string.Empty;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    str += list[i];
                    int k = j;
                    if (mod > 0 && i < mod)
                        k++;
                    while (k-- > 0)
                        str += " ";
                }
                str += list.Last();
                res.Add(str);
            }
            list.Clear();
        }

        /// 69. Sqrt(x), #Binary Search
        ///Given a non-negative integer x, compute and return the square root of x.
        ///Since the return type is an integer, the decimal digits are truncated, and only the integer part of the result is returned.
        ///0 <= x <= 2^31 - 1
        public int MySqrt(int x)
        {
            if (x <= 1) return x;
            long left = 0;
            long right = 50000;
            while (left < right)
            {
                long mid = (left + right+1) / 2;
                if (mid * mid == x) return (int)mid;
                else if (mid * mid > x) right = mid - 1;
                else left = mid;
            }
            return (int)left;
        }

        public int MySqrt_Math(int x)
        {
            long r = x;
            while (r * r > x)
                r = (r + x / r) / 2;
            return (int)r;
        }

        ///70. Climbing Stairs, #DP
        ///Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?
        ///1 <= n <= 45
        public int ClimbStairs_DP1(int n)
        {
            if (n == 1) return 1;
            if (n == 2) return 2;
            int prev = 1;
            int curr = 2;
            for (int i = 3; i <= n; i++)
            {
                int temp = prev + curr;
                prev = curr;
                curr = temp;
            }
            return curr;
        }

        public int ClimbStairs_DP2(int n)
        {
            if (n == 1) return 1;
            if (n == 2) return 2;
            int[] dp = new int[n + 1];
            dp[1] = 1;
            dp[2] = 2;
            for (int i = 3; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }
            return dp[n];
        }

        ///71. Simplify Path
        ///Given a string path, which is an absolute path (starting with a slash '/') to a file
        ///or directory in a Unix-style file system,convert it to the simplified canonical path.
        public string SimplifyPath(string path)
        {
            List<string> list = new List<string>();
            var paths = path.Split('/').Where(s => s.Length > 0 && s != ".").ToList();
            foreach (var dir in paths)
            {
                if (dir == "..")
                {
                    if (list.Count > 0)
                        list.RemoveAt(list.Count - 1);
                }
                else
                {
                    list.Add(dir);
                }
            }
            string ans = string.Empty;
            foreach (var dir in list) ans += "/" + dir;
            return ans == string.Empty ? "/" : ans;
        }

        /// 72. Edit Distance
        ///Given two strings word1 and word2, return the minimum number of operations required to convert word1 to word2.
        ///Insert/Delete/Replace a character
        public int MinDistance(string word1, string word2)
        {
            var rowLen = word1.Length;
            var colLen = word2.Length;
            int[,] dp = new int[rowLen + 1, colLen + 1];
            for (int i = 1; i <= rowLen; i++)
            {
                dp[i, 0] = i;
            }
            for (int j = 1; j <= colLen; j++)
            {
                dp[0, j] = j;
            }
            for (int i = 1; i <= rowLen; i++)
            {
                for (int j = 1; j <= colLen; j++)
                {
                    if (word1[i - 1] == word2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        //replace=dp[i - 1][j - 1]+1, delete = dp[i - 1][j])+1, insert = dp[i][j - 1]+1
                        dp[i, j] = Math.Min(dp[i - 1, j - 1], Math.Min(dp[i, j - 1], dp[i - 1, j])) + 1;
                    }
                }
            }
            return dp[rowLen, colLen];
        }

        /// 73. Set Matrix Zeroes, #DFS
        ///Given an m x n integer matrix matrix, if an element is 0, set its entire row and column to 0's.
        public void SetZeroes(int[][] matrix)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;
            bool[,] visit = new bool[rowLen, colLen];
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (visit[i, j]) continue;
                    if (matrix[i][j] == 0)
                    {
                        visit[i, j] = true;
                        for (int m = 0; m < colLen; m++)
                        {
                            if (!visit[i, m] && matrix[i][m] != 0)
                            {
                                matrix[i][m] = 0;
                                visit[i, m] = true;
                            }
                        }
                        for (int n = 0; n < rowLen; n++)
                        {
                            if (!visit[n, j] && matrix[n][j] != 0)
                            {
                                matrix[n][j] = 0;
                                visit[n, j] = true;
                            }
                        }
                    }
                }
            }
        }

        /// 74. Search a 2D Matrix, #Binary Search
        ///a value in an m x n matrix.
        ///Integers in each row are sorted from left to right.
        ///The first integer of each row is greater than the last integer of the previous row.
        public bool SearchMatrix_74_1D_BinarySearch_O_lgmn(int[][] matrix, int target)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;
            int left = 0;
            int right = colLen * rowLen - 1;
            while (left < right)
            {
                int mid = (left + right)/2;
                if (matrix[mid / colLen][mid % colLen] == target)
                    return true;
                else if (matrix[mid / colLen][mid % colLen] < target)
                    left = mid + 1;
                else
                    right = mid;
            }
            return matrix[left / colLen][left % colLen] == target;
        }
        //O(m+n)
        public bool SearchMatrix_TopRightToLeftBottom(int[][] matrix, int target)
        {
            if (target < matrix.First().First() || target > matrix.Last().Last())
                return false;
            int m = matrix.Length;
            int col = matrix[0].Length - 1;
            int row = 0;
            //serach form top-right
            while (col >= 0 && row <= m - 1)
            {
                if (target == matrix[row][col]) return true;
                else if (target < matrix[row][col]) col--;//move left
                else if (target > matrix[row][col]) row++;//move down
            }
            return false;
        }
        ///75. Sort Colors
        ///We will use the integers 0, 1, and 2 to represent the color red, white, and blue, respectively.
        /// sort as 0->1->2
        public void SortColors(int[] nums)
        {
            int i = 0;
            int red = 0;
            int white = 0;
            int blue = 0;
            int temp;
            while (i < nums.Length - blue)
            {
                if (nums[i] == 0)
                {
                    if (i != red)
                    {
                        temp = nums[red];
                        nums[red] = nums[i];
                        nums[i] = temp;
                    }
                    red++;
                    i++;
                }
                else if (nums[i] == 1)
                {
                    i++;
                    white++;
                }
                else
                {
                    temp = nums[nums.Length - 1 - blue];
                    nums[nums.Length - 1 - blue] = nums[i];
                    nums[i] = temp;
                    blue++;
                }
            }
        }

        ///76. Minimum Window Substring
        ///Given two strings s and t of lengths m and n respectively
        ///return "" or the minimum window substring of s such that every character in t (including duplicates) is included in the window.
        public string MinWindow(string s, string t)
        {
            if (s.Length < t.Length) return String.Empty;

            int[] arrT = new int[26 + 32];//'a'-'A'=32
            int[] arrS = new int[26 + 32];
            for (int i = 0; i < t.Length; i++)
            {
                arrT[t[i] - 'A']++;
                arrS[s[i] - 'A']++;
            }

            int len = t.Length;
            int min = s.Length + 1;
            string ans = string.Empty;
            for (int i = t.Length - 1; i < s.Length; i++)
            {
                if (i == t.Length - 1)
                {
                }
                else
                {
                    arrS[s[i] - 'A']++;
                    len++;

                    int j = i - len + 1;
                    while (j <= i)
                    {
                        if (arrS[s[j] - 'A'] > 0 && arrS[s[j] - 'A'] > arrT[s[j] - 'A'])
                        {
                            arrS[s[j] - 'A']--;
                            len--;
                        }
                        else
                        {
                            break;
                        }
                        j++;
                    }
                }

                bool failed = false;
                for (int k = 0; k < arrT.Length; k++)
                {
                    if (arrT[k] > 0 && arrT[k] > arrS[k])
                    {
                        failed = true;
                        break;
                    }
                }

                if (!failed)
                {
                    if (len < min)
                    {
                        min = len;
                        ans = s.Substring(i - len + 1, len);
                        if (len == t.Length)
                            return ans;
                    }
                }
            }
            return min > s.Length ? string.Empty : ans;
        }

        /// 77. Combinations, #Backtracking
        //Given two integers n and k, return all possible combinations of k numbers out of the range [1, n].
        //1 <= n <= 20, 1 <= k <= n
        public IList<IList<int>> Combine(int n, int k)
        {
            var res = new List<IList<int>>();
            Combine(n, 1, k, new List<int>(), res);
            return res;
        }

        private void Combine(int n, int curr, int k, List<int> list, IList<IList<int>> res)
        {
            if (list.Count == k)
            {
                res.Add(list);
                return;
            }
            if (curr > n) return;
            Combine(n, curr + 1, k, list, res);
            Combine(n, curr + 1, k, new List<int>(list) { curr}, res);
        }

        ///78. Subsets - Unique nums, #Backtracking
        ///Given an integer array nums of unique elements, return all possible subsets (the power set).
        public IList<IList<int>> Subsets(int[] nums)
        {
            var ans = new List<IList<int>>();
            var list = new List<int>();
            SubSets_Backtracking(nums, 0, list, ans);
            return ans;
        }

        public void SubSets_Backtracking(int[] nums, int start, IList<int> list, IList<IList<int>> ans)
        {
            if (start == nums.Length)
            {
                ans.Add(list);
                return;
            }
            SubSets_Backtracking(nums, start + 1, new List<int>(list), ans);
            SubSets_Backtracking(nums, start + 1, new List<int>(list) { nums[start] }, ans);
        }

        ///79. Word Search, #Backtracking, #DFS
        ///Given an m x n grid of characters board and a string word, return true if word exists in the grid.
        public bool Exist(char[][] board, string word)
        {
            return Exist_Backtracking(board, null, 0, 0, word, 0);
        }

        public bool Exist_Backtracking(char[][] board, bool[] visit, int r, int c, string word, int index)
        {
            if (index == word.Length)
                return true;
            var rowLen = board.Length;
            var colLen = board[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };

            if (index == 0)
            {
                for (int i = 0; i < rowLen; i++)
                {
                    for (var j = 0; j < colLen; j++)
                    {
                        if (board[i][j] == word[index])
                        {
                            var nextVisit = new bool[rowLen * colLen];
                            nextVisit[i * colLen + j] = true;
                            bool result = Exist_Backtracking(board, nextVisit, i, j, word, index + 1);
                            if (result)
                                return true;
                        }
                    }
                }
            }
            else
            {
                foreach (var t in dxy4)
                {
                    int row = r + t[0];
                    int col = c + t[1];
                    if (row >= 0 && row < rowLen && col >= 0 && col < colLen
                        && !visit[row * colLen + col] && board[row][col] == word[index])
                    {
                        var nextVisit = new List<bool>(visit).ToArray();
                        nextVisit[row * colLen + col] = true;
                        bool result = Exist_Backtracking(board, nextVisit, row, col, word, index + 1);
                        if (result)
                            return true;
                    }
                }
            }
            return false;
        }

        /// 80. Remove Duplicates from Sorted Array II
        /// nums sorted in non-decreasing order, remove the duplicates in-place such that each unique element appears only once.
        /// -10^4 <= nums[i] <= 10^4
        public int RemoveDuplicates(int[] nums)
        {
            int ship = 10000;
            int repeat = 2;
            int[] arr = new int[ship * 2 + 1];
            foreach (var i in nums)
                arr[i + ship]++;
            int count = 0;
            int skipCount = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (count + skipCount == arr.Length)
                    break;
                if (arr[i] == 0)
                    continue;
                skipCount += arr[i] - repeat;
                int j = arr[i] > repeat ? repeat : arr[i];
                while (j > 0)
                {
                    nums[count] = i - ship;
                    count++;
                    j--;
                }
            }
            return count;
        }

        ///81. Search in Rotated Sorted Array II
        ///There is an integer array nums sorted in non-decreasing order (not necessarily with distinct values).
        ///Given the array nums after the rotation, return true if target is in nums, or false if it is not in nums.

        public bool Search_81(int[] nums, int target)
        {
            int i = 0;
            while (i <= nums.Length - 1)
            {
                if (nums[i] == target)
                    return true;
                if (nums[i] < nums[0])
                {
                    //rotated
                    if (target > nums[0]) { return false; }
                    if (target < nums[i]) { return false; }
                }
                i++;
            }
            return false;
        }

        /// 82. Remove Duplicates from Sorted List II
        /// remove all duplicates, [1,1,1,2,2,3]=>[3], [1,2,2,3]=>[1,3]
        public ListNode DeleteDuplicates(ListNode head)
        {
            ListNode target = null;
            ListNode last = null;
            ListNode node = head;
            while (node != null && node.next != null)
            {
                if (node.val == node.next.val)
                {
                    if (node == head)
                    {
                        target = node;
                        while (node != null && node.val == target.val)
                            node = node.next;
                        head = node;
                        last = null;
                    }
                    else
                    {
                        target = node;
                        while (node != null && node.val == target.val)
                            node = node.next;
                        last.next = node;
                    }
                }
                else
                {
                    last = node;
                    node = node.next;
                }
            }
            return head;
        }

        /// 83. Remove Duplicates from Sorted List

        public ListNode DeleteDuplicates_83(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            var queue = new Queue<ListNode>();
            int last = int.MinValue;

            while (head != null)
            {
                if (last == head.val)
                {
                    head = head.next;
                }
                else
                {
                    last = head.val;
                    var next = head.next;
                    head.next = null;
                    queue.Enqueue(head);
                    head = next;
                }
            }

            ListNode result = null;
            ListNode current = null;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (result == null)
                {
                    result = node;
                    current = result;
                }
                else
                {
                    current.next = node;
                    current = node;
                }
            }

            return result;
        }

        ///84. Largest Rectangle in Histogram, #Monotonic
        ///heights representing the histogram's bar height where the width of each bar is 1,
        ///return the area of the largest rectangle in the histogram.
        /// 0<= height <=10000, heights.Length>=1
        public int LargestRectangleArea_My(int[] heights)
        {
            int n = heights.Length;
            int[] lessFromLeft = new int[n]; // idx of the first bar the left that is lower than current
            int[] lessFromRight = new int[n]; // idx of the first bar the right that is lower than current

            var stack = new Stack<int>();//stack only store strictly increase elements
            for (int i = 0; i < n; i++)
            {
                while (stack.Count > 0 && heights[stack.Peek()] >= heights[i])
                    stack.Pop();

                lessFromLeft[i] = stack.Count > 0 ? stack.Peek() : -1;
                stack.Push(i);
            }

            stack.Clear();
            for (int i = n - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && heights[stack.Peek()] >= heights[i])
                    stack.Pop();

                lessFromRight[i] = stack.Count > 0 ? stack.Peek() : n;
                stack.Push(i);
            }
            int maxArea = 0;
            for (int i = 0; i < n; i++)
            {
                //if lessFromLeft[i]==-1, start from 0; if lessFromRight[i]=n, end with n-1
                // width is range [lessFromLeft[i]+1,lessFromRight[i]-1]
                maxArea = Math.Max(maxArea, heights[i] * (lessFromRight[i] - 1 - (lessFromLeft[i] + 1) + 1));
            }
            return maxArea;
        }

        public int LargestRectangleArea(int[] heights)
        {
            int n = heights.Length;
            int[] lessFromLeft = new int[n]; // idx of the first bar the left that is lower than current
            int[] lessFromRight = new int[n]; // idx of the first bar the right that is lower than current
            lessFromRight[n - 1] = n;
            lessFromLeft[0] = -1;

            for (int i = 1; i < heights.Length; i++)
            {
                int prev = i - 1;
                while (prev >= 0 && heights[prev] >= heights[i])
                {
                    //prev--;time out
                    prev = lessFromLeft[prev];//this will save time
                }
                lessFromLeft[i] = prev;
            }

            for (int i = heights.Length - 2; i >= 0; i--)
            {
                int next = i + 1;

                while (next < heights.Length && heights[next] >= heights[i])
                {
                    //next++;time out
                    next = lessFromRight[next];//this will save time
                }
                lessFromRight[i] = next;
            }

            int maxArea = 0;
            for (int i = 0; i < heights.Length; i++)
            {
                //if lessFromLeft[i]==-1, start from 0; if lessFromRight[i]=n, end with n-1
                // width is range [lessFromLeft[i]+1,lessFromRight[i]-1]
                maxArea = Math.Max(maxArea, heights[i] * (lessFromRight[i]-1 - (lessFromLeft[i]+1) + 1));
            }

            return maxArea;
        }

        public int LargestRectangleArea_HardToLearn(int[] heights)
        {
            int n = heights.Length;
            int max = 0;
            var stack = new Stack<int>();
            for (int i = 0; i <= n; i++)
            {
                int height = (i == n) ? 0 : heights[i];
                // stack is only store ascending heights
                //if current height[i] is small , calculate max of top, then discard top
                while (stack.Count>0 && height < heights[stack.Peek()])
                {
                    int currHeight = heights[stack.Pop()];
                    //width is [top+1, i-1]
                    int width = (stack.Count==0) ? i : i - 1 - stack.Peek();
                    max = Math.Max(max, currHeight * width);
                }
                stack.Push(i);
            }
            return max;
        }

        ///86. Partition List
        ///all nodes less than x come before nodes greater than or equal to x.
        public ListNode Partition(ListNode head, int x)
        {
            if (head == null) return null;
            ListNode head1 = null;
            ListNode tail1 = null;
            ListNode head2 = null;
            ListNode tail2 = null;

            var node = head;
            while (node != null)
            {
                if (node.val < x)
                {
                    if (head1 == null)
                    {
                        head1 = node;
                        tail1 = node;
                    }
                    else
                    {
                        tail1.next = node;
                        tail1 = tail1.next;
                    }
                    if (tail2 != null) tail2.next = null;
                }
                else
                {
                    if (head2 == null)
                    {
                        head2 = node;
                        tail2 = node;
                    }
                    else
                    {
                        tail2.next = node;
                        tail2 = tail2.next;
                    }
                    if (tail1 != null) tail1.next = null;
                }
                node = node.next;
            }

            if (tail1 != null) tail1.next = head2;
            return head1 ?? head2;
        }

        ///87. Scramble String, #DP
        ///Given two strings s1 and s2 of the same length, return true if s2 is a scrambled string of s1, otherwise, return false.
        public bool IsScramble(string s1, string s2)
        {
            // Let F(i, j, k) = whether the substring S1[i..i + k - 1] is a scramble of S2[j..j + k - 1] or not
            // Since each of these substrings is a potential node in the tree, we need to check for all possible cuts.
            // Let q be the length of a cut (hence, q < k), then we are in the following situation:
            //
            // S1 [   x1    |         x2         ]
            //    i         i + q                i + k - 1

            // here we have two possibilities:
            // S2 [   y1    |         y2         ]
            //    j         j + q                j + k - 1

            // or

            // S2 [       y1        |     y2     ]
            //    j                 j + k - q    j + k - 1

            // which in terms of F means:
            // F(i, j, k) = for some 1 <= q < k we have:
            //  (F(i, j, q) AND F(i + q, j + q, k - q)) OR (F(i, j + k - q, q) AND F(i + q, j, k - q))
            // Base case is k = 1, where we simply need to check for S1[i] and S2[j] to be equal
            int len = s1.Length;
            bool[,,] dp = new bool[len, len, len + 1];
            for (int k = 1; k <= len; ++k)
            {
                for (int i = 0; i + k <= len; ++i)
                {
                    for (int j = 0; j + k <= len; ++j)
                    {
                        if (k == 1)
                            dp[i, j, k] = s1[i] == s2[j];
                        else
                        {
                            for (int q = 1; q < k && !dp[i, j, k]; ++q)
                            {
                                dp[i, j, k] = (dp[i, j, q] && dp[i + q, j + q, k - q]) || (dp[i, j + k - q, q] && dp[i + q, j, k - q]);
                            }
                        }
                    }
                }
            }
            return dp[0, 0, len];
        }

        /// 88. Merge Sorted Array, #Two Pointers
        /// Merge two sorted array nums1 and nums2, into a single array sorted in non-decreasing order.
        /// nums1.Length = m + n, nums2.Length == n , 0 <= m, n <= 200, 1 <= m + n <= 200
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int[] temp = new int[m];
            Array.Copy(nums1, temp, m);

            int i = 0, j = 0;
            while (i < m || j < n)
            {
                if (i == m) nums1[i + j] = nums2[j++];
                else if (j == n) nums1[i + j] = temp[i++];
                else
                {
                    if (temp[i] <= nums2[j]) nums1[i + j] = temp[i++];
                    else nums1[i + j] = nums2[j++];
                }
            }
        }

        ///89. Gray Code. #Backtracking
        ///An n-bit gray code sequence is a sequence of 2n integers inclusive range [0, 2n - 1],
        ///The first integer is 0, An integer appears no more than once in the sequence,
        ///The binary representation of every pair of adjacent integers differs by exactly one bit
        ///The binary representation of the first and last integers differs by exactly one bit.
        ///Given an integer n, return any valid n-bit gray code sequence.
        public IList<int> GrayCode(int n)
        {
            var ans = new List<int>();
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            int i = 2;
            for (; i < Math.Pow(2, n); i++)
            {
                var code = GrayCode_Code(i);
                if (!dict.ContainsKey(code)) dict.Add(code, new List<int>());
                dict[code].Add(i);
            }

            ans.Add(0);
            ans.Add(1);
            int lastCode = 0;
            while (i > 2)
            {
                int upCode = lastCode + 1;
                int downCode = lastCode - 1;
                if (dict.ContainsKey(upCode))
                {
                    ans.Insert(ans.Count - 1, dict[upCode][0]);
                    dict[upCode].RemoveAt(0);
                    if (dict[upCode].Count == 0) dict.Remove(upCode);
                    lastCode = upCode;
                }
                else if (dict.ContainsKey(downCode))
                {
                    ans.Insert(ans.Count - 1, dict[downCode][0]);
                    dict[downCode].RemoveAt(0);
                    if (dict[downCode].Count == 0) dict.Remove(downCode);
                    lastCode = downCode;
                }
                i--;
            }
            return ans;
        }

        public int GrayCode_Code(int val)
        {
            int ans = 0;
            while (val > 0)
            {
                if ((val & 1) == 1) ans++;
                val >>= 1;
            }
            return ans;
        }

        /// 90. Subsets II
        ///Given an integer array nums that may contain duplicates, return all possible subsets (the power set).
        ///The solution set must not contain duplicate subsets. Return the solution in any order.

        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            var ans = new List<IList<int>>();

            Array.Sort(nums);

            ans.Add(new List<int>());

            for (int n = 1; n <= nums.Length - 1; n++)
            {
                var llist = new List<IList<int>>();

                SubsetsWithDup_Add(nums, 0, n, llist, ans);
            }

            ans.Add(nums);

            return ans;
        }

        public void SubsetsWithDup_Add(int[] nums, int start, int number, IList<IList<int>> llist, IList<IList<int>> ans)
        {
            if (start >= nums.Length)
                return;

            llist = llist.Where(o => o.Count < number && o.Count + (nums.Length - start) >= number).ToList();

            var subs = new List<IList<int>>();
            foreach (var list in llist)
            {
                var sub = new List<int>(list)
                {
                    nums[start]
                };

                subs.Add(sub);
            }
            foreach (var sub in subs)
            {
                llist.Add(sub);
            }

            llist.Add(new List<int>() { nums[start] });

            var targets = llist.Where(o => o.Count == number).ToList();

            for (int i = 0; i < targets.Count; i++)
            {
                if (ans.FirstOrDefault(x => x.SequenceEqual(targets[i])) == null)
                    ans.Add(targets[i]);
            }

            SubsetsWithDup_Add(nums, start + 1, number, llist, ans);
        }

        /// 91. Decode Ways, #DP
        ///A message containing letters from A-Z can be encoded into numbers using the following mapping:
        ///'A' -> "1", Z->26, 1 <= s.Length <= 100
        ///"AAJF" with the grouping (1 1 10 6)
        ///"KJF" with the grouping(11 10 6)
        public int NumDecodings(string s)
        {
            if (s[0] == '0')
            {
                return 0;
            }

            int[] waysToDecode = new int[s.Length + 1];
            waysToDecode[0] = 1;
            waysToDecode[1] = 1;
            for (int i = 1; i < s.Length; i++)
            {
                int curr = s[i] - '0';
                int prev = s[i - 1] - '0';

                // can't make progress, return 0
                if (prev == 0 && curr == 0 || (curr == 0 && (prev * 10 + curr > 26)))
                {
                    return 0;
                }
                // can't use the previous value, so can only get to this state from the previous
                else if (prev == 0 || (prev * 10 + curr) > 26)
                {
                    waysToDecode[i + 1] = waysToDecode[i];
                }
                // can't use current state, can only get to this state from i - 1 state
                else if (curr == 0)
                {
                    waysToDecode[i + 1] = waysToDecode[i - 1];
                }
                // can get to this state from the previous two states
                else
                {
                    waysToDecode[i + 1] = waysToDecode[i] + waysToDecode[i - 1];
                }
            }

            return waysToDecode[waysToDecode.Length - 1];
        }

        ///92. Reverse Linked List II
        ///Given the head of a singly linked list and two integers left and right where left <= right,
        ///reverse the nodes of the list from position left to position right, and return the reversed list.

        public ListNode ReverseBetween(ListNode head, int left, int right)
        {
            ListNode ans = null;
            ListNode tail = null;
            ListNode prev = null;

            var node = head;
            int len = 0;
            List<ListNode> list = new List<ListNode>();
            while (node != null)
            {
                len++;
                if (len == left)
                {
                    if (len == 1)
                    {
                        ans = null;
                        tail = null;
                    }
                    else
                    {
                        ans = head;
                        tail = prev;
                    }
                }

                if (len >= left)
                {
                    list.Add(node);
                }

                if (len == right)
                {
                    var next = node.next;

                    for (int i = list.Count - 1; i >= 1; i--)
                    {
                        list[i].next = list[i - 1];
                    }
                    list[0].next = next;
                    if (tail == null)
                    {
                        ans = list.Last();
                    }
                    else
                    {
                        //ans = head;
                        tail.next = list.Last();
                    }
                    break;
                }
                prev = node;
                node = node.next;
            }

            return ans;
        }

        ///93. Restore IP Addresses
        ///A valid IP address consists of exactly four integers separated by single dots.
        ///Each integer is between 0 and 255 (inclusive) and cannot have leading zeros.
        ///For example, "0.1.2.201" and "192.168.1.1" are valid IP addresses, but "0.011.255.245", "192.168.1.312" and "192.168@1.1" are invalid IP addresses.
        ///Given a string s containing only digits, return all possible valid IP addresses that can be formed by inserting dots into s.
        ///You are not allowed to reorder or remove any digits in s.You may return the valid IP addresses in any order.
        ///1 <= s.length <= 20,s consists of digits only.
        public IList<string> RestoreIpAddresses(string s)
        {
            var res = new List<string>();
            if (s.Length > 12 || s.Length < 4) return res;
            for (int i = 0; i < s.Length - 3 && i <= 2; i++)
            {
                if (i > 0 && s[0] == '0') continue;
                for (int j = i + 1; j < s.Length - 2 && j <= i + 3; j++)
                {
                    if (j > i + 1 && s[i + 1] == '0') continue;
                    for (int k = j + 1; k < s.Length - 1 && k <= j + 3; k++)
                    {
                        if (k > j + 1 && s[j + 1] == '0') continue;
                        if (k + 1 < s.Length - 1 && s[k + 1] == '0') continue;
                        if (s.Length - 1 - k > 3) continue;

                        int ip0 = int.Parse(s.Substring(0, i + 1));
                        if (ip0 >= 256) continue;
                        int ip1 = int.Parse(s.Substring(i + 1, j - i));
                        if (ip1 >= 256) continue;
                        int ip2 = int.Parse(s.Substring(j + 1, k - j));
                        if (ip2 >= 256) continue;
                        int ip3 = int.Parse(s.Substring(k + 1));
                        if (ip3 >= 256) continue;
                        res.Add($"{ip0}.{ip1}.{ip2}.{ip3}");
                    }
                }
            }
            return res;
        }

        /// 94. Binary Tree Inorder Traversal, #BTree
        /// Left->Node->Right
        public IList<int> InorderTraversal_Iteration(TreeNode root)
        {
            List<int> res = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode node = root;
            while (node != null || stack.Any())
            {
                if (node != null)
                {
                    if (node.left == null)
                    {
                        res.Add(node.val);
                        node = node.right;
                    }
                    else
                    {
                        stack.Push(node);
                        node = node.left;
                    }
                }
                else
                {
                    var top = stack.Pop();
                    res.Add(top.val);
                    node = top.right;
                }
            }
            return res;
        }

        public IList<int> InorderTraversal_Recursion(TreeNode root)
        {
            var result = new List<int>();
            InorderTraversal_Recursion(root, result);
            return result;
        }

        public void InorderTraversal_Recursion(TreeNode node, IList<int> list)
        {
            if (node == null) return;
            InorderTraversal_Recursion(node.left, list);
            list.Add(node.val);
            InorderTraversal_Recursion(node.right, list);
        }

        ///95. Unique Binary Search Trees II, #BTree
        ///Given an integer n, return all the structurally unique BST's (binary search trees),
        ///which has exactly n nodes of unique values from 1 to n. Return the answer in any order.
        ///1 <= n <= 8
        public IList<TreeNode> GenerateTrees(int n)
        {
            return GenerateTrees_Recursion(1,n);
        }

        private List<TreeNode> GenerateTrees_Recursion(int s, int e)
        {
            List<TreeNode> res = new List<TreeNode>();
            if (s > e)
            {
                res.Add(null); // empty tree
                return res;
            }
            for (int i = s; i <= e; i++)
            {
                List<TreeNode> leftSubtrees = GenerateTrees_Recursion(s, i - 1);
                List<TreeNode> rightSubtrees = GenerateTrees_Recursion(i + 1, e);
                foreach (TreeNode left in leftSubtrees)
                {
                    foreach (TreeNode right in rightSubtrees)
                    {
                        TreeNode root = new TreeNode(i);
                        root.left = left;
                        root.right = right;
                        res.Add(root);
                    }
                }
            }
            return res;
        }

        /// 96. Unique Binary Search Trees , #BTree, #DP
        /// Given an integer n, return the number of structurally unique BST's (binary search trees)
        /// which has exactly n nodes of unique values from 1 to n.
        public int NumTrees(int n)
        {
            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    //Just treat each number as root, and then left part * right part is the answer.
                    dp[i] += dp[i - j] * dp[j - 1];
                }
            }
            return dp[n];
        }

        ///97. Interleaving String, #DP
        ///Given strings s1, s2, and s3, find whether s3 is formed by an interleaving of s1 and s2.
        ///An interleaving of two strings s and t is a configuration where they are divided into non-empty substrings
        public bool IsInterleave(string s1, string s2, string s3)
        {
            if ((s1 == null || s1.Length == 0) && (s2 == null || s2.Length == 0))
            {
                return s3 == null || s3.Length == 0;
            }
            if (s1 == null || s1.Length == 0) { return s2 == (s3); }
            if (s2 == null || s2.Length == 0) { return s1 == (s3); }
            if (s1.Length + s2.Length != s3.Length) { return false; }

            bool[,] dp = new bool[s1.Length + 1, s2.Length + 1];// would be false by default.
            dp[0, 0] = true;

            for (int i = 1; i <= s1.Length; i++)
            {
                if (dp[i - 1, 0] && s1[i - 1] == s3[i - 1])
                {
                    dp[i, 0] = true;
                }
            }
            for (int j = 1; j <= s2.Length; j++)
            {
                if (dp[0, j - 1] && s2[j - 1] == s3[j - 1])
                {
                    dp[0, j] = true;
                }
            }
            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    if (dp[i - 1, j] && s1[i - 1] == s3[i + j - 1])
                    {
                        dp[i, j] = true;
                    }
                    else if (dp[i, j - 1] && s2[j - 1] == s3[i + j - 1])
                    {
                        dp[i, j] = true;
                    }
                }
            }
            return dp[s1.Length, s2.Length];
        }

        /// 98. Validate Binary Search Tree, #BTree
        /// left.val<=Node.Val<=right.val
        public bool IsValidBST(TreeNode root)
        {
            return IsValidBST_Recursion(root);
        }

        private bool IsValidBST_Recursion(TreeNode root, TreeNode left = null, TreeNode right = null)
        {
            if (root == null)
                return true;
            if ((left != null && root.val <= left.val)
                || (right != null && root.val >= right.val))
                return false;
            return IsValidBST_Recursion(root.left, left, root) && IsValidBST_Recursion(root.right, root, right);
        }

        /// 99. Recover Binary Search Tree, #BTree
        ///BST where the values of exactly two nodes of the tree were swapped by mistake.
        ///Recover the tree without changing its structure.
        public void RecoverTree(TreeNode root)
        {
            //not pass
            Dictionary<int, TreeNode> dict = new Dictionary<int, TreeNode>();
            bool found = false;
            RecoverTree_Recursion(root, dict, ref found, null, null);
        }

        public void RecoverTree_Recursion(TreeNode root, Dictionary<int, TreeNode> dict, ref bool found, TreeNode left = null, TreeNode right = null)
        {
            if (found) return;
            if (root == null) return;
            dict.Add(root.val, root);

            if ((left != null && root.val <= left.val))
            {
                int temp = root.val;
                dict[root.val].val = left.val;
                dict[left.val].val = temp;
                found = true;
                return;
            }
            if ((right != null && root.val >= right.val))
            {
                int temp = root.val;
                dict[root.val].val = right.val;
                dict[right.val].val = temp;
                found = true;
                return;
            }
            RecoverTree_Recursion(root.left, dict, ref found, left, root);
            RecoverTree_Recursion(root.right, dict, ref found, root, right);
        }
    }
}