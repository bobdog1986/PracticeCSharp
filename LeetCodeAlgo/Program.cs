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
            var arr1 = new int[] { 4, 5, 6, 7, 0, 1, 2 };
            //var arr2 = new int[] {-76, 3, 66, -32, 64, 2, -19, -8, -5, -93, 80, -5, -76, -78, 64, 2, 16};
            //var arr1 = new string[] { "acc", "aaa", "aaba" };
            var mat1 = new int[][]
            {
                //new int[]{32768,65536},
                //new int[]{32768,65536},
                new int[]{-19,57,},
                new int[]{-40,-5,},
                //new int[]{6,5,7,},
                //new int[]{0,0,1,0,1,1,1,0,1,1},
            };
            int[][] mat2 = new int[][] {
                //2
                //new int[] { 3, 9 },   new int[]{7, 12},  new int[]{3, 8},
                //new int[] {6 , 8},    new int[]{9, 10},  new int[]{2, 9},
                //new int[] {0 , 9},    new int[]{3, 9},   new int[]{0, 6},
                //new int[]{2, 8},

                //new int[]{int.MinValue, int.MaxValue},new int[]{int.MinValue, int.MaxValue},
                //new int[]{int.MinValue+1, int.MaxValue-1},new int[]{int.MinValue+2, int.MaxValue-2},
                //new int[]{int.MinValue+1, int.MaxValue-1},new int[]{int.MinValue+2, int.MaxValue-2},
                //new int[]{int.MinValue+3, int.MinValue + 10},new int[]{int.MaxValue - 20, int.MaxValue-10},

                //new int[]{-2147483646, -2147483645 },new int[]{2147483646, 2147483647 }

                //4
                //new int[]{4289383, 51220269},new int[]{81692777, 96329692},new int[]{57747793, 81986128},
                //new int[]{19885386, 69645878},new int[]{96516649, 186158070},new int[]{25202362, 75692389},
                //new int[]{83368690, 85888749},new int[]{44897763, 112411689},new int[]{65180540, 105563966},
                //new int[]{4089172, 7544908 },

                //[[35005211,56600579],[94702567,121658996],[36465782,97487312],[78722862,112387985],[45174067,113877202],
                //[1513929,3493731],[15634022,51357080],[69133069,95031236],[59961393,148979849],[28175011,84653053]]
                //3
                //new int[]{35005211, 56600579},new int[]{94702567, 121658996},new int[]{36465782, 97487312},
                //new int[]{78722862, 112387985},new int[]{45174067, 113877202},new int[]{1513929,3493731},
                //new int[]{15634022,51357080},new int[]{69133069,95031236},new int[]{59961393,148979849},new int[]{28175011,84653053 },

                //3
                //new int[] { 1, 2 },   new int[]{3, 4},  new int[]{5, 6},new int[]{5,7}

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

            var result = anwser.FindMinArrowShots(mat2);
            //var result = anwser.getFactorial(20);
            //var result2 = anwser.IsSameAfterReversals(0);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");
            Console.WriteLine(String.Join(",", result));

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