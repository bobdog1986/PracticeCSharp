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
        public IList<IList<int>> MergeSimilarItems(int[][] items1, int[][] items2)
        {
            var res = new List<IList<int>>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var i in items1)
            {
                if (dict.ContainsKey(i[0])) dict[i[0]] += i[1];
                else dict.Add(i[0], i[1]);
            }
            foreach (var i in items2)
            {
                if (dict.ContainsKey(i[0])) dict[i[0]] += i[1];
                else dict.Add(i[0], i[1]);
            }
            var keys = dict.Keys.OrderBy(x => x).ToArray();
            foreach (var k in keys)
                res.Add(new List<int>() { k, dict[k] });
            return res;
        }

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

        ///2365. Task Scheduler II, #HashMap
        //space represents the minimum number of days that must pass after the completion of a task
        //before another task of the same type can be performed.
        public long TaskSchedulerII(int[] tasks, int space)
        {
            Dictionary<int, long> dict = new Dictionary<int, long>();//store {taskId,lastDayOfTask}
            long days = 0;
            for(int i = 0; i < tasks.Length;i++)
            {
                if (dict.ContainsKey(tasks[i]))
                {
                    if(days - dict[tasks[i]] > space)//no need to wait
                    {
                        dict[tasks[i]]=days;
                        days++;
                    }
                    else
                    {
                        days = dict[tasks[i]] + space+1;//must wait until dsy = dict[tasks[i]] + space+1
                        dict[tasks[i]] = days;
                        days++;
                    }
                }
                else
                {
                    dict.Add(tasks[i], days);
                    days++;
                }
            }
            return days;
        }

        ///2366. Minimum Replacements to Sort the Array, #Greedy, #Good
        // In one operation you can replace any element of the array with any two elements that sum to it.
        public long MinimumReplacement(int[] nums)
        {
            long res = 0;
            int n = nums.Length;
            int max = nums.Last();
            for(int i = n - 1; i >= 0; i--)
            {
                //x is count to split, still work for nums[i]<=max
                int x = (nums[i] + max - 1) / max;
                res += x - 1;
                //every bucket must contain at least nums[i]/k,some of them may contains nums[i]/k+1.
                //like fullfil buckets nums[i]/k or nums[i]/k+1 loops, not fullfil the first one ,then next...
                max = nums[i] / x;
            }
            return res;
        }

        ///2367. Number of Arithmetic Triplets
        public int ArithmeticTriplets(int[] nums, int diff)
        {
            int n = nums.Length;
            int res = 0;
            for(int i = 0; i < n - 2; i++)
            {
                for(int j = i + 1; j < n - 1; j++)
                {
                    if (nums[j] -nums[i] !=diff) continue;
                    for(int k = j + 1; k < n; k++)
                    {
                        if (nums[k] -nums[j]!=diff) continue;
                        if (nums[k] - nums[j] ==diff && nums[j] - nums[i]==diff)
                            res++;
                    }
                }
            }
            return res;
        }

        ///2368. Reachable Nodes With Restrictions
        public int ReachableNodes(int n, int[][] edges, int[] restricted)
        {
            HashSet<int> res =new  HashSet<int>();
            List<int>[] graph = new List<int>[n];
            for(int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }
            foreach(var e in edges)
            {
                graph[e[0]].Add(e[1]);
                graph[e[1]].Add(e[0]);
            }
            HashSet<int> set = new HashSet<int>(restricted);
            ReachableNodes(0, -1, graph, set, res);
            return res.Count;
        }

        private void ReachableNodes(int i, int prev, List<int>[] graph, HashSet<int> set, HashSet<int> res)
        {
            res.Add(i);
            foreach(var j in graph[i])
            {
                if (j == prev) continue;
                if (set.Contains(j)) continue;
                ReachableNodes(j, i, graph, set, res);
            }
        }
    }
}
