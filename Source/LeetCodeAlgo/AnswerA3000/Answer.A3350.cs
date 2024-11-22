using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///3354. Make Array Elements Equal to Zero
        public int CountValidSelections(int[] nums)
        {
            int res = 0;
            int total = nums.Sum();
            int curr = 0;
            for (int i = 0; i<nums.Length; i++)
            {
                curr+=nums[i];
                if (nums[i]==0)
                {
                    if (curr*2 == total)
                    {
                        res+=2;
                    }
                    else if (curr*2 == total+1 ||curr*2 == total-1)
                    {
                        res++;
                    }
                }
            }
            return res;
        }
    }
}
