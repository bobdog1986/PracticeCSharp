using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeProblems
{
    public class RandomNode
    {
        public class Node
        {
            public int val;
            public Node next;
            public Node random;

            public Node(int _val)
            {
                val = _val;
                next = null;
                random = null;
            }
        }

        ///138. Copy List with Random Pointer, #Good!
        //Random may pointer to itself
        public Node CopyRandomList(Node head)
        {
            Node curr = head;
            Node next = null;
            // First round: make copy of each node,
            // and link them together side-by-side in a single list.
            while (curr != null)
            {
                next = curr.next;

                Node copy1 = new Node(curr.val);
                curr.next = copy1;
                copy1.next = next;

                curr = next;
            }

            // Second round: assign random pointers for the copy nodes.
            curr = head;
            while (curr != null)
            {
                if (curr.random != null)
                {
                    //every copy node is the next of prev node in origin sequence
                    curr.next.random = curr.random.next;
                }
                curr = curr.next.next;
            }

            // Third round: restore the original list, and extract the copy list.
            curr = head;
            Node pseudoHead = new Node(0);
            Node copyPrev = pseudoHead;
            while (curr != null)
            {
                next = curr.next.next;//real next in origin sequence

                // extract the copy
                copyPrev.next = curr.next;
                copyPrev = copyPrev.next;

                // restore the original list
                curr.next = next;

                curr = next;
            }
            return pseudoHead.next;
        }


        public void printRandomNode(Node node, int maxLen = 20)
        {
            if (node == null)
            {
                Console.WriteLine("node is []");
                return;
            }
            List<int> list = new List<int>();
            List<Node> rList = new List<Node>();
            while (node != null && list.Count <= maxLen)
            {
                list.Add(node.val);
                rList.Add(node.random);
                node = node.next;
            }

            Console.WriteLine($"Node is [{string.Join(",", list)}]");

            StringBuilder sb = new StringBuilder();
            sb.Append("Rand is [");
            foreach (var n in rList)
            {
                if (n == null)
                {
                    sb.Append("-,");
                }
                else
                {
                    sb.Append(n.val + ",");
                }
            }
            sb.Append("]");
            Console.WriteLine(sb.ToString());

        }
    }
}
