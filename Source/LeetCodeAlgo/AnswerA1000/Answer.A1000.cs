﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1002. Find Common Characters
        ///return an array of all characters that show up in all strings within the words (including duplicates).
        public IList<string> CommonChars(string[] words)
        {
            var mat = new List<int[]>();
            foreach (var w in words)
            {
                var arr = new int[26];
                foreach (var c in w)
                    arr[c - 'a']++;
                mat.Add(arr);
            }
            List<string> res = new List<string>();
            for (int i = 0; i < 26; i++)
            {
                int min = 100;
                foreach (var arr in mat)
                {
                    if (arr[i] == 0)
                    {
                        min = 0;
                        break;
                    }
                    else
                    {
                        min = Math.Min(min, arr[i]);
                    }
                }
                while (min-- > 0)
                    res.Add(((char)('a' + i)).ToString());
            }
            return res;
        }
        ///1003. Check If Word Is Valid After Substitutions
        public bool IsValid_1003(string s)
        {
            if (s.Length % 3 != 0) return false;
            var stack = new Stack<string>();
            foreach (var c in s)
            {
                if (c == 'a') stack.Push("a");
                else if (c == 'b')
                {
                    if (stack.Count == 0) return false;
                    else
                    {
                        if (stack.Peek() != "a") return false;
                        else stack.Push(stack.Pop() + "b");
                    }
                }
                else
                {
                    if (stack.Count == 0) return false;
                    else
                    {
                        if (stack.Peek() != "ab") return false;
                        else stack.Pop();
                    }
                }
            }
            return stack.Count == 0;
        }


        /// 1004. Max Consecutive Ones III, #Sliding Window
        //return the maximum number of consecutive 1's in the array if you can flip at most k 0's.
        public int LongestOnes(int[] nums, int k)
        {
            int max = 0;
            int left = 0;//left index, i is right index
            int zeroCount = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    zeroCount++;
                    while (zeroCount > k && left <= i)
                    {
                        if (nums[left] == 0) { zeroCount--; }
                        left++;
                    }
                }
                max = Math.Max(max, i - left + 1);
            }
            return max;
        }

        ///1005. Maximize Sum Of Array After K Negations, #PriorityQueue
        public int LargestSumAfterKNegations(int[] nums, int k)
        {
            var pq = new PriorityQueue<int, int>();
            foreach (var n in nums)
                pq.Enqueue(n, n);
            while (k-- > 0)
            {
                var top = pq.Dequeue();
                pq.Enqueue(-top, -top);
            }
            int sum = 0;
            while (pq.Count > 0)
                sum += pq.Dequeue();
            return sum;
        }
        /// 1007. Minimum Domino Rotations For Equal Row
        ///Return the minimum number of rotations so that all the values in tops are the same,
        ///or all the values in bottoms are the same.If it cannot be done, return -1.
        public int MinDominoRotations(int[] tops, int[] bottoms)
        {
            int n = tops.Length;
            for (int i = 0, a = 0, b = 0; i < n && (tops[i] == tops[0] || bottoms[i] == tops[0]); ++i)
            {
                if (tops[i] != tops[0]) a++;
                if (bottoms[i] != tops[0]) b++;
                if (i == n - 1) return Math.Min(a, b);
            }
            for (int i = 0, a = 0, b = 0; i < n && (tops[i] == bottoms[0] || bottoms[i] == bottoms[0]); ++i)
            {
                if (tops[i] != bottoms[0]) a++;
                if (bottoms[i] != bottoms[0]) b++;
                if (i == n - 1) return Math.Min(a, b);
            }
            return -1;
        }
        ///1008. Construct Binary Search Tree from Preorder Traversal, #BTree, #BST
        public TreeNode BstFromPreorder(int[] preorder)
        {
            int i = 0;
            return BstFromPreorder_O_N_Recurr(preorder, int.MaxValue, ref i);
        }
        public TreeNode BstFromPreorder_O_N_Recurr(int[] preorder, int bound, ref int index)
        {
            if (index >= preorder.Length || preorder[index] > bound) return null;
            var node = new TreeNode(preorder[index++]);
            node.left = BstFromPreorder_O_N_Recurr(preorder, node.val, ref index);
            node.right = BstFromPreorder_O_N_Recurr(preorder, bound, ref index);
            return node;
        }

        public TreeNode BstFromPreorder_N2(int[] preorder)
        {
            return BstFromPreorder_N2_Recurr(preorder, 0, preorder.Length - 1);
        }
        public TreeNode BstFromPreorder_N2_Recurr(int[] preorder, int left, int right)
        {
            if (left > right) return null;
            else if (left == right) return new TreeNode(preorder[left]);
            else
            {
                var node = new TreeNode(preorder[left]);
                int i = left + 1;
                while (i <= right)
                {
                    if (preorder[i] < preorder[left])
                        i++;
                    else
                        break;
                }
                node.left = BstFromPreorder_N2_Recurr(preorder, left + 1, i - 1);
                node.right = BstFromPreorder_N2_Recurr(preorder, i, right);
                return node;
            }
        }

        ///1009. Complement of Base 10 Integer
        public int BitwiseComplement(int n)
        {
            if (n == 0) return 1;
            int res = 0;
            int seed = 1;
            while (n > 0)
            {
                if ((n & 1) == 0) res += seed;
                seed <<= 1;
                n >>= 1;
            }
            return res;
        }

        ///1010. Pairs of Songs With Total Durations Divisible by 60
        //Return the number of pairs of songs for which their total duration in seconds is divisible by 60.
        //Formally, we want the number of indices i, j such that i < j with (time[i] + time[j]) % 60 == 0.
        public int NumPairsDivisibleBy60(int[] time)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < 60; i++)
                dict.Add(i, 0);
            long res = 0;
            foreach (var n in time)
            {
                int mod = n % 60;
                dict[mod]++;
            }
            if (dict[0] >= 2)
            {
                res += getFactorial(dict[0], 2) / 2;
            }
            if (dict[30] >= 2)
            {
                res += getFactorial(dict[30], 2) / 2;
            }
            for (int i = 1; i <= 29; i++)
            {
                res += dict[i] * dict[60 - i];
            }
            return (int)res;
        }

        ///1013. Partition Array Into Three Parts With Equal Sum
        public bool CanThreePartsEqualSum(int[] arr)
        {
            int sum = arr.Sum();
            if (sum % 3 != 0) return false;
            return CanThreePartsEqualSum(arr, 0, sum / 3, 0);
        }

        private bool CanThreePartsEqualSum(int[] arr, int index, int target, int count)
        {
            int n = arr.Length;
            if (count == 2)
            {
                if (index == arr.Length) return false;
                int sum = 0;
                while (index < n)
                {
                    sum += arr[index];
                    index++;
                }
                return sum == target;
            }
            else
            {
                int sum = 0;
                for (; index < n; index++)
                {
                    sum += arr[index];
                    if (sum == target)
                    {
                        if (CanThreePartsEqualSum(arr, index + 1, target, count + 1))
                            return true;
                    }
                }
                return false;
            }
        }

        /// 1014. Best Sightseeing Pair
        ///find max = values[i] + values[j] + i - j, 1 <= values[i] <= 1000
        public int MaxScoreSightseeingPair(int[] values)
        {
            //bestPoint =Max( value[i] + i)
            int maxv = Math.Max(values[0], values[1] + 1);
            int ans = values[1] + values[0] - 1;
            for (int i = 2; i < values.Length; i++)
            {
                ans = Math.Max(ans, maxv + values[i] - i);
                maxv = Math.Max(maxv, values[i] + i);
            }
            return ans;
        }

        ///1015. Smallest Integer Divisible by K
        //find the length of the smallest positive integer n such that n is divisible by k, and n only contains digit 1.
        //Return the length of n.If there is no such n, return -1. eg, k=3, n=111, return length of (111)
        //Note: n may not fit in a 64-bit signed integer. 1 <= k <= 10^5
        public int SmallestRepunitDivByK(int k)
        {
            //only hold the reminder
            //If N exist, N <= K, just do a brute force check.
            //Also if K % 2 == 0, return -1, because 111....11 can't be even.
            //Also if K % 5 == 0, return -1, because 111....11 can't end with 0 or 5.
            if (k % 2 == 0 || k % 5 == 0) return -1;
            int reminder = 0;
            for (int N = 1; N <= k; ++N)
            {
                reminder = (reminder * 10 + 1) % k;
                if (reminder == 0) return N;//N is length
            }
            return -1;
        }
        ///1018. Binary Prefix Divisible By 5
        public IList<bool> PrefixesDivBy5(int[] nums)
        {
            var res = new List<bool>();
            int curr = 0;
            foreach (var n in nums)
            {
                curr = (curr << 1) + n;
                res.Add(curr % 5 == 0);
                curr %= 100;
            }
            return res;
        }

        ///1019. Next Greater Node In Linked List, #Monotonic Stack
        //find the next greater value of current listnode
        public int[] NextLargerNodes(ListNode head)
        {
            var curr = head;
            List<int> list = new List<int>();
            while (curr != null)
            {
                list.Add(curr.val);
                curr = curr.next;
            }
            int[] res = new int[list.Count];
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < list.Count; i++)
            {
                while (stack.Count > 0 && list[stack.Peek()] < list[i])
                {
                    res[stack.Pop()] = list[i];
                }
                stack.Push(i);
            }
            while (stack.Count > 0)
            {
                res[stack.Pop()] = 0;
            }
            return res;
        }

        /// 1020. Number of Enclaves, #DFS, #Graph
        ///Return the number of land cells in grid for which we cannot walk off the boundary of the grid in any number of moves.
        public int NumEnclaves(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            int ans = 0;
            bool[,] visit = new bool[rowLen, colLen];
            for (int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == 0 || visit[i, j]) continue;
                    Queue<int[]> q = new Queue<int[]>();
                    q.Enqueue(new int[] { i, j });
                    visit[i, j] = true;
                    int count = 1;
                    bool ignore = false;
                    while (q.Count > 0)
                    {
                        var p = q.Dequeue();
                        if (p[0] == 0 || p[0] == rowLen - 1 || p[1] == 0 || p[1] == colLen - 1)
                            ignore = true;
                        foreach (var d in dxy4)
                        {
                            var r = p[0] + d[0];
                            var c = p[1] + d[1];
                            if (r >= 0 && r < rowLen && c >= 0 && c < colLen && grid[r][c] == 1 && !visit[r, c])
                            {
                                visit[r, c] = true;
                                count++;
                                q.Enqueue(new int[] { r, c });
                            }
                        }
                    }
                    if (!ignore)
                        ans += count;
                }
            return ans;
        }

        ///1021. Remove Outermost Parentheses
        public string RemoveOuterParentheses(string s)
        {
            StringBuilder sb = new StringBuilder();
            int left = 0;
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(') count++;
                else count--;
                if (count == 0)
                {
                    sb.Append(s.Substring(left + 1, i - (left + 1)));
                    left = i + 1;
                }
            }
            return sb.ToString();
        }
        ///1022. Sum of Root To Leaf Binary Numbers,#BTree
        public int SumRootToLeaf(TreeNode root)
        {
            int res = 0;
            SumRootToLeaf(root, 0, ref res);
            return res;
        }

        private void SumRootToLeaf(TreeNode root, int curr, ref int res)
        {
            if (root == null)
            {
                return;
            }
            else
            {
                curr = (curr << 1) + root.val;
                if (root.left == null && root.right == null)
                {
                    res += curr;
                }
                else
                {
                    SumRootToLeaf(root.left, curr, ref res);
                    SumRootToLeaf(root.right, curr, ref res);
                }
            }
        }
        ///1023. Camelcase Matching
        //can insert any lower chars to queries[i]
        public IList<bool> CamelMatch(string[] queries, string pattern)
        {
            int n = queries.Length;
            bool[] res = new bool[n];
            for (int i = 0; i < queries.Length; i++)
            {
                res[i] = CamelMatch(queries[i], pattern);
            }

            return res.ToList();
        }

        /// 1024. Video Stitching, #Greedy
        //clips[i] = [starti, endi] indicates that the ith clip started at starti and ended at endi.
        //For example, a clip [0, 7] can be cut into segments[0, 1] + [1, 3] + [3, 7].
        //Return the minimum number of clips needed so that we can cut the clips into segments
        //that cover the entire sporting event [0, time]. If the task is impossible, return -1.
        public int VideoStitching(int[][] clips, int time)
        {
            clips = clips.OrderBy(x => x[0]).ThenBy(x => -x[1]).ToArray();
            if (clips[0][0] > 0) return -1;
            int curr = clips[0][1];
            int res = 1;
            int n = clips.Length;
            for (int i = 1; i < n && curr < time; i++)
            {
                if (clips[i][0] > curr) return -1;
                else if (clips[i][1] < curr) continue;
                else
                {
                    int j = i;
                    int max = curr;
                    while (j < n)
                    {
                        if (clips[j][0] > curr) break;
                        max = Math.Max(max, clips[j++][1]);
                    }
                    res++;
                    curr = max;
                    i = j - 1;
                }
            }
            return curr >= time ? res : -1;
        }

        private bool CamelMatch(string query, string pattern)
        {
            int i = 0;
            int j = 0;
            while (i < query.Length && j < pattern.Length)
            {
                if (query[i] == pattern[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    if (char.IsUpper(query[i])) return false;
                    else
                    {
                        i++;
                    }
                }
            }

            if (j == pattern.Length)
            {
                while (i < query.Length)
                {
                    if (char.IsUpper(query[i])) return false;
                    else i++;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        /// 1026. Maximum Difference Between Node and Ancestor, #BTree
        public int MaxAncestorDiff(TreeNode root)
        {
            int res = 0;
            MaxAncestorDiff(root, int.MinValue, int.MaxValue, ref res);
            return res;
        }

        private void MaxAncestorDiff(TreeNode root, int max, int min, ref int res)
        {
            if (root == null) return;
            if (max != int.MinValue)
                res = Math.Max(res, Math.Abs(max - root.val));
            if (min != int.MaxValue)
                res = Math.Max(res, Math.Abs(min - root.val));
            max = Math.Max(max, root.val);
            min = Math.Min(min, root.val);
            MaxAncestorDiff(root.left, max, min, ref res);
            MaxAncestorDiff(root.right, max, min, ref res);
        }
        /// 1028. Recover a Tree From Preorder Traversal
        public TreeNode RecoverFromPreorder(string traversal)
        {
            return RecoverFromPreorder(traversal, 1);
        }

        public TreeNode RecoverFromPreorder(string str, int count)
        {

            int sum = 0;
            int leftStart = -1;
            int rightStart = -1;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != '-')
                {
                    if (sum == count)
                    {
                        if (leftStart == -1)
                        {
                            leftStart = i;
                        }
                        else if (rightStart == -1)
                        {
                            rightStart = i;
                            break;
                        }
                    }
                    sum = 0;
                }
                else
                {
                    sum++;
                }
            }

            if (leftStart == -1 && rightStart == -1)
            {
                return new TreeNode(int.Parse(str));
            }
            else if (rightStart == -1)
            {
                var val = int.Parse(str.Substring(0, leftStart - count));
                var leftNode = RecoverFromPreorder(str.Substring(leftStart), count + 1);
                var res = new TreeNode(val);
                res.left = leftNode;
                return res;
            }
            else
            {
                var val = int.Parse(str.Substring(0, leftStart - count));
                var leftNode = RecoverFromPreorder(str.Substring(leftStart, rightStart - count - leftStart), count + 1);
                var rightNode = RecoverFromPreorder(str.Substring(rightStart), count + 1);
                var res = new TreeNode(val);
                res.left = leftNode;
                res.right = rightNode;
                return res;
            }
        }
        /// 1029. Two City Scheduling
        ///A company is planning to interview 2n people. Given the array costs where costs[i] = [aCosti, bCosti],
        ///the cost of flying the ith person to city a is aCosti, and the cost of flying the ith person to city b is bCosti.
        ///Return the minimum cost to fly every person to a city such that exactly n people arrive in each city.
        public int TwoCitySchedCost(int[][] costs)
        {
            int res = costs.Sum(x => x[0]);
            int[] diff = costs.Select(x => x[1] - x[0]).ToArray();
            Array.Sort(diff);
            for (int i = 0; i < diff.Length / 2; i++)
                res += diff[i];
            return res;
        }
        ///1030. Matrix Cells in Distance Order
        public int[][] AllCellsDistOrder(int rows, int cols, int rCenter, int cCenter)
        {
            PriorityQueue<int[], int> pq = new PriorityQueue<int[], int>();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    pq.Enqueue(new int[] { i, j }, Math.Abs(rCenter - i) + Math.Abs(cCenter - j));
                }
            }
            var res = new int[rows * cols][];
            int index = 0;
            while (pq.Count > 0)
                res[index++] = pq.Dequeue();
            return res;
        }

        ///1034. Coloring A Border, #BFS
        //border cell is on edge(r[0],r[m-1],c[0],c[n-1]) or any adjacent not same color
        public int[][] ColorBorder(int[][] grid, int row, int col, int color)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            var visit = initVisitMatrix(m, n);
            var dxy4 = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
            var queue = new Queue<int[]>();
            queue.Enqueue(new int[] { row, col });

            visit[row][col] = true;
            var list = new List<int[]>();
            while (queue.Count > 0)
            {
                int size = queue.Count;
                while (size-- > 0)
                {
                    var cell = queue.Dequeue();
                    bool isBorder = cell[0] == 0 || cell[1] == 0 || cell[0] == m - 1 || cell[1] == n - 1;
                    foreach (var d in dxy4)
                    {
                        var r = cell[0] + d[0];
                        var c = cell[1] + d[1];
                        if (r >= 0 && c >= 0 && r < m && c < n)
                        {
                            if (grid[r][c] == grid[row][col])
                            {
                                if (!visit[r][c])
                                {
                                    visit[r][c] = true;
                                    queue.Enqueue(new int[] { r, c });
                                }
                            }
                            else isBorder = true;
                        }
                    }
                    if (isBorder)
                        list.Add(cell);
                }
            }
            foreach (var cell in list)
                grid[cell[0]][cell[1]] = color;
            return grid;
        }

        ///1035. Uncrossed Lines , #DP
        /// straight line connecting two numbers nums1[i] and nums2[j] such that:
        //nums1[i] == nums2[j], and the line we draw does not intersect any other connecting(non-horizontal) line.
        //Return the maximum number of connecting lines we can draw in this way.
        public int MaxUncrossedLines(int[] nums1, int[] nums2)
        {
            //The Longest Common Subsequence
            int m = nums1.Length;
            int n = nums2.Length;
            var dp = new int[m + 1, n + 1];
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (nums1[i - 1] == nums2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }
            return dp[m, n];
        }

        /// 1037. Valid Boomerang
        public bool IsBoomerang(int[][] points)
        {
            var x = points[0];
            var y = points[1];
            var z = points[2];

            if ((x[0] == y[0] && x[1] == y[1])
                || (x[0] == z[0] && x[1] == z[1])
                || (y[0] == z[0] && y[1] == z[1]))
                return false;

            if (x[1] == y[1] && x[1] == z[1]) return false;
            if (x[0] == y[0] && x[0] == z[0]) return false;

            var line1 = IsBoomerang_GetLine(x, y);
            var line2 = IsBoomerang_GetLine(x, z);
            if (line1[0] == line2[0] && line1[1] == line2[1])
                return false;

            return true;
        }

        private double[] IsBoomerang_GetLine(int[] p1, int[] p2)
        {
            if (p1[1] == p2[1])
            {
                return new double[] { 0, p1[1] };
            }
            else
            {
                double a = 1.0 * (p1[1] - p2[1]) / (p1[0] - p2[0]);
                double b = 1.0 * (p2[0] * p1[1] - p1[0] * p2[1]) / (p2[0] - p1[0]);
                return new double[] { a, b };
            }
        }

        /// 1038. Binary Search Tree to Greater Sum Tree, #BTree
        public TreeNode BstToGst(TreeNode root)
        {
            BstToGst_Recursion(root, 0);
            return root;
        }

        private int BstToGst_Recursion(TreeNode root, int sum)
        {
            if (root == null)
                return sum;
            var rightVal = BstToGst_Recursion(root.right, sum);
            root.val += rightVal;
            return BstToGst_Recursion(root.left, root.val);
        }

        ///1042. Flower Planting With No Adjacent, #Greedy
        //for any two gardens connected by a path, they have different types of flowers.
        //Return any answer, where answer[i] is the type of flower planted in the (i+1)th garden.
        //The flower types are denoted 1, 2, 3, or 4
        public int[] GardenNoAdj(int n, int[][] paths)
        {
            //Create a graph
            var graph = initListGraph(n);

            //Add the edges
            foreach (var path in paths)
            {
                int x = path[0] - 1;
                int y = path[1] - 1;
                graph[x].Add(y);
                graph[y].Add(x);
            }

            int[] res = new int[n];
            //For each garden
            for (int i = 0; i < n; i++)
            {
                //Use 5 instead of 4 so we can easily use 1-based indexing of the garden colors
                int[] colors = new int[5];
                foreach (var adj in graph[i])
                {
                    colors[res[adj]] = 1; //Mark the color as used if neighbor has used it before.
                }

                //Now just use a color that has not been used yet
                for (int c = 1; c <= 4; c++)
                {
                    if (colors[c] == 0) //colors[c] == 0 => the color has not been used yet,
                        res[i] = c; //so let's use that one
                }
            }
            return res;
        }

        ///1043. Partition Array for Maximum Sum, #DP , #Good
        //partition array into (contiguous) subarrays of length at most k. 
        //After partitioning, each subarray has their values changed to the maximum value of that subarray.
        //Return the largest sum of the given array after partitioning. 
        public int MaxSumAfterPartitioning(int[] arr, int k)
        {
            int n = arr.Length;
            int[] dp = new int[n];
            int max = 0;
            for (int i = 0; i < k; i++)
            {
                max = Math.Max(max, arr[i]);
                dp[i] = max * (i + 1);
            }
            //Time Complex : O(nk)
            for (int i = k; i < n; i++)
            {
                int curr = 0;
                for (int j = i; j > i - k; j--)
                {
                    curr = Math.Max(curr, arr[j]);
                    dp[i] = Math.Max(dp[i], dp[j - 1] + curr * (i - j + 1));
                }
            }
            return (int)dp[n - 1];
        }

        /// 1046. Last Stone Weight, #PriorityQueue
        /// x == y, both stones are destroyed, and
        ///If x != y, the stone of weight x is destroyed, and the stone of weight y has new weight y - x.
        ///Return the smallest possible weight of the left stone.If there are no stones left, return 0.
        public int LastStoneWeight(int[] stones)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            foreach (var n in stones)
                pq.Enqueue(n, -n);
            while (pq.Count >= 2)
            {
                int max1 = pq.Dequeue();
                int max2 = pq.Dequeue();
                if (max1 > max2)
                    pq.Enqueue(max1 - max2, max2 - max1);
            }
            return pq.Count > 0 ? pq.Dequeue() : 0;
        }
        /// 1047. Remove All Adjacent Duplicates In String, #Two Pointer
        ///"caacbc"=>"bc"
        public string RemoveDuplicates_TwoPointer(string s)
        {
            int i = 0;
            char[] res = s.ToArray();
            for (int j = 0; j < s.Length; ++j, ++i)
            {
                res[i] = res[j];
                if (i > 0 && res[i - 1] == res[i]) // count = 2
                    i -= 2;
            }
            return new string(res, 0, i);
        }

        public string RemoveDuplicatesMy(string s)
        {
            int i = 0;
            var list = s.ToList();
            while (i < list.Count)
            {
                if (i > 0 && list[i] == list[i - 1])
                {
                    list.RemoveAt(i);
                    list.RemoveAt(i - 1);
                    i--;
                }
                else
                {
                    i++;
                }
            }
            return new string(list.ToArray());
        }

        ///1048. Longest String Chain, #DP
        // chain is word[i]->word[i+1] only add a char
        public int LongestStrChain(string[] words)
        {
            Dictionary<string, int> dp = new Dictionary<string, int>();
            words = words.Distinct().OrderBy(x => x.Length).ToArray();
            int res = 0;
            foreach (var word in words)
            {
                int best = 0;
                for (int i = 0; i < word.Length; ++i)
                {
                    string prev = word.Substring(0, i) + word.Substring(i + 1);
                    int count = dp.ContainsKey(prev) ? dp[prev] + 1 : 1;
                    best = Math.Max(best, count);
                }
                dp.Add(word, best);
                res = Math.Max(res, best);
            }
            return res;
        }

    }
}