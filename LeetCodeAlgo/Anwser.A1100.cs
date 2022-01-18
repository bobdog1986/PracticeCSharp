using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

﻿namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //1137. N-th Tribonacci Number
        public int Tribonacci(int n)
        {
            if (n == 0)
            {
                return 0;
            }

            if (n == 1)
            {
                return 1;
            }

            return TribSum(n);
        }

        public int TribSum(int n)
        {
            int[] dp = new int[n + 1];
            dp[0] = 0;
            dp[1] = 1;
            dp[2] = 1;
            for (int i = 3; i <= n; i++)
                dp[i] = dp[i - 1] + dp[i - 2] + dp[i - 3];
            return dp[n];
        }
    }
}