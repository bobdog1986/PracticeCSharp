using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///3151. Special Array I
        //public bool IsArraySpecial(int[] nums)
        //{
        //    for (int i = 1; i<nums.Length; i++)
        //    {
        //        if (nums[i]%2 == nums[i-1]%2) return false;
        //    }
        //    return true;
        //}

        ///3160. Find the Number of Distinct Colors Among the Balls
        //public int[] QueryResults(int limit, int[][] queries)
        //{
        //    int n = queries.Length;
        //    int[] res = new int[n];
        //    var ballDict = new Dictionary<int, int>();
        //    var colorDict = new Dictionary<int, int>();
        //    for (int i = 0; i<n; i++)
        //    {
        //        if (ballDict.ContainsKey(queries[i][0]))
        //        {
        //            colorDict[ballDict[queries[i][0]]]--;
        //            if (colorDict[ballDict[queries[i][0]]]==0)
        //                colorDict.Remove(ballDict[queries[i][0]]);
        //            ballDict[queries[i][0]]=queries[i][1];
        //        }
        //        else
        //        {
        //            ballDict.Add(queries[i][0], queries[i][1]);
        //        }
        //        if (!colorDict.ContainsKey(queries[i][1]))
        //            colorDict.Add(queries[i][1], 0);
        //        colorDict[queries[i][1]]++;
        //        res[i] = colorDict.Keys.Count();
        //    }

        //    return res;
        //}
    }
}