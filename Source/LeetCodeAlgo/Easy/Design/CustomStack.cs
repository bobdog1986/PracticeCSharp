using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///1381. Design a Stack With Increment Operation
    ///void inc(int k, int val) Increments the bottom k elements of the stack by val.
    public class CustomStack
    {
        private readonly int[] arr;
        private int last;
        public CustomStack(int maxSize)
        {
            last = -1;
            arr = new int[maxSize];
        }

        public void Push(int x)
        {
            if (last < arr.Length - 1)
                arr[++last] = x;
        }

        public int Pop()
        {
            if (last >= 0) return arr[last--];
            else return -1;
        }

        public void Increment(int k, int val)
        {
            for (int i = 0; i <= last && i + 1 <= k; i++)
                arr[i] += val;
        }
    }
}
