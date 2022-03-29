using System;
using System.Linq;
using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    // Definition for a binary tree node.

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class Interval
    {
        public int start;
        public int end;

        public Interval()
        {
            start = 0; end = 0;
        }

        public Interval(int s, int e)
        {
            start = s; end = e;
        }
    }

    //class Point
    //{
    //    public int x;
    //    public int y;

    //    public Point()
    //    { x = 0; y = 0; }

    //    public Point(int a, int b)
    //    { x = a; y = b; }
    //}

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

    public class Node_1
    {
        public int val;
        public Node_1 left;
        public Node_1 right;
        public Node_1 next;

        public Node_1()
        { }

        public Node_1(int _val)
        {
            val = _val;
        }

        public Node_1(int _val, Node_1 _left, Node_1 _right, Node_1 _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }
    }

    public class Node_Childs
    {
        public int val;
        public IList<Node_Childs> children;

        public Node_Childs() { }

        public Node_Childs(int _val)
        {
            val = _val;
        }

        public Node_Childs(int _val, IList<Node_Childs> _children)
        {
            val = _val;
            children = _children;
        }
    }

    ///133. Clone Graph , Definition for a Node.
    public class Node_Neighbors
    {
        public int val;
        public IList<Node_Neighbors> neighbors;

        public Node_Neighbors()
        {
            val = 0;
            neighbors = new List<Node_Neighbors>();
        }

        public Node_Neighbors(int _val)
        {
            val = _val;
            neighbors = new List<Node_Neighbors>();
        }

        public Node_Neighbors(int _val, List<Node_Neighbors> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }
    }
}