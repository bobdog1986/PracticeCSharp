using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1961. Check If String Is a Prefix of Array
        ///Given a string s and an array of strings words, determine whether s is a prefix string of words.
        ///A string s is a prefix string of words if s can be made by concatenating the first k strings in words
        public bool IsPrefixString(string s, string[] words)
        {
            foreach(var w in words)
            {
                if (s.StartsWith(w)) s = s.Substring(w.Length);
                else return false;
                if (s.Length == 0) return true;
            }
            return false;
        }

        ///1979. Find Greatest Common Divisor of Array
        ///return the greatest common divisor of the smallest number and largest number in nums.
        ///2 <= nums.length <= 1000,1 <= nums[i] <= 1000
        public int FindGCD(int[] nums)
        {
            int max = 1;
            int min = 1000;
            foreach(var n in nums)
            {
                max=Math.Max(max, n);
                min=Math.Min(min, n);
            }
            return getGcb(max, min);
        }
    }
}
