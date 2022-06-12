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
            string matStrInt = "[[5,3],[4,0],[2,1]]";
            var matInt = anwser.buildMatrix(matStrInt);
            string matStrInt2 = "[[9,8],[1,5],[10,12],[18,6],[2,4],[14,3]]";
            var matInt2 = anwser.buildMatrix(matStrInt2);
            string arrStrInt = "[76265,7826,16834,63341,68901,58882,50651,75609]";
            var arrInt = anwser.buildArray(arrStrInt);

            Stopwatch sw = new Stopwatch();
            Console.WriteLine("**************start watch ms*******");
            sw.Start();

            var result = anwser.DistinctNames(new string[] { "coffee", "donuts", "time", "toffee" });

            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");

            LogHelper.log(result);

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}