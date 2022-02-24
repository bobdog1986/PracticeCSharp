using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///968. Binary Tree Cameras, #Greedy, #DFS
        ///You are given the root of a binary tree. We install cameras on the tree nodes
        ///where each camera at a node can monitor its parent, itself, and its immediate children.
        ///Return the minimum number of cameras needed to monitor all nodes of the tree.
        public int MinCameraCover(TreeNode root)
        {
            int ans = 0;
            var leftLevel = MinCameraCover_Recursion(root.left, ref ans);
            var rightLevel = MinCameraCover_Recursion(root.right, ref ans);
            if(leftLevel == 0 || rightLevel == 0 || (leftLevel == 2 && rightLevel == 2))
            {
                ans++;
            }
            return ans;
        }

        public int MinCameraCover_Recursion(TreeNode node, ref int ans)
        {
            if(node == null) return 2;
            if(node.left==null && node.right==null) return 0;

            var leftVal = MinCameraCover_Recursion(node.left, ref ans);
            var rightVal = MinCameraCover_Recursion(node.right, ref ans);
            if (leftVal == 0 || rightVal ==0)
            {
                ans++;
                return 1;
            }
            else if(leftVal == 1 || rightVal ==1)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        /// 973. K Closest Points to Origin
        ///return the k closest points to the origin (0, 0).
        public int[][] KClosest(int[][] points, int k)
        {
            Dictionary<int,List<int[]>> dict=new Dictionary<int, List<int[]>>();
            foreach(var p in points)
            {
                var distance = p[0] * p[0] + p[1] * p[1];
                if (dict.ContainsKey(distance))
                {
                    dict[distance].Add(p);
                }
                else
                {
                    dict.Add(distance, new List<int[]>() { p});
                }
            }
            var mat = dict.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            List<int[]> ans = new List<int[]>();
            foreach (var list in mat)
            {
                ans.AddRange(list);
                if (ans.Count == k)
                    break;
            }
            return ans.ToArray();
        }
        ///966. Vowel Spellchecker
        ///Given a wordlist, we want to implement a spellchecker that converts a query word into a correct word.
        public string[] Spellchecker(string[] wordlist, string[] queries)
        {
            var ans=new List<string>();

            Dictionary<char, int> map = new Dictionary<char, int>()
            {
                {'a', 0},{'e', 0},{'i', 0},{'o', 0},{'u', 0},
                {'A', 0}, {'E', 0}, {'I', 0}, {'O', 0}, {'U', 0},
            };

            //Capitalization hashmap
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //Vowel Errors hashmap
            Dictionary<int, Dictionary<string, Dictionary<string, string>>> vowelDict
                = new Dictionary<int, Dictionary<string, Dictionary<string, string>>>();

            foreach (var word in wordlist)
            {
                //store to Capitalization hashmap
                if (!dict.ContainsKey(word))
                    dict.Add(word, word);
                var capWord = word.ToUpper();
                if(!dict.ContainsKey(capWord))
                    dict.Add(capWord, word);

                //two parts, Vowel idnexes and other letters
                List<int> list = new List<int>();
                List<char> others = new List<char>();
                for(int i=0;i< word.Length;i++)
                {
                    if (map.ContainsKey(word[i]))
                        list.Add(i);
                    else
                        others.Add(word[i]);
                }

                if (list.Count > 0)
                {
                    //using Length as 1st level key
                    if (!vowelDict.ContainsKey(word.Length))
                        vowelDict.Add(word.Length,new Dictionary<string, Dictionary<string, string>>());
                    var indexStr = string.Join("_", list);

                    //using indexStr string join "_" as 2nd level key
                    if (!vowelDict[word.Length].ContainsKey(indexStr))
                        vowelDict[word.Length].Add(indexStr, new Dictionary<string, string>());

                    //using otherStr as 3rd level key
                    var otherStr = new string(others.ToArray()).ToUpper();
                    if(!vowelDict[word.Length][indexStr].ContainsKey(otherStr))
                        vowelDict[word.Length][indexStr].Add(otherStr,word);
                }
            }

            foreach(var query in queries)
            {
                if (dict.ContainsKey(query))
                {
                    ans.Add(dict[query]);
                }
                else if (dict.ContainsKey(query.ToUpper()))
                {
                    ans.Add(dict[query.ToUpper()]);
                }
                else
                {
                    List<int> list = new List<int>();
                    List<char> others = new List<char>();
                    for (int i = 0; i < query.Length; i++)
                    {
                        if (map.ContainsKey(query[i]))
                            list.Add(i);
                        else
                            others.Add(query[i]);
                    }
                    if (list.Count >0 && vowelDict.ContainsKey(query.Length))
                    {
                        var indexStr = string.Join("_", list);
                        var otherStr = new string(others.ToArray()).ToUpper();
                        if (vowelDict[query.Length].ContainsKey(indexStr)
                            && vowelDict[query.Length][indexStr].ContainsKey(otherStr))
                        {
                            ans.Add(vowelDict[query.Length][indexStr][otherStr]);
                        }
                        else
                        {
                            ans.Add(string.Empty);
                        }
                    }
                    else
                    {
                        ans.Add(string.Empty);
                    }
                }
            }
            return ans.ToArray();
        }

        /// 997. Find the Town Judge
        ///In a town, there are n people labeled from 1 to n. There is a rumor that one of these people is secretly the town judge.
        ///The town judge trusts nobody.Everybody (except for the town judge) trusts the town judge.
        ///Return the label of the town judge if the town judge exists and can be identified, or return -1 otherwise.
        public int FindJudge(int n, int[][] trust)
        {
            int[] beTrustedArr = new int[n+1];
            int[] trustArr = new int[n+1];
            foreach(var t in trust)
            {
                beTrustedArr[t[1]]++;
                trustArr[t[0]]++;
            }
            for(int i = 1; i <= n; i++)
            {
                if(beTrustedArr[i]==n-1 && trustArr[i]==0)
                    return i;
            }
            return -1;
        }
    }
}
