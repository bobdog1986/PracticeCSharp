using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///2349. Design a Number Container System
    //Insert or Replace a number at the given index in the system.
    //Return the smallest index for the given number in the system.
    //void change(int index, int number) Fills the container at index with the number.Or update if exist
    //int find(int number) Returns the smallest index for the given number, or -1 if there is no index with number
    public class NumberContainers
    {
        private readonly Dictionary<int, int> dict;
        private readonly Dictionary<int, PriorityQueue<int, int>> map;

        public NumberContainers()
        {
            dict = new Dictionary<int, int>();
            map = new Dictionary<int, PriorityQueue<int, int>>();
        }

        public void Change(int index, int number)
        {
            dict[index] = number;
            if (!map.ContainsKey(number))
                map.Add(number, new PriorityQueue<int, int>());
            map[number].Enqueue(index, index);
        }

        public int Find(int number)
        {
            if (!map.ContainsKey(number)) return -1;
            var pq = map[number];
            while (pq.Count > 0)
            {
                int index = pq.Peek();
                if (dict[index] == number) return index;
                else pq.Dequeue();//dict[index] already updated, not equal to number now
            }
            return -1;
        }
    }

}
