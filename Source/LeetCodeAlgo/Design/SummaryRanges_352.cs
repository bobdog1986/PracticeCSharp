using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;

namespace LeetCodeAlgo.Design
{
    /// 352. Data Stream as Disjoint Intervals, #Binary Search
    //void addNum(int value) Adds the integer value to the stream.
    //int[][] getIntervals() Returns a summary of the integers in the stream currently as a list of disjoint intervals[starti, endi].
    //The answer should be sorted by starti.
    public class SummaryRanges
	{
        private readonly List<int[]> list;
        private readonly HashSet<int> set;

		public SummaryRanges()
		{
            list = new List<int[]>();
            set = new HashSet<int>();
		}

        public void AddNum(int value)
        {
            if (set.Contains(value))
                return;
            set.Add(value);

            if(list.Count ==0)
            {
                list.Add(new int[] { value, value });
            }
            else
            {
                if (value < list[0][0])
                {
                    if(value == list[0][0] - 1)
                    {
                        list[0][0]--;
                    }
                    else
                    {
                        list.Insert(0, new int[] { value, value });
                    }
                }
                else if(value > list.Last()[1])
                {
                    if(value == list.Last()[1] + 1)
                    {
                        list.Last()[1]++;
                    }
                    else
                    {
                        list.Add(new int[] { value, value });
                    }
                }
                else
                {
                    int left = 0;
                    int right = list.Count - 1;
                    while (left < right)
                    {
                        int mid = (left + right) / 2;
                        if (list[mid][0] < value)
                        {
                            left = mid + 1;
                        }
                        else
                        {
                            right = mid;
                        }
                    }

                    int prev = -1;
                    if (left > 0 && list[left - 1][1] == value - 1)
                        prev = left - 1;
                    int next = -1;
                    if (list[left][0] == value + 1)
                        next = left;
                    if(prev !=-1 && next != -1)
                    {
                        list[prev][1] = list[next][1];
                        list.RemoveAt(next);
                    }
                    else if(prev!=-1)
                    {
                        list[prev][1]++;
                    }
                    else if(next !=-1)
                    {
                        list[next][0]--;
                    }
                    else
                    {
                        list.Insert(left, new int[] { value, value });
                    }
                }
            }
        }

        public int[][] GetIntervals()
        {
            return list.ToArray();
        }
    }
}

