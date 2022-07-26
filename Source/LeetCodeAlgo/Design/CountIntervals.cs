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
        private readonly SegmentIntervalTree root;

        public CountIntervals()
        {
            root = new SegmentIntervalTree(1, 1_000_000_000);
        }

        public void Add(int left, int right)
        {
            root.Insert(left, right);
        }

        public int Count()
        {
            return root.Count();
        }
    }

}
