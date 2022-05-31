using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///802. Find Eventual Safe States, #DFS, #Graph
        public IList<int> EventualSafeNodes(int[][] graph)
        {
            HashSet<int> map = new HashSet<int>();
            int[] dp = new int[graph.Length];
            bool[] visit = new bool[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                EventualSafeNodes_dfs(graph, i, visit, dp, map);
            }
            return map.OrderBy(x => x).ToList();
        }

        public int EventualSafeNodes_dfs(int[][] graph, int i, bool[] visit, int[] dp, HashSet<int> map)
        {
            if (visit[i])
            {
                return dp[i];
            }
            visit[i] = true;
            int ans = 1;
            foreach (var j in graph[i])
            {
                if (visit[j]) ans &= dp[j];
                else ans &= EventualSafeNodes_dfs(graph, j, visit, dp, map);
            }

            if (ans == 1)
            {
                if (!map.Contains(i)) map.Add(i);
            }
            dp[i] = ans;
            return ans;
        }

        ///804. Unique Morse Code Words
        public int UniqueMorseRepresentations(string[] words)
        {
            string[] morseDict = new string[] { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..",
                ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-",
                ".--", "-..-", "-.--", "--.." };
            var set=new HashSet<string>();
            foreach (var word in words)
            {
                var morseCode = string.Join("", word.Select(x => morseDict[x - 'a']));
                set.Add(morseCode);
            }
            return set.Count;
        }
        /// 807. Max Increase to Keep City Skyline
        public int MaxIncreaseKeepingSkyline(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            int[] rowMax=grid.Select(x=>x.Max()).ToArray();
            int[] colMax=new int[colLen];
            for(int c = 0; c < colLen; c++)
            {
                int max = int.MinValue;
                for (int r = 0; r < rowLen; r++)
                {
                    max = Math.Max(max, grid[r][c]);
                }
                colMax[c] = max;
            }

            int res = 0;
            for(int r = 0; r < rowLen; r++)
            {
                for (int c = 0; c < colLen; c++)
                {
                    int max = Math.Min(rowMax[r], colMax[c]);
                    res += max > grid[r][c] ? max - grid[r][c] : 0;
                }
            }
            return res;
        }
        ///811. Subdomain Visit Count
        public IList<string> SubdomainVisits(string[] cpdomains)
        {
            var res=new List<string>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach(var cpdomain in cpdomains)
            {
                var arr1=cpdomain.Split(' ');
                int time=int.Parse(arr1[0]);
                string str=arr1[1];
                var domains = str.Split('.');
                string curr="";
                for (int i = domains.Length-1; i>=0; i--)
                {
                    curr = domains[i]+curr;
                    if (dict.ContainsKey(curr)) dict[curr] += time;
                    else dict.Add(curr, time);
                    curr = "." + curr;
                }
            }
            foreach(var key in dict.Keys)
            {
                res.Add($"{dict[key]} {key}");
            }
            return res;
        }
        /// 814. Binary Tree Pruning, #BTree
        ///return the same tree where every subtree (of the given tree) not containing a 1 has been removed.
        ///A subtree of a node node is node plus every node that is a descendant of node.
        public TreeNode PruneTree(TreeNode root)
        {
            if (!PruneTree_AnyChildEqualToOne(root)) return null;
            return root;
        }

        private bool PruneTree_AnyChildEqualToOne(TreeNode root)
        {
            if (root == null) return false;
            var leftHasOne = PruneTree_AnyChildEqualToOne(root.left);
            var rightHasOne = PruneTree_AnyChildEqualToOne(root.right);
            if (!leftHasOne) root.left = null;
            if (!rightHasOne) root.right = null;

            if (root.val == 1 || leftHasOne || rightHasOne) return true;
            else return false;
        }

        /// 830. Positions of Large Groups
        ///A group is considered large if it has 3 or more characters.
        ///Return the intervals of every large group sorted in increasing order by start index.
        public IList<IList<int>> LargeGroupPositions(string s)
        {
            var ans = new List<IList<int>>();
            int count = 1;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == s[i - 1])
                {
                    count++;
                }
                else
                {
                    if (count >= 3)
                    {
                        ans.Add(new List<int>() { i - count, i - 1 });
                    }
                    count = 1;
                }
            }
            if (count >= 3)
            {
                ans.Add(new List<int>() { s.Length - count, s.Length - 1 });
            }
            return ans;
        }

        /// 841. Keys and Rooms
        ///Given an array rooms where rooms[i] is the set of keys that you can obtain if you visited room i,
        ///return true if you can visit all the rooms, or false otherwise.
        public bool CanVisitAllRooms(IList<IList<int>> rooms)
        {
            int count = rooms.Count;
            int[] arr = new int[count];
            arr[0] = 1;
            count--;
            List<int> list = new List<int>(rooms[0]);
            while (list.Count > 0 && count > 0)
            {
                List<int> next = new List<int>();
                foreach (var key in list)
                {
                    if (arr[key] == 0)
                    {
                        arr[key] = 1;
                        count--;
                        next.AddRange(rooms[key]);
                    }
                }
                list = next;
            }
            return count == 0;
        }

        /// 844. Backspace String Compare
        ///Given two strings s and t, return true if they are equal when both are typed into empty text editors.
        ///'#' means a backspace character.Note that after backspacing an empty text, the text will continue empty.
        public bool BackspaceCompare(string s, string t)
        {
            var str1 = BackspaceCompare_Get(s);
            var str2 = BackspaceCompare_Get(t);
            return str1 == str2;
        }

        private string BackspaceCompare_Get(string s)
        {
            char[] arr=new char[s.Length];
            int i = 0;
            for(int j = 0; j < s.Length; j++)
            {
                if(s[j] == '#')
                {
                    if(i>0)
                        i--;
                }
                else arr[i++]=s[j];
            }
            return new string(arr.Take(i).ToArray());
        }

        ///847. Shortest Path Visiting All Nodes, #Graph, #BFS
        ///You have an undirected, connected graph of n nodes labeled from 0 to n - 1.
        ///You are given an array graph where graph[i] is a list of all the nodes connected with node i by an edge.
        ///Return the length of the shortest path that visits every node.
        ///You may start and stop at any node, you may revisit nodes multiple times, and you may reuse edges.
        public int ShortestPathLength(int[][] graph)
        {
            int n = graph.Length;
            //n<=12
            //[bitMaskOfVisited, currNode , nodeCountOfVisited]
            Queue<int[]> queue = new Queue<int[]>();
            HashSet<int> set = new HashSet<int>();

            for (int i = 0; i < n; i++)
            {
                int tmp = (1 << i);
                set.Add((tmp << 4) + i);
                queue.Enqueue(new int[] { tmp, i, 1 });
            }

            while (queue.Count > 0)
            {
                var curr = queue.Dequeue();
                if (curr[0] == (1 << n) - 1)
                {
                    return curr[2] - 1;//step = count -1
                }
                else
                {
                    foreach (int neighbor in graph[curr[1]])
                    {
                        int bitMask = curr[0] | (1 << neighbor);
                        if (!set.Add((bitMask << 4) + neighbor)) continue;
                        queue.Enqueue(new int[] { bitMask, neighbor, curr[2] + 1 });
                    }
                }
            }
            return -1;
        }

        /// 848. Shifting Letters, #Prefix Sum
        ///Now for each shifts[i] = x, we want to shift the first i + 1 letters of s, x times.
        ///Return the final string after all such shifts to s are applied.
        public string ShiftingLetters(string s, int[] shifts)
        {
            //cache the total shifts of s[i]
            long[] arr = new long[s.Length];
            //cache the sum from right to left, reduce time complexity from O(n^2) to O(n)
            long sum = 0;
            for (int i = shifts.Length - 1; i >= 0; i--)
            {
                sum += shifts[i];
                arr[i] = sum;
            }
            var carr = s.ToCharArray();
            for (int i = 0; i < carr.Length; i++)
            {
                var val = carr[i] + arr[i] % 26;//mod of 26
                if (val > 'z') val -= 26;
                carr[i] = (char)(val);
            }
            return new string(carr);
        }

        /// 849. Maximize Distance to Closest Person
        public int MaxDistToClosest(int[] seats)
        {
            int max = 1;

            int len = 0;
            for (int i = 0; i < seats.Length; i++)
            {
                if (seats[i] == 0)
                {
                    len++;
                }
                else
                {
                    if (len > 0)
                    {
                        if (len == i)
                        {
                            //continous seats from 0-index
                            max = Math.Max(max, len);
                        }
                        else
                        {
                            max = Math.Max(max, (len + 1) / 2);
                        }

                        len = 0;
                    }
                }
            }

            //continous seats to last-index
            if (len > 0)
                max = Math.Max(max, len);
            return max;
        }
    }
}