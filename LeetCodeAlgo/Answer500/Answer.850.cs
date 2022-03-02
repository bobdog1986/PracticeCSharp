using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///884. Uncommon Words from Two Sentences
        ///Given two sentences s1 and s2, return a list of all the uncommon words. You may return the answer in any order.
        public string[] UncommonFromSentences(string s1, string s2)
        {
            Dictionary<string, int> sentences = new Dictionary<string, int>();
            var word1 = s1.Split(' ');
            var word2 = s2.Split(' ');
            foreach(var w1 in word1)
            {
                if (string.IsNullOrEmpty(w1)) continue;
                if(!sentences.ContainsKey(w1))sentences.Add(w1, 1);
                else sentences[w1]++;
            }
            foreach (var w2 in word2)
            {
                if (string.IsNullOrEmpty(w2)) continue;
                if (!sentences.ContainsKey(w2)) sentences.Add(w2, 1);
                else sentences[w2]++;
            }

            return sentences.Where(x=>x.Value==1).Select(x=>x.Key).ToArray();
        }
        /// 888. Fair Candy Swap
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
