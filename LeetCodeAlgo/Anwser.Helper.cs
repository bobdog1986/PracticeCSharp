using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

﻿namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        /// <summary>
        /// find target in array from [left,right], return index or -1
        /// </summary>
        public int binarySearch(int[] nums, int left, int right, int target)
        {
            if (left == right && nums[left] == target)
                return left;

            int low = left;
            int high = right;
            int i = low + (high - low) / 2;

            while (i >= low && i <= high && (high - low) >= 1)
            {
                if (target < nums[low] || target > nums[high])
                    return -1;

                if (target == nums[low])
                    return low;
                if (target == nums[high])
                    return high;

                if (nums[i] == target)
                {
                    return i;
                }
                else if (nums[i] > target)
                {
                    high = i - 1;
                    i = low + (high - low) / 2;
                }
                else
                {
                    low = i + 1;

                    i = low + (high - low) / 2;
                }
            }

            return -1;
        }

        public int getFactorial(int n)
        {
            if (n == 0 || n == 1) return 1;
            int r = 1;
            while (n >= 1)
            {
                r *= n;
                n--;
            }
            return r;
        }

        public int getFactorial(int n, int count)
        {
            int r = 1;

            while (count > 0 && n > 0)
            {
                r *= n;
                n--;
                count--;
            }

            return r;
        }

        public int getCombines(int n, int count)
        {
            return getFactorial(n, count) / getFactorial(count);
        }
    }
}