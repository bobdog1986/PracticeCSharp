using System;
using System.Diagnostics;
using System.Linq;

namespace LeetCodeAlgo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Run\r\n****************************\r\n");
            var anwser = new Anwser();
            //var arr1 = new int[] { 5, 7, -24, 12, 13, 2, 3, 12, 5, 6, 35 };//[[],[],[]] should 6
            //var arr1 = new int[] { -813, 82, -728, -966, -35, 446, -608, -554, -411, 987, -354, -700, -34, 395, -977, 544, -330, 596 };//10
            //should 25
            var arr1 = new int[] { -813, 82, -728, -82, -432, 887, -551, 324, -315, 306, -164,
                -499, -873, -613, 932, 177, 61, 52, 1000, -710, 372, -306, -584, -332, -500,
                407, 399, -648, 290, -866, 222, 562, 993, -338, -590, 303, -16, -134, 226,
                -648, 909, 582, 177, 899, -343, 55, 629, 248, 333, 1, -921, 143, 629, 981,
                -435, 681, 844, 349, 613, 457, 797, 695, 485, 15, 710, -450, -775, 961, -445,
                -905, 466, 942, 995, -289, -397, 434, -14, 34, -903, 314, 862, -441, 507, -966,
                525, 624, -706, 39, 152, 536, 874, -364, 747, -35, 446, -608, -554, -411, 987,
                -354, -700, -34, 395, -977, 544, -330, 596, 335, -612, 28, 586, 228, -664, -841,
                -999, -100, -620, 718, 489, 346, 450, 772, 941, 952, -560, 58, 999, -879, 396,
                -101, 897, -1000, -566, -296, -555, 938, 941, 475, -260, -52, 193, 379, 866, 226,
                -611, -177, 507, 910, -594, -856, 156, 71, -946, -660, -716, -295, -927, 148, 620,
                201, 706, 570, -659, 174, 637, -293, 736, -735, 377, -687, -962, 768, 430, 576, 160,
                577, -329, 175, 51, 699, -113, 950, -364, 383, 5, 748, -250, -644, -576, -227, 603,
                832, -483, -237, 235, 893, -336, 452, -526, 372, -418, 356, 325, -180, 134, -698 };
            var arr2 = new int[] { -1,2 };
            var val1 = -2;
            var val2 = 2;
            var str1 = "intention";
            var str2 = "execution";
            var word1 = new string[] { "ab", "ab" };
            var mat1 = new int[][]
            {
                //new int[]{0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new int[]{1,2,3 },
                new int[]{4,5,6 },
                new int[]{7,8,9},
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
            var listnode = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));
            //anwser.PrintListNode(listnode);
            Console.WriteLine("Correct Anwser should be : ");
            Console.WriteLine(string.Join("\r\n", mat1.Select(o => string.Join(",", o))));
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

            //anwser.LongestValidParentheses(str1);

            var result = anwser.CircularArrayLoop(arr2);
            //var result = anwser.getFactorial(20);
            //var result2 = anwser.IsSameAfterReversals(0);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");
            Console.WriteLine(string.Join(",", result));
            //anwser.PrintListNode(result);

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