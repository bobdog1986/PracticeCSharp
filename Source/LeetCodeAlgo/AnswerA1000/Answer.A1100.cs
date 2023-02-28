using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1103. Distribute Candies to People
        public int[] DistributeCandies(int candies, int num_people)
        {
            int[] res = new int[num_people];
            int index = 0;
            int seed = 1;
            while (candies > 0)
            {
                int curr = Math.Min(seed++, candies);
                res[index++] += curr;
                index %= num_people;
                candies -= curr;
            }
            return res;
        }
        /// 1104. Path In Zigzag Labelled Binary Tree, #BTree
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
        ///1108. Defanging an IP Address
        public string DefangIPaddr(string address)
        {
            return address.Replace(".", "[.]");
        }

        ///1109. Corporate Flight Bookings
        //There are n flights that are labeled from 1 to n.
        //bookings[i] = [firsti, lasti, seatsi] ,flights range [firsti, lasti] with seatsi seats reserved
        //Return an array answer of length n, where answer[i] is the total number of seats reserved for flight i.
        public int[] CorpFlightBookings_Lee215_SweepLine(int[][] bookings, int n)
        {
            int[] res = new int[n];
            foreach (var book in bookings)
            {
                res[book[0] - 1] += book[2];
                if (book[1] < n) res[book[1]] -= book[2];
            }
            for (int i = 1; i < n; ++i)
                res[i] += res[i - 1];
            return res;
        }

        ///1122. Relative Sort Array
        //Given two arrays arr1 and arr2, the elements of arr2 are distinct, and all elements in arr2 are also in arr1.
        //Sort the elements of arr1 such that the relative ordering of items in arr1 are the same as in arr2.
        //Elements that do not appear in arr2 should be placed at the end of arr1 in ascending order.
        public int[] RelativeSortArray(int[] arr1, int[] arr2)
        {
            var set = arr2.ToHashSet();
            var tails=new List<int>();
            var dict = new Dictionary<int, int>();
            foreach(var n in arr1)
            {
                if (set.Contains(n))
                {
                    if (dict.ContainsKey(n)) dict[n]++;
                    else dict.Add(n, 1);
                }
                else tails.Add(n);
            }
            int i = 0;
            int[] res = new int[arr1.Length];
            foreach(var n in arr2)
            {
                while (dict[n]-->0)
                    res[i++] = n;
            }
            tails = tails.OrderBy(x => x).ToList();
            foreach (var n in tails)
                res[i++] = n;
            return res;
        }

        /// 1124. Longest Well-Performing Interval , #HashMap
        //>8 is tiring day, well-performing interval is tiring days > non-tiring days
        //Return the length of the longest well-performing interval.
        public int LongestWPI(int[] hours)
        {
            int res = 0;
            int count = 0;//we need reset count ,but store count-1 to dict
            //so index of (count-1)(exclusive) to current index(inclusive) is 1 more 8 hour-day
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for(int i = 0; i < hours.Length; i++)
            {
                count += hours[i] > 8 ? 1 : -1;
                if (count > 0) res = i + 1;
                else
                {
                    if(!dict.ContainsKey(count))dict.Add(count, i);
                    if (dict.ContainsKey(count - 1))
                        res = Math.Max(res, i - dict[count - 1]);
                }
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

        ///1131. Maximum of Absolute Value Expression
        //return the maximum value of: |arr1[i] - arr1[j]| + |arr2[i] - arr2[j]| + |i - j|
        public int MaxAbsValExpr(int[] arr1, int[] arr2)
        {
            int n = arr1.Length;
            int max1 = int.MinValue, max2 = int.MinValue, max3 = int.MinValue, max4 = int.MinValue;
            int min1 = int.MaxValue, min2 = int.MaxValue, min3 = int.MaxValue, min4 = int.MaxValue;
            for (int i = 0; i < n; i++)
            {
                // 1st scenario arr1[i] + arr2[i] + i
                max1 = Math.Max(arr1[i] + arr2[i] + i, max1);
                min1 = Math.Min(arr1[i] + arr2[i] + i, min1);
                // 2nd scenario arr1[i] + arr2[i] - i
                max2 = Math.Max(arr1[i] + arr2[i] - i, max2);
                min2 = Math.Min(arr1[i] + arr2[i] - i, min2);
                // 3rd scenario arr1[i] - arr2[i] - i
                max3 = Math.Max(arr1[i] - arr2[i] - i, max3);
                min3 = Math.Min(arr1[i] - arr2[i] - i, min3);
                // 4th scenario arr1[i] - arr2[i] + i
                max4 = Math.Max(arr1[i] - arr2[i] + i, max4);
                min4 = Math.Min(arr1[i] - arr2[i] + i, min4);
            }
            int diff1 = max1 - min1;
            int diff2 = max2 - min2;
            int diff3 = max3 - min3;
            int diff4 = max4 - min4;
            return Math.Max(Math.Max(diff1, diff2), Math.Max(diff3, diff4));
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


        ///1138. Alphabet Board Path
        public string AlphabetBoardPath(string target)
        {
            var sb = new StringBuilder();
            int row = 0;
            int col = 0;
            foreach(var t in target)
            {
                int r = (t - 'a') / 5;
                int c = (t - 'a') % 5;

                if (t == 'z')
                {
                    if (c > col)
                    {
                        while (col < c)
                        {
                            sb.Append('R');
                            col++;
                        }
                    }
                    else if (c < col)
                    {
                        while (col > c)
                        {
                            sb.Append('L');
                            col--;
                        }
                    }

                    if (r > row)
                    {
                        while (row < r)
                        {
                            sb.Append('D');
                            row++;
                        }
                    }
                    else if (r < row)
                    {
                        while (row > r)
                        {
                            sb.Append('U');
                            row--;
                        }
                    }
                }
                else
                {
                    if (r > row)
                    {
                        while (row < r)
                        {
                            sb.Append('D');
                            row++;
                        }
                    }
                    else if (r < row)
                    {
                        while (row > r)
                        {
                            sb.Append('U');
                            row--;
                        }
                    }

                    if (c > col)
                    {
                        while (col < c)
                        {
                            sb.Append('R');
                            col++;
                        }
                    }
                    else if (c < col)
                    {
                        while (col > c)
                        {
                            sb.Append('L');
                            col--;
                        }
                    }

                }

                sb.Append('!');
            }
            return sb.ToString();
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

        ///1145. Binary Tree Coloring Game, #Good
        //Split tree to two parts, the one without x has more nodes than x-part tree
        public bool BtreeGameWinningMove(TreeNode root, int n, int x) {
            int max=0;
            BtreeGameWinningMove(root,n,x,ref max);
            return max>n-max;
        }

        private int BtreeGameWinningMove(TreeNode root,int n, int x, ref int max){
            if(root == null) return 0;
            int left = BtreeGameWinningMove(root.left,n,x, ref max);
            int right = BtreeGameWinningMove(root.right,n,x, ref max);
            if(root.val==x){
                max = Math.Max(max,left);
                max = Math.Max(max,right);
                max= Math.Max(max, n-(left+right+1));
            }
            return left+right+1;
        }

        ///1146. Snapshot Array, see SnapshotArray

    }
}