using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        /// 258. Add Digits
        ///Given an integer num, repeatedly add all its digits until the result has only one digit, and return it.
        public int AddDigits(int num)
        {
            if (num < 10) return num;
            int total = 0;
            while (num >= 10)
            {
                total += num % 10;
                num /= 10;
            }
            total += num;
            return AddDigits(total);
        }

        ///264. Ugly Number II - NOT mine
        /// An ugly number is a positive integer whose prime factors are limited to 2, 3, and 5.
        /// Given an integer n, return the nth ugly number.
        /// (1) 1×2, 2×2, 3×2, 4×2, 5×2, …
        /// (2) 1×3, 2×3, 3×3, 4×3, 5×3, …
        /// (3) 1×5, 2×5, 3×5, 4×5, 5×5, …
        public int NthUglyNumber(int n)
        {
            int[] ugly = new int[n];
            ugly[0] = 1;
            int index2 = 0, index3 = 0, index5 = 0;
            int factor2 = 2, factor3 = 3, factor5 = 5;
            for (int i = 1; i < n; i++)
            {
                int min = Math.Min(Math.Min(factor2, factor3), factor5);
                ugly[i] = min;
                if (factor2 == min)
                    factor2 = 2 * ugly[++index2];
                if (factor3 == min)
                    factor3 = 3 * ugly[++index3];
                if (factor5 == min)
                    factor5 = 5 * ugly[++index5];
            }
            return ugly[n - 1];
        }
        /// 278. First Bad Version
        public int FirstBadVersion(int n)
        {
            return FirstBadVersion(1, n);
        }

        public int FirstBadVersion(int start, int end)
        {
            if (start == end) return start;

            int num = end - start + 1;
            int mid = num / 2 + start - 1;

            if (IsBadVersion(mid))
            {
                return FirstBadVersion(start, mid);
            }
            else
            {
                return FirstBadVersion(mid + 1, end);
            }
        }

        public bool IsBadVersion(int n)
        {
            int bad = 10;
            return (n >= bad);
        }

        ///279. Perfect Squares, #DP
        ///Given an integer n, return the least number of perfect square numbers that sum to n.
        ///A perfect square is an integer that is the square of an integer;
        public int NumSquares(int n)
        {
            int[] dp = new int[n + 1];
            for (int i = 1; i <= n; i++)
                dp[i] = int.MaxValue;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j * j <= i; j++)
                {
                    dp[i] = Math.Min(dp[i], dp[i - j * j] + 1);
                }
            }
            return dp[n];
        }

        /// 283. Move Zeroes
        /// move all 0's to the end of it while maintaining the relative order of the non-zero elements.
        public void MoveZeroes(int[] nums)
        {
            int i = 0;
            int[] temp=new int[nums.Length];
            foreach(var n in nums)
            {
                if(n != 0)
                    temp[i++] = n;
            }
            for (int j=0;j < nums.Length; j++)
            {
                nums[j] = j < i ? temp[j] : 0;
            }
        }

        ///290. Word Pattern
        ///Given a pattern and a string s, find if s follows the same pattern.
        ///pattern = "abba", s = "dog cat cat dog", return true
        ///pattern = "abba", s = "dog dog dog dog", return false
        public bool WordPattern(string pattern, string s)
        {
            var carr = pattern.ToCharArray();
            var words = s.Split(' ');

            if (carr.Length != words.Length)
                return false;

            Dictionary<char, string> dict = new Dictionary<char, string>();

            for (int i = 0; i < carr.Length; i++)
            {
                if (dict.ContainsKey(carr[i]))
                {
                    if (dict[carr[i]] != words[i])
                    {
                        return false;
                    }
                }
                else if (dict.ContainsValue(words[i]))
                {
                    return false;
                }
                else
                {
                    dict.Add(carr[i], words[i]);
                }
            }

            return true;
        }

        ///297. Serialize and Deserialize Binary Tree, see Codec

    }
}
