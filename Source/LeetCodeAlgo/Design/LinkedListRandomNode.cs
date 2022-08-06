using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design.LinkedListRandomNode
{
    /// 382. Linked List Random Node
    ///Each node must have the same probability of being chosen. #Reservoir Sampling
    public class Solution
    {
        private readonly List<int> list;
        private readonly Random random;
        public Solution(ListNode head)
        {
            random = new Random();
            list = new List<int>();
            while (head != null)
            {
                list.Add(head.val);
                head = head.next;
            }
        }

        public int GetRandom()
        {
            int index = random.Next(list.Count);
            return list[index];
        }
    }
}
