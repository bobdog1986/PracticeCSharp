using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2487. Remove Nodes From Linked List
        //Remove every node which has a node with a strictly greater value anywhere to the right side of it.
        public ListNode RemoveNodes_2487(ListNode head)
        {
            if (head==null||head.next==null)
                return head;
            ListNode[] arr = new ListNode[100000];
            int i = -1;
            while (head!=null)
            {
                if (i>=0)
                {
                    if (head.val>arr[i].val)
                    {
                        arr[i]=null;
                        i--;
                    }
                    else
                    {
                        arr[++i] = head;
                        head= head.next;
                    }
                }
                else
                {
                    arr[++i] = head;
                    head = head.next;
                }
            }

            var res = arr[0];
            for(int j = 0; j<=i; j++)
            {
                arr[j].next = arr[j+1];
            }
            arr[i].next=null;
            return res;
        }

        public ListNode RemoveNodes_2487_Recurr(ListNode head)
        {
            if (head==null) return null;
            var next = RemoveNodes_2487_Recurr(head.next);
            if (next == null) return head;
            else
            {
                if (next.val>head.val) return next;
                else
                {
                    head.next = next;
                    return head;
                }
            }
        }

    }
}
