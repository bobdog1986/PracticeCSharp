using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2276. Count Integers in Intervals, #Segment Tree
    ///Implement the CountIntervals class:
    ///void add(int left, int right) Adds the interval[left, right] to the set of intervals.
    ///int count() Returns the number of integers that are present in at least one interval.
    ///Note that an interval [left, right] denotes all the integers x where left <= x <= right.
    public class CountIntervals
    {
        private Node2276 root;
        public CountIntervals()
        {
            root = new Node2276(0, 1000000000, 0);
        }

        public void Add(int left, int right)
        {
            setRange(root, left, right);
        }
        private void setRange(Node2276 node, int left, int right)
        {
            if (left <= node.lower && node.upper <= right)
            {
                node.val = node.upper - node.lower + 1;
                node.left = null;
                node.right = null;
                return;
            }

            int mid = (node.upper + node.lower) / 2;
            if (node.left == null && node.right == null)
            {
                node.left = new Node2276(node.lower, mid, node.val > 0 ? mid - node.lower + 1 : 0);
                node.right = new Node2276(mid + 1, node.upper, node.val > 0 ? node.upper - (mid + 1) + 1 : 0);
            }
            if (left <= mid)
                setRange(node.left, left, right);
            if (right > mid)
                setRange(node.right, left, right);
            node.val = node.left.val + node.right.val;
        }

        public int Count()
        {
            return root.val;
        }
    }

    public class Node2276
    {
        public int lower, upper, val;
        public Node2276 left, right;
        public Node2276(int lower, int upper, int val)
        {
            this.lower = lower;
            this.upper = upper;
            this.val = val;
        }
    }


    public class CountIntervals_SegmentTree
    {
        private class SegmentTreeNode
        {
            private int start, end;
            private SegmentTreeNode leftNode = null, rightNode = null;

            public int Count { get; private set; }

            public SegmentTreeNode(int start, int end)
            {
                this.start = start;
                this.end = end;
            }

            public int Add(int left, int right)
            {
                if (left > right || left > end || right < start)
                    return Count;
                if ((left == start && right == end) || Count == end - start + 1)
                    return Count = end - start + 1;

                int middle = start + (end - start) / 2;

                this.leftNode = this.leftNode ?? new SegmentTreeNode(start, middle);
                this.rightNode = this.rightNode ?? new SegmentTreeNode(middle + 1, end);

                return Count = this.leftNode.Add(left, Math.Min(middle, right)) + this.rightNode.Add(Math.Max(left, middle + 1), right);
            }
        }

        private SegmentTreeNode root = new(1, 1_000_000_000);

        public void Add(int left, int right)
        {
            root.Add(left, right);
        }

        public int Count()
        {
            return root.Count;
        }
    }

}
