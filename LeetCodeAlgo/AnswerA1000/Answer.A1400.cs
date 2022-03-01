using System.Collections.Generic;
using System.Linq;
using System;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1400. Construct K Palindrome Strings
        ///return true if you can use all the characters in s to construct k palindrome.
        ///1 <= s.length <= 10^5, s consists of lowercase English letters. 1 <= k <= 105
        public bool CanConstruct(string s, int k)
        {
            if (s.Length < k) return false;
            if (s.Length == k) return true;
            Dictionary<char,int> dict=new Dictionary<char, int>();
            foreach(var c in s)
            {
                if(!dict.ContainsKey(c))dict.Add(c, 0);
                dict[c]++;
            }
            var singleCount = dict.Where(x => x.Value % 2 == 1).Count();
            return k >= singleCount;
        }

        /// 1417. Reformat The String
        ///You are given an alphanumeric string s. Only lowercase English letters and digits.
        ///1 letter follow 1 digit , or reverse
        ///Return the reformatted string or return an empty string if it is impossible to reformat the string.
        public string Reformat(string s)
        {
            List<char> ans = new List<char>();
            List<char> letters = new List<char>();
            List<char> digits = new List<char>();

            foreach(var c in s)
            {
                if(c>='0'&&c<='9')
                    digits.Add(c);
                else
                    letters.Add(c);
            }
            if (letters.Count > digits.Count + 1 || digits.Count > letters.Count + 1)
                return string.Empty;
            if(letters.Count > digits.Count)
            {
                int i = 0;
                for (; i< digits.Count; i++)
                {
                    ans.Add(letters[i]);
                    ans.Add(digits[i]);
                }
                ans.Add(letters[i]);
            }
            else if(letters.Count < digits.Count)
            {
                int i = 0;
                for (; i < letters.Count; i++)
                {
                    ans.Add(digits[i]);
                    ans.Add(letters[i]);
                }
                ans.Add(digits[i]);
            }
            else
            {
                int i = 0;
                for (; i < letters.Count; i++)
                {
                    ans.Add(digits[i]);
                    ans.Add(letters[i]);
                }
            }
            return string.Join("", ans);
        }
        /// 1422. Maximum Score After Splitting a String
        ///Split to 2 string,score is the number of zeros in the left + the number of ones in the right substring.
        public int MaxScore(string s)
        {
            Dictionary<int, int> dict=new Dictionary<int, int>();
            int numOf0 = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if(s[i]=='0')
                    numOf0++;

                if(i<s.Length-1)
                    dict.Add(i, numOf0);
            }
            int max = 0;
            foreach(int key in dict.Keys)
                max=Math.Max(max, dict[key]+s.Length-numOf0-(key+1- dict[key]));
            return max;
        }

        ///1437. Check If All 1's Are at Least Length K Places Away
        ///Given an binary array nums and an integer k, return true if all 1's are at least k places away from each other
        public bool KLengthApart(int[] nums, int k)
        {
            var ans = true;
            int last = -k-1;
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 1)
                {
                    if(i- last <=k)
                        return false;
                    last = i;
                }
            }
            return ans;
        }
        ///1446. Consecutive Characters
        ///The power of the string is the maximum length of a non-empty substring that contains only one unique character.
        public int MaxPower(string s)
        {
            int max = 0;
            int count = 1;
            char c = s[0];
            for(int i = 1; i < s.Length; i++)
            {
                if(c == s[i]) { count++; }
                else
                {
                    max = Math.Max(max, count);
                    c = s[i];
                    count = 1;
                }
            }
            max = Math.Max(max, count);
            return max;
        }
        /// 1448. Count Good Nodes in Binary Tree
        ///a node X in the tree is named good if in the path from root to X there are no nodes with a value greater than X.
        public int GoodNodes(TreeNode root)
        {
            if (root == null)
                return 0;
            int ans = 1;
            int max = root.val;
            GoodNodes_Recursion(root.left, max, ref ans);
            GoodNodes_Recursion(root.right, max, ref ans);
            return ans;
        }
        public void GoodNodes_Recursion(TreeNode node,int max, ref int ans)
        {
            if (node == null)
                return;
            if (node.val >= max)
                ans++;
            max = Math.Max(node.val, max);
            GoodNodes_Recursion(node.left, max, ref ans);
            GoodNodes_Recursion(node.right, max, ref ans);
        }

        ///1464. Maximum Product of Two Elements in an Array
        ///Return the maximum value of (nums[i]-1)*(nums[j]-1).
        ///2 <= nums.length <= 500, 1 <= nums[i] <= 10^3
        public int MaxProduct(int[] nums)
        {
            int max1 = Math.Max(nums[0], nums[1]);
            int max2 = Math.Min(nums[0], nums[1]);
            for(int i = 2; i < nums.Length; i++)
            {
                if (nums[i] >= max1)
                {
                    max2 = max1;
                    max1 = nums[i];
                }
                else if (nums[i] > max2)
                {
                    max2 = nums[i];
                }
            }
            return (max1 - 1) * (max2 - 1);
        }
        /// 1482. Minimum Number of Days to Make m Bouquets , ### Binary Search
        ///You want to make m bouquets. To make a bouquet, you need to use k adjacent flowers from the garden.
        ///The garden consists of n flowers, the ith flower will bloom in the bloomDay[i] and then can be used in exactly one bouquet.
        ///Return the minimum number of days you need to wait to be able to make m bouquets from the garden.
        ///If it is impossible to make m bouquets return -1.
        ///1 <= bloomDay.length <= 10^5, 1 <= bloomDay[i] <= 10^9, 1 <= m <= 10^6, 1 <= k <= bloomDay.length
        public int MinDays(int[] bloomDay, int m, int k)
        {
            if (m * k > bloomDay.Length)
                return -1;
            if (m * k == bloomDay.Length)
                return bloomDay.Max();
            int left = 1;
            int right = 1000000000;
            while (left < right)
            {
                int mid = (left + right) / 2;
                int flowers = 0;
                int bouquet = 0;
                for (int i = 0; i < bloomDay.Length; i++)
                {
                    if (bloomDay[i] > mid)
                    {
                        flowers = 0;
                    }
                    else
                    {
                        flowers++;
                        if (flowers >= k)
                        {
                            bouquet++;
                            flowers = 0;
                        }
                    }
                }
                if (bouquet < m)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            return left;
        }
    }
}