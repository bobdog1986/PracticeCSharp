using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        //2400. Number of Ways to Reach a Position After Exactly k Steps
        public int NumberOfWays(int startPos, int endPos, int k)
        {
            long mod = 1_000_000_007;
            Dictionary<int, long> dict = new Dictionary<int, long>();
            dict.Add(startPos, 1);
            while (k-- > 0)
            {
                var next = new Dictionary<int, long>();
                foreach (var x in dict.Keys)
                {
                    if (!next.ContainsKey(x + 1))
                        next.Add(x + 1, 0);
                    next[x + 1] += dict[x];//move right
                    next[x + 1] %= mod;

                    if (!next.ContainsKey(x - 1))
                        next.Add(x - 1, 0);
                    next[x - 1] += dict[x];//move left
                    next[x - 1] %= mod;
                }
                dict = next;
            }
            if (dict.ContainsKey(endPos)) return (int)dict[endPos];
            else return 0;
        }
    }
}
