using System;
using System.Diagnostics;
using System.Linq;
using LeetCodeAlgo.Design;

namespace LeetCodeAlgo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Run\r\n****************************\r\n");
            var anwser = new Answer();
            var arr1 = new int[] {1, 6, 10, 8, 7, 3, 2};
            var arr2 = new int[] {4,3,5,1,2};
            int k = 2;
            var val1 = 3;
            var val2 = 100;
            ///uint uintVal =0b10000000_00000000_00000000_00000000;
            //var str1 = "())(((()m)(";
            var str1 = @"ababbbabbaba";
            var str2 = "**aa*****ba*a*bb**aa*ab****a*aaaaaa***a*aaaa**bbabb*b*b**aaaaaaaaa*a********ba*bbb***a*ba*bb*bb**a*b*bb";
            //var str1 = "aaabbbaabaaaaababaabaaabbabbbbbbbbaabababbabbbaaaaba";
            //var str2 = "a*******b";
            //var str1 = "abcabczzzde";
            //var str2 = "*abc???de*";
            var word1 = new string[] { "0201", "0101", "0102", "1212", "2002" };
            var word2 = new string[] { "ABC", "ACB", "ABC", "ACB", "ACB" };
            var mat1 = new int[][]
            {
                new int[]{1,2},
                new int[]{1,3},
                new int[]{2,4},

                //new int[]{0,1},
                //new int[]{1,3},
                //new int[]{2,3},
                //new int[]{4,0},
                //new int[]{4,5},
            };
            var mat2 = new int[][] {
                //new int[] { 3, 9 },   new int[]{7, 12},  new int[]{3, 8},
                new int[] { 2,1 },
                //new int[]{3,5},  new int[]{6,7},new int[]{8,10},new int[]{12,16}
            };
            var grid0 = new char[][]
            {
                new char[]{'o', 'a', 'a', 'n'},
                new char[]{'e', 't', 'a', 'e'},
                new char[]{'i', 'h', 'k', 'r' },
                new char[]{'i', 'f', 'l', 'v' },
            };


            var grid1 = new char[][]
            {
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
            };

            //var listnode1 = anwser.buildListNode(new int[] { 1, 2,3,4});
            //anwser.printListNode(listnode1);
            //var listnode2 = anwser.buildListNode(new int[] { 1000000, 1000001, 1000002 });
            //anwser.printListNode(listnode2);

            //Console.WriteLine("Correct Anwser should be : ");
            //Console.WriteLine(string.Join("\r\n", mat1.Select(o => string.Join(",", o))));
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("**************start watch ms*******");
            sw.Start();
            //string bTreeStr = "3,3,6,6,3,5,";
            //var treeNode = anwser.deserializeTree(bTreeStr);
            //anwser.printTree(treeNode);

            //Console.WriteLine(String.Join(",", arr1));

            //if (grid1.Length > 0)
            //    Console.WriteLine(String.Join("\r\n", grid1.Select(o => String.Join(",", o))));
            //else
            //    Console.WriteLine("Result count = 0");

            //anwser.SolveSudoku(grid1);
            //Console.WriteLine("input string is = "+ str1);
            // anwser.GcdOfStrings(arr1);
            var result = anwser.MinCut(str1);
            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");
            //Console.WriteLine(string.Join(",", result.val));
            //anwser.printTree(result);
            //anwser.printListNode(result);
            //Console.WriteLine($"Result = {result}");

            Console.WriteLine(String.Join(",", result));
            //Console.WriteLine(String.Join(",", arr1));

            //if (result.Count() > 0)
            //{
            //    //Console.WriteLine(String.Join("\r\n\r\n", result.Select(o => String.Join("\r\n", o))));
            //    Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            //}
            //else Console.WriteLine("!!!Result count = 0");

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}