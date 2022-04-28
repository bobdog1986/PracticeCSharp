using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///905. Sort Array By Parity
        ///Given an integer array nums, move all the even integers at the beginning of the array followed by all the odd integers.
        public int[] SortArrayByParity(int[] nums)
        {
            var res = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] % 2 == 0) res.Insert(0, nums[i]);
                else res.Add(nums[i]);
            }
            return res.ToArray();
        }

        ///908. Smallest Range I
        ///The score of nums is the difference between the maximum and minimum elements in nums.
        ///Return the minimum score of nums after applying the mentioned operation at most once for each index in it.
        public int SmallestRangeI(int[] nums, int k)
        {
            int max = nums[0];
            int min = nums[0];
            foreach (var n in nums)
            {
                max = Math.Max(max, n);
                min = Math.Min(min, n);
            }
            return Math.Max(0, max - min - 2 * k);
        }

        ///910. Smallest Range II
        ///For each index i where 0 <= i<nums.length, change nums[i] to be either nums[i] + k or nums[i] - k.
        ///Return the minimum difference between the max and min elements after changing the values at each index.
        public int SmallestRangeII(int[] nums, int k)
        {
            Array.Sort(nums);
            int n = nums.Length;
            int res = nums[n - 1] - nums[0];
            for (int i = 0; i < n - 1; ++i)
            {
                int max = Math.Max(nums[i] + k, nums[n - 1] - k);
                int min = Math.Min(nums[i + 1] - k, nums[0] + k);
                res = Math.Min(res, max - min);
            }
            return res;
        }

        ///917. Reverse Only Letters
        ///not English letters remain in the same position.English letters(lowercase or uppercase) should be reversed.
        public string ReverseOnlyLetters(string s)
        {
            var arr = s.ToArray();
            List<int> list = new List<int>();
            List<char> letters = new List<char>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (char.IsLetter(s[i]))
                {
                    list.Add(i);
                    letters.Insert(0, s[i]);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                arr[list[i]] = letters[i];
            }
            return new string(arr);
        }

        /// 918. Maximum Sum Circular Subarray, #Kadane
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

        ///921. Minimum Add to Make Parentheses Valid
        ///Return the minimum number of moves required to make s valid. ()) to ()() or (())
        public int MinAddToMakeValid(string s)
        {
            int left=0;
            int right=0;
            for(int i=0; i<s.Length; i++)
            {
                if(s[i] == '(')
                {
                    right++;
                }
                else
                {
                    if (right > 0) right--;
                    else left++;
                }
            }
            return left + right;
        }

        /// 923. 3Sum With Multiplicity
        ///Given an integer array arr, and an integer target, return the number of tuples i, j, k
        ///such that i < j < k and arr[i] + arr[j] + arr[k] == target.  return it modulo 10^9 + 7.
        ///0<= arr[i] <=100
        public int ThreeSumMulti(int[] arr, int target)
        {
            long[] nums = new long[101];
            foreach (int i in arr)
                nums[i]++;
            long res = 0;
            for (int i = 0; i <= 100; i++)
                for (int j = i; j <= 100; j++)
                {
                    int k = target - i - j;
                    if (k > 100 || k < 0) continue;
                    if (i == j && j == k)
                        res += nums[i] * (nums[i] - 1) * (nums[i] - 2) / 6;
                    else if (i == j && j != k)
                        res += nums[i] * (nums[i] - 1) / 2 * nums[k];
                    else if (j < k)
                        res += nums[i] * nums[j] * nums[k];
                }
            return (int)(res % 10_0000_0007);
        }

        /// 925. Long Pressed Name
        ///Your friend is typing his name into a keyboard. Sometimes, when typing a character c,
        ///the key might get long pressed, and the character will be typed 1 or more times.
        ///You examine the typed characters of the keyboard.Return True if it is possible
        ///that it was your friends name, with some characters (possibly none) being long pressed
        public bool IsLongPressedName(string name, string typed)
        {
            int i = 0;
            int j = 0;
            for (; j < typed.Length && i < name.Length; j++)
            {
                if (typed[j] == name[i]) i++;
                else
                {
                    if (j == 0 || typed[j] != typed[j - 1]) return false;
                }
            }

            if (i != name.Length) return false;
            while (j < typed.Length)
            {
                if (typed[j] != typed[j - 1]) return false;
                j++;
            }
            return true;
        }

        /// 929. Unique Email Addresses
        ///If you add periods '.' between some characters in the local name part of an email address, ignored
        ///If you add a plus '+' in the local name, everything after the first plus sign will be ignored.
        public int NumUniqueEmails(string[] emails)
        {
            HashSet<string> map = new HashSet<string>();
            foreach (var email in emails)
            {
                var arr = email.Split("@");
                var str0 = arr[0].Split("+")[0].Replace(".", "");//first str before '@', then trim all '.'
                var str = str0 + "@" + arr[1];
                if (!map.Contains(str)) map.Add(str);
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
                    List<int[]> visits = new List<int[]>() { new int[] { i, j } };
                    grid[i][j] = 0;
                    while (visits.Count > 0)
                    {
                        curr.AddRange(visits);
                        List<int[]> nexts = new List<int[]>();
                        foreach (var v in visits)
                        {
                            foreach (var d in dxy4)
                            {
                                int r = v[0] + d[0];
                                int c = v[1] + d[1];
                                if (r >= 0 && r < grid.Length && c >= 0 && c < grid[i].Length && grid[r][c] == 1)
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
                    int len = int.MaxValue;
                    if (i[0] == j[0]) len = Math.Abs(i[1] - j[1]) - 1;
                    else if (i[1] == j[1]) len = Math.Abs(i[0] - j[0]) - 1;
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
            foreach (var c in s)
            {
                if (c == 'I') increase++;
                else decrease++;
            }
            ans.Add(list[decrease]);
            foreach (var c in s)
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

        ///946. Validate Stack Sequences
        ///Given two integer arrays pushed and popped each with distinct values,
        ///return true if this could have been the result of a sequence of push and pop operations on an initially empty stack, or false otherwise.
        public bool ValidateStackSequences(int[] pushed, int[] popped)
        {
            Stack<int> stack = new Stack<int>();
            var list1 = pushed.ToList();
            var list2 = popped.ToList();
            int count = 0;//how many nums in popped array have been handled
            while (count++ < pushed.Length)
            {
                var index = list1.IndexOf(list2[0]);
                if (index == -1)
                {
                    if (stack.Count == 0) return false;
                }
                else
                {
                    for (int i = 0; i <= index; i++)
                    {
                        stack.Push(list1[0]);//push all nums before popped[0] to stack
                        list1.RemoveAt(0);//then remove
                    }
                }
                var pop = stack.Pop();
                if (pop != list2[0]) return false;
                list2.RemoveAt(0);//if equal, remove popped[0]
            }
            return true;
        }
    }
}