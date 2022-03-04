using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {

        ///1171. Remove Zero Sum Consecutive Nodes from Linked List
        public ListNode RemoveZeroSumSublists(ListNode head)
        {
            var node = head;

            Dictionary<int, ListNode> dict = new Dictionary<int, ListNode>();
            int sum = 0;
            dict.Add(0, null);
            while (node != null)
            {
                sum += node.val;
                if (dict.ContainsKey(sum))
                {
                    var last = dict[sum];
                    if (last == null)
                    {
                        while (head != node.next)
                        {
                            if (dict.ContainsValue(head))
                            {
                                dict.Remove(dict.FirstOrDefault(x => x.Value == head).Key);
                            }
                            head = head.next;
                        }
                    }
                    else
                    {
                        var curr = last.next;
                        while (curr != node.next)
                        {
                            if (dict.ContainsValue(curr))
                            {
                                dict.Remove(dict.FirstOrDefault(x => x.Value == curr).Key);
                            }
                            curr = curr.next;
                        }
                        last.next = node.next;
                    }
                }
                else
                {
                    dict.Add(sum, node);
                }

                node = node.next;
            }

            return head;
        }
    }
}
