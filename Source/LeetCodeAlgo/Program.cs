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
            var answer = new Answer();
            ///!!!*** read from string files
            //var strMatLines = File.ReadLines("StringMatrix.txt").ToList();
            //var strArrLines = File.ReadLines("StringArr.txt").ToList();
            var intArrLines = File.ReadLines("IntArr.txt").ToList();
            //var intMatLines = File.ReadLines("IntMatrix.txt").ToList();

            //var matStrLine0 = answer.buildStringMatrix(strMatLines[0]);//build string[][] from file
            //var matCharLine0 = answer.buildCharMatrix(strMatLines[0]);//build char[][] from file due to leetcode using ""
            //var arrStrLine0 = answer.buildStringArray(strArrLines[0]);
            //var arrCharLine0 = answer.buildCharArray(strArrLines[0]);

            ///build listnode
            //string listNodeStr1 = "[1,2,3,4]";//intArrLines[0]
            //var listnode1 = answer.buildListNode(listNodeStr1);
            //answer.printListNode(listnode1);

            ///build treenode
            string bTreeStr1 = "[5,1,2,3,null,6,4]";//intArrLines[0];
            var treeNode1 = answer.deserializeTree(bTreeStr1);
            //Answer.printTree(treeNode1);

            ///build int[][] and int[] from string
            string matStrInt1 = "[[1,2,3,4]]";
            var matInt1 = answer.buildMatrix(matStrInt1);
            //string matStrInt2 = "[[1,3,1,15],[1,3,3,1]]";
            //var matInt2 = answer.buildMatrix(matStrInt2);
            string arrStrInt1 = "[3,3,2]";
            var arrInt1 = answer.buildArray(arrStrInt1);
            //string arrStrInt2 = "[-10, -8, -7, -6]";
            //var arrInt2 = answer.buildArray(arrStrInt2);

            ///build int[][] and int[] from file
            //var matIntLine0 = answer.buildMatrix(intMatLines[0]);
            //var matIntLine1 = answer.buildMatrix(intMatLines[1]);
            var arrIntLine0 = answer.buildArray(intArrLines[0]);
            //var arrIntLine1 = answer.buildArray(intArrLines[1]);

            Console.WriteLine("**************start watch ms*******");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var result = answer.FindDiagonalOrder(matInt1);

            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");

            LogHelper.log(result);

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}