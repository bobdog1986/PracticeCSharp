using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy
{
    public partial class Easy
    {
        ///1005. Maximize Sum Of Array After K Negations, #PriorityQueue
        public int LargestSumAfterKNegations(int[] nums, int k)
        {
            var pq = new PriorityQueue<int, int>();
            foreach (var n in nums)
                pq.Enqueue(n, n);
            while (k-- > 0)
            {
                var top = pq.Dequeue();
                pq.Enqueue(-top, -top);
            }
            int sum = 0;
            while (pq.Count > 0)
                sum += pq.Dequeue();
            return sum;
        }
    }
}
