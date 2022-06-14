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

            uint uintVal =0b10000000_00000000_00000000_00000000;

            ///!!!*** read from string files

            //var strMatLines = File.ReadLines("StringMat.txt").ToList();
            //var strArrLines = File.ReadLines("StringArr.txt").ToList();

            ///build string[][] from file
            //string matStrStr1 = strMatLines[0];
            //var matStr1 = anwser.buildStringMatrix(matStrStr1);

            ///build string[] from file
            //string arrStrStr1 = strArrLines[0];
            //var arrStr1 = anwser.buildStringArray(arrStrStr1);

            ///build char[][] and char[] by file, due to leetcode using double quote "" wrap a char
            //string matStrChar1 = strMatLines[0];
            //var matChar1 = anwser.buildCharMatrix(matStrChar1);
            //string arrStrChar1 = strArrLines[0];
            //var arrChar1 = anwser.buildCharArray(arrStrChar1);

            ///!!!*** read from int files

            var intArrLines = File.ReadLines("IntArr.txt").ToList();
            //var intMatLines = File.ReadLines("IntMat.txt").ToList();

            ///build listnode
            //string listNodeStr1 = "[1,2,3,4]";//intArrLines[0]
            //var listnode1 = anwser.buildListNode(listNodeStr1);
            //anwser.printListNode(listnode1);

            ///build treenode
            //string bTreeStr1 = "[21,7,14,1,1,2,2,3,3]";//intArrLines[0]
            //var treeNode1 = anwser.deserializeTree(bTreeStr1);
            //anwser.printTree(treeNode1)

            ///build int[][] and int[] from string
            string matStrInt1 = "[[78,96,64],[37,100,30]]";
            var matInt1 = anwser.buildMatrix(matStrInt1);
            string arrStrInt1 = "[1,2,0,1,2]";
            var arrInt1 = anwser.buildArray(arrStrInt1);

            ///build int[][] and int[] from file
            //string matStrInt2 = intMatLines[0];
            //var matInt2 = anwser.buildMatrix(matStrInt2);
            string arrStrInt2 = intArrLines[0];
            var arrInt2 = anwser.buildArray(arrStrInt2);

            Console.WriteLine("**************start watch ms*******");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var result = anwser.MaximumTop(arrInt2,34);

            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");

            LogHelper.log(result);

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}