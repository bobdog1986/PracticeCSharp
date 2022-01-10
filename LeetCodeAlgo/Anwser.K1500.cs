using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    //1567. Maximum Length of Subarray With Positive Product

    public partial class Anwser
    {
        public int GetMaxLen(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0]>0?1:0;

            int max = 0;

            List<int> list = new List<int>();
            List<int> indexsOfNegative = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    if (list.Count == 0)
                        continue;

                    max = Math.Max(max, GetMaxLen(list, indexsOfNegative));

                    list.Clear();
                    indexsOfNegative.Clear();
                }
                else
                {
                    list.Add(nums[i]);
                    if (nums[i] < 0)
                    {
                        indexsOfNegative.Add(list.Count - 1);
                    }
                }
            }

            max = Math.Max(max, GetMaxLen(list, indexsOfNegative));

            return max;
        }

        public int GetMaxLen(List<int> list, List<int> indexsOfNegative = null)
        {
            if (list.Count ==0)
                return 0;

            if (list.Count == 1)
                return list[0] > 0 ? 1 : 0;

            if (indexsOfNegative == null || indexsOfNegative.Count % 2 == 0)
            {
                return list.Count;
            }
            else
            {
                return Math.Max(list.Count - indexsOfNegative[0]-1, indexsOfNegative[indexsOfNegative.Count - 1]);
            }
        }
    }
}
