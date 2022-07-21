using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    /// 705. Design HashSet
    public class MyHashSet
    {
        private readonly bool[] arr;
        private const int LEN = 1000001;
        public MyHashSet()
        {
            arr = new bool[LEN];
        }

        public void Add(int key)
        {
            arr[key] = true;
        }

        public void Remove(int key)
        {
            arr[key] = false;
        }

        public bool Contains(int key)
        {
            return arr[key];
        }
    }
}
