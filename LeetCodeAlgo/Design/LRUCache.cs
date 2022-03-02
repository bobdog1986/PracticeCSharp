using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    /// 146. LRU Cache
    /// Design a data structure that follows the constraints of a Least Recently Used (LRU) cache.
    ///The functions get and put must each run in O(1) average time complexity.

    public class LRUCache
    {
        Dictionary<int, int> dict;
        private int capacity;
        List<int> records;
        public LRUCache(int capacity)
        {
            this.capacity=capacity;
            dict = new Dictionary<int, int>();
            records = new List<int>();
        }

        public int Get(int key)
        {
            records.Add(key);
            if (dict.ContainsKey(key)) return dict[key];
            else return -1;


        }

        public void Put(int key, int value)
        {
            records.Add(key);
            if(dict.ContainsKey(key)) dict[key] = value;
            else dict.Add(key, value);

            if(dict.Count> capacity)
            {
                int i = 0;
                int j=records.Count;
                while (i < j)
                {
                    //if()
                    i++;
                }
            }
        }
    }
}
