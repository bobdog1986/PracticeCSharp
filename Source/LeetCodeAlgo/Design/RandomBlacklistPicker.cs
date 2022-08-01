using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design.RandomBlacklistPicker
{
    ///710. Random Pick with Blacklist
    //randomly pick in [0,n-1] and skip all blacklist
    public class Solution
    {
        private readonly Random r;
        private readonly int Count;
        private readonly Dictionary<int,int> dict;
        public Solution(int n, int[] blacklist)
        {
            dict = new Dictionary<int, int>();
            r = new Random();
            var set = blacklist.ToHashSet();
            Count = n - set.Count;
            int start = Count;
            foreach(var b in set)
            {
                if (b >= Count) continue;
                while (set.Contains(start))
                    start++;
                dict.Add(b, start++);
            }
        }

        public int Pick()
        {
            var x = r.Next(Count);
            if (dict.ContainsKey(x)) return dict[x];
            else return x;
        }
    }
}
