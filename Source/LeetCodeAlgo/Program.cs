﻿using System;
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
            //var strMatLines = File.ReadLines("StringMatrix.txt").ToList();
            List<string> strArrLines = File.ReadLines("StringArr.txt").ToList();
            List<string> intArrLines = File.ReadLines("IntArr.txt").ToList();
            //List<string> intMatLines = File.ReadLines("IntMatrix.txt").ToList();

            //var matStrLine0 = answer.buildStringMatrix(strMatLines[0]);//build string[][] from file
            //var matCharLine0 = answer.buildCharMatrix(strMatLines[0]);//build char[][] from file due to leetcode using ""
            var arrStrLine0 = answer.buildStringArray(strArrLines[0]);
            //var arrCharLine0 = answer.buildCharArray(strArrLines[0]);

            ///build ListNode, auto print
            //string listNodeStr1 = "[5,2,6,3,9,1,7,3,8]";//intArrLines[0]
            //ListNode listnode1 = answer.buildListNode(listNodeStr1);

            ///build TreeNode, auto print
            //string bTreeStr1 = "[5,1,2,3,null,6,4]";//intArrLines[0];
            //TreeNode treeNode1 = answer.deserializeTree(bTreeStr1);

            ///build int[][] and int[] from string
            string matStrInt1 = " [[1,2],[3,5],[2,2]]";
            var mat1 = answer.buildMatrix(matStrInt1);
            //string matStrInt2 = "[[1,3,1,15],[1,3,3,1]]";
            //var matInt2 = answer.buildMatrix(matStrInt2);
            string arrStrInt1 = "[62,100,4]";
            var arr1 = answer.buildArray(arrStrInt1);
            string arrStrInt2 = "[4,5,2,6,7,3,1]";
            var arr2 = answer.buildArray(arrStrInt2);

            ///build int[][] and int[] from file
            //var matIntLine0 = answer.buildMatrix(intMatLines[0]);
            //var matIntLine1 = answer.buildMatrix(intMatLines[1]);
            var arrIntLine0 = answer.buildArray(intArrLines[0]);
            //var arrIntLine1 = answer.buildArray(intArrLines[1]);

            Console.WriteLine("**************start watch ms*******");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var result = answer.MaxAverageRatio(mat1,2);

            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");

            LogHelper.log(result);

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}