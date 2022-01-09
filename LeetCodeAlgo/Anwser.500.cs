using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //509. Fibonacci Number
        public int Fib(int n)
        {
            if(n == 0)
            {
                return 0;
            }
            if(n == 1)
            {
                return 1;

            }

            return Fib(n - 1) + Fib(n-2);
        }

        //557. Reverse Words in a String III
        public string ReverseWords(string s)
        {
            var arr=s.Split(' ');
            if(arr.Length == 0)
                return s;

            for(int i =0;i<arr.Length;i++)
            {
                var carr= arr[i].ToCharArray();
                ReverseString(carr);
                arr[i] = String.Join("",carr);
            }

            return String.Join(" ", arr);
        }
        //566. Reshape the Matrix
        public int[][] MatrixReshape(int[][] mat, int r, int c)
        {
            int row=mat.Length;
            int col=mat[0].Length;

            if (row * col != r * c)
                return mat;

            List<int[]> list=new List<int[]>();
            for (int i = 0; i < r; i++)
            {
                var list2=new List<int>();
                for (int j = 0; j < c; j++)
                {
                    int index = i * c + j ;
                    list2.Add(mat[index / col][index % col]);
                }
                list.Add(list2.ToArray());
            }

            return list.ToArray();
        }


        //567. Permutation in String

        public bool CheckInclusion(string s1, string s2)
        {
            if(s1.Length==1)
                return s2.Contains(s1);

            int[] arr1 = new int[26];

            foreach (var c in s1)
            {
                arr1[c - 'a']++;
            }

            for (int i = 0; i <= s2.Length-s1.Length; i++)
            {
                if(arr1[s2[i]-'a']!=0)
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

        public bool IsTwoArrayEqual(int[] arr1,int[] arr2)
        {
            if (arr2.Length != arr1.Length)
                return false;

            int i = 0;
            while (i < arr1.Length)
            {
                if(arr1[i] != arr2[i])
                    return false;
                i++;
            }
            return true;
        }
    }
}
