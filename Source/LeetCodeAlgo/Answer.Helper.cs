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
        private long GCD_Long(long x, long y)
        {
            return y == 0 ? x : GCD_Long(y, x % y);
        }

        private int GCD(int x, int y)
        {
            return y == 0 ? x : GCD(y, x % y);
        }

        //Least Common Multiple
        private long LCM_Long(long x, long y)
        {
            return (x * y) / GCD_Long(x, y);
        }

        private int LCM(int x, int y)
        {
            return (int)(((long)x * y) / GCD(x, y));
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

        private bool[][] createVisit(int m,int n)
        {
            var visit=new bool[m][];
            for (int i = 0; i < m; i++)
                visit[i] = new bool[n];
            return visit;
        }
        private List<int>[] createListGraph(int n)
        {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();
            return graph;
        }

        private int[][] createIntMatrix(int m,int n)
        {
            var dp = new int[m][];
            for (int i = 0; i < m; i++)
                dp[i] = new int[n];
            return dp;
        }

        private int[][] createIntMatrix(int m, int n, int seed)
        {
            var dp = new int[m][];
            for (int i = 0; i < m; i++)
            {
                dp[i] = new int[n];
                Array.Fill(dp[i], seed);
            }
            return dp;
        }

        private int[][] createDxy4()
        {
            return new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
        }

        private int[][] createDxy8()
        {
            return new int[8][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 },
                                    new int[] { 1, 1 }, new int[] { -1,-1 }, new int[] { -1, 1 }, new int[] { 1, -1 }};
        }

    }
}