using System.Collections.Generic;
using System.Linq;
using System;

namespace LeetCodeAlgo
{
    public partial class Answer
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

        ///1403. Minimum Subsequence in Non-Increasing Order
        ///1 <= nums.length <= 500, 1 <= nums[i] <= 100
        public IList<int> MinSubsequence(int[] nums)
        {
            Array.Sort(nums);
            int total = nums.Sum();
            var ans=new List<int>();
            int sum = 0;
            for(int i=nums.Length-1; i>=0; i--)
            {
                sum+=nums[i];
                ans.Add(nums[i]);
                if (sum > total - sum) break;
            }
            return ans;
        }
        ///1409. Queries on a Permutation With Key
        public int[] ProcessQueries(int[] queries, int m)
        {
            int[] arr= new int[m];
            for (int i = 0; i < m; i++)
                arr[i] = i + 1;
            var list = arr.ToList();
            int[] res= new int[queries.Length];
            for(int i=0;i<queries.Length;i++)
            {
                int index = list.IndexOf(queries[i]);
                res[i] = index;
                list.RemoveAt(index);
                list.Insert(0, queries[i]);
            }
            return res;
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


    }
}