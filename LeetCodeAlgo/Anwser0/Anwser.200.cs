using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///200. Number of Islands
        ///Given an m x n 2D binary grid grid which represents a map of '1's (land) and '0's (water), return the number of islands.
        ///An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically.
        ///You may assume all four edges of the grid are all surrounded by water.
        public int NumIslands(char[][] grid)
        {
            int ans = 0;
            int rowlen = grid.Length;
            int colLen = grid[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            var queue = new Queue<int[]>();
            for (int i = 0; i < rowlen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        ans++;
                        queue.Clear();
                        queue.Enqueue(new int[] { i, j });
                        while (queue.Count > 0)
                        {
                            var node = queue.Dequeue();
                            var row = node[0];
                            var col = node[1];

                            if (grid[row][col] == '1')
                            {
                                grid[row][col] = '0';
                                foreach (var d in dxy4)
                                {
                                    int r = row + d[0];
                                    int c = col + d[1];
                                    if (r >= 0 && r < rowlen && c >= 0 && c < colLen)
                                    {
                                        if (grid[r][c] == '1')
                                        {
                                            queue.Enqueue(new int[] { r, c });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ans;
        }

        ///201. Bitwise AND of Numbers Range
        ///[left, right], return the bitwise AND of all numbers in this range, inclusive.
        public int RangeBitwiseAnd(int left, int right)
        {
            if (left == 0)
                return 0;
            //we can always find two nums that left_1000 and left_0111, will cause lower bits to 0
            //so, only keep high bits; eg. [1,2]->0, [1,3]->0,[2->3]->2
            int moveFactor = 0;
            while (left != right)
            {
                left >>= 1;
                right >>= 1;
                moveFactor++;
            }
            return left << moveFactor;
        }

        /// 202. Happy Number
        /// Starting with any positive integer, replace the number by the sum of the squares of its digits.
        /// Repeat the process until the number==1 (where it will stay), or it loops endlessly in a cycle which does not include 1.
        /// Those numbers for which this process ends in 1 are happy. Return true if n is a happy number, and false if not.
        public bool IsHappy(int n)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            return IsHappy(n, dict);
        }

        public bool IsHappy(int n, Dictionary<int, int> dict)
        {
            if (n == 1) return true;
            if (dict.ContainsKey(n))
                return false;
            dict.Add(n, 1);
            return IsHappy(GetDigitSquare(n), dict);
        }

        public int GetDigitSquare(int n)
        {
            int result = 0;
            int last;
            while (n > 0)
            {
                last = n % 10;
                result += last * last;
                n /= 10;
            }
            return result;
        }

        //203. Remove Linked List Elements
        public ListNode RemoveElements(ListNode head, int val)
        {
            while (head != null)
            {
                if (val == head.val)
                {
                    head = head.next;
                }
                else
                {
                    break;
                }
            }

            if (head == null)
                return null;

            var current = head;

            while (current.next != null)
            {
                if (current.next.val == val)
                {
                    current.next = current.next.next;
                }
                else
                {
                    current = current.next;
                }
            }

            return head;
        }

        ///204. Count Primes
        ///Given an integer n, return the number of prime numbers that are strictly less than n.
        public int CountPrimes(int n)
        {
            bool[] notPrime = new bool[n];
            int count = 0;
            for (int i = 2; i < n; i++)
            {
                if (notPrime[i] == false)
                {
                    count++;
                    for (int j = 2; i * j < n; j++)
                    {
                        notPrime[i * j] = true;
                    }
                }
            }
            return count;
        }

        /// 206. Reverse Linked List
        ///Given the head of a singly linked list, reverse the list, and return the reversed list.
        ///The number of nodes in the list is the range [0, 5000].
        public ListNode ReverseList(ListNode head)
        {
            if (head == null)
                return null;
            List<ListNode> list = new List<ListNode>();
            var node = head;
            while (node != null)
            {
                list.Add(node);
                node = node.next;
            }
            for (int i = list.Count - 1; i > 0; i--)
            {
                list[i].next = list[i - 1];
            }
            list[0].next = null;
            return list.Last();
        }

        ///207. Course Schedule, #Graph
        ///You are given an array prerequisites where prerequisites[i] = [ai, bi]
        ///indicates that you must take course bi first if you want to take course ai.
        ///Return true if you can finish all courses. Otherwise, return false.
        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            var ans = true;
            int len = numCourses;
            bool[,] visit=new bool[len, len];
            foreach(var p in prerequisites)
            {
                if (p[0] == p[1]) return false;
                if (visit[p[1], p[0]]) return false;
                visit[p[0], p[1]] = true;

                for (int i = 0; i < len; i++)
                {
                    if(visit[p[1], i])
                    {
                        if (visit[i, p[0]]) return false;
                        for(int j = 0; j < len; j++)
                        {
                            if (visit[i, j]) visit[p[0], j] = true;
                        }
                    }

                    //update
                    if (visit[i, p[0]]) visit[i, p[1]] = true;
                }

            }
            return ans;
        }
        /// 209. Minimum Size Subarray Sum ,#Prefix Sum
        ///return the minimal length of a contiguous subarray of which the sum >= target.
        ///If there is no such subarray, return 0 instead.
        public int MinSubArrayLen(int target, int[] nums)
        {
            int min = nums.Length + 1;
            int sum = 0;
            int left = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                while (sum >= target)
                {
                    int len = i - left + 1;
                    min = Math.Min(min, len);
                    sum -= nums[left];
                    left++;
                }
            }
            return min == nums.Length + 1 ? 0 : min;
        }

        ///210. Course Schedule II, #Graph
        ///Return the ordering of courses you should take to finish all courses.
        ///If there are many valid answers, return any of them. If it is impossible to finish all courses, return an empty array.
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            if (numCourses == 0) return null;
            // Convert graph presentation from edges to indegree of adjacent list.
            int[] indegree = new int[numCourses];
            int[] order = new int[numCourses];
            int index = 0;
            for (int i = 0; i < prerequisites.Length; i++) // Indegree - how many prerequisites are needed.
                indegree[prerequisites[i][0]]++;

            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < numCourses; i++)
                if (indegree[i] == 0)
                {
                    // Add the course to the order because it has no prerequisites.
                    order[index++] = i;
                    queue.Enqueue(i);
                }

            // How many courses don't need prerequisites.
            while (queue.Count>0)
            {
                int prerequisite = queue.Dequeue(); // Already finished this prerequisite course.
                for (int i = 0; i < prerequisites.Length; i++)
                {
                    if (prerequisites[i][1] == prerequisite)
                    {
                        indegree[prerequisites[i][0]]--;
                        if (indegree[prerequisites[i][0]] == 0)
                        {
                            // If indegree is zero, then add the course to the order.
                            order[index++] = prerequisites[i][0];
                            queue.Enqueue(prerequisites[i][0]);
                        }
                    }
                }
            }

            return (index == numCourses) ? order : new int[0];
        }
        /// 211. Design Add and Search Words Data Structure, see WordDictionary

        /// 213. House Robber II
        ///All houses at this place are arranged in a circle. N-1 is next to 0
        public int Rob(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int[] withoutFirst = new int[nums.Length - 1];
            for (int i = 0; i < nums.Length - 1; i++)
                withoutFirst[i] = nums[i];
            int[] withoutLast = new int[nums.Length - 1];
            for (int i = 0; i < nums.Length - 1; i++)
                withoutLast[i] = nums[i + 1];

            //Return maximum of two results
            return Math.Max(Rob_Line(withoutFirst), Rob_Line(withoutLast));
        }

        public int Rob_Line(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int[] dp = new int[nums.Length];

            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);

            for (int i = 2; i < nums.Length; i++)
            {
                dp[i] = Math.Max(nums[i] + dp[i - 2], dp[i - 1]);
            }

            return dp[nums.Length - 1];
        }

        ///215. Kth Largest Element in an Array
        ///return the kth largest element in the array.
        ///-10^4 <= nums[i] <= 10^4
        public int FindKthLargest(int[] nums, int k)
        {
            Array.Sort(nums);
            return nums[nums.Length - k];
        }

        /// 217. Contains Duplicate, #HashMap
        public bool ContainsDuplicate(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (dict.ContainsKey(n))
                    dict[n]++;
                else
                    dict.Add(n, 1);
            }
            return dict.Values.Max() > 1;
        }

        ///221. Maximal Square, #DP
        ///Given an m x n binary matrix filled with 0's and 1's,
        ///find the largest square containing only 1's and return its area.
        public int MaximalSquare(char[][] matrix)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;
            //loop matrix from top to bottom, you can also try left to right
            //store count of last '1'
            int[] dp = new int[colLen];
            //max len of square
            int len = 0;
            foreach (var row in matrix)
            {
                for (int i = 0; i < colLen; i++)
                {
                    //max count of '1' on vertical direction
                    dp[i] = row[i] == '0' ? 0 : dp[i] + 1;
                }

                for (int i = 0; i < colLen; i++)
                {
                    if (dp[i] <= len)
                        continue;
                    //count of '1' on left side and right side
                    int left = 0;
                    int right = 0;
                    //width = me(1) + left + right, max count of '1' on horizontal direction
                    int width = 1;

                    //j to avoid to death loop
                    int j = 0;
                    int count = Math.Max(i, dp.Length - 1 - i);

                    while (j < count && width <= dp[i] && (i - 1 - left >= 0 || i + 1 + right <= dp.Length - 1))
                    {
                        if (((i - 1 - left) < 0 || dp[i - 1 - left] < dp[i])
                            && ((i + 1 + right > dp.Length - 1) || dp[i + 1 + right] < dp[i]))
                        {
                            break;
                        }

                        if (i - 1 - left >= 0 && dp[i - 1 - left] >= dp[i])
                            left++;

                        if (i + 1 + right <= dp.Length - 1 && dp[i + 1 + right] >= dp[i])
                            right++;

                        width = 1 + left + right;

                        j++;
                    }

                    len = Math.Max(Math.Min(width, dp[i]), len);
                }
            }

            return len * len;
        }

        /// 226. Invert Binary Tree
        ///Given the root of a binary tree, invert the tree, and return its root.

        public TreeNode InvertTree(TreeNode root)
        {
            InvertTree_Recursion(root);
            return root;
        }

        public void InvertTree_Recursion(TreeNode node)
        {
            if (node == null)
                return;

            if (node.left == null && node.right == null)
                return;

            var temp = node.left;
            node.left = node.right;
            node.right = temp;

            InvertTree_Recursion(node.left);
            InvertTree_Recursion(node.right);
        }

        ///227. Basic Calculator II
        ///1 <= s.length <= 3 * 10^5,non-negative integers in the range [0, 2^31 - 1].
        ///s consists of integers and operators ('+', '-', '*', '/') separated by some number of spaces.
        public int Calculate(string s)
        {
            List<int> list = new List<int>();
            int num = 0;
            char sign = '+';
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    num = num * 10 + s[i] - '0';
                }
                else if(s[i]!=' ')
                {
                    if (sign == '-')
                    {
                        list.Add(-num);
                    }
                    if (sign == '+')
                    {
                        list.Add(num);
                    }
                    if (sign == '*')
                    {
                        list[list.Count-1]*= num;
                    }
                    if (sign == '/')
                    {
                        list[list.Count - 1] /= num;
                    }
                    sign = s[i];
                    num = 0;
                }
            }

            if (sign == '-')
            {
                list.Add(-num);
            }
            if (sign == '+')
            {
                list.Add(num);
            }
            if (sign == '*')
            {
                list[list.Count - 1] *= num;
            }
            if (sign == '/')
            {
                list[list.Count - 1] /= num;
            }
            return list.Count == 0 ? 0 : list.Sum();
        }

        /// 230. Kth Smallest Element in a BST
        ///copy from InorderTraversal_Iteration()
        public int KthSmallest(TreeNode root, int k)
        {
            List<int> values = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode node = root;
            int n = k;
            while (node != null || stack.Any())
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }
                else
                {
                    var item = stack.Pop();
                    if (n == 1)
                    {
                        return item.val;
                    }
                    n--;
                    values.Add(item.val);
                    node = item.right;
                }
            }
            //need happen
            return values[k - 1];
        }

        /// 231. Power of Two
        ///Given an integer n, return true if it is a power of two.

        public bool IsPowerOfTwo(int n)
        {
            if (n <= 0)
                return false;

            while (n >= 1)
            {
                if (n == 1)
                    return true;

                if (n % 2 == 1)
                    return false;

                n = n / 2;
            }

            return false;
        }

        //232. Implement Queue using Stacks

        //232. Implement Queue using Stacks

        //240
        public bool SearchMatrix(int[,] matrix, int target)
        {
            if (matrix == null) return false;

            return SerachMatrix(matrix, 0, matrix.GetLength(0), target);
        }

        public bool SerachMatrix(int[,] matrix, int startRowIndex, int endColIndex, int target)
        {
            if (matrix == null) return false;
            int col = matrix.GetLength(0);
            int row = matrix.GetLength(1);

            for (int i = startRowIndex; i < row; i++)
            {
                for (int j = 0; j < endColIndex; j++)
                {
                    if (matrix[i, j] == target) return true;
                    if (matrix[i, j] > target) return SerachMatrix(matrix, i, j, target);
                }
            }
            return false;
        }

        ///234. Palindrome Linked List
        ///Given the head of a singly linked list, return true if it is a palindrome.
        public bool IsPalindrome(ListNode head)
        {
            List<int> list = new List<int>();
            while (head != null)
            {
                list.Add(head.val);
                head = head.next;
            }
            for (int i = 0; i < list.Count / 2; i++)
            {
                if (list[i] != list[list.Count - 1 - i])
                    return false;
            }
            return true;
        }

        /// 235. Lowest Common Ancestor of a Binary Search Tree
        ///Given a binary search tree (BST), find the lowest common ancestor (LCA) of two given nodes in the BST.
        public TreeNode LowestCommonAncestor_235BST(TreeNode root, TreeNode p, TreeNode q)
        {
            TreeNode left = p.val < q.val ? p : q;
            TreeNode right = p.val < q.val ? q : p;

            return LowestCommonAncestor_235BST_Recursion(root, left, right);
        }

        public TreeNode LowestCommonAncestor_235BST_Recursion(TreeNode root, TreeNode left, TreeNode right)
        {
            if (left.val < root.val && right.val > root.val)
                return root;

            if (left.val == root.val)
                return left;

            if (right.val == root.val)
                return right;

            if (left.val < root.val && right.val < root.val)
            {
                return LowestCommonAncestor_235BST_Recursion(root.left, left, right);
            }
            else
            {
                return LowestCommonAncestor_235BST_Recursion(root.right, left, right);
            }
        }

        ///236. Lowest Common Ancestor of a Binary Tree - NOT BST, using Recursion
        ///Given a binary tree, find the lowest common ancestor (LCA) of two given nodes in the tree.
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
                return null;
            if (root.val == p.val || root.val == q.val)
                return root;
            var left = LowestCommonAncestor(root.left, p, q);
            var right = LowestCommonAncestor(root.right, p, q);
            if (left != null && right != null)
                return root;
            return left ?? right;
        }

        ///237. Delete Node in a Linked List
        ///Write a function to delete a node in a singly-linked list.
        ///You will not be given access to the head of the list, instead you will be given access to the node to be deleted directly.
        public void DeleteNode(ListNode node)
        {
            ListNode last = null;
            ListNode curr = node;
            while (curr != null)
            {
                if (curr.next == null)
                {
                    last.next = null;
                    break;
                }
                curr.val = curr.next.val;
                last = curr;
                curr = curr.next;
            }
        }

        /// 238. Product of Array Except Self
        ///return an array such that answer[i] = product of all the elements of nums except nums[i].
        ///O(n) time and without using the division operation.
        public int[] ProductExceptSelf(int[] nums)
        {
            int[] ans = new int[nums.Length];

            int[] left = new int[nums.Length];
            int[] right = new int[nums.Length];

            int product1 = 1;
            int product2 = 1;
            for (int i = 0; i < nums.Length; i++)
            {
                left[i] = product1;
                right[nums.Length - 1 - i] = product2;

                product1 *= nums[i];
                product2 *= nums[nums.Length - 1 - i];
            }

            for (int i = 0; i < nums.Length; i++)
            {
                ans[i] = left[i] * right[i];
            }

            return ans;
        }

        ///239. Sliding Window Maximum
        ///You are given an array of integers nums, there is a sliding window of size k which is moving from the very left of the array to the very right.
        ///You can only see the k numbers in the window. Each time the sliding window moves right by one position.
        ///Return the max sliding window. -10^4 <= nums[i] <= 10^4
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            //O(n)= O(N)
            int[] max_left = new int[nums.Length];
            int[] max_right = new int[nums.Length];
            //split nums to (len/k) * k-len window,
            //calculate max from both of left and right sides
            max_left[0] = nums[0];
            max_right[nums.Length - 1] = nums[nums.Length -1];
            for (int i = 1; i < nums.Length; i++)
            {
                max_left[i] = (i % k == 0) ? nums[i] : Math.Max(max_left[i - 1], nums[i]);
                int j = nums.Length - i - 1;
                max_right[j] = (j % k == 0) ? nums[j] : Math.Max(max_right[j + 1], nums[j]);
            }
            int[] ans = new int[nums.Length - k + 1];
            for (int i = 0, j = 0; i + k <= nums.Length; i++)
            {
                ans[j++] = Math.Max(max_right[i], max_left[i + k - 1]);
            }
            return ans;
        }

        //O(n)= O(Nk)
        public int[] MaxSlidingWindow_My(int[] nums, int k)
        {
            int[] ans = new int[nums.Length - k + 1];
            int index = -1;
            int max = -10000;
            for(int i = 0; i < nums.Length-k+1; i++)
            {
                if (i > 0)
                {
                    //new tail
                    if(nums[i+k-1] >= max)
                    {
                        max = nums[i + k - 1];
                        index = i + k - 1;
                        ans[i] = max;
                        continue;
                    }
                    else
                    {
                        if (index > i - 1)
                        {
                            ans[i] = max;
                            continue;
                        }
                    }
                }

                max = -10000;
                for(int j = i; j < k+i; j++)
                {
                    if (nums[j] >= max)
                    {
                        max=nums[j];
                        index = j;
                    }
                }
                ans[i] = max;
            }
            return ans;
        }
        /// 240. Search a 2D Matrix II, #Binary Search
        public bool SearchMatrix(int[][] matrix, int target)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;

            if (matrix[0][0] > target || matrix[rowLen - 1][colLen - 1] < target)
                return false;

            foreach (var arr in matrix)
            {
                if (arr[0] > target || arr[arr.Length - 1] < target)
                    continue;

                int col1 = 0;
                int col2 = colLen - 1;
                int col = (col2 - col1) / 2;

                while (col1 <= col2 && col <= col2 && col >= col1)
                {
                    if (col1 == col2)
                    {
                        if (arr[col1] == target)
                            return true;
                        break;
                    }

                    if (arr[col] == target)
                    {
                        return true;
                    }
                    else if (arr[col] > target)
                    {
                        col2 = col - 1;
                        col = (col2 - col1) / 2 + col1;
                    }
                    else
                    {
                        col1 = col + 1;
                        col = (col2 - col1) / 2 + col1;
                    }
                }
            }

            return false;
        }

        /// 242. Valid Anagram
        /// Given two strings s and t, return true if t is an anagram of s, and false otherwise.
        /// An Anagram is a word or phrase formed by rearranging the letters of a different word or phrase, same length
        /// Input: s = "anagram", t = "nagaram" =>Output: true
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length)
                return false;
            int[] arr = new int[26];
            foreach (var c in s)
                arr[c - 'a']++;
            foreach (var c in t)
            {
                if (arr[c - 'a'] == 0)
                    return false;
                arr[c - 'a']--;
            }
            return true;
        }
    }
}