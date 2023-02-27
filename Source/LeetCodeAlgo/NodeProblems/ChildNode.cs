using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeProblems
{
    public class ChildNode
    {

        public class Node
        {
            public int val;
            public Node prev;
            public Node next;
            public Node child;
        }

        ///430. Flatten a Multilevel Doubly Linked List
        public Node Flatten(Node head)
        {
            if (head == null) return null;
            Node curr = head;
            Node prev = null;
            Node childHead = null;
            Stack<Node> stack = new Stack<Node>();
            Stack<Node> subHeads = new Stack<Node>();
            while (curr != null || stack.Count > 0)
            {
                if (curr == null)
                {
                    var top = stack.Pop();
                    var next = top.next;
                    if (prev != null)
                        prev.next = next;
                    if (next != null)
                        next.prev = prev;

                    top.next = childHead;
                    childHead.prev = top;

                    childHead = subHeads.Count > 0 ? subHeads.Pop() : null;

                    curr = next;
                }
                else
                {
                    if (curr.child == null)
                    {
                        prev = curr;
                        curr = curr.next;
                    }
                    else
                    {
                        if (childHead != null)
                            subHeads.Push(childHead);

                        childHead = curr.child;
                        curr.child = null;
                        stack.Push(curr);
                        curr = childHead;
                    }
                }
            }
            return head;
        }
    }
}
