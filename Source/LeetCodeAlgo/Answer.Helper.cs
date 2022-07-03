using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        private int getFibonacci(int n)
        {
            if (n <= 1)
                return n;
            int a = 0;
            int dp = 1;
            int temp = 0;
            while (n-- > 1)
            {
                temp = dp;
                dp += a;
                a = temp;
            }
            return dp;
        }

        //Greatest Common Divisor
        private long getGCD_Long(long x, long y)
        {
            return y == 0 ? x : getGCD_Long(y, x % y);
        }

        private int getGCD(int x, int y)
        {
            return y == 0 ? x : getGCD(y, x % y);
        }

        //Least Common Multiple
        private long getLCM_Long(long x, long y)
        {
            return (x * y) / getGCD_Long(x, y);
        }

        private int getLCM(int x, int y)
        {
            return (int)(((long)x * y) / getGCD(x, y));
        }

        private long getFactorial(long n)
        {
            if (n == 0 || n == 1) return 1;
            long r = 1;
            while (n >= 1)
            {
                r *= n;
                n--;
            }
            return r;
        }

        private long getFactorial(long n, long count)
        {
            long r = 1;
            while (count > 0 && n > 0)
            {
                r *= n;
                n--;
                count--;
            }
            return r;
        }

        private long getCombines(long n, long count)
        {
            return getFactorial(n, count) / getFactorial(count);
        }

        private bool[][] initVisitMatrix(int m,int n)
        {
            var visit=new bool[m][];
            for (int i = 0; i < m; i++)
                visit[i] = new bool[n];
            return visit;
        }

        private bool[][][] initVisitMatrix(int m, int n, int k)
        {
            var visit = new bool[m][][];
            for (int i = 0; i < m; i++)
            {
                visit[i] = new bool[n][];
                for (int j = 0; j < n; j++)
                    visit[i][j] = new bool[k];
            }
            return visit;
        }

        private List<int>[] initListGraph(int n)
        {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();
            return graph;
        }

        private int[][] init2DMatrix(int m, int n, int seed=0)
        {
            var dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n];
                if(seed!=0)
                    Array.Fill(dp[i], seed);
            }
            return dp;
        }

        private int[][][] init3DMatrix(int m, int n,int k, int seed=0)
        {
            var dp = new int[m][][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n][];
                for(int j = 0; j < n; j++)
                {
                    dp[i][j] = new int[k];
                    if(seed!=0)
                        Array.Fill(dp[i][j], seed);
                }
            }
            return dp;
        }

        private long[][] init2DLongMatrix(int m, int n,  long seed = 0)
        {
            var dp = new long[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new long[n];
                if (seed != 0)
                    Array.Fill(dp[i], seed);
            }
            return dp;
        }

        private long[][][] init3DLongMatrix(int m, int n, int k, long seed = 0)
        {
            var dp = new long[m][][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new long[n][];
                for (long j = 0; j < n; j++)
                {
                    dp[i][j] = new long[k];
                    if (seed != 0)
                        Array.Fill(dp[i][j], seed);
                }
            }
            return dp;
        }

        private int[][] initDxy4()
        {
            return new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
        }

        private int[][] initDxy8()
        {
            return new int[8][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 },
                                    new int[] { 1, 1 }, new int[] { -1,-1 }, new int[] { -1, 1 }, new int[] { 1, -1 }};
        }

        public TrieItem initTrieRoot(IEnumerable<string> words)
        {
            var root = new TrieItem();
            insertToTrie(root, words);
            return root;
        }

        public void insertToTrie(TrieItem root, IEnumerable<string> words)
        {
            foreach(var word in words)
            {
                insertToTrie(root, word);
            }
        }

        public void insertToTrie(TrieItem root, string word)
        {
            var curr = root;
            foreach (var c in word)
            {
                if (!curr.dict.ContainsKey(c))
                    curr.dict.Add(c, new TrieItem());
                curr = curr.dict[c];
            }
            curr.word = word;
            curr.exist = true;
        }

        public bool searchPrefixInTrie(TrieItem root, string word)
        {
            var curr = root;
            foreach (var c in word)
            {
                if (!curr.dict.ContainsKey(c)) return false;
                curr = curr.dict[c];
            }
            return true;
        }

        public bool searchWholeWordInTrie(TrieItem root, string word)
        {
            var curr = root;
            foreach (var c in word)
            {
                if (!curr.dict.ContainsKey(c)) return false;
                curr = curr.dict[c];
            }
            return curr.exist||word==curr.word;
        }

    }
}