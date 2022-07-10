using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2336. Smallest Number in Infinite Set
    public class SmallestInfiniteSet
    {
        private readonly HashSet<int> set = new HashSet<int>();
        private readonly PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

        public SmallestInfiniteSet()
        {
            for(int i = 1; i <= 1000; i++)
            {
                pq.Enqueue(i, i);
                set.Add(i);
            }
        }

        public int PopSmallest()
        {
            var top = pq.Dequeue();
            set.Remove(top);
            return top;
        }

        public void AddBack(int num)
        {
            if (!set.Contains(num))
            {
                set.Add(num);
                pq.Enqueue(num, num);
            }
        }
    }
}
