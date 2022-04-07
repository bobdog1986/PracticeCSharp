using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1603. Design Parking System

        ///1608. Special Array With X Elements Greater Than or Equal X
        ///Return x if the array is special, otherwise, return -1.
        ///0 <= nums[i] <= 1000
        public int SpecialArray(int[] nums)
        {
            int[] arr = new int[1001];
            foreach(var n in nums)
                arr[n]++;

            int count = 0;
            for(int i = 1000; i >= 0; i--)
            {
                count += arr[i];
                if (count == i) return i;
                else if (count > i) break;
            }
            return -1;
        }

        public int SpecialArray_BinarySearch(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums);
            //<= n
            for (int i = 0; i <= n; i++)
            {
                int left = 0, right = n;
                while (left < right)
                {
                    int mid = left + (right - left) / 2;
                    if (nums[mid] >= i) right = mid;
                    else left = mid + 1;
                }
                if ((n - left) == i) return i;//find how many num >=i
            }
            return -1;
        }
        /// 1609. Even Odd Tree, #BFS, #BTree
        ///A binary tree is named Even-Odd if it meets the following conditions:
        ///odd-indexed level, all nodes at the level have even integer values in strictly decreasing order(from left to right).
        ///even-indexed level, all nodes at the level have odd integer values in strictly increasing order (from left to right).
        public bool IsEvenOddTree(TreeNode root)
        {
            List<TreeNode> list =new List<TreeNode>() { root};
            bool odd = false;
            while(list.Count > 0)
            {
                List<TreeNode> next = new List<TreeNode>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (odd)
                    {
                        if (list[i].val % 2 == 1) return false;
                        if (i < list.Count - 1 && list[i].val <= list[i + 1].val) return false;
                    }
                    else
                    {
                        if (list[i].val % 2 == 0) return false;
                        if (i < list.Count - 1 && list[i].val >= list[i + 1].val) return false;
                    }
                    if (list[i].left != null) next.Add(list[i].left);
                    if (list[i].right != null) next.Add(list[i].right);
                }
                odd = !odd;
                list = next;
            }
            return true;
        }

        ///1615. Maximal Network Rank, #Graph
        ///Given the integer n and the array roads, return the maximal network rank of the entire infrastructure.
        ///two cities donot need connected
        public int MaximalNetworkRank(int n, int[][] roads)
        {
            int max = 0;
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();

            foreach (var road in roads)
            {
                graph[road[0]].Add(road[1]);
                graph[road[1]].Add(road[0]);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (graph[i].Contains(j))
                        max = Math.Max(max, graph[i].Count + graph[j].Count - 1);
                    else
                        max = Math.Max(max, graph[i].Count + graph[j].Count);
                }

            }
            return max;
        }
        ///1630. Arithmetic Subarrays
        ///Return a list of boolean elements answer, where answer[i] is true if the subarray
        ///nums[l[i]], nums[l[i]+1], ... , nums[r[i]] can be rearranged to form an arithmetic sequence or false.
        public IList<bool> CheckArithmeticSubarrays_QuickSort(int[] nums, int[] l, int[] r)
        {
            bool[] res=new bool[l.Length];
            for(int i= 0; i < l.Length; i++)
            {
                int len = r[i] -l[i]+1;
                int[] arr=new int[len];
                for(int j = l[i]; j <= r[i]; j++)
                {
                    arr[j - l[i]] = nums[j];
                }

                res[i] = CheckArithmeticSubarrays_QuickSort(arr);
            }
            return res.ToList();
        }
        private bool CheckArithmeticSubarrays_QuickSort(int[] arr)
        {
            if (arr.Length <= 2) return true;

            Array.Sort(arr);
            int diff = arr[1] - arr[0];
            for(int i = 2; i < arr.Length; i++)
            {
                if(arr[i] - arr[i-1]!= diff) return false;
            }
            return true;
        }

        public IList<bool> CheckArithmeticSubarrays(int[] nums, int[] l, int[] r)
        {
            bool[] res = new bool[l.Length];
            for (int i = 0; i < l.Length; i++)
            {
                res[i] = CheckArithmeticSubarrays_IsArithmeticSeq(nums, l[i], r[i]);
            }
            return res.ToList();
        }

        private bool CheckArithmeticSubarrays_IsArithmeticSeq(int[] nums, int start, int end)
        {
            if (end - start < 2) return true;

            int min = int.MaxValue, max = int.MinValue;
            HashSet<int> set = new HashSet<int>();
            for (int i = start; i <= end; i++)
            {
                min = Math.Min(min, nums[i]);
                max = Math.Max(max, nums[i]);
                set.Add(nums[i]);
            }

            if ((max - min) % (end - start) != 0) return false;
            int interval = (max - min) / (end - start);
            for (int i = 1; i <= end - start; i++)
            {
                if (!set.Contains(min + i * interval)) return false;
            }
            return true;
        }
        /// 1636. Sort Array by Increasing Frequency
        ///sort the array in increasing order based on the frequency of the values.
        ///If multiple values have the same frequency, sort them in decreasing order.
        public int[] FrequencySort(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var n in nums)
            {
                if(dict.ContainsKey(n))dict[n]++;
                else dict.Add(n, 1);
            }
            var keys=dict.Keys.OrderBy(x=>dict[x]).ThenBy(x=>-x);
            var res = new int[nums.Length];
            int i = 0;
            foreach(var key in keys)
            {
                while (dict[key]-- > 0)
                {
                    res[i++] = key;
                }
            }
            return res;
        }
    }
}
