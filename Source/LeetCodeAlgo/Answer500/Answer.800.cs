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
            int[] dp=new int[graph.Length];
            bool[] visit = new bool[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                EventualSafeNodes_dfs(graph, i, visit, dp, map);
            }

            return map.OrderBy(x=>x).ToList();
        }

        public int EventualSafeNodes_dfs(int[][] graph,int i,bool[] visit, int[] dp, HashSet<int> map)
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

            if (ans==1)
            {
                if (!map.Contains(i)) map.Add(i);
            }
            dp[i] = ans;
            return ans;
        }
        /// 830. Positions of Large Groups
        ///A group is considered large if it has 3 or more characters.
        ///Return the intervals of every large group sorted in increasing order by start index.
        public IList<IList<int>> LargeGroupPositions(string s)
        {
            var ans=new List<IList<int>>();
            int count = 1;
            for(int i=1; i<s.Length; i++)
            {
                if (s[i] == s[i - 1])
                {
                    count++;
                }
                else
                {
                    if (count >= 3)
                    {
                        ans.Add(new List<int>() { i-count,i-1});
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
            int[] arr=new int[count];
            arr[0] = 1;
            count--;
            List<int> list=new List<int>(rooms[0]);
            while(list.Count > 0 && count>0)
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
            return count==0;
        }
        /// 844. Backspace String Compare
        ///Given two strings s and t, return true if they are equal when both are typed into empty text editors.
        ///'#' means a backspace character.Note that after backspacing an empty text, the text will continue empty.
        public bool BackspaceCompare(string s, string t)
        {
            var arr1 = s.ToArray();
            var arr2 = t.ToArray();

            Stack<char> stack1 = new Stack<char>();
            Stack<char> stack2 = new Stack<char>();

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == '#')
                {
                    if (stack1.Count > 0)
                        stack1.Pop();
                }
                else
                {
                    stack1.Push(arr1[i]);
                }
            }

            for (int j = 0; j < arr2.Length; j++)
            {
                if (arr2[j] == '#')
                {
                    if (stack2.Count > 0)
                        stack2.Pop();
                }
                else
                {
                    stack2.Push(arr2[j]);
                }
            }

            if (stack1.Count != stack2.Count)
                return false;
            int count = stack1.Count;
            for (int i = 0; i < count; i++)
            {
                if (stack1.Pop() != stack2.Pop())
                    return false;
            }

            return true;

        }
        ///848. Shifting Letters, #Prefix Sum
        ///Now for each shifts[i] = x, we want to shift the first i + 1 letters of s, x times.
        ///Return the final string after all such shifts to s are applied.
        public string ShiftingLetters(string s, int[] shifts)
        {
            //cache the total shifts of s[i]
            long[] arr=new long[s.Length];
            //cache the sum from right to left, reduce time complexity from O(n^2) to O(n)
            long sum = 0;
            for(int i = shifts.Length-1; i >=0; i--)
            {
                sum += shifts[i];
                arr[i] = sum;
            }
            var carr = s.ToCharArray();
            for(int i=0;i< carr.Length; i++)
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