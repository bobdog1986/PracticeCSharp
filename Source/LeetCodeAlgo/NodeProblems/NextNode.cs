using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeProblems
{
    public class NextNode
    {
        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node next;

            public Node()
            { }

            public Node(int _val)
            {
                val = _val;
            }

            public Node(int _val, Node _left, Node _right, Node _next)
            {
                val = _val;
                left = _left;
                right = _right;
                next = _next;
            }
        }

        /// 116. Populating Next Right Pointers in Each Node
        // You are given a perfect binary tree where all leaves are on the same level
        // Populate each next pointer to point to its next right node.
        // If there is no next right node, the next pointer should be set to NULL.
        public Node Connect_116(Node root)
        {
            if (root == null)
                return null;
            List<Node> list = new List<Node> { root };
            while (list.Count != 0)
            {
                List<Node> subs = new List<Node>();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].next = i == list.Count - 1 ? null : list[i + 1];
                    if (list[i].left != null)
                        subs.Add(list[i].left);
                    if (list[i].right != null)
                        subs.Add(list[i].right);
                }
                list = subs;
            }

            return root;
        }

        /// 117. Populating Next Right Pointers in Each Node II
        // Populate each next pointer to point to its next right node.
        // If there is no next right node, the next pointer should be set to NULL.
        public Node Connect(Node root)
        {
            if (root == null)
                return null;
            List<Node> list = new List<Node> { root };
            while (list.Count != 0)
            {
                List<Node> subs = new List<Node>();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].next = i == list.Count - 1 ? null : list[i + 1];
                    if (list[i].left != null)
                        subs.Add(list[i].left);
                    if (list[i].right != null)
                        subs.Add(list[i].right);
                }
                list = subs;
            }
            return root;
        }
    }
}
