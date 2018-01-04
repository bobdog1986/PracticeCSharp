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
            int data = 333;
            Console.WriteLine("result="+ a.IntToRoman(data));
            Console.ReadLine();
        }
    }
}
