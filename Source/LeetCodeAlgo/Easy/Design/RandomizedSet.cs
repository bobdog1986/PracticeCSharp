using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///380. Insert Delete GetRandom O(1)

    public class RandomizedSet
    {
        private readonly HashSet<int> set;
        private readonly Random random;
        public RandomizedSet()
        {
            random = new Random();
            set = new HashSet<int>();
        }

        public bool Insert(int val)
        {
            return set.Add(val);
        }

        public bool Remove(int val)
        {
            return set.Remove(val);
        }

        public int GetRandom()
        {
            var index = random.Next(0, set.Count);
            return set.ElementAt(index);
        }
    }
}
