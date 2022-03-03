using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    public class RandomizedSet
    {
        private readonly Dictionary<int, int> dict = new Dictionary<int, int>();
        private Random random=new Random();
        public RandomizedSet()
        {

        }

        public bool Insert(int val)
        {
            if (dict.ContainsKey(val))
                return false;
            dict.Add(val, val);
            return true;
        }

        public bool Remove(int val)
        {
            return dict.Remove(val);
        }

        public int GetRandom()
        {
            var keys=dict.Keys.ToList();

            var index = random.Next(0, keys.Count);

            return keys[index];
        }
    }
}
