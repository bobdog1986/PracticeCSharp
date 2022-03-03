﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///875. Koko Eating Bananas
        ///There are n piles of bananas, the ith pile has piles[i] bananas.
        ///The guards have gone and will come back in h hours.
        ///Find min numb to eat all bananas in h hours. each time can only eat 1 index;
        public int MinEatingSpeed(int[] piles, int h)
        {
            if (piles.Length == h)
                return piles.Max();

            int low = 1, high = 1000000000;
            int mid = (low + high) / 2;
            while (low <= high)
            {
                int sum = 0;
                for (int i = 0; i < piles.Length; i++)
                    sum += (int)Math.Ceiling(1.0 * piles[i] / mid);

                if (sum > h)
                    low = mid + 1;
                else
                    high = mid - 1;

                mid = (low + high) / 2;
            }
            return low;
        }

        /// 876. Middle of the Linked List
        public ListNode MiddleNode(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            var next = head.next;

            int count = 1;
            //List<int> nodes = new List<int>();
            while (next != null)
            {
                count++;
                //nodes.Add(head.val);
                next = next.next;
            }

            int len = count / 2;

            while (len > 0)
            {
                head = head.next;
                len--;
            }

            return head;
        }

        ///878. Nth Magical Number, #Binary Search
        ///A positive integer is magical if it is divisible by either a or b.
        ///Given the three integers n, a, and b, return the nth magical number.
        ///return it modulo 10^9 + 7. 1 <= n <= 10^9, 2 <= a, b <= 4 * 10^4
        public int NthMagicalNumber(int n, int a, int b)
        {
            int mod = 1_000_000_007;
            int c = a * b / getGcb(a, b);

            long low = 0;
            long high = (long)n * Math.Min(a, b);
            //high will alway >=n, increase low and decrease high to the edge!
            while (low < high)
            {
                long mid = low + (high - low) / 2;
                if (mid / a + mid / b - mid / c < n)
                    low = mid + 1;
                else
                    high = mid;
            }

            return (int)(low % mod);
        }

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