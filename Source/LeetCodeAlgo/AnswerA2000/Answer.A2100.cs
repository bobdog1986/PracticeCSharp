﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2103. Rings and Rods
        ///Return the number of rods that have all three colors of rings on them.
        public int CountPoints(string rings)
        {
            int[,] mat = new int[10, 3];
            for (int i = 0; i < rings.Length; i += 2)
            {
                int index = rings[i + 1] - '0';
                if (rings[i] == 'R')
                {
                    mat[index, 0] = 1;
                }
                else if (rings[i] == 'G')
                {
                    mat[index, 1] = 1;
                }
                else if (rings[i] == 'B')
                {
                    mat[index, 2] = 1;
                }
            }

            int res = 0;
            for (int i = 0; i < 10; i++)
            {
                if (mat[i, 0] == 1 && mat[i, 1] == 1 && mat[i, 2] == 1) res++;
            }
            return res;
        }

        ///2108. Find First Palindromic String in the Array
        public string FirstPalindrome(string[] words)
        {
            foreach(var w in words)
                if (w == new string(w.Reverse().ToArray())) return w;
            return "";
        }
        ///2114. Maximum Number of Words Found in Sentences
        ///A sentence is a list of words that are separated by a single space with no leading or trailing spaces.
        ///Return the maximum number of words that appear in a single sentence.
        public int MostWordsFound(string[] sentences)
        {
            //return sentences.Max(x => x.Split(' ').Count());
            return sentences.Max(x => x.Where(x=>x==' ').Count())+1;
        }
        /// 2119. A Number After a Double Reversal
        ///eg. 1234 reverse to 4321, then again to 1234== origin 1234, return true
        public bool IsSameAfterReversals(int num)
        {
            return num == 0 || num % 10 != 0;
        }

        ///2129. Capitalize the Title
        /// Capitalize the string by changing the capitalization of each word such that:
        ///If the length of the word is 1 or 2 letters, change all letters to lowercase.
        ///Otherwise, change the first letter to uppercase and the remaining letters to lowercase.

        public string CapitalizeTitle(string title)
        {
            var arr = title.Split(' ').Where(x => x.Length > 0).Select(x =>
             {
                 var str = x.ToLower();
                 if (x.Length <= 2) return str;
                 return str.Substring(0, 1).ToUpper() + str.Substring(1);
             });
            return string.Join(" ", arr);
        }

        ///2133. Check if Every Row and Column Contains All Numbers
        ///An n x n matrix is valid if every row and every column contains all the integers from 1 to n (inclusive).
        ///Given an n x n integer matrix matrix, return true if the matrix is valid. Otherwise, return false.
        public bool CheckValid(int[][] matrix)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;
            bool[,] colVisitMatrix = new bool[rowLen, colLen];
            for (int i = 0; i < rowLen; i++)
            {
                bool[] rowVisit = new bool[rowLen];
                for (int j = 0; j < colLen; j++)
                {
                    if (colVisitMatrix[j, matrix[i][j] - 1]) return false;
                    colVisitMatrix[j, matrix[i][j] - 1] = true;

                    if (rowVisit[matrix[i][j] - 1]) return false;
                    rowVisit[matrix[i][j] - 1] = true;
                }
            }
            return true;
        }

        /// 2148. Count Elements With Strictly Smaller and Greater Elements
        ///return the number of elements that have both a strictly smaller and a strictly greater element appear in nums.
        ///-100000 <= nums[i] <= 100000
        public int CountElements(int[] nums)
        {
            int ship = 100000;
            int[] arr = new int[ship * 2 + 1];
            int start = arr.Length - 1;
            int end = 0;
            foreach (var num in nums)
            {
                var index = num + ship;
                arr[index]++;
                start = Math.Min(start, index);
                end = Math.Max(end, index);
            }
            int sum = 0;
            for (int i = start + 1; i <= end - 1; i++)
                sum += arr[i];
            return sum;
        }
    }
}