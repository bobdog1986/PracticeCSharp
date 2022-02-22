﻿using System;
using System.Diagnostics;
using System.Linq;
using LeetCodeAlgo.AnwserStructs;

namespace LeetCodeAlgo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Run\r\n****************************\r\n");
            var anwser = new Anwser();
            //var arr1 = new int[] { 5, 7, -24, 12, 13, 2, 3, 12, 5, 6, 35 };
            var arr1 = new int[] { 432,43243 };
            var arr2 = new int[] { -1, 2 };
            int k = 2;
            var val1 = 4;
            var val2 = 2;
            var str1 = "hot";
            var str2 = "dog";
            var word1 = new string[] { "hot", "dog"};
            var word2 = new string[] { "ABC", "ACB", "ABC", "ACB", "ACB" };
            var mat1 = new int[][]
            {
                new int[]{0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            };
            var mat2 = new int[][] {
                //new int[] { 3, 9 },   new int[]{7, 12},  new int[]{3, 8},
                new int[] { 1,0 },   new int[]{2,0},  new int[]{3,1},new int[]{3,2},
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

            var listnode = anwser.buildListNode(new int[] {4,3,2,1 });
            //anwser.printListNode(listnode);
            //Console.WriteLine("Correct Anwser should be : ");
            //Console.WriteLine(string.Join("\r\n", mat1.Select(o => string.Join(",", o))));
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("**************start watch ms*******");
            sw.Start();
            //string bTreeStr = "1,2,3";
            //var treeNode = anwser.deserializeTree(bTreeStr);
            ////var treeStr= anwser.serializeTree(treeNode);
            //anwser.printTree(treeNode);

            //Console.WriteLine(String.Join(",", arr1));
            //if (grid1.Length > 0)
            //    Console.WriteLine(String.Join("\r\n", grid1.Select(o => String.Join(",", o))));
            //else
            //    Console.WriteLine("Result count = 0");

            //anwser.SolveSudoku(grid1);
            ////var result = anwser.IsMatch("baabbbaccbccacacc","c*..b*a*a.*a..*c");
            uint uintVal =0b10000000_00000000_00000000_00000000;
            var result = anwser.FindOrder(val1,mat2);
            //var result2 = anwser.IsSameAfterReversals(0);
            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");
            //Console.WriteLine(string.Join(",", result.val));
            //anwser.printListNode(result);
            Console.WriteLine($"Result = {result}");

            //Console.WriteLine(String.Join(",", arr1));
            //if (result.Count > 0)
            //    Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            //else
            //    Console.WriteLine("!!!Result count = 0");
            //Console.WriteLine(String.Join(",", arr1));
            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}