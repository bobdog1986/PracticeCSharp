using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        /// 2119. A Number After a Double Reversal
        ///1234 reverse to 4321, then again to 1234== origin 1234, return true
        public bool IsSameAfterReversals(int num)
        {
            return num == 0 || num % 10 != 0;
        }
        ///2148. Count Elements With Strictly Smaller and Greater Elements
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