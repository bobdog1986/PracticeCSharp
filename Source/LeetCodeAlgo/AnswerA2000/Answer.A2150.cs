using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2185. Counting Words With a Given Prefix
        public int PrefixCount(string[] words, string pref)
        {
            return words.Where(word => word.StartsWith(pref)).Count();
        }

        ///2190. Most Frequent Number Following Key In an Array
        ///0 <= i <= n - 2, nums[i] == key and, nums[i + 1] == target.
        ///Return the target with the maximum count.
        public int MostFrequent(int[] nums, int key)
        {
            int max = 0;
            int res = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            for(int i = 0; i < nums.Length-1; i++)
            {
                if(nums[i] == key)
                {
                    if(map.ContainsKey(nums[i+1]))map[nums[i+1]]++;
                    else map.Add(nums[i+1], 1);
                    if (map[nums[i + 1]] > max)
                    {
                        max = map[nums[i+1]];
                        res = nums[i + 1];
                    }
                }
            }
            return res;
        }
    }
}
