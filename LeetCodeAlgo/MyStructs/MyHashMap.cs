using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.MyStructs
{
    /// 706. Design HashMap
    public class MyHashMap
    {
        private readonly int[] map;
        private const int LEN = 1000001;
        public MyHashMap()
        {
            map = new int[LEN];
            for (int i = 0; i < map.Length; i++)
                map[i] = -1;
        }

        public void Put(int key, int value)
        {
            if (key < 0 || key >= LEN)
                return;

            map[key] = value;
        }

        public int Get(int key)
        {
            if (key < 0 || key >= LEN)
                return -1;
            return map[key];
        }

        public void Remove(int key)
        {
            if (key < 0 || key >= LEN)
                return;
            map[key] = -1;
        }
    }
}
