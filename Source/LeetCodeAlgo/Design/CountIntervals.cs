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
        private class SegmentTreeNode
        {
            private int start;
            private int end;
            private SegmentTreeNode leftNode = null;
            private SegmentTreeNode rightNode = null;

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
                {
                    Count = end - start + 1;
                    return Count;
                }

                int mid = start + (end - start) / 2;
                if(leftNode == null)
                    this.leftNode = new SegmentTreeNode(start, mid);
                if(rightNode == null)
                    this.rightNode = new SegmentTreeNode(mid + 1, end);

                var leftCount = this.leftNode.Add(left, Math.Min(mid, right));
                var rightCount = this.rightNode.Add(Math.Max(left, mid + 1), right);
                Count = leftCount + rightCount;
                return Count;
            }
        }

        private readonly SegmentTreeNode root ;

        public CountIntervals()
        {
            root = new SegmentTreeNode(1, 1_000_000_000);
        }

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
