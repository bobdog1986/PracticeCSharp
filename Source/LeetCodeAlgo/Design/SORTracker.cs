using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2102. Sequentially Ordinal Rank Tracker, #PriorityQueue, #Heap
    ///A scenic location is represented by its name and attractiveness score,
    ///where name is a unique string among all locations and score is an integer.
    ///Locations can be ranked from the best to the worst. The higher the score, the better the location.
    ///If the scores of two locations are equal, then the location with the lexicographically smaller name is better.
    public class SORTracker
    {
        //Explaination：
        //1.minHeap：count of minHeap equal to call times and store all location keys which rank less than call times.
        //2.maxHeap: store all locations keys which rank greater or equal to call times. So the top of maxHeap is the next key.
        //3.Everytime call Add() API, we enqueue it to maxHeap first, then check if the top of maxheap should swap with top of minheap.
        private readonly PriorityQueue<string, string> maxHeap;
        private readonly PriorityQueue<string, string> minHeap;
        private readonly Dictionary<string, int> dict;

        public SORTracker()
        {
            dict = new Dictionary<string, int>();
            //beware of the diffierent compare instance of minheap and maxheap
            maxHeap = new PriorityQueue<string, string>(Comparer<string>.Create(
                new Comparison<string>((x, y) =>
                {
                    if (dict[x] != dict[y]) return dict[y] - dict[x];
                    else return x.CompareTo(y);
                })));

            minHeap = new PriorityQueue<string, string>(Comparer<string>.Create(
                new Comparison<string>((x, y) =>
                {
                    if (dict[x] != dict[y]) return dict[x] - dict[y];
                    else return y.CompareTo(x);
                })));
        }

        public void Add(string name, int score)
        {
            dict.Add(name, score);
            maxHeap.Enqueue(name, name);//enqueue to maxHeap first
            while (minHeap.Count > 0)
            {
                var max = maxHeap.Peek();
                var min = minHeap.Peek();
                //check if should swap the top of maxHeap and minHeap
                if (dict[max] > dict[min] ||
                    (dict[max] == dict[min] && max.CompareTo(min) == -1))
                {
                    max = maxHeap.Dequeue();
                    min = minHeap.Dequeue();
                    maxHeap.Enqueue(min, min);
                    minHeap.Enqueue(max, max);
                }
                else break;
            }
        }

        public string Get()
        {
            var key = maxHeap.Dequeue();
            minHeap.Enqueue(key, key);//dequeue the top of maxheap then enqueue it to minheap
            return key;
        }
    }
}