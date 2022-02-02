using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        /// 53. Maximum Subarray
        /// find the contiguous subarray (containing at least one number) which has the largest sum and return its sum.
        public int MaxSubArray(int[] nums)
        {
            int sum = 0;
            int max = nums.Max();

            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (sum <= 0)
                {
                    sum = 0;
                }
                else
                {
                    max = Math.Max(max, sum);
                }
            }

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

        ///55. Jump Game
        ///start at 0-index,nums[i] maximum jump length at that position.
        //Return true if you can reach the last index, or false otherwise.
        public bool CanJump(int[] nums)
        {
            if (nums.Length == 1)
                return true;
            if (nums.Length == 2)
                return nums[0] > 0;
            bool[] dp = new bool[nums.Length];
            int i = nums.Length - 1;
            dp[i] = true;
            i--;
            while (i >= 0)
            {
                if (nums[i] == 0)
                {
                    dp[i] = false;
                }
                else
                {
                    bool has = false;
                    for (int j = 1; j <= nums[i]; j++)
                    {
                        if (i + j <= nums.Length - 1 && dp[i + j])
                        {
                            has = true;
                            break;
                        }
                    }
                    dp[i] = has;
                }
                i--;
            }
            return dp[0];
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

            //must convert to list, or exceed time limit
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

        public IList<Interval> Merge(IList<Interval> intervals)
        {
            if (intervals == null || intervals.Count <= 1) return intervals;

            IList<Interval> result = new List<Interval>();

            for (int i = 0; i < intervals.Count; i++)
            {
                Interval current = intervals[i];
                result.Add(current);
                result = TrimIntervalFromEnd(result);
            }
            return result;
        }

        public IList<Interval> TrimIntervalFromEnd(IList<Interval> list)
        {
            if (list == null || list.Count <= 1) return list;
            while (list.Count > 1)
            {
                Interval current = list[list.Count - 1];
                Interval pre = list[list.Count - 2];
                if (pre.end < current.start)
                {
                    break;
                }
                else
                {
                    if (pre.start <= current.start)
                    {
                        pre.start = Math.Min(pre.start, current.start);
                        pre.end = Math.Max(pre.end, current.end);
                        list.Remove(current);
                    }
                    else
                    {
                        SwapIntervalNode(ref pre, ref current);
                        list.Remove(current);
                        list = TrimIntervalFromEnd(list);
                        list.Add(current);
                    }
                }
            }

            return list;
        }

        public void SwapIntervalNode(ref Interval a, ref Interval b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public Interval MergeIntervalNodes(Interval current, Interval next)
        {
            return new Interval(Math.Min(current.start, next.start), Math.Max(current.end, next.end));
        }

        //57 not pass
        public IList<Interval> Insert(IList<Interval> intervals, Interval newInterval)
        {
            return intervals;
        }

        ///58. Length of Last Word
        public int LengthOfLastWord(string s)
        {
            int ans = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == ' ')
                {
                    if (ans == 0)
                        continue;
                    else
                        break;
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

        ///62. Unique Paths
        ///Move from grid[0][0] to grid[m - 1][n - 1], each step can only move down or right.
        ///A(m-1+n-1)/A(m-1)/A(n-1)
        public int UniquePaths(int m, int n)
        {
            if (m == 1 || n == 1)
                return 1;
            int all = m - 1 + n - 1;
            long ans = 1;
            int j = 1;
            int x = 2;
            int y = 2;
            while (j <= all)
            {
                ans *= j;
                j++;
                if (x <= m - 1 && ans % x == 0)
                {
                    ans /= x;
                    x++;
                }
                if (y <= n - 1 && ans % y == 0)
                {
                    ans /= y;
                    y++;
                }
            }
            return (int)ans;
        }

        ///63. Unique Paths II
        ///An obstacle and space is marked as 1 and 0 respectively in the grid.
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int rLen = obstacleGrid.Length;
            int cLen = obstacleGrid[0].Length;

            if (obstacleGrid[0][0] == 1
                || obstacleGrid[rLen - 1][cLen - 1] == 1)
                return 0;

            if (rLen == 1 && cLen == 1)
            {
                return obstacleGrid[0][0] == 0 ? 1 : 0;
            }
            int[][] dp = new int[rLen][];
            for (int i = 0; i < rLen; i++)
            {
                dp[i] = new int[cLen];
            }

            dp[0][0] = 1;

            for (int i = 0; i < rLen; i++)
            {
                for (int j = 0; j < cLen; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            //
                        }
                        else
                        {
                            if (obstacleGrid[i][j] == 1)
                            {
                                dp[i][j] = 0;

                            }
                            else
                            {
                                dp[i][j] = dp[i][j - 1];
                            }
                        }

                    }
                    else
                    {
                        if (obstacleGrid[i][j] == 1)
                        {
                            dp[i][j] = 0;
                        }
                        else
                        {
                            dp[i][j] += dp[i - 1][j];

                            if (j > 0)
                            {
                                dp[i][j] += dp[i][j - 1];
                            }
                        }
                    }
                }
            }


            return dp[rLen - 1][cLen - 1];
        }
        ///64. Minimum Path Sum
        ///Given a m x n grid filled with non-negative numbers,
        ///find a path from top left to bottom right,
        ///which minimizes the sum of all numbers along its path.
        ///Note: You can only move either down or right at any point in time.
        public int MinPathSum(int[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;
            var memo = new int[m + 1, n + 1];
            for (int i = 2; i <= m; i++)
                memo[i, 0] = int.MaxValue;
            for (int i = 0; i <= n; i++)
                memo[0, i] = int.MaxValue;
            memo[1, 0] = 0;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    memo[i, j] = Math.Min(memo[i - 1, j], memo[i, j - 1]) + grid[i - 1][j - 1];
                }
            }
            return memo[m, n];
        }

        public int MinPathSum_MyDp_Ugly(int[][] grid)
        {
            int i = 0;
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            List<int> dp = new List<int>();

            //r+c-1 times, everytime calculate all possible path dp values
            int count = rowLen - 1 + colLen;
            while (i < count)
            {
                if (i == 0)
                {
                    dp.Add(grid[0][0]);
                }
                else
                {
                    List<int> dp2 = new List<int>();

                    int dpLen = 0;
                    int startCol = 0;
                    int startRow = 0;

                    if (rowLen == colLen)
                    {
                        if (i < rowLen)
                        {
                            dpLen = i + 1;
                            startCol = i;
                            startRow = 0;
                        }
                        else
                        {
                            dpLen = rowLen - (i - (rowLen - 1));
                            startCol = (colLen - 1);
                            startRow = i - (rowLen - 1);
                        }
                    }
                    else if (rowLen > colLen)
                    {
                        if (i < colLen)
                        {
                            dpLen = i + 1;
                            startCol = i;
                            startRow = 0;
                        }
                        else if (i < rowLen)
                        {
                            dpLen = colLen;
                            startCol = colLen - 1;
                            startRow = i - (colLen - 1);
                        }
                        else
                        {
                            dpLen = colLen - (i - (rowLen - 1));
                            startCol = colLen - 1;
                            startRow = i - (colLen - 1);
                        }
                    }
                    else//rowLen<colLen
                    {
                        if (i < rowLen)
                        {
                            dpLen = i + 1;
                            startCol = i;
                            startRow = 0;
                        }
                        else if (i < colLen)
                        {
                            dpLen = rowLen;
                            startCol = i;
                            startRow = 0;
                        }
                        else
                        {
                            dpLen = rowLen - (i - (colLen - 1));
                            startCol = colLen - 1;
                            startRow = i - (colLen - 1);
                        }
                    }

                    int j = 0;
                    while (j < dpLen)
                    {
                        int y = startCol - j;
                        int x = startRow + j;

                        int a = 0;

                        if (rowLen == colLen)
                        {
                            if (x == 0)
                            {
                                //first one
                                a = grid[x][y] + dp[j];
                            }
                            else if (y == 0)
                            {
                                //last one
                                a = grid[x][y] + dp[j - 1];
                            }
                            else
                            {
                                if (i < rowLen)
                                {
                                    a = Math.Min(grid[x][y] + dp[j - 1], grid[x][y] + dp[j]);
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j + 1], grid[x][y] + dp[j]);
                                }
                            }
                        }
                        else if (rowLen > colLen)
                        {
                            if (dpLen > dp.Count)
                            {
                                if (x == 0)
                                {
                                    //first one, from up
                                    a = grid[x][y] + dp[j];
                                }
                                else if (y == 0)
                                {
                                    //last one, from left
                                    a = grid[x][y] + dp[j - 1];
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j - 1], grid[x][y] + dp[j]);
                                }
                            }
                            else if (dpLen == dp.Count)
                            {
                                if (y == 0)
                                {
                                    //last one, from left
                                    a = grid[x][y] + dp[j];
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j + 1], grid[x][y] + dp[j]);
                                }
                            }
                            else
                            {
                                a = Math.Min(grid[x][y] + dp[j + 1], grid[x][y] + dp[j]);
                            }
                        }
                        else
                        {
                            if (dpLen > dp.Count)
                            {
                                if (x == 0)
                                {
                                    a = grid[x][y] + dp[j];
                                }
                                else if (y == 0)
                                {
                                    a = grid[x][y] + dp[j - 1];
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j - 1], grid[x][y] + dp[j]);
                                }
                            }
                            else if (dpLen == dp.Count)
                            {
                                if (x == 0)
                                {
                                    a = grid[x][y] + dp[j];
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j - 1], grid[x][y] + dp[j]);
                                }
                            }
                            else
                            {
                                a = Math.Min(grid[x][y] + dp[j + 1], grid[x][y] + dp[j]);
                            }
                        }

                        dp2.Add(a);
                        j++;
                    }
                    dp = dp2;
                }

                i++;
            }

            return dp[0];
        }

        ///Recursion will time out
        public int MinPathSum_Recursion(int[][] grid, int r, int c)
        {
            if (r == 0 && c == 0)
                return grid[0][0];

            if (r <= 0)
                return grid[r][c] + MinPathSum_Recursion(grid, 0, c - 1);

            if (c <= 0)
                return grid[r][c] + MinPathSum_Recursion(grid, r - 1, 0);

            return Math.Min(grid[r][c] + MinPathSum_Recursion(grid, r, c - 1), grid[r][c] + MinPathSum_Recursion(grid, r - 1, c));
        }

        /// 65
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
        /// 0-index is the highest bit
        ///Input: digits = [1,2,3]
        ///Output: [1,2,4]

        public int[] PlusOne(int[] digits)
        {
            if (digits == null || digits.Length == 0)
                return new int[] { 1 };

            int i = digits.Length - 1;
            while (i >= 0)
            {
                if (digits[i] != 9)
                {
                    digits[i]++;
                    return digits;
                }
                else
                {
                    if (0 == i)
                    {
                        digits[i]++;
                        break;
                    }
                    else
                    {
                        digits[i] = 0;
                    }
                }

                i--;
            }

            if (digits[0] == 10)
            {
                digits = new int[digits.Length + 1];
                digits[0] = 1;
            }
            return digits;
        }

        /// 67. Add Binary
        /// Given two binary strings a and b, return their sum as a binary string.
        ///1 <= a.length, b.length <= 104, no leading zero
        public string AddBinary(string a, string b)
        {
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
                return string.Empty;

            if (string.IsNullOrEmpty(a))
                return b;
            if (string.IsNullOrEmpty(b))
                return a;

            string s1;
            string s2;
            if (a.Length <= b.Length)
            {
                s1 = a;
                s2 = b;
            }
            else
            {
                s1 = b;
                s2 = a;
            }

            int i = 0;

            List<char> result = new List<char>();

            bool carry = false;
            while (i <= s2.Length - 1)
            {
                if (i >= s1.Length)
                {
                    if (s2[s2.Length - 1 - i] == '0')
                    {
                        if (carry)
                        {
                            result.Insert(0, '1');
                            carry = false;
                        }
                        else
                        {
                            result.Insert(0, '0');
                        }
                    }
                    else
                    {
                        if (carry)
                        {
                            result.Insert(0, '0');
                            carry = true;
                        }
                        else
                        {
                            result.Insert(0, '1');
                        }
                    }
                }
                else
                {
                    if (s1[s1.Length - 1 - i] == '0' && s2[s2.Length - 1 - i] == '0')
                    {
                        if (carry)
                        {
                            result.Insert(0, '1');
                            carry = false;
                        }
                        else
                        {
                            result.Insert(0, '0');
                        }
                    }
                    else if (s1[s1.Length - 1 - i] == '1' && s2[s2.Length - 1 - i] == '1')
                    {
                        if (carry)
                        {
                            result.Insert(0, '1');
                            carry = true;
                        }
                        else
                        {
                            result.Insert(0, '0');
                            carry = true;
                        }
                    }
                    else
                    {
                        if (carry)
                        {
                            result.Insert(0, '0');
                            carry = true;
                        }
                        else
                        {
                            result.Insert(0, '1');
                            carry = false;
                        }
                    }
                }

                i++;
            }

            if (carry)
            {
                result.Insert(0, '1');
            }

            return string.Join("", result);
        }

        //69
        public int MySqrt(int x)
        {
            long r = x;
            while (r * r > x)
                r = (r + x / r) / 2;
            return (int)r;
        }

        ///70. Climbing Stairs
        ///Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?
        public int ClimbStairs(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 2;

            int dp1 = 1;
            int dp2 = 2;
            int dp = 0;
            for (int i = 3; i <= n; i++)
            {
                dp = dp1 + dp2;
                dp1 = dp2;
                dp2 = dp;
            }

            return dp;
        }

        public int ClimbStairs_Recursion(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 2;

            return ClimbStairs_Recursion(n - 1) + ClimbStairs_Recursion(n - 2);
        }

        ///72. Edit Distance
        ///Given two strings word1 and word2, return the minimum number of operations required to convert word1 to word2.
        ///Insert/Delete/Replace a character
        public int MinDistance(string word1, string word2)
        {
            //insert is same to delete
            var len1 = word1.Length;
            var len2 = word2.Length;
            if (len1 == 0 || len2 == 0)
                return Math.Max(len1, len2);

            int[][] dp = new int[len1 + 1][];
            for (int i = 0; i < dp.Length; i++)
            {
                dp[i] = new int[len2 + 1];
            }

            for (int i = 1; i <= len1; i++)
            {
                dp[i][0] = i;
            }
            for (int j = 1; j <= len2; j++)
            {
                dp[0][j] = j;
            }

            for (int i = 1; i <= len1; i++)
            {
                for (int j = 1; j <= len2; j++)
                {
                    if (word1[i - 1] == word2[j - 1])
                    {
                        dp[i][j] = dp[i - 1][j - 1];
                    }
                    else
                    {
                        //replace=dp[i - 1][j - 1]+1, delete = dp[i - 1][j])+1, insert = dp[i][j - 1]+1
                        dp[i][j] = Math.Min(dp[i - 1][j - 1], Math.Min(dp[i][j - 1], dp[i - 1][j])) + 1;
                    }
                }
            }

            return dp[len1][len2];

        }
        /// 74. Search a 2D Matrix
        ///a value in an m x n matrix.
        ///Integers in each row are sorted from left to right.
        ///The first integer of each row is greater than the last integer of the previous row.

        public bool SearchMatrix_74(int[][] matrix, int target)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;

            if (matrix[0][0] > target || matrix[rowLen - 1][colLen - 1] < target)
                return false;

            int row1 = 0;
            int row2 = rowLen - 1;
            int row = (row2 - row1) / 2;

            while (row1 <= row2 && row >= row1 && row <= row2)
            {
                if (matrix[row][colLen - 1] == target)
                {
                    return true;
                }
                else if (matrix[row][colLen - 1] > target)
                {
                    row2 = row - 1;
                    row = (row2 - row1) / 2 + row1;
                }
                else
                {
                    row1 = row + 1;
                    row = (row2 - row1) / 2 + row1;
                }
            }

            //find row
            var arr = matrix[row];

            int col1 = 0;
            int col2 = colLen - 1;
            int col = (col2 - col1) / 2;

            while (col1 <= col2 && col <= col2 && col >= col1)
            {
                if (arr[col] == target)
                {
                    return true;
                }
                else if (arr[col] > target)
                {
                    col2 = col - 1;
                    col = (col2 - col1) / 2 + col1;
                }
                else
                {
                    col1 = col + 1;
                    col = (col2 - col1) / 2 + col1;
                }
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

        /// 77. Combinations
        public IList<IList<int>> Combine(int n, int k)
        {
            if (n == 0)
                return null;

            if (k == 0)
                return null;

            if (n < k)
                return null;

            List<IList<int>> result = new List<IList<int>>();

            if (n == k)
            {
                List<int> list = new List<int>();

                for (int i = 1; i <= n; i++)
                    list.Add(i);

                result.Add(list);
                return result;
            }

            if (k == 1)
            {
                for (int i = 1; i <= n; i++)
                {
                    List<int> list = new List<int>
                    {
                        i
                    };
                    result.Add(list);
                }

                return result;
            }

            for (int i = 1; i <= n - k + 1; i++)
            {
                var list1 = Combine(n - 1, k, i);
                var list2 = Combine(n - 1, k - 1, i);

                if (list1 != null && list1.Count > 0)
                {
                    foreach (var item in list1)
                        result.Add(item);
                }

                if (list2 != null && list2.Count > 0)
                {
                    foreach (var item in list2)
                    {
                        item.Add(n);
                    }

                    foreach (var item in list2)
                        result.Add(item);
                }
            }

            return result;
        }

        public IList<IList<int>> Combine(int n, int k, int start)
        {
            if (n == 0)
                return null;

            if (k == 0)
                return null;

            if (n - start + 1 < k)
                return null;

            if (n - start + 1 == k)
            {
                List<IList<int>> result = new List<IList<int>>();

                List<int> list = new List<int>();

                for (int i = start; i <= n; i++)
                    list.Add(i);

                result.Add(list);
                return result;
            }

            var list1 = Combine(n - 1, k, start);
            var list2 = Combine(n - 1, k - 1, start);

            if (list2 != null && list2.Count > 0)
            {
                foreach (var i in list2)
                {
                    i.Add(n);
                }
            }

            if (list1 != null && list1.Count > 0)
            {
                if (list2 != null && list2.Count > 0)
                {
                    foreach (var i in list2)
                    {
                        list1.Add(i);
                    }
                }

                return list1;
            }

            if (list2 != null && list2.Count > 0)
            {
                return list2;
            }

            return null;
        }

        ///78. Subsets
        ///Given an integer array nums of unique elements, return all possible subsets (the power set).
        public IList<IList<int>> Subsets(int[] nums)
        {
            var ans = new List<IList<int>>
            {
                new List<int>()
            };

            for (int n = 1; n <= nums.Length - 1; n++)
            {
                var llist = new List<IList<int>>();

                SubSets_Add(nums, 0, n, llist, ans);
            }

            ans.Add(nums);

            return ans;
        }

        public void SubSets_Add(int[] nums, int start, int number, IList<IList<int>> llist, IList<IList<int>> ans)
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

            var targets = llist.Where(o => o.Count == number);
            foreach (var t in targets)
            {
                ans.Add(t);
            }

            SubSets_Add(nums, start + 1, number, llist, ans);
        }

        ///79. Word Search
        ///Given an m x n grid of characters board and a string word, return true if word exists in the grid.
        public bool Exist(char[][] board, string word)
        {
            var rLen = board.Length;
            var cLen = board[0].Length;
            var visit = createVisitArray(rLen, cLen);
            return Exist(board, visit, 0, 0, word, 0);
        }

        public bool Exist(char[][] board, bool[][] visit, int r, int c, string word, int index)
        {
            if (index >= word.Length)
                return true;
            var rLen = board.Length;
            var cLen = board[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };

            if (index == 0)
            {
                for (int i = 0; i < rLen; i++)
                {
                    for (var j = 0; j < cLen; j++)
                    {
                        if (board[i][j] == word[index])
                        {
                            var arr = createVisitArray(rLen, cLen);
                            arr[i][j] = true;
                            bool result = Exist(board, arr, i, j, word, index + 1);
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
                    if (row >= 0 && row < rLen && col >= 0 && col < cLen
                        && !visit[row][col] && board[row][col] == word[index])
                    {
                        var arr = createVisitArray(rLen, cLen, visit);
                        arr[row][col] = true;
                        bool result = Exist(board, arr, row, col, word, index + 1);
                        if (result)
                            return true;
                    }
                }
            }
            return false;
        }

        public bool[][] createVisitArray(int r, int c, bool[][] copy = null)
        {
            bool[][] ans = new bool[r][];
            for (int i = 0; i < r; i++)
                ans[i] = new bool[c];
            if (copy != null)
            {
                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        ans[i][j] = copy[i][j];
                    }
                }
            }
            return ans;
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

        ///84. Largest Rectangle in Histogram -- not done
        ///heights representing the histogram's bar height where the width of each bar is 1,
        ///return the area of the largest rectangle in the histogram.
        /// 0<= height <=10000, heights.Length>=1
        public int LargestRectangleArea(int[] heights)
        {

            int[] dp = new int[heights.Length];

            int start = 0;
            int end = 0;
            int minHeight = heights[0];
            int curr = (end - start + 1) * minHeight;
            int max = 0;
            max = Math.Max(max, curr);

            for (int i = 1; i < heights.Length; i++)
            {
            }




            return max;

        }
        /// 88. Merge Sorted Array
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (nums1 == null || nums1.Length == 0 || m == 0)
            {
                if (nums2 == null || nums2.Length == 0 || n == 0)
                {
                    //
                }
                else
                {
                    for (int a = 0; a < nums1.Length; a++)
                    {
                        if (a < m + n)
                        {
                            nums1[a] = nums2[a];
                        }
                        else
                        {
                            nums1[a] = 0;
                        }
                    }
                }
            }
            else
            {
                if (nums2 == null || nums2.Length == 0 || n == 0)
                {
                    for (int a = 0; a < nums1.Length; a++)
                    {
                        if (a < m + n)
                        {
                            nums1[a] = nums1[a];
                        }
                        else
                        {
                            nums1[a] = 0;
                        }
                    }
                }
                else
                {
                    int[] result = new int[m + n];
                    int k = 0;
                    int j = 0;
                    int i = 0;
                    while (i < m && j < n)
                    {
                        if (nums1[i] <= nums2[j])
                        {
                            result[k] = nums1[i];
                            k++;
                            i++;
                        }
                        else
                        {
                            result[k] = nums2[j];
                            k++;
                            j++;
                        }
                    }

                    while (k < m + n)
                    {
                        if (i < m)
                        {
                            result[k] = nums1[i];
                            k++;
                            i++;
                        }

                        if (j < n)
                        {
                            result[k] = nums2[j];
                            k++;
                            j++;
                        }
                    }

                    for (int a = 0; a < nums1.Length; a++)
                    {
                        if (a < k)
                        {
                            nums1[a] = result[a];
                        }
                        else
                        {
                            nums1[a] = 0;
                        }
                    }
                }
            }
            Console.WriteLine($"nums1 = {string.Join(",", nums1)}");
        }

        ///90. Subsets II
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

        /// 91. Decode Ways
        ///A message containing letters from A-Z can be encoded into numbers using the following mapping:
        ///'A' -> "1", Z->26, 1 <= s.length <= 100
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
                int prev = s[i-1]- '0';

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

        /// 94. Binary Tree Inorder Traversal
        public IList<int> InorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            InorderTraversal_Recursion(root, result);
            return result;
        }

        public void InorderTraversal_Recursion(TreeNode node, IList<int> list)
        {
            if (node == null)
                return;
            InorderTraversal_Recursion(node.left, list);
            list.Add(node.val);

            InorderTraversal_Recursion(node.right, list);
        }

        public IList<int> InorderTraversal_Iteration(TreeNode root)
        {
            List<int> values = new List<int>();

            if (root == null) return values;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode node = root;

            while (node != null || stack.Any())
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }
                else
                {
                    var item = stack.Pop();
                    values.Add(item.val);
                    node = item.right;
                }
            }
            return values;
        }

        public IList<int> LayerTraversal(TreeNode root)
        {
            List<int> values = new List<int>();

            if (root == null) return values;

            IList<TreeNode> nodes = new List<TreeNode> { root };

            while (nodes != null && nodes.Count > 0)
            {
                nodes = GetInorderAndReturnSubNodes(nodes, values);
            }

            return values;
        }

        public IList<TreeNode> GetInorderAndReturnSubNodes(IList<TreeNode> nodes, List<int> values)
        {
            if (nodes == null || nodes.Count == 0) return null;
            IList<TreeNode> subNodes = new List<TreeNode>();
            foreach (var n in nodes)
            {
                values.Add(n.val);
                if (n.left != null) { subNodes.Add(n.left); }
                if (n.right != null) { subNodes.Add(n.right); }
            }
            return subNodes;
        }

        /// 96. Unique Binary Search Trees
        /// Given an integer n, return the number of structurally unique BST's (binary search trees)
        /// which has exactly n nodes of unique values from 1 to n.
        public int NumTrees(int n)
        {
            return 0;
        }

        ///98. Validate Binary Search Tree
        /// left.val<=Node.Val<=right.val

        public bool IsValidBST(TreeNode root)
        {
            return IsValidBST_Recursion(root);
        }

        public bool IsValidBST_Recursion(TreeNode root, TreeNode left = null, TreeNode right = null)
        {
            if (root == null)
                return true;

            if (left != null && root.val <= left.val)
                return false;

            if (right != null && root.val >= right.val)
                return false;

            return IsValidBST_Recursion(root.left, left, root) && IsValidBST_Recursion(root.right, root, right);
        }
    }
}
