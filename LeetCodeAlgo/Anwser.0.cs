﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    /// <summary>
    /// Range 1-100
    /// </summary>
    public partial class Anwser
    {
        /// 1. Two Sum
        /// Given an array of integers nums and an integer target,
        /// return indices of the two numbers such that they add up to target.
        /// each input would have exactly one solution, and you may not use the same element twice.
        public int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[2] { i, j };
                    }
                }
            }
            return null;
        }

        ///2. Add Two Numbers
        ///You are given two non-empty linked lists representing two non-negative integers.
        ///The digits are stored in reverse order, and each of their nodes contains a single digit.
        ///Add the two numbers and return the sum as a linked list.
        ///Input: l1 = [2,4,3], l2 = [5,6,4]
        ///Output: [7,0,8]
        ///Explanation: 342 + 465 = 807.

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var node1 = l1;
            var node2 = l2;

            bool carry = false;
            ListNode root = null;
            var current = root;
            while(node1!=null || node2 != null)
            {
                var node=new ListNode();
                int val = 0;
                if (node1 == null)
                {
                    val= node2.val;
                    if (carry)
                        val++;
                    carry = val / 10 == 1;
                    node.val = val % 10;

                    node2 = node2.next;
                }
                else if (node2 == null)
                {
                    val = node1.val;
                    if (carry)
                        val++;
                    carry = val / 10 == 1;
                    node.val = val % 10;

                    node1 = node1.next;

                }
                else
                {
                    val = node1.val + node2.val;
                    if (carry)
                        val++;
                    carry = val / 10 == 1;
                    node.val = val % 10;

                    node1 = node1.next;
                    node2 = node2.next;
                }

                if (current == null)
                {
                    root= node;
                    current = node;
                }
                else
                {
                    current.next = node;
                    current = node;
                }
            }

            if (carry)
            {
                current.next = new ListNode(1);
            }

            return root;
        }

        /// 3. Longest Substring Without Repeating Characters
        public int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;
            if (s.Length == 1)
                return 1;

            int max = 1;
            int len = 0;
            List<char> list = new List<char>();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (list.Contains(c))
                {
                    max = Math.Max(max, len);
                    //list.Clear();
                    var a = list.IndexOf(c);
                    if (a == list.Count - 1)
                    {
                        list.Clear();
                        list.Add(c);
                        len = 1;
                    }
                    else
                    {
                        while (a >= 0)
                        {
                            list.RemoveAt(0);
                            a--;
                            len--;
                        }
                        list.Add(c);
                        len++;
                    }
                }
                else
                {
                    len++;
                    list.Add((char)c);
                }
            }

            max = Math.Max(max, len);

            return max;
        }

        ///4. Median of Two Sorted Arrays
        ///Given two sorted arrays nums1 and nums2 of size m and n respectively, return the median of the two sorted arrays.
        ///The overall run time complexity should be O(log (m+n)).

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length == 0 && nums2.Length == 0)
                return 0;
            int i = 0, j = 0;
            int[] nums = new int[nums1.Length + nums2.Length];
            while (i < nums1.Length || j < nums2.Length)
            {
                if (i == nums1.Length)
                {
                    nums[i + j] = nums2[j];
                    j++;
                }
                else if (j == nums2.Length)
                {
                    nums[i + j] = nums1[i];
                    i++;
                }
                else
                {
                    if (nums1[i] <= nums2[j])
                    {
                        nums[i + j] = nums1[i];
                        i++;
                    }
                    else
                    {
                        nums[i + j] = nums2[j];
                        j++;
                    }
                }
            }

            if (nums.Length % 2 == 0)
            {
                return (nums[nums.Length / 2 - 1] + nums[nums.Length / 2]) / 2.0;
            }
            else
            {
                return nums[nums.Length / 2];
            }
        }

        ///8. String to Integer (atoi)
        ///Implement the myAtoi(string s) function, which converts a string to a int (similar to C/C++'s atoi function).
        ///Constraints:
        ///0 <= s.length <= 200
        ///s consists of English letters(lower-case and upper-case), digits(0-9), ' ', '+', '-', and '.'.

        public int MyAtoi(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            s = s.Trim();

            if (string.IsNullOrEmpty(s)) return 0;
            List<char> list = new List<char>();
            int sign = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    list.Add(s[i]);
                }
                else
                {
                    if (list.Count > 0)
                        break; ;

                    if (s[i] == '+' && sign == 0)
                    {
                        sign = 1;
                    }
                    else if (s[i] == '-' && sign == 0)
                    {
                        sign = -1;
                    }
                    else
                    {
                        return 0;
                        //
                    }
                }
            }

            while (true)
            {
                if (list.Count == 0)
                    break;

                if (list[0] == '0')
                {
                    list.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }

            if (list.Count > 0)
            {
                long ll;
                try
                {
                    ll = long.Parse(string.Join("", list));
                }
                catch (Exception ex)
                {
                    return sign == -1 ? int.MinValue : int.MaxValue;
                }

                if (sign == -1)
                {
                    ll = 0 - ll;

                    if (ll <= int.MinValue)
                        ll = int.MinValue;

                    return (int)ll;
                }
                else
                {
                    if (ll >= int.MaxValue)
                        ll = int.MaxValue;

                    return (int)ll;
                }
            }

            return 0;
        }

        //12
        public string IntToRoman(int num)
        {
            if ((num < 0) || (num > 3999)) throw new ArgumentOutOfRangeException();
            if (num >= 1000) return "M" + IntToRoman(num - 1000);
            if (num >= 900) return "CM" + IntToRoman(num - 900);
            if (num >= 500) return "D" + IntToRoman(num - 500);
            if (num >= 400) return "CD" + IntToRoman(num - 400);
            if (num >= 100) return "C" + IntToRoman(num - 100);
            if (num >= 90) return "XC" + IntToRoman(num - 90);
            if (num >= 50) return "L" + IntToRoman(num - 50);
            if (num >= 40) return "XL" + IntToRoman(num - 40);
            if (num >= 10) return "X" + IntToRoman(num - 10);
            if (num >= 9) return "IX" + IntToRoman(num - 9);
            if (num >= 5) return "V" + IntToRoman(num - 5);
            if (num >= 4) return "IV" + IntToRoman(num - 4);
            if (num > 1) return "I" + IntToRoman(num - 1);
            if (num == 1) return "I";
            return string.Empty;
        }

        //13
        public int RomanToInt(string s)
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>()
            {
                { 'I', 1},
                { 'V', 5},
                { 'X', 10},
                { 'L', 50},
                { 'C', 100},
                { 'D', 500},
                { 'M', 1000}
            };

            int number = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (i + 1 < s.Length && dictionary[s[i]] < dictionary[s[i + 1]])
                {
                    number -= dictionary[s[i]];
                }
                else
                {
                    number += dictionary[s[i]];
                }
            }
            return number;
        }

        ///18. 4Sum
        ///Given an array nums of n integers, return an array of all the unique
        ///quadruplets [nums[a], nums[b], nums[c], nums[d]] such that:
        ///Input: nums = [2,2,2,2,2], target = 8
        ///Output: [[2,2,2,2]]
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            List<IList<int>> result = new List<IList<int>>();

            int number = 4;

            if (nums == null || nums.Length < number)
                return result;

            Array.Sort(nums);

            var list = new List<int>();

            FourSum_Recursion(nums, result, list, 0, target, number, 0, 0);
            return result;
        }

        public void FourSum_Recursion(int[] nums, IList<IList<int>> result, IList<int> list, int index, int target, int needNumber, int sum, int currentCount)
        {
            if (index + needNumber > nums.Length)
                return;

            if (needNumber <= 1)
            {
                bool found = false;
                for (int i = index; i < nums.Length; i++)
                {
                    if (target - sum > nums[nums.Length - 1]
                        || target - sum < nums[i])
                        break;

                    if (sum + nums[i] == target)
                    {
                        list.Add(nums[i]);
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    if (result.Count == 0 ||
                            result.FirstOrDefault(item =>
                                                item[0] == list[0]
                                             && item[1] == list[1]
                                             && item[2] == list[2]
                                             && item[3] == list[3]) == null)
                        result.Add(list);
                }
            }
            else
            {
                for (int i = index; i < nums.Length - needNumber + 1; i++)
                {
                    var sub = new List<int>(list);
                    sub.Add(nums[i]);

                    FourSum_Recursion(nums, result, sub, i + 1, target, needNumber - 1, sum + nums[i], currentCount + 1);
                }
            }
        }

        public int GetCode(int[] nums)
        {
            return nums[0] + nums[1] * 10 + nums[2] * 100 + nums[3] * 1000;
        }

        public bool IsSameFourIntArray(int[] first, int[] second)
        {
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i]) return false;
            }
            return true;
        }

        //19. Remove Nth Node From End of List

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head == null || head.next == null)
                return null;
            int count = GetListNodeCount(head);
            if (n == count)
                return head.next;

            var node1 = GetListNode(head, count - n - 1);
            node1.next = node1.next.next;

            return head;
        }

        public ListNode GetListNode(ListNode listNode, int index)
        {
            while (index > 0)
            {
                listNode = listNode.next;
                index--;
            }
            return listNode;
        }

        public int GetListNodeCount(ListNode listnode)
        {
            int count = 0;
            while (listnode != null)
            {
                count++;
                listnode = listnode.next;
            }
            return count;
        }

        public void PrintListNode(ListNode listNode)
        {
            List<int> list = new List<int>();
            while (listNode != null)
            {
                list.Add(listNode.val);
                listNode = listNode.next;
            }

            Console.WriteLine($"ListNode is [{string.Join(",", list)}]");
        }

        //20. Valid Parentheses

        public bool IsValid(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            Stack<char> qe = new Stack<char>();
            foreach (var c in s)
            {
                if (c == '[' || c == '{' || c == '(')
                {
                    qe.Push(c);
                }

                if (c == ']' || c == '}' || c == ')')
                {
                    if (qe.Count == 0)
                        return false;

                    var a = qe.Pop();

                    if (a == '[' && c == ']'
                        || a == '(' && c == ')'
                        || a == '{' && c == '}')
                    {
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return qe.Count == 0;
        }

        //21. Merge Two Sorted Lists
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null || list2 == null)
                return list1 ?? list2;

            ListNode list = null;

            if (list1.val <= list2.val)
            {
                list = list1;
                list1 = list1.next;
            }
            else
            {
                list = list2;
                list2 = list2.next;
            }
            ListNode last = list;

            while (list1 != null || list2 != null)
            {
                if (list1 == null)
                {
                    last.next = list2;
                    break;
                }
                else if (list2 == null)
                {
                    last.next = list1;
                    break;
                }
                else
                {
                    if (list1.val <= list2.val)
                    {
                        last.next = list1;
                        list1 = list1.next;
                    }
                    else
                    {
                        last.next = list2;
                        list2 = list2.next;
                    }

                    last = last.next;
                }
            }
            return list;
        }

        //26
        public int RemoveDuplicates(int[] nums)
        {
            int find = 0;
            for (int i = 0; i < nums.Length - 1;)
            {
                if (nums[i] == nums[i + 1])
                {
                    Array.Copy(nums, i + 1, nums, i, nums.Length - i - 1 - find);

                    find++;
                }
                else
                {
                    i++;
                }
                if (i + find >= nums.Length - 1) break;
            }

            nums = nums.ToList().GetRange(0, nums.Length - find).ToArray();

            return nums.Length;
        }

        //27
        public int RemoveElement(int[] nums, int val)
        {
            int find = 0;
            for (int i = 0; i < nums.Length;)
            {
                if (nums[i] == val)
                {
                    Array.Copy(nums, i + 1, nums, i, nums.Length - i - 1 - find);

                    find++;
                }
                else
                {
                    i++;
                }
                if (i + find >= nums.Length) break;
            }
            nums = nums.ToList().GetRange(0, nums.Length - find).ToArray();

            return nums.Length;
        }

        //35. Search Insert Position

        public int SearchInsert(int[] nums, int target)
        {
            return SearchInsert(nums, target, 0, nums.Length - 1);
        }

        public int SearchInsert(int[] nums, int target, int start, int end)
        {
            if (start == end)
            {
                if (nums[start] >= target)
                {
                    return start;
                }
                else
                {
                    return start + 1;
                }
            }

            if (start + 1 == end)
            {
                if (nums[start] >= target)
                {
                    return start;
                }
                else if (nums[end] < target)
                {
                    return end + 1;
                }
                else
                {
                    return start + 1;
                }
            }

            int num = end - start + 1;
            int mid = num / 2 + start;
            if (nums[mid] == target)
            {
                return SearchInsert(nums, target, start, mid);
            }
            else if (nums[mid] < target)
            {
                return SearchInsert(nums, target, mid + 1, end);
            }
            else
            {
                return SearchInsert(nums, target, start, mid - 1);
            }
        }

        //36. Valid Sudoku

        public bool IsValidSudoku(char[][] board)
        {
            int[][] arrRow = new int[9][];
            int[][] arrCol = new int[9][];
            int[][] arrBlock = new int[9][];

            for (int i = 0; i < 9; i++)
            {
                arrRow[i] = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                arrCol[i] = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                arrBlock[i] = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    char v = board[i][j];
                    if (v == '.')
                    {
                        continue;
                    }

                    int k = v - '1';

                    if (arrRow[i][k] == 0 && arrCol[j][k] == 0 && arrBlock[i / 3 * 3 + j / 3][k] == 0)
                    {
                        arrRow[i][k] = 1;
                        arrCol[j][k] = 1;
                        arrBlock[i / 3 * 3 + j / 3][k] = 1;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //38
        public string CountAndSay(int n)
        {
            string s = "1";
            if (n == 1) return s;

            for (int i = 0; i < n - 1; i++)
            {
                s = GetCountAndSay(s);
            }
            return s;
        }

        public string GetCountAndSay(string s)
        {
            var arr = s.ToCharArray();
            List<char> result = new List<char>();
            char pre = arr[0];
            int occured = 1;
            char current;
            for (int i = 1; i < arr.Length; i++)
            {
                current = arr[i];
                if (current == pre)
                {
                    occured++;
                }
                else
                {
                    result.AddRange(occured.ToString().ToCharArray());
                    result.Add(pre);
                    pre = current;
                    occured = 1;
                }
            }
            result.AddRange(occured.ToString().ToCharArray());
            result.Add(pre);
            return new string(result.ToArray());
        }

        //45. Jump Game II
        public int Jump(int[] nums)
        {
            if (nums.Length == 1)
                return 0;
            if (nums.Length == 2)
                return 1;

            bool[] canDp = GetCanJumpArray(nums);
            int[] dp = new int[nums.Length];

            int i = nums.Length - 1;
            dp[i] = 0;
            i--;

            while (i >= 0)
            {
                if (canDp[i] == false)
                {
                    dp[i] = 0;
                }
                else
                {
                    for (int j = nums[i]; j >= 1; j--)
                    {
                        if (i + j >= nums.Length - 1)
                        {
                            dp[i] = 1;
                            break;
                        }
                        else if (canDp[i + j])
                        {
                            if (dp[i] == 0)
                            {
                                dp[i] = Math.Min(nums.Length - 1, 1 + dp[i + j]);
                            }
                            else
                            {
                                dp[i] = Math.Min(dp[i], 1 + dp[i + j]);
                            }
                        }
                    }
                }

                i--;
            }

            return dp[0];
        }

        //46. Permutations
        //1 <= nums.length <= 6
        //-10 <= nums[i] <= 10
        //All the integers of nums are unique.
        public IList<IList<int>> Permute(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return null;

            var result = new List<IList<int>>();

            if (nums.Length == 1)
            {
                result.Add(nums.ToList());
                return result;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                var list1 = new List<int>();
                var list2 = new List<int>();

                foreach (var n in nums)
                    list2.Add(n);

                list1.Add(nums[i]);
                list2.RemoveAt(i);

                Permute_Recurison(list1, list2, result);
            }

            return result;
        }

        public void Permute_Recurison(IList<int> head, IList<int> tail, IList<IList<int>> result)
        {
            if (tail == null || tail.Count == 0)
            {
                result.Add(head);
                return;
            }
            else
            {
                for (int i = 0; i < tail.Count; i++)
                {
                    var list1 = new List<int>();
                    var list2 = new List<int>();

                    foreach (var m in head)
                        list1.Add(m);

                    foreach (var n in tail)
                        list2.Add(n);

                    list1.Add(tail[i]);
                    list2.RemoveAt(i);

                    Permute_Recurison(list1, list2, result);
                }
            }
        }

        //53. Maximum Subarray

        public int MaxSubArray(int[] nums)
        {
            int sum = 0;
            int max = nums.Max();

            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (sum <= 0)
                {
                    sum = 0;
                }
                else
                {
                    max = Math.Max(max, sum);
                }
            }

            return max;
        }

        //55. Jump Game
        public bool CanJump(int[] nums)
        {
            if (nums.Length == 1)
                return true;
            if (nums.Length == 2)
                return nums[0] > 0;

            bool[] dp = new bool[nums.Length];

            int i = nums.Length - 1;
            dp[i] = true;
            i--;

            while (i >= 0)
            {
                if (nums[i] == 0)
                {
                    dp[i] = false;
                }
                else
                {
                    bool has = false;
                    for (int j = 1; j <= nums[i]; j++)
                    {
                        if (i + j <= nums.Length - 1 && dp[i + j])
                        {
                            has = true;
                            break;
                        }
                    }

                    dp[i] = has;
                }

                i--;
            }
            return dp[0];
        }

        public bool[] GetCanJumpArray(int[] nums)
        {
            if (nums.Length == 1)
                return new bool[] { true };
            if (nums.Length == 2)
                return new bool[] { true, true };

            bool[] dp = new bool[nums.Length];

            int i = nums.Length - 1;
            dp[i] = true;
            i--;

            while (i >= 0)
            {
                if (nums[i] == 0)
                {
                    dp[i] = false;
                }
                else
                {
                    bool has = false;
                    for (int j = 1; j <= nums[i]; j++)
                    {
                        if (i + j <= nums.Length - 1 && dp[i + j])
                        {
                            has = true;
                            break;
                        }
                    }

                    dp[i] = has;
                }

                i--;
            }
            return dp;
        }

        //56
        public IList<Interval> Merge(IList<Interval> intervals)
        {
            if (intervals == null || intervals.Count <= 1) return intervals;

            IList<Interval> result = new List<Interval>();

            for (int i = 0; i < intervals.Count; i++)
            {
                Interval current = intervals[i];
                result.Add(current);
                result = TrimIntervalFromEnd(result);
            }
            return result;
        }

        public IList<Interval> TrimIntervalFromEnd(IList<Interval> list)
        {
            if (list == null || list.Count <= 1) return list;
            while (list.Count > 1)
            {
                Interval current = list[list.Count - 1];
                Interval pre = list[list.Count - 2];
                if (pre.end < current.start)
                {
                    break;
                }
                else
                {
                    if (pre.start <= current.start)
                    {
                        pre.start = Math.Min(pre.start, current.start);
                        pre.end = Math.Max(pre.end, current.end);
                        list.Remove(current);
                    }
                    else
                    {
                        SwapIntervalNode(ref pre, ref current);
                        list.Remove(current);
                        list = TrimIntervalFromEnd(list);
                        list.Add(current);
                    }
                }
            }

            return list;
        }

        public void SwapIntervalNode(ref Interval a, ref Interval b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public Interval MergeIntervalNodes(Interval current, Interval next)
        {
            return new Interval(Math.Min(current.start, next.start), Math.Max(current.end, next.end));
        }

        //57 not pass
        public IList<Interval> Insert(IList<Interval> intervals, Interval newInterval)
        {
            return intervals;
        }

        //65
        public bool IsNumber(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            s = s.TrimStart();
            s = s.TrimEnd();
            if (string.IsNullOrEmpty(s)) return false;

            try
            {
                double d = double.Parse(s);
                return true;
            }
            catch
            {
                if (s.Contains(" ")) return false;

                if (s.Contains("e"))
                {
                    int idx = s.IndexOf('e');
                    if (idx == s.LastIndexOf('e') && (idx > 0 && idx < s.Length - 1))
                    {
                        var arr = s.Split('e');
                        if (arr.Length != 2) return false;
                        double part;
                        long exp;
                        try
                        {
                            part = double.Parse(arr[0]);
                            exp = long.Parse(arr[1]);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
            }

            return false;
        }

        ///66. Plus One
        /// 0-index is the highest bit
        ///Input: digits = [1,2,3]
        ///Output: [1,2,4]

        public int[] PlusOne(int[] digits)
        {
            if(digits == null || digits.Length == 0)
                return new int[] { 1};

            int i = digits.Length-1;
            while (i >=0)
            {
                if (digits[i] != 9)
                {
                    digits[i]++;
                    return digits;
                }
                else
                {
                    if (0 == i)
                    {
                        digits[i]++;
                        break;
                    }
                    else
                    {
                        digits[i] = 0;
                    }
                }

                i--;
            }

            if (digits[0] == 10)
            {
                digits = new int[digits.Length + 1];
                digits[0] = 1;
            }
            return digits;
        }

        /// 67. Add Binary
        /// Given two binary strings a and b, return their sum as a binary string.
        ///1 <= a.length, b.length <= 104, no leading zero
        public string AddBinary(string a, string b)
        {
            if(string.IsNullOrEmpty(a)&& string.IsNullOrEmpty(b))
                return string.Empty;

            if (string.IsNullOrEmpty(a))
                return b;
            if (string.IsNullOrEmpty(b))
                return a;

            string s1;
            string s2;
            if (a.Length <= b.Length)
            {
                s1 = a;
                s2 = b;
            }
            else
            {
                s1 = b;
                s2 = a;
            }

            int i = 0;

            List<char> result = new List<char>();

            bool carry = false;
            while (i<= s2.Length-1)
            {
                if (i >= s1.Length)
                {
                    if (s2[s2.Length-1-i] == '0')
                    {
                        if (carry)
                        {
                            result.Insert(0,'1');
                            carry = false;

                        }
                        else
                        {
                            result.Insert(0, '0');
                        }
                    }
                    else
                    {
                        if (carry)
                        {
                            result.Insert(0, '0');
                            carry = true;

                        }
                        else
                        {
                            result.Insert(0, '1');
                        }
                    }
                }
                else
                {
                    if (s1[s1.Length - 1 - i] == '0' && s2[s2.Length - 1 - i] == '0')
                    {
                        if (carry)
                        {
                            result.Insert(0, '1');
                            carry = false;

                        }
                        else
                        {
                            result.Insert(0, '0');
                        }
                    }
                    else if(s1[s1.Length - 1 - i] == '1' && s2[s2.Length - 1 - i] == '1')
                    {
                        if (carry)
                        {
                            result.Insert(0, '1');
                            carry = true;

                        }
                        else
                        {
                            result.Insert(0, '0');
                            carry = true;
                        }
                    }
                    else
                    {
                        if (carry)
                        {
                            result.Insert(0, '0');
                            carry = true;

                        }
                        else
                        {
                            result.Insert(0, '1');
                            carry = false;
                        }
                    }

                }

                i++;
            }

            if (carry)
            {
                result.Insert(0, '1');
            }

            return String.Join("",result);

        }



        //69
        public int MySqrt(int x)
        {
            long r = x;
            while (r * r > x)
                r = (r + x / r) / 2;
            return (int)r;
        }

        //70. Climbing Stairs
        public int ClimbStairs(int n)
        {
            //bottom to up
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 2;

            int seed1 = 1;
            int seed2 = 1;
            for (int i = n - 2; i > 0; i--)
            {
                int temp = seed1;
                seed1 = seed2;
                seed2 = seed2 + temp;
            }

            return seed1 + seed2;

            //brute force, out of memory
            if (n == 0) return 0;
            int totalCount = 0;

            int root = 0;

            Queue<int> nodes = new Queue<int>();
            nodes.Enqueue(root);

            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                var left = node + 1;
                var right = node + 2;

                if (left < n)
                {
                    nodes.Enqueue(left);
                }
                else if (left == n)
                {
                    totalCount++;
                }

                if (right < n)
                {
                    nodes.Enqueue(right);
                }
                else if (right == n)
                {
                    totalCount++;
                }
            }

            return totalCount;
        }

        public int ClimbStairsN(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 2;

            return ClimbStairsN(n - 1) + ClimbStairsN(n - 2);

            return 0;
        }

        //74. Search a 2D Matrix
        public bool SearchMatrix(int[][] matrix, int target)
        {
            int row = matrix.Length;
            int col = matrix[0].Length;

            if (matrix[0][0] == target || matrix[row - 1][col - 1] == target)
                return true;
            if (matrix[0][0] > target || matrix[row - 1][col - 1] < target)
                return false;

            int a = 0;
            int b = row - 1;

            int m = (a + b) / 2;
            bool findRow = false;
            while (m >= a && m <= b)
            {
                if (matrix[m][col - 1] == target)
                {
                    return true;
                }
                else if (matrix[m][col - 1] > target)
                {
                    if ((m > a))
                    {
                        if (matrix[m - 1][col - 1] == target)
                        {
                            return true;
                        }
                        else if (matrix[m - 1][col - 1] < target)
                        {
                            findRow = true;

                            break;
                        }
                        else
                        {
                            b = m;
                            m = (a + b) / 2;
                        }
                    }
                    else
                    {
                        findRow = true;
                        break;
                    }
                }
                else
                {
                    if ((m < b))
                    {
                        if (matrix[m + 1][col - 1] == target)
                        {
                            return true;
                        }
                        else if (matrix[m + 1][col - 1] > target)
                        {
                            findRow = true;
                            m++;
                            break;
                        }
                        else
                        {
                            a = m;
                            m = (a + b) / 2;
                        }
                    }
                    else
                    {
                        findRow = true;
                        break;
                    }
                }
            }

            if (!findRow)
                return false;

            if (matrix[m][col - 1] == target || matrix[m][0] == target)
                return true;

            int n = col - 1;
            n = n / 2;

            int x = 0;
            int y = col - 1;

            bool result = false;
            while (n >= x && n <= y && (y - x > 1))
            {
                if (matrix[m][n] == target)
                {
                    return true;
                }
                else if (matrix[m][n] > target)
                {
                    y = n;
                    n = (n - x) / 2;
                }
                else
                {
                    x = n;
                    n += (y - n) / 2;
                }
            }

            return false;
        }

        ///77. Combinations
        public IList<IList<int>> Combine(int n, int k)
        {
            if (n == 0)
                return null;

            if (k == 0)
                return null;

            if (n < k)
                return null;

            List<IList<int>> result = new List<IList<int>>();

            if (n == k)
            {
                List<int> list = new List<int>();

                for (int i = 1; i <= n; i++)
                    list.Add(i);

                result.Add(list);
                return result;
            }

            if (k == 1)
            {
                for (int i = 1; i <= n; i++)
                {
                    List<int> list = new List<int>();

                    list.Add(i);
                    result.Add(list);
                }

                return result;
            }

            for (int i = 1; i <= n - k + 1; i++)
            {
                var list1 = Combine(n - 1, k, i);
                var list2 = Combine(n - 1, k - 1, i);

                if (list1 != null && list1.Count > 0)
                {
                    foreach (var item in list1)
                        result.Add(item);
                }

                if (list2 != null && list2.Count > 0)
                {
                    foreach (var item in list2)
                    {
                        item.Add(n);
                    }

                    foreach (var item in list2)
                        result.Add(item);
                }
            }

            return result;
        }

        public IList<IList<int>> Combine(int n, int k, int start)
        {
            if (n == 0)
                return null;

            if (k == 0)
                return null;

            if (n - start + 1 < k)
                return null;

            if (n - start + 1 == k)
            {
                List<IList<int>> result = new List<IList<int>>();

                List<int> list = new List<int>();

                for (int i = start; i <= n; i++)
                    list.Add(i);

                result.Add(list);
                return result;
            }

            var list1 = Combine(n - 1, k, start);
            var list2 = Combine(n - 1, k - 1, start);

            if (list2 != null && list2.Count > 0)
            {
                foreach (var i in list2)
                {
                    i.Add(n);
                }
            }

            if (list1 != null && list1.Count > 0)
            {
                if (list2 != null && list2.Count > 0)
                {
                    foreach (var i in list2)
                    {
                        list1.Add(i);
                    }
                }

                return list1;
            }

            if (list2 != null && list2.Count > 0)
            {
                return list2;
            }

            return null;
        }

        //83. Remove Duplicates from Sorted List

        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            var queue = new Queue<ListNode>();
            int last = int.MinValue;

            while (head != null)
            {
                if (last == head.val)
                {
                    head = head.next;
                }
                else
                {
                    last = head.val;
                    var next = head.next;
                    head.next = null;
                    queue.Enqueue(head);
                    head = next;
                }
            }

            ListNode result = null;
            ListNode current = null;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (result == null)
                {
                    result = node;
                    current = result;
                }
                else
                {
                    current.next = node;
                    current = node;
                }
            }

            return result;
        }

        //88. Merge Sorted Array
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (nums1 == null || nums1.Length == 0 || m == 0)
            {
                if (nums2 == null || nums2.Length == 0 || n == 0)
                {
                    //
                }
                else
                {
                    for (int a = 0; a < nums1.Length; a++)
                    {
                        if (a < m + n)
                        {
                            nums1[a] = nums2[a];
                        }
                        else
                        {
                            nums1[a] = 0;
                        }
                    }
                }
            }
            else
            {
                if (nums2 == null || nums2.Length == 0 || n == 0)
                {
                    for (int a = 0; a < nums1.Length; a++)
                    {
                        if (a < m + n)
                        {
                            nums1[a] = nums1[a];
                        }
                        else
                        {
                            nums1[a] = 0;
                        }
                    }
                }
                else
                {
                    int[] result = new int[m + n];
                    int k = 0;
                    int j = 0;
                    int i = 0;
                    while (i < m && j < n)
                    {
                        if (nums1[i] <= nums2[j])
                        {
                            result[k] = nums1[i];
                            k++;
                            i++;
                        }
                        else
                        {
                            result[k] = nums2[j];
                            k++;
                            j++;
                        }
                    }

                    while (k < m + n)
                    {
                        if (i < m)
                        {
                            result[k] = nums1[i];
                            k++;
                            i++;
                        }

                        if (j < n)
                        {
                            result[k] = nums2[j];
                            k++;
                            j++;
                        }
                    }

                    for (int a = 0; a < nums1.Length; a++)
                    {
                        if (a < k)
                        {
                            nums1[a] = result[a];
                        }
                        else
                        {
                            nums1[a] = 0;
                        }
                    }
                }
            }
            Console.WriteLine($"nums1 = {string.Join(",", nums1)}");
        }

        //94. Binary Tree Inorder Traversal
        public IList<int> InorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            InorderTraversal_Recursion(root, result);
            return result;
        }

        public void InorderTraversal_Recursion(TreeNode node, IList<int> list)
        {
            if (node == null)
                return;
            InorderTraversal_Recursion(node.left, list);
            list.Add(node.val);

            InorderTraversal_Recursion(node.right, list);
        }

        public IList<int> InorderTraversal_Iteration(TreeNode root)
        {
            List<int> values = new List<int>();

            if (root == null) return values;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode node = root;

            while (node != null || stack.Any())
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }
                else
                {
                    var item = stack.Pop();
                    values.Add(item.val);
                    node = item.right;
                }
            }
            return values;
        }

        public IList<int> LayerTraversal(TreeNode root)
        {
            List<int> values = new List<int>();

            if (root == null) return values;

            IList<TreeNode> nodes = new List<TreeNode> { root };

            while (nodes != null && nodes.Count > 0)
            {
                nodes = GetInorderAndReturnSubNodes(nodes, values);
            }

            return values;
        }

        public IList<TreeNode> GetInorderAndReturnSubNodes(IList<TreeNode> nodes, List<int> values)
        {
            if (nodes == null || nodes.Count == 0) return null;
            IList<TreeNode> subNodes = new List<TreeNode>();
            foreach (var n in nodes)
            {
                values.Add(n.val);
                if (n.left != null) { subNodes.Add(n.left); }
                if (n.right != null) { subNodes.Add(n.right); }
            }
            return subNodes;
        }

        /// 96. Unique Binary Search Trees
        public int NumTrees(int n)
        {
            return 0;
        }

        public int getFactorial(int n)
        {
            if (n == 0 || n == 1) return 1;

            return n * getFactorial(n - 1);
        }

        public int getFactorial(int n, int count)
        {
            int result = 1;

            while (count > 0 && n > 0)
            {
                result *= n;
                n--;
                count--;
            }
            return result;
        }

        public int getCombine(int n, int count)
        {
            return getFactorial(n, count) / getFactorial(count);
        }

        //98
        public bool IsValidBST(TreeNode root)
        {
            if (root == null) return true;
            if (root.left == null && root.right == null) return true;
            return IsValidNode(root, (long)int.MaxValue + 1, (long)int.MinValue - 1);
            //return (root.left != null && ((root.left.val < root.val) && IsValidBST(root.left))) &&
            //    (root.right != null && ((root.right.val > root.val) && IsValidBST(root.right)));

            //return (root.left == null || ((root.left.val < root.val) && IsValidBST(root.left))) &&
            //    (root.right == null || ((root.right.val > root.val) && IsValidBST(root.right)));
        }

        public bool IsValidNode(TreeNode node, long maxlimit, long minlimit)
        {
            if (node == null) return true;
            if (node.val >= maxlimit || node.val <= minlimit) return false;

            return IsValidNode(node.left, Math.Min(node.val, maxlimit), minlimit) &&
                IsValidNode(node.right, maxlimit, Math.Max(node.val, minlimit));
        }
    }
}