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
            var arr1 = new int[] {1,4,25,10,25 };
            var arr2 = new int[] { 2, 1, 2, 2, 2, 2, 2, 2 };
            int k = 2;
            var val1 = 2;
            var val2 = 100;
            ///uint uintVal =0b10000000_00000000_00000000_00000000;
            var str1 = @"ababbbabbaba";
            var str2 = "**aa*****ba*a*bb**aa*ab****a*aaaaaa***a*aaaa**bbabb*b*b**aaaaaaaaa*a********ba*bbb***a*ba*bb*bb**a*b*bb";
            var word1 = new string[] { "What", "must", "be", "acknowledgment", "shall", "be" };
            var word2 = new string[] { "ABC", "ACB", "ABC", "ACB", "ACB" };
            var mat1 = new int[][]
            {
                new int[]{1,2},
                new int[]{1,3},
                new int[]{2,4},
            };
            var mat2 = new int[][]
            {
                //new int[] { 3, 9 },   new int[]{7, 12},  new int[]{3, 8},
                new int[] { 2,1 },
                //new int[]{3,5},  new int[]{6,7},new int[]{8,10},new int[]{12,16}
            };
            var grid0 = new char[][]
            {
                new char[]{'o', 'a', 'a', 'n'},
                new char[]{'e', 't', 'a', 'e'},
            };
            var grid1 = new char[][]
            {
                //new char[]{ 'X', 'O','X','X'},
                //new char[]{'1','1','1'},
            };

            //var listnode1 = anwser.buildListNode(new int[] { 1, 2,3,4});
            //anwser.printListNode(listnode1);
            //var listnode2 = anwser.buildListNode(new int[] { 1000000, 1000001, 1000002 });
            //anwser.printListNode(listnode2);

            //string bTreeStr = "3,3,6,6,3,5,";
            //var treeNode = anwser.deserializeTree(bTreeStr);
            //anwser.printTree(treeNode)

            //string mat3Str = "[[7,0],[4,4],[7,1],[5,0],[6,1],[5,2]]";
            //var mat3 = anwser.buildMatrix(mat3Str);

            //Console.WriteLine("Correct Anwser should be : ");
            //Console.WriteLine(string.Join("\r\n", mat1.Select(o => string.Join(",", o))));
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("**************start watch ms*******");
            sw.Start();

            var result = anwser.FullJustify(word1,16);

            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");

            LogHelper.log(result);

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}