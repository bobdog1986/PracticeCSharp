using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    /// 705. Design HashSet
    public class MyHashSet
    {
        private readonly byte[] arr;
        private const int LEN = 1000000;
        public MyHashSet()
        {
            arr=new byte[LEN/8+1];
        }

        public void Add(int key)
        {
            int index = key / 8;
            int offset = key % 8;
            arr[index] |= (byte)(1 << offset);
        }

        public void Remove(int key)
        {
            int index = key / 8;
            int offset = key % 8;
            arr[index] &= (byte)(~(1 << offset));
        }

        public bool Contains(int key)
        {
            int index = key / 8;
            int offset = key % 8;
            return (arr[index] & (1<<offset))!=0;
        }
    }
}
