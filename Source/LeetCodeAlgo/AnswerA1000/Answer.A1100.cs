﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1137. N-th Tribonacci Number
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