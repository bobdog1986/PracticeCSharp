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
            var arr1 = new int[] {2,2,2,1,1,1,0,0,0};
            //var arr2 = new int[] {-76, 3, 66, -32, 64, 2, -19, -8, -5, -93, 80, -5, -76, -78, 64, 2, 16};
            //var arr1 = new string[] { "acc", "aaa", "aaba" };
            var mat1 = new int[][]
            {
                //new int[]{32768,65536},
                //new int[]{32768,65536},
                new int[]{1,3,5,7},
                new int[]{10,11,16,20},
                new int[]{23,30,34,60},
                //new int[]{6,5,7,},
                //new int[]{0,0,1,0,1,1,1,0,1,1},
            };
            int[][] mat2 = new int[][] {
                //2
                //new int[] { 3, 9 },   new int[]{7, 12},  new int[]{3, 8},
                //new int[] {6 , 8},    new int[]{9, 10},  new int[]{2, 9},
                //new int[] {0 , 9},    new int[]{3, 9},   new int[]{0, 6},
                //new int[]{2, 8},
                //2
                new int[] { 10, 16 },   new int[]{2, 8},  new int[]{1, 6},new int[]{7,12}
            };

            var listnode = new ListNode(1, new ListNode(1, new ListNode(3, null)));//new ListNode(4,new ListNode(5)))));
            //anwser.PrintListNode(listnode);
            //Console.WriteLine(String.Join("\r\n", mat1.Select(o => String.Join(",", o))));
            //Console.WriteLine(string.Join(",", arr2));
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("**************start watch ms*******");
            sw.Start();
            //[5,4,6,null,null,3,7]
            var tree = new TreeNode(0,
                new TreeNode(-1), null);
            //        var tree = new TreeNode(3,
            //new TreeNode(1, new TreeNode(0), new TreeNode(2)),
            //new TreeNode(5, new TreeNode(4), new TreeNode(6)));
            //var result = anwser.NumDecodings("111111111111111111111111111111111111111111111");
            Console.WriteLine(String.Join(",", arr1));
            anwser.SortColors(arr1);
            // var result = anwser.SortColors(arr1);
            //var result = anwser.getFactorial(20);
            //var result2 = anwser.IsSameAfterReversals(0);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");
            //Console.WriteLine(String.Join(",", result));
            Console.WriteLine(String.Join(",", arr1));

            //Console.WriteLine($"Result = {result}");
            //Console.WriteLine(result.val.ToString() + result.next.val);
            //if (result.Count > 0)
            //    Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            //else
            //    Console.WriteLine("Result count = 0");
            //Console.WriteLine(String.Join(",", arr1));

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}