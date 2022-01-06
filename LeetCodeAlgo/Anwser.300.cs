using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //344. Reverse String
        public void ReverseString(char[] s)
        {
            if (s == null || s.Length <= 1)
                return;

            int half=s.Length/2;
            char temp;
            for (int i = 0; i < half; i++)
            {
                temp = s[i];
                s[i] = s[s.Length - 1 - i];
                s[s.Length - 1 - i] = temp;
            }

        }
        //350. Intersection of Two Arrays II
        public int[] Intersect(int[] nums1, int[] nums2)
        {
            Array.Sort(nums1);
            Array.Sort(nums2);

            if(nums1.Length < nums2.Length)
            {
                var temp = nums1;
                nums1 = nums2;
                nums2 = temp;
            }


            List<int> result = new List<int>();
            int i=0;
            int j = 0;
            while(i<nums1.Length && j<nums2.Length)
            {
                if(nums1[i] == nums2[j])
                {
                    result.Add(nums1[i]);
                    i++;
                    j++;
                }
                else if (nums1[i] > nums2[j])
                {
                    j++;
                }
                else
                {
                    i++;
                }
            }


            return result.ToArray();
        }
        //367 not pass
        public bool IsPerfectSquare(int num)
        {
            //leetcode pass anwser
            int i = 1;
            while (num > 0)
            {
                num -= i;
                i += 2;
            }
            return num == 0;
        }


    }
}
