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
            foreach (var n in nums1)
            {
                if (dict1.ContainsKey(n))
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
            var res = new List<int>();
            foreach (var key in dict1.Keys)
            {
                if (!dict2.ContainsKey(key))
                    continue;
                int count = Math.Min(dict1[key], dict2[key]);
                while (count-- > 0)
                    res.Add(key);
            }
            return res.ToArray();
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

        ///354. Russian Doll Envelopes, #Long Increasing Subsequence (LIS)
        ///Return the maximum envelopes you can Russian doll(i.e., put one inside the other, < ).
        public int MaxEnvelopes(int[][] envelopes)
        {
            Array.Sort(envelopes, (x1, x2) =>
            {
                if (x1[0] == x2[0])
                    return x2[1] - x1[1];
                else
                    return x1[0] - x2[0];
            });

            int[] dp = new int[envelopes.Length];
            int len = 0;
            foreach (var envelope in envelopes)
            {
                int index = Array.BinarySearch(dp, 0, len, envelope[1]);
                if (index < 0)
                    index = -(index + 1);
                dp[index] = envelope[1];
                if (index == len)
                    len++;
            }
            return len;
        }

        ///357. Count Numbers with Unique Digits
        ///return the count of all numbers with unique digits, x, where 0 <= x < 10^n.
        public int CountNumbersWithUniqueDigits(int n)
        {
            if (n == 0) return 1;
            //f(0) = 1.(0)
            //f(1) = 10. (0, 1, 2, 3, ...., 9)
            //f(2) = 9 * 9.
            //f(3) = f(2) * 8 = 9 * 9 * 8
            //f(10) = 9 * 9 * 8 * 7 * 6 * ... * 1
            //f(11) = 0 = f(12) = f(13)....
            int res = 10;
            int uniqueDigits = 9;
            int availableNumber = 9;
            while (n-- > 1 && availableNumber > 0)
            {
                uniqueDigits = uniqueDigits * availableNumber;
                res += uniqueDigits;
                availableNumber--;
            }
            return res;
        }

        /// 365. Water and Jug Problem
        ///If targetCapacity liters of water are measurable, you must have targetCapacity
        ///liters of water contained within one or both buckets by the end.
        public bool CanMeasureWater(int jug1Capacity, int jug2Capacity, int targetCapacity)
        {
            //limit brought by the statement that water is finallly in one or both buckets
            if (jug1Capacity + jug2Capacity < targetCapacity)
                return false;

            //case x or y is zero
            if (jug1Capacity == targetCapacity
                || jug2Capacity == targetCapacity
                || jug1Capacity + jug2Capacity == targetCapacity)
                return true;

            //get GCD, then we can use the property of Bézout's identity
            return targetCapacity % getGCD(jug1Capacity, jug2Capacity) == 0;
        }

        /// 367. Valid Perfect Square, in Easy


        ///371. Sum of Two Integers
        ///Given two integers a and b, return the sum of the two integers without using the operators + and -.
        ///-1000 <= a, b <= 1000
        public int GetSum(int a, int b)
        {
            int ans = 0;
            int bit = 1;
            bool carry = false;
            //check every bit 1 or 0
            for (int i = 0; i < 32; i++)
            {
                bool left = (a & bit) != 0;
                bool right = (b & bit) != 0;
                int count = 0;
                if (left) count++;
                if (right) count++;
                if (carry) count++;
                carry = count >= 2;
                bool add = (count % 2) == 1;
                if (add)
                {
                    ans |= bit;
                }
                bit <<= 1;
            }
            return ans;
        }

        /// 373. Find K Pairs with Smallest Sums,  ,#Priority Queue
        /// You are given two integer arrays nums1 and nums2 sorted in ascending order and an integer k.
        ///Define a pair(u, v) which consists of one element from the first array and one element from the second array.
        ///Return the k pairs (u1, v1), (u2, v2), ..., (uk, vk) with the smallest sums.
        public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
        {
            int len1 = nums1.Length;
            int len2 = nums2.Length;
            //min heap, store indexes in nums1 and nums2, sort by nums1[i]+nums2[j]
            var priorityQueue = new PriorityQueue<int[], int>();
            for (int i = 0; i < Math.Min(len1, k); i++)
            {
                //enqueue atmost Math.Min(len1,k) count of [i,0] combines, auto sorted by nums1[i] + nums2[0]
                //{nums1[0],nums2[0]} always the first element
                priorityQueue.Enqueue(new int[] { i, 0 }, nums1[i] + nums2[0]);
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

            for (int i = 1; i < nums.Length; i++)
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
            return Math.Max(max1, max2);
        }

        ///377. Combination Sum IV -Permutation , #DP
        ///Given an array of distinct integers nums and a target integer target,
        ///return the number of possible combinations that add up to target.
        public int CombinationSum4(int[] nums, int target)
        {
            int[] dp = new int[target + 1];
            dp[0] = 1;
            for (int sum = 0; sum <= target; sum++)
            {
                foreach (var n in nums)
                {
                    dp[sum] += sum - n >= 0 ? dp[sum - n] : 0;
                }
            }
            return dp.Last();
        }

        ///378. Kth Smallest Element in a Sorted Matrix, , #Priority Queue
        ///Given an n x n matrix where each of the rows and columns is sorted in ascending order,
        ///return the kth smallest element in the matrix. O(n^2)
        public int KthSmallest(int[][] matrix, int k)
        {
            PriorityQueue<int[], int> priorityQueue = new PriorityQueue<int[], int>();
            for (int i = 0; i < matrix.Length; i++)
            {
                priorityQueue.Enqueue(new int[] { i, 0 }, matrix[i][0]);
            }
            int count = 0;
            while (count++ < k)
            {
                var i = priorityQueue.Dequeue();
                if (count == k) return matrix[i[0]][i[1]];
                if (i[1] != matrix[0].Length - 1)
                    priorityQueue.Enqueue(new int[] { i[0], i[1] + 1 }, matrix[i[0]][i[1] + 1]);
            }
            return 0;
        }

        /// 380. Insert Delete GetRandom O(1), see RandomizedSet

        ///382. Linked List Random Node


        ///383. Ransom Note, in Easy


        ///384. Shuffle an Array, see Solution_384_Shuffle

        ///386. Lexicographical Numbers， #Trie, #DFS
        //Given an integer n, return all the numbers in the range [1, n] sorted in lexicographical order.
        //You must write an algorithm that runs in O(n) time and uses O(1) extra space.
        public IList<int> LexicalOrder(int n)
        {
            var res = new List<int>();
            for (int i = 1; i < 10; ++i)
            {
                LexicalOrder_DFS(i, n, res);
            }
            return res;
        }

        private void LexicalOrder_DFS(int curr, int n, List<int> res)
        {
            if (curr > n) return;
            else
            {
                res.Add(curr);
                for (int i = 0; i < 10; ++i)
                {
                    if (10 * curr + i > n)
                        return;
                    LexicalOrder_DFS(10 * curr + i, n, res);
                }
            }
        }

        public IList<int> LexicalOrder_Iteration(int n)
        {
            var res = new List<int>(); ;
            int curr = 1;
            for (int i = 1; i <= n; i++)
            {
                res.Add(curr);
                if (curr * 10 <= n)
                {
                    curr *= 10;
                }
                else if (curr % 10 != 9 && curr + 1 <= n)
                {
                    curr++;
                }
                else
                {
                    while ((curr / 10) % 10 == 9)
                    {
                        curr /= 10;
                    }
                    curr = curr / 10 + 1;
                }
            }
            return res;
        }

        public IList<int> LexicalOrder_Trie(int n)
        {
            var res = new List<int>();
            var root = new TrieItem();
            for(int i = 1; i <= n; i++)
            {
                var curr = root;
                var str = i.ToString();
                foreach(var c in str)
                {
                    if (curr.arr10 == null)
                        curr.arr10 = new TrieItem[10];
                    if (curr.arr10[c - '0'] == null)
                        curr.arr10[c - '0'] = new TrieItem();
                    curr = curr.arr10[c - '0'];
                }
                curr.val = i;
            }
            LexicalOrder_Trie(root, res);
            return res;
        }

        private void LexicalOrder_Trie(TrieItem root, List<int> res)
        {
            if (root == null) return;
            if (root.val != -1) res.Add(root.val);
            if (root.arr10 != null)
            {
                foreach(var i in root.arr10)
                {
                    if (i != null)
                        LexicalOrder_Trie(i, res);
                }
            }
        }

        public IList<int> LexicalOrder_PQ(int n)
        {
            PriorityQueue<int, string> pq = new PriorityQueue<int, string>();
            for (int i = 1; i <= n; i++)
                pq.Enqueue(i, i.ToString());
            var res = new List<int>();
            while (pq.Count > 0)
                res.Add(pq.Dequeue());
            return res;
        }

        ///387. First Unique Character in a String, in Easy


        ///389. Find the Difference, in Easy


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
            int j = 0;
            for (int i = 0; i < tLen && j < sLen; i++)
            {
                if (s[j] == t[i]) j++;
            }
            return j == sLen;
        }

        ///394. Decode String
        ///The encoding rule is: k[encoded_string], where the encoded_string inside the square brackets is being repeated exactly k times.
        ///Input: s = "3[a]2[bc]"       =>      Output: "aaabcbc";
        ///Input: s = "3[a2[c]]"        =>      Output: "accaccacc"
        ///Input: s = "2[abc]3[cd]ef"   =>      Output: "abcabccdcdcdef"
        public string DecodeString(string s)
        {
            int startOfDigit = -1;
            int endOfDigit = -1;
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]) && startOfDigit == -1) startOfDigit = i;
                if (s[i] == '[')
                {
                    count++;
                    if (endOfDigit == -1) endOfDigit = i - 1;
                }
                if (s[i] == ']')
                {
                    count--;
                    if (count == 0)
                    {
                        int k = int.Parse(s.Substring(startOfDigit, endOfDigit - startOfDigit + 1));
                        string curr = "";
                        string sub = DecodeString(s.Substring(endOfDigit + 2, i - (endOfDigit + 2)));
                        while (k-- > 0)
                        {
                            curr += sub;
                        }
                        return s.Substring(0, startOfDigit) + curr + DecodeString(s.Substring(i + 1));
                    }
                }
            }
            return s;
        }

        public string DecodeString_Stack(string s)
        {
            var curr = "";
            Stack<int> countStack = new Stack<int>();
            Stack<string> strStack = new Stack<string>();
            int idx = 0;
            while (idx < s.Length)
            {
                if (char.IsDigit(s[idx]))
                {
                    int val = 0;
                    while (char.IsDigit(s[idx]))
                    {
                        val = 10 * val + (s[idx++] - '0');
                    }
                    countStack.Push(val);
                }
                else if (s[idx] == '[')
                {
                    strStack.Push(curr);
                    curr = "";
                    idx++;
                }
                else if (s[idx] == ']')
                {
                    StringBuilder sb = new StringBuilder();
                    int repeatTimes = countStack.Pop();
                    for (int i = 0; i < repeatTimes; i++)
                    {
                        sb.Append(curr);
                    }
                    curr = strStack.Pop() + sb.ToString();
                    idx++;
                }
                else
                {
                    curr += s[idx++];
                }
            }
            return curr;
        }

        ///395. Longest Substring with At Least K Repeating Characters, #Divide And Conquer
        ///return the length of the longest substring of s such that the frequency of each character >= k.
        public int LongestSubstring(string s, int k)
        {
            if (s.Length < k) return 0;

            int res = 0;
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (dict.ContainsKey(c)) dict[c]++;
                else dict.Add(c, 1);
            }

            bool match = dict.Keys.Where(x => dict[x] < k).Count() == 0;
            if (match)
                return s.Length;

            int left = 0;
            int right = 0;
            for (; right < s.Length; right++)
            {
                if (dict[s[right]] < k)
                {
                    if (right - 1 >= left)
                    {
                        res = Math.Max(res, LongestSubstring(s.Substring(left, right - 1 - left + 1), k));
                    }
                    left = right + 1;
                }
            }
            res = Math.Max(res, LongestSubstring(s.Substring(left, right - 1 - left + 1), k));
            return res;
        }

        ///397. Integer Replacement, #Bit Manipulation
        //If n is even, replace n with n / 2. If n is odd, replace n with either n + 1 or n - 1.
        //Return the minimum number of operations needed for n to become 1, 1 <= n <= 231 - 1
        public int IntegerReplacement_Recursion(int n)
        {
            if (n == 1) return 0;
            if (n == 2) return 1;
            if (n == int.MaxValue) return 32;
            if (n % 2 == 0)
                return 1 + IntegerReplacement_Recursion(n / 2);
            else
                return 1 + Math.Min(IntegerReplacement_Recursion(n + 1), IntegerReplacement_Recursion(n - 1));
        }

        public int IntegerReplacement_TailRecur(int res , int n)
        {
            if (n == 1) return res;
            if (n == 2) return res+1;
            if (n == int.MaxValue) return res+32;
            if (n % 2 == 0)
                return IntegerReplacement_TailRecur(res+1,n / 2);
            else
                return Math.Min(IntegerReplacement_TailRecur(res + 1, n + 1), IntegerReplacement_TailRecur(res + 1, n - 1));
        }

        public int IntegerReplacement(int n)
        {
            //https://leetcode.com/problems/integer-replacement/discuss/87920/A-couple-of-Java-solutions-with-explanations
            //If n=3 or n-1 has less 1's than n+1, decrement n.
            int res = 0;
            long a = n;
            while (a != 1)
            {
                if ((a & 1) == 0)
                {
                    a >>= 1;
                }
                else if (a == 3 || ((a >> 1) & 1) == 0)
                {
                    --a;
                }
                else
                {
                    ++a;
                }
                ++res;
            }
            return res;
        }

        ///398. Random Pick Index, see RandomPickIndex

        /// 399. Evaluate Division, #Graph, #DFS
        ///where equations[i] = [Ai, Bi] and values[i] represent the equation Ai / Bi = values[i].
        ///Each Ai or Bi is a string that represents a single variable.
        ///queries[j] = [Cj, Dj] represents the jth query where you must find the answer for Cj / Dj = ?.
        ///Return the answers to all queries.If a single answer cannot be determined, return -1.0.
        public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
        {
            double[] res = new double[queries.Count];
            Dictionary<string, Dictionary<string, double>> graph = new Dictionary<string, Dictionary<string, double>>();
            for (int i = 0; i < equations.Count; i++)
            {
                var curr = equations[i];
                if (!graph.ContainsKey(curr[0])) graph.Add(curr[0], new Dictionary<string, double>());
                if (!graph.ContainsKey(curr[1])) graph.Add(curr[1], new Dictionary<string, double>());
                graph[curr[0]].Add(curr[1], values[i]);
                graph[curr[1]].Add(curr[0], 1 / values[i]);
            }

            for (int i = 0; i < queries.Count; i++)
            {
                bool find = false;
                double val = -1;
                CalcEquation_DFS(graph, new HashSet<string>(), queries[i][0], queries[i][1], 1, ref find, ref val);
                res[i] = find ? 1 / val : -1;
            }
            return res;
        }

        private void CalcEquation_DFS(Dictionary<string, Dictionary<string, double>> graph, HashSet<string> visit, string key, string target, double seed, ref bool find, ref double val)
        {
            if (find) return;
            if (visit.Contains(key)) return;
            visit.Add(key);
            if (!graph.ContainsKey(key)) return;
            if (graph[key].ContainsKey(target))
            {
                find = true;
                val = seed / graph[key][target];
                return;
            }
            foreach (var subKey in graph[key].Keys)
            {
                CalcEquation_DFS(graph, visit, subKey, target, seed / graph[key][subKey], ref find, ref val);
            }
        }
    }
}