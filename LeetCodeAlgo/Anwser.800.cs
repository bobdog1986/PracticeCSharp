using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //876. Middle of the Linked List
        public ListNode MiddleNode(ListNode head)
        {
            if(head == null||head.next == null)
                return head;

            var next = head.next;

            int count = 1;
            //List<int> nodes = new List<int>();
            while(next != null)
            {
                count++;
                //nodes.Add(head.val);
                next = next.next;
            }

            int len = count / 2;

            while (len > 0)
            {
                head = head.next;
                len--;
            }

            return head;
        }
    }
}
