using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    /// 398. Random Pick Index

    public class Solution_398_RandomPickIndex
    {
        private readonly Dictionary<int, List<int>> dict;
        private readonly Random rand;
        public Solution_398_RandomPickIndex(int[] nums)
        {
            rand = new Random();
            dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i]))
                    dict.Add(nums[i], new List<int>());
                dict[nums[i]].Add(i);
            }
        }

        public int Pick(int target)
        {
            int count = dict[target].Count;
            var next = rand.Next(count);
            return dict[target][next];
        }
    }
}
