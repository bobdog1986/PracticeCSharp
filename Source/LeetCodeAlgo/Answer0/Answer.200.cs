using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///200. Number of Islands, #DFS
        ///Given an m x n 2D binary grid grid which represents a map of '1's (land) and '0's (water), return the number of islands.
        ///An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically.
        ///You may assume all four edges of the grid are all surrounded by water.
        public int NumIslands_BFS(char[][] grid)
        {
            int res = 0;
            int m = grid.Length;
            int n = grid[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            var q = new Queue<int[]>();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        res++;
                        q.Clear();
                        q.Enqueue(new int[] { i, j });
                        while (q.Count > 0)
                        {
                            var node = q.Dequeue();
                            var row = node[0];
                            var col = node[1];
                            if (grid[row][col] == '1')
                            {
                                grid[row][col] = '0';
                                foreach (var d in dxy4)
                                {
                                    var r = row + d[0];
                                    var c = col + d[1];
                                    if (r >= 0 && r < m && c >= 0 && c < n && grid[r][c] == '1')
                                    {
                                        q.Enqueue(new int[] { r, c });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return res;
        }

        public int NumIslands_DFS(char[][] grid)
        {
            int res = 0;
            int m = grid.Length;
            int n = grid[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        res++;
                        NumIslands_DFS(grid, i, j, dxy4);
                    }
                }
            }
            return res;
        }

        private void NumIslands_DFS(char[][] grid, int i,int j, int[][] dxy4)
        {
            grid[i][j] = '0';
            foreach(var d in dxy4)
            {
                int r = i + d[0];
                int c = j + d[1];
                if (r >= 0 && r < grid.Length && c >= 0 && c < grid[0].Length && grid[r][c] == '1')
                    NumIslands_DFS(grid, r, c, dxy4);
            }
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
        /// Starting with any positive int, replace the number by the sum of the squares of its digits.
        /// Repeat the process until the number==1 (where it will stay), or it loops endlessly in a cycle which does not include 1.
        /// Those numbers for which this process ends in 1 are happy. Return true if n is a happy number, and false if not.
        public bool IsHappy(int n)
        {
            HashSet<int> set = new HashSet<int>();
            return IsHappy(n, set);
        }

        private bool IsHappy(int n, HashSet<int> set)
        {
            if (n == 1) return true;
            if (set.Contains(n))
                return false;
            set.Add(n);
            return IsHappy(GetDigitSquare(n), set);
        }

        private int GetDigitSquare(int n)
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
        ///Given an int n, return the number of prime numbers that are strictly less than n.
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

        ///205. Isomorphic Strings
        ///Given two strings s and t, determine if they are isomorphic.
        ///Two strings s and t are isomorphic if the characters in s can be replaced to get t.
        public bool IsIsomorphic(string s, string t)
        {
            Dictionary<char, char> dict = new Dictionary<char, char>();
            for(int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]))
                {
                    if (dict[s[i]] != t[i]) return false;
                }
                else
                {
                    if (dict.ContainsValue(t[i])) return false;
                    else dict.Add(s[i], t[i]);
                }
            }
            return true;
        }
        /// 206. Reverse Linked List
        ///Given the head of a singly linked list, reverse the list, and return the reversed list.
        ///The number of nodes in the list is the range [0, 5000].
        public ListNode ReverseList_My(ListNode head)
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
        public ListNode ReverseList(ListNode head)
        {
            ListNode prev = null;
            ListNode node = head;
            while (node != null)
            {
                var next = node.next;
                node.next = prev;

                prev = node;
                node = next;
            }
            return prev;
        }

        public ListNode ReverseList_Recursion(ListNode head)
        {
            return ReverseList_Recursion(null, head);
        }

        private ListNode ReverseList_Recursion(ListNode prev, ListNode node)
        {
            if (node == null)
                return prev;

            var next = node.next;
            node.next = prev;
            return ReverseList_Recursion(node, next);
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
        ///208. Implement Trie (Prefix Tree), see Trie

        /// 209. Minimum Size Subarray Sum ,#Sliding Window
        ///return the minimal length of a contiguous subarray of which the sum >= target.
        ///If there is no such subarray, return 0 instead.
        ///1 <= target <= 10^9, 1 <= nums.length <= 10^5, 1 <= nums[i] <= 10^5
        public int MinSubArrayLen(int target, int[] nums)
        {
            int left = 0;
            int right = 0;
            int sum = 0;
            int min = int.MaxValue;
            while (right < nums.Length)
            {
                sum += nums[right];
                while (sum >= target)
                {
                    min = Math.Min(min, right - left+1);
                    sum -= nums[left++];
                }
                right++;
            }
            return min == int.MaxValue ? 0 : min;
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

        ///212. Word Search II, #Trie, #Backtracking, #DFS
        ///Given an m x n board of characters and a list of strings words, return all words on the board.
        ///Each word must be constructed from letters of sequentially adjacent cells,
        ///where adjacent cells are horizontally or vertically neighboring.
        ///The same letter cell may not be used more than once in a word.
        public IList<string> FindWords212(char[][] board, string[] words)
        {
            List<string> res = new List<string> ();
            TrieNode root = buildTrie(words);
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length; j++)
                {
                    FindWords212_dfs(board, i, j, root, res);
                }
            }
            return res;
        }

        public void FindWords212_dfs(char[][] board, int i, int j, TrieNode p, List<string> res)
        {
            char c = board[i][j];
            if (c == '#' || p.next[c - 'a'] == null) return;
            p = p.next[c - 'a'];
            if (p.word != null)
            {
                // found one
                res.Add(p.word);
                p.word = null;// de-duplicate
            }
            board[i][j] = '#';
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            foreach(var d in dxy4)
            {
                var row = i + d[0];
                var col = j + d[1];
                if(row>=0&&row<board.Length && col>=0 && col<board[0].Length)
                {
                    FindWords212_dfs(board, row, col, p, res);
                }
            }
            //if failed to match a word , we need recovery it back
            board[i][j] = c;
        }

        public TrieNode buildTrie(string[] words)
        {
            TrieNode root = new TrieNode();
            foreach (var w in words)
            {
                TrieNode curr = root;
                foreach (char c in w)
                {
                    int i = c - 'a';
                    if (curr.next[i] == null) curr.next[i] = new TrieNode();
                    curr = curr.next[i];
                }
                curr.word = w;
            }
            return root;
        }

        public class TrieNode
        {
            public TrieNode[] next = new TrieNode[26];
            public string word;
        }

        /// 213. House Robber II, #DP
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
        public int FindKthLargest_Heap(int[] nums, int k)
        {
            var pq = new PriorityQueue<int, int>();
            foreach(var n in nums)
            {
                if (pq.Count < k) pq.Enqueue(n, n);
                else
                {
                    if (n > pq.Peek())
                    {
                        pq.Enqueue(n, n);
                        pq.Dequeue();
                    }
                }
            }
            return pq.Peek();
        }
        ///216. Combination Sum III, #Backtracking
        ///Find all valid combinations of k numbers that sum up to n :
        ///Only numbers 1 through 9 are used.And Each number is used at most once.
        ///Return a list of all possible valid combinations.
        public IList<IList<int>> CombinationSum3(int k, int n)
        {
            var ans =new List<IList<int>>();
            var list=new List<int>();
            var arr=new List<int>() { 1,2,3,4,5,6,7,8,9};
            CombinationSum3(k, n, list, arr, ans);
            return ans;
        }

        public void CombinationSum3(int k, int n , IList<int> list, IList<int> arr,IList<IList<int>> ans)
        {
            if (n == 0 && k==0) ans.Add(list);
            if (n < 0 || k < 0) return;
            foreach(var i in arr)
            {
                if (i <= n)
                {
                    var nextArr = arr.Where(x => x > i).ToList();
                    var nextList = new List<int>(list) { i };
                    CombinationSum3(k - 1, n - i, nextList, nextArr, ans);
                }
            }
        }
        /// 217. Contains Duplicate
        public bool ContainsDuplicate(int[] nums)
        {
            HashSet<int> set = new HashSet<int>();
            foreach (var n in nums)
            {
                if (set.Contains(n)) return true;
                else set.Add(n);
            }
            return false;
        }

        ///219. Contains Duplicate II
        ///Given an int array nums and an int k, return true if nums[i] == nums[j] and abs(i - j) <= k.
        ///1 <= nums.length <= 10^5,0 <= k <= 10^5
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            if (nums.Length == 1 || k == 0) return false;
            Dictionary<int,int> dict=new Dictionary<int, int>();
            for(int i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(nums[i])) { dict[nums[i]]++; }
                else { dict.Add(nums[i], 1); }
                if (dict[nums[i]] > 1) return true;
                if (i >= k) dict[nums[i - k]]--;
            }
            return false;
        }

        /// 220. Contains Duplicate III， #Bucket
        /// return true if abs(nums[i] - nums[j]) <= t and abs(i - j) <= k.
        ///  1 <= nums.length <= 2 * 10^4,-2^31 <= nums[i] <= 2^31 - 1, 0 <= k <= 10^4, 0 <= t <= 2^31 - 1
        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            if (k ==0 || nums.Length==1) return false;
            Dictionary<long, long> map = new Dictionary<long, long>();//hold {bucketId,index} pairs
            for (int i = 0; i < nums.Length; i++)
            {
                //split whole int range [-2^31, 2^31 - 1] to t+1 width buckets
                //using t+1 as width, the distance bewteen left-0 and right-t is t, same bucket return true
                //And, if t not plus 1, when t == 0, num divide by 0 will cause crash.
                long remappedNum = (long)nums[i] - int.MinValue;
                long bucket = remappedNum / ((long)t + 1);
                if (map.ContainsKey(bucket)) return true;
                // if the two different numbers are located in two adjacent bucket, the value still might be less than t
                if (map.ContainsKey(bucket - 1) && remappedNum - map[bucket - 1] <= t)return true;
                if((map.ContainsKey(bucket + 1) && map[bucket + 1] - remappedNum <= t)) return true;
                //update buckets,remove the i-k ,only hold at most k elements
                if (map.Count >= k)
                {
                    long lastBucket = ((long)nums[i - k] - int.MinValue) / ((long)t + 1);
                    map.Remove(lastBucket);
                }
                map.Add(bucket, remappedNum);
            }
            return false;
        }

        /// 221. Maximal Square, #DP
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

        ///222. Count Complete Tree Nodes, #BTree
        ///Given the root of a complete binary tree, return the number of the nodes in the tree.
        public int CountNodes(TreeNode root)
        {
            int res = 0;
            CountNodes(root, ref res);
            return res;
        }
        public void CountNodes(TreeNode root, ref int res)
        {
            if (root == null) return;
            res ++;
            CountNodes(root.left, ref res);
            CountNodes(root.right, ref res);
        }

        ///223. Rectangle Area
        public int ComputeArea(int ax1, int ay1, int ax2, int ay2, int bx1, int by1, int bx2, int by2)
        {
            if (ax1 > bx1)
                return ComputeArea(bx1, by1, bx2, by2, ax1, ay1, ax2, ay2);

            int res= ComputeArea_Rect(ax1, ay1, ax2, ay2) + ComputeArea_Rect(bx1, by1, bx2, by2);
            if (by1>=ay2 || by2<=ay1 || bx1 >= ax2)
            {
                //if no overlay
            }
            else
            {
                if (bx2 <= ax2)
                {
                    if (by1 <= ay1 && by2 >= ay2)
                    {
                        res -= ComputeArea_Rect(bx1, ay1, bx2, ay2);
                    }
                    else if (by1 <= ay1)
                    {
                        res -= ComputeArea_Rect(bx1, ay1, bx2, by2);
                    }
                    else if (by2 >= ay2)
                    {
                        res -= ComputeArea_Rect(bx1, by1, bx2, ay2);
                    }
                    else
                    {
                        res -= ComputeArea_Rect(bx1, by1, bx2, by2);
                    }
                }
                else
                {
                    if (by1 <= ay1 && by2 >= ay2)
                    {
                        res -= ComputeArea_Rect(bx1, ay1, ax2, ay2);
                    }
                    else if (by1 <= ay1)
                    {
                        res -= ComputeArea_Rect(bx1, ay1, ax2, by2);
                    }
                    else if (by2 >= ay2)
                    {
                        res -= ComputeArea_Rect(bx1, by1, ax2, ay2);
                    }
                    else
                    {
                        res -= ComputeArea_Rect(bx1, by1, ax2, by2);
                    }
                }
            }

            return res;
        }
        private int ComputeArea_Rect(int ax1, int ay1, int ax2, int ay2)
        {
            return (ax2 - ax1) * (ay2 - ay1);
        }

        /// 225. Implement Stack using Queues, see MyStack

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
        ///1 <= s.length <= 3 * 10^5,non-negative ints in the range [0, 2^31 - 1].
        ///s consists of ints and operators ('+', '-', '*', '/') separated by some number of spaces.
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

        ///228. Summary Ranges
        ///Return the smallest sorted list of ranges that cover all the numbers in the array exactly.
        ///0 <= nums.length <= 20
        public IList<string> SummaryRanges(int[] nums)
        {
            List<List<int>> ans = new List<List<int>>();
            List<int> curr = new List<int>();
            foreach(var n in nums)
            {
                if (curr.Count == 0)
                {
                    curr.Add(n);
                }
                else
                {
                    if (curr.Last() == n - 1)
                    {
                        if (curr.Count == 1)
                        {
                            curr.Add(n);
                        }
                        else
                        {
                            curr[1] = n;
                        }
                    }
                    else
                    {
                        ans.Add(curr);
                        curr = new List<int>() { n};
                    }
                }
            }
            if(curr.Count>0) ans.Add(curr);
            return ans.Select(x => string.Join("->", x)).ToList();
        }
        ///229. Majority Element II
        ///Given an int array of size n, find all elements that appear more than ⌊ n/3 ⌋ times.
        public IList<int> MajorityElement_229(int[] nums)
        {
            var res = new List<int>();
            int count = nums.Length / 3;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var n in nums)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, 1);
                else dict[n]++;
                if(dict[n]==count+1)res.Add(n);
            }
            return res;
        }
        /// 230. Kth Smallest Element in a BST, #BTree
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
            //never happen
            return values[k - 1];
        }

        /// 231. Power of Two
        ///Given an int n, return true if it is a power of two.

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

        /// 235. Lowest Common Ancestor of a Binary Search Tree, #BTree, #BST
        ///Given a binary search tree (BST), find the lowest common ancestor (LCA) of two given nodes in the BST.
        public TreeNode LowestCommonAncestor_235BST(TreeNode root, TreeNode p, TreeNode q)
        {
            TreeNode left = p.val < q.val ? p : q;
            TreeNode right = p.val < q.val ? q : p;
            return LowestCommonAncestor_235BST_Recursion(root, left, right);
        }

        private TreeNode LowestCommonAncestor_235BST_Recursion(TreeNode root, TreeNode left, TreeNode right)
        {
            if (left.val < root.val && right.val > root.val) return root;
            if (left.val == root.val) return left;
            if (right.val == root.val) return right;
            if (left.val < root.val && right.val < root.val)
                return LowestCommonAncestor_235BST_Recursion(root.left, left, right);
            else
                return LowestCommonAncestor_235BST_Recursion(root.right, left, right);
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

        /// 238. Product of Array Except Self, #Prefix Sum
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
        ///You are given an array of ints nums, there is a sliding window of size k which is moving from the very left of the array to the very right.
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
        /// 240. Search a 2D Matrix II
        ///This matrix has the following properties:
        ///ints in each row are sorted in ascending from left to right.
        ///ints in each column are sorted in ascending from top to bottom.
        public bool SearchMatrix(int[][] matrix, int target)
        {
            if (target < matrix.First().First() || target > matrix.Last().Last())
                return false;
            int m = matrix.Length;
            int col = matrix[0].Length - 1;
            int row = 0;
            //serach form top-right
            while (col >= 0 && row <= m - 1)
            {
                if (target == matrix[row][col])
                    return true;
                else if (target < matrix[row][col])
                    col--;//move left

                else if (target > matrix[row][col])
                    row++;//move down
            }
            return false;
        }

        ///241. Different Ways to Add Parentheses
        ///Given a string expression of numbers and operators, return all possible results from computing
        ///all the different possible ways to group numbers and operators. You may return the answer in any order.
        public IList<int> DiffWaysToCompute(string expression)
        {
            var res = new List<int>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '-' ||
                    expression[i] == '*' ||
                    expression[i] == '+')
                {
                    var part1 = expression.Substring(0, i);
                    var part2 = expression.Substring(i + 1);
                    var part1Res = DiffWaysToCompute(part1);
                    var part2Res = DiffWaysToCompute(part2);
                    foreach (int p1 in part1Res)
                    {
                        foreach (int p2 in part2Res)
                        {
                            int c = 0;
                            switch (expression[i])
                            {
                                case '+':
                                    c = p1 + p2;
                                    break;
                                case '-':
                                    c = p1 - p2;
                                    break;
                                case '*':
                                    c = p1 * p2;
                                    break;
                            }
                            res.Add(c);
                        }
                    }
                }
            }
            if (res.Count == 0)
            {
                res.Add(int.Parse(expression));
            }
            return res;
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
                if (arr[c - 'a']-- == 0) return false;
            return true;
        }
    }
}