using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///933. Number of Recent Calls, #Queue, #Binary Search
    //After insert t by Ping(t), return the count in range [t - 3000, t].
    public class RecentCounter
    {
        private readonly Queue<int> q = new Queue<int>();
        public int Ping(int t)
        {
            q.Enqueue(t);
            while (q.Count > 0 && q.Peek() < t - 3000)
                q.Dequeue();
            return q.Count;
        }
    }

    public class RecentCounter_BinarySearch
    {
        private readonly List<int> list= new List<int>();

        public int Ping(int t)
        {
            list.Add(t);
            if (t - 3000 <= list[0]) return list.Count;
            else
            {
                int left = 0;
                int right = list.Count - 1;
                while (left < right)
                {
                    int mid = (left + right) / 2;
                    if (list[mid] < t - 3000)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid;
                    }
                }
                return list.Count - 1 - left + 1;
            }
        }
    }

}
