using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2938. Separate Black and White Balls
        //public long MinimumSteps(string s)
        //{
        //    int ones = 0;
        //    long res = 0;
        //    foreach (var i in s)
        //    {
        //        if (i=='0')
        //        {
        //            res+=ones;
        //        }
        //        else ones++;
        //    }
        //    return res;
        //}

        ///2944. Minimum Number of Coins for Fruits, #DP
        //public int MinimumCoins(int[] prices)
        //{
        //    int n = prices.Length;
        //    int[] dp = new int[n+1];
        //    Array.Fill(dp, int.MaxValue);
        //    dp[0]=0;
        //    for (int i = 1; i<=n; i++)
        //    {
        //        for (int j = i; j<=i*2 && j<=n; j++)
        //        {
        //            dp[j] = Math.Min(dp[j], dp[i-1]+prices[i-1]);
        //        }
        //    }

        //    return dp[n];
        //}
    }
}
