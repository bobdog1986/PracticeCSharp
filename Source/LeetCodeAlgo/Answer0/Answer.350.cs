using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///350. Intersection of Two Arrays II, #Two Pointers, #HashMap
        ///Given two integer arrays nums1 and nums2, return an array of their intersection.
        ///Each element in the result must appear as many times as it shows in both arrays and you may return the result in any order.
        public int[] Intersect(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> dict1 = new Dictionary<int, int>();
            foreach(var n in nums1)
            {
                if(dict1.ContainsKey(n))
                    dict1[n]++;
                else
                    dict1.Add(n, 1);
            }
            Dictionary<int, int> dict2 = new Dictionary<int, int>();
            foreach (var n in nums2)
            {
                if (dict2.ContainsKey(n))
                    dict2[n]++;
                else
                    dict2.Add(n, 1);
            }
            var ans=new List<int>();
            foreach(var key in dict1.Keys)
            {
                if (!dict2.ContainsKey(key))
                    continue;
                int i = 0;
                int count = Math.Min(dict1[key], dict2[key]);
                while (i++< count)
                    ans.Add(key);
            }
            return ans.ToArray();
        }
        public int[] Intersect_350_TwoPointers(int[] nums1, int[] nums2)
        {

            var ans = new List<int>();
            //O(n)= mlogm+nlogn+(m+n)
            Array.Sort(nums1);
            Array.Sort(nums2);
            int i = 0;
            int j = 0;
            while (i < nums1.Length && j < nums2.Length)
            {
                if (nums1[i] == nums2[j])
                {
                    ans.Add(nums1[i]);
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
            return ans.ToArray();
        }

        ///367 not pass
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

        ///371. Sum of Two Integers
        ///Given two integers a and b, return the sum of the two integers without using the operators + and -.
        ///-1000 <= a, b <= 1000
        public int GetSum(int a, int b)
        {
            int ans = 0;
            int bit = 1;
            bool carry = false;
            //check every bit 1 or 0
            for(int i=0; i < 32; i++)
            {
                bool left = (a & bit) !=0;
                bool right = (b & bit) != 0;
                int count = 0;
                if (left) count++;
                if(right) count++;
                if (carry) count++;
                carry = count >=2;
                bool add = (count % 2) == 1;
                if (add)
                {
                    ans |= bit;
                }
                bit <<= 1;
            }
            return ans;
        }

        /// 373. Find K Pairs with Smallest Sums, #Heap ,#Priority Queue
        /// You are given two integer arrays nums1 and nums2 sorted in ascending order and an integer k.
        ///Define a pair(u, v) which consists of one element from the first array and one element from the second array.
        ///Return the k pairs (u1, v1), (u2, v2), ..., (uk, vk) with the smallest sums.
        public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
        {
            int len1 = nums1.Length;
            int len2 = nums2.Length;
            //min heap, store indexes in nums1 and nums2, sort by nums1[i]+nums2[j]
            var priorityQueue = new PriorityQueue<int[], int>();
            for (int i = 0; i <Math.Min(len1,k); i++)
            {
                //enqueue atmost Math.Min(len1,k) count of [i,0] combines, auto sorted by nums1[i] + nums2[0]
                //{nums1[0],nums2[0]} always the first element
                priorityQueue.Enqueue(new int[] {i ,0 }, nums1[i] + nums2[0]);
            }
            IList<IList<int>> ans = new List<IList<int>>();
            while (k-- > 0 && priorityQueue.Count > 0)
            {
                var first = priorityQueue.Dequeue();
                int i = first[0];
                int j = first[1];
                //the first element must be {nums1[0],nums2[0]} , we add it to result
                ans.Add(new List<int>() { nums1[i], nums2[j] });
                //every time we add [i,j] to ans, then j=j+1, enqueue nums1[i]+nums2[j+1] if possible, so we will never miss any combines
                if (++j < nums2.Length)
                {
                    priorityQueue.Enqueue(new int[] { i, j }, nums1[i] + nums2[j]);
                }
            }
            return ans;
        }
        /// 374. Guess Number Higher or Lower, see GuessGame



        /// 376. Wiggle Subsequence, #DP
        ///A wiggle sequence is a sequence where the differences between numbers strictly alternate between positive and negative.
        ///For example, [1, 7, 4, 9, 2, 5] is a wiggle sequence because the differences (6, -3, 5, -7, 3) alternate between positive and negative.
        ///Given an integer array nums, return the length of the longest wiggle subsequence of nums.
        public int WiggleMaxLength(int[] nums)
        {
            int pos = nums[0];
            int posSign = 1;

            int neg = nums[0];
            int negSign = -1;

            int max1 = 1;
            int max2 = 1;

            for(int i=1; i<nums.Length; i++)
            {
                if ((posSign == 1 && nums[i] < pos)
                    || (posSign == -1 && nums[i] > pos))
                {
                    max1++;
                    posSign = -posSign;
                }
                pos = nums[i];

                if ((negSign == 1 && nums[i] < neg)
                    || (negSign == -1 && nums[i] > neg))
                {
                    max2++;
                    negSign = -negSign;
                }
                neg = nums[i];
            }
            return Math.Max(max1,max2);
        }

        ///377. Combination Sum IV -Permutation , #DP
        ///Given an array of distinct integers nums and a target integer target,
        ///return the number of possible combinations that add up to target.
        public int CombinationSum4(int[] nums, int target)
        {
            int[] dp=new int[target+1];
            dp[0] = 1;
            for(int sum = 0; sum <= target; sum++)
            {
                foreach (var n in nums)
                {
                    dp[sum] += sum - n >= 0 ? dp[sum - n] : 0;
                }
            }
            return dp.Last();
        }

        /// 380. Insert Delete GetRandom O(1), see RandomizedSet

        ///383. Ransom Note
        ///Given two strings ransomNote and magazine, return true if ransomNote can be constructed from magazine and false otherwise.
        ///Each letter in magazine can only be used once in ransomNote.
        public bool CanConstruct(string ransomNote, string magazine)
        {
            int[] arr = new int[26];
            foreach (var c in magazine)
                arr[c - 'a']++;
            foreach(var c in ransomNote)
                if (arr[c - 'a']-- == 0) return false;
            return true;
        }

        ///384. Shuffle an Array, see Solution_384_Shuffle

        ///387. First Unique Character in a String
        ///find the first non-repeating character in it and return its index. If it does not exist, return -1.
        public int FirstUniqChar(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]))
                    dict[s[i]]=-1;
                else
                    dict.Add(s[i],i);
            }
            foreach (var key in dict.Keys)
            {
                if (dict[key] != -1)
                    return dict[key];
            }
            return -1;
        }

        ///389. Find the Difference
        ///String t is generated by random shuffling string s and then add one more letter at a random position.
        ///Return the letter that was added to t.
        public char FindTheDifference(string s, string t)
        {
            int letterCount = 26;
            int[] arr = new int[letterCount];
            foreach (var c in s)
                arr[c - 'a']--;
            foreach (var c in t)
                arr[c - 'a']++;
            for(int i=0; i<arr.Length; i++)
            {
                if (arr[i] != 0)
                    return (char)(i + 'a');
            }
            return 'a';
        }

        /// 392. Is Subsequence
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
            if (sLen > tLen) return false;
            for (int i = 0; i <= tLen - sLen; i++)
            {
                if (t[i] == s[0])
                {
                    int j = 1;
                    int k = i + 1;
                    while (j < s.Length && k <= tLen - (sLen - j))
                    {
                        if (s[j] == t[k]) { j++; }
                        k++;
                    }
                    if (j == s.Length) return true;
                }
            }
            return false;
        }
    }
}