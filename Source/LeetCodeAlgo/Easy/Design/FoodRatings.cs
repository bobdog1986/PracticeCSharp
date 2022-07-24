using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    //2353. Design a Food Rating System, #PriorityQueue
    //Modify the rating of a food item listed in the system.
    //Return the highest-rated food item for a type of cuisine in the system.
    public class FoodRatings
    {
        private readonly Dictionary<string, int> dict;
        private readonly Dictionary<string, string> set;
        private readonly Dictionary<string, PriorityQueue<string[], string[]>> map;

        public FoodRatings(string[] foods, string[] cuisines, int[] ratings)
        {
            dict = new Dictionary<string, int>();
            set = new Dictionary<string, string>();
            map = new Dictionary<string, PriorityQueue<string[], string[]>>();
            int n = foods.Length;
            for (int i = 0; i < n; i++)
            {
                dict.Add(foods[i], ratings[i]);
                set.Add(foods[i], cuisines[i]);
                if (!map.ContainsKey(cuisines[i]))
                    map.Add(cuisines[i], new PriorityQueue<string[], string[]>(Comparer<string[]>.Create((x, y) =>
                    {
                        var rate1 = int.Parse(x[1]);
                        var rate2 = int.Parse(y[1]);
                        if (rate1 > rate2) return -1;
                        else if (rate1 < rate2) return 1;
                        else return x[0].CompareTo(y[0]);
                    })));

                map[cuisines[i]].Enqueue(new string[] { foods[i], ratings[i].ToString() }, new string[] { foods[i], ratings[i].ToString() });

            }
        }

        public void ChangeRating(string food, int newRating)
        {
            dict[food] = newRating;
            var cuisine = set[food];
            map[cuisine].Enqueue(new string[] { food, newRating.ToString() }, new string[] { food, newRating.ToString() });
        }

        public string HighestRated(string cuisine)
        {
            var pq = map[cuisine];
            while (pq.Count > 0)
            {
                var top = pq.Peek();
                var rate = int.Parse(top[1]);
                if (dict[top[0]] == rate) return top[0];
                else pq.Dequeue();
            }
            return "";
        }
    }
}
