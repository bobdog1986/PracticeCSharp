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
            var anwser = new Anwser();
            var input = new int[] { 1,2,3,-1,4,-1};
            Console.WriteLine(string.Join(",", input.Select(x => x.ToString())));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = anwser.Tribonacci(34);
            sw.Stop();
            Console.WriteLine("stop watch ms = " + sw.ElapsedMilliseconds);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
