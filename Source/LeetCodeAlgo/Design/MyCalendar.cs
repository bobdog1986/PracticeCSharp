using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///729. My Calendar I, #SegmentTree, #Binary Search
    ///We can add a new event if adding the event will not cause a double booking.
    ///A double booking happens when two events have some non-empty intersection(i.e., some moment is common to both events.).
    ///The event can be represented as a pair of integers start and end
    ///that represents a booking on the half-open interval [start, end),
    ///the range of real numbers x such that start <= x < end.
    public class MyCalendar_SegmentTree
    {
        private readonly SegmentIntervalTree tree = new SegmentIntervalTree(0, 1_000_000_000);

        public bool Book(int start, int end)
        {
            if (tree.Count(start, end-1) == 0)
            {
                tree.Insert(start, end-1);
                //Console.WriteLine($"book [{start},{end}) success!");
                return true;
            }
            else
            {
                //Console.WriteLine($"book [{start},{end}) failed!!!!");
                return false;
            }
        }
    }

    public class MyCalendar_BinarySearch
    {
        private readonly List<int[]> list = new List<int[]>();

        public bool Book(int start, int end)
        {
            end--;
            if (list.Count == 0)
            {
                list.Add(new int[] { start, end});
            }
            else
            {
                if (end < list.First()[0])
                {
                    list.Insert(0, new int[] { start, end });
                }
                else if (start > list.Last()[1])
                {
                    list.Add(new int[] { start, end});
                }
                else
                {
                    int left = 0;
                    int right = list.Count - 1;
                    while (left < right)
                    {
                        int mid = (left + right) / 2;
                        if (list[mid][0] < start)
                        {
                            left = mid+1;
                        }
                        else if (list[mid][0] > start)
                        {
                            right = mid;
                        }
                        else
                            return false;
                    }
                    if (left > 0 && list[left - 1][1] >= start)
                        return false;
                    if (list[left][0] <=end)
                        return false;
                    list.Insert(left, new int[] { start, end});
                }
            }
            return true;
        }
    }
}