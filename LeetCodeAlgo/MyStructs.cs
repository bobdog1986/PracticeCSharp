using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public class MyQueue
    {
        public MyQueue()
        {
        }

        private Stack<int> q1 = new Stack<int>();
        private Stack<int> q2 = new Stack<int>();

        public void Push(int x)
        {
            var a = (q1.Count != 0 || q2.Count == 0) ? q1 : q2;
            a.Push(x);
        }

        public int Pop()
        {
            var a = (q1.Count != 0 || q2.Count == 0) ? q1 : q2;
            var b = (q1.Count != 0 || q2.Count == 0) ? q2 : q1;

            while (a.Count > 1)
            {
                b.Push(a.Pop());
            }

            var result = a.Pop();

            while (b.Count > 0)
            {
                a.Push(b.Pop());
            }

            return result;
        }

        public int Peek()
        {
            var a = (q1.Count != 0 || q2.Count == 0) ? q1 : q2;
            var b = (q1.Count != 0 || q2.Count == 0) ? q2 : q1;

            while (a.Count > 0)
            {
                b.Push(a.Pop());
            }

            var result = b.Peek();

            while (b.Count > 0)
            {
                a.Push(b.Pop());
            }
            return result;
        }

        public bool Empty()
        {
            return q1.Count == 0 && q2.Count == 0;
        }
    }

    /// <summary>
    /// 706. Design HashMap
    /// </summary>
    public class MyHashMap
    {
        private int[] map;
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