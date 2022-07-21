using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///500. Keyboard Row
        ///the first row consists of the characters "qwertyuiop",
        ///the second row consists of the characters "asdfghjkl", and
        ///the third row consists of the characters "zxcvbnm".
        ///return the words that can be typed using only one row of American keyboard like the image below.
        public string[] FindWords(string[] words)
        {
            List<string> result = new List<string>();
            Dictionary<char, int> dict = new Dictionary<char, int>()
            {
                {'q',1 },{'w',1 },{'e',1 },{'r',1 },{'t',1 },{'y',1 },{'u',1 },{'i',1 },{'o',1 },{'p',1 },
                {'a',2 },{'s',2 },{'d',2 },{'f',2 },{'g',2 },{'h',2 },{'j',2 },{'k',2 },{'l',2 },
                {'z',3 },{'x',3 },{'c',3 },{'v',3 },{'b',3 },{'n',3 },{'m',3 },
            };

            foreach(var w in words)
            {
                int last = dict[char.ToLower(w[0])];
                for(int i = 1; i <w.Length; i++)
                {
                    if(dict[char.ToLower(w[i])]!=last)
                    {
                        last = 0;
                        break;
                    }
                }
                if (last != 0) result.Add(w);
            }

            return result.ToArray();
        }

        ///501. Find Mode in Binary Search Tree, #BTree
        ///Given the root of a binary search tree (BST) with duplicates, return all the most frequently occurred elements.
        ///If the tree has more than one mode, return them in any order.
        public int[] FindMode(TreeNode root)
        {
            var res=new List<int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            List<TreeNode> list=new List<TreeNode>() { root};
            int max = 0;
            while (list.Count > 0)
            {
                List<TreeNode> next = new List<TreeNode>();
                foreach (var i in list)
                {
                    if (i != null)
                    {
                        if (dict.ContainsKey(i.val)) dict[i.val]++;
                        else dict.Add(i.val, 1);
                        max = Math.Max(max, dict[i.val]);

                        if (i.left != null) next.Add(i.left);
                        if (i.right != null) next.Add(i.right);
                    }
                }

                list = next;
            }
            foreach (var k in dict.Keys)
                if (dict[k] == max) res.Add(k);

            return res.ToArray();
        }

        ///502. IPO, #PriorityQueue
        //ith project has a pure profit profits[i] and a minimum capital of capital[i] is needed to start it.
        //Pick at most k projects to maximize your final capital, and return the final maximized capital.
        public int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
        {
            int n = profits.Length;
            int[][] arr = new int[n][];
            for (int i = 0; i < n; i++)
            {
                arr[i] = new int[] { capital[i], profits[i] };
            }
            arr = arr.OrderBy(x => x[0]).ToArray();

            var pq = new PriorityQueue<int, int>();
            int j = 0;
            for (; j < n; j++)
            {
                if (w < arr[j][0]) break;
                pq.Enqueue(arr[j][1], -arr[j][1]);
            }
            while (k-- > 0 && pq.Count > 0)
            {
                w += pq.Dequeue();
                for (; j < n; j++)
                {
                    if (w < arr[j][0]) break;
                    pq.Enqueue(arr[j][1], -arr[j][1]);
                }
            }
            return w;
        }
        /// 503. Next Greater Element II ,#Monotonic Stack
        ///Given a circular integer array nums (i.e., the next element of nums[nums.length - 1] is nums[0]),
        ///return the next greater number for every element in nums.
        ///The next greater number of a number x is the first greater number to its traversing-order next in the array,
        ///which means you could search circularly to find its next greater number.If it doesn't exist, return -1 for this number.
        public int[] NextGreaterElements(int[] nums)
        {
            int len = nums.Length;
            int[] res = new int[len];
            Array.Fill(res, -1);
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < len * 2; i++)
            {
                while (stack.Count > 0 && nums[i % len] > nums[stack.Peek() % len])
                {
                    res[stack.Pop() % len] = nums[i % len];
                }
                if (i < len && res[i % len] == -1)
                    stack.Push(i);
            }
            return res;
        }


        ///504. Base 7, in Easy

        ///506. Relative Ranks
        ///You are given an integer array score of size n, where score[i] is the score of the ith athlete in a competition.
        ///All the scores are guaranteed to be unique.
        ///Return an array answer of size n where answer[i] is the rank of the ith athlete.
        public string[] FindRelativeRanks(int[] score)
        {
            int[] arr = score.OrderBy(x => -x).ToArray();
            Dictionary<int, string> dict = new Dictionary<int, string>();
            for(int i = 0; i < arr.Length; i++)
            {
                string str = "";
                int rank = i + 1;
                if (rank == 0) str = "Gold Medal";
                else if (rank == 1) str = "Silver Medal";
                else if (rank == 3) str = "Bronze Medal";
                else str = rank.ToString();
                dict.Add(arr[i], str);
            }
            return score.Select(x => dict[x]).ToArray();
        }

        ///507. Perfect Number
        //a positive integer equal to the sum of its positive divisors,excluding the number itself.
        public bool CheckPerfectNumber(int num)
        {
            if (num == 1) return false;
            int sum = 1;
            for(int i = 2; i * i <= num; i++)
                if (num % i == 0) sum += i + num / i;
            return sum == num;
        }

        /// 508. Most Frequent Subtree Sum, #BTree
        public int[] FindFrequentTreeSum(TreeNode root)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int maxFreq = 0;
            FindFrequentTreeSum(root, dict, ref maxFreq);
            return dict.Keys.Where(x => dict[x]==maxFreq).Select(x=>x).ToArray();
        }

        private int FindFrequentTreeSum(TreeNode root, Dictionary<int, int> dict, ref int maxFreq)
        {
            if (root == null) return 0;
            else
            {
                var leftSum = FindFrequentTreeSum(root.left, dict, ref maxFreq);
                var rightSum = FindFrequentTreeSum(root.right, dict, ref maxFreq);

                var sum = leftSum + rightSum + root.val;
                if (dict.ContainsKey(sum)) dict[sum]++;
                else dict.Add(sum, 1);

                maxFreq = Math.Max(maxFreq, dict[sum]);
                return sum;
            }
        }

        /// 509. Fibonacci Number
        ///0 <= n <= 30, F(0) = 0, F(1) = 1, F(n) = F(n - 1) + F(n - 2), for n > 1.
        public int Fib(int n)
        {
            if (n <= 1)
                return n;
            int a = 0;
            int dp = 1;
            int temp = 0;
            while (n-->1)
            {
                temp = dp;
                dp += a;
                a=temp;
            }
            return dp;
        }

        public int Fib_Recursion(int n)
        {
            return Fib_Memo(n,new Dictionary<int, int>());
        }

        private int Fib_Memo(int n, Dictionary<int,int> dict)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (dict.ContainsKey(n)) return dict[n];
            var x = Fib_Memo(n - 1,dict) + Fib_Memo(n - 2,dict);
            dict.Add(n, x);
            return x;
        }

        ///513. Find Bottom Left Tree Value, #BTree
        ///Given the root of a binary tree, return the leftmost value in the last row of the tree.
        public int FindBottomLeftValue(TreeNode root)
        {
            int res = root.val;
            var list = new List<TreeNode>() {root };
            while (list.Count > 0)
            {
                res = list[0].val;
                var next = new List<TreeNode>();
                foreach(var i in list)
                {
                    if (i.left != null) next.Add(i.left);
                    if (i.right != null) next.Add(i.right);
                }
                list = next;
            }
            return res;
        }
        ///515. Find Largest Value in Each Tree Row, #BTree
        public IList<int> LargestValues(TreeNode root)
        {
            var res = new List<int>();
            if (root == null) return res;
            var list=new List<TreeNode>() { root};
            while (list.Count > 0)
            {
                res.Add(list.Max(x => x.val));
                var next = new List<TreeNode>();
                foreach (var i in list)
                {
                    if (i.left != null) next.Add(i.left);
                    if (i.right != null) next.Add(i.right);
                }
                list = next;
            }
            return res;
        }

        /// 516. Longest Palindromic Subsequence, #DP
        ///Given a string s, find the longest palindromic subsequence's length in s.
        ///A subsequence is a sequence that can be derived from another sequence
        ///by deleting some or no elements without changing the order of the remaining elements.
        public int LongestPalindromeSubseq(string s)
        {
            if(s == null||s.Length==0) return 0;
            if (s.Length == 1) return 1;

            //dp[i][j]: the longest palindromic subsequence's length of substring in range [i, j]
            //dp[i][j] = dp[i + 1][j - 1] + 2, if s.charAt(i) == s.charAt(j)
            //otherwise, dp[i][j] = Math.max(dp[i + 1][j], dp[i][j - 1])
            //Initialization: dp[i][i] = 1
            int n = s.Length;
            int[,] dp = new int[n, n];
            for (int i = n - 1; i >= 0; i--)
            {
                dp[i, i] = 1;
                for (int j = i + 1; j < n; j++)
                {
                    if (s[i] == s[j])
                    {
                        dp[i, j] = dp[i + 1, j - 1] + 2;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i + 1, j], dp[i, j - 1]);
                    }
                }
            }
            return dp[0, s.Length - 1];
        }

        public int LongestPalindromeSubseq2(string s)
        {
            if (s == null || s.Length == 0) return 0;
            if (s.Length == 1) return 1;

            int[,] dp = new int[s.Length, s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                dp[i, i] = 1;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (s[i] == s[j])
                    {
                        dp[i, j] = 2 + dp[i - 1, j + 1];
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j + 1]);
                    }
                }
            }
            return dp[s.Length - 1, 0];
        }

        public int LongestPalindromeSubseq_Memo(string s)
        {
            return LongestPalindromeSubseq_Memo(s, 0, s.Length - 1, new int[s.Length,s.Length]);
        }

        private int LongestPalindromeSubseq_Memo(string s, int i, int j, int[,] memo)
        {
            if (memo[i,j] != 0)
            {
                return memo[i,j];
            }
            if (i > j) return 0;
            if (i == j) return 1;
            if (s[i] == s[j])
            {
                memo[i,j] = LongestPalindromeSubseq_Memo(s, i + 1, j - 1, memo) + 2;
            }
            else
            {
                memo[i,j] = Math.Max(LongestPalindromeSubseq_Memo(s, i + 1, j, memo), LongestPalindromeSubseq_Memo(s, i, j - 1, memo));
            }
            return memo[i,j];
        }

        ///518. Coin Change 2, #DP
        ///Return the number of combinations that make up that amount.
        ///If that amount of money cannot be made up by any combination of the coins, return 0.
        public int Change(int amount, int[] coins)
        {
            int[] dp = new int[amount + 1];
            dp[0] = 1;
            foreach (var coin in coins)
            {
                for (int i = coin; i <= amount; i++)
                {
                    dp[i] += dp[i - coin];
                }
            }
            return dp[amount];
        }

        /// 520. Detect Capital
        ///3 pattern: all UpCase, all LowerCase, only first char UpCase others lower
        ///Given a string word, return true if the usage of capitals in it is right.
        public bool DetectCapitalUse(string word)
        {
            if (word.Length == 1)
                return true;

            bool lastIsUpper = char.IsUpper(word[word.Length - 1]);

            bool ans = true;

            if (lastIsUpper)
            {
                for (int i = 0; i < word.Length - 1; i++)
                {
                    if (!char.IsUpper(word[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 1; i < word.Length - 1; i++)
                {
                    if (char.IsUpper(word[i]))
                    {
                        return false;
                    }
                }
            }
            return ans;
        }

        ///521. Longest Uncommon Subsequence I, in Easy

        /// 524. Longest Word in Dictionary through Deleting
        public string FindLongestWord(string s, IList<string> dictionary)
        {
            var list =dictionary.ToList();
            list.Sort((x, y) =>
            {
                if (x.Length == y.Length)
                {
                    return string.Compare(x, y, StringComparison.Ordinal);
                }
                else
                {
                    return y.Length - x.Length;
                }
            });

            foreach(var word in list)
            {
                int i = 0;
                foreach(var c in s)
                {
                    if (c == word[i])
                    {
                        i++;
                        if (i == word.Length) return word;
                    }
                }
            }
            return string.Empty;
        }

        /// 525. Contiguous Array
        ///Given a binary array nums, return the maximum length of a contiguous subarray with an equal number of 0 and 1.
        ///1 <= nums.length <= 105
        ///nums[i] is either 0 or 1.
        public int FindMaxLength(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int sum = 0;
            //assume 0 is -1, then find diff of index between two dict[sum]
            //dict[0] should inited as -1, because [0,1] will add dict[0]-index1,
            dict.Add(0, -1);
            int max = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                //assume 0 is -1, then find diff of index between two dict[sum]
                if (nums[i] == 0)
                {
                    sum -= 1;
                }
                else
                {
                    sum += 1;
                }

                if (dict.ContainsKey(sum))
                {
                    max = Math.Max(max, i - dict[sum]);
                }
                else
                {
                    dict.Add(sum, i);
                }
            }
            return max;
        }

        ///528. Random Pick with Weight, see Solution_RandomPick

        /// 530. Minimum Absolute Difference in BST, #BTree
        ///Given the root of a Binary Search Tree (BST), return the minimum absolute difference between any two nodes in the tree.
        public int GetMinimumDifference(TreeNode root)
        {
            HashSet<int> set = new HashSet<int>();
            List<TreeNode> list=new List<TreeNode>() { root};
            while (list.Count > 0)
            {
                List<TreeNode> next = new List<TreeNode>();
                foreach(var i in list)
                {
                    if (i == null) continue;
                    set.Add(i.val);
                    if (i.left != null) next.Add(i.left);
                    if (i.right != null) next.Add(i.right);
                }
                list = next;
            }
            var arr= set.OrderBy(x => x).ToList();
            int min = arr[1] - arr[0];
            for(int i=2; i < arr.Count; i++)
                min = Math.Min(min, arr[i] - arr[i - 1]);
            return min;
        }

        /// 532. K-diff Pairs in an Array, #HashMap
        ///Given an array of integers nums and an integer k, return the number of unique k-diff pairs in the array.
        /// |nums[i] - nums[j]| == k, -10^7 <= nums[i] <= 10^7, 0 <= k <= 10^7
        public int FindPairs(int[] nums, int k)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (dict.ContainsKey(n))
                    dict[n]++;
                else
                    dict.Add(n, 1);
            }
            int ans = 0;
            if (k == 0)
            {
                foreach (var key in dict.Keys)
                {
                    if (dict[key] >= 2)
                        ans++;
                }
            }
            else
            {
                var keys = dict.Keys.OrderBy(x => x).ToList();
                foreach (var key in keys)
                {
                    if (key + k > keys.Last())
                        break;
                    if (dict.ContainsKey(key + k))
                        ans++;
                }
            }
            return ans;
        }

        ///535. Encode and Decode TinyURL, see Codec

        ///537. Complex Number Multiplication
        //A complex number as a string on the form "real+imaginaryi"
        public string ComplexNumberMultiply(string num1, string num2)
        {
            var str1 = num1.Split('+').ToList();
            var str2 = num2.Split('+').ToList();
            int real1 = int.Parse(str1[0]);
            int real2 = int.Parse(str2[0]);
            int img1 = int.Parse(str1[1].Substring(0, str1[1].Length - 1));
            int img2 = int.Parse(str2[1].Substring(0, str2[1].Length - 1));
            int real = real1 * real2 - img1 * img2;
            int img = real1 * img2 + img1 * real2;
            return $"{real}+{img}i";
        }

        /// 538. Convert BST to Greater Tree, #BTree, #BST
        public TreeNode ConvertBST1(TreeNode root)
        {
            TreeNode node = root;
            Stack<TreeNode> stack = new Stack<TreeNode>();

            int sum = 0;
            while (node != null || stack.Count > 0)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.right;
                }
                else
                {
                    node = stack.Pop();
                    sum += node.val;
                    node.val = sum;

                    node = node.left;
                }
            }

            return root;
        }

        public TreeNode ConvertBST(TreeNode root)
        {
            ConvertBST_dfs(root, 0);
            return root;
        }
        public int ConvertBST_dfs(TreeNode root, int val)
        {
            if (root == null) return val;
            int right = ConvertBST_dfs(root.right, val);
            int left = ConvertBST_dfs(root.left, root.val + right);
            root.val = root.val + right;
            return left;
        }
        ///540. Single Element in a Sorted Array, #Binary Search
        //only 1 num once, others twice, find it
        public int SingleNonDuplicate(int[] nums)
        {
            int n = nums.Length;
            int left = 0;
            int right =n - 1;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if ((mid % 2 == 0 && nums[mid] == nums[mid + 1]) || (mid % 2 == 1 && nums[mid] == nums[mid - 1]))
                    left = mid + 1;//left half is ok
                else
                    right = mid;//left half miss one
            }
            return nums[left];
        }
        /// 541. Reverse String II
        ///Given a string s and an integer k, reverse the first k characters for every 2k characters
        public string ReverseStr(string s, int k)
        {
            string res = "";
            for(int i=0; i < s.Length; i += k)
            {
                int count = i + k <= s.Length ? k : s.Length - i;
                if(i/k % 2 ==0)
                {
                    res += new string(s.Substring(i, count).Reverse().ToArray());
                }
                else
                {
                    res += s.Substring(i, count);
                }
            }
            return res;
        }

        /// 542. 01 Matrix, #Graph, #BFS
        /// Given an m x n binary matrix mat, return the distance of the nearest 0 for each cell.
        /// The distance between two adjacent cells is 1.
        public int[][] UpdateMatrix(int[][] mat)
        {
            int rowLen = mat.Length;
            int colLen = mat[0].Length;
            int[][] res = new int[rowLen][];
            for (int i = 0; i < rowLen; i++)
                res[i] = new int[colLen];
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            Queue<int[]> q = new Queue<int[]>();
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (mat[i][j] == 0)
                    {
                        res[i][j] = 0;
                        q.Enqueue(new int[] { i, j });
                    }
                    else
                    {
                        res[i][j] = Math.Max(i, rowLen - i - 1) + Math.Max(j, colLen - j - 1);
                    }
                }
            }
            int step = 1;
            bool[,] visit = new bool[rowLen, colLen];
            while (q.Count > 0)
            {
                int size = q.Count;
                while (size-- > 0)
                {
                    var p = q.Dequeue();
                    foreach (var d in dxy4)
                    {
                        var r = p[0] + d[0];
                        var c = p[1] + d[1];
                        if (r >= 0 && r < rowLen && c >= 0 && c < colLen && !visit[r, c] && mat[r][c] == 1)
                        {
                            visit[r, c] = true;
                            res[r][c] = step;
                            q.Enqueue(new int[] { r, c });
                        }
                    }
                }
                step++;
            }
            return res;
        }

        ///543. Diameter of Binary Tree, #BTree
        ///The diameter of a binary tree is the length of the longest path between any two nodes in a tree.
        ///This path may or may not pass through the root. number of edges between them.
        public int DiameterOfBinaryTree(TreeNode root)
        {
            int res = 0;
            DiameterOfBinaryTree_Recur(root, ref res);
            return res;
        }

        private int DiameterOfBinaryTree_Recur(TreeNode root, ref int res)
        {
            if (root == null) return 0;
            var left = DiameterOfBinaryTree_Recur(root.left, ref res);
            var right = DiameterOfBinaryTree_Recur(root.right, ref res);
            res = Math.Max(res, left + right);
            return Math.Max(left,right)+1;
        }

        /// 547. Number of Provinces, #DFS, #Union Find
        public int FindCircleNum_UnionFind(int[][] isConnected)
        {
            int n = isConnected.Length;
            var uf = new UnionFind(n);
            for(int i=0; i<n; i++)
            {
                for(int j=0; j<n; j++)
                {
                    if (i == j) continue;
                    if (isConnected[i][j] == 1)
                    {
                        uf.Union(i, j);
                    }
                }
            }
            return uf.GroupCount;
        }


        public int FindCircleNum(int[][] isConnected)
        {
            int res = 0;
            int[] arr = new int[isConnected.Length];
            for (int i = 0; i < isConnected.Length; i++)
            {
                if (arr[i] != 0) continue;
                res++;
                arr[i] = 1;
                for (int j = 0; j < isConnected[i].Length; j++)
                {
                    if (i != j && isConnected[i][j] == 1)
                    {
                        FindCircleNum_dfs(isConnected, arr, j);
                    }
                }
            }
            return res;
        }

        private void FindCircleNum_dfs(int[][] isConnected, int[] arr, int index)
        {
            if (arr[index] != 0)
                return;
            arr[index] = 1;
            for (int j = 0; j < isConnected[index].Length; j++)
            {
                if (index != j && isConnected[index][j] == 1)
                {
                    FindCircleNum_dfs(isConnected, arr, j);
                }
            }
        }
    }
}