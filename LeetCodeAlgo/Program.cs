using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LeetCodeAlgo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run\r\n****************************\r\n");
            var anwser = new Anwser();
            var arr1 = new int[] { 3,4,2};
            //var arr2 = new int[] { 9, 4, 9, 8, 4 };
        //    var arr3 = new int[][] 
        //    {
        //        new int[]{114,114,116,118,120,120,120,122,122,124,124,126,127,128,129,131,133,133,135,137,139,139 },
        //        //new int[]{1, 3 },
        //        //new int[]{-8,-7,-5,-3,-3,-1,1}, 
        //        //new int[] { 2,2,2,3,3,5,7 },
        //        //new int[] {8,9,11,11,13,15,17 },
        //        //new int[] { 18,18,18,20,20,20,21},
        //        //new int[] {23,24,26,26,26,27,27 },
        //        //new int[] {28,29,29,30,32,32,34 },
        //};
            //var listnode=new ListNode(1,new ListNode(2));
            //anwser.PrintListNode(listnode);
            Console.WriteLine(string.Join(",", arr1));
            //Console.WriteLine(string.Join(",", arr2));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //[3,9,20,null,null,15,7]
            var root =new TreeNode(3,new TreeNode(9),new TreeNode(20,new TreeNode(15),new TreeNode(7)));
            //var result = anwser.CheckInclusion("hello", "ooolleoooleh");//ooolleoooleh
            //var result = anwser.CheckInclusion("abc", "ccccbbbbaaaa");//ooolleoooleh
            var result = anwser.DeleteAndEarn(arr1);//ooolleoooleh
            //"rvwrk"
            //"lznomzggwrvrkxecjaq"
            //anwser.PrintListNode(result);
            //var result =anwser.Intersect(arr1,arr2);
            //anwser.Merge(arr1,3,arr2,3);
            sw.Stop();
            Console.WriteLine("stop watch ms = " + sw.ElapsedMilliseconds);
            Console.WriteLine(result);
            //Console.WriteLine(String.Join(",",result));
            //Console.WriteLine(String.Join(",", arr1));
            Console.ReadLine();
        }
    }
}
