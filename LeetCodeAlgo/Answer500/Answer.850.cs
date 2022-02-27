using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///888. Fair Candy Swap
        ///Return an integer array answer where answer[0] is the number of candies in the box that Alice must exchange,
        ///and answer[1] is the number of candies in the box that Bob must exchange.
        public int[] FairCandySwap(int[] aliceSizes, int[] bobSizes)
        {
            Dictionary<int, int> aliceDict = new Dictionary<int, int>();
            int sumAlice = 0;
            Dictionary<int, int> bobDict = new Dictionary<int, int>();
            int sumBob = 0;
            for (int i = 0; i < aliceSizes.Length; i++)
            {
                if (!aliceDict.ContainsKey(aliceSizes[i])) { aliceDict.Add(aliceSizes[i], i); }
                sumAlice += aliceSizes[i];
            }
            for (int i = 0; i < bobSizes.Length; i++)
            {
                if (!bobDict.ContainsKey(bobSizes[i])) { bobDict.Add(bobSizes[i], i); }
                sumBob += bobSizes[i];
            }
            int diff = (sumBob - sumAlice) / 2;
            foreach (var key in aliceDict.Keys)
            {
                if (bobDict.ContainsKey(key + diff))
                    return new int[] { key, key + diff };
            }
            return null;
        }
    }
}
