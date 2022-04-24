using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1396. Design Underground System
    public class UndergroundSystem
    {
        private readonly Dictionary<int, (string, int)> checkInMap;
        private readonly Dictionary<string, List<int>> dict;
        public UndergroundSystem()
        {
            checkInMap = new Dictionary<int, (string, int)>();
            dict = new Dictionary<string, List<int>>();
        }

        public void CheckIn(int id, string stationName, int t)
        {
            if (checkInMap.ContainsKey(id)) checkInMap[id] = (stationName, t);
            else checkInMap.Add(id, (stationName, t));
        }

        public void CheckOut(int id, string stationName, int t)
        {
            var checkIn = checkInMap[id];
            var key = checkIn.Item1 + "_" + stationName;
            var time = t - checkIn.Item2;
            if (dict.ContainsKey(key)) dict[key].Add(time);
            else dict.Add(key, new List<int> { time });
        }

        public double GetAverageTime(string startStation, string endStation)
        {
            var key = $"{startStation}_{endStation}";
            if (!dict.ContainsKey(key)) return 0;
            else return dict[key].Sum() * 1.0 / dict[key].Count;
        }
    }

}
