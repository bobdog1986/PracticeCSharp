using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1071. Greatest Common Divisor of Strings
        ///Given two strings str1 and str2, return the largest string x such that x divides both str1 and str2.
        public string GcdOfStrings(string str1, string str2)
        {
            while(str1.Length>0 && str2.Length > 0 && str1 + str2 == str2 + str1)
            {
                if(str1 == str2)return str1;
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
                if (str1.Length%gcb==0 && str2.Length % gcb == 0)
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
            for(int i = 0; i < str1.Length; i += str2.Length)
            {
                if (str1.Substring(i, str2.Length) != str2) return false;
            }
            return true;
        }
        ///1091. Shortest Path in Binary Matrix
        ///Given an n x n binary matrix grid, return the length of the shortest clear path in the matrix.
        ///If there is no clear path, return -1.
        ///8 direction
        public int ShortestPathBinaryMatrix(int[][] grid)
        {
            int len = grid.Length;

            if (len == 1)
                return grid[0][0] == 0 ? 1 : -1;

            if (grid[0][0] == 1 || grid[len - 1][len - 1] == 1)
                return -1;

            int[][] visit = new int[len][];

            for (int i = 0; i < len; i++)
            {
                visit[i] = new int[len];
            }

            int[][] dxy = new int[8][] {
                new int[]{0,1}, new int[] {1, 0 },
                new int[]{ 0,-1}, new int[] { -1, 0 },
                new int[]{ 1,1 }, new int[]{ -1, -1 },
                new int[]{ -1, 1 },new int[] { 1, -1 } };

            List<int[]> list = new List<int[]>();

            int step = 0;

            list.Add(new int[] { 0, 0 });
            visit[0][0] = 1;
            step++;

            while (list.Count > 0)
            {
                List<int[]> sub = new List<int[]>();

                foreach (var cell in list)
                {
                    int r = cell[0];
                    int c = cell[1];

                    foreach (var d in dxy)
                    {
                        int r1 = r + d[0];
                        int c1 = c + d[1];

                        if (r1 >= 0 && r1 <= len - 1 && c1 >= 0 && c1 <= len - 1)
                        {
                            if (r1 == len - 1 && c1 == len - 1)
                                return step + 1;

                            if (visit[r1][c1] == 1)
                                continue;

                            visit[r1][c1] = 1;

                            if (grid[r1][c1] == 0)
                            {
                                sub.Add(new int[] { r1, c1, });
                            }

                        }


                    }
                }

                list = sub;


                step++;
            }





            return -1;
        }
    }
}
