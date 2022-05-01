using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.AnswerA2000
{
    public partial class Answer
    {
        ///2259. Remove Digit From Number to Maximize Result
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

    }
}
