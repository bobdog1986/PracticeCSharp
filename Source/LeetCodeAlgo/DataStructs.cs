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


    public class Employee
    {
        public int id;
        public int importance;
        public IList<int> subordinates;
    }

}