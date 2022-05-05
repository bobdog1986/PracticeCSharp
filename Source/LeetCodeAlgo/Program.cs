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
            var arr1 = new int[] {1,2,3,4,5,90 };
            var arr2 = new int[] { 2, 1, 2, 2, 2, 2, 2, 2 };
            int k = 2;
            var val1 = 2;
            var val2 = 100;
            ///uint uintVal =0b10000000_00000000_00000000_00000000;
            var str1 = @"ababbbabbaba";
            var str2 = "**aa*****ba*a*bb**aa*ab****a*aaaaaa***a*aaaa**bbabb*b*b**aaaaaaaaa*a********ba*bbb***a*ba*bb*bb**a*b*bb";
            var word1 = new string[] { "01", "10" };
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

            var llist1=new List<IList<string>> { new List<string> {"a","e" }, new List<string> { "b", "e" } };
            var llist2 = new List<IList<string>> { new List<string> { "a", "b" }, new List<string> { "e", "e" }, new List<string> { "x", "x" } };

            var listnode1 = anwser.buildListNode(new int[] { 1, 3, 2, 2, 3, 2, 2, 2, 7});
            //anwser.printListNode(listnode1);
            //var listnode2 = anwser.buildListNode(new int[] { 1000000, 1000001, 1000002 });
            //anwser.printListNode(listnode2);

            string bTreeStr = "[4,1,6,0,2,5,7,null,null,null,3,null,null,null,8]";
            var treeNode = anwser.deserializeTree(bTreeStr);
            //anwser.printTree(treeNode)

            string mat3Str = "[[1,0,0,0],[0,0,0,0],[0,0,2,-1]]";
            var mat3 = anwser.buildMatrix(mat3Str);
            string arrStr3 = "[22,25,39,3,45,45,12,17,32,9]";
            var arr3 = anwser.buildArray(arrStr3);
            //Console.WriteLine("Correct Anwser should be : ");
            //Console.WriteLine(string.Join("\r\n", mat1.Select(o => string.Join(",", o))));
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("**************start watch ms*******");
            sw.Start();

            var result = anwser.CountCollisions("LLRLRLLSLRLLSLSSSS");

            sw.Stop();
            Console.WriteLine($"**********stop watch sec ={sw.Elapsed.TotalSeconds}*******");
            Console.WriteLine("***********Output Result*******");

            LogHelper.log(result);

            Console.WriteLine("=========Finish!========");
            Console.ReadLine();
        }
    }
}