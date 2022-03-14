using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1603. Design Parking System

        /// 1609. Even Odd Tree, #BFS, #BTree
        ///A binary tree is named Even-Odd if it meets the following conditions:
        ///odd-indexed level, all nodes at the level have even integer values in strictly decreasing order(from left to right).
        ///even-indexed level, all nodes at the level have odd integer values in strictly increasing order (from left to right).
        public bool IsEvenOddTree(TreeNode root)
        {
            List<TreeNode> list =new List<TreeNode>() { root};
            bool odd = false;
            while(list.Count > 0)
            {
                List<TreeNode> next = new List<TreeNode>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (odd)
                    {
                        if (list[i].val % 2 == 1) return false;
                        if (i < list.Count - 1 && list[i].val <= list[i + 1].val) return false;
                    }
                    else
                    {
                        if (list[i].val % 2 == 0) return false;
                        if (i < list.Count - 1 && list[i].val >= list[i + 1].val) return false;
                    }
                    if (list[i].left != null) next.Add(list[i].left);
                    if (list[i].right != null) next.Add(list[i].right);
                }
                odd = !odd;
                list = next;
            }
            return true;
        }

        ///1615. Maximal Network Rank, #Graph
        ///Given the integer n and the array roads, return the maximal network rank of the entire infrastructure.
        ///two cities donot need connected
        public int MaximalNetworkRank(int n, int[][] roads)
        {
            int max = 0;
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();

            foreach (var road in roads)
            {
                graph[road[0]].Add(road[1]);
                graph[road[1]].Add(road[0]);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (graph[i].Contains(j))
                        max = Math.Max(max, graph[i].Count + graph[j].Count - 1);
                    else
                        max = Math.Max(max, graph[i].Count + graph[j].Count);
                }

            }
            return max;
        }
        /// 1636. Sort Array by Increasing Frequency
        ///sort the array in increasing order based on the frequency of the values.
        ///If multiple values have the same frequency, sort them in decreasing order.
        public int[] FrequencySort(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var n in nums)
            {
                if(dict.ContainsKey(n))dict[n]++;
                else dict.Add(n, 1);
            }
            var keys=dict.Keys.OrderBy(x=>dict[x]).ThenBy(x=>-x);
            var res = new int[nums.Length];
            int i = 0;
            foreach(var key in keys)
            {
                while (dict[key]-- > 0)
                {
                    res[i++] = key;
                }
            }
            return res;
        }
    }
}
