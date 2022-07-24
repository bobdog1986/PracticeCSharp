using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2351. First Letter to Appear Twice, in Easy

        ///2352. Equal Row and Column Pairs, in Easy

        ///2353. Design a Food Rating System, see FoodRatings

        ///2354. Number of Excellent Pairs, #Bit Manipulation
        //sum of the number of set bits in num1 OR num2 and num1 AND num2 >= k,
        //Return the number of distinct excellent pairs. 1 <= k <= 60
        public long CountExcellentPairs(int[] nums, int k)
        {
            //The important point to realize the sum of OR and AND is just the sum of bits of two numbers.
            var dict = new Dictionary<int, HashSet<int>>();
            var set = new HashSet<int>();
            foreach(var n in nums)
            {
                set.Add(n);
                var bits = getBitCount(n);
                if (!dict.ContainsKey(bits))
                    dict.Add(bits, new HashSet<int>());
                dict[bits].Add(n);
            }
            long res = 0;
            foreach (var i in set)
            {
                int need = k - getBitCount(i);
                foreach (int key in dict.Keys)
                    if (key >= need)
                        res += (long)dict[key].Count;
            }
            return res;
        }

        private int getBitCount(int n)
        {
            int res = 0;
            while (n > 0)
            {
                if ((n & 1) == 1) res++;
                n >>= 1;
            }
            return res;
        }
    }
}
