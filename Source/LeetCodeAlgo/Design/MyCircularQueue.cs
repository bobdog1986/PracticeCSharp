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
        private readonly int MAX_COUNT;
        private readonly List<int> list;
        public MyCircularQueue(int k)
        {
            MAX_COUNT = k;
            list = new List<int>();
        }

        public bool EnQueue(int value)
        {
            if (list.Count >= MAX_COUNT) return false;
            list.Add(value);
            return true;
        }

        public bool DeQueue()
        {
            if(list.Count == 0) return false;
            list.RemoveAt(0);
            return true;
        }

        public int Front()
        {
            return list.Count > 0 ? list.First() : -1;
        }

        public int Rear()
        {
            return list.Count > 0 ? list.Last() : -1;
        }

        public bool IsEmpty()
        {
            return list.Count == 0;
        }

        public bool IsFull()
        {
            return list.Count == MAX_COUNT;
        }
    }
}
