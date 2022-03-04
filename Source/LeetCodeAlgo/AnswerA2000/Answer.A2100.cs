using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        /// 2119. A Number After a Double Reversal
        ///eg. 1234 reverse to 4321, then again to 1234== origin 1234, return true
        public bool IsSameAfterReversals(int num)
        {
            return num == 0 || num % 10 != 0;
        }
        ///2129. Capitalize the Title
        /// Capitalize the string by changing the capitalization of each word such that:
        ///If the length of the word is 1 or 2 letters, change all letters to lowercase.
        ///Otherwise, change the first letter to uppercase and the remaining letters to lowercase.

        public string CapitalizeTitle(string title)
        {
            var arr= title.Split(' ').Where(x => x.Length > 0).Select(x =>
            {
                var str = x.ToLower();
                if (x.Length <= 2) return str;
                return str.Substring(0, 1).ToUpper() + str.Substring(1);
            });
            return string.Join(" ", arr);
        }
        /// 2148. Count Elements With Strictly Smaller and Greater Elements
        ///return the number of elements that have both a strictly smaller and a strictly greater element appear in nums.
        ///-100000 <= nums[i] <= 100000
        public int CountElements(int[] nums)
        {
            int ship = 100000;
            int[] arr=new int[ship*2+1];
            int start = arr.Length-1;
            int end = 0;
            foreach(var num in nums)
            {
                var index = num + ship;
                arr[index]++;
                start=Math.Min(start, index);
                end = Math.Max(end, index);
            }
            int sum = 0;
            for (int i = start+1; i <= end-1; i++)
                sum += arr[i];
            return sum;
        }

    }
}