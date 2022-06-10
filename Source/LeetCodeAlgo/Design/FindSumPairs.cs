using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1865. Finding Pairs With a Certain Sum

    public class FindSumPairs
    {
        private readonly Dictionary<int, int> dict1;
        private readonly Dictionary<int, int> dict2;
        private readonly int[] arr2;
        public FindSumPairs(int[] nums1, int[] nums2)
        {
            arr2 = nums2;
            dict1 = new Dictionary<int, int>();
            dict2 = new Dictionary<int, int>();
            foreach(var n1 in nums1)
            {
                if (dict1.ContainsKey(n1)) dict1[n1]++;
                else dict1.Add(n1, 1);
            }

            foreach (var n2 in nums2)
            {
                if (dict2.ContainsKey(n2)) dict2[n2]++;
                else dict2.Add(n2, 1);
            }
        }

        public void Add(int index, int val)
        {
            dict2[arr2[index]]--;
            arr2[index] += val;
            if (dict2.ContainsKey(arr2[index])) dict2[arr2[index]]++;
            else dict2.Add(arr2[index], 1);
        }

        public int Count(int tot)
        {
            int res = 0;
            foreach(var k in dict1.Keys)
            {
                if (dict2.ContainsKey(tot - k))
                    res += dict1[k] * dict2[tot - k];
            }
            return res;
        }
    }
}
