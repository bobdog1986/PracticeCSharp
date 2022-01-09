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

        //567. Permutation in String

        public bool CheckInclusion(string s1, string s2)
        {
            if(s1.Length==1)
                return s2.Contains(s1);

            int len1 = s1.Length;
            int len2 = s2.Length;

            int[] arr1 = new int[26];

            //HashSet<char> h1 = new HashSet<char>();
            foreach (var c in s1)
            {
                //h1.Add(c);
                arr1[(int)(c - 'a')]++;
            }

            //HashSet<char> h2 = new HashSet<char>();
            //foreach (var c in s2)
            //{
            //    h2.Add(c);
            //}

            //return h1.SetEquals(h2);

            int[] arr2 = new int[26];

            for (int i = 0; i <= s2.Length-s1.Length; i++)
            {
                //if (s1.Contains(s2[i]))
                if(arr1[s2[i]-'a']!=0)
                {
                    arr2 = new int[26];

                    //h2 = new HashSet<char>();
                    //h2.Add(s2[i]);
                    //forward
                    int j = 0;
                    while (j <= len1 - 1)
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
