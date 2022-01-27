using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.AnwserA2000
{
    public partial class Anwser
    {
        ///2022. Convert 1D Array Into 2D Array
        public int[][] Construct2DArray(int[] original, int m, int n)
        {
            if(original.Length!= m*n)
                return new int[0][] { };
            var ans = new int[m][];
            for (int i = 0; i < m*n; i++)
            {
                if (i % n == 0)
                    ans[i / n] = new int[n];
                ans[i / n][i % n] = original[i];
            }
            return ans;
        }
    }
}
