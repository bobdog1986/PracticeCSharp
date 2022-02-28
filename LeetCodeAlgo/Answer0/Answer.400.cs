using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///402. Remove K Digits
        ///a non-negative integer num, and an integer k, return the smallest possible integer after removing k digits from num.
        ///1 <= k <= num.length <= 105
        ///num consists of only digits.
        ///num does not have any leading zeros except for the zero itself.
        public string RemoveKdigits(string num, int k)
        {
            if (k == num.Length) return "0";

            var digits = num.ToArray().ToList();
            int i = 0;
            while (i++ < k)
            {
                int j = 0;
                while (j < digits.Count)
                {
                    if (j == digits.Count - 1 || digits[j] > digits[j + 1])
                    {
                        digits.RemoveAt(j);
                        break;
                    }
                    j++;
                }
            }

            i = 0;
            int count = digits.Count;
            while (i++ < count)
            {
                if (digits.Count > 0 && digits[0] == '0')
                {
                    digits.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
            return digits.Count == 0 ? "0" : new string(digits.ToArray());
        }
        /// 409. Longest Palindrome
        ///case sensitive, Aa is different
        public int LongestPalindrome_409(string s)
        {
            if (s.Length <= 1)
                return s.Length;

            Dictionary<char, int> dict = new Dictionary<char, int>();

            foreach (var c in s)
            {
                if (dict.ContainsKey(c))
                {
                    dict[c]++;
                }
                else
                {
                    dict.Add(c, 1);
                }
            }

            int sumOfEven = 0;
            int sumOfOdd = 0;

            foreach (var d in dict)
            {
                sumOfEven += d.Value / 2;
                sumOfOdd += d.Value % 2;
            }

            int ans = sumOfEven * 2;

            if (sumOfOdd > 0)
                ans++;

            return ans;
        }

        ///412. Fizz Buzz
        ///Given an integer n, return a string array answer (1-indexed) where:
        ///answer[i] == "FizzBuzz" if i is divisible by 3 and 5.
        ///answer[i] == "Fizz" if i is divisible by 3.
        ///answer[i] == "Buzz" if i is divisible by 5.
        ///answer[i] == i(as a string) if none of the above conditions are true.
        public IList<string> FizzBuzz(int n)
        {
            var ans = new string[n];
            for (int i = 1; i <= n; i++)
            {
                ans[i - 1] = i.ToString();
            }
            for (int i = 3-1; i < n; i += 3)
            {
                ans[i] = "Fizz";
            }
            for (int i = 5-1; i < n; i += 5)
            {
                ans[i] = "Buzz";
            }
            for (int i = 15-1; i < n; i += 15)
            {
                ans[i] = "FizzBuzz";
            }
            return ans;
        }

        /// 413. Arithmetic Slices, #DP
        ///at least 3 nums with same distance, eg. [1,2,3,4], [1,1,1]
        ///-1000 <= nums[i] <= 1000
        ///A subarray is a contiguous subsequence of the array.
        public int NumberOfArithmeticSlices_413(int[] nums)
        {
            int ans = 0;
            int dp = 0;
            for (int i = 2; i < nums.Length; i++)
            {
                if (nums[i] - nums[i - 1] == nums[i - 1] - nums[i - 2])
                {
                    dp++;
                    ans += dp;
                }
                else
                {
                    dp = 0;
                }
            }
            return ans;
        }

        ///415. Add Strings
        ///Given two non-negative integers, num1 and num2 represented as string, return the sum of num1 and num2 as a string.
        // input maybe so big!!! cannot use any int/long type
        public string AddStrings(string num1, string num2)
        {
            if (num1 == "0")
                return num2;

            if (num2 == "0")
                return num1;

            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();

            foreach (var c in num1)
            {
                list1.Insert(0, getDigit(c));
            }
            foreach (var c in num2)
            {
                list2.Insert(0, getDigit(c));
            }

            List<char> ans = new List<char>();
            bool isCarry = false;

            for (int i = 0; i < list1.Count || i < list2.Count; i++)
            {
                int a = 0;
                if (list1.Count > i)
                    a = list1[i];

                int b = 0;
                if (list2.Count > i)
                    b = list2[i];

                int c = a + b;
                if (isCarry)
                    c++;

                isCarry = c / 10 == 1;

                ans.Insert(0, getChar(c % 10));
            }

            if (isCarry)
                ans.Insert(0, '1');

            return string.Join("", ans);
        }

        public char getChar(int c)
        {
            return (char)(c + '0');
        }

        public int getDigit(char c)
        {
            return c - '0';
        }

        ///421. Maximum XOR of Two Numbers in an Array
        ///return the maximum result of nums[i] XOR nums[j], where 0 <= i <= j < n.
        ///1 <= nums.length <= 2 * 10^5, 0 <= nums[i] <= 2^31 - 1
        public int FindMaximumXOR(int[] nums)
        {
            int ans = 0;

            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    ans = Math.Max(ans, nums[i] ^ nums[j]);
                }
            }
            return ans;
        }

        ///424. Longest Repeating Character Replacement, ### Siding Window
        ///You can choose any character of the string and change it to any other uppercase English character at most k times.
        ///Return the length of the longest substring containing the same letter you can get after performing the above operations.
        ///1 <= s.length <= 10^5, 0 <= k <= s.length, s consists of only uppercase English letters.
        public int CharacterReplacement(string s, int k)
        {
            if (k == s.Length)
                return k;

            int ans = k;
            int count = 0;
            int[] arr = new int[26];
            int left = 0;
            int max = 0;
            for (int i = 0; i < s.Length; i++)
            {
                arr[s[i] - 'A']++;
                count++;
                max = arr.Max();
                if (max + k >= count)
                {
                    ans = Math.Max(ans, count);
                }
                else
                {
                    arr[s[left] - 'A']--;
                    left++;
                    count--;
                }
            }
            return ans;
        }

        ///429. N-ary Tree Level Order Traversal
        ///Given an n-ary tree, return the level order traversal of its nodes' values.
        public IList<IList<int>> LevelOrder(Node root)
        {
            var ans=new List<IList<int>>();
            var nodes=new List<Node>() { root};
            while(nodes.Count > 0)
            {
                var next = new List<Node>();
                var list = new List<int>();
                foreach(var n in nodes)
                {
                    if (n == null) continue;
                    list.Add(n.val);
                    next.AddRange(n.children);
                }
                if(list.Count>0)ans.Add(list);
                nodes = next;
            }
            return ans;
        }


        /// 435. Non-overlapping Intervals
        /// there are some embeded intervals, use Math.Min()
        public int EraseOverlapIntervals(int[][] intervals)
        {
            int ans = 0;
            var mat = intervals.OrderBy(x => x[0]).ToList();
            int end = mat[0][1];
            for (int i = 1; i < mat.Count; i++)
            {
                if (mat[i][0] < end)
                {
                    ans++;
                    end = Math.Min(end, mat[i][1]);
                }
                else
                {
                    end = mat[i][1];
                }
            }
            return ans;
        }

        /// 438. Find All Anagrams in a string
        /// Input: s = "cbaebabacd", p = "abc", Output: [0,6]
        /// The substring with start index = 0 is "cba", which is an anagram of "abc".
        /// The substring with start index = 6 is "bac", which is an anagram of "abc".
        /// should use sliding window
        public List<int> FindAnagrams(string s, string p)
        {
            var ans = new List<int>();
            if (p.Length > s.Length)
                return ans;
            int left = 0, right = 0;
            int[] arr = new int[26];
            int[] target = new int[26];

            while (right < p.Length)
            {
                arr[s[right] - 'a']++;
                target[p[right] - 'a']++;
                right++;
            }
            right--;

            while (right < s.Length)
            {
                bool isEqual = true;
                for (int i = 0; i < 26; i++)
                {
                    if (arr[i] != target[i])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                    ans.Add(left);

                right++;
                if (right < s.Length)
                    arr[s[right] - 'a']++;

                arr[s[left] - 'a']--;
                left++;
            }
            return ans;
        }

        public IList<int> FindAnagrams_My(string s, string p)
        {
            List<int> ans = new List<int>();

            var arr1 = s.ToArray();
            var arr2 = p.ToArray();

            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (var i in arr2)
            {
                if (dict.ContainsKey(i))
                {
                    dict[i]++;
                }
                else
                {
                    dict.Add(i, 1);
                }
            }

            Dictionary<char, List<int>> match = new Dictionary<char, List<int>>();

            int len = 0;
            int start = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr2.Contains(arr1[i]))
                {
                    if (len == 0)
                        start = i;

                    if (match.ContainsKey(arr1[i]))
                    {
                        if (match[arr1[i]].Count < dict[arr1[i]])
                        {
                            match[arr1[i]].Add(i);
                            len++;
                        }
                        else
                        {
                            int j = match[arr1[i]][0];
                            foreach (var pair in match)
                            {
                                int k = pair.Value.RemoveAll(x => x <= j);
                                len = len - k;
                                start += k;
                            }

                            match[arr1[i]].Add(i);
                            len++;
                        }
                    }
                    else
                    {
                        match.Add(arr1[i], new List<int>() { i });
                        len++;
                    }

                    if (len == arr2.Length)
                    {
                        ans.Add(start);
                        foreach (var m in match)
                        {
                            if (m.Value.Contains(start))
                            {
                                m.Value.Remove(start);
                                break;
                            }
                        }
                        start++;
                        len--;
                    }
                }
                else
                {
                    match.Clear();
                    len = 0;
                }
            }

            return ans;
        }

        /// 443
        public int Compress(char[] chars)
        {
            if (chars.Length == 1) return chars.Length;
            List<char> result = new List<char>();
            char pre = chars[0];
            int occured = 1;
            char current;
            for (int i = 1; i < chars.Length; i++)
            {
                current = chars[i];
                if (current == pre)
                {
                    occured++;
                }
                else
                {
                    result.Add(pre);
                    if (occured > 1) { result.AddRange(occured.ToString().ToCharArray()); }

                    pre = current;
                    occured = 1;
                }

                if (i == chars.Length - 1)
                {
                    result.Add(pre);
                    if (occured > 1) { result.AddRange(occured.ToString().ToCharArray()); }
                }
            }

            Array.Copy(result.ToArray(), chars, result.Count);
            return result.Count;
        }

        public char GetSingleNumChar(int num)
        {
            return (char)(num + 0x30);
        }

        public char[] GetNumCharArray(int num)
        {
            return num.ToString().ToCharArray();
        }

        ///446. Arithmetic Slices II - Subsequence, #DP
        ///at least 3 nums with same distance, eg. [1,2,3,4], [1,1,1]
        ///1  <= nums.length <= 1000, -2^31 <= nums[i] <= 2^31 - 1
        ///Subsequence may not continuous
        public int NumberOfArithmeticSlices(int[] nums)
        {
            int ans = 0;

            return ans;
        }
    }
}