﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///700. Search in a Binary Search Tree, #BST
        ///You are given the root of a binary search tree (BST) and an integer val.
        ///Find the node in the BST equals val and return the subtree rooted with that node.
        ///If such a node does not exist, return null.
        public TreeNode SearchBST(TreeNode root, int val)
        {
            while (root != null)
            {
                if (val == root.val)
                    return root;
                root = val > root.val ? root.right : root.left;
            }
            return root;
        }

        public TreeNode SearchBST_Recursion(TreeNode root, int val)
        {
            if (root == null)
                return null;
            if (root.val == val) return root;
            return root.val > val? SearchBST_Recursion (root.left,val): SearchBST_Recursion(root.right, val);
        }

        ///701. Insert into a Binary Search Tree, #BST
        ///It's guaranteed that val does not exist in the original BST.
        public TreeNode InsertIntoBST(TreeNode root, int val)
        {
            var insert = new TreeNode(val);
            if (root == null)
                return insert;
            var node = root;
            while (node != null)
            {
                if (val > node.val)
                {
                    if (node.right == null)
                    {
                        node.right = insert;
                        break;
                    }
                    else node = node.right;
                }
                else
                {
                    if (node.left == null)
                    {
                        node.left = insert;
                        break;
                    }
                    else node = node.left;
                }
            }
            return root;
        }

        ///703. Kth Largest Element in a Stream, see KthLargest

        /// 704. Binary Search  O(logn), #Binary Search
        ///Given an array of integers nums which is sorted in ascending order,and an integer target
        ///If target exists, then return its index.Otherwise, return -1.
        ///All the integers in nums are unique. 1 <= nums.length <= 104, -104 < nums[i], target < 104
        public int Search_704(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] == target)
                    return mid;
                else if (nums[mid] > target)
                    right = mid-1;
                else
                    left = mid + 1;
            }
            return nums[left] == target ? left : -1;
        }

        ///705. Design HashSet, see MyHashSet

        ///706. Design HashMap, see MyHashMap

        ///707. Design Linked List, see MyLinkedList

        ///709. To Lower Case
        public string ToLowerCase(string s)
        {
            return s.ToLower();
        }

        ///710. Random Pick with Blacklist,see RandomBlacklistPicker


        ///714. Best Time to Buy and Sell Stock with Transaction Fee
        ///https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-transaction-fee/discuss/108870/Most-consistent-ways-of-dealing-with-the-series-of-stock-problems
        public int MaxProfit_714(int[] prices, int fee)
        {
            int len = prices.Length;
            if (len <= 1)
                return 0;

            //buy[i]: Max profit till index i. The series of transaction is ending with a buy.
            //sell[i]: Max profit till index i.The series of transaction is ending with a sell.
            int[] sell = new int[len];
            int[] buy = new int[len];
            //buy[i] = Math.Max(buy[i - 1], sell[i - 2] - prices[i]);
            //sell[i] = Math.Max(sell[i - 1], buy[i - 1] + prices[i]);

            buy[0] = -prices[0];
            sell[0] = 0;
            for (int i = 1; i < len; i++)
            {
                buy[i] = Math.Max(buy[i - 1], sell[i - 1] - prices[i]);
                sell[i] = Math.Max(sell[i - 1], buy[i - 1] + prices[i] - fee);
            }
            return sell[len - 1];
        }

        /// 717. 1-bit and 2-bit Characters
        ///The first character can be represented by one bit 0.
        ///The second character can be represented by two bits (10 or 11).
        ///Given a binary array bits that ends with 0, return true if the last character must be a one-bit character.
        ///1 <= bits.length <= 1000
        public bool IsOneBitCharacter(int[] bits)
        {
            bool odd = true;
            for (int i = bits.Length - 2; i >= 0; i--)
            {
                if (odd && bits[i] == 0)
                    return true;
                if (!odd && bits[i] == 0)
                    return false;
                odd = !odd;
            }
            return odd;
        }

        /// 713. Subarray Product Less Than K, #Sliding Window
        ///return the number of contiguous subarrays where the product of all is less than k.
        ///1 <= nums.length <= 3 * 10^4, 1 <= nums[i] <= 1000, 0 <= k <= 10^6
        public int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            if (k <= 1)
                return 0;

            int res = 0;
            int product = 1;
            int start = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                product *= nums[i];
                while (product >= k && start <= i)
                {
                    product = product / nums[start];
                    start++;
                }
                if (product < k)
                {
                    res += (i - start + 1);//all subarray contain i from [start,i]
                }
            }
            return res;
        }

        ///718. Maximum Length of Repeated Subarray, #DP, #Longest Common Substring
        //return the maximum length of a subarray that appears in both arrays.
        public int FindLength(int[] nums1, int[] nums2)
        {
            int m = nums1.Length;
            int n = nums2.Length;
            int[,] dp = new int[m + 1, n + 1];
            int max = 0;
            for(int i = 1; i <= m; i++)
            {
                for(int j = 1; j <= n; j++)
                {
                    if (nums1[i - 1] == nums2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                        max = Math.Max(max, dp[i, j]);
                    }
                }
            }
            return max;
        }


        ///719. Find K-th Smallest Pair Distance, #Binary Search
        //The distance of a pair of integers a and b is the absolute difference between a and b.
        //return the kth smallest distance among all pairs nums[i] and nums[j] where 0 <= i<j<nums.length.
        //2 <= n <= 10^4, 1 <= k <= n * (n - 1) / 2
        public int SmallestDistancePair(int[] nums, int k)
        {
            int n = nums.Length;
            Array.Sort(nums);
            int max = nums[n - 1] - nums[0];
            if (k == n * (n - 1) / 2)
                return max;
            int min = nums[1] - nums[0];
            for (int i = 1; i < n; i++)
                min = Math.Min(min, nums[i] - nums[i - 1]);
            if (k == 1)
                return min;

            int left=min;
            int right = max;
            while (left < right)
            {
                int mid = (left + right+1) / 2;
                int count = 0;
                for(int i = 0; i < n-1; i++)
                {
                    int target = nums[i]+mid;
                    if (nums[i + 1] >= target) continue;
                    else if (nums[n - 1] < target) count += n - i - 1;//all range [i+1,n-1]
                    else
                    {
                        int low = i + 1;
                        int high = n - 1;
                        while (low < high)
                        {
                            int center = (low + high+1) / 2;
                            if (nums[center] >= target) high = center - 1;
                            else low = center;
                        }
                        count+= low - i;//range [i+1,low]
                    }
                    if (count >= k) break;
                }

                if (count >= k)
                    right = mid - 1;
                else
                    left = mid;
            }
            return left;
        }

        ///720. Longest Word in Dictionary
        public string LongestWord(string[] words)
        {
            Dictionary<int, HashSet<string>> dict = new Dictionary<int, HashSet<string>>();
            foreach(var w in words)
            {
                if (dict.ContainsKey(w.Length)) dict[w.Length].Add(w);
                else dict.Add(w.Length, new HashSet<string>() { w });
            }
            if (!dict.ContainsKey(1)) return string.Empty;
            var curr = dict[1];
            for(int i = 2; i <= 1000; i++)
            {
                if (!dict.ContainsKey(i)) break;
                var next = dict[i].Where(x => curr.Contains(x.Substring(0, x.Length - 1))).ToHashSet();
                if (next.Count > 0) curr = next;
                else break;
            }
            return curr.OrderBy(x => x).First();
        }

        ///721. Accounts Merge, #Union Find
        //each list is {name, mails}, if two list contains same mail ,they belong to same person
        //diffierent people may own same name
        public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            int n = accounts.Count();
            var uf = new UnionFind(n);

            var dict = new Dictionary<string, int>();

            for(int i = 0; i < n; i++)
            {
                HashSet<int> set = new HashSet<int>();
                set.Add(i);
                for(int j = 1; j < accounts[i].Count(); j++)
                {
                    if (dict.ContainsKey(accounts[i][j]))
                    {
                        set.Add(dict[accounts[i][j]]);
                    }
                    else
                    {
                        dict.Add(accounts[i][j], i);
                    }
                }
                foreach(var k1 in set)
                {
                    foreach(var k2 in set)
                    {
                        uf.Union(k1, k2);
                    }
                }
            }

            var map = new Dictionary<int, HashSet<string>>();
            for(int i = 0; i < n; i++)
            {
                int k = uf.Find(i);
                if (!map.ContainsKey(k))
                {
                    map.Add(k, new HashSet<string>());
                    map[k].Add(accounts[i][0]);
                }
                for (int j = 1; j < accounts[i].Count(); j++)
                    map[k].Add(accounts[i][j]);
            }

            var res=new List<IList<string>>();
            foreach (var set in map.Values)
            {
                string name= set.ElementAt(0);
                var mails = set.Skip(1).ToList();
                mails.Sort((x, y) => string.CompareOrdinal(x, y));
                mails.Insert(0, name);
                res.Add(mails);
            }
            return res;
        }

        ///724. Find Pivot Index
        // leftSum = rightSum , exclude current number
        public int PivotIndex(int[] nums)
        {
            int sum = nums.Sum();
            int curr = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if (curr == sum - curr - nums[i]) return i;
                curr += nums[i];
            }
            return -1;
        }
        /// 725. Split Linked List in Parts
        ///The number of nodes in the list is in the range [0, 1000]. 1 <= k <= 50
        public ListNode[] SplitListToParts(ListNode head, int k)
        {
            var ans = new ListNode[k];
            int count = 0;
            var node = head;
            while (node != null)
            {
                count++;
                node = node.next;
            }

            if (count == 0 || k == 1)
            {
                ans[0] = head;
            }
            else
            {
                int len = count / k;
                int plusIndex = count - len * k;
                node = head;
                for (int i = 0; i < ans.Length; i++)
                {
                    int num = len;
                    if (i < plusIndex) num++;
                    int j = 0;
                    ListNode currHead = null;
                    ListNode currTail = null;
                    while (j++ < num)
                    {
                        if (currHead == null)
                        {
                            currHead = node;
                            currTail = node;
                        }
                        else
                        {
                            currTail.next = node;
                            currTail = currTail.next;
                        }
                        node = node.next;
                    }
                    if (currTail != null) currTail.next = null;
                    ans[i] = currHead;
                }
            }
            return ans;
        }

        /// 728
        public IList<int> SelfDividingNumbers(int left, int right)
        {
            List<int> result = new List<int>();
            for (int i = left; i <= right; i++)
            {
                bool pass = false;
                int temp = i;
                while (temp > 0)
                {
                    int mod = temp % 10;
                    if (mod == 0 || i % mod != 0)
                    {
                        pass = true;
                        break;
                    }
                    temp /= 10;
                }
                if (pass) continue;

                result.Add(i);
            }

            return result;
        }

        public bool IsSelfDivided(int num)
        {
            List<int> divid = new List<int>();
            int full = num;
            while (full > 0)
            {
                divid.Add(full % 10);
                full /= 10;
            }

            foreach (var i in divid)
            {
                if (i == 0 || num % i != 0) return false;
            }
            return true;
        }

        ///729. My Calendar I, see MyCalendar

        /// 733. Flood Fill, #BFS
        public int[][] FloodFill(int[][] image, int sr, int sc, int newColor)
        {
            int color = image[sr][sc];
            if (color == newColor)
                return image;
            int rowLen = image.Length;
            int colLen = image[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { sr, sc });
            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                if (image[point[0]][point[1]] == color)
                {
                    image[point[0]][point[1]] = newColor;
                    foreach (var d in dxy4)
                    {
                        var r = point[0] + d[0];
                        var c = point[1] + d[1];
                        if (r >= 0 && r < rowLen && c >= 0 && c < colLen && image[r][c] == color)
                        {
                            queue.Enqueue(new int[] { r, c });
                        }
                    }
                }
            }
            return image;
        }

        ///735. Asteroid Collision
        //(positive meaning right, negative meaning left). Each asteroid moves at the same speed.
        //If two asteroids meet, the smaller one will explode.If both are the same size, both will explode.
        //Find out the state of the asteroids after all collisions.
        public int[] AsteroidCollision(int[] asteroids)
        {
            Stack<int> stack = new Stack<int>();
            for(int i=0;i < asteroids.Length; i++)
            {
                if(asteroids[i] > 0)
                {
                    stack.Push(asteroids[i]);
                }
                else
                {
                    while(stack.Count>0 && stack.Peek() > 0)
                    {
                        var top = stack.Peek();
                        if (top + asteroids[i] == 0)
                        {
                            stack.Pop();
                            asteroids[i] = 0;
                            break;
                        }
                        else if(top + asteroids[i] > 0)
                        {
                            asteroids[i] = 0;
                            break;
                        }
                        else
                        {
                            stack.Pop();
                        }
                    }
                    if (asteroids[i] < 0)
                        stack.Push(asteroids[i]);
                }
            }

            int[] res = new int[stack.Count];
            for (int i = res.Length - 1; i >= 0; i--)
                res[i] = stack.Pop();
            return res;

        }

        ///739. Daily Temperatures, #Monotonic Stack
        ///Given an array of integers temperatures represents the daily temperatures,
        ///return an array answer such that answer[i] is the number of days you have to wait after the ith day to get a warmer temperature.
        ///If there is no future day for which this is possible, keep answer[i] == 0 instead.

        public int[] DailyTemperatures(int[] temperatures)
        {
            int n = temperatures.Length;
            Stack<int> stack = new Stack<int>();
            int[] res = new int[n];
            for (int i = 0; i < n; i++)
            {
                while (stack.Count > 0 && temperatures[i] > temperatures[stack.Peek()])
                {
                    int idx = stack.Pop();
                    res[idx] = i - idx;
                }
                stack.Push(i);
            }
            return res;
        }

        /// 740. Delete and Earn
        ///eat nums[i] will earn all nums[i] but delete nums[i]+1 and nums[i]-1, return the max of Earn
        ///1 <= nums[i] <= 10^4
        public int DeleteAndEarn(int[] nums)
        {
            int[] arr = new int[10001];
            int[] dp = new int[10001];
            int max = 0;
            int min = 10000;
            for (int i = 0; i < nums.Length; i++)
            {
                arr[nums[i]]++;
                max = Math.Max(max, nums[i]);
                min = Math.Min(min, nums[i]);
            }
            if (max == min)
                return arr[max] * max;

            int j = min;
            dp[j] = arr[j] * j;
            dp[j + 1] = Math.Max(dp[j], arr[j + 1] * (j + 1));
            j += 2;
            while (j <= max)
            {
                dp[j] = Math.Max(dp[j - 1], dp[j - 2] + arr[j] * (j));
                j++;
            }
            return dp[max];
        }

        ///743. Network Delay Time, #Graph, #BFS
        ///Return the time it takes for all the n nodes to receive the signal.
        ///If it is impossible for all the n nodes to receive the signal, return -1.
        public int NetworkDelayTime(int[][] times, int n, int k)
        {
            int[][] graph = new int[n][];
            for(int i=0;i<n; ++i)
            {
                graph[i] = new int[n];
                for (int j = 0; j < n; ++j)
                    graph[i][j] = -1;
            }

            foreach (var time in times)
                graph[time[0] - 1][time[1] - 1] = time[2];

            int[] res = new int[n];
            for (int i = 0; i < n; ++i)
                res[i] = int.MaxValue;

            var list = new List<int>() {  k - 1 };
            res[k - 1] = 0;
            while (list.Count > 0)
            {
                var next = new List<int>();
                foreach(var p in list)
                {
                    for(int i = 0; i < n; ++i)
                    {
                        if (graph[p][i] == -1) continue;
                        if (res[i] > res[p] + graph[p][i])
                        {
                            res[i] = res[p] + graph[p][i];
                            if(!next.Contains(i))
                                next.Add(i);
                        }
                    }
                }
                list = next;
            }

            int max = int.MinValue;
            for(int i = 0; i < n; ++i)
            {
                if (res[i] == int.MaxValue) return -1;
                max = Math.Max(max, res[i]);
            }
            return max;
        }


        /// 744. Find Smallest Letter Greater Than Target, #Binary Search
        //letters are wrap around, eg. a>z
        public char NextGreatestLetter(char[] letters, char target)
        {
            if (target >= letters[letters.Length - 1] || target < letters[0])
                return letters[0];

            int n = letters.Length;
            int left = 0, right = n - 1;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (letters[mid] > target) right = mid;
                else left = mid + 1;
            }
            return letters[left];
        }

        ///745. Prefix and Suffix Search, see WordFilter

        /// 746. Min Cost Climbing Stairs, #DP
        ///cost[i] is the cost of ith step on a staircase. pay the cost, you can either climb one or two steps.
        ///You can either start from the step with index 0, or the step with index 1
        ///Return the minimum cost to reach the top of the floor. 2 <= cost.length <= 1000
        public int MinCostClimbingStairs(int[] cost)
        {
            int n = cost.Length;
            int[] dp = new int[n+1];
            dp[1] = cost[0];
            dp[2] = cost[1];
            for (int i = 3; i <= n; i++)
            {
                dp[i] = cost[i-1] + Math.Min(dp[i - 2], dp[i - 1]);
            }
            return Math.Min(dp[n - 1], dp[n]);
        }

        ///747. Largest Number At Least Twice of Others, #PriorityQueue
        ///Determine whether the largest element in the array is at least twice as much as every other number
        ///If it is, return the index of the largest element, or return -1 otherwise.
        public int DominantIndex(int[] nums)
        {
            if (nums.Length == 1) return 0;
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                pq.Enqueue(i, -nums[i]);
            }
            int maxIndex =  pq.Dequeue();
            int secondIndex = pq.Dequeue();
            return nums[maxIndex] >= nums[secondIndex] * 2 ? maxIndex : -1;
        }

        ///748. Shortest Completing Word
        public string ShortestCompletingWord(string licensePlate, string[] words)
        {
            var res = string.Empty;
            int[] arr = new int[26];
            foreach(var c in licensePlate)
            {
                if (char.IsLetter(c))
                    arr[char.ToLower(c) - 'a']++;
            }

            foreach(var word in words)
            {
                int[] curr = new int[26];
                foreach (var c in word)
                {
                    if (char.IsLetter(c))
                        curr[char.ToLower(c) - 'a']++;
                }
                bool find = true;
                for(int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] > 0 && arr[i] > curr[i])
                    {
                        find = false;
                        break;
                    }
                }

                if (find)
                {
                    if(res.Length==0 || word.Length < res.Length)
                    {
                        res = word;
                    }
                }
            }

            return res;
        }

    }
}