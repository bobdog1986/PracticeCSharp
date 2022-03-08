using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///918. Maximum Sum Circular Subarray
        ///Kadane algorithm, find max-of-positive and min-of-negtive, return max-of-positive or Sum()- min-of-neg
        public int MaxSubarraySumCircular(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];

            int sumOfPositive = nums[0];
            int max = nums[0];
            int allSum = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                sumOfPositive = Math.Max(sumOfPositive + nums[i], nums[i]);
                max = Math.Max(sumOfPositive, max);
                allSum += nums[i];
            }

            if (max < 0)
            {
                return max;
            }

            int sumOfNegative = nums[0];
            int min = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                sumOfNegative = Math.Min(sumOfNegative + nums[i], nums[i]);
                min = Math.Min(sumOfNegative, min);
            }

            return Math.Max(max, allSum - min);
        }

        ///929. Unique Email Addresses
        ///If you add periods '.' between some characters in the local name part of an email address, ignored
        ///If you add a plus '+' in the local name, everything after the first plus sign will be ignored.
        public int NumUniqueEmails(string[] emails)
        {
            HashSet<string> map = new HashSet<string>();
            foreach(var email in emails)
            {
                var arr = email.Split("@");
                var str0 = arr[0].Split("+")[0].Replace(".", "");//first str before '@', then trim all '.'
                var str = str0 + "@" +arr[1];
                if(!map.Contains(str))map.Add(str);
            }
            return map.Count;
        }

        /// 931. Minimum Falling Path Sum
        /// Given an n x n array of integers matrix, return the minimum sum of any falling path through matrix.
        public int MinFallingPathSum(int[][] matrix)
        {
            var len = matrix.Length;
            if (len == 1)
                return matrix[0][0];

            int[] dp = new int[len];

            for (int i = 0; i < len; i++)
                dp[i] = matrix[0][i];

            for (int i = 1; i < len; i++)
            {
                int[] dp2 = new int[len];
                for (int k = 0; k < len; k++)
                    dp2[k] = dp[k];

                for (int j = 0; j < len; j++)
                {
                    int a = dp2[j] + matrix[i][j];

                    if (j > 0)
                        a = Math.Min(a, dp2[j - 1] + matrix[i][j]);

                    if (j < len - 1)
                        a = Math.Min(a, dp2[j + 1] + matrix[i][j]);

                    dp[j] = a;
                }
            }

            return dp.Min();
        }

        ///934. Shortest Bridge, #Graph, #DFS
        ///You are given an n x n binary matrix grid where 1 represents land and 0 represents water.
        ///An island is a 4-directionally connected group of 1's not connected to any other 1's. There are exactly two islands in grid.
        ///You may change 0's to 1's to connect the two islands to form one island. Return the smallest number of 0's to connect 2 islands
        public int ShortestBridge(int[][] grid)
        {
            List<int[]> list1 = new List<int[]>();
            List<int[]> list2 = new List<int[]>();
            int[][] dxy4 = new int[4][] { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 0) continue;
                    List<int[]> curr = new List<int[]>();
                    List<int[]> visits = new List<int[]>() { new int[] {i,j } };
                    grid[i][j] = 0;
                    while (visits.Count > 0)
                    {
                        curr.AddRange(visits);
                        List<int[]> nexts = new List<int[]>();
                        foreach(var v in visits)
                        {
                            foreach (var d in dxy4)
                            {
                                int r = v[0] + d[0];
                                int c = v[1] + d[1];
                                if(r>=0&&r<grid.Length && c>=0&&c<grid[i].Length && grid[r][c] == 1)
                                {
                                    grid[r][c] = 0;
                                    nexts.Add(new int[] { r, c });
                                }
                            }
                        }
                        visits = nexts;
                    }
                    if (list1.Count == 0) { list1 = curr; }
                    else { list2 = curr; }
                }
                if (list2.Count > 0) break;
            }
            int min = int.MaxValue;
            foreach (var i in list1)
            {
                foreach (var j in list2)
                {
                    int len=int.MaxValue;
                    if (i[0] == j[0]) len = Math.Abs(i[1] - j[1])-1;
                    else if(i[1] == j[1]) len = Math.Abs(i[0] - j[0])-1;
                    else len = Math.Abs(i[1] - j[1]) - 1 + Math.Abs(i[0] - j[0]);
                    min = Math.Min(min, len);
                    if (min == 1) return min;
                }
            }
            return min;
        }

        /// 941. Valid Mountain Array
        ///len>=3, arr[i]> all [0,i-1],and [i+1,len-1]
        public bool ValidMountainArray(int[] arr)
        {
            if (arr.Length < 3)
                return false;

            bool isClimbing = true;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == arr[i - 1])
                    return false;

                if (isClimbing)
                {
                    if (arr[i] < arr[i - 1])
                    {
                        if (i == 1)
                            return false;

                        isClimbing = false;
                    }
                }
                else
                {
                    if (arr[i] > arr[i - 1])
                        return false;
                }
            }
            return !isClimbing;
        }
        ///942. DI String Match
        ///s[i] == 'I' if perm[i] < perm[i + 1], s[i] == 'D' if perm[i] > perm[i + 1].
        ///Given a string s, reconstruct the permutation perm and return it.
        public int[] DiStringMatch(string s)
        {
            var ans = new List<int>();
            var list = new List<int>();
            for (int i = 0; i <= s.Length; i++)
            {
                list.Add(i);
            }
            int increase = 0;
            int decrease = 0;
            foreach(var c in s)
            {
                if (c == 'I') increase++;
                else decrease++;
            }
            ans.Add(list[decrease]);
            foreach(var c in s)
            {
                if (c == 'I')
                {
                    ans.Add(list[list.Count - 1 - (increase - 1)]);
                    increase--;
                }
                else
                {
                    ans.Add(list[decrease - 1]);
                    decrease--;
                }
            }
            return ans.ToArray();
        }
    }
}