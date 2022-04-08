using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///703. Kth Largest Element in a Stream, #PriorityQueue, #Heap
    ///Design a class to find the kth largest element in a stream.
    ///Note that it is the kth largest element in the sorted order, not the kth distinct element.
    public class KthLargest
    {
        private readonly PriorityQueue<int, int> minHeap;
        private int rank;

        public KthLargest(int k, int[] nums)
        {
            rank = k;
            minHeap = new PriorityQueue<int, int>();
            foreach (var n in nums)
                EnqueueInternal(n);
        }

        private void EnqueueInternal(int val)
        {
            minHeap.Enqueue(val, val);
            if(minHeap.Count>rank)
                minHeap.Dequeue();
        }

        public int Add(int val)
        {
            EnqueueInternal(val);
            return minHeap.Peek();
        }
    }
}
