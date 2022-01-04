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
            var input = new int[] { 1,2,3,4,5,6,7};
            Console.WriteLine(string.Join(",", input.Select(x => x.ToString())));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            anwser.Rotate(input, 2);
            sw.Stop();
            Console.WriteLine("stop watch ms = " + sw.ElapsedMilliseconds);
            Console.WriteLine(string.Join(",", input.Select(x=>x.ToString())));
            Console.ReadLine();
        }
    }
}
