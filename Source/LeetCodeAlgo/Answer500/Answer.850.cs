using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///859. Buddy Strings
        ///return true if you can swap two letters in s so the result is equal to goal, otherwise, return false.
        public bool BuddyStrings(string s, string goal)
        {
            if (s.Length != goal.Length) return false;
            int[] arr1 = new int[26];
            int[] arr2 = new int[26];
            int diff = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != goal[i]) diff++;
                if (diff > 2) return false;
                arr1[s[i] - 'a']++;
                arr2[goal[i] - 'a']++;
            }
            bool len2 = false;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i]) return false;
                if (arr1[i] >= 2) len2 = true;
            }
            if (diff == 0 && !len2) return false;
            else return true;
        }
        /// 875. Koko Eating Bananas
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
            while (next != null)
            {
                count++;
                next = next.next;
            }
            int len = count / 2;
            while (len-- > 0)
            {
                head = head.next;
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
        ///886. Possible Bipartition, #Graph, #DFS, #BFS
        ///Given the integer n and the array dislikes where dislikes[i] = [ai, bi]
        ///indicates that the person labeled ai does not like the person labeled bi,
        ///return true if it is possible to split everyone into two groups in this way.
        public bool PossibleBipartition(int n, int[][] dislikes)
        {
            int[,] graph = new int[n+1,n+1];
            foreach(var dis in dislikes)
            {
                graph[dis[0], dis[1]] = 1;
                graph[dis[1], dis[0]] = 1;
            }
            ///visit[i] = 0 means node i hasn't been visited.
            ///visit[i] = 1 means node i has been grouped to 1.
            ///visit[i] = -1 means node i has been grouped to - 1.
            int[] visit = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                if (visit[i] == 0 && !PossibleBipartition_dfs(graph, visit, i, 1))
                {
                    return false;
                }
            }
            return true;
        }
        private bool PossibleBipartition_dfs(int[,] graph, int[] visit, int index, int slot)
        {
            visit[index] = slot;
            for (int i = 1; i < visit.Length; i++)
            {
                if (graph[index,i] == 1)
                {
                    if (visit[i] == slot)
                    {
                        return false;
                    }
                    if (visit[i] == 0 && !PossibleBipartition_dfs(graph, visit, i, -slot))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool PossibleBipartition_bfs(int n, int[][] dislikes)
        {
            int[,] graph = new int[n + 1, n + 1];
            foreach (var dis in dislikes)
            {
                graph[dis[0], dis[1]] = 1;
                graph[dis[1], dis[0]] = 1;
            }
            ///visit[i] = 0 means node i hasn't been visited.
            ///visit[i] = 1 means node i has been grouped to 1.
            ///visit[i] = -1 means node i has been grouped to - 1.
            int[] visit = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                if (visit[i] == 0)
                {
                    visit[i] = 1;
                    Queue<int> q = new Queue<int>();
                    q.Enqueue(i);
                    while (q.Count>0)
                    {
                        int cur = q.Dequeue();
                        for(int j=1; j <= n; j++)
                        {
                            if (graph[cur, j] == 1)
                            {
                                if (visit[j] == 0)
                                {
                                    visit[j] = -visit[cur] ;
                                    q.Enqueue(j);
                                }
                                else
                                {
                                    if (visit[j] == visit[cur]) return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
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

        ///896. Monotonic Array
        ///An array is monotonic if it is either monotone increasing or monotone decreasing.
        ///An array nums is monotone increasing if for all i <= j, nums[i] <= nums[j].
        ///An array nums is monotone decreasing if for all i <= j, nums[i] >= nums[j].
        ///Given an integer array nums, return true if the given array is monotonic, or false otherwise.
        public bool IsMonotonic(int[] nums)
        {
            bool increasing = true;
            bool decreasing = true;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (!increasing && !decreasing) return false;
                if (increasing) increasing = nums[i] <= nums[i + 1];
                if (decreasing) decreasing = nums[i] >= nums[i + 1];
            }
            return increasing || decreasing;
        }
    }
}
