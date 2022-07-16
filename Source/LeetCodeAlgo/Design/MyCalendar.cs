using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///729. My Calendar I
    ///We can add a new event if adding the event will not cause a double booking.
    ///A double booking happens when two events have some non-empty intersection(i.e., some moment is common to both events.).
    ///The event can be represented as a pair of integers start and end
    ///that represents a booking on the half-open interval [start, end),
    ///the range of real numbers x such that start <= x < end.
    public class MyCalendar
    {
        private class IntervalTreeNode
        {
            public int Start;
            public int End;
            IntervalTreeNode left;
            IntervalTreeNode right;

            public IntervalTreeNode(int start, int end)
            {
                this.Start = start;
                this.End = end;
            }

            public bool Insert(IntervalTreeNode node)
            {
                if (node.Start == node.End || node.Start > node.End)
                    return false;
                if (node.End <= this.Start)
                {
                    if (left == null)
                        left = node;
                    else
                        return left.Insert(node);
                }
                else if (node.Start >= this.End)
                {
                    if (right == null)
                        right = node;
                    else
                        return right.Insert(node);
                }
                else
                    return false;

                return true;
            }
        }

        private IntervalTreeNode root;
        public MyCalendar()
        {

        }

        public bool Book(int start, int end)
        {
            if (this.root == null)
            {
                this.root = new IntervalTreeNode(start, end);
                return true;
            }
            return this.root.Insert(new IntervalTreeNode(start, end));
        }
    }
}