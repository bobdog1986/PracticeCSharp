﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1054. Distant Barcodes, #PriorityQueue, #Heap
        ///Rearrange the barcodes so that no two adjacent barcodes are equal.
        public int[] RearrangeBarcodes(int[] barcodes)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var bar in barcodes)
            {
                if(dict.ContainsKey(bar))dict[bar]++;
                else dict.Add(bar, 1);
            }

            //sort by occurrence - max heap
            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();

            foreach (var k in dict.Keys)
            {
                queue.Enqueue(k,dict[k]);
            }

            // poll from queue - put into res array
            int[] res = new int[barcodes.Length];
            int i = 0;//start from index-0, traversal all even indexes
            while (queue.Count>0)
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
            int gcb = getGcb(str1.Length, str2.Length);
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

        ///1089. Duplicate Zeros
        ///Given a fixed-length integer array arr, duplicate each occurrence of zero, shifting the remaining elements to the right.
        public void DuplicateZeros(int[] arr)
        {
            int n = arr.Length;
            int[] temp = new int[n];
            for (int i = 0,j=0; i < n&&j<n; i++)
            {
                temp[j++] = arr[i];
                if (arr[i]==0 && j<n)
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
            visit[0,0] = true;
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
                            visit[r,c] = true;
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
    }
}