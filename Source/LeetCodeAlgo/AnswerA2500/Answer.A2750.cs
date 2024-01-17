using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2767. Partition String Into Minimum Beautiful Substrings, #DP
        //public int MinimumBeautifulSubstrings(string s)
        //{
        //    int n = s.Length;
        //    int[] dp = new int[n+1];
        //    Array.Fill(dp, int.MaxValue);
        //    dp[0]=0;
        //    HashSet<string> set = new HashSet<string>();
        //    int max = 1<<16;
        //    int p = 5;
        //    set.Add("1");
        //    while (p<max)
        //    {
        //        string str = Convert.ToString(p, 2);
        //        set.Add(str);
        //        p*=5;
        //    }

        //    for(int i = 1; i<=n; i++)
        //    {
        //        for(int j = 1; j<=i; j++)
        //        {
        //            string x = s.Substring(i-1-j+1, j);
        //            if (set.Contains(x) && dp[i-j]<int.MaxValue)
        //            {
        //                dp[i]=Math.Min(dp[i], dp[i-j]+1);
        //            }
        //        }
        //    }

        //    return dp[n]==int.MaxValue? -1: dp[n];
        //}

    }
}
