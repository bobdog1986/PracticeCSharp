using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///509. Fibonacci Number
        public int Fib(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            if (n == 1)
            {
                return 1;
            }

            int dp = 0;
            int a1 = 0;
            int a2 = 1;

            int i = 2;
            while (i <= n)
            {
                dp = a1 + a2;
                a1 = a2;
                a2 = dp;
                i++;
            }

            return dp;
        }
        public int Fib_Recursion(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            if (n == 1)
            {
                return 1;
            }

            return Fib_Recursion(n - 1) + Fib_Recursion(n - 2);
        }

        //542. 01 Matrix
        public int[][] UpdateMatrix(int[][] mat)
        {
            int rowLen = mat.Length;
            int colLen = mat[0].Length;

            int[][] result = new int[rowLen][];
            for (int i = 0; i < rowLen; i++)
            {
                result[i] = new int[colLen];
            }

            //init every cell as max value, assuming the only 0 at furthest corner cell
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    result[i][j] = Math.Max(i, rowLen - i - 1) + Math.Max(j, colLen - j - 1);
                }
            }

            //left-top to right-bottom
            int MAX_DISTANCE = (rowLen - 1) + (colLen - 1);

            //dp value
            int distance;

            //loop rows first, forward sequence then backward
            for (int r = 0; r < rowLen; r++)
            {
                if (colLen == 1)
                    break;

                distance = MAX_DISTANCE;
                for (int c = 0; c < colLen; c++)
                {
                    if (mat[r][c] == 0)
                    {
                        distance = 0;
                    }
                    else
                    {
                        if (distance != MAX_DISTANCE)
                        {
                            distance++;
                        }
                    }
                    result[r][c] = Math.Min(result[r][c], distance);
                }

                distance = MAX_DISTANCE;
                for (int c = colLen - 1; c >= 0; c--)
                {
                    if (mat[r][c] == 0)
                    {
                        distance = 0;
                    }
                    else
                    {
                        if (distance != MAX_DISTANCE)
                        {
                            distance++;
                        }
                    }
                    result[r][c] = Math.Min(result[r][c], distance);
                }
            }

            //then loop cols
            for (int c = 0; c < colLen; c++)
            {
                if (rowLen == 1)
                    break;

                distance = MAX_DISTANCE;
                for (int r = 0; r < rowLen; r++)
                {
                    if (mat[r][c] == 0)
                    {
                        distance = 0;
                    }
                    else
                    {
                        if (distance != MAX_DISTANCE)
                        {
                            distance++;
                        }
                    }
                    result[r][c] = Math.Min(result[r][c], distance);
                }

                distance = MAX_DISTANCE;
                for (int r = rowLen - 1; r >= 0; r--)
                {
                    if (mat[r][c] == 0)
                    {
                        distance = 0;
                    }
                    else
                    {
                        if (distance != MAX_DISTANCE)
                        {
                            distance++;
                        }
                    }
                    result[r][c] = Math.Min(result[r][c], distance);
                }
            }

            if (rowLen == 1 || colLen == 1)
                return result;

            //every cell value is min of itself and all adjacents(left,right,top,bottom)
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (j > 0)
                    {
                        result[i][j] = Math.Min(result[i][j], result[i][j - 1] + 1);
                    }

                    if (j < colLen - 1)
                    {
                        result[i][j] = Math.Min(result[i][j], result[i][j + 1] + 1);
                    }

                    if (i > 0)
                    {
                        result[i][j] = Math.Min(result[i][j], result[i - 1][j] + 1);
                    }

                    if (i < rowLen - 1)
                    {
                        result[i][j] = Math.Min(result[i][j], result[i + 1][j] + 1);
                    }
                }
            }

            return result;
        }

        public int[] UpdateMatrixRow(int[] data)
        {
            if (data == null)
                return null;
            if (data.Length == 0)
                return data;

            int[] result = new int[data.Length];

            int left = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 0)
                {
                    left = 0;
                }
                else
                {
                    left++;
                }
                result[i] = left;
            }

            int right = 0;
            for (int i = data.Length - 1; i >= 0; i--)
            {
                if (data[i] == 0)
                {
                    right = 0;
                }
                else
                {
                    right++;
                }
                result[i] = Math.Min(result[i], right);
            }

            return result;
        }

        ///547. Number of Provinces
        ///BFS/DFS, same to 200
        public int FindCircleNum(int[][] isConnected)
        {
            int[] arr = new int[isConnected.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = 1;
            }
            int ans = 0;
            for (int i = 0; i < isConnected.Length; i++)
            {
                if (arr[i] == 0)
                {
                    continue;
                }
                else
                {
                    ans++;
                    arr[i] = 0;
                }

                for (int j = 0; j < isConnected[i].Length; j++)
                {
                    if (i!=j && isConnected[i][j] == 1)
                    {
                        FindCircleNum_RemoveAllConnected(isConnected, arr, i, j);
                    }
                }
            }
            return ans;
        }

        public void FindCircleNum_RemoveAllConnected(int[][] isConnected,int[] arr, int r, int c)
        {
            if (arr[c] == 0)
                return;

            arr[c] = 0;

            for (int j = 0; j < isConnected[c].Length; j++)
            {
                if (c != j && isConnected[c][j] == 1 )
                {
                    FindCircleNum_RemoveAllConnected(isConnected, arr, c, j);
                }
            }
        }
        /// 557. Reverse Words in a String III
        public string ReverseWords(string s)
        {
            var arr = s.Split(' ');
            if (arr.Length == 0)
                return s;

            for (int i = 0; i < arr.Length; i++)
            {
                var carr = arr[i].ToCharArray();
                ReverseString(carr);
                arr[i] = String.Join("", carr);
            }

            return String.Join(" ", arr);
        }

        //566. Reshape the Matrix
        public int[][] MatrixReshape(int[][] mat, int r, int c)
        {
            int row = mat.Length;
            int col = mat[0].Length;

            if (row * col != r * c)
                return mat;

            List<int[]> list = new List<int[]>();
            for (int i = 0; i < r; i++)
            {
                var list2 = new List<int>();
                for (int j = 0; j < c; j++)
                {
                    int index = i * c + j;
                    list2.Add(mat[index / col][index % col]);
                }
                list.Add(list2.ToArray());
            }

            return list.ToArray();
        }

        //567. Permutation in String

        public bool CheckInclusion(string s1, string s2)
        {
            if (s1.Length == 1)
                return s2.Contains(s1);

            int[] arr1 = new int[26];

            foreach (var c in s1)
            {
                arr1[c - 'a']++;
            }

            for (int i = 0; i <= s2.Length - s1.Length; i++)
            {
                if (arr1[s2[i] - 'a'] != 0)
                {
                    int[] arr2 = new int[26];

                    int j = 0;
                    while (j <= s1.Length - 1)
                    {
                        arr2[(s2[i + j] - 'a')]++;
                        j++;
                    }

                    if (IsTwoArrayEqual(arr1, arr2))
                        return true;
                }
            }

            return false;
        }

        public bool IsTwoArrayEqual(int[] arr1, int[] arr2)
        {
            if (arr2.Length != arr1.Length)
                return false;

            int i = 0;
            while (i < arr1.Length)
            {
                if (arr1[i] != arr2[i])
                    return false;
                i++;
            }
            return true;
        }
    }
}