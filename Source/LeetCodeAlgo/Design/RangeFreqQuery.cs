using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2080. Range Frequency Queries, #Binary Search
    ///int query(int left, int right, int value) Returns the frequency of value in the subarray arr[left...right]
    public class RangeFreqQuery
    {
        private readonly Dictionary<int,List<int>> freqs = new Dictionary<int,List<int>>();
        public RangeFreqQuery(int[] arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                if (!freqs.ContainsKey(arr[i]))
                    freqs.Add(arr[i], new List<int>());
                freqs[arr[i]].Add(i);
            }
        }

        public int Query(int left, int right, int value)
        {
            if (!freqs.ContainsKey(value)) return 0;
            var list = freqs[value];
            if (list.First() > right || list.Last() < left) return 0;
            if (left <= list.First() && right >= list.Last()) return list.Count;

            int count1 = 0;
            int count2 = 0;
            if (left > list.First())
            {
                int low = 0;
                int high = list.Count - 1;
                while (low < high)
                {
                    int mid = (low + high + 1) / 2;
                    if (list[mid] < left)
                    {
                        low = mid;
                    }
                    else
                    {
                        high = mid - 1;
                    }
                }
                count1 = low + 1;
            }

            if (right >= list.Last()) count2 = list.Count;
            else
            {
                int low = 0;
                int high = list.Count - 1;
                while (low < high)
                {
                    int mid = (low + high + 1) / 2;
                    if (list[mid] <= right)
                    {
                        low = mid;
                    }
                    else
                    {
                        high = mid - 1;
                    }
                }
                count2 = low + 1;
            }
            return count2 - count1;
        }
    }
}
