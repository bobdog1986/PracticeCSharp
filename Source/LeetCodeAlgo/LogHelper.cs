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

        public static void log(bool result)
        {
            Console.WriteLine($"Result = {result}");
        }

        public static void log(string result)
        {
            var res = string.IsNullOrEmpty(result) ? "String.Empty" : result;
            Console.WriteLine($"Result = {res}");
        }

        public static void log(IEnumerable<long> result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");
            if (result.Count() > 0)
                Console.WriteLine(string.Join(",", result));
        }

        public static void log(IEnumerable<int> result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");

            if (result.Count() > 0)
                Console.WriteLine(string.Join(",", result));
        }

        public static void log(IEnumerable<double> result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");
            if (result.Count() > 0)
                Console.WriteLine(string.Join(",", result));
        }

        public static void log(IEnumerable<string> result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");
            if (result.Count() > 0)
                Console.WriteLine(string.Join(",", result));
        }

        public static void log(int[][] result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");
            if (result.Count() > 0)
            {
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
        }

        public static void log(IList<int[]> result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");
            if (result.Count() > 0)
            {
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
        }

        public static void log(IList<IList<int>> result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");
            if (result.Count() > 0)
            {
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
        }

        public static void log(IList<IList<string>> result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");
            if (result.Count() > 0)
            {
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
        }

        public static void log(string[][] result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");
            if (result.Count() > 0)
            {
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
        }

        public static void log(char[][] result)
        {
            Console.WriteLine($"!!!Result count = {result.Count()}");
            if (result.Count() > 0)
            {
                Console.WriteLine(String.Join("\r\n", result.Select(o => String.Join(",", o))));
            }
        }
    }
}