﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        /// 1. Two Sum
        /// return indices of the two numbers such that they add up to target.
        /// each input would have exactly one solution, and you may not use the same element twice.
        /// 2 <= nums.length <= 10^4, -10^9 <= nums[i] <= 10^9, -10^9 <= target <= 10^9
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
            while (node1 != null || node2 != null)
            {
                var node = new ListNode();
                int val = 0;
                if (node1 == null)
                {
                    val = node2.val;
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
                    root = node;
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
        /// Given a string s, find the length of the longest substring without repeating characters.
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
                    list.Add(c);
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

        ///5. Longest Palindromic Substring
        ///Given a string s, return the longest palindromic substring in s.
        ///Input: s = "babad" Output: "bab"
        public string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length == 1)
                return s;

            string ans = s.Substring(0, 1);
            int len = 1;

            //find all aba pattern, loop only all possible index
            for (int i = (len - 1) / 2 + 1; i < s.Length && 2 * (s.Length - 1 - i) + 1 > len; i++)
            {
                int count = 0;
                while (i - 1 - count >= 0 && i + 1 + count <= s.Length - 1)
                {
                    if (s[i - 1 - count] == s[i + 1 + count])
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (count + count + 1 > len)
                {
                    len = count + count + 1;
                    ans = s.Substring((i - count), len);
                }
            }

            //find all abba pattern, loop only all possible index
            for (int i = len / 2; i < s.Length - 1 && 2 * (s.Length - 1 - i) > len; i++)
            {
                int count = 0;
                while (i - count >= 0 && i + 1 + count <= s.Length - 1)
                {
                    if (s[i - count] == s[i + 1 + count])
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (count + count > len)
                {
                    len = count + count;
                    ans = s.Substring((i - count + 1), len);
                }
            }

            return ans;
        }

        ///6. Zigzag Conversion
        ///Zigzag is Z line, row=3
        ///A     A     A
        ///A  A  A  A  A
        ///A     A
        public string Convert_6_ZigZag(string s, int numRows)
        {
            if (s.Length <= numRows || numRows == 1)
                return s;

            int zagLen = numRows + (numRows - 2);
            int zagCount = 1;
            int zagColCount = 1 + (numRows - 2);
            while (true)
            {
                int sum1 = zagCount * zagLen;

                if (s.Length <= sum1)
                {
                    break;
                }
                else
                {
                    zagCount++;
                }
            }

            char[][] mat = new char[numRows][];
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i] = new char[zagCount * zagLen];
            }

            for (int i = 0; i < s.Length; i++)
            {
                int zagIndex = i / zagLen;
                int zagModulo = i % zagLen;

                int c = zagIndex * zagColCount;
                if (zagModulo == 0)
                {
                }
                else if (zagModulo < numRows)
                {
                    //c++;
                }
                else
                {
                    //c++;
                    c += zagModulo - numRows + 1;
                }

                int r = 0;
                if (zagModulo == 0)
                {
                    //r=0
                }
                if (zagModulo < numRows)
                {
                    r = zagModulo;
                }
                else
                {
                    r = numRows - 1 - (zagModulo - numRows + 1);
                }

                mat[r][c] = s[i];
            }

            List<char> list = new List<char>();
            foreach (var r in mat)
            {
                foreach (var a in r)
                {
                    if ((byte)a != 0)
                    {
                        list.Add(a);
                    }
                }
            }

            return string.Join("", list);
        }

        ///7. Reverse Integer
        ///Given a signed 32-bit integer x, return x with its digits reversed.
        ///If reversing x causes the value to go outside the signed 32-bit integer range [-2^31, 2^31 - 1], then return 0.
        ///Assume the environment does not allow you to store 64-bit integers (signed or unsigned).
        public int Reverse(int x)
        {
            if (x == 0) return 0;

            bool isNeg = false;
            if (x < 0)
            {
                isNeg = true;
                if (x == int.MinValue)
                    return 0;
                x = -x;
            }

            int result = 0;
            while (x > 0)
            {
                if (result > (int.MaxValue / 10))
                    return 0;

                result *= 10;
                result += x % 10;
                x = x / 10;
            }

            return isNeg ? -result : result;
        }

        /// 8. String to Integer (atoi)
        ///Implement the myAtoi(string s) function, which converts a string to a int (similar to C/C++'s atoi function).
        ///Constraints:
        ///0 <= s.length <= 200
        ///s consists of English letters(lower-case and upper-case), digits(0-9), ' ', '+', '-', and '.'.
        public int MyAtoi(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;
            s = s.Trim();

            if (string.IsNullOrEmpty(s))
                return 0;

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
                        break;

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
                    }
                }
            }

            //remove head 0
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

        ///9. Palindrome Number
        ///An integer is a palindrome when it reads the same backward as forward.
        ///For example, 121 is a palindrome while 123 is not.
        /// -121 is not
        public bool IsPalindrome(int x)
        {
            if (x < 0)
                return false;

            if (x < 10)
                return true;

            int len = 0;
            int n = x;
            int m = 0;
            List<int> digits = new List<int>();
            while (n > 0)
            {
                len++;
                m = n % 10;

                n = n / 10;
                digits.Add(m);
            }

            if (len % 2 == 1)
            {
                for (int i = 1; i <= (len - 1) / 2; i++)
                {
                    if (digits[(len - 1) / 2 - i] != digits[(len - 1) / 2 + i])
                        return false;
                }
            }
            else
            {
                for (int i = 0; i < len / 2; i++)
                {
                    if (digits[(len - 1) / 2 - i] != digits[(len + 1) / 2 + i])
                        return false;
                }
            }

            return true;
        }

        ///11. Container With Most Water
        /// max value of (j-i)*min(arr[i],arr[j])
        public int MaxArea(int[] height)
        {
            int max = 0;

            int left = 0;
            int right = height.Length - 1;

            while (left < right)
            {
                if (height[left] == 0)
                {
                    left++;
                    continue;
                }

                if (height[right] == 0)
                {
                    right--;
                    continue;
                }

                if (height[left] <= height[right])
                {
                    max = Math.Max(max, (right - left) * height[left]);
                    left++;
                }
                else
                {
                    max = Math.Max(max, (right - left) * height[right]);
                    right--;
                }
            }

            return max;
        }

        public int MaxArea_HeadToEnd(int[] height)
        {
            int max = 0;

            int maxEnd = 0;
            int maxHeight = 0;
            for (int i = 0; i < height.Length - 1; i++)
            {
                if (height[i] == 0)
                    continue;

                if (height[i] * (height.Length - 1 - i + 1) <= max)
                    continue;

                for (int j = i + 1; j < height.Length; j++)
                {
                    if (height[j] == 0)
                        continue;

                    if (j <= maxEnd && height[j] <= maxHeight)
                        continue;

                    var h = Math.Min(height[i], height[j]);
                    var a = (j - i) * h;
                    if (a > max)
                    {
                        max = a;
                        maxEnd = j;
                        maxHeight = h;
                    }
                }
            }

            return max;
        }

        /// 12. Integer to Roman
        /// Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.
        /// I             1
        /// V             5
        /// X             10
        /// L             50
        /// C             100
        /// D             500
        /// M             1000
        /// CM            900
        /// CD            400
        /// XC            90
        /// XL            40
        /// IX            9
        /// IV            4
        public string IntToRoman(int num)
        {
            if ((num <= 0) || (num > 3999)) throw new ArgumentOutOfRangeException();
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

        ///13. Roman to Integer
        ///Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.
        ///It is guaranteed that s is a valid roman numeral in the range [1, 3999]
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

        ///14. Longest Common Prefix
        /// find the longest common prefix string amongst an array of strings.
        /// Input: strs = ["flower","flow","flight"]
        /// Output: "fl"
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 1)
                return strs[0].Length == 0 ? string.Empty : strs[0];

            string result = strs[0];

            for (int i = 1; i < strs.Length; i++)
            {
                result = LongestCommonPrefix(result, strs[i]);
                if (string.IsNullOrEmpty(result))
                    return string.Empty;
            }

            return result;
        }

        public string LongestCommonPrefix(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                return string.Empty;

            List<char> list = new List<char>();

            for (int i = 0; i < str1.Length && i < str2.Length; i++)
            {
                if (str1[i] == str2[i])
                {
                    list.Add(str1[i]);
                }
                else
                {
                    break;
                }
            }

            return list.Count > 0 ? string.Join("", list) : string.Empty;
        }

        ///15. 3Sum
        ///nums[i] + nums[j] + nums[k] == 0. no duplicate values
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            if (nums == null || nums.Length <= 2)
                return new List<IList<int>>();

            var llist = new List<IList<int>>();

            int[] pos = new int[100001];
            int[] neg = new int[100001];

            int right = 0;
            int left = 0;

            foreach (int i in nums)
            {
                if (i == 0)
                {
                    pos[0]++;
                }
                else if (i > 0)
                {
                    pos[i]++;
                    right = Math.Max(right, i);
                }
                else
                {
                    neg[-i]++;
                    left = Math.Max(left, -i);
                }
            }

            if (pos[0] >= 3)
            {
                llist.Add(new List<int> { 0, 0, 0 });
            }

            if (pos[0] >= 1)
            {
                for (int i = 1; i <= right; i++)
                {
                    if (pos[i] > 0 && neg[i] > 0)
                    {
                        llist.Add(new List<int> { -i, 0, i });
                    }
                }
            }

            for (int i = 1; i <= right; i++)
            {
                if (pos[i] == 0)
                    continue;
                for (int j = 1; j <= left; j++)
                {
                    if (i == j)
                        continue;
                    if (neg[j] == 0)
                        continue;

                    var target = j - i;
                    if (target > 0)
                    {
                        if (pos[target] == 0)
                            continue;

                        if (target == i && pos[i] <= 1)
                            continue;

                        //already added
                        if (target < i)
                            continue;

                        llist.Add(new List<int> { -j, i, target });
                    }
                    else if (target < 0)
                    {
                        if (neg[-target] == 0)
                            continue;

                        if (-target == j && neg[j] <= 1)
                            continue;

                        //already added
                        if (-target < j)
                            continue;

                        llist.Add(new List<int> { target, -j, i });
                    }
                    else
                    {
                        //no zero
                    }
                }
            }

            return llist;
        }

        ///16. 3Sum Closest
        ///return three integers nums such that the sum is closest to target.
        ///-1000 <= nums[i] <= 1000, len = [3,1000], -10000<=target<=10000
        public int ThreeSumClosest(int[] nums, int target)
        {
            int closestSum = nums[0] + nums[1] + nums[2];
            Array.Sort(nums);

            // Go through nums
            for (int i = 0; i < nums.Length - 2; i++)
            {
                // do 2sum modified
                int right = nums.Length - 1;
                int left = i + 1;
                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];
                    if (sum == target)
                        return sum;
                    if (Math.Abs(target - sum) < Math.Abs(target - closestSum))
                        closestSum = sum;
                    if (sum > target)
                        right--;
                    else
                        left++;
                }
            }
            return closestSum;
        }

        ///17. Letter Combinations of a Phone Number
        ///digits[i] is a digit in the range['2', '9'].
        /// 2- ABC, 3-DEF, 4-GHI, 5-JKL,6-MNO,7-PQRS,8-TUV 9-WXYZ
        public IList<string> LetterCombinations(string digits)
        {
            Dictionary<char, List<char>> dict = new Dictionary<char, List<char>>
            {
                { '2', new List<char>() { 'a', 'b', 'c' } },
                { '3', new List<char>() { 'd', 'e', 'f' } },
                { '4', new List<char>() { 'g', 'h', 'i' } },
                { '5', new List<char>() { 'j', 'k', 'l' } },
                { '6', new List<char>() { 'm', 'n', 'o' } },
                { '7', new List<char>() { 'p', 'q', 'r', 's' } },
                { '8', new List<char>() { 't', 'u', 'v' } },
                { '9', new List<char>() { 'w', 'x', 'y', 'z' } }
            };

            var results = new List<List<char>>();
            foreach (var d in digits)
            {
                if (results.Count == 0)
                {
                    List<List<char>> list = new List<List<char>>();

                    foreach (char c in dict[d])
                    {
                        list.Add(new List<char> { c });
                    }

                    results = list;
                }
                else
                {
                    List<List<char>> list = new List<List<char>>();

                    foreach (var l in results)
                    {
                        foreach (char c in dict[d])
                        {
                            var sub = new List<char>(l)
                            {
                                c
                            };
                            list.Add(sub);
                        }
                    }

                    results = list;
                }
            }

            return results.Select(x => string.Join("", x)).ToList();
        }

        /// 18. 4Sum
        ///Given an array nums of n integers, return an array of all the unique
        ///quadruplets [nums[a], nums[b], nums[c], nums[d]] such that:
        ///Input: nums = [2,2,2,2,2], target = 8
        ///Output: [[2,2,2,2]]
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            List<IList<int>> result = new List<IList<int>>();
            int len = nums.Length;
            Array.Sort(nums);
            for (int i = 0; i < len - 3; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                    continue;

                for (int j = i + 1; j < len - 2; j++)
                {
                    if (j > i + 1 && nums[j] == nums[j - 1])
                        continue;

                    var sum = nums[i] + nums[j];

                    int left = j + 1;
                    int right = len - 1;

                    while (left < right)
                    {
                        var t = nums[left] + nums[right] + sum;

                        if (t == target)
                        {
                            result.Add(new List<int> { nums[i], nums[j], nums[left], nums[right] });
                            while (left < len - 1 && nums[left] == nums[left + 1])
                                left++;
                            while (right > 0 && nums[right] == nums[right - 1])
                                right--;

                            left++;
                            right--;
                        }
                        else if (t < target)
                            left++;
                        else
                            right--;
                    }
                }
            }
            return result;
        }

        ///19. Remove Nth Node From End of List
        ///Given the head of a linked list, remove the nth node from the end of the list and return its head.
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

        ///20. Valid Parentheses
        ///Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.
        public bool IsValid(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            Stack<char> stack = new Stack<char>();
            foreach (var c in s)
            {
                if (c == '[' || c == '{' || c == '(')
                {
                    stack.Push(c);
                }

                if (c == ']' || c == '}' || c == ')')
                {
                    if (stack.Count == 0)
                        return false;

                    var a = stack.Pop();

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

            return stack.Count == 0;
        }

        ///21. Merge Two Sorted Lists
        ///Merge the two lists in a one sorted list. The list should be made by splicing together the nodes of the first two lists.
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

        ///22. Generate Parentheses ()
        ///Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.
        ///1 <= n <= 8, if n=3, return ["((()))","(()())","(())()","()(())","()()()"]
        public IList<string> GenerateParenthesis(int n)
        {
            List<string> list = new List<string>();
            int i = 1;
            while (i <= n)
            {
                List<string> next = new List<string>();
                if (list.Count == 0)
                {
                    next.Add("()");
                }
                else
                {
                    foreach (var l in list)
                    {
                        int j = 0;
                        while (j < l.Length)
                        {
                            if (l[j] == '(' || (l[j - 1] == '(' && l[j] == ')'))
                            {
                                string str = l.Insert(j, "()");
                                if (!next.Contains(str))
                                    next.Add(str);
                            }
                            j++;
                        }
                    }
                }
                list = next;
                i++;
            }
            return list;
        }
        ///23. Merge k Sorted Lists
        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0)
                return null;

            var nodes = lists.Where(x => x != null).ToList();

            if (nodes == null || nodes.Count == 0)
                return null;

            if (nodes.Count == 1)
                return nodes[0];

            return nodes.Aggregate((x, y) => MergeKLists_Merge2(x, y));
        }

        public ListNode MergeKLists_Merge2(ListNode node1, ListNode node2)
        {
            while (node2 != null)
            {
                var next = node2.next;
                if (node2.val <= node1.val)
                {
                    var temp = node1;
                    node2.next = temp;
                    node1 = node2;
                }
                else
                {
                    var node = node1;
                    while (node != null)
                    {
                        if (node.val < node2.val && (node.next == null || node.next.val >= node2.val))
                        {
                            var temp = node.next;
                            node.next = node2;
                            node2.next = temp;
                            break;
                        }
                        node = node.next;
                    }
                }
                node2 = next;
            }
            return node1;
        }

        ///24. Swap Nodes in Pairs
        ///swap every two adjacent nodes and return its head. head = [1,2,3,4] => Output: [2,1,4,3]
        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            var node = head;
            //var pre=node.next;
            var biNext = node.next.next;
            var node1 = node;
            var node2 = node.next;
            node1.next = biNext;
            node2.next = node1;
            var pre = node2.next;
            head = node2;
            node = biNext;
            while (node != null && node.next != null)
            {
                biNext = node.next.next;
                pre.next = node.next;
                pre.next.next = node;
                node.next = biNext;
                pre = node;
                node = biNext;
            }

            return head;
        }
        ///25. Reverse Nodes in k-Group
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (k == 1)
                return head;
            ListNode pre = null;
            ListNode node = head;
            while (node != null)
            {
                int i = 0;
                Stack<ListNode> stack = new Stack<ListNode>();
                while (i < k)
                {
                    if (node == null)
                        break;
                    stack.Push(node);
                    node = node.next;
                    i++;
                }
                if (i == k)
                {
                    ListNode subNode = stack.Pop();
                    ListNode curr = subNode;
                    while (stack.Count != 0)
                    {
                        var pop = stack.Pop();
                        curr.next = pop;
                        curr = curr.next;
                    }
                    curr.next = node;
                    if (pre == null)
                    {
                        head = subNode;
                    }
                    else
                    {
                        pre.next = subNode;
                    }
                    pre = curr;
                }
                else
                {
                    break;
                }
            }
            return head;
        }

        ///26. Remove Duplicates from Sorted Array
        public int RemoveDuplicates(int[] nums)
        {
            int[] arr = new int[201];
            foreach (var i in nums)
                arr[i + 100]++;
            var count = arr.Where(x => x != 0).Count();
            int[] ans = new int[count];
            int j = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    nums[j] = i - 100;
                    j++;
                }
            }
            return count;
        }

        ///27. Remove Element
        ///Do not allocate extra space for another array.
        ///You must do this by modifying the input array in-place with O(1) extra memory.
        public int RemoveElement(int[] nums, int val)
        {
            int count = 0;
            for (int i = 0; i < nums.Length - count; i++)
            {
                while (nums[i] == val)
                {
                    count++;
                    int k = 0;
                    while (k < nums.Length - 1 - i)
                    {
                        nums[i + k] = nums[i + k + 1];
                        k++;
                    }
                    if (i >= nums.Length - count)
                        break;
                }
            }

            return nums.Length - count;
        }

        ///28. Implement strStr()
        ///Return the index of the first occurrence of needle in haystack, or -1 if needle is not part of haystack.
        ///0 <= haystack.length, needle.length <= 5 * 10^4
        public int StrStr(string haystack, string needle)
        {
            if (string.IsNullOrEmpty(needle))
                return 0;

            for (int i = 0; i < haystack.Length + 1 - needle.Length; i++)
            {
                if (haystack[i] == needle[0])
                {
                    int j = 1;
                    while (j < needle.Length && i + j < haystack.Length)
                    {
                        if (needle[j] != haystack[i + j])
                            break;
                        j++;
                    }

                    if (j == needle.Length)
                        return i;
                }
            }
            return -1;
        }
        ///29. Divide Two Integers
        ///-2^31 <= dividend, divisor <= 2^31 - 1, divisor!=0
        public int Divide(int dividend, int divisor)
        {
            if (dividend == 0)
                return 0;

            if (divisor == 1)
                return dividend;

            if (divisor == -1)
            {
                return dividend == int.MinValue ? int.MaxValue : -dividend;
            }

            if (divisor == int.MinValue)
            {
                return dividend == int.MinValue ? 1 : 0;
            }

            if (divisor == int.MaxValue)
            {
                if (dividend <= int.MinValue + 1)
                {
                    return -1;
                }
                else if (dividend == int.MaxValue)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }


            int ans = 0;

            if (dividend > 0)
            {
                if (divisor > 0)
                {
                    while (dividend - divisor >= 0)
                    {
                        dividend -= divisor;
                        ans++;
                    }
                }
                else
                {
                    while (dividend + divisor >= 0)
                    {
                        dividend += divisor;
                        ans--;
                    }
                }
            }
            else
            {
                if (divisor > 0)
                {
                    while (dividend + divisor <= 0)
                    {
                        dividend += divisor;
                        ans--;
                    }
                }
                else
                {
                    while (dividend - divisor <= 0)
                    {
                        dividend -= divisor;
                        if (ans < int.MaxValue)
                            ans++;
                    }
                }
            }

            return ans;

        }
        ///30. Substring with Concatenation of All Words
        ///return index of substring == all words, s = "barfoothefoobarman", words = ["foo","bar"], return [0,9]
        ///1 <= s.length <= 104,1 <= words.length <= 5000,1 <= words[i].length <= 30, all lower-case letter
        public IList<int> FindSubstring(string s, string[] words)
        {
            List<int> ans = new List<int>();
            int len = words.Select(x => x.Length).Sum();
            List<string> exists = new List<string>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (dict.ContainsKey(word))
                {
                    dict[word]++;
                }
                else
                {
                    dict.Add(word, 1);
                }
            }
            for (int i = 0; i < s.Length + 1 - len; i++)
            {
                if (exists.Where(x => x == s.Substring(i, len)).Count() > 0)
                {
                    ans.Add(i);
                    continue;
                }
                Dictionary<string, int> dict2 = new Dictionary<string, int>();
                foreach (var d in dict)
                    dict2.Add(d.Key, d.Value);

                bool find = FindSubstring(s, words, i, dict2);
                if (find)
                {
                    ans.Add(i);
                    exists.Add(s.Substring(i, len));
                }
            }
            return ans;
        }
        public bool FindSubstring(string s, string[] words, int index, Dictionary<string, int> dict)
        {
            if (words.Length == 0)
                return true;
            if (words.Length == 1)
            {
                return s.Substring(index, words[0].Length) == words[0];
            }
            var pools = dict.Where(x => x.Value > 0).Select(x => x.Key).ToList();
            var matchs = pools.Where(x => x == s.Substring(index, x.Length)).ToList();
            if (matchs.Count > 0)
            {
                foreach (var match in matchs)
                {
                    List<string> list = new List<string>(words);
                    list.Remove(match);
                    dict[match]--;
                    bool find = FindSubstring(s, list.ToArray(), index + match.Length, dict);
                    if (find)
                        return true;
                }
            }
            return false;
        }
        ///31. Next Permutation
        ///For example, the next permutation of arr = [1,2,3] is [1,3,2].
        ///Find last num that nums[i+1]>nums[i], then find the last nums[j]>nums[i],
        ///if cannot find num, is the last sequence, then sort() it;
        public void NextPermutation(int[] nums)
        {
            if (nums.Length == 1)
                return;
            int i = 0;
            //Find first no in nums which is smaller than its previous no.
            for (i = nums.Length - 1; i > 0; i--)
            {
                if (nums[i - 1] < nums[i])
                    break;
            }
            if (i != 0)
            {
                //Find first greater than i-1 and swap i-1 and j
                for (int j = nums.Length - 1; j >= i; j--)
                {
                    if (nums[i - 1] < nums[j])
                    {
                        //swap
                        int temp = nums[i - 1];
                        nums[i - 1] = nums[j];
                        nums[j] = temp;
                        break;
                    }
                }
            }
            //why????  reverse everything from i to end
            reverse(nums, i, nums.Length - 1);
        }

        public void reverse(int[] nums, int start, int end)
        {
            for (int i = start, j = end; i < j; i++, j--)
            {
                int temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;
            }
        }
        ///32. Longest Valid Parentheses
        ///Given a string containing just the characters '(' and ')',
        ///find the length of the longest valid (well-formed) parentheses substring.
        public int LongestValidParentheses(string s)
        {
            if (s.Length <= 1)
                return 0;
            int maxLen = 0;
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length - 1 - maxLen; i++)
            {
                if (s[i] == '(')
                {
                    int j = i;
                    int len = 0;
                    int count = 0;
                    stack.Clear();
                    while (j < s.Length)
                    {
                        if (s[j] == '(')
                        {
                            stack.Push(s[j]);
                        }
                        else
                        {
                            if (stack.Count == 0)
                            {
                                break;
                            }
                            else
                            {
                                var c = stack.Pop();
                                if (c == '(')
                                {
                                    if (stack.Count == 0)
                                    {
                                        count++;
                                        len += 2 * count;
                                        count = 0;
                                    }
                                    else
                                    {
                                        count++;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        j++;
                    }
                    if (stack.Count == 0)
                        i += len;
                    maxLen = Math.Max(maxLen, len);
                }
            }
            return maxLen;
        }
        /// 35. Search Insert Position

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

        ///33. Search in Rotated Sorted Array
        /// original array [1,2,3,4,5] sorted in ascending order (with distinct values).
        /// then possibly rotated to eg. [3,4,5,1,2]
        /// return the index of target if it is in nums or -1
        public int Search(int[] nums, int target)
        {
            if (nums.Length == 1)
                return nums[0] == target ? 0 : -1;

            int i = 0;
            while (i <= nums.Length - 1)
            {
                if (nums[i] == target)
                    return i;

                if (nums[i] < nums[0])
                {
                    //rotated
                    if (target > nums[0])
                    {
                        return -1;
                    }

                    if (target < nums[i])
                    {
                        return -1;
                    }
                }

                i++;
            }

            return -1;
        }

        /// 34. Find First and Last Position of Element in Sorted Array
        ///[5,7,7,8,8,10], target = 8, return [3,4], if not found return [-1,-1]
        public int[] SearchRange(int[] nums, int target)
        {
            int[] result = new int[] { -1, -1 };
            if (nums.Length == 0)
                return result;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < target)
                {
                    continue;
                }
                else if (nums[i] == target)
                {
                    if (result[0] == -1)
                    {
                        result[0] = i;

                        if (result[1] == -1)
                        {
                            result[1] = i;
                        }
                    }
                    else
                    {
                        result[1] = i;
                    }
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        /// 36. Valid Sudoku

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

        ///38. Count and Say
        ///countAndSay(n) is the way you would "say" the digit string from countAndSay(n-1),countAndSay(1) = "1"
        ///eg. countAndSay(2)=say 'one' '1'= 11,countAndSay(3)=two '1' =21, countAndSay(3)=one '2' one '1'=1211
        public string CountAndSay(int n)
        {
            string ans = "1";
            int i = 1;
            while (i++ < n)
                ans = CountAndSay(ans);
            return ans;
        }

        public string CountAndSay(string s)
        {
            List<char> ans = new List<char>();
            char c = s[0];
            int count = 1;
            for (int i = 1; i < s.Length; i++)
            {
                if (c == s[i])
                {
                    count++;
                }
                else
                {
                    if (count != 0)
                    {
                        ans.Add(getChar(count));
                        ans.Add(c);
                    }
                    count = 1;
                    c = s[i];
                }
            }
            if (count != 0)
            {
                ans.Add(getChar(count));
                ans.Add(c);
            }
            return string.Join("", ans);
        }

        ///39. Combination Sum
        ///Given an array of distinct integers candidates and a target integer target,
        ///return a list of all unique combinations of candidates where the chosen numbers sum to target.
        ///The same number may be chosen from candidates an unlimited number of times.
        ///count of return <= 150, 1 <= candidates[i] <= 200, 1 <= target <= 500
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var ans = new List<IList<int>>();
            var nums = candidates.OrderBy(x => -x).ToList();
            var llist = new List<IList<int>>();
            bool first = true;
            for (int i = 0; i < nums.Count; i++)
            {
                if (nums[i] > target)
                    continue;
                var next = new List<IList<int>>();
                if (first)
                {
                    first = false;
                    int n = target / nums[i];
                    int m = target % nums[i];
                    if (m == 0)
                    {
                        var comb = new List<int>();
                        for (int j = 0; j < n; j++)
                            comb.Add(nums[i]);
                        ans.Add(comb);
                        n--;
                    }
                    //remember add empty to next index
                    int k = 0;
                    while (k <= n)
                    {
                        var sub = new List<int>();
                        for (int j = 0; j < k; j++)
                            sub.Add(nums[i]);
                        next.Add(sub);
                        k++;
                    }
                }
                else
                {
                    foreach (var list in llist)
                    {
                        int goal = target - list.Sum();
                        if (goal < nums[i])
                        {
                            //add to next index
                            next.Add(list);
                            continue;
                        }
                        int n = goal / nums[i];
                        int m = goal % nums[i];
                        if (m == 0)
                        {
                            var comb = new List<int>(list);
                            for (int j = 0; j < n; j++)
                                comb.Add(nums[i]);
                            ans.Add(comb);
                            n--;
                        }
                        //remember add empty to next index
                        int k = 0;
                        while (k <= n)
                        {
                            var sub = new List<int>(list);
                            for (int j = 0; j < k; j++)
                                sub.Add(nums[i]);
                            next.Add(sub);
                            k++;
                        }
                    }
                }
                llist = next;
            }
            return ans;
        }
        ///40. Combination Sum II
        ///Each number in candidates may only be used once in the combination.
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            var ans = new List<IList<int>>();
            var nums = candidates.OrderBy(x => -x).ToList();

            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            for (int j = 0; j < nums.Count; j++)
            {
                if (dict.ContainsKey(nums[j]))
                {
                    dict[nums[j]].Add(j);
                }
                else
                {
                    dict.Add(nums[j], new List<int>() { j });
                }
            }

            var llist = new List<IList<int>>();
            bool first = true;
            for (int i = 0; i < nums.Count; i++)
            {
                if (nums[i] > target)
                    continue;
                var next = new List<IList<int>>();
                if (first)
                {
                    first = false;
                    int n = target / nums[i];
                    if (target == nums[i])
                    {
                        var comb = new List<int>() { nums[i] };
                        ans.Add(comb);
                        n--;
                    }

                    //remember add empty to next index
                    int k = 0;
                    while (k <= Math.Min(n, 1))
                    {
                        var sub = new List<int>();
                        for (int j = 0; j < k; j++)
                            sub.Add(nums[i]);
                        next.Add(sub);
                        k++;
                    }
                }
                else
                {
                    foreach (var list in llist)
                    {
                        if (dict[nums[i]].Count > 1)
                        {
                            var idx = dict[nums[i]].IndexOf(i);
                            int count = list.Where(x => x == nums[i]).Count();
                            if (count != idx)
                            {
                                //add to next index
                                next.Add(list);
                                continue;
                            }
                        }

                        int goal = target - list.Sum();
                        if (goal < nums[i])
                        {
                            //add to next index
                            next.Add(list);
                            continue;
                        }

                        int n = goal / nums[i];
                        if (goal == nums[i])
                        {
                            var comb = new List<int>(list)
                            {
                                nums[i]
                            };
                            ans.Add(comb);
                            n--;
                        }
                        //remember add empty to next index
                        int k = 0;
                        while (k <= Math.Min(n, 1))
                        {
                            var sub = new List<int>(list);
                            for (int j = 0; j < k; j++)
                                sub.Add(nums[i]);
                            next.Add(sub);
                            k++;
                        }
                    }
                }
                llist = next;
            }
            return ans;
        }
        /// 43. Multiply Strings
        ///Given two non-negative integers num1 and num2 represented as strings,
        ///return the product of num1 and num2, also represented as a string.
        public string Multiply(string num1, string num2)
        {
            int[] digits1 = new int[num1.Length];
            for (int i = 0; i < digits1.Length; i++)
            {
                digits1[digits1.Length - 1 - i] = getDigit(num1[i]);
            }

            int[] digits2 = new int[num2.Length];
            for (int i = 0; i < digits2.Length; i++)
            {
                digits2[digits2.Length - 1 - i] = getDigit(num2[i]);
            }

            int[] result = new int[digits1.Length + digits2.Length];

            int carry;

            for (int i = 0; i < digits1.Length; i++)
            {
                for (int j = 0; j < digits2.Length; j++)
                {
                    result[i + j] += digits1[i] * digits2[j];
                    carry = result[i + j] / 10;
                    result[i + j] %= 10;
                    if (carry > 0)
                    {
                        result[i + j + 1] += carry;
                        carry = 0;
                    }
                }
            }

            int len = result.Length;

            int a = result.Length - 1;
            while (a >= 1)
            {
                if (result[a] == 0)
                {
                    len--;
                    a--;
                }
                else
                {
                    break;
                }
            }


            char[] ans = new char[len];

            for (int i = 0; i < len; i++)
            {
                ans[i] = getChar(result[len - 1 - i]);
            }

            return string.Join("", ans);
        }
        /// 45. Jump Game II
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

        ///46. Permutations
        ///1 <= nums.length <= 6, -10 <= nums[i] <= 10
        ///All the integers of nums are unique.
        public IList<IList<int>> Permute(int[] nums)
        {
            var ans = new List<IList<int>>();
            for (int j = 0; j < nums.Length; j++)
            {
                var list = new List<int>
                {
                    nums[j]
                };
                ans.Add(list);
            }
            int i = 1;
            while (i < nums.Length)
            {
                var next = new List<IList<int>>();

                for (int k = 0; k < ans.Count; k++)
                {
                    for (int j = 0; j < nums.Length; j++)
                    {
                        if (ans[k].Contains(nums[j]))
                            continue;

                        var sub = new List<int>(ans[k])
                        {
                            nums[j]
                        };
                        next.Add(sub);
                    }
                }
                ans = next;
                i++;
            }

            return ans;
        }

        ///47. Permutations II
        ///1 <= nums.length <= 8, -10 <= nums[i] <= 10
        ///Given a collection of numbers that might contain duplicates, return all possible unique permutations in any order.
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            var idxList = new List<IList<int>>();

            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();

            for (int j = 0; j < nums.Length; j++)
            {
                if (dict.ContainsKey(nums[j]))
                {
                    dict[nums[j]].Add(j);
                }
                else
                {
                    dict.Add(nums[j], new List<int>() { j });
                }
            }

            int i = 0;
            while (i < nums.Length)
            {
                var next = new List<IList<int>>();

                if (i == 0)
                {
                    for (int j = 0; j < nums.Length; j++)
                    {
                        if (dict[nums[j]].IndexOf(j) > 0)
                            continue;
                        next.Add(new List<int>() { j });
                    }
                }
                else
                {
                    for (int k = 0; k < idxList.Count; k++)
                    {
                        for (int j = 0; j < nums.Length; j++)
                        {
                            if (idxList[k].Contains(j))
                                continue;

                            if (dict[nums[j]].Count > 1)
                            {
                                var idx = dict[nums[j]].IndexOf(j);
                                int count = idxList[k].Where(x => dict[nums[j]].Contains(x)).Count();

                                if (count != idx)
                                    continue;
                            }

                            var sub = new List<int>(idxList[k])
                            {
                                j
                            };
                            next.Add(sub);
                        }
                    }
                }

                idxList = next;
                i++;
            }

            var ans = new List<IList<int>>();

            foreach (var list in idxList)
            {
                var data = new List<int>();
                foreach (var j in list)
                    data.Add(nums[j]);
                ans.Add(data);
            }

            return ans;
        }
        /// 48. Rotate Image
        ///You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees (clockwise).
        ///You have to rotate the image in-place, which means you have to modify the input 2D matrix directly.
        ///DO NOT allocate another 2D matrix and do the rotation.
        public void Rotate(int[][] matrix)
        {
            int[][] temp = new int[matrix.Length][];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = new int[matrix[i].Length];

            for (int i = 0; i < temp.Length; i++)
            {
                for (int j = 0; j < temp[i].Length; j++)
                {
                    temp[i][j] = matrix[temp.Length - 1 - j][i];
                }
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    matrix[i][j] = temp[i][j];
                }
            }
        }

        ///49. Group Anagrams
        ///Given an array of strings strs, group the anagrams together. You can return the answer in any order.
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var ans = new List<IList<string>>();

            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();

            for (int i = 0; i < strs.Length; i++)
            {
                var word = string.Join("", strs[i].ToArray().OrderBy(c => c));

                if (dict.ContainsKey(word))
                {
                    dict[word].Add(i);
                }
                else
                {
                    dict.Add(word, new List<int>() { i });
                }
            }

            foreach (var list in dict.Values)
            {
                List<string> words = new List<string>();
                foreach (var i in list)
                {
                    words.Add(strs[i]);
                }
                ans.Add(words);
            }

            return ans;
        }

    }
}