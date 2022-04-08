using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1952. Three Divisors
        ///n has exactly 3 divisors, 1 <= n <= 10^4
        public bool IsThree(int n)
        {
            //get primes <=100, https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes
            int[] arr = new int[101];
            for(int i = 2; i*i <= 100; i++)
            {
                int j = i+i;
                while (j <= 100)
                {
                    arr[j] = 1;
                    j += i;
                }
            }
            if (n > 2)
            {
                var sqrt = (int)Math.Sqrt(n);
                if(arr[sqrt]==0 && sqrt* sqrt==n)return true;
                else return false;
            }
            else return false;
        }
        ///1957. Delete Characters to Make Fancy String
        ///A fancy string is a string where no three consecutive characters are equal.
        public string MakeFancyString(string s)
        {
            List<char> list = new List<char>();
            char c = s[0];
            int count = 1;
            list.Add(c);
            for(int i=1; i<s.Length; i++)
            {
                if(s[i] == c)
                {
                    count++;
                }
                else
                {
                    c = s[i];
                    count = 1;
                }
                if (count <= 2) list.Add(c);
            }
            return new string(list.ToArray());
        }


        /// 1961. Check If String Is a Prefix of Array
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

        ///1963. Minimum Number of Swaps to Make the String Balanced
        ///Return the minimum number of swaps to make s balanced.
        public int MinSwaps(string s)
        {
            //from middle of string, ]][[ need 1 swap
            int stack_size = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '[')
                    stack_size++;
                else
                {
                    if (stack_size > 0)
                        stack_size--;
                }
            }
            return (stack_size + 1) / 2;//both ]][[, ][ need 1 swap
        }
        /// 1974. Minimum Time to Type Word Using Special Typewriter
        ///Given a string word, return the minimum number of seconds to type out the characters in word.
        public int MinTimeToType(string word)
        {
            int res = 0;
            char c = 'a';
            for(int i=0; i<word.Length; i++)
            {
                res+= Math.Min(Math.Abs(word[i] -c), 26 - Math.Abs(word[i] - c));
                res++;
                c=word[i];
            }
            return res;
        }
        /// 1979. Find Greatest Common Divisor of Array
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

        ///1980. Find Unique Binary String
        ///Given an array of strings nums containing n unique binary strings each of length n,
        ///return a binary string of length n that does not appear in nums. If there are multiple
        ///answers, you may return any of them.
        public string FindDifferentBinaryString(string[] nums)
        {
            HashSet<string> set =new HashSet<string>();

            FindDifferentBinaryString(nums[0].Length, "", set);
            HashSet<string> map = new HashSet<string>(nums);


            foreach (var n in set)
            {
                if (!map.Contains(n)) return n;
            }

            return "";
        }
        public void FindDifferentBinaryString(int count, string curr, HashSet<string> res)
        {
            if (count == 0) res.Add(curr);
            else
            {
                FindDifferentBinaryString(count - 1, curr + "0", res);
                FindDifferentBinaryString(count - 1, curr + "1", res);
            }
        }

        /// 1984. Minimum Difference Between Highest and Lowest of K Scores
        ///You are given a 0-indexed integer array nums, where nums[i] represents the score of the ith student.
        ///You are also given an integer k. Pick the scores of any k students from the array
        ///so that the difference between the highest and the lowest of the k scores is minimized.Return it;
        public int MinimumDifference(int[] nums, int k)
        {
            if (k == 1) return 0;
            Array.Sort(nums);
            int min = int.MaxValue;
            for(int i=0; i<nums.Length-k+1; i++)
            {
                min = Math.Min(min, nums[i + k - 1] - nums[i]);
            }
            return min;
        }

    }
}
