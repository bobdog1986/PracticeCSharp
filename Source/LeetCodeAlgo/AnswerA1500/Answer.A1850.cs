﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1855. Maximum Distance Between a Pair of Values
        ///You are given two non-increasing 0-indexed integer arrays nums1 and nums2​​​​​​
        ///i <= j and nums1[i] <= nums2[j]. The distance of the pair is j - i​​​​.
        ///Return the maximum distance of any valid pair(i, j). If there are no valid pairs, return 0.
        public int MaxDistance(int[] nums1, int[] nums2)
        {
            int i = 0;
            int j = 0;
            int res = 0;
            while(i<nums1.Length && j < nums2.Length)
            {
                if (i > j)
                {
                    j++;
                }
                else
                {
                    if (nums1[i] > nums2[j]) i++;
                    else
                    {
                        res = Math.Max(res, j - i);
                        j++;
                    }
                }
            }
            return res;
        }
        ///1859. Sorting the Sentence
        public string SortSentence(string s)
        {
            return string.Join(' ', s.Split(' ').OrderBy(x => x.Last()).Select(x => x.Substring(0, x.Length - 1)));
        }
        /// 1860. Incremental Memory Leak
        ///Return an array containing [crashTime, memory1crash, memory2crash]
        public int[] MemLeak(int memory1, int memory2)
        {
            int i = 1;
            while (Math.Max(memory1, memory2) >= i)
            {
                if (memory1 >= memory2) memory1 -= i;
                else memory2 -= i;
                i++;
            }
            return new int[] { i, memory1, memory2 };
        }

        ///1861. Rotating the Box
        ///A stone '#',A stationary obstacle '*',Empty '.'
        public char[][] RotateTheBox(char[][] box)
        {
            int m = box.Length;
            int n = box[0].Length;
            var res = new char[n][];
            for (int i = 0; i < n; i++)
                res[i] = new char[m];
            for (int i = 0; i < m; i++)
            {
                int r = 0;
                int stone = 0;
                for (int j = 0; j < n; j++)
                {
                    if (box[i][j] == '*')
                    {
                        while (stone> 0)
                        {
                            res[r++][m - 1 - i] = '#';
                            stone--;
                        }
                        res[r++][m - 1 - i] = '*';
                    }
                    else if (box[i][j] == '.')
                    {
                        res[r++][m - 1 - i] = '.';
                    }
                    else
                    {
                        stone++;
                    }
                }
                while (stone > 0)
                {
                    res[r++][m - 1 - i] = '#';
                    stone--;
                }
            }
            return res;
        }
        /// 1876. Substrings of Size Three with Distinct Characters
        ///A string is good if there are no repeated characters.
        ///Given a string s,return the number of good substrings of length three in s.
        public int CountGoodSubstrings(string s)
        {
            int res = 0;
            Dictionary<char, int> dict = new Dictionary<char, int>();
            for (int i = 0; i < s.Length - 2; i++)
            {
                if (i == 0)
                {
                    dict.Add(s[0], 1);
                    if (dict.ContainsKey(s[1])) dict[s[1]]++;
                    else dict.Add(s[1], 1);
                }
                if (dict.ContainsKey(s[i + 2])) dict[s[i + 2]]++;
                else dict.Add(s[i + 2], 1);
                if (dict.Count == 3) res++;
                dict[s[i]]--;
                if (dict[s[i]] == 0) dict.Remove(s[i]);
            }
            return res;
        }

        ///1877. Minimize Maximum Pair Sum in Array
        ///Return the minimized maximum pair sum after optimally pairing up the elements.
        public int MinPairSum(int[] nums)
        {
            int max = int.MinValue;
            Array.Sort(nums);
            for (int i = 0; i < nums.Length / 2; i++)
                max = Math.Max(max, nums[i] + nums[nums.Length - 1 - i]);
            return max;
        }
        ///1880. Check if Word Equals Summation of Two Words
        public bool IsSumEqual(string firstWord, string secondWord, string targetWord)
        {
            return IsSumEqual(firstWord) + IsSumEqual(secondWord) == IsSumEqual(targetWord);
        }

        private int IsSumEqual(string str)
        {
            return int.Parse(new string(str.Select(x => (char)(x - 'a' + '0')).ToArray()));
        }
        /// 1884. Egg Drop With 2 Eggs and N Floors
        public int TwoEggDrop(int n)
        {
            int res = 0;
            int seed = 1;
            while (n > 0)
            {
                n -= seed++;
                res++;
            }
            return res;
        }
        /// 1886. Determine Whether Matrix Can Be Obtained By Rotation
        ///Given two n x n binary matrices mat and target, return true if it is possible to
        ///make mat equal to target by rotating mat in 90-degree increments, or false otherwise.
        public bool FindRotation(int[][] mat, int[][] target)
        {
            int n = mat.Length;
            bool[] rotate = new bool[4];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //every time rotate right 90 degree, row=j,col=n-1-i;
                    if (!rotate[0] && mat[i][j] != target[i][j]) rotate[0] = true;
                    if (!rotate[1] && mat[j][n - 1 - i] != target[i][j]) rotate[1] = true;
                    if (!rotate[2] && mat[n - 1 - i][n - 1 - j] != target[i][j]) rotate[2] = true;
                    if (!rotate[3] && mat[n - 1 - j][i] != target[i][j]) rotate[3] = true;
                    if (rotate[0] && rotate[1] && rotate[2] && rotate[3]) return false;
                }
            }
            return true;
        }

        ///1887. Reduction Operations to Make the Array Elements Equal
        public int ReductionOperations(int[] nums)
        {
            Dictionary<int,int> dict = new Dictionary<int,int>();
            foreach(var n in nums)
            {
                if (dict.ContainsKey(n)) dict[n]++;
                else dict.Add(n,1);
            }
            int res = 0;
            var keys = dict.Keys.OrderBy(x => x).ToList();
            for(int i=1;i<keys.Count;i++)
                res+=i*dict[keys[i]];
            return res;
        }
        /// 1894. Find the Student that Will Replace the Chalk, #Binary Search
        ///Every time k-=chalk[i], return not enough for index i;
        public int ChalkReplacer(int[] chalk, int k)
        {
            long[] prefix = new long[chalk.Length];
            long sum = 0;
            for (int i = 0; i < chalk.Length; i++)
            {
                sum += chalk[i];
                prefix[i] = sum;
            }
            k = (int)(k % sum);
            int left = 0;
            int right = chalk.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (k == prefix[mid]) return mid + 1;
                else if (k < prefix[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return left;
        }
    }
}