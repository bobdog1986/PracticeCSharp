using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///981. Time Based Key-Value Store, #Binary Search

    public class TimeMap
    {
        private class TimeMapItem
        {
            public TimeMapItem(int timeStamp, string value)
            {
                this.TimeStamp = timeStamp;
                this.Value = value;
            }
            public string Value;
            public int TimeStamp;
        }

        private readonly Dictionary<string, List<TimeMapItem>> dict;

        public TimeMap()
        {
            dict = new Dictionary<string, List<TimeMapItem>>();
        }

        public void Set(string key, string value, int timestamp)
        {
            var curr = new TimeMapItem(timestamp, value);
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, new List<TimeMapItem>());
                dict[key].Add(curr);
            }
            else
            {
                var list = dict[key];
                if (timestamp < list[0].TimeStamp) dict[key].Insert(0, curr);
                else if(timestamp > list.Last().TimeStamp) dict[key].Add(curr);
                else
                {
                    int left = 0;
                    int right = list.Count - 1;
                    while (left < right)
                    {
                        int mid = (left + right +1) / 2;
                        if (list[mid].TimeStamp > timestamp)
                        {
                            right = mid-1;
                        }
                        else
                        {
                            left = mid;
                        }
                    }
                    dict[key].Insert(left+1, curr);
                }
            }
        }

        public string Get(string key, int timestamp)
        {
            if (!dict.ContainsKey(key)) return string.Empty;

            var items = dict[key];
            int left = 0;
            int right = items.Count - 1;
            if (timestamp < items[left].TimeStamp)
                return string.Empty;
            if (timestamp >= items[right].TimeStamp)
                return items[right].Value;

            while (left < right)
            {
                var mid = (left + right + 1) / 2;
                if (items[mid].TimeStamp == timestamp)
                {
                    return items[mid].Value;
                }
                else if (items[mid].TimeStamp > timestamp)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid;
                }
            }
            return items[left].Value;
        }
    }
}
