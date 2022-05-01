using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2255. Count Prefixes of a Given String
        public int CountPrefixes(string[] words, string s)
        {
            return words.Where(x => s.StartsWith(x)).Count();
        }


        ///2256. Minimum Average Difference
        ///The average difference of the index i is the absolute difference between the
        ///average of the first i + 1 elements of nums and the average of the last n - i - 1 elements.
        ///Both averages should be rounded down to the nearest integer.
        ///Return the index with the minimum average difference.If there are multiple such indices, return the smallest one.
        public int MinimumAverageDifference(int[] nums)
        {
            int res = 0;
            int min = int.MaxValue;

            long sum = 0;
            foreach (var x in nums)
                sum += x;

            int n=nums.Length;
            long curr = 0;

            for(int i = 0; i < nums.Length; i++)
            {
                curr += nums[i];
                sum-=nums[i];

                long left = curr / (i + 1);
                long right = i<nums.Length-1?  sum / (n - i - 1):0;
                var diff = (int)Math.Abs(left - right);
                if(diff < min)
                {
                    min= diff;
                    res = i;
                }
            }
            return res;
        }
        /// 2259. Remove Digit From Number to Maximize Result
        public string RemoveDigit(string number, char digit)
        {
            string res = string.Empty;
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == digit)
                {
                    var str = number.Substring(0, i) + number.Substring(i + 1);
                    if (string.IsNullOrEmpty(str) || string.Compare(res, str) < 0)
                        res = str;
                }
            }
            return res;
        }

        ///2260. Minimum Consecutive Cards to Pick Up
        ///Return the minimum number of consecutive cards have a pair of matching cards.
        ///If it is impossible to have matching cards, return -1.
        public int MinimumCardPickup(int[] cards)
        {
            int res = int.MaxValue;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < cards.Length; i++)
            {
                if (dict.ContainsKey(cards[i]))
                {
                    res = Math.Min(res, i - dict[cards[i]] + 1);
                    dict[cards[i]] = i;
                }
                else
                {
                    dict.Add(cards[i], i);
                }
            }
            return res == int.MaxValue ? -1 : res;
        }

        ///2261. K Divisible Elements Subarrays
        ///return the number of distinct subarrays which have at most k elements divisible by p.
        public int CountDistinct(int[] nums, int k, int p)
        {
            int count = 0;
            int left = 0;
            HashSet<string> set = new HashSet<string>();
            var list = nums.ToList();
            int i = 0;
            for (; i < list.Count; i++)
            {
                if (list[i] % p == 0)
                {
                    if (count == k)
                    {
                        for (int j = left; j <= i - 1; j++)
                        {
                            for (int x = j; x <= i - 1; x++)
                            {
                                var str = string.Join('_', list.GetRange(j, x - j + 1));
                                set.Add(str);
                            }
                        }
                        while (left <= i)
                        {
                            if (list[left++] % p == 0)
                                break;
                        }
                        count--;
                    }
                    count++;
                }
            }
            for (int j = left; j <= i - 1; j++)
            {
                for (int x = j; x <= i - 1; x++)
                {
                    var str = string.Join('_', list.GetRange(j, x - j + 1));
                    set.Add(str);
                }
            }
            return set.Count;
        }

        ///2262. Total Appeal of A String, #DP
        ///The appeal of a string is the number of distinct characters found in the string.
        ///Given a string s, return the total appeal of all of its substrings.
        public long AppealSum(string s)
        {
            long res = 0;
            long cur = 0;
            long[] prev = new long[26];
            for (int i = 0; i < s.Length; ++i)
            {
                cur += i + 1 - prev[s[i] - 'a'];
                prev[s[i] - 'a'] = i + 1;
                res += cur;
            }
            return res;
        }

    }

}
