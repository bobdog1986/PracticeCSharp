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
        public MyCalendar()
        {
        }

        public bool Book(int start, int end)
        {
            return false;
        }
    }
}