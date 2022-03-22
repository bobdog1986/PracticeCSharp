using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public static class LogHelper
    {
        public static void log(TreeNode result)
        {
            Answer.printTree(result);
        }

        public static void log(ListNode result)
        {
            Answer.printListNode(result);
        }
        public static void log(long result)
        {
            Console.WriteLine($"Result = {result}");
        }
        public static void log(double result)
        {
            Console.WriteLine($"Result = {result}");
        }
        public static void log(int result)
        {
            Console.WriteLine($"Result = {result}");
        }
        public static void log(string result)
        {
            Console.WriteLine($"Result = {result}");
        }

        public static void log(int[] result)
        {
            Console.WriteLine(string.Join(",", result));
        }
        public static void log(IList<int> result)
        {
            Console.WriteLine(string.Join(",", result));
        }

        public static void log(int[][] result)
        {
            if (result.Count() > 0)
            {
                //Console.WriteLine(String.Join("\r\n\r\n", result.Select(o => String.Join("\r\n", o))));
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
            else Console.WriteLine("!!!Result count = 0");
        }

        public static void log(IList<int[]> result)
        {
            if (result.Count() > 0)
            {
                //Console.WriteLine(String.Join("\r\n\r\n", result.Select(o => String.Join("\r\n", o))));
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
            else Console.WriteLine("!!!Result count = 0");
        }
        public static void log(IList<IList<int>> result)
        {
            if (result.Count() > 0)
            {
                //Console.WriteLine(String.Join("\r\n\r\n", result.Select(o => String.Join("\r\n", o))));
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
            else Console.WriteLine("!!!Result count = 0");
        }
        public static void log(string[][] result)
        {
            if (result.Count() > 0)
            {
                //Console.WriteLine(String.Join("\r\n\r\n", result.Select(o => String.Join("\r\n", o))));
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
            else Console.WriteLine("!!!Result count = 0");
        }
    }
}
