using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2351. First Letter to Appear Twice, in Easy

        ///2352. Equal Row and Column Pairs, in Easy

        ///2353. Design a Food Rating System, see FoodRatings

        ///2354. Number of Excellent Pairs, #Bit Manipulation
        //sum of the number of set bits in num1 OR num2 and num1 AND num2 >= k,
        //Return the number of distinct excellent pairs. 1 <= k <= 60
        public long CountExcellentPairs(int[] nums, int k)
        {
            //The important point to realize the sum of OR and AND is just the sum of bits of two numbers.
            var dict = new Dictionary<int, HashSet<int>>();
            var set = new HashSet<int>();
            foreach(var n in nums)
            {
                set.Add(n);
                var bits = getBitsCount(n);
                if (!dict.ContainsKey(bits))
                    dict.Add(bits, new HashSet<int>());
                dict[bits].Add(n);
            }
            long res = 0;
            foreach (var i in set)
            {
                int need = k - getBitsCount(i);
                foreach (int key in dict.Keys)
                    if (key >= need)
                        res += (long)dict[key].Count;
            }
            return res;
        }


        //2357. Make Array Zero by Subtracting Equal Amounts
        public int MinimumOperations_A(int[] nums)
        {
            var set = nums.ToHashSet();
            set.Remove(0);
            return set.Count;
        }

        ///2358. Maximum Number of Groups Entering a Competition, #Binary Search
        //group count start from 1 and auto-increase 1, sum of group must strictly increase
        //Return the maximum number of groups that can be formed.
        public int MaximumGroups(int[] grades)
        {
            int n = grades.Length;
            int left = 1;
            int right = n;
            while (left < right)
            {
                int mid = (left + right +1)/ 2;
                long count = (long)mid * (1 + mid) / 2;
                if (count <= n)
                    left = mid;
                else
                    right = mid - 1;
            }
            return left;
        }

        ///2359. Find Closest Node to Given Two Nodes, #Graph, #Cycle
        //a directed graph of n nodes numbered from 0 to n - 1, where each node has at most one outgoing edge.
        //edges of size n, indicating that there is a directed edge from node i to node edges[i].
        //Return the index of the node that can be reached from both node1 and node2,
        //that the maximum between the distance from node1, and from node2 to that node is minimized.
        //return the node with the smallest index, and if no possible return -1
        public int ClosestMeetingNode(int[] edges, int node1, int node2)
        {
            int res = -1;
            Dictionary<int, int> dict1 = new Dictionary<int, int>();
            int cost = 0;
            int idx = node1;
            while (idx != -1)
            {
                if (dict1.ContainsKey(idx))
                    break;
                dict1.Add(idx, cost);
                cost++;
                idx = edges[idx];
            }

            Dictionary<int, int> dict2 = new Dictionary<int, int>();
            cost = 0;
            idx = node2;
            while (idx != -1)
            {
                if (dict2.ContainsKey(idx))
                    break;
                dict2.Add(idx, cost);
                cost++;
                idx = edges[idx];
            }

            int min = int.MaxValue;
            foreach (var k1 in dict1.Keys)
            {
                if (dict2.ContainsKey(k1))
                {
                    int curr = Math.Max(dict1[k1], dict2[k1]);
                    if (curr < min)
                    {
                        min = curr;
                        res = k1;
                    }
                    else if (curr == min)
                    {
                        if (k1 < res)
                            res = k1;
                    }
                }
            }
            return res;
        }

        ///2360. Longest Cycle in a Graph, #Graph, #Cycle, #Good
        // a directed graph of n nodes numbered from 0 to n - 1, where each node has at most one outgoing edge.
        //edges of size n, indicating that there is a directed edge from node i to node edges[i].
        //If there is no outgoing edge from node i, then edges[i] == -1
        //Return the length of the longest cycle in the graph. If no cycle exists, return -1.
        public int LongestCycle(int[] edges)
        {
            int res = -1;
            HashSet<int> visit = new HashSet<int>();// global visisted
            for (int i = 0; i < edges.Length; i++)
            {
                if (visit.Contains(i)) continue;//if visited,skip
                int distance = 0;
                int idx = i;
                Dictionary<int, int> dict = new Dictionary<int, int>();// local visited
                while (idx != -1)//if idx==-1, not a cycle
                {
                    if (dict.ContainsKey(idx))
                    {
                        res = Math.Max(res, distance - dict[idx]);//cycle found
                        break;
                    }
                    if (visit.Contains(idx)) break;//if visited, skip
                    visit.Add(idx);
                    dict.Add(idx, distance++);//store {idx,distacne}, from i to idx
                    idx = edges[idx];
                }
            }
            return res;
        }

        ///2363. Merge Similar Items

        ///2364. Count Number of Bad Pairs, #HashMap, #Good
        //A pair of indices (i, j) is a bad pair if i < j and j - i != nums[j] - nums[i].
        //Return the total number of bad pairs in nums.
        public long CountBadPairs(int[] nums)
        {
            int n = nums.Length;
            long res = (long)n*(n-1)/2;
            Dictionary<int,int> dict = new Dictionary<int, int>();
            for(int i = 0; i < n; i++)
            {
                int val = i - nums[i];
                if (dict.ContainsKey(val)) dict[val]++;
                else dict.Add(val, 1);
            }
            foreach(var k in dict.Keys)
            {
                res -= (long)dict[k] * (dict[k] - 1) / 2;
            }
            return res;
        }

    }
}
