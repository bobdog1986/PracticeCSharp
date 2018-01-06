using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LeetCodeAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run\r\n****************************\r\n");
            var a = new Anwser();
            //var input = new char[]{ 'a', 'a', 'b', 'b', 'b', 'c', 'c' };
            //var input = new int[] { 1, 4 };
            var input = 25;
            Console.WriteLine(int.MaxValue);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = a.MySqrt(input);
            sw.Stop();
            Console.WriteLine("result="+ result);
            Console.WriteLine("stop watch ms = " + sw.ElapsedMilliseconds);
            //2761ms

            Console.ReadLine();
        }
    }
}
