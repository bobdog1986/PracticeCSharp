using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///752. Open the Lock, #Graph, #BFS
        //The lock initially starts at '0000',deadends dead ends;
        //return the minimum number of turns required to open the lock, or -1 if impossible.
        public int OpenLock(string[] deadends, string target)
        {
            int steps = 0;
            HashSet<string> dead = new HashSet<string>(deadends);
            HashSet<string> visit = new HashSet<string>() { "0000" };

            var queue = new Queue<string>();
            queue.Enqueue("0000");
            while (queue.Count > 0)
            {
                int size = queue.Count;
                while (size-- > 0)
                {
                    var curr = queue.Dequeue();
                    if (curr == target) return steps;
                    if (dead.Contains(curr)) continue;
                    for (int j = 0; j < 4; j++)
                    {
                        var up = curr.ToArray();
                        up[j] = (char)((up[j] - '0' + 1) % 10 + '0');
                        var upKey = new string(up);
                        if (!dead.Contains(upKey) && !visit.Contains(upKey))
                        {
                            queue.Enqueue(upKey);
                            visit.Add(upKey);
                        }

                        var down = curr.ToArray();
                        down[j] = (char)((down[j] - '0' - 1+10) % 10 + '0');
                        var downKey = new string(down);
                        if (!dead.Contains(downKey) && !visit.Contains(downKey))
                        {
                            queue.Enqueue(downKey);
                            visit.Add(downKey);
                        }
                    }
                }
                steps++;
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

        ///766. Toeplitz Matrix
        public bool IsToeplitzMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length - 1; i++)
            {
                for (int j = 0; j < matrix[i].Length - 1; j++)
                {
                    if (matrix[i][j] != matrix[i + 1][j + 1]) return false;
                }
            }
            return true;
        }
        /// 767. Reorganize String
        //rearrange the characters of s so that any two adjacent characters are not the same.
        //Return any possible rearrangement of s or return "" if not possible.
        public string ReorganizeString(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach(var c in s)
            {
                if (dict.ContainsKey(c)) dict[c] ++;
                else dict.Add(c, 1);
            }

            var max = dict.Values.Max();
            if (max > (s.Length + 1) / 2) return "";

            var keys = dict.Keys.OrderBy(x => -dict[x]).ToList();
            char[] res = new char[s.Length];
            int left = 0;
            for(int i = 0; i < s.Length; i += 2)
            {
                res[i] = keys[left];
                if (--dict[keys[left]] == 0) left++;
            }
            for (int i = 1; i < s.Length; i += 2)
            {
                res[i] = keys[left];
                if (--dict[keys[left]] == 0) left++;
            }
            return new string(res);
        }
        /// 771. Jewels and Stones
        public int NumJewelsInStones(string jewels, string stones)
        {
            var set = jewels.ToHashSet();
            return stones.Count(c=>set.Contains(c));
        }

        ///777. Swap Adjacent in LR String, #Two Pointers
        //"XL" with "LX", and "RX"=>"XR"
        public bool CanTransform(string start, string end)
        {
            int n = start.Length;
            int i = 0;
            int j = 0;

            while (i < n || j < n)
            {
                // stop at char that is not 'X'
                while (i < n && start[i] == 'X') { i++; }
                while (j < n && end[j] == 'X') { j++; }

                if (i >= n || j >= n) { break; }

                // relative order for 'R' and 'L' in 2 strings should be the same
                if (start[i] != end[j]) { return false; }
                // R can only move to right
                if (start[i] == 'R' && i > j) { return false; }
                // L can only move to left
                if (start[i] == 'L' && i < j) { return false; }
                // check next
                i++;
                j++;
            }

            return i == j;
        }
        ///778. Swim in Rising Water, #Dijkstra
        //return the min of cost in all path from (0,0) to (n-1,n-1), cost is max cell of a path
        public int SwimInWater(int[][] grid)
        {
            var dp = getDijkstraMaxCell(grid);
            return dp.Last().Last();
        }

        ///779. K-th Symbol in Grammar
        //row[1]=0, row[i] to row[i+1] =>  0 with 01, and 1 with 10. return k(1-idnex) in row[n]
        public int KthGrammar(int n, int k)
        {
            return KthGrammar1(n, k - 1);
        }

        private int KthGrammar1(int n, int k)
        {
            if (n == 1) return 0;
            if (k % 2 == 0) return KthGrammar1(n - 1, k / 2) == 0 ? 0 : 1;
            else return KthGrammar1(n - 1, k / 2) == 0 ? 1 : 0;
        }

        ///781. Rabbits in Forest, #Greedy
        //We asked n rabbits "How many rabbits have the same color as you?" and
        //collected the answers in an integer array answers where answers[i] is the answer of the ith rabbit.
        //Given the array answers, return the minimum number of rabbits that could be in the forest.
        public int NumRabbits(int[] answers)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var n in answers)
            {
                if (dict.ContainsKey(n)) dict[n]++;
                else dict.Add(n, 1);
            }
            int res = 0;
            foreach(var k in dict.Keys)
            {
                res += (k+1)*(int)(Math.Ceiling(1.0 * dict[k] / (k + 1)));
            }
            return res;
        }
        /// 783. Minimum Distance Between BST Nodes
        ///root of a Binary Search Tree (BST), return the minimum difference between any two different nodes
        public int MinDiffInBST(TreeNode root)
        {
            int res=int.MaxValue;
            var list = PreorderTraversal_Iteratively(root);
            var arr= list.OrderBy(x => x).ToList();
            for (int i = 0; i < arr.Count - 1; i++)
                res = Math.Min(res, arr[i + 1] - arr[i]);
            return res;
        }
        /// 784. Letter Case Permutation, ref 77 Combines()
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

        ///789. Escape The Ghosts
        public bool EscapeGhosts(int[][] ghosts, int[] target)
        {
            return !ghosts.Any(g => Math.Abs(g[0] - target[0]) + Math.Abs(g[1] - target[1]) <= Math.Abs(target[0]) + Math.Abs(target[1]));
        }


        ///791. Custom Sort String
        //Permute the characters of s so that they match the order that order was sorted.
        //order = "cba", s = "abcd" => Output: "cbad"
        public string CustomSortString(string order, string s)
        {
            StringBuilder sb = new StringBuilder();
            int n = order.Length;
            Dictionary<char, int> dict = new Dictionary<char, int>();
            for (int i = 0; i < n; i++)
                dict.Add(order[i], i);
            int[] arr = new int[n];
            foreach (var c in s)
            {
                if (dict.ContainsKey(c)) arr[dict[c]]++;
                else sb.Append(c);
            }
            for (int i = 0; i < n; i++)
            {
                while (arr[i]-- > 0)
                    sb.Append(order[i]);
            }
            return sb.ToString();
        }
        ///792. Number of Matching Subsequences, #Binary Search
        //return the number of words[i] that is a subsequence of s.For example, "ace" is a subsequence of "abcde".
        //A subsequence is generated from the original string with some(can be none) deleted without changing the order.
        public int NumMatchingSubseq(string s, string[] words)
        {
            int n = s.Length;
            List<int>[] posArr = new List<int>[26];
            for (int i = 0; i < posArr.Length; i++)
                posArr[i] = new List<int>();
            for(int i = 0; i < n; i++)
                posArr[s[i] - 'a'].Add(i);
            return words.Count(w => isSubSequence_binarySearch(posArr, s, w));
        }
        private bool isSubSequence_binarySearch(List<int>[] posArr, string s, string t)
        {
            int n = s.Length;
            int curr = 0;
            for (int i = 0; i < t.Length; i++)
            {
                if (curr >= n) return false;
                var list = posArr[t[i] - 'a'];
                if (list.Count == 0 || list.Last() < curr) return false;
                int left = 0;
                int right = list.Count - 1;
                while (left < right)
                {
                    int mid = (left + right) / 2;
                    if (list[mid] >= curr)
                        right = mid;
                    else
                        left = mid + 1;
                }
                curr = list[left] + 1;
            }
            return true;
        }

        ///796. Rotate String
        ///Given two strings s and goal, return true if and only if s can become goal after some number of shifts on s.
        public bool RotateString(string s, string goal)
        {
            if (s.Length != goal.Length) return false;
            for(int i = 0; i < s.Length; i++)
            {
                if(s[i] == goal[0])
                {
                    if(goal == s.Substring(i)+s.Substring(0,i))return true;
                }
            }
            return false;
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