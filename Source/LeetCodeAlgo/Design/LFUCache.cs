using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///460. LFU Cache
    //Design and implement a data structure for a Least Frequently Used (LFU) cache.
    //  int get(int key) Gets the value of the key if the key exists in the cache. Otherwise, returns -1.
    //  void put(int key, int value) Update the value of the key if present, or inserts the key if not already present.
    //      When the cache reaches its capacity, it should invalidate and remove the least frequently used key before inserting a new item.
    //      For this problem, when two or more keys with the same frequency, the least recently used key would be invalidated.
    //The functions get and put must each run in O(1) average time complexity.

    public class LFUCache
    {
        private int _capacity;
        private Dictionary<int, int> dict;
        private Dictionary<int, int> keyFreqMap;
        private Dictionary<int, Queue<int>> timeKeyQueue;
        private Dictionary<int, HashSet<int>> timeKeySet;

        private int minFreq;

        public LFUCache(int capacity)
        {
            this._capacity = capacity;
            this.minFreq =0;
            dict = new Dictionary<int, int>();
            keyFreqMap =new Dictionary<int, int>();
            timeKeyQueue =new Dictionary<int, Queue<int>>();
            timeKeySet = new Dictionary<int, HashSet<int>>();
        }

        public int Get(int key)
        {
            if (_capacity==0)
                return -1;
            if (dict.ContainsKey(key))
            {
                UseInternal(key);
                return dict[key];
            }
            else return -1;
        }
        private void UseInternal(int key)
        {
            //work for new create
            if (!keyFreqMap.ContainsKey(key))
                keyFreqMap.Add(key, 0);
            int time = keyFreqMap[key]++;
            if (!timeKeySet.ContainsKey(time))
                timeKeySet.Add(time, new HashSet<int>());
            timeKeySet[time].Remove(key);

            if (time == minFreq && timeKeySet[time].Count==0)
                minFreq++;

            time++;

            if (!timeKeySet.ContainsKey(time))
                timeKeySet.Add(time, new HashSet<int>());
            timeKeySet[time].Add(key);

            if (!timeKeyQueue.ContainsKey(time))
                timeKeyQueue.Add(time, new Queue<int>());
            timeKeyQueue[time].Enqueue(key);
        }

        public void Put(int key, int value)
        {
            if (_capacity==0) return;

            if (dict.ContainsKey(key))
            {
                UseInternal(key);
                dict[key]=value;
            }
            else
            {
                if (dict.Keys.Count == _capacity)
                {
                    //find
                    int remove = -1;
                    while (remove == -1)
                    {
                        if (!timeKeySet.ContainsKey(minFreq) || timeKeySet[minFreq].Count==0)
                        {
                            minFreq++;
                        }
                        else
                        {
                            var q = timeKeyQueue[minFreq];
                            while (q.Count>0)
                            {
                                int top = q.Dequeue();
                                if (keyFreqMap.ContainsKey(top)&&keyFreqMap[top]==minFreq)
                                {
                                    remove = top;
                                    break;
                                }
                            }
                            if (remove==-1)
                                minFreq++;
                        }
                    }
                    //do remove
                    if (dict.ContainsKey(remove))
                        dict.Remove(remove);
                    if (keyFreqMap.ContainsKey(remove))
                        keyFreqMap.Remove(remove);
                    if (timeKeySet.ContainsKey(minFreq))
                        timeKeySet[minFreq].Remove(remove);
                }

                minFreq=0;
                UseInternal(key);
                dict[key]=value;
            }
        }
    }
}
