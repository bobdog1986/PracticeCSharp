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
            var input = new int[] { 1,3,5};
            Console.WriteLine(string.Join(",", input.Select(x => x.ToString())));
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //[3,9,20,null,null,15,7]
            var root =new TreeNode(3,new TreeNode(9),new TreeNode(20,new TreeNode(15),new TreeNode(7)));
            var result = anwser.MaxDepth(root);
            sw.Stop();
            Console.WriteLine("stop watch ms = " + sw.ElapsedMilliseconds);
            Console.WriteLine(result);
            //Console.WriteLine(String.Join(",",result));
            Console.ReadLine();
        }
    }
}
