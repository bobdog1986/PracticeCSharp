﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2150. Find All Lonely Numbers in the Array
        ///You are given an integer array nums. A number x is lonely when it appears only once,
        ///and no adjacent numbers (i.e. x + 1 and x - 1) appear in the array.
        ///Return all lonely numbers in nums.You may return the answer in any order.
        ///0 <= nums[i] <= 10^6
        public IList<int> FindLonely(int[] nums)
        {
            List<int> res = new List<int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (dict.ContainsKey(n)) dict[n]++;
                else dict.Add(n, 1);
            }
            foreach (var key in dict.Keys)
            {
                if (dict[key] == 1 && !dict.ContainsKey(key - 1) && !dict.ContainsKey(key + 1))
                    res.Add(key);
            }
            return res;
        }

        ///2154. Keep Multiplying Found Values by Two
        ///If original is found in nums, multiply it by two (i.e., set original = 2 * original).
        ///Otherwise, stop the process.Repeat this process with the new number as long as you keep finding the number.
        ///Return the final value of original.
        public int FindFinalValue(int[] nums, int original)
        {
            HashSet<int> set = new HashSet<int>(nums);
            while (set.Contains(original))
                original += original;
            return original;
        }

        ///2161. Partition Array According to Given Pivot
        ///smaller than pivot on left, same as pivot on mid, larger on right
        public int[] PivotArray(int[] nums, int pivot)
        {
            int left = 0;
            int mid = 0;
            int right = 0;
            int[] arr = new int[nums.Length];
            int[] res = new int[nums.Length];
            foreach (var n in nums)
            {
                if (n < pivot) res[left++]=n;
                else if (n == pivot) mid++;
                else arr[right++]=n;
            }
            while (mid-- > 0)
            {
                res[left++] = pivot;
            }
            int j = 0;
            while (j<right)
            {
                res[left + j] = arr[j];
                j++;
            }
            return res;
        }
        /// 2176. Count Equal and Divisible Pairs in an Array
        ///return the number of pairs (i, j) where 0 <= i < j < n, such that nums[i] == nums[j] and (i * j) is divisible by k.
        public int CountPairs(int[] nums, int k)
        {
            int res = 0;
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i])) dict.Add(nums[i], new List<int>());
                dict[nums[i]].Add(i);
            }
            foreach (var key in dict.Keys)
            {
                if (dict[key].Count >= 2)
                {
                    for (int i = 0; i < dict[key].Count - 1; i++)
                    {
                        for (int j = i + 1; j < dict[key].Count; j++)
                        {
                            if (dict[key][i] * dict[key][j] % k == 0) res++;
                        }
                    }
                }
            }
            return res;
        }

        ///2177. Find Three Consecutive Integers That Sum to a Given Number
        public long[] SumOfThree(long num)
        {
            return num % 3 == 0 ? new long[] { num / 3 - 1, num / 3, num / 3 + 1 } : new long[] { };
        }
        /// 2180. Count Integers With Even Digit Sum, nums<=1000
        public int CountEven(int num)
        {
            ///10 = 2, 4, 6, 8(4)
            ///20 = 2, 4, 6, 8, 11, 13, 15, 17, 19, 20(10)
            ///30 = 2, 4, 6, 8, 11, 13, 15, 17, 19, 20, 22, 24, 26, and 28. (14)
            int temp = num, sum = 0;
            while (num > 0)
            {
                sum += num % 10;
                num /= 10;
            }
            return sum % 2 == 0 ? temp / 2 : (temp - 1) / 2;
        }

        /// 2185. Counting Words With a Given Prefix
        public int PrefixCount(string[] words, string pref)
        {
            return words.Where(word => word.StartsWith(pref)).Count();
        }

        ///2186. Minimum Number of Steps to Make Two Strings Anagram II
        ///You are given two strings s and t. In one step, you can append any character to either s or t.
        ///Return the minimum number of steps to make s and t anagrams of each other.
        ///An anagram of a string is a string that contains the same characters with a different(or the same) ordering.
        public int MinSteps(string s, string t)
        {
            int[] arr = new int[26];
            foreach (var c in s)
                arr[c - 'a']++;
            foreach (var c in t)
                arr[c - 'a']--;
            return arr.Sum(x => Math.Abs(x));
        }
        /// 2190. Most Frequent Number Following Key In an Array
        ///0 <= i <= n - 2, nums[i] == key and, nums[i + 1] == target.
        ///Return the target with the maximum count.
        public int MostFrequent(int[] nums, int key)
        {
            int max = 0;
            int res = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == key)
                {
                    if (map.ContainsKey(nums[i + 1])) map[nums[i + 1]]++;
                    else map.Add(nums[i + 1], 1);
                    if (map[nums[i + 1]] > max)
                    {
                        max = map[nums[i + 1]];
                        res = nums[i + 1];
                    }
                }
            }
            return res;
        }

        ///2191. Sort the Jumbled Numbers
        ///Transfer digits according to mapping array, then sort
        public int[] SortJumbled(int[] mapping, int[] nums)
        {
            return nums.OrderBy(x => SortJumbled(x, mapping)).ToArray();
        }

        public int SortJumbled(int n, int[] mapping)
        {
            if (n < 10) return mapping[n];//avoid n==0 issue
            int res = 0;
            int x = 1;
            while (n > 0)
            {
                res += mapping[n % 10] * x;
                n /= 10;
                x *= 10;
            }
            return res;
        }
        /// 2195. Append K Integers With Minimal Sum, #PriorityQueue, #Heap
        ///You are given an integer array nums and an integer k.
        ///Append k unique positive integers that do not appear in nums to nums such that the resulting total sum is minimum.
        ///Return the sum of the k integers appended to nums.
        public long MinimalKSum(int[] nums, int k)
        {
            long res = 0;
            PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();
            foreach (var n in nums)
                priorityQueue.Enqueue(n, n);//min heap

            int lastIndex = 0;
            while (priorityQueue.Count > 0)
            {
                int currIndex = priorityQueue.Dequeue();
                if (lastIndex == currIndex) continue;
                else
                {
                    //how many nums in range [lastIndex + 1,currIndex - 1], inclusive
                    int count = currIndex - 1 - (lastIndex + 1) + 1;
                    if (count >= k) count = k;//if exceed k, only append k nums
                    res += (lastIndex + 1 + lastIndex + count) * (long)count / 2;
                    k -= count;
                    lastIndex = currIndex;//update lastIndex
                }
            }

            if (k > 0)// if still k>0
            {
                res += (lastIndex + 1 + lastIndex + k) * (long)k / 2;
            }

            return res;
        }

        /// 2196. Create Binary Tree From Descriptions
        ///descriptions[i] = [parenti, childi, isLefti] indicates that parenti is the parent of childi in a binary tree of unique values. Furthermore,
        ///If isLefti == 1, then childi is the left child of parenti.
        ///If isLefti == 0, then childi is the right child of parenti.
        ///Construct the binary tree described by descriptions and return its root.
        public TreeNode CreateBinaryTree(int[][] descriptions)
        {
            //set contains all nodes,but only the root's value is true
            Dictionary<TreeNode, bool> set = new Dictionary<TreeNode, bool>();
            Dictionary<int, TreeNode> dict = new Dictionary<int, TreeNode>();
            foreach (var desc in descriptions)
            {
                TreeNode parent = null;
                if (!dict.ContainsKey(desc[0])) dict.Add(desc[0], new TreeNode(desc[0]));
                parent = dict[desc[0]];
                if (!set.ContainsKey(parent)) set.Add(parent, true);

                TreeNode child = null;
                if (!dict.ContainsKey(desc[1])) dict.Add(desc[1], new TreeNode(desc[1]));
                child = dict[desc[1]];
                if (!set.ContainsKey(child)) set.Add(child, false);
                else set[child] = false;

                if (desc[2] == 1) parent.left = child;
                else parent.right = child;
            }

            return set.FirstOrDefault(x => x.Value).Key;
        }
    }
}