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
            //var arr1 = new int[] { 5, 7, -24, 12, 13, 2, 3, 12, 5, 6, 35 };//[[},new int[]{],[]] should 6
            //var arr1 = new int[] { -813, 82, -728, -966, -35, 446, -608, -554, -411, 987, -354, -700, -34, 395, -977, 544, -330, 596 };//10
            //should 25
            var arr1 = new int[] { 1, 3, 5, 4, 7 };
            var arr2 = new int[] { -1, 2 };
            var val1 = -2;
            var val2 = 2;
            var str1 = "111111111111111111111111111111111111111111111";
            var str2 = "execution";
            var word1 = new string[] { "ab", "ab" };
            var mat1 = new int[][]
            {
                //new int[]{0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                new int[]{7, 3},new int[]{19, 19},new int[]{-16, 3},new int[]{13, 17},new int[]{-18, 1},new int[]{-18, -17},new int[]{13, -3},new int[]{3, 7},new int[]{-11, 12},new int[]{7, 19},new int[]{19, -12},new int[]{20, -18},new int[]{-16, -15},new int[]{-10, -15},new int[]{-16, -18},new int[]{-14, -1},new int[]{18, 10},new int[]{-13, 8},new int[]{7, -5 }
                ,new int[]{-4, -9},new int[]{-11, 2},new int[]{-9, -9},new int[]{-5, -16},new int[]{10, 14},new int[]{-3, 4},new int[]{1, -20},new int[]{2, 16},new int[]{0, 14},new int[]{-14, 5},new int[]{15, -11},new int[]{3, 11},new int[]{11, -10},new int[]{-1, -7},new int[]{16, 7},new int[]{1, -11},new int[]{-8, -3},new int[]{1, -6},new int[]{19, 7}
                ,new int[]{3, 6},new int[]{-1, -2},new int[]{7, -3},new int[]{-6, -8},new int[]{7, 1},new int[]{-15, 12},new int[]{-17, 9},new int[]{19, -9},new int[]{1, 0},new int[]{9, -10},new int[]{6, 20},new int[]{-12, -4},new int[]{-16, -17},new int[]{14, 3},new int[]{0, -1},new int[]{-18, 9},new int[]{-15, 15},new int[]{-3, -15},new int[]{-5, 20}
                ,new int[]{15, -14},new int[]{9, -17},new int[]{10, -14},new int[]{-7, -11},new int[]{14, 9},new int[]{1, -1},new int[]{15, 12},new int[]{-5, -1},new int[]{-17, -5},new int[]{15, -2},new int[]{-12, 11},new int[]{19, -18},new int[]{8, 7},new int[]{-5, -3},new int[]{-17, -1},new int[]{-18, 13},new int[]{15, -3},new int[]{4, 18 }
                ,new int[]{-14, -15},new int[]{15, 8},new int[]{-18, -12},new int[]{-15, 19},new int[]{-9, 16},new int[]{-9, 14},new int[]{-12, -14},new int[]{-2, -20},new int[]{-3, -13},new int[]{10, -7},new int[]{-2, -10},new int[]{9, 10},new int[]{-1, 7},new int[]{-17, -6},new int[]{-15, 20},new int[]{5, -17},new int[]{6, -6},new int[]{-11, -8 } 

            };
            var grid1 = new char[][]
            {
                //new char[]{ 'X', 'O','X','X'},
                //new char[]{ 'X', 'O','O','X'},
                //new char[]{ 'X', 'X','O','X'},
                //new char[]{ 'X', 'O','X','X'},
                //soduko testcase 1
                new char[]{'.', '.', '9', '7', '4', '8', '.', '.', '.'},
                new char[]{'7', '.', '.', '.', '.', '.', '.', '.', '.'},
                new char[]{'.', '2', '.', '1', '.', '9', '.', '.', '.'},
                new char[]{'.', '.', '7', '.', '.', '.', '2', '4', '.'},
                new char[]{'.', '6', '4', '.', '1', '.', '5', '9', '.'},
                new char[]{'.', '9', '8', '.', '.', '.', '3', '.', '.'},
                new char[]{'.', '.', '.', '8', '.', '3', '.', '2', '.'},
                new char[]{'.', '.', '.', '.', '.', '.', '.', '.', '6'},
                new char[]{'.', '.', '.', '2', '7', '5', '9', '.', '.'},

                //new char[]{'1','1','1'},
                //new char[]{'1','1','1'},
            };

            var grid2 = new char[][]
            {
                //soduko sample 1
                new char[]{'5','3','.','.','7','.','.','.','.'},
                new char[]{'6','.','.','1','9','5','.','.','.'},
                new char[]{'.','9','8','.','.','.','.','6','.'},
                new char[]{'8','.','.','.','6','.','.','.','3'},
                new char[]{'4','.','.','8','.','3','.','.','1'},
                new char[]{'7','.','.','.','2','.','.','.','6'},
                new char[]{'.','6','.','.','.','.','2','8','.'},
                new char[]{'.','.','.','4','1','9','.','.','5'},
                new char[]{'.','.','.','.','8','.','.','7','9'},
                //new char[]{'0','0','0'},
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
            var tree1 = new TreeNode(3,
                new TreeNode(5, new TreeNode(6), new TreeNode(2, new TreeNode(7), new TreeNode(4))),
                new TreeNode(1, new TreeNode(0), new TreeNode(8)));
            var tree2 = new TreeNode(7);
            var tree3 = new TreeNode(6);

            //        var tree = new TreeNode(3,
            //new TreeNode(1, new TreeNode(0), new TreeNode(2)),
            //new TreeNode(5, new TreeNode(4), new TreeNode(6)));
            //var result = anwser.NumDecodings("111111111111111111111111111111111111111111111");
            //Console.WriteLine(String.Join(",", arr1));
            //if (grid1.Length > 0)
            //    Console.WriteLine(String.Join("\r\n", grid1.Select(o => String.Join(",", o))));
            //else
            //    Console.WriteLine("Result count = 0");

            //anwser.SolveSudoku(grid1);

            var result = anwser.LowestCommonAncestor(tree1,tree2,tree3);
            //var result = anwser.getFactorial(20);
            //var result2 = anwser.IsSameAfterReversals(0);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");
            Console.WriteLine(string.Join(",", result.val));
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