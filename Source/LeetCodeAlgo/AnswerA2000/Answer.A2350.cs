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
                var bits = getBitsCount(n);
                if (!dict.ContainsKey(bits))
                    dict.Add(bits, new HashSet<int>());
                dict[bits].Add(n);
            }
            long res = 0;
            foreach (var i in set)
            {
                int need = k - getBitsCount(i);
                foreach (int key in dict.Keys)
                    if (key >= need)
                        res += (long)dict[key].Count;
            }
            return res;
        }


        //2357. Make Array Zero by Subtracting Equal Amounts
        public int MinimumOperations_A(int[] nums)
        {
            var set = nums.ToHashSet();
            set.Remove(0);
            return set.Count;
        }

        ///2358. Maximum Number of Groups Entering a Competition, #Binary Search
        //group count start from 1 and auto-increase 1, sum of group must strictly increase
        //Return the maximum number of groups that can be formed.
        public int MaximumGroups(int[] grades)
        {
            int n = grades.Length;
            int left = 1;
            int right = n;
            while (left < right)
            {
                int mid = (left + right +1)/ 2;
                long count = (long)mid * (1 + mid) / 2;
                if (count <= n)
                    left = mid;
                else
                    right = mid - 1;
            }
            return left;
        }
    }
}
