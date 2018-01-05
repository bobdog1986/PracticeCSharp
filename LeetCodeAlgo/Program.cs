using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run\r\n****************************\r\n");
            var a = new Anwser();
            //var input = new char[]{ 'a', 'a', 'b', 'b', 'b', 'c', 'c' };
            var input = 2;
            var result = a.CountAndSay(input);
            Console.WriteLine("result="+ result);

            Console.ReadLine();
        }
    }
}
