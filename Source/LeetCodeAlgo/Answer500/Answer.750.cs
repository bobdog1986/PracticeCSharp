using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {

        ///763. Partition Labels
        ///s consists of lowercase English letters.
        public IList<int> PartitionLabels(string s)
        {
            if (s.Length == 1)
                return new List<int>() { 1 };

            IList<int> ans = new List<int>();

            Dictionary<char, List<int>> dict = new Dictionary<char, List<int>>();

            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]))
                {
                    dict[s[i]].Add(i);
                }
                else
                {
                    dict.Add(s[i], new List<int>() { i });
                }
            }

            List<char> list = new List<char>();
            int len = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (!list.Contains(s[i]))
                    list.Add(s[i]);

                bool canPartition = !list.Any(c => dict[c].Any(x => x > i));

                if (canPartition)
                {
                    list.Clear();

                    int len2 = i + 1;
                    ans.Add(len2 - len);
                    len = len2;
                }

            }

            return ans;
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

        ///797. All Paths From Source to Target
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
    }
}
