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
            Answer answer = new Answer();
            ///!!!*** read from string files
            List<string> strMatLines = File.ReadLines("StringMatrix.txt").ToList();
            List<string> strArrLines = File.ReadLines("StringArr.txt").ToList();
            List<string> intArrLines = File.ReadLines("IntArr.txt").ToList();
            List<string> intMatLines = File.ReadLines("IntMatrix.txt").ToList();

            //var matStrLine0 = answer.buildStringMatrix(strMatLines[0]);//build string[][] from file
            char[][] matCharLine0 = answer.buildCharMatrix(strMatLines[0]);//build char[][] from file due to leetcode using ""
            string[] arrStrLine0 = answer.buildStringArray(strArrLines[0]);
            //string[] arrStrLine1 = answer.buildStringArray(strArrLines[1]);
            //char[] arrCharLine0 = answer.buildCharArray(strArrLines[0]);

            ///build ListNode, auto print
            //string listNodeStr1 = "[0,1,2]";//intArrLines[0]
            //ListNode listnode1 = answer.buildListNode(listNodeStr1);

            ///build TreeNode, auto print
            //string bTreeStr1 = "[5,1,2,3,null,6,4]";//intArrLines[0];
            //TreeNode treeNode1 = answer.deserializeTree(bTreeStr1);

            ///build int[][] and int[] from string
            string matStrInt1 = " [[0,1],[1,2],[1,3],[3,4]]";
            int[][] mat1 = answer.buildMatrix(matStrInt1);
            //string matStrInt2 = "[[1,3,1,15],[1,3,3,1]]";
            //int[][] mat2 = answer.buildMatrix(matStrInt2);
            string arrStrInt1 = "[1,10000000]";
            int[] arr1 = answer.buildArray(arrStrInt1);
            string arrStrInt2 = "[8,2,6,10]";
            int[] arr2 = answer.buildArray(arrStrInt2);

            ///build int[][] and int[] from file
            //int[][] matIntLine0 = answer.buildMatrix(intMatLines[0]);
            //int[][] matIntLine1 = answer.buildMatrix(intMatLines[1]);
            //int[] arrIntLine1 = answer.buildArray(intArrLines[1]);

            Console.WriteLine("**************start watch ms*******");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var result = answer.MaximumGap(arr1);

            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");

            LogHelper.log(result);

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}