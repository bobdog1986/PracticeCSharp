using System;
using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        /// find target in array from [left,right], return index or -1
        public int binarySearch(int[] nums, int target, int left=-1, int right=-1)
        {
            if (left == -1)
                left = 0;
            if (right == -1)
                right = nums.Length - 1;

            if (left < 0 || left >= nums.Length || right < 0 || right >= nums.Length)
                throw new ArgumentOutOfRangeException("Index Out of Array");

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

        public void printListNode(ListNode listNode)
        {
            List<int> list = new List<int>();
            while (listNode != null)
            {
                list.Add(listNode.val);
                listNode = listNode.next;
            }

            Console.WriteLine($"ListNode is [{string.Join(",", list)}]");
        }

        public ListNode buildListNode(int[] arr)
        {
            ListNode head = new ListNode(arr[0]);
            var current = head;
            for(int i = 1; i < arr.Length; i++)
            {
                current.next = new ListNode(arr[i]);
                current = current.next;
            }

            return head;
        }

        public int[] createArray(int len, int seed = int.MinValue)
        {
            int[] arr = new int[len];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = seed;
            return arr;
        }

        /// 找出最大公约数
        public int Gcb(int m, int n)
        {
            if (m < 1 || n < 1)
                return m > 0 ? m : n;
            if (m == 1 || n == 1)
                return 1;
            if (m % n == 0)
                return n;

            int remainder = m % n;
            m = n;
            n = remainder;
            return Gcb(m, n);
        }

        public long GcbLong(long m, long n)
        {
            if (m < 1 || n < 1)
                return m > 0 ? m : n;
            if (m == 1 || n == 1)
                return 1;
            if (m % n == 0)
                return n;

            long remainder = m % n;
            m = n;
            n = remainder;
            return GcbLong(m, n);
        }


    }
}