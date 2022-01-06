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
            var arr1 = new int[] { 2, 7, 9, 3, 1 };
            //var arr2 = new int[] { 9, 4, 9, 8, 4 };

            Console.WriteLine(string.Join(",", arr1));
            //Console.WriteLine(string.Join(",", arr2));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //[3,9,20,null,null,15,7]
            var root =new TreeNode(3,new TreeNode(9),new TreeNode(20,new TreeNode(15),new TreeNode(7)));
            var result = anwser.Rob(arr1);
            //var result =anwser.Intersect(arr1,arr2);
            //anwser.Merge(arr1,3,arr2,3);
            sw.Stop();
            Console.WriteLine("stop watch ms = " + sw.ElapsedMilliseconds);
            //Console.WriteLine(result);
            Console.WriteLine(String.Join(",",result));
            //Console.WriteLine(String.Join(",", arr1));
            Console.ReadLine();
        }
    }
}
