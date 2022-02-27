using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///303. Range Sum Query - Immutable
    public class NumArray
    {
        private Dictionary<int, int> dict;
        private int[] arr;
        public NumArray(int[] nums)
        {
            arr = nums;
            dict=new Dictionary<int, int>();
            int sum = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
                dict.Add(i, sum);
            }
        }

        public int SumRange(int left, int right)
        {
            int sumRight = dict[right];
            int sumLeft = dict[left]-arr[left];
            return sumRight - sumLeft;
        }
    }
}
