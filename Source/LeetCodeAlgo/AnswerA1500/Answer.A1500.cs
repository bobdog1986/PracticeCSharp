using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1502. Can Make Arithmetic Progression From Sequence
        ///A sequence of numbers is an arithmetic progression if the difference between any two consecutive elements is same.
        public bool CanMakeArithmeticProgression(int[] arr)
        {
            Array.Sort(arr);
            int diff = arr[1] - arr[0];
            for (int i = 2; i < arr.Length; i++)
                if (arr[i] - arr[i - 1] != diff) return false;
            return true;
        }

        ///1507. Reformat Date
        public string ReformatDate(string date)
        {
            var list = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var arr = date.Split(' ');
            int year = int.Parse(arr[2]);
            int month = (list.IndexOf(arr[1]) + 1);
            int day = int.Parse(arr[0].Substring(0, arr[0].Length - 2));
            return new DateTime(year, month, day).ToString("yyyy-MM-dd");
        }

        ///1508. Range Sum of Sorted Subarray Sums, #PriorityQueue
        //Create new int[] of all range of subarrays,then get sum of [left,right], 1-indexed
        public int RangeSum(int[] nums, int n, int left, int right)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int sum = 0;
                for (int j = i; j<nums.Length; j++)
                {
                    sum += nums[j];
                    pq.Enqueue(sum, sum);
                }
            }
            long res = 0;
            long mod = 10_0000_0007;
            int k = 1;
            while (k <= right)
            {
                if (k < left) pq.Dequeue();
                else res += pq.Dequeue();
                k++;
            }
            return (int)(res % mod);
        }

        /// 1512. Number of Good Pairs
        ///Given an array of integers nums, return the number of good pairs.
        ///A pair(i, j) is called good if nums[i] == nums[j] and i<j.
        ///nums = [1,1,1,1], result =6;
        ///1 <= nums.length <= 100, 1 <= nums[i] <= 100
        public int NumIdenticalPairs(int[] nums)
        {
            if (nums == null || nums.Length <= 1)
                return 0;
            int[] arr = new int[100 + 1];
            foreach (var i in nums)
                arr[i]++;
            return arr.Sum(i => i * (i - 1) / 2);
        }

        ///1513. Number of Substrings With Only 1s
        ///Given a binary string s, return the number of substrings with all characters 1's.
        ///Since the answer may be too large, return it modulo 109 + 7.
        public int NumSub(string s)
        {
            long ans = 0;
            long mod = 10_0000_0007;
            Dictionary<long, long> dict = new Dictionary<long, long>();
            long count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if ('1' == s[i]) { count++; }
                else
                {
                    if (count == 0) continue;
                    if (!dict.ContainsKey(count)) { NumSub(count, dict); }
                    ans += dict[count];
                    ans %= mod;
                    count = 0;
                }
            }
            if (!dict.ContainsKey(count)) { NumSub(count, dict); }
            ans += dict[count];
            ans %= mod;
            return (int)(ans % mod);
        }

        public void NumSub(long count, Dictionary<long, long> dict)
        {
            long ans = 0;
            long seed = 0;
            int i = 0;
            while (i <= count)
            {
                ans += seed;
                if (!dict.ContainsKey(i)) dict.Add(i, ans);
                i++;
                seed++;
            }
        }

        ///1514. Path with Maximum Probability
        //find the path with the maximum probability of success to go from start to end
        public double MaxProbability(int n, int[][] edges, double[] succProb, int start, int end)
        {
            double[] res = new double[n];
            res[start]=1;
            List<double[]>[] graph = new List<double[]>[n];
            for (int i = 0; i<n; i++)
                graph[i]=new List<double[]>();

            for(int i=0;i< edges.Length; i++)
            {
                graph[edges[i][0]].Add(new double[] { edges[i][1], succProb[i] });
                graph[edges[i][1]].Add(new double[] { edges[i][0], succProb[i] });
            }

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            while (q.Count > 0)
            {
                int top = q.Dequeue();
                foreach(var i in graph[top])
                {
                    double product = res[top] * i[1];
                    int j = (int)i[0];
                    if (res[j]<product)
                    {
                        res[j] = product;
                        q.Enqueue(j);
                    }
                }
            }
            return res[end];
        }

        ///1518. Water Bottles
        public int NumWaterBottles(int numBottles, int numExchange)
        {
            int res = numBottles;
            while (numBottles >= numExchange)
            {
                int curr = numBottles / numExchange;
                numBottles = numBottles % numExchange;
                res += curr;
                numBottles += curr;
            }
            return res;
        }

        ///1519. Number of Nodes in the Sub-Tree With the Same Label, #DFS
        public int[] CountSubTrees(int n, int[][] edges, string labels)
        {
            int[] res = new int[n];
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i<n; i++)
                graph[i] = new List<int>();

            foreach (var e in edges)
            {
                graph[e[0]].Add(e[1]);
                graph[e[1]].Add(e[0]);
            }

            bool[] visit = new bool[n];
            visit[0] = true;

            int[][] matrix = new int[n][];
            for (int i = 0; i<matrix.Length; i++)
                matrix[i] = new int[26];

            CountSubTrees_DFS(0, labels, graph, visit, matrix, res);
            return res;
        }

        private void CountSubTrees_DFS(int i, string labels, List<int>[] graph, bool[] visit, int[][] matrix, int[] res)
        {
            var arr = graph[i].Where(x => !visit[x]).ToArray();

            foreach (var j in arr)
            {
                visit[j] = true;
                CountSubTrees_DFS(j, labels, graph, visit, matrix, res);
                for (int k = 0; k < 26; k++)
                {
                    matrix[i][k] += matrix[j][k];
                }
            }
            res[i] = ++matrix[i][labels[i] - 'a'];
        }

        ///1523. Count Odd Numbers in an Interval Range
        ///Given two non-negative integers low and high. Return the count of odd numbers between low and high (inclusive).
        public int CountOdds(int low, int high)
        {
            var diff = high - low;
            if (diff % 2 == 0) return diff / 2 + (low % 2 == 1 ? 1 : 0);
            else return diff / 2 + 1;
        }

        ///1525. Number of Good Ways to Split a String
        public int NumSplits(string s)
        {
            int res = 0;
            HashSet<char> set1 = new HashSet<char>();
            HashSet<char> set2 = new HashSet<char>();
            int[] arr1 = new int[s.Length];
            int[] arr2 = new int[s.Length];
            for (int i = 0; i<s.Length; i++)
            {
                set1.Add(s[i]);
                arr1[i] = set1.Count;

                set2.Add(s[s.Length-1- i]);
                arr2[s.Length - 1 - i] = set2.Count;
            }

            for (int i = 0; i<s.Length-1; i++)
            {
                if (arr1[i] == arr2[i + 1])
                    res++;
            }
            return res;
        }

        /// 1526. Minimum Number of Increments on Subarrays to Form a Target Array
        /// In one operation you can choose any subarray from initial and increment each value by one.
        ///Return the minimum number of operations to form a target array from initial.
        public int MinNumberOperations(int[] target)
        {
            int res = 0;
            int pre = 0;
            foreach (var n in target)
            {
                if (n > pre)
                    res += n - pre;
                pre = n;
            }
            return res;
        }

        /// 1528. Shuffle String
        public string RestoreString(string s, int[] indices)
        {
            char[] arr = new char[indices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                arr[indices[i]] = s[i];
            }
            return new string(arr);
        }

        ///1529. Minimum Suffix Flips
        ///In one operation, you can pick an index i where 0 <= i<n
        ///and flip all bits in the inclusive range[i, n - 1]. Flip means changing '0' to '1' and '1' to '0'.
        ///Return the minimum number of operations needed to make s equal to target
        public int MinFlips(string target)
        {
            int res = 0;
            int i = 0;
            int n = target.Length;
            char curr = '0';
            while (i< n)
            {
                if (target[i] != curr)
                {
                    res++;
                    curr = curr == '0' ? '1' : '0';
                }
                i++;
            }
            return res;
        }

        /// 1539. Kth Missing Positive Number, #Binary Search
        //Given an array arr of positive integers sorted in a strictly increasing order, and an integer k.
        //Find the kth positive integer that is missing from this array.
        // public int FindKthPositive(int[] arr, int k)
        // {
        //     int left = 0;
        //     int right = arr.Length;
        //     int mid;
        //     while (left < right)
        //     {
        //         mid = (left + right) / 2;
        //         if (arr[mid] - 1 - mid < k)
        //             left = mid + 1;
        //         else
        //             right = mid;
        //     }
        //     return left + k;
        // }

        ///1544. Make The String Great
        public string MakeGood(string s)
        {
            for (int i = 0; i < s.Length-1; i++)
            {
                if (Math.Abs(s[i] - s[i + 1]) == 32)
                    return MakeGood(s.Substring(0, i) + s.Substring(i + 2, s.Length - i - 2));
            }
            return s;
        }
    }
}