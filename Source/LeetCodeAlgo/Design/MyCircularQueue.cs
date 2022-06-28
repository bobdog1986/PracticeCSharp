using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///622. Design Circular Queue
    ///The circular queue is a linear data structure in which the operations
    ///are performed based on FIFO(First In First Out) principle and
    ///the last position is connected back to the first position to make a circle.It is also called "Ring Buffer".
    public class MyCircularQueue
    {
        private readonly int[] arr;
        private int index;
        private int count;
        public MyCircularQueue(int k)
        {
            index = 0;
            count = 0;
            arr = new int[k];
        }

        public bool EnQueue(int value)
        {
            if (count == arr.Length) return false;
            arr[index++] = value;
            index %= arr.Length;
            count++;
            return true;
        }

        public bool DeQueue()
        {
            if (count == 0) return false;
            count--;
            return true;
        }

        public int Front()
        {
            return IsEmpty() ? -1 : arr[(index - count + arr.Length) % arr.Length];
        }

        public int Rear()
        {
            return IsEmpty() ? -1 : arr[(index - 1 + arr.Length) % arr.Length];
        }

        public bool IsEmpty()
        {
            return count == 0;
        }

        public bool IsFull()
        {
            return count == arr.Length;
        }
    }
}
