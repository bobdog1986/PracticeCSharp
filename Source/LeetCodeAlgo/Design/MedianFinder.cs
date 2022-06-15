using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///295. Find Median from Data Stream, #PriorityQueue, 
    ///The median is the middle value in an ordered integer list.
    ///If the size of the list is even, there is no middle value and the median is the mean of the two middle values.
    public class MedianFinder
    {
        //maxHeap alway hold small half elements
        private PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
        //maxHeap alway hold big half elements
        private PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>();
        public MedianFinder()
        {
        }

        public void AddNum(int num)
        {
            maxHeap.Enqueue(num,num);
            var head = maxHeap.Dequeue();
            minHeap.Enqueue(head,-head);
            if (maxHeap.Count < minHeap.Count)
            {
                var next = minHeap.Dequeue();
                maxHeap.Enqueue(next, next);
            }
        }

        public double FindMedian()
        {
            return maxHeap.Count > minHeap.Count
                   ? maxHeap.Peek()
                   : (maxHeap.Peek()*0.5 + minHeap.Peek() * 0.5);
        }
    }
}
