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
    }
}
