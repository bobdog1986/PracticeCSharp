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
        private readonly PriorityQueue<int, int> maxHeap;
        private readonly PriorityQueue<int, int> minHeap;
        private int rank;

        public KthLargest(int k, int[] nums)
        {
            rank = k;
            maxHeap = new PriorityQueue<int, int>();
            minHeap = new PriorityQueue<int, int>();
            foreach (var n in nums)
                EnqueueInternal(n);
        }

        private void EnqueueInternal(int val)
        {
            if (minHeap.Count < rank - 1)
            {
                minHeap.Enqueue(val, val);
            }
            else
            {
                maxHeap.Enqueue(val, -val);
                while(minHeap.Count>0 && minHeap.Peek()<maxHeap.Peek())
                {
                    var temp1=minHeap.Dequeue();
                    var temp2=maxHeap.Dequeue();
                    minHeap.Enqueue(temp2, temp2);
                    maxHeap.Enqueue(temp1, -temp1);
                }
            }
        }

        public int Add(int val)
        {
            EnqueueInternal(val);
            return maxHeap.Peek();
        }
    }
}
