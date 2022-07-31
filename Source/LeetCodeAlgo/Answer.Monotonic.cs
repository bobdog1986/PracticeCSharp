using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    //all monotonic impl
    public partial class Answer
    {
        ///prefix Sum array
        private long[] initPrefixSum(int[] nums)
        {
            int n = nums.Length;
            long[] res = new long[n];
            long sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += nums[i];
                res[i] = sum;
            }
            return res;
        }

        //find cloest smaller element on left side and right side
        private int[] initMonotonicLeftSmallerArr(int[] nums)
        {
            int n = nums.Length;
            int[] arr = new int[n];
            int[] res = new int[n];
            int index = -1;
            for (int i = 0; i < n; i++)
            {
                while (index >= 0 && nums[arr[index]] >= nums[i])
                    index--;
                res[i] = index == -1 ? -1 : arr[index];
                arr[++index] = i;
            }
            return res;
        }

        private int[] initMonotonicRightSmallerArr(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];
            int[] arr = new int[n];
            int index = n;
            for (int i = n - 1; i >= 0; i--)
            {
                while (index < n && nums[arr[index]] >= nums[i])
                    index++;
                res[i] = index == n ? n : arr[index];
                arr[--index] = i;
            }
            return res;
        }

        //find cloest bigger element on left side and right side
        private int[] initMonotonicLeftBiggerArr(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];
            int[] arr = new int[n];
            int index = -1;
            for (int i = 0; i < n; i++)
            {
                while (index >= 0 && nums[arr[index]] <= nums[i])
                    index--;
                res[i] = index == -1 ? -1 : arr[index];
                arr[++index] = i;
            }
            return res;
        }

        private int[] initMonotonicRightBiggerArr(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];
            int[] arr = new int[n];
            int index = n;
            for (int i = n - 1; i >= 0; i--)
            {
                while (index < n && nums[arr[index]] <= nums[i])
                    index++;
                res[i] = index == n ? n : arr[index];
                arr[++index] = i;
            }
            return res;
        }
    }
}