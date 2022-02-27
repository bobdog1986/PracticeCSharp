using System;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1512. Number of Good Pairs
        ///Given an array of integers nums, return the number of good pairs.
        ///A pair(i, j) is called good if nums[i] == nums[j] and i<j.
        ///nums = [1,1,1,1], result =6;
        ///1 <= nums.length <= 100
        ///1 <= nums[i] <= 100
        public int NumIdenticalPairs(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            int result = 0;

            int[] arr = new int[100 + 1];
            foreach (var i in nums)
                arr[i]++;

            foreach (var i in arr)
            {
                if (i > 1)
                {
                    result += i * (i - 1) / 2;
                }
            }

            return result;
        }

        ///1567. Maximum Length of Subarray With Positive Product
        public int GetMaxLen(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0] > 0 ? 1 : 0;

            int max = 0;

            int count = 0;
            int negCount = 0;
            int negStart = -1;
            int negEnd = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    if (count <= max)
                    {
                    }
                    else
                    {
                        if (negCount % 2 == 0)
                        {
                            max = count;
                        }
                        else
                        {
                            var temp = Math.Max(Math.Max(count - negStart - 1, negStart), Math.Max(count - negEnd - 1, negEnd));
                            max = Math.Max(max, temp);
                        }
                    }

                    count = 0;
                    negCount = 0;
                    negStart = -1;
                    negEnd = 0;
                }
                else
                {
                    if (nums[i] > 0)
                    {
                    }
                    else
                    {
                        if (negStart == -1)
                            negStart = count;

                        negEnd = count;

                        negCount++;
                    }
                    count++;
                }
            }

            if (negCount % 2 == 0)
            {
                max = Math.Max(max, count);
            }
            else
            {
                var temp = Math.Max(Math.Max(count - negStart - 1, negStart), Math.Max(count - negEnd - 1, negEnd));

                max = Math.Max(max, temp);
            }
            return max;
        }
    }
}