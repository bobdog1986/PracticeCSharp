using System;
using System.Linq;
using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1104. Path In Zigzag Labelled Binary Tree, #BTree
        ///Zigzag sequence: 1-> 3,2 -> 4, 5, 6, 7 -> 15,14....8->...
        public IList<int> PathInZigZagTree(int label)
        {
            var res=new List<int>();
            bool forward = true;
            int levelCount = 1;
            int total = 0;
            total += levelCount;
            while (label > total)
            {
                levelCount <<= 1;
                total += levelCount;
                forward = !forward;
            }

            while (label > 0)
            {
                res.Insert(0, label);
                int curr = label - (total - levelCount);
                int half = (curr-1) / 2;
                if (!forward)
                    half = levelCount / 2 -1- half;

                int nextTotal = (total - levelCount);
                int nextLevel = levelCount / 2;

                //next forward
                forward = !forward;
                total -= levelCount;
                levelCount = nextLevel;
                if(forward)
                    label = nextTotal - nextLevel + (half + 1);
                else
                    label = nextTotal - half ;
            }

            return res;
        }
        /// 1129. Shortest Path with Alternating Colors, #Graph, #BFS
        public int[] ShortestAlternatingPaths(int n, int[][] redEdges, int[][] blueEdges)
        {
            int[] res=new int[n];
            //init graph
            List<int>[] redGraph= new List<int>[n];
            List<int>[] blueGraph = new List<int>[n];
            for(int i=0; i<n; i++)
            {
                redGraph[i]=new List<int>();
                blueGraph[i]=new List<int>();
                if (i != 0)
                    res[i] = -1;
            }

            //not dual-directions
            foreach(var edge in redEdges)
                redGraph[edge[0]].Add(edge[1]);
            foreach (var edge in blueEdges)
                blueGraph[edge[0]].Add(edge[1]);

            //avoid duplicate visit
            bool[] visitFromRedEdge = new bool[n];
            bool[] visitFromBlueEdge = new bool[n];

            //first loop seed data
            var listRed = redGraph[0];
            var listBlue = blueGraph[0];
            foreach (var i in listRed)
                visitFromRedEdge[i] = true;
            foreach (var i in listBlue)
                visitFromBlueEdge[i] = true;

            int level = 1;
            while(listRed.Count>0 || listBlue.Count > 0)
            {
                List<int> nextRed = new List<int>();
                List<int> nextBlue = new List<int>();
                foreach(var red in listRed)
                {
                    if (res[red] == -1)
                        res[red] = level;
                    if (level % 2 == 1)
                    {
                        foreach(var i in blueGraph[red])
                        {
                            if (visitFromBlueEdge[i]) continue;
                            visitFromBlueEdge[i] = true;
                            nextRed.Add(i);
                        }
                    }
                    else
                    {
                        foreach (var i in redGraph[red])
                        {
                            if (visitFromRedEdge[i]) continue;
                            visitFromRedEdge[i] = true;
                            nextRed.Add(i);
                        }
                    }
                }
                foreach (var blue in listBlue)
                {
                    if (res[blue] == -1)
                        res[blue] = level;
                    if (level % 2 == 1)
                    {
                        foreach (var i in redGraph[blue])
                        {
                            if (visitFromRedEdge[i]) continue;
                            visitFromRedEdge[i] = true;
                            nextBlue.Add(i);
                        }
                    }
                    else
                    {
                        foreach (var i in blueGraph[blue])
                        {
                            if (visitFromBlueEdge[i]) continue;
                            visitFromBlueEdge[i] = true;
                            nextBlue.Add(i);
                        }
                    }
                }
                listRed = nextRed;
                listBlue = nextBlue;
                level++;
            }
            return res;
        }

        /// 1137. N-th Tribonacci Number
        ///T0 = 0, T1 = 1, T2 = 1, and Tn+3 = Tn + Tn+1 + Tn+2 for n >= 0.
        public int Tribonacci(int n)
        {
            if (n <= 1)
                return n;
            if (n == 2)
                return 1;
            int a1 = 0;
            int a2 = 1;
            int a3 = 1;
            int dp = 0;
            int i = 3;
            while (i <= n)
            {
                dp = a1 + a2 + a3;
                a1 = a2;
                a2 = a3;
                a3 = dp;
                i++;
            }
            return dp;
        }

        public int Tribonacci_Recursion(int n)
        {
            if (n <= 1)
                return n;
            if (n == 2)
                return 1;
            return Tribonacci_Recursion(n - 3) + Tribonacci_Recursion(n - 2) + Tribonacci_Recursion(n - 1);
        }

        ///1143. Longest Common Subsequence
        ///return the length of their longest common subsequence. If there is no common subsequence, return 0.
        public int LongestCommonSubsequence(string text1, string text2)
        {
            int rowLen=text1.Length;
            int colLen=text2.Length;
            int[,] dp=new int[rowLen+1, colLen + 1];

            for(int i = 0; i < rowLen; i++)
            {
                for(int j=0;j<colLen; j++)
                {
                    if (text1[i] == text2[j])
                    {
                        dp[i + 1,j + 1] = 1 + dp[i,j];
                    }
                    else
                    {
                        dp[i + 1,j + 1] = Math.Max(dp[i,j + 1], dp[i + 1,j]);
                    }
                }
            }

            return dp[rowLen,colLen];
        }

        ///1146. Snapshot Array, see SnapshotArray

    }
}