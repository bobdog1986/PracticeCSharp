using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///409. Longest Palindrome
        ///case sensitive, Aa is different
        public int LongestPalindrome(string s)
        {
            if (s.Length <= 1)
                return s.Length;

            Dictionary<char,int> dict = new Dictionary<char,int>();

            foreach(var c in s)
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

            foreach(var d in dict)
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
                {
                    count++;
                }

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

            foreach(var c in num1)
            {
                list1.Insert(0, GetDigitFromChar(c));
            }
            foreach (var c in num2)
            {
                list2.Insert(0, GetDigitFromChar(c));
            }

            List<char> ans=new List<char>();
            bool isCarry = false;

            for(int i=0; i < list1.Count||i<list2.Count; i++)
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

                ans.Insert(0, GetCharFormDigit(c%10));

            }

            if (isCarry)
                ans.Insert(0,'1');

            return String.Join("",ans);
        }

        public char GetCharFormDigit(int c)
        {
            if (c == 0)
                return '0';
            else if (c == 1)
                return '1';
            else if (c == 2)
                return '2';
            else if (c == 3)
                return '3';
            else if (c == 4)
                return '4';
            else if (c == 5)
                return '5';
            else if (c == 6)
                return '6';
            else if (c == 7)
                return '7';
            else if (c == 8)
                return '8';
            else if (c == 9)
                return '9';
            else
                return ' ';
        }

        public int GetDigitFromChar(char c)
        {

            if (c == '0')
                return 0;
            else if (c == '1')
                return 1;
            else if (c == '2')
                return 2;
            else if (c == '3')
                return 3;
            else if (c == '4')
                return 4;
            else if (c == '5')
                return 5;
            else if (c == '6')
                return 6;
            else if (c == '7')
                return 7;
            else if (c == '8')
                return 8;
            else if (c == '9')
                return 9;
            else
                return -1;

        }

        /// 435. Non-overlapping Intervals
        /// there are some embeded intervals, use Math.Min()
        public int EraseOverlapIntervals(int[][] intervals)
        {
            int ans=0;
            var mat = intervals.OrderBy(x => x[0]).ToList();

            int end = mat[0][1];

            for(int i = 1; i < mat.Count; i++)
            {
                if (mat[i][0] < end)
                {
                    ans++;
                    end = Math.Min(end, mat[i][1]);
                }
                else
                {
                    end=mat[i][1];
                }
            }

            return ans;
        }
        /// 438. Find All Anagrams in a string
        /// should use sliding window
        public List<int> FindAnagrams(string s, string p)
        {
            if (p.Length > s.Length)
                return new List<int>();
            int left = 0, right = 0;
            int[] arr = new int[26];
            int[] target = new int[26];
            List<int> al = new List<int>();

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
                for(int i = 0; i < 26; i++)
                {
                    if(arr[i] != target[i])
                    {
                        isEqual = false;
                        break;
                    }
                }
                if (isEqual)
                    al.Add(left);

                right++;
                if (right < s.Length)
                    arr[s[right] - 'a']++;

                arr[s[left] - 'a']--;
                left++;
            }
            return al;
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

        ///452. Minimum Number of Arrows to Burst Balloons
        ///points.Length = Balloons number, Balloons horizontal -231 <= xstart < xend <= 231 - 1
        public int FindMinArrowShots(int[][] points)
        {
            if (points.Length <= 1)
                return points.Length;

            var arr = points.OrderBy(p => p[1]).ToList();

            var shot = 1;
            int end = arr[0][1];

            for (int i = 1; i < arr.Count; i++)
            {
                if (end < arr[i][0])
                {
                    end = arr[i][1];
                    shot++;
                }
            }

            return shot;
        }

        /// 492
        public int[] ConstructRectangle(int area)
        {
            int[] result = new int[2] { area, 1 };
            int min = (int)Math.Sqrt(area);
            for (int len = min; len < area; len++)
            {
                if (area % len == 0)
                {
                    if (len >= area / len)
                    {
                        return new int[2] { len, area / len };
                    }
                }
            }
            return result;
        }

        //495
        public int FindPoisonedDuration(int[] timeSeries, int duration)
        {
            if (timeSeries == null || timeSeries.Length == 0) return 0;

            Array.Sort(timeSeries);

            int begin = timeSeries[0];
            int expired = begin + duration;

            int total = 0;
            for (int i = 1; i < timeSeries.Length; i++)
            {
                if (timeSeries[i] < expired)
                {
                    expired = timeSeries[i] + duration;
                }
                else
                {
                    total += expired - begin;

                    begin = timeSeries[i];
                    expired = begin + duration;
                }
            }

            total += expired - begin;
            return total;
        }
    }
}