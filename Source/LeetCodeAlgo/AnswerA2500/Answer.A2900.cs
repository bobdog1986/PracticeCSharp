using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2918. Minimum Equal Sum of Two Arrays After Replacing Zeros
        //public long MinSum(int[] nums1, int[] nums2)
        //{
        //    long zeros1 = 0;
        //    long zeros2 = 0;
        //    long sum1 = 0;
        //    long sum2 = 0;
        //    foreach (var i in nums1)
        //    {
        //        if (i==0) zeros1++;
        //        sum1+=i;
        //    }
        //    foreach (var i in nums2)
        //    {
        //        if (i==0) zeros2++;
        //        sum2+=i;
        //    }

        //    if (zeros1>0)
        //    {
        //        if (zeros2>0)
        //        {
        //            if (sum1==sum2) return sum1+Math.Max(zeros1, zeros2);
        //            else if (sum1>sum2) return sum1+zeros1;
        //            else return Math.Max(sum1+zeros1, sum2+zeros2);
        //        }
        //        else
        //        {
        //            if (sum1+zeros1>sum2) return -1;
        //            else return sum2;
        //        }
        //    }
        //    else
        //    {
        //        if (zeros2>0)
        //        {
        //            if (sum2+zeros2>sum1) return -1;
        //            else return sum1;
        //        }
        //        else
        //        {
        //            if (sum1==sum2) return sum1;
        //            else return -1;
        //        }
        //    }

        //}

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

        ///2947. Count Beautiful Substrings I
        //public int BeautifulSubstrings(string s, int k)
        //{
        //    HashSet<char> set = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
        //    int n = s.Length;
        //    int res = 0;
        //    for (int i = 0; i<n; i++)
        //    {
        //        int v = 0;
        //        int c = 0;
        //        for (int j = i; j<n; j++)
        //        {
        //            if (set.Contains(s[j])) v++;
        //            else c++;
        //            if (v==c && (v*c%k==0))
        //                res++;
        //        }
        //    }
        //    return res;
        //}
    }
}
