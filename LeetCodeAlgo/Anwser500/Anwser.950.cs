using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///973. K Closest Points to Origin
        ///return the k closest points to the origin (0, 0).
        public int[][] KClosest(int[][] points, int k)
        {
            Dictionary<int,List<int[]>> dict=new Dictionary<int, List<int[]>>();
            foreach(var p in points)
            {
                var distance = p[0] * p[0] + p[1] * p[1];
                if (dict.ContainsKey(distance))
                {
                    dict[distance].Add(p);
                }
                else
                {
                    dict.Add(distance, new List<int[]>() { p});
                }
            }
            var mat = dict.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            List<int[]> ans = new List<int[]>();
            foreach (var list in mat)
            {
                ans.AddRange(list);
                if (ans.Count == k)
                    break;
            }
            return ans.ToArray();
        }
        /// 997. Find the Town Judge
        ///In a town, there are n people labeled from 1 to n. There is a rumor that one of these people is secretly the town judge.
        ///The town judge trusts nobody.Everybody (except for the town judge) trusts the town judge.
        ///Return the label of the town judge if the town judge exists and can be identified, or return -1 otherwise.
        public int FindJudge(int n, int[][] trust)
        {
            int[] beTrustedArr = new int[n+1];
            int[] trustArr = new int[n+1];
            foreach(var t in trust)
            {
                beTrustedArr[t[1]]++;
                trustArr[t[0]]++;
            }
            for(int i = 1; i <= n; i++)
            {
                if(beTrustedArr[i]==n-1 && trustArr[i]==0)
                    return i;
            }
            return -1;
        }
    }
}
