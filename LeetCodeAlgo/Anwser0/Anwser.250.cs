using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //258
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

        ///
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

        //283. Move Zeroes
        public void MoveZeroes(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return;

            int zeroCount = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (i + zeroCount >= nums.Length - 1)
                    break;

                while (nums[i] == 0)
                {
                    for (int j = i; j < nums.Length - 1 - zeroCount; j++)
                    {
                        nums[j] = nums[j + 1];
                    }

                    nums[nums.Length - 1 - zeroCount] = 0;

                    zeroCount++;

                    if (i + zeroCount >= nums.Length - 1)
                        break;
                }
            }

            Console.WriteLine($"zer0Count = {zeroCount}");
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

        ///
    }
}
