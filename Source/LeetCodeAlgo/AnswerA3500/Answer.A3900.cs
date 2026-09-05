using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///3903, Smallest Stable Index I
        ///3904, Smallest Stable Index II
        //public int FirstStableIndex(int[] nums, int k)
        //{
        //    int res = int.MaxValue;
        //    int n = nums.Length;
        //    int[] arrMax = new int[n];
        //    int[] arrMin = new int[n];
        //    int max = int.MinValue;
        //    for (int i = 0; i<n; i++)
        //    {
        //        max=Math.Max(max, nums[i]);
        //        arrMax[i]=max;
        //    }
        //    int min = int.MaxValue;
        //    for (int i = n-1; i>=0; i--)
        //    {
        //        min=Math.Min(min, nums[i]);
        //        arrMin[i]=min;
        //    }

        //    for (int i = 0; i<n; i++)
        //    {
        //        int score = arrMax[i]-arrMin[i];
        //        if (score<=k)
        //        {
        //            return i;
        //        }
        //    }

        //    return res == int.MaxValue ? -1 : res;
        //}

    }
}
