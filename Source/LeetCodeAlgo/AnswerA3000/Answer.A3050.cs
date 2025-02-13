using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///3066. Minimum Operations to Exceed Threshold Value II, #PriorityQueue
        //public int MinOperations_3066(int[] nums, int k)
        //{
        //    int res = 0;
        //    var pq = new PriorityQueue<long, long>();
        //    foreach (var i in nums)
        //    {
        //        pq.Enqueue(i, i);
        //    }
        //    while (pq.Count>=2 &&pq.Peek()<k)
        //    {
        //        res++;
        //        long a = pq.Dequeue();
        //        long b = pq.Dequeue();
        //        pq.Enqueue(a*2+b, a*2+b);
        //    }
        //    return res;
        //}
    }
}