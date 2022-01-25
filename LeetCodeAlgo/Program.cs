using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;

namespace LeetCodeAlgo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Run\r\n****************************\r\n");
            var anwser = new Anwser();
            var arr1 = new int[] { 1,1,1,3,3,5 };//[[],[],[]]
            var input1 = 3;
            var str1 = "2";
            var str2 = "3";
            //var arr1 = new string[] { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa", "aaaaaaaa", "aaaaaaaaa", "aaaaaaaaaa" };
            var mat1 = new int[][]
            {
                //new int[]{0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new int[]{1,1,1,5 },
                new int[]{1,1,3,3 },
                new int[]{3,5 },
            };
            var grid1 = new char[][]
            {
                new char[]{ 'X', 'O','X','X'},
                new char[]{ 'X', 'O','O','X'},
                new char[]{ 'X', 'X','O','X'},
                new char[]{ 'X', 'O','X','X'},

                //new char[]{'1','1','1'},
                //new char[]{'1','1','1'},
            };
            var mat2 = new int[][] {
                //new int[] { 3, 9 },   new int[]{7, 12},  new int[]{3, 8},
                new int[] { 10, 16 },   new int[]{2, 8},  new int[]{1, 6},new int[]{7,12}
            };
            var listnode = new ListNode(1, new ListNode(1, new ListNode(3, null)));
            //anwser.PrintListNode(listnode);
            Console.WriteLine("Correct Anwser should be : ");
            Console.WriteLine(String.Join("\r\n", mat1.Select(o => String.Join(",", o))));
            //Console.WriteLine(string.Join(",", arr2));
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("**************start watch ms*******");
            sw.Start();
            var tree = new TreeNode(0,
                new TreeNode(-1), null);
            //        var tree = new TreeNode(3,
            //new TreeNode(1, new TreeNode(0), new TreeNode(2)),
            //new TreeNode(5, new TreeNode(4), new TreeNode(6)));
            //var result = anwser.NumDecodings("111111111111111111111111111111111111111111111");
            //Console.WriteLine(String.Join(",", arr1));
            //if (grid1.Length > 0)
            //    Console.WriteLine(String.Join("\r\n", grid1.Select(o => String.Join(",", o))));
            //else
            //    Console.WriteLine("Result count = 0");

            //anwser.Solve(grid1);
            var result = anwser.GenerateParenthesis(input1);
            //var result = anwser.getFactorial(20);
            //var result2 = anwser.IsSameAfterReversals(0);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");
            Console.WriteLine(String.Join(",", result));
            //Console.WriteLine(String.Join(",", arr1));
            //if (result.Count > 0)
            //    Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            //else
            //    Console.WriteLine("!!!Result count = 0");
            //Console.WriteLine($"Result = {result}");
            //Console.WriteLine(String.Join(",", arr1));
            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}