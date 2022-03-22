﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///752. Open the Lock, #Graph
        public int OpenLock(string[] deadends, string target)
        {
            int res = 0;
            HashSet<string> dead = new HashSet<string>(deadends);
            HashSet<string> visit = new HashSet<string>() { "0000" };

            var queue = new Queue<string>();
            queue.Enqueue("0000");
            while (queue.Count > 0)
            {
                int size = queue.Count;
                for (int i = 0; i < size; i++)
                {
                    var str = queue.Dequeue();
                    if (str == target) return res;
                    if (dead.Contains(str)) continue;
                    for (int j = 0; j < 4; j++)
                    {
                        var upKey = str;
                        var downKey = str;
                        if (str[j] == '9')
                        {
                            var arr = str.ToArray();
                            arr[j] = '0';
                            upKey = new string(arr);
                        }
                        else
                        {
                            var arr = str.ToArray();

                            arr[j] = (char)(arr[j] + 1);
                            upKey = new string(arr);
                        }

                        if (str[j] == '0')
                        {
                            var arr = str.ToArray();

                            arr[j] = '9';
                            downKey = new string(arr);
                        }
                        else
                        {
                            var arr = str.ToArray();

                            arr[j] = (char)(arr[j] - 1);
                            downKey = new string(arr);
                        }

                        if (!dead.Contains(upKey) && !visit.Contains(upKey))
                        {
                            queue.Enqueue(upKey);
                            visit.Add(upKey);
                        }
                        if (!dead.Contains(downKey) && !visit.Contains(downKey))
                        {
                            queue.Enqueue(downKey);
                            visit.Add(downKey);
                        }
                    }
                }
                res++;
            }

            return -1;
        }

        /// 762. Prime Number of Set Bits in Binary Representation
        ///Return the count of numbers in range [left, right] having a prime number of set bits in binary representation.
        public int CountPrimeSetBits(int left, int right)
        {
            int ans = 0;
            HashSet<int> map = new HashSet<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31 };
            for (int i = left; i <= right; i++)
            {
                if (map.Contains(CountPrimeSetBits(i)))
                    ans++;
            }
            return ans;
        }

        public int CountPrimeSetBits(int n)
        {
            int count = 0;
            while (n > 0)
            {
                count += n & 1;
                n >>= 1;
            }
            return count;
        }

        /// 763. Partition Labels
        ///s consists of lowercase English letters.
        public IList<int> PartitionLabels(string s)
        {
            var res = new List<int>();
            Dictionary<char, HashSet<int>> dict = new Dictionary<char, HashSet<int>>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!dict.ContainsKey(s[i]))dict.Add(s[i], new HashSet<int>());
                dict[s[i]].Add(i);
            }

            HashSet<char> currSet = new HashSet<char>();
            int len = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (!currSet.Contains(s[i]))
                    currSet.Add(s[i]);
                bool canPartition = !currSet.Any(c => dict[c].Any(x => x > i));
                if (canPartition)
                {
                    currSet.Clear();
                    int len2 = i + 1;
                    res.Add(len2 - len);
                    len = len2;
                }
            }
            return res;
        }

        ///771. Jewels and Stones
        public int NumJewelsInStones(string jewels, string stones)
        {
            int res = 0;
            HashSet<char> set = new HashSet<char>();
            foreach (var j in jewels)
                set.Add(j);

            foreach (var s in stones)
                if (set.Contains(s)) res++;
            return res;
        }
        /// 784. Letter Case Permutation
        ///ref 77 Combines()
        ///Given a string s, you can transform every letter individually to be lowercase or uppercase to create another string.
        ///Return a list of all possible strings we could create.Return the output in any order.
        /// 1 <= s.length <= 12
        /// s consists of lowercase English letters, uppercase English letters, and digits.
        public IList<string> LetterCasePermutation(string s)
        {
            var result = new List<string>();

            var carrIndexs = new List<int>();

            var carr = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsLetter(carr[i]))
                {
                    carrIndexs.Add(i);
                }
            }

            if (carrIndexs.Count == 0)
            {
                result.Add(s);
            }
            else
            {
                IList<IList<int>> combines = new List<IList<int>>
                {
                    new List<int>()
                };
                for (int i = 1; i <= carrIndexs.Count; i++)
                {
                    var comb2 = Combine(carrIndexs.Count, i);
                    foreach (var j in comb2)
                        combines.Add(j);
                }

                foreach (var c in combines)
                {
                    var arr = s.ToCharArray();
                    for (int i = 0; i < carrIndexs.Count; i++)
                    {
                        if (c.Contains(i + 1))
                        {
                            arr[carrIndexs[i]] = char.ToUpper(arr[carrIndexs[i]]);
                        }
                        else
                        {
                            arr[carrIndexs[i]] = char.ToLower(arr[carrIndexs[i]]);
                        }
                    }

                    result.Add(string.Join("", arr));
                }

                return result;
            }
            return result;
        }

        ///785. Is Graph Bipartite? #Graph, #DFS
        ///Same to 886
        ///A graph is bipartite if the nodes can be partitioned into two independent sets A and B
        ///such that every edge in the graph connects a node in set A and a node in set B.
        ///Return true if and only if it is bipartite.
        public bool IsBipartite(int[][] graph)
        {
            int n = graph.Length;
            ///visit[i] = 0 means node i hasn't been visited.
            ///visit[i] = 1 means node i has been grouped to 1.
            ///visit[i] = -1 means node i has been grouped to - 1.
            int[] visit = new int[n];
            for (int i = 0; i < n; i++)
            {
                if (visit[i] == 0 && !IsBipartite_dfs(graph, visit, i, 1)) return false;
            }
            return true;
        }
        private bool IsBipartite_dfs(int[][] graph, int[] visit, int index, int slot)
        {
            visit[index] = slot;
            foreach(var i in graph[index])
            {
                if (visit[i] == slot) return false;
                if (visit[i] == 0 && !IsBipartite_dfs(graph, visit, i, -slot)) return false;
            }
            return true;
        }
        /// 797. All Paths From Source to Target
        ///only 0 to N-1
        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            var ans = new List<IList<int>>();

            int len = graph.Length;

            //only 0 to N-1
            for (int i = 0; i < 1; i++)
            {
                var curr = new List<IList<int>>();
                var all = new List<IList<int>>();

                var list = new List<int>() { i };

                curr.Add(list);

                while (curr.Count > 0)
                {
                    var sub = new List<IList<int>>();

                    foreach (var item in curr)
                    {
                        var nexts = AllPathsSourceTarget_Add(graph, item);
                        if (nexts.Count > 0)
                        {
                            foreach (var next in nexts)
                            {
                                if (next.Last() == len - 1)
                                {
                                    all.Add(next);
                                }
                                else
                                {
                                    sub.Add(next);
                                }
                            }
                        }
                    }

                    curr = sub;
                }

                if (all.Count > 0)
                {
                    foreach (var j in all)
                    {
                        if (j.Last() == len - 1)
                            ans.Add(j);
                    }
                }
            }

            return ans;
        }

        public IList<IList<int>> AllPathsSourceTarget_Add(int[][] graph, IList<int> list)
        {
            var ans = new List<IList<int>>();

            if (list.Count > 0 && graph[list.Last()].Length > 0)
            {
                foreach (var j in graph[list.Last()])
                {
                    var sub = new List<int>(list)
                    {
                        j
                    };
                    ans.Add(sub);
                }
            }

            return ans;
        }

        ///799. Champagne Tower , #DP
        ///0 <= poured <= 10^9 , 0 <= query_glass <= query_row< 100
        public double ChampagneTower(int poured, int query_row, int query_glass)
        {
            //from top to buttom
            double[,] result = new double[101, 101];
            result[0, 0] = poured;
            for (int i = 0; i <= query_row; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (result[i, j] >= 1)
                    {
                        result[i + 1, j] += (result[i, j] - 1) / 2.0;
                        result[i + 1, j + 1] += (result[i, j] - 1) / 2.0;
                        result[i, j] = 1;
                    }
                }
            }
            return result[query_row, query_glass];
        }
    }
}