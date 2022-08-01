using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy
{
    public partial class Easy
    {
        /*
        ///1502. Can Make Arithmetic Progression From Sequence
        ///A sequence of numbers is an arithmetic progression if the difference between any two consecutive elements is same.
        public bool CanMakeArithmeticProgression(int[] arr)
        {
            Array.Sort(arr);
            int diff = arr[1] - arr[0];
            for (int i = 2; i < arr.Length; i++)
                if (arr[i] - arr[i - 1] != diff) return false;
            return true;
        }

        ///1507. Reformat Date
        public string ReformatDate(string date)
        {
            var list = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var arr = date.Split(' ');
            int year = int.Parse(arr[2]);
            int month = (list.IndexOf(arr[1]) + 1);
            int day = int.Parse(arr[0].Substring(0, arr[0].Length - 2));
            return new DateTime(year, month, day).ToString("yyyy-MM-dd");
        }

        /// 1512. Number of Good Pairs
        ///Given an array of integers nums, return the number of good pairs.
        ///A pair(i, j) is called good if nums[i] == nums[j] and i<j.
        ///nums = [1,1,1,1], result =6;
        ///1 <= nums.length <= 100, 1 <= nums[i] <= 100
        public int NumIdenticalPairs(int[] nums)
        {
            if (nums == null || nums.Length <= 1)
                return 0;
            int[] arr = new int[100 + 1];
            foreach (var i in nums)
                arr[i]++;
            return arr.Sum(i => i * (i - 1) / 2);
        }
        */
    }
}
