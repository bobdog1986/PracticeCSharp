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
            var anwser = new Answer();
            ///!!!*** read from string files
            var strMatLines = File.ReadLines("StringMatrix.txt").ToList();
            //var strArrLines = File.ReadLines("StringArr.txt").ToList();
            var intArrLines = File.ReadLines("IntArr.txt").ToList();
            var intMatLines = File.ReadLines("IntMatrix.txt").ToList();


            //var matStrLine0 = anwser.buildStringMatrix(strMatLines[0]);//build string[][] from file
            var matCharLine0 = anwser.buildCharMatrix(strMatLines[0]);//build char[][] from file due to leetcode using ""

            //var arrStrLine0 = anwser.buildStringArray(strArrLines[0]);
            //var arrCharLine0 = anwser.buildCharArray(strArrLines[0]);

            ///build listnode
            //string listNodeStr1 = "[1,2,3,4]";//intArrLines[0]
            //var listnode1 = anwser.buildListNode(listNodeStr1);
            //anwser.printListNode(listnode1);

            ///build treenode
            //string bTreeStr1 = "[21,7,14,1,1,2,2,3,3]";//intArrLines[0]
            //var treeNode1 = anwser.deserializeTree(bTreeStr1);
            //anwser.printTree(treeNode1)

            ///build int[][] and int[] from string
            string matStrInt1 = "[[1,10],[3,3]]";
            var matInt1 = anwser.buildMatrix(matStrInt1);
            //string matStrInt2 = "[[1,3,1,15],[1,3,3,1]]";
            //var matInt2 = anwser.buildMatrix(matStrInt2);
            string arrStrInt1 = "[3,3,2]";
            var arrInt1 = anwser.buildArray(arrStrInt1);
            //string arrStrInt2 = "[-10, -8, -7, -6]";
            //var arrInt2 = anwser.buildArray(arrStrInt2);

            ///build int[][] and int[] from file
            //var matIntLine0 = anwser.buildMatrix(intMatLines[0]);
            //var matIntLine1 = anwser.buildMatrix(intMatLines[1]);
            //var arrIntLine0 = anwser.buildArray(intArrLines[0]);
            //var arrIntLine1 = anwser.buildArray(intArrLines[1]);

            Console.WriteLine("**************start watch ms*******");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var result = anwser.MinimumLengthEncoding(new string[] { "atime", "time", "btime" });

            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");

            LogHelper.log(result);

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}