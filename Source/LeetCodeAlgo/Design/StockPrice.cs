using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2034. Stock Price Fluctuation, #PriorityQueue
    public class StockPrice
    {
        private readonly PriorityQueue<int[], int> maxHeap;
        private readonly PriorityQueue<int[], int> minHeap;
        private readonly Dictionary<int, int> dict;
        private int current;

        public StockPrice()
        {
            current = 0;
            dict=new Dictionary<int, int>();
            maxHeap = new PriorityQueue<int[], int>();
            minHeap = new PriorityQueue<int[], int>();
        }

        public void Update(int timestamp, int price)
        {
            current = Math.Max(current, timestamp);
            if (dict.ContainsKey(timestamp))
            {
                dict[timestamp] = price;
            }
            else
            {
                dict.Add(timestamp ,price);
            }
            maxHeap.Enqueue(new int[] { timestamp, price }, -price);
            minHeap.Enqueue(new int[] { timestamp, price }, price);
        }

        public int Current()
        {
            return dict[current];
        }

        public int Maximum()
        {
            while(dict[maxHeap.Peek()[0]]!= maxHeap.Peek()[1])
            {
                maxHeap.Dequeue();
            }
            return maxHeap.Peek()[1];
        }

        public int Minimum()
        {
            while (dict[minHeap.Peek()[0]] != minHeap.Peek()[1])
            {
                minHeap.Dequeue();
            }
            return minHeap.Peek()[1];
        }
    }
}
