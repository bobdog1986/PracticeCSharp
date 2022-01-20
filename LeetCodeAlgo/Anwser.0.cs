using System;
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

            if (s.Length == 1)
                return s;

            string result = s[s.Length / 2].ToString();

            LongestPalindrome_Odd_Recursion(s, (s.Length - 1) / 2, ref result);
            LongestPalindrome_Odd_Recursion(s, (s.Length + 1) / 2, ref result);

            LongestPalindrome_Even_Recursion(s, (s.Length - 1) / 2, ref result);
            LongestPalindrome_Even_Recursion(s, (s.Length + 1) / 2, ref result);

            return result;
        }

        public void LongestPalindrome_Odd_Recursion(string s, int center, ref string result)
        {
            if (center <= 0 || center >= s.Length - 1)
                return;

            List<char> list = new List<char>() { s[center] };

            for (int i = 1; i <= center && i <= s.Length - 1 - center; i++)
            {
                if (s[center - i] == s[center + i])
                {
                    list.Add(s[center + i]);
                    list.Insert(0, s[center - i]);
                }
                else
                {
                    if (list.Count > result.Length)
                    {
                        result = String.Join("", list);
                    }

                    if (center <= (s.Length - 1) / 2)
                    {
                        LongestPalindrome_Odd_Recursion(s, center - 1, ref result);
                    }
                    if (center >= (s.Length + 1) / 2)
                    {
                        LongestPalindrome_Odd_Recursion(s, center + 1, ref result);
                    }
                    break;
                }
            }

            if (list.Count > result.Length)
            {
                result = String.Join("", list);
            }
        }

        public void LongestPalindrome_Even_Recursion(string s, int center, ref string result)
        {
            if (center < 0 || center > s.Length - 1)
                return;

            List<char> list = new List<char>();

            for (int i = 0; i <= center && i <= s.Length - 1 - center - 1; i++)
            {
                if (s[center - i] == s[center + i + 1])
                {
                    list.Add(s[center + i + 1]);
                    list.Insert(0, s[center - i]);
                }
                else
                {
                    if (center <= (s.Length - 1) / 2)
                    {
                        LongestPalindrome_Even_Recursion(s, center - 1, ref result);
                    }
                    if (center >= (s.Length + 1) / 2)
                    {
                        LongestPalindrome_Even_Recursion(s, center + 1, ref result);
                    }
                    break;
                }
            }

            if (list.Count > result.Length)
            {
                result = String.Join("", list);
            }
        }

        ///6. Zigzag Conversion
        ///Zigzag is Z line, row=3
        ///A     A     A
        ///A  A  A  A  A
        ///A     A
        public string Convert_6(string s, int numRows)
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

            return String.Join("", list);
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

            return list.Count > 0 ? String.Join("", list) : String.Empty;
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

        /// <summary>
        /// NOT PASS , time out
        /// </summary>
        public void ThreeSum_Recursion(int[] nums, int left, int right, IList<IList<int>> result)
        {
            if (left + 1 >= right)
                return;

            if (result.FirstOrDefault(x => x[0] == nums[left] && x[2] == nums[right]) != null)
            {
                ThreeSum_Recursion(nums, left + 1, right, result);
                ThreeSum_Recursion(nums, left, right - 1, result);
            }

            if (nums[right - 2] + nums[right - 1] + nums[right] < 0)
                return;
            if (nums[left] + nums[left + 1] + nums[left + 2] > 0)
                return;

            int sum = nums[left] + nums[right];

            int target = 0 - sum;

            var idx = binarySearch(nums, left, right, target);
            if (idx == -1)
            {
            }
            else
            {
                List<int> list = new List<int>() { nums[left], nums[idx], nums[right] };

                if (result.FirstOrDefault(l => l[0] == nums[left]
                                         && l[1] == nums[idx]
                                         && l[2] == nums[right]) == null)
                {
                    result.Add(list);
                }
            }

            ThreeSum_Recursion(nums, left + 1, right, result);
            ThreeSum_Recursion(nums, left, right - 1, result);
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

        public int ThreeSumClosest_My(int[] nums, int target)
        {
            if (nums.Length == 3)
                return nums[0] + nums[1] + nums[2];

            int ship = 1000;
            int[] arr = new int[2001];
            int startIndex = 2000;
            int endIndex = 0;

            foreach (int i in nums)
            {
                arr[i + ship]++;
                startIndex = Math.Min(i + ship, startIndex);
                endIndex = Math.Max(i + ship, endIndex);
            }

            int allShip = 3000;
            int shipTarget = target + allShip;

            int maxOf3 = 0;
            int maxOf2 = 0;
            //int maxOf1 = endIndex;
            int needNumb = 3;
            for (int i = endIndex; i >= startIndex && needNumb > 0; i--)
            {
                if (arr[i] == 0)
                    continue;

                int j = arr[i];
                while (needNumb > 0 && j > 0)
                {
                    maxOf3 += i;
                    needNumb--;
                    j--;

                    if (needNumb == 1)
                        maxOf2 = maxOf3;
                }
            }

            //target >= sum of max3, just return maxOf3
            if (shipTarget >= maxOf3)
                return maxOf3 - allShip;

            int minOf3 = 0;
            int minOf2 = 0;
            //int minOf1 = startIndex;
            needNumb = 3;
            for (int i = startIndex; i <= endIndex && needNumb > 0; i++)
            {
                if (arr[i] == 0)
                    continue;

                int j = arr[i];
                while (needNumb > 0 && j > 0)
                {
                    minOf3 += i;
                    needNumb--;
                    j--;

                    if (needNumb == 1)
                        minOf2 = minOf3;
                }
            }

            //target <= sum of min3
            if (shipTarget <= minOf3)
                return minOf3 - allShip;

            int minDiff;
            int goal = 0;

            if (Math.Abs(maxOf3 - shipTarget) <= Math.Abs(minOf3 - shipTarget))
            {
                minDiff = maxOf3 - shipTarget;
            }
            else
            {
                minDiff = minOf3 - shipTarget;
            }

            //search from right or left
            if (shipTarget >= allShip)
            {
                for (int i = endIndex; i >= startIndex; i--)
                {
                    if (arr[i] == 0)
                        continue;

                    int end2 = arr[i] >= 2 ? i : i - 1;

                    for (int j = end2; j >= startIndex; j--)
                    {
                        if (arr[j] == 0)
                            continue;

                        //serach form minDiff=0
                        //i + j + goal = minDiff + shipTarget;
                        goal = shipTarget - i - j;
                        if ((goal < startIndex && startIndex - goal >= Math.Abs(minDiff))
                            || (goal > j && goal - j >= Math.Abs(minDiff)))
                        {
                            continue;
                        }

                        int len = 0;
                        int k = 0;
                        if (goal <= startIndex)
                        {
                            //search from start
                            len = startIndex - goal;
                            while (len < Math.Abs(minDiff))
                            {
                                k = goal + len;
                                if (k >= startIndex && k <= j && arr[k] != 0)
                                {
                                    arr[i]--;
                                    arr[j]--;
                                    arr[k]--;

                                    //check if valid 3 nums
                                    if (arr[i] >= 0 && arr[j] >= 0 && arr[k] >= 0)
                                    {
                                        if (len == 0)
                                            return target;

                                        minDiff = len;

                                        //remember to restore
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;

                                        break;
                                    }
                                    else
                                    {
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;
                                    }
                                }

                                len++;
                            }
                        }
                        else if (goal >= j)
                        {
                            //search from end
                            len = goal - j;

                            while (len < Math.Abs(minDiff))
                            {
                                k = goal - len;
                                if (k >= startIndex && k <= j && arr[k] != 0)
                                {
                                    arr[i]--;
                                    arr[j]--;
                                    arr[k]--;
                                    if (arr[i] >= 0 && arr[j] >= 0 && arr[k] >= 0)
                                    {
                                        if (len == 0)
                                            return target;

                                        minDiff = -len;

                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;
                                        break;
                                    }
                                    else
                                    {
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;
                                    }
                                }
                                len++;
                            }
                        }
                        else
                        {
                            //search from goal to both sides
                            while ((((goal + len) <= j && (goal + len) >= startIndex) || ((goal - len) <= j && (goal - len) >= startIndex))
                                    && len < Math.Abs(minDiff))
                            {
                                k = goal + len;

                                if (k >= startIndex && k <= j && arr[k] != 0)
                                {
                                    arr[i]--;
                                    arr[j]--;
                                    arr[k]--;

                                    //check if valid 3 nums
                                    if (arr[i] >= 0 && arr[j] >= 0 && arr[k] >= 0)
                                    {
                                        if (len == 0)
                                            return target;

                                        minDiff = len;

                                        //remember to restore
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;

                                        break;
                                    }
                                    else
                                    {
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;
                                    }
                                }

                                k = goal - len;
                                if (k >= startIndex && k <= j && arr[k] != 0)
                                {
                                    arr[i]--;
                                    arr[j]--;
                                    arr[k]--;
                                    if (arr[i] >= 0 && arr[j] >= 0 && arr[k] >= 0)
                                    {
                                        if (len == 0)
                                            return target;

                                        minDiff = -len;

                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;

                                        break;
                                    }
                                    else
                                    {
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;
                                    }
                                }

                                len++;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (arr[i] == 0)
                        continue;

                    int start2 = arr[i] >= 2 ? i : i + 1;

                    for (int j = start2; j <= endIndex; j++)
                    {
                        if (arr[j] == 0)
                            continue;

                        goal = shipTarget - i - j;

                        if ((goal < j && j - goal >= Math.Abs(minDiff))
                            || (goal > endIndex && goal - endIndex >= Math.Abs(minDiff)))
                        {
                            continue;
                        }

                        int len = 0;
                        int k = 0;

                        if (goal <= j)
                        {
                            //search from start
                            len = j - goal;
                            while (len < Math.Abs(minDiff))
                            {
                                k = goal + len;
                                if (k >= j && k <= endIndex && arr[k] != 0)
                                {
                                    arr[i]--;
                                    arr[j]--;
                                    arr[k]--;
                                    if (arr[i] >= 0 && arr[j] >= 0 && arr[k] >= 0)
                                    {
                                        if (len == 0)
                                            return target;

                                        minDiff = len;

                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;

                                        break;
                                    }
                                    else
                                    {
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;
                                    }
                                }

                                len++;
                            }
                        }
                        else if (goal >= endIndex)
                        {
                            //search from end
                            len = goal - endIndex;
                            while (len < Math.Abs(minDiff))
                            {
                                k = goal - len;
                                if (k >= j && k <= endIndex && arr[k] != 0)
                                {
                                    arr[i]--;
                                    arr[j]--;
                                    arr[k]--;
                                    if (arr[i] >= 0 && arr[j] >= 0 && arr[k] >= 0)
                                    {
                                        if (len == 0)
                                            return target;

                                        minDiff = -len;

                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;

                                        break;
                                    }
                                    else
                                    {
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;
                                    }
                                }

                                len++;
                            }
                        }
                        else
                        {
                            len = 0;
                            while ((((goal - len) >= j && (goal - len) <= endIndex) || ((goal + len) >= j && (goal + len) <= endIndex))
                                    && len < Math.Abs(minDiff))
                            {
                                k = goal + len;

                                if (k >= j && k <= endIndex && arr[k] != 0)
                                {
                                    arr[i]--;
                                    arr[j]--;
                                    arr[k]--;

                                    //check if valid 3 nums
                                    if (arr[i] >= 0 && arr[j] >= 0 && arr[k] >= 0)
                                    {
                                        if (len == 0)
                                            return target;

                                        minDiff = len;

                                        //remember to restore
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;

                                        break;
                                    }
                                    else
                                    {
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;
                                    }
                                }

                                k = goal - len;
                                if (k >= j && k <= endIndex && arr[k] != 0)
                                {
                                    arr[i]--;
                                    arr[j]--;
                                    arr[k]--;
                                    if (arr[i] >= 0 && arr[j] >= 0 && arr[k] >= 0)
                                    {
                                        if (len == 0)
                                            return target;

                                        minDiff = -len;

                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;

                                        break;
                                    }
                                    else
                                    {
                                        arr[i]++;
                                        arr[j]++;
                                        arr[k]++;
                                    }
                                }

                                len++;
                            }
                        }
                    }
                }
            }

            return minDiff + shipTarget - allShip;
        }

        ///17. Letter Combinations of a Phone Number
        ///digits[i] is a digit in the range['2', '9'].
        /// 2- ABC, 3-DEF, 4-GHI, 5-JKL,6-MNO,7-PQRS,8-TUV 9-WXYZ
        public IList<string> LetterCombinations(string digits)
        {
            Dictionary<char,List<char>> dict=new Dictionary<char, List<char>>();
            dict.Add('2', new List<char>() { 'a', 'b', 'c' });
            dict.Add('3', new List<char>() { 'd', 'e', 'f' });
            dict.Add('4', new List<char>() { 'g', 'h', 'i' });
            dict.Add('5', new List<char>() { 'j', 'k', 'l' });
            dict.Add('6', new List<char>() { 'm', 'n', 'o' });
            dict.Add('7', new List<char>() { 'p', 'q', 'r', 's' });
            dict.Add('8', new List<char>() { 't', 'u', 'v' });
            dict.Add('9', new List<char>() { 'w','x', 'y', 'z' });

            var results=new List<List<char>>();
            foreach(var d in digits)
            {
                if (results.Count == 0)
                {
                    List<List<char>> list=new List<List<char>>();

                    foreach(char c in dict[d])
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
                        foreach(char c in dict[d])
                        {
                            var sub = new List<char>(l);
                            sub.Add(c);
                            list.Add(sub);
                        }
                    }

                    results = list;
                }
            }

            return results.Select(x=>string.Join("",x)).ToList();
        }

        /// 18. 4Sum
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

        ///46. Permutations
        ///1 <= nums.length <= 6
        ///-10 <= nums[i] <= 10
        ///All the integers of nums are unique.
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

        ///56. Merge Intervals
        ///Given an array of intervals where intervals[i] = [starti, endi], merge all overlapping intervals,
        ///and return an array of the non-overlapping intervals that cover all the intervals in the input.
        ///Input: intervals =  [[1,3],[2,6],[8,10],[15,18]]
        ///Output: [[1,6],[8,10],[15,18]]
        public int[][] Merge(int[][] intervals)
        {
            if(intervals.Length==1)
                return intervals;

            List<int[]> list = new List<int[]>();

            //must convert to list, or exceed time limit
            var mat = intervals.OrderBy(x => x[0]).ToList();

            int[] last = null;
            for(int i = 0; i < mat.Count; i++)
            {
                var current = mat[i];
                if (last == null)
                {
                    last = current;
                }
                else
                {
                    if(last[1]< current[0])
                    {
                        list.Add(last);
                        last = current;
                    }
                    else
                    {
                        last[0] = Math.Min(last[0], current[0]);
                        last[1] = Math.Max(last[1], current[1]);
                    }
                }
            }

            if(last!=null)
                list.Add(last);

            return list.ToArray();
        }


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

        ///64. Minimum Path Sum
        ///Given a m x n grid filled with non-negative numbers,
        ///find a path from top left to bottom right,
        ///which minimizes the sum of all numbers along its path.
        ///Note: You can only move either down or right at any point in time.
        public int MinPathSum(int[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;
            var memo = new int[m + 1, n + 1];
            for (int i = 2; i <= m; i++)
                memo[i, 0] = int.MaxValue;
            for (int i = 0; i <= n; i++)
                memo[0, i] = int.MaxValue;
            memo[1, 0] = 0;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    memo[i, j] = Math.Min(memo[i - 1, j], memo[i, j - 1]) + grid[i - 1][j - 1];
                }
            }
            return memo[m, n];
        }

        public int MinPathSum_MyDp_Ugly(int[][] grid)
        {
            int i = 0;
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            List<int> dp=new List<int>();

            //r+c-1 times, everytime calculate all possible path dp values
            int count = rowLen - 1 + colLen;
            while (i< count)
            {
                if (i == 0)
                {
                    dp.Add(grid[0][0]);
                }
                else
                {
                    List<int> dp2=new List<int>();

                    int dpLen=0;
                    int startCol = 0;
                    int startRow = 0;

                    if (rowLen == colLen)
                    {
                        if (i < rowLen)
                        {
                            dpLen = i + 1;
                            startCol = i;
                            startRow = 0;
                        }
                        else
                        {
                            dpLen = rowLen - (i - (rowLen - 1));
                            startCol = (colLen - 1);
                            startRow = i - (rowLen - 1);
                        }

                    }
                    else if (rowLen > colLen)
                    {
                        if (i < colLen)
                        {
                            dpLen = i + 1;
                            startCol = i;
                            startRow = 0;
                        }
                        else if (i < rowLen)
                        {
                            dpLen = colLen;
                            startCol = colLen - 1;
                            startRow = i - (colLen - 1);
                        }
                        else
                        {
                            dpLen = colLen - (i - (rowLen - 1));
                            startCol = colLen - 1;
                            startRow = i - (colLen - 1);
                        }
                    }
                    else//rowLen<colLen
                    {
                        if (i < rowLen)
                        {
                            dpLen = i + 1;
                            startCol = i;
                            startRow = 0;
                        }
                        else if (i < colLen)
                        {
                            dpLen = rowLen;
                            startCol = i;
                            startRow = 0;
                        }
                        else
                        {
                            dpLen = rowLen - (i - (colLen - 1));
                            startCol = colLen - 1;
                            startRow = i - (colLen - 1);
                        }
                    }

                    int j = 0;
                    while(j < dpLen)
                    {
                        int y = startCol - j;
                        int x = startRow + j;

                        int a = 0;

                        if (rowLen == colLen)
                        {
                            if (x == 0)
                            {
                                //first one
                                a = grid[x][y] + dp[j];
                            }
                            else if (y == 0)
                            {
                                //last one
                                a = grid[x][y] + dp[j - 1];
                            }
                            else
                            {
                                if (i < rowLen)
                                {
                                    a = Math.Min(grid[x][y] + dp[j - 1], grid[x][y] + dp[j]);
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j + 1], grid[x][y] + dp[j]);
                                }
                            }
                        }
                        else if (rowLen > colLen)
                        {
                            if (dpLen > dp.Count)
                            {
                                if (x == 0)
                                {
                                    //first one, from up
                                    a = grid[x][y] + dp[j];
                                }
                                else if (y == 0)
                                {
                                    //last one, from left
                                    a = grid[x][y] + dp[j - 1];
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j - 1], grid[x][y] + dp[j]);
                                }
                            }
                            else if (dpLen == dp.Count)
                            {
                                if (y == 0)
                                {
                                    //last one, from left
                                    a = grid[x][y] + dp[j];
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j + 1], grid[x][y] + dp[j]);
                                }
                            }
                            else
                            {
                                a = Math.Min(grid[x][y] + dp[j + 1], grid[x][y] + dp[j]);
                            }
                        }
                        else
                        {
                            if (dpLen > dp.Count)
                            {
                                if (x == 0)
                                {
                                    a = grid[x][y] + dp[j];
                                }
                                else if (y == 0)
                                {
                                    a = grid[x][y] + dp[j - 1];
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j - 1], grid[x][y] + dp[j]);
                                }
                            }
                            else if (dpLen == dp.Count)
                            {
                                if (x == 0)
                                {
                                    a = grid[x][y] + dp[j];
                                }
                                else
                                {
                                    a = Math.Min(grid[x][y] + dp[j - 1], grid[x][y] + dp[j]);
                                }
                            }
                            else
                            {
                                a = Math.Min(grid[x][y] + dp[j + 1], grid[x][y] + dp[j]);
                            }
                        }

                        dp2.Add(a);
                        j++;
                    }
                    dp = dp2;
                }

                i++;
            }

            return dp[0];
        }

        ///Recursion will time out
        public int MinPathSum_Recursion(int[][] grid, int r, int c)
        {
            if (r == 0 && c == 0)
                return grid[0][0];

            if (r <= 0)
                return grid[r][c] + MinPathSum_Recursion(grid,0, c - 1);

            if(c<=0)
                return grid[r][c] + MinPathSum_Recursion(grid, r-1, 0);

            return Math.Min(grid[r][c] + MinPathSum_Recursion(grid, r, c - 1), grid[r][c] + MinPathSum_Recursion(grid, r - 1, c));
        }

        /// 65
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
            if (digits == null || digits.Length == 0)
                return new int[] { 1 };

            int i = digits.Length - 1;
            while (i >= 0)
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
            if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b))
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
            while (i <= s2.Length - 1)
            {
                if (i >= s1.Length)
                {
                    if (s2[s2.Length - 1 - i] == '0')
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
                    else if (s1[s1.Length - 1 - i] == '1' && s2[s2.Length - 1 - i] == '1')
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

            return String.Join("", result);
        }

        //69
        public int MySqrt(int x)
        {
            long r = x;
            while (r * r > x)
                r = (r + x / r) / 2;
            return (int)r;
        }

        ///70. Climbing Stairs
        ///Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?
        public int ClimbStairs(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 2;

            int dp1 = 1;
            int dp2 = 2;
            int dp = 0;
            for (int i = 3; i <= n; i++)
            {
                dp = dp1 + dp2;
                dp1 = dp2;
                dp2 = dp;
            }

            return dp;
        }

        public int ClimbStairs_Recursion(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 2;

            return ClimbStairs_Recursion(n - 1) + ClimbStairs_Recursion(n - 2);
        }

        ///74. Search a 2D Matrix
        ///a value in an m x n matrix.
        ///Integers in each row are sorted from left to right.
        ///The first integer of each row is greater than the last integer of the previous row.

        public bool SearchMatrix(int[][] matrix, int target)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;

            if(matrix[0][0]>target||matrix[rowLen-1][colLen-1]<target)
                return false;

            int row1 = 0;
            int row2 = rowLen - 1;
            int row = (row2 - row1) / 2;

            while (row1 <= row2 && row>=row1 && row<=row2)
            {
                if (matrix[row][colLen - 1] == target)
                {
                    return true;
                }
                else if(matrix[row][colLen - 1] > target)
                {
                    row2 = row - 1;
                    row = (row2 - row1) / 2 + row1;
                }
                else
                {
                    row1 = row + 1;
                    row = (row2 - row1) / 2 + row1;
                }
            }

            //find row
            var arr = matrix[row];

            int col1 = 0;
            int col2 = colLen - 1;
            int col = (col2 - col1) / 2;

            while (col1 <= col2 && col<=col2 && col>=col1)
            {
                if (arr[col] == target)
                {
                    return true;
                }
                else if (arr[col] > target)
                {
                    col2 = col - 1;
                    col = (col2 - col1) / 2 + col1;
                }
                else
                {
                    col1 = col + 1;
                    col = (col2 - col1) / 2 + col1;
                }
            }

            return false;
        }

        ///75. Sort Colors
        ///We will use the integers 0, 1, and 2 to represent the color red, white, and blue, respectively.
        /// sort as 0->1->2
        public void SortColors(int[] nums)
        {
            int i = 0;
            int red = 0;
            int white = 0;
            int blue = 0;
            int temp;
            while (i < nums.Length - blue)
            {
                if(nums[i] == 0)
                {
                    if (i != red)
                    {
                        temp = nums[red];
                        nums[red] = nums[i];
                        nums[i] = temp;
                    }
                    red++;
                    i++;
                }
                else if (nums[i] == 1)
                {
                    i++;
                    white++;
                }
                else
                {
                    temp = nums[nums.Length - 1 - blue];
                    nums[nums.Length - 1 - blue]=nums[i];
                    nums[i] = temp;
                    blue++;
                }
            }
        }
        /// 77. Combinations
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

        ///82. Remove Duplicates from Sorted List II
        /// remove all duplicates, [1,1,1,2,2,3]=>[3], [1,2,2,3]=>[1,3]
        public ListNode DeleteDuplicates(ListNode head)
        {

            ListNode target = null;
            ListNode last = null;
            ListNode node = head;

            while (node != null && node.next!=null)
            {
                if (node.val==node.next.val)
                {
                    if (node == head)
                    {
                        target = node;
                        while (node != null && node.val == target.val)
                        {
                            node = node.next;
                        }

                        head = node;
                        //target = null;
                        last = null;
                    }
                    else
                    {
                        target = node;
                        while (node != null && node.val == target.val)
                        {
                            node = node.next;
                        }
                        last.next = node;
                    }
                }
                else
                {
                    last = node;
                    node = node.next;
                    //target = node;
                }
            }

            return head;
        }
        /// 83. Remove Duplicates from Sorted List

        public ListNode DeleteDuplicates_83(ListNode head)
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

        ///91. Decode Ways
        ///A message containing letters from A-Z can be encoded into numbers using the following mapping:
        ///'A' -> "1", Z->26
        ///"AAJF" with the grouping (1 1 10 6)
        ///"KJF" with the grouping(11 10 6)

        public int NumDecodings(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length == 0)
                return 0;
            if (s[0] == '0')
                return 0;
            if (s.Length == 1)
                return 1;

            var arr = s.ToArray();

            int len = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '1' || arr[i] == '2')
                {
                    len++;
                }
                else
                {
                }
            }

            if (len == arr.Length)
                return NumDecodings_Only1or2(len);

            return NumDecodings_Recursion(arr, 0);
        }

        public int NumDecodings_Recursion(char[] arr, int start)
        {
            if (arr == null || arr.Length == 0)
                return 0;

            if (start < 0 || start >= arr.Length)
                return 0;

            int i = start;

            if (arr[i] == '0')
                return 0;

            if (i == arr.Length - 1)
                return 1;

            if (i == arr.Length - 2)
            {
                if (arr[i] == '1')
                {
                    if (arr[i + 1] == '0')
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else if (arr[i] == '2')
                {
                    if (arr[i + 1] == '0')
                    {
                        return 1;
                    }
                    else if (arr[i + 1] >= '7' && arr[i + 1] <= '9')
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return arr[i + 1] == '0' ? 0 : 1;
                }
            }

            if (arr[i] == '1')
            {
                if (arr[i + 1] == '0')
                {
                    return NumDecodings_Recursion(arr, i + 2);
                }
                else
                {
                    return NumDecodings_Recursion(arr, i + 1) + NumDecodings_Recursion(arr, i + 2);
                }
            }
            else if (arr[i] == '2')
            {
                if (arr[i + 1] >= '7' && arr[i + 1] <= '9')
                {
                    return NumDecodings_Recursion(arr, i + 2);
                }
                else if (arr[i + 1] == '0')
                {
                    return NumDecodings_Recursion(arr, i + 2);
                }
                else
                {
                    return NumDecodings_Recursion(arr, i + 1) + NumDecodings_Recursion(arr, i + 2);
                }
            }
            else
            {
                return NumDecodings_Recursion(arr, i + 1);
            }
        }

        public int NumDecodings_Only1or2(int n)
        {
            //Resursion will timeout
            if (n == 1)
                return 1;
            if (n == 2)
                return 2;

            int dp1 = 1;
            int dp2 = 2;
            int dp = dp1 + dp2;
            for (int i = 3; i <= n; i++)
            {
                dp = dp1 + dp2;
                dp1 = dp2;
                dp2 = dp;
            }

            return dp;
        }

        /// 94. Binary Tree Inorder Traversal
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
        /// Given an integer n, return the number of structurally unique BST's (binary search trees)
        /// which has exactly n nodes of unique values from 1 to n.
        public int NumTrees(int n)
        {
            return 0;
        }

        ///98. Validate Binary Search Tree
        /// left.val<=Node.Val<=right.val

        public bool IsValidBST(TreeNode root)
        {
            return IsValidBST_Recursion(root);
        }

        public bool IsValidBST_Recursion(TreeNode root, TreeNode left = null, TreeNode right = null)
        {
            if (root == null)
                return true;

            if (left != null && root.val <= left.val)
                return false;

            if (right != null && root.val >= right.val)
                return false;

            return IsValidBST_Recursion(root.left, left, root) && IsValidBST_Recursion(root.right, root, right);
        }
    }
}