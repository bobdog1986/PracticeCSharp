using System;
using System.Diagnostics;
using System.Linq;
using LeetCodeAlgo.Design;
using System.IO;

namespace LeetCodeAlgo
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Run\r\n****************************\r\n");
            //read data from file
            string filename = "Input.txt";
            var lines = File.ReadLines(filename).ToList();

            var anwser = new Answer();
            uint uintVal =0b10000000_00000000_00000000_00000000;
            var str1 = @"ababbbabbaba";
            var word1 = new string[] { "01", "10" };

            ///build listnode
            //var listnode1 = anwser.buildListNode(new int[] { 1, 3, 2, 2, 3, 2, 2, 2, 7});
            //anwser.printListNode(listnode1);

            ///build treenode
            //string bTreeStr = "[21,7,14,1,1,2,2,3,3]";
            //var treeNode = anwser.deserializeTree(bTreeStr);
            //anwser.printTree(treeNode)

            ///build string[][] and int[] from string
            //string matStrStr = lines[0];
            //var matStr = anwser.buildMatrix(matStrInt);
            //string arrStrStr = lines[0];
            //var arrStr= anwser.buildArray(arrStrInt);

            ///build char[][] and char[] due to leetcode using double quote "" wrap a char
            //string matStrChar = lines[0];
            //var matChar = anwser.buildCharMatrix(matStrChar);
            //string arrStrChar = lines[0];
            //var arrChar = anwser.buildCharArray(arrStrChar);

            ///build int[][] and int[] from string
            string matStrInt = "[[0,1,1],[1,1,0],[1,1,0]]";
            var matInt = anwser.buildMatrix(matStrInt);
            string arrStrInt = "[1,5,11,5]";
            var arrInt = anwser.buildArray(arrStrInt);

            Stopwatch sw = new Stopwatch();
            Console.WriteLine("**************start watch ms*******");
            sw.Start();

            var result = anwser.MatchReplacement();

            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");

            LogHelper.log(result);

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}