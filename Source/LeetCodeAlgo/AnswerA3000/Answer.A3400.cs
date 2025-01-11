using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///3402. Minimum Operations to Make Columns Strictly Increasing
        //public int MinimumOperations(int[][] grid)
        //{
        //    int res = 0;
        //    int m = grid.Length;
        //    int n = grid[0].Length;
        //    for (int i = 0; i<n; i++)
        //    {
        //        int prev = -1;
        //        for (int j = 0; j<m; j++)
        //        {
        //            if (grid[j][i]>prev)
        //            {
        //                prev = grid[j][i];
        //            }
        //            else
        //            {
        //                res+= prev+1 - grid[j][i];
        //                prev++;
        //            }
        //        }
        //    }
        //    return res;
        //}

        ///3412. Find Mirror Score of a String
        //public long CalculateScore(string s)
        //{
        //    var dict = new Dictionary<char, Stack<int>>();
        //    long res = 0;
        //    for (int i = 0; i<s.Length; i++)
        //    {
        //        var c = s[i];
        //        char r = (char)('a'+'z'-c);
        //        if (dict.ContainsKey(r)&&dict[r].Count>0)
        //        {
        //            res+= i-dict[r].Pop();
        //        }
        //        else
        //        {
        //            if (!dict.ContainsKey(c)) dict.Add(c, new Stack<int>());
        //            dict[c].Push(i);
        //        }
        //    }
        //    return res;
        //}

    }
}
