using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///3623. Count Number of Trapezoids I, #Prefix Sum
        //public int CountTrapezoids(int[][] points)
        //{
        //    var dict = new Dictionary<int, long>();
        //    foreach (var p in points)
        //    {
        //        if (!dict.ContainsKey(p[1]))
        //            dict.Add(p[1], 0);
        //        dict[p[1]]++;
        //    }
        //    long res = 0;
        //    long mod = 1_000_000_007;
        //    var keys = dict.Keys.Where(x => dict[x]>=2).ToArray();
        //    long sum = keys.Sum(x => dict[x]*(dict[x]-1)/2%mod);
        //    long curr = 0;
        //    for (int i = 0; i<keys.Length; i++)
        //    {
        //        long a = dict[keys[i]]*(dict[keys[i]]-1)/2%mod;
        //        curr+=a;
        //        curr%=mod;
        //        res+= a*(sum+mod-curr);
        //        res%=mod;
        //    }
        //    return (int)(res%mod);
        //}

    }
}
