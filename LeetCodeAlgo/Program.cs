﻿using System;
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
            //var arr1 = new int[] {332484035, 524908576, 855865114, 632922376, 222257295, 690155293, 112677673, 679580077, 337406589, 290818316, 877337160, 901728858, 679284947, 688210097, 692137887, 718203285, 629455728, 941802184};
            //var arr2 = new int[] {-76, 3, 66, -32, 64, 2, -19, -8, -5, -93, 80, -5, -76, -78, 64, 2, 16};
            var arr1 = new int[] { 312884470 };//{ 3,6,7,11 };
            //var input1 = 823855818;
            var input1 = 312884469;//result=23
            //var arr1 = new string[] { "acc", "aaa", "aaba" };
            var mat1 = new int[][]
            {
                //new int[]{32768,65536},
                //new int[]{32768,65536},
                new int[]{1,3,1,2,3},
                new int[]{1,5,3,1,3},
                new int[]{4,2,3,1,3},
                new int[]{4,2,3,1,3},
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
            //anwser.SortColors(arr1);
            var result = anwser.MinEatingSpeed(arr1, input1);
            //var result = anwser.getFactorial(20);
            //var result2 = anwser.IsSameAfterReversals(0);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");
            Console.WriteLine(String.Join(",", result));
            //Console.WriteLine(String.Join(",", arr1));

            //Console.WriteLine($"Result = {result}");
            //Console.WriteLine(result.val.ToString() + result.next.val);
            //if (result.Length > 0)
            //    Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            //else
            //    Console.WriteLine("Result count = 0");
            //Console.WriteLine(String.Join(",", arr1));

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}