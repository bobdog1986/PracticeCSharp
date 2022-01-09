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
        //383. Ransom Note
        public bool CanConstruct(string ransomNote, string magazine)
        {
            var arr1= ransomNote.ToArray().ToList();
            var arr2= magazine.ToArray().ToList();
            while (arr1.Count>0)
            {
                var j = arr2.IndexOf(arr1[0]);
                if (j>= 0)
                {
                    arr1.RemoveAt(0);
                    arr2.RemoveAt(j);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }



        //387. First Unique Character in a String

        public int FirstUniqChar(string s)
        {
            Dictionary<char,int> dic=new Dictionary<char, int>();


            for(int i=0; i<s.Length; i++)
            {
                if (dic.ContainsKey(s[i]))
                {
                    dic[s[i]] = -1;
                }
                else
                {
                    dic.Add(s[i], i);
                }
            }

            foreach(var i in dic.Values)
            {
                if (i != -1)
                    return i;
            }

            return -1;
        }

    }
}
