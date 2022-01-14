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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run\r\n****************************\r\n");
            var anwser = new Anwser();
            var arr1 = new int[] { 1,1,2,3,4,4,4 };
            var arr2 = new int[] { 3,4 };
            //var arr1 = "1ac1b";
            var mat1 = new int[][]
            {
                new int[]{2},
                new int[]{3,4,},
                new int[]{6,5,7,},
                //new int[]{0,0,1,0,1,1,1,0,1,1},
                //new int[]{1,1,1,1,0,1,1,1,1,1},
                //new int[]{1,1,1,1,1,0,0,0,1,1},
                //new int[]{1,0,1,0,1,1,1,0,1,1},
                //new int[]{0,0,1,1,1,0,1,1,1,1},
                //new int[]{1,0,1,1,1,1,1,1,1,1},
                //new int[]{1,1,1,1,0,1,0,1,0,1},
                //new int[]{0,1,0,0,0,1,0,0,1,1},
                //new int[]{1,1,1,0,1,1,0,1,0,1},
                //new int[]{1,0,1,1,1,0,1,1,1,0},
                                //new int[]{-8,-7,-5,-3,-3,-1,1}, 
                //new int[] { 2,2,2,3,3,5,7 },
                //new int[] {8,9,11,11,13,15,17 },
                //new int[] { 18,18,18,20,20,20,21},
                //new int[] {23,24,26,26,26,27,27 },
                //new int[] {28,29,29,30,32,32,34 },
            };
            var listnode = new ListNode(1, new ListNode(1, new ListNode(3, null)));//new ListNode(4,new ListNode(5)))));
            //anwser.PrintListNode(listnode);
            Console.WriteLine(String.Join("\r\n", mat1.Select(o => String.Join(",", o))));

            //Console.WriteLine(string.Join(",", arr1));
            //Console.WriteLine(string.Join(",", arr2));
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("**************");
            Console.WriteLine("start watch ms");
            sw.Start();
            var root =new TreeNode(3,
                new TreeNode(20, new TreeNode(15), new TreeNode(7)),
                new TreeNode(20,new TreeNode(7),new TreeNode(15)));
            //var result = anwser.MaxSubarraySumCircular(arr1);//ooolleoooleh
            //anwser.PrintListNode(result);
            var result = anwser.NumIdenticalPairs(arr1);
            //var result1 = anwser.IsSameAfterReversals(1230);
            //var result2 = anwser.IsSameAfterReversals(0);
            sw.Stop();
            Console.WriteLine("stop watch ms = " + sw.ElapsedMilliseconds);
            Console.WriteLine("***");
            Console.WriteLine(result);
            //Console.WriteLine(result1);
            //Console.WriteLine(result2);
            //Console.WriteLine(result.val.ToString() + result.next.val);
            //if(result.Count > 0)
            //    Console.WriteLine(String.Join("\r\n", result.Select(o=> String.Join(",",o))));
            //else
            //    Console.WriteLine("Result count = 0");
            //Console.WriteLine(String.Join(",", arr1));

            Console.WriteLine("=====Finish!=====");
            Console.ReadLine();
        }
    }
}
