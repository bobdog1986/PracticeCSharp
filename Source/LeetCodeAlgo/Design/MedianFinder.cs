using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///295. Find Median from Data Stream, #PriorityQueue
    ///The median is the middle value in an ordered integer list.
    ///If the size of the list is even, there is no middle value and the median is the mean of the two middle values.
    public class MedianFinder
    {
        //minHalf alway hold small half elements, is a maxHeap
        private PriorityQueue<int, int> minHalf = new PriorityQueue<int, int>();
        //maxHalf alway hold big half elements, is a minHeap
        private PriorityQueue<int, int> maxHalf = new PriorityQueue<int, int>();

        public void AddNum(int num)
        {
            maxHalf.Enqueue(num,num);
            var head = maxHalf.Dequeue();
            minHalf.Enqueue(head,-head);
            if (maxHalf.Count < minHalf.Count)
            {
                var next = minHalf.Dequeue();
                maxHalf.Enqueue(next, next);
            }
        }

        public double FindMedian()
        {
            return maxHalf.Count > minHalf.Count
                   ? maxHalf.Peek()
                   : (maxHalf.Peek()*0.5 + minHalf.Peek() * 0.5);
        }
    }
}
