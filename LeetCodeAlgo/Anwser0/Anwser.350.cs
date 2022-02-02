using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Anwser0
{
    public partial class Anwser
    {
        //350. Intersection of Two Arrays II
        public int[] Intersect(int[] nums1, int[] nums2)
        {
            Array.Sort(nums1);
            Array.Sort(nums2);

            if (nums1.Length < nums2.Length)
            {
                var temp = nums1;
                nums1 = nums2;
                nums2 = temp;
            }

            List<int> result = new List<int>();
            int i = 0;
            int j = 0;
            while (i < nums1.Length && j < nums2.Length)
            {
                if (nums1[i] == nums2[j])
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

        ///380. Insert Delete GetRandom O(1), see RandomizedSet

        /// 383. Ransom Note
        public bool CanConstruct(string ransomNote, string magazine)
        {
            var arr1 = ransomNote.ToArray().ToList();
            var arr2 = magazine.ToArray().ToList();
            while (arr1.Count > 0)
            {
                var j = arr2.IndexOf(arr1[0]);
                if (j >= 0)
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

        ///384. Shuffle an Array, see Solution_384_Shuffle

        /// 387. First Unique Character in a String

        public int FirstUniqChar(string s)
        {
            Dictionary<char, int> dic = new Dictionary<char, int>();

            for (int i = 0; i < s.Length; i++)
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

            foreach (var i in dic.Values)
            {
                if (i != -1)
                    return i;
            }

            return -1;
        }

        ///392. Is Subsequence
        ///Given two strings s and t, return true if s is a subsequence of t, or false otherwise.
        ///A subsequence of a string is a new string that is formed from the original string by deleting some (can be none)
        public bool IsSubsequence(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            if (string.IsNullOrEmpty(t))
                return false;

            int sLen = s.Length;
            int tLen = t.Length;

            if (sLen > tLen)
                return false;

            for (int i = 0; i <= tLen - sLen; i++)
            {
                if (t[i] == s[0])
                {
                    int j = 1;
                    int k = i + 1;
                    while (j < s.Length && k <= tLen - (sLen - j))
                    {
                        if (s[j] == t[k])
                        {
                            j++;
                        }

                        k++;
                    }

                    if (j == s.Length)
                        return true;
                }
            }

            return false;
        }
    }
}
