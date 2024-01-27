using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2870. Minimum Number of Operations to Make Array Empty
        //public int MinOperations_2870(int[] nums)
        //{
        //    var dict = new Dictionary<int, int>();
        //    foreach (var i in nums)
        //    {
        //        if (dict.ContainsKey(i)) dict[i]++;
        //        else dict.Add(i, 1);
        //    }
        //    int res = 0;
        //    foreach (var k in dict.Keys)
        //    {
        //        if (dict[k]==1)
        //            return -1;
        //        else
        //        {
        //            if (dict[k]%3==0)
        //                res+=dict[k]/3;
        //            else
        //                res+=dict[k]/3+1;
        //        }
        //    }
        //    return res;
        //}

        ///2895. Minimum Processing Time
        //public int MinProcessingTime(IList<int> processorTime, IList<int> tasks)
        //{
        //    processorTime = processorTime.OrderBy(x=>x).ToList();
        //    tasks = tasks.OrderBy(x=>-x).ToList();
        //    int n = processorTime.Count;
        //    int res = 0;
        //    for(int i=0;i<n; i++)
        //    {
        //        int max = 0;
        //        for(int j = i*4; j<i*4+4; j++)
        //        {
        //            max=Math.Max(max, tasks[j]);
        //        }
        //        res= Math.Max(res, processorTime[i]+max);
        //    }
        //    return res;
        //}

    }
}
