using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///409. Longest Palindrome
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

        /// 413. Arithmetic Slices
        ///at least 3 nums with same distance, eg. [1,2,3,4], [1,1,1]
        ///-1000 <= nums[i] <= 1000
        ///A subarray is a contiguous subsequence of the array.
        public int NumberOfArithmeticSlices(int[] nums)
        {
            if (nums == null || nums.Length <= 2)
                return 0;
            int sum = 0;
            for (int i = 0; i < nums.Length - 2; i++)
            {
                int len = nums[i + 1] - nums[i];
                int count = 0;
                while (i + count * 1 < nums.Length && nums[i + count * 1] == nums[i] + count * len)
                    count++;

                if (count >= 3)
                {
                    int j = 3;
                    while (j <= count)
                    {
                        sum += count - (j - 1);
                        j++;
                    }
                }
                i += count - 2;
            }
            return sum;
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
        public int FindMaximumXOR(int[] nums)
        {
            int ans = 0;

            for(int i = 0;i < nums.Length-1; i++)
            {
                for(int j = i+1; j < nums.Length; j++)
                {
                    ans = Math.Max(ans, nums[i] ^ nums[j]);
                }
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
            var ans= new List<int>();
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


    }
}