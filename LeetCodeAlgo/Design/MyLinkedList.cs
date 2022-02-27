using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///707. Design Linked List
    public class MyLinkedList
    {

        private List<ListNode> nodes=new List<ListNode> ();
        public MyLinkedList()
        {

        }

        public int Get(int index)
        {
            if (index >= nodes.Count)
                return -1;

            return nodes[index].val;
        }

        public void AddAtHead(int val)
        {
            var head = nodes.Count > 0 ? nodes[0] : null;
            nodes.Insert(0, new ListNode(val, head));
        }

        public void AddAtTail(int val)
        {
            var tail = nodes.Count > 0 ? nodes[nodes.Count - 1]: null;
            var add = new ListNode(val);
            nodes.Add(add);
            if(tail != null)
                tail.next = add;
        }

        public void AddAtIndex(int index, int val)
        {
            if (index > nodes.Count)
            {
                //
            }
            else if (index == nodes.Count)
            {
                AddAtTail(val);
            }
            else
            {
                if (index == 0)
                {
                    AddAtHead(val);
                }
                else
                {
                    var prev=nodes[index-1];
                    var next=nodes[index];
                    var add=new ListNode(val);
                    nodes.Insert(index, add);
                    prev.next = add;
                    add.next = next;
                }
            }
        }

        public void DeleteAtIndex(int index)
        {
            if(index >= nodes.Count)
            {
                //
            }
            else
            {
                if (index == 0)
                {
                    nodes.RemoveAt(index);
                }
                else if(index == nodes.Count - 1)
                {
                    nodes.RemoveAt(index);
                    nodes.Last().next = null;
                }
                else
                {
                    var prev = nodes[index - 1];
                    var next = nodes[index + 1];
                    nodes.RemoveAt(index);
                    prev.next = next;
                }
            }
        }
    }
}
