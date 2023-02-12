using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using System.Text;

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
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (!dict.ContainsKey(c)) dict.Add(c, 0);
                dict[c]++;
            }
            var singleCount = dict.Where(x => x.Value % 2 == 1).Count();
            return k >= singleCount;
        }

        ///1402. Reducing Dishes
        public int MaxSatisfaction(int[] satisfaction)
        {
            int res = 0;
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            foreach (var n in satisfaction)
                pq.Enqueue(n, -n);

            List<int> list = new List<int>();
            int sum = 0;
            while (pq.Count > 0)
            {
                var curr = pq.Dequeue();
                if (curr >= 0 || curr + sum > 0)
                {
                    list.Insert(0, curr);
                    sum += curr;
                }
                else break;
            }

            for (int i = 0; i < list.Count; i++)
                res += (i + 1) * list[i];
            return res;
        }

        ///1403. Minimum Subsequence in Non-Increasing Order
        ///1 <= nums.length <= 500, 1 <= nums[i] <= 100
        public IList<int> MinSubsequence(int[] nums)
        {
            Array.Sort(nums);
            int total = nums.Sum();
            var ans = new List<int>();
            int sum = 0;
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                sum += nums[i];
                ans.Add(nums[i]);
                if (sum > total - sum) break;
            }
            return ans;
        }

        ///1408. String Matching in an Array
        //public IList<string> StringMatching(string[] words)
        //{
        //    var res = new List<string>();
        //    foreach(var word in words)
        //    {
        //        if(words.Any(x=>x.Length>word.Length && x.Contains(word, StringComparison.OrdinalIgnoreCase)))
        //        {
        //            res.Add(word);
        //        }
        //    }
        //    return res;
        //}

        /// 1409. Queries on a Permutation With Key
        public int[] ProcessQueries(int[] queries, int m)
        {
            int[] arr = new int[m];
            for (int i = 0; i < m; i++)
                arr[i] = i + 1;
            var list = arr.ToList();
            int[] res = new int[queries.Length];
            for (int i = 0; i < queries.Length; i++)
            {
                int index = list.IndexOf(queries[i]);
                res[i] = index;
                list.RemoveAt(index);
                list.Insert(0, queries[i]);
            }
            return res;
        }

        ///1415. The k-th Lexicographical String of All Happy Strings of Length n, #Backtracking
        ///A happy string is a string that:consists only of letters of the set['a', 'b', 'c'].
        ///s[i] != s[i + 1] for all values of i from 1 to s.length - 1 (string is 1-indexed).
        ///Given two integers n and k, consider a list of all happy strings of length n sorted in lexicographical order.
        ///Return the kth string of this list or return an empty string if there are less than k happy strings of length n.
        public string GetHappyString(int n, int k)
        {
            char[] arr = new char[] { 'a', 'b', 'c' };
            List<string> list = new List<string>();

            GetHappyString(arr, "", -1, n, k, list);
            return list.Count<k ?string.Empty:list.Last();
        }
        private void GetHappyString(char[] arr,string curr, int index, int count,int k, List<string> res)
        {
            if (res.Count == k) return;
            if(count == 0)
            {
                res.Add(curr);
                return;
            }

            for(int i = 0; i < arr.Length; i++)
            {
                if (i == index) continue;
                GetHappyString(arr, curr + arr[i], i, count - 1, k, res);
            }
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

            foreach (var c in s)
            {
                if (c >= '0' && c <= '9')
                    digits.Add(c);
                else
                    letters.Add(c);
            }
            if (letters.Count > digits.Count + 1 || digits.Count > letters.Count + 1)
                return string.Empty;
            if (letters.Count > digits.Count)
            {
                int i = 0;
                for (; i < digits.Count; i++)
                {
                    ans.Add(letters[i]);
                    ans.Add(digits[i]);
                }
                ans.Add(letters[i]);
            }
            else if (letters.Count < digits.Count)
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

        ///1418. Display Table of Food Orders in a Restaurant
        public IList<IList<string>> DisplayTable(IList<IList<string>> orders)
        {
            Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string, int>>();
            HashSet<string> foodSet = new HashSet<string>();
            foreach(var order in orders)
            {
                foodSet.Add(order[2]);

                if (!dict.ContainsKey(order[1])) dict.Add(order[1], new Dictionary<string, int>());
                if (dict[order[1]].ContainsKey(order[2])) dict[order[1]][order[2]]++;
                else dict[order[1]].Add(order[2],1);
            }
            var foods = foodSet.OrderBy(x=>x, StringComparer.Ordinal).ToList();

            var res=new List<IList<string>>();
            var headers=new List<string>();
            headers.Add("Table");
            headers.AddRange(foods);
            res.Add(headers);
            var keys=dict.Keys.OrderBy(x=>int.Parse(x)).ToList();
            foreach (var table in keys)
            {
                var curr=new List<string>();
                curr.Add(table);
                foreach(var food in foods)
                {
                    if (!dict[table].ContainsKey(food)) curr.Add("0");
                    else curr.Add(dict[table][food].ToString());
                }
                res.Add(curr);
            }
            return res;
        }
        /// 1422. Maximum Score After Splitting a String
        ///Split to 2 string,score is the number of zeros in the left + the number of ones in the right substring.
        public int MaxScore(string s)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int numOf0 = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '0')
                    numOf0++;

                if (i < s.Length - 1)
                    dict.Add(i, numOf0);
            }
            int max = 0;
            foreach (int key in dict.Keys)
                max = Math.Max(max, dict[key] + s.Length - numOf0 - (key + 1 - dict[key]));
            return max;
        }

        ///1423. Maximum Points You Can Obtain from Cards, #Sliding Window
        //In one step, you can take one card from the beginning or from the end of the row.
        //You have to take exactly k cards.Your score is the sum of the points of the cards you have taken.
        //Given the integer array cardPoints and the integer k, return the maximum score you can obtain.
        public int MaxScore(int[] cardPoints, int k)
        {
            //equal to find the min continuous subArray contains n-k elements
            int n = cardPoints.Length;
            if (k == n) return cardPoints.Sum();
            int min = int.MaxValue;
            int count = 0;
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += cardPoints[i];
                count++;
                if (count ==n- k)
                {
                    min = Math.Min(min, sum);
                    sum -= cardPoints[i - count+1];
                    count--;
                }
            }
            return cardPoints.Sum() - min;
        }

        ///1436. Destination City
        public string DestCity(IList<IList<string>> paths)
        {
            var departures = paths.Select(x => x[0]).ToHashSet();
            return paths.First(x => !departures.Contains(x[1]))[1];
        }

        ///1437. Check If All 1's Are at Least Length K Places Away
        ///Given an binary array nums and an integer k, return true if all 1's are at least k places away from each other
        public bool KLengthApart(int[] nums, int k)
        {
            var ans = true;
            int last = -k - 1;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 1)
                {
                    if (i - last <= k)
                        return false;
                    last = i;
                }
            }
            return ans;
        }

        ///1442. Count Triplets That Can Form Two Arrays of Equal XOR, #Prefix Sum
        ///We want to select three indices i, j and k where (0 <= i < j <= k < arr.length).
        ///a = arr[i] ^ arr[i + 1] ^ ... ^ arr[j - 1] b = arr[j] ^ arr[j + 1] ^ ... ^ arr[k]
        ///Return the number of triplets (i, j and k) Where a == b.
        ///xor of range [i,k] ==0

        public int CountTriplets(int[] arr)
        {
            int n = arr.Length + 1;
            int res = 0;
            int[] prefixXor = new int[n];

            for (int i = 1; i < n; ++i)
                prefixXor[i] = arr[i - 1] ^ prefixXor[i - 1];

            for (int i = 0; i < n-1; ++i)
                for (int j = i + 1; j < n; ++j)
                    if (prefixXor[i] == prefixXor[j])
                        res += j - i - 1;
            return res;
        }


        /// 1446. Consecutive Characters
        ///The power of the string is the maximum length of a non-empty substring that contains only one unique character.
        public int MaxPower(string s)
        {
            int max = 0;
            int count = 1;
            char c = s[0];
            for (int i = 1; i < s.Length; i++)
            {
                if (c == s[i]) { count++; }
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

        public void GoodNodes_Recursion(TreeNode node, int max, ref int ans)
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