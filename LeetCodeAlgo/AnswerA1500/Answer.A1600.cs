﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1609. Even Odd Tree, #BFS, #BTree
        ///A binary tree is named Even-Odd if it meets the following conditions:
        ///odd-indexed level, all nodes at the level have even integer values in strictly decreasing order(from left to right).
        ///even-indexed level, all nodes at the level have odd integer values in strictly increasing order (from left to right).
        public bool IsEvenOddTree(TreeNode root)
        {
            List<TreeNode> list =new List<TreeNode>() { root};
            bool odd = false;
            while(list.Count > 0)
            {
                List<TreeNode> next = new List<TreeNode>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (odd)
                    {
                        if (list[i].val % 2 == 1) return false;
                        if (i < list.Count - 1 && list[i].val <= list[i + 1].val) return false;
                    }
                    else
                    {
                        if (list[i].val % 2 == 0) return false;
                        if (i < list.Count - 1 && list[i].val >= list[i + 1].val) return false;
                    }
                    if (list[i].left != null) next.Add(list[i].left);
                    if (list[i].right != null) next.Add(list[i].right);
                }
                odd = !odd;
                list = next;
            }
            return true;
        }

        /// 1658. Minimum Operations to Reduce X to Zero
        ///Return the minimum number of operations to reduce x to exactly 0 if it is possible, otherwise, return -1.
        ///This problem is equivalent to finding the longest subarray whose sum is == totalSum - x
        public int MinOperations(int[] nums, int x)
        {
            int sum = nums.Sum() - x;
            if (sum < 0) return -1;
            if (sum == 0) return nums.Length;

            int start = 0, windowSum = 0, len = -1;
            for (int end = 0; end < nums.Length; end++)
            {
                if (windowSum < sum)
                    windowSum += nums[end];
                while (windowSum >= sum)
                {
                    if (windowSum == sum)
                        len = Math.Max(len, end - start + 1);
                    windowSum -= nums[start];
                    start++;
                }
            }

            return len == -1 ? -1 : nums.Length - len;
        }
        ///1672.Richest Customer Wealth
        public int MaximumWealth(int[][] accounts)
        {
            return accounts.Select(x => x.Sum()).Max();
        }

        ///1694. Reformat Phone Number
        ///number consists of digits, spaces ' ', and/or dashes '-'.
        ///Firstly, remove all spaces and dashes.
        ///Then, group the digits from left to right into blocks of length 3 until there are 4 or fewer digits.
        ///The final digits are then grouped as follows:
        /// 2 digits: A single block of length 2.
        /// 3 digits: A single block of length 3.
        /// 4 digits: Two blocks of length 2 each.
        public string ReformatNumber(string number)
        {
            List<char> list=new List<char>();
            foreach(var n in number)
            {
                if (n >= '0' && n <= '9')
                {
                    list.Add(n);
                }
            }
            return ReformatNumber(list, list.Count);
        }

        public string ReformatNumber(List<char> list, int count)
        {
            if(count == 2)
            {
                return new String(new char[] { list[list.Count - 2], list[list.Count - 1] });
            }
            else if(count == 3)
            {
                return new String(new char[] { list[list.Count - 3], list[list.Count - 2], list[list.Count - 1] });
            }
            else if(count ==4)
            {
                return new String(new char[] { list[list.Count - 4], list[list.Count - 3], '-',list[list.Count - 2], list[list.Count - 1] });
            }
            else
            {
                return new String(new char[] { list[list.Count - count], list[list.Count - count + 1], list[list.Count - count + 2] })
                    + "-" + ReformatNumber(list, count - 3);
            }
        }
    }
}