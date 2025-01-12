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

        ///3381. Maximum Subarray Sum With Length Divisible by K, #PrefixSum , #Kadane
        //Return the maximum sum of a subarray of nums, such that the size of the subarray is divisible by k.
        public long MaxSubarraySum(int[] nums, int k)
        {
            int n = nums.Length;
            long[] prefix = new long[n];
            long sum1 = 0;
            for (int i = 0; i<n; i++)
            {
                sum1+=nums[i];
                prefix[i]=sum1;
            }

            long res = long.MinValue;
            for (int i = 0; i<k; i++)
            {
                long sum = 0;
                long max = long.MinValue;
                for (int j = i; j+k-1<n; j+=k)
                {
                    long a = j>0 ? prefix[j-1] : 0;
                    long b = prefix[j+k-1];
                    sum += b-a;
                    max = Math.Max(max, sum);
                    if (sum <= 0) sum = 0;
                }
                res=Math.Max(res, max);
            }
            return res;
        }
    }
}
