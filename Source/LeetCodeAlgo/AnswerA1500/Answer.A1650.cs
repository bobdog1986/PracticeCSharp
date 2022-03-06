using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1652. Defuse the Bomb
        ///Given the circular array code and an integer key k, return the decrypted code to defuse the bomb!
        public int[] Decrypt(int[] code, int k)
        {
            var ans=new int[code.Length];
            for(int i=0; i<code.Length; i++)
            {
                if (k > 0)
                {
                    for (int j = 1; j <= k; j++)
                    {
                        var index = (i + j) % code.Length;
                        ans[i] += code[index];
                    }
                }
                else if (k < 0)
                {
                    for (int j = -1; j >= k; j--)
                    {
                        var index = (i + j) % code.Length;
                        if (index < 0)
                            index += code.Length;
                        ans[i] += code[index];
                    }
                }
            }
            return ans;
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

        ///1669. Merge In Between Linked Lists
        ///Remove list1's nodes from the ath node to the bth node, and put list2 in their place.
        public ListNode MergeInBetween(ListNode list1, int a, int b, ListNode list2)
        {
            int i = 0;
            var node = list1;
            ListNode tail1 = null;
            ListNode nextHead1 = null;
            while (node != null)
            {
                i++;
                if (i == a) { tail1 = node; }
                node = node.next;
                if (i == b)
                {
                    nextHead1 = node.next;
                    break;
                }
            }
            node = list2;
            while (node.next != null)
            {
                node = node.next;
            }
            node.next=nextHead1;
            tail1.next = list2;
            return list1;
        }

        ///1672.Richest Customer Wealth
        public int MaximumWealth(int[][] accounts)
        {
            return accounts.Select(x => x.Sum()).Max();
        }

        ///1678. Goal Parser Interpretation
        ///Given the string command, return the Goal Parser's interpretation of command.
        public string Interpret(string command)
        {
            if (string.IsNullOrEmpty(command)) return string.Empty;
            else if (command.StartsWith("G")) return "G" + Interpret(command.Substring(1));
            else if (command.StartsWith("()")) return "o" + Interpret(command.Substring(2));
            else if (command.StartsWith("(al)")) return "al" + Interpret(command.Substring(4));
            else return command;
        }
        ///1684. Count the Number of Consistent Strings
        ///Return the number of consistent strings in the array words.
        public int CountConsistentStrings(string allowed, string[] words)
        {
            HashSet<char> map = new HashSet<char>();
            foreach(var c in allowed)
                if (!map.Contains(c)) map.Add(c);

            int res = 0;
            foreach(var word in words)
            {
                bool valid = true;
                foreach(var w in word)
                {
                    if (!map.Contains(w))
                    {
                        valid = false;
                        break;
                    }
                }
                if(valid) res++;
            }
            return res;
        }

        /// 1694. Reformat Phone Number
        ///number consists of digits, spaces ' ', and/or dashes '-'.
        ///Firstly, remove all spaces and dashes.
        ///Then, group the digits from left to right into blocks of length 3 until there are 4 or fewer digits.
        ///The final digits are then grouped as follows:
        /// 2 digits: A single block of length 2.
        /// 3 digits: A single block of length 3.
        /// 4 digits: Two blocks of length 2 each.
        public string ReformatNumber(string number)
        {
            List<char> list = new List<char>();
            foreach (var n in number)
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
            if (count == 2)
            {
                return new String(new char[] { list[list.Count - 2], list[list.Count - 1] });
            }
            else if (count == 3)
            {
                return new String(new char[] { list[list.Count - 3], list[list.Count - 2], list[list.Count - 1] });
            }
            else if (count == 4)
            {
                return new String(new char[] { list[list.Count - 4], list[list.Count - 3], '-', list[list.Count - 2], list[list.Count - 1] });
            }
            else
            {
                return new String(new char[] { list[list.Count - count], list[list.Count - count + 1], list[list.Count - count + 2] })
                    + "-" + ReformatNumber(list, count - 3);
            }
        }
    }
}
