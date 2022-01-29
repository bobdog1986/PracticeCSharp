using System.Collections.Generic;
using System;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///2001. Number of Pairs of Interchangeable Rectangles
        public long InterchangeableRectangles(int[][] rectangles)
        {
            long sum = 0;
            Dictionary<string, long> pairs = new Dictionary<string, long>();
            foreach (var rect in rectangles)
            {
                var gcb = Gcb(rect[0], rect[1]);
                var key = rect[0] / gcb + ":" + rect[1] / gcb;
                if (pairs.ContainsKey(key))
                    pairs[key]++;
                else
                    pairs.Add(key, 1);
            }
            foreach (var pair in pairs)
            {
                if (pair.Value > 1)
                {
                    sum += pair.Value * (pair.Value - 1) / 2;
                }
            }
            return sum;
        }

        ///2006. Count Number of Pairs With Absolute Difference K
        ///return the number of pairs (i, j) where i < j such that |nums[i] - nums[j]| == k.
        ///1 <= k <= 99, 1 <= nums[i] <= 100, 1 <= nums.length <= 200
        public int CountKDifference(int[] nums, int k)
        {
            if (nums.Length <= 1)
                return 0;
            int[] arr=new int[101];
            int start = 100;
            int end = 1;
            foreach(var num in nums)
            {
                arr[num]++;
                start = Math.Min(start, num);
                end= Math.Max(end, num);
            }
            int ans = 0;
            for (int i = start; i <= end - k; i++)
            {
                if(arr[i] > 0 && arr[i + k] > 0)
                {
                    ans += arr[i] * arr[i + k];
                }
            }
            return ans;
        }

        ///2022. Convert 1D Array Into 2D Array
        public int[][] Construct2DArray(int[] original, int m, int n)
        {
            if (original.Length != m * n)
                return new int[0][] { };
            var ans = new int[m][];
            for (int i = 0; i < m * n; i++)
            {
                if (i % n == 0)
                    ans[i / n] = new int[n];
                ans[i / n][i % n] = original[i];
            }
            return ans;
        }
    }
}