using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///100. Same Tree, #BTree
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
                return true;
            if (p == null || q == null)
                return false;

            List<TreeNode> list1 = new List<TreeNode>() { p};
            List<TreeNode> list2 = new List<TreeNode>() { q};

            while(list1.Count>0|| list2.Count > 0)
            {
                if (list1.Count != list2.Count)
                    return false;

                List<TreeNode> next1 = new List<TreeNode>();
                List<TreeNode> next2 = new List<TreeNode>();
                for (int i=0; i<list1.Count; i++)
                {
                    if (list1[i].val != list2[i].val)
                        return false;

                    if ((list1[i].left == null && list2[i].left != null)
                        || (list1[i].left != null && list2[i].left == null)
                        || (list1[i].right == null && list2[i].right != null)
                        || (list1[i].right != null && list2[i].right == null))
                        return false;

                    if (list1[i].left != null)
                    {
                        next1.Add(list1[i].left);
                        next2.Add(list2[i].left);
                    }
                    if (list1[i].right != null)
                    {
                        next1.Add(list1[i].right);
                        next2.Add(list2[i].right);
                    }
                }

                list1 = next1;
                list2 = next2;
            }
            return true;
        }
        /// 101. Symmetric Tree, #BTree
        ///Given the root of a binary tree, check whether it is a mirror of itself (i.e., symmetric around its center).
        public bool IsSymmetric(TreeNode root)
        {
            return root == null || IsSymmetric(root.left, root.right);
        }

        public bool IsSymmetric(TreeNode left, TreeNode right)
        {
            if (left == null && right == null)
                return true;
            if (left == null || right == null)
                return false;
            if (left.val != right.val)
                return false;
            return IsSymmetric(left.left, right.right) && IsSymmetric(left.right, right.left);
        }

        /// 102. Binary Tree Level Order Traversal, #BTree
        /// Given the root of a binary tree, return the level order traversal of its nodes' values.
        /// (i.e., from left to right, level by level).
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var ans = new List<IList<int>>();
            if (root == null) return ans;
            var nodes = new List<TreeNode>() { root };
            while (nodes.Count > 0)
            {
                var next = new List<TreeNode>();
                var list = new List<int>();
                foreach (TreeNode node in nodes)
                {
                    if (node == null) continue;
                    list.Add(node.val);
                    if (node.left != null) next.Add(node.left);
                    if (node.right != null) next.Add(node.right);
                }
                nodes = next;
                ans.Add(list);
            }
            return ans;
        }

        ///103. Binary Tree Zigzag Level Order Traversal, #BTree
        ///return the zigzag level order traversal of its nodes' values.
        ///(i.e., from left to right, then right to left for the next level and alternate between).
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            var ans=new List<IList<int>>();
            if (root == null) return ans;

            var nodes=new List<TreeNode>() { root};
            bool forward = true;
            while (nodes.Count > 0)
            {
                var nexts = new List<TreeNode>();
                var vals = new List<int>();
                foreach(var node in nodes)
                {
                    vals.Add(node.val);
                    if (forward)
                    {
                        if (node.left != null) nexts.Insert(0, node.left);
                        if (node.right != null) nexts.Insert(0, node.right);
                    }
                    else
                    {
                        if (node.right != null) nexts.Insert(0, node.right);
                        if (node.left != null) nexts.Insert(0, node.left);
                    }
                }
                forward = !forward;
                nodes = nexts;
                ans.Add(vals);
            }
            return ans;
        }

        /// 104. Maximum Depth of Binary Tree, #BTree
        /// Given the root of a binary tree, return its maximum depth.
        ///A binary tree's maximum depth is the number of nodes along the longest path
        ///from the root node down to the farthest leaf node.
        public int MaxDepth_104(TreeNode root)
        {
            if (root == null)
                return 0;
            int depth = 0;
            var nodes = new List<TreeNode>() { root };
            while (nodes.Count > 0)
            {
                depth++;
                var subs = new List<TreeNode>();
                foreach (var node in nodes)
                {
                    if (node == null)
                        continue;
                    if (node.left != null)
                        subs.Add(node.left);
                    if (node.right != null)
                        subs.Add(node.right);
                }
                nodes = subs;
            }
            return depth;
        }
        public int MaxDepth_104_Recursion(TreeNode root)
        {
            int maxDepth = 0;
            MaxDepth_104_Recursion(root, 0, ref maxDepth);
            return maxDepth;
        }

        public void MaxDepth_104_Recursion(TreeNode node, int curr, ref int max)
        {
            if (node == null) return;
            curr++;
            max = Math.Max(curr, max);
            if (node.left != null) MaxDepth_104_Recursion(node.left, curr, ref max);
            if (node.right != null) MaxDepth_104_Recursion(node.right, curr, ref max);
        }

        ///105. Construct Binary Tree from Preorder and Inorder Traversal, #BTree
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            return BuildTree(preorder, inorder, 0, preorder.Length - 1, 0, preorder.Length - 1);
        }
        public TreeNode BuildTree(int[] preorder, int[] inorder, int preLeft, int preRight, int inLeft, int inRight)
        {
            if (preLeft > preRight||inLeft>inRight)
                return null;

            if(preLeft == preRight || inLeft == inRight)
            {
                return new TreeNode(preorder[preLeft]);
            }

            TreeNode node = new TreeNode(preorder[preLeft]);
            var index = BuildTree_FindIndex(preorder[preLeft], inorder, inLeft,inRight);
            var countLeft = index - inLeft;
            var countRight = inRight-index;

            if (countLeft > 0)
            {
                node.left = BuildTree(preorder, inorder, preLeft + 1, preLeft + 1+countLeft-1, inLeft, index - 1);
            }

            if (countRight > 0)
            {
                node.right = BuildTree(preorder, inorder,
                    preLeft + 1 + countLeft - 1+1, preLeft + 1 + countLeft - 1 + 1 + countRight-1,
                    index+1, inRight);
            }
            return node;
        }
        public int BuildTree_FindIndex(int target, int[] array, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (target == array[i]) return i;
            }
            throw new ArgumentOutOfRangeException();
        }

        ///107. Binary Tree Level Order Traversal II, #BTree
        ///Given the root of a binary tree, return the bottom-up level order traversal of its nodes' values.
        ///(i.e., from left to right, level by level from leaf to root).
        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            var ans = new List<IList<int>>();
            if (root == null) return ans;
            var nodes = new List<TreeNode>() { root };
            while (nodes.Count > 0)
            {
                var next = new List<TreeNode>();
                var list = new List<int>();
                foreach (TreeNode node in nodes)
                {
                    if (node == null) continue;
                    list.Add(node.val);
                    if (node.left != null) next.Add(node.left);
                    if (node.right != null) next.Add(node.right);
                }
                nodes = next;
                ans.Insert(0,list);
            }
            return ans;
        }
        /// 108. Convert Sorted Array to Binary Search Tree, #BTree
        public TreeNode SortedArrayToBST(int[] nums)
        {
            Array.Sort(nums);
            return SortedArrayToBST_Recursion(nums, 0, nums.Length - 1);
        }

        private TreeNode SortedArrayToBST_Recursion(int[] nums, int start, int end)
        {
            if (start > end)
                return null;
            if (start == end)
                return new TreeNode(nums[start]);
            var mid = (start + end) / 2;
            var node = new TreeNode(nums[mid]);
            if (mid - 1 >= start)
            {
                node.left = SortedArrayToBST_Recursion(nums, start, mid - 1);
            }
            if (mid + 1 <= end)
            {
                node.right = SortedArrayToBST_Recursion(nums, mid + 1, end);
            }
            return node;
        }
        ///109. Convert Sorted List to Binary Search Tree, #BTree
        ///Given the head of a singly linked list where elements are sorted in ascending order, convert it to a height balanced BST.
        ///a height-balanced binary tree is defined as a binary tree in which the depth of the two subtrees of every node never differ by more than 1.
        public TreeNode SortedListToBST(ListNode head)
        {
            if (head == null)
                return null;
            return SortedListToBST(head, 0);
        }

        public TreeNode SortedListToBST(ListNode head, int count)
        {
            if (count == 0)
            {
                var node = head;
                while(node != null)
                {
                    count++;
                    node = node.next;
                }
            }

            if (count == 1)
            {
                return new TreeNode(head.val);
            }
            else if(count == 2)
            {
                return new TreeNode(head.next.val,new TreeNode(head.val));
            }
            else if(count ==3)
            {
                return new TreeNode(head.next.val, new TreeNode(head.val), new TreeNode(head.next.next.val));
            }
            else
            {
                var mid = count / 2;
                var node = head;
                while (mid > 0)
                {
                    node=node.next;
                    mid--;
                }
                return new TreeNode(node.val, SortedListToBST(head, count / 2), SortedListToBST(node.next, count - count / 2 - 1));
            }
        }

        /// 110. Balanced Binary Tree, #BTree
        ///Given a binary tree, determine if it is height-balanced.
        ///a binary tree in which the left and right subtrees of every node differ in height by no more than 1.
        public bool IsBalanced(TreeNode root)
        {
            if (root == null)
                return true;
            var leftDeep = IsBalanced_Deep(root.left, 1);
            var rightDeep = IsBalanced_Deep(root.right, 1);
            if (leftDeep == -1 || rightDeep == -1)
            {
                return false;
            }
            else
            {
                if(leftDeep-rightDeep>=-1 && leftDeep - rightDeep <= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int IsBalanced_Deep(TreeNode root, int deep)
        {
            if (root == null)
                return deep;

            int leftDeep = IsBalanced_Deep(root.left, deep + 1);
            int rightDeep = IsBalanced_Deep(root.right, deep + 1);

            if (leftDeep == -1 || rightDeep == -1)
            {
                return -1;
            }
            else
            {
                if (leftDeep - rightDeep >= -1 && leftDeep - rightDeep <= 1)
                {
                    return Math.Max(leftDeep, rightDeep);
                }
                else
                {
                    return -1;
                }
            }
        }
        ///111. Minimum Depth of Binary Tree, #BTree
        ///The minimum depth shortest path from the root node down to the nearest leaf node.
        public int MinDepth(TreeNode root)
        {
            if (root == null) return 0;
            int ans = 0;
            List<TreeNode> list=new List<TreeNode>() { root};
            while (list.Count > 0)
            {
                ans++;
                List<TreeNode> next = new List<TreeNode>();
                foreach(var node in list)
                {
                    if (node.left == null && node.right == null) return ans;
                    if (node.left != null) next.Add(node.left);
                    if (node.right != null) next.Add(node.right);
                }
                list = next;
            }
            return ans;
        }


        /// 112. Path Sum, #BTree
        /// Given the root of a binary tree and an integer targetSum,
        /// return true if the tree has a root-to-leaf path such that adding up all the values along the path equals targetSum.
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
                return false;

            var result = HasPathSum_Recursion(root, 0, targetSum);
            if (result)
                return true;

            return false;
        }

        public bool HasPathSum_Recursion(TreeNode node, int sum, int targetSum)
        {
            if (node == null)
                return false;

            sum += node.val;

            if (node.left == null && node.right == null && sum == targetSum)
            {
                return true;
            }

            var resultLeft = HasPathSum_Recursion(node.left, sum, targetSum);
            if (resultLeft)
                return true;
            var resultRight = HasPathSum_Recursion(node.right, sum, targetSum);
            if (resultRight)
                return true;

            return false;
        }

        ///113. Path Sum II, #BTree
        ///return all root-to-leaf paths where the sum of the node values in the path equals targetSum
        public IList<IList<int>> PathSum_113(TreeNode root, int targetSum)
        {
            var ans=new List<IList<int>>();
            PathSum_Recursion(root, targetSum, new List<int>(), ans);
            return ans;
        }

        private void PathSum_Recursion(TreeNode node, int targetSum, IList<int> list, IList<IList<int>> ans)
        {
            if (node == null)
                return;
            list.Add(node.val);
            if (node.left == null && node.right == null)
            {
                if (list.Sum() == targetSum)
                    ans.Add(list);
                return;
            }
            PathSum_Recursion(node.left, targetSum, new List<int>(list), ans);
            PathSum_Recursion(node.right, targetSum, new List<int>(list), ans);
        }

        ///114. Flatten Binary Tree to Linked List, #BTree
        ///Given the root of a binary tree, flatten the tree into a "linked list":
        ///The "linked list" should be in the same order as a pre-order traversal of the binary tree.
        public void Flatten(TreeNode root)
        {
            Flatten(root, null);
        }
        public TreeNode Flatten(TreeNode root, TreeNode pre)
        {
            if (root == null) return pre;
            pre = Flatten(root.right, pre);
            pre = Flatten(root.left, pre);
            root.right = pre;
            root.left = null;
            pre = root;
            return pre;
        }

        public void Flatten_My(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            Flatten_My(root, stack);
        }
        public void Flatten_My(TreeNode root, Stack<TreeNode> stack)
        {
            if (root != null)
            {
                if (root.left == null && root.right == null)
                {
                    if (stack.Count() == 0) return;
                    else
                    {
                        var node = stack.Pop();
                        root.right = node;
                        Flatten_My(root.right, stack);
                    }
                }
                else
                {
                    if (root.left == null)
                    {
                        Flatten_My(root.right, stack);
                    }
                    else
                    {
                        if (root.right != null)
                            stack.Push(root.right);
                        root.right = root.left;
                        root.left = null;
                        Flatten_My(root.right, stack);
                    }
                }
            }
        }
        /// 116. Populating Next Right Pointers in Each Node
        /// You are given a perfect binary tree where all leaves are on the same level
        /// Populate each next pointer to point to its next right node. If there is no next right node, the next pointer should be set to NULL.
        /// Initially, all next pointers are set to NULL.
        public Node_1 Connect_116(Node_1 root)
        {
            if (root == null)
                return null;
            List<Node_1> list = new List<Node_1> { root };
            while (list.Count != 0)
            {
                List<Node_1> subs = new List<Node_1>();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].next = i == list.Count - 1 ? null : list[i + 1];
                    if (list[i].left != null)
                        subs.Add(list[i].left);
                    if (list[i].right != null)
                        subs.Add(list[i].right);
                }
                list = subs;
            }

            return root;
        }

        /// 117. Populating Next Right Pointers in Each Node II
        /// Populate each next pointer to point to its next right node.
        /// If there is no next right node, the next pointer should be set to NULL.
        public Node_1 Connect(Node_1 root)
        {
            if (root == null)
                return null;
            List<Node_1> list = new List<Node_1>
            {
                root
            };
            while (list.Count != 0)
            {
                List<Node_1> subs = new List<Node_1>();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].next = i == list.Count - 1 ? null : list[i + 1];
                    if (list[i].left != null)
                        subs.Add(list[i].left);
                    if (list[i].right != null)
                        subs.Add(list[i].right);
                }
                list = subs;
            }

            return root;
        }

        ///118. Pascal's Triangle
        ///Given an integer numRows, return the first numRows of Pascal's triangle.
        ///    1
        ///   1 1
        ///  1 2 1
        /// 1 3 3 1
        public IList<IList<int>> Generate(int numRows)
        {
            List<IList<int>> list = new List<IList<int>>();
            int i = 1;
            list.Add(new List<int>() { 1 });
            i++;
            while (i <= numRows)
            {
                var list2 = new List<int>();
                int j = 1;
                list2.Add(1);
                j++;
                while (j < i)
                {
                    list2.Add(list[i - 1 - 1][j - 1 - 1] + list[i - 1 - 1][j - 1]);
                    j++;
                }
                list2.Add(1);
                list.Add(list2);
                i++;
            }
            return list;
        }

        ///119. Pascal's Triangle II
        ///Given an integer rowIndex, return the rowIndexth (0-indexed) row of the Pascal's triangle.
        ///In Pascal's triangle, each number is the sum of the two numbers directly above it as shown:
        public IList<int> GetRow(int rowIndex)
        {
            List<int> currentRow = new List<int>() { 1 };
            int lastRowIndex = 0;
            while (lastRowIndex < rowIndex)
            {
                var list2 = new List<int>();
                int j = 0;
                list2.Add(1);
                j++;
                while (j < lastRowIndex + 1)
                {
                    list2.Add(currentRow[j - 1] + currentRow[j]);
                    j++;
                }
                list2.Add(1);
                currentRow = list2;
                lastRowIndex++;
            }

            return currentRow;
        }

        ///120. Triangle
        ///Given a triangle array, return the minimum path sum from top to bottom.
        ///   2
        ///  3 4
        /// 6 5 7   => 2 3 5
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            if (triangle == null)
                return 0;

            int level = 1;
            List<int> minList = new List<int>
            {
                triangle[level - 1][0]
            };

            while (++level <= triangle.Count)
            {
                List<int> subList = new List<int>();

                for (int i = 0; i < level; i++)
                {
                    if (i == 0)
                    {
                        subList.Add(triangle[level - 1][0] + minList[0]);
                    }
                    else if (i > 0 && i < level - 1)
                    {
                        subList.Add(Math.Min(triangle[level - 1][i] + minList[i - 1], triangle[level - 1][i] + minList[i]));
                    }
                    else
                    {
                        subList.Add(triangle[level - 1][level - 1] + minList[level - 1 - 1]);
                    }
                }

                minList = subList;
            }

            return minList.Min();
        }

        ///121. Best Time to Buy and Sell Stock - trade 1 time
        public int MaxProfit_121_OnlyOneTrade(int[] prices)
        {
            if (prices == null || prices.Length <= 1)
                return 0;

            if (prices.Length == 2)
            {
                if (prices[0] >= prices[1])
                    return 0;
                else
                    return prices[1] - prices[0];
            }

            int MinSoFar = prices[0];
            int maxProfit = 0;
            foreach (var currentPrice in prices)
            {
                MinSoFar = Math.Min(MinSoFar, currentPrice);
                maxProfit = Math.Max(maxProfit, currentPrice - MinSoFar);
            }
            return maxProfit;
        }

        ///122. Best Time to Buy and Sell Stock II - can trade many times
        ///1 <= prices.length <= 10^5, 0 <= prices[i] <= 10^4
        public int MaxProfit_122_TradeManyTimes(int[] prices)
        {
            int sum = 0;
            bool isHold = true;
            int buy = prices[0];

            for(int i = 1; i < prices.Length; i++)
            {
                if (isHold)
                {
                    if (i < prices.Length - 1 )
                    {

                        if (prices[i] < buy)
                        {
                            buy = prices[i];
                        }
                        else
                        {
                            if (prices[i] > prices[i + 1])
                            {
                                if (prices[i] > buy)
                                {
                                    isHold = false;
                                    sum += prices[i] - buy;
                                }
                            }
                        }
                    }
                    else
                    {
                        if(prices[i] > buy)
                            sum+=prices[i]-buy;
                    }

                }
                else
                {
                    buy=prices[i];
                    isHold = true;
                }
            }
            return sum;
        }

        ///124. Binary Tree Maximum Path Sum, #Backtracking, #BTree
        ///A path in a binary tree is a sequence of nodes where each pair of adjacent nodes in the sequence has an edge connecting them.
        ///A node can only appear in the sequence at most once. Note that the path does not need to pass through the root.
        ///The path sum of a path is the sum of the node's values in the path.
        ///Given the root of a binary tree, return the maximum path sum of any non-empty path.
        ///The number of nodes in the tree is in the range [1, 3 * 104]., -1000 <= Node.val <= 1000
        public int MaxPathSum(TreeNode root)
        {
            int ans = int.MinValue;
            MaxPathSum_BackTracking(root, ref ans);
            return ans;
        }
        public int MaxPathSum_BackTracking(TreeNode root, ref int ans)
        {
            if (root == null) return 0;
            int left = Math.Max(0, MaxPathSum_BackTracking(root.left, ref ans));
            int right = Math.Max(0, MaxPathSum_BackTracking(root.right, ref ans));
            ans = Math.Max(ans, root.val + left + right);
            return Math.Max(root.val + left, root.val + right);
        }

        /// 125. Valid Palindrome, #Two Pointers
        ///after converting all uppercase letters into lowercase letters and removing all non-alphanumeric characters,
        ///a-z, A-Z, 0-9
        public bool IsPalindrome(string s)
        {
            List<char> list = new List<char>();
            foreach (var c in s)
            {
                if (char.IsLetter(c))
                    list.Add(char.ToLower(c));
                if (char.IsDigit(c))
                    list.Add(c);
            }
            for (int i = 0; i < list.Count / 2; i++)
            {
                if (list[i] != list[list.Count - 1 - i])
                    return false;
            }
            return true;
        }

        public bool IsPalindrome_TwoPointers(string s)
        {
            int left = 0;
            int right = s.Length - 1;
            while (left < right)
            {
                while (left < right && !char.IsLetterOrDigit(s[left]))
                {
                    left++;
                }
                while (left < right && !char.IsLetterOrDigit(s[right]))
                {
                    right--;
                }
                if (left < right && char.ToLower(s[left]) != char.ToLower(s[right]))
                {
                    return false;
                }
                left++;
                right--;
            }
            return true;
        }

        ///126. Word Ladder II, #Graph , #BFS
        //A transformation sequence from beginWord to endWord is a sequence beginWord -> s1 -> s2 -> ... -> sk such that:
        //adjacent pair of words differs by a single letter. sk == endWord, beginWord does not need to be in wordList.
        //return all the shortest transformation sequences from beginWord to endWord, or an empty list
        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            if (!wordList.Contains(endWord)) return new List<IList<string>>();

            Dictionary<string, HashSet<string>> graph = new Dictionary<string, HashSet<string>>();
            graph.Add(beginWord, new HashSet<string>());
            foreach (var w in wordList)
            {
                if (!graph.ContainsKey(w)) graph.Add(w, new HashSet<string>());
                if (oneCharDiff(beginWord, w))
                    graph[beginWord].Add(w);
            }

            foreach (var w1 in wordList)
            {
                foreach (var w2 in wordList)
                {
                    if (oneCharDiff(w1, w2))
                    {
                        graph[w1].Add(w2);
                        graph[w2].Add(w1);
                    }
                }
            }

            var res = new List<IList<string>>();
            res.Add(new List<string>() { beginWord });
            var visit = new HashSet<string>() { beginWord };
            while (res.Count > 0)
            {
                var next = new List<IList<string>>();
                foreach (var list in res)
                {
                    var prev = list.Last();
                    foreach (var word in graph[prev])
                    {
                        if (visit.Contains(word)) continue;
                        next.Add(new List<string>(list) { word });
                    }
                }
                res = next;
                foreach(var list in next)
                {
                    visit.Add(list.Last());
                }
                if (visit.Contains(endWord)) break;
            }
            return res.Where(x=>x.Last()==endWord).ToList();
        }

        private bool oneCharDiff(string origin, string target)
        {
            if (origin == target) return false;
            int diff = 0;
            for (int i = 0; i < origin.Length; i++)
            {
                if (origin[i] != target[i]) diff++;
                if (diff > 1) return false;
            }
            return diff == 1;
        }
        /// 127. Word Ladder, #Graph, #BFS,
        // A transformation sequence from word beginWord to word endWord using a dictionary wordList
        // is a sequence of words beginWord -> s1 -> s2 -> ... -> sk such that:
        // Every adjacent pair of words differs by a single letter.
        // Note that beginWord does not need to be in wordList, endWord = wordlist.Last()
        // return the number of words in the shortest transformation sequence from beginWord to endWord, or 0 if not exists.
        // 1 <= beginWord.length <= 10, beginWord != endWord
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            if (!wordList.Contains(endWord)) return 0;
            List<string> list = new List<string>() { beginWord };
            var words = new HashSet<string>(wordList);
            words.Remove(beginWord);
            int depth = 1;
            while (list.Count > 0)
            {
                depth++;
                List<string> next = new List<string>();
                foreach(var curr in list)
                {
                    var canVisitWords = words.Where(x => oneCharDiff(curr, x));
                    foreach (var word in canVisitWords)
                    {
                        if (word == endWord) return depth;
                        next.Add(word);
                        words.Remove(word);
                    }
                }
                list = next;
            }
            return 0;
        }


        /// 128. Longest Consecutive Sequence
        /// Given an unsorted array of integers nums, return the length of the longest consecutive elements sequence. O(n) time.
        /// 0 <= nums.length <= 105, -10^9 <= nums[i] <= 10^9
        public int LongestConsecutive(int[] nums)
        {
            if (nums.Length <= 1) return nums.Length;
            Array.Sort(nums);
            int max = 1;
            int count = 1;
            int pre = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] <= pre + 1)
                {
                    if (nums[i] != pre) count++;
                }
                else
                {
                    max = Math.Max(count, max);
                    count = 1;
                }
                pre = nums[i];
            }
            return Math.Max(count, max);
        }

        public int LongestConsecutive_HashMap_O_n(int[] nums)
        {
            int res = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (int n in nums)
            {
                if (!dict.ContainsKey(n))
                {
                    int left = (dict.ContainsKey(n - 1)) ? dict[n - 1] : 0;
                    int right = (dict.ContainsKey(n + 1)) ? dict[n + 1] : 0;
                    int sum = left + right + 1;
                    dict.Add(n, sum);
                    // keep track of the max length
                    res = Math.Max(res, sum);
                    // extend the length to the boundary(s)
                    // of the sequence
                    // will do nothing if n has no neighbors
                    if(dict.ContainsKey(n - left))
                    {
                        dict[n - left]=sum;
                    }
                    else
                    {
                        dict.Add(n - left, sum);
                    }
                    if (dict.ContainsKey(n + right))
                    {
                        dict[n + right] = sum;
                    }
                    else
                    {
                        dict.Add(n + right, sum);
                    }
                }
            }
            return res;
        }

        ///129. Sum Root to Leaf Numbers, #BTree
        ///Return the total sum of all root-to-leaf numbers.A leaf node is a node with no children.
        public int SumNumbers(TreeNode root)
        {
            int res = 0;
            SumNumbers(root, 0, ref res);
            return res;
        }

        private void SumNumbers(TreeNode root, int curr, ref int res)
        {
            if (root == null) { return; }
            if (root.left == null && root.right == null)
            {
                res += curr * 10 + root.val;
            }
            else
            {
                SumNumbers(root.left, curr * 10 + root.val, ref res);
                SumNumbers(root.right, curr * 10 + root.val, ref res);
            }
        }
        /// 130. Surrounded Regions
        ///Given an m x n matrix board containing 'X' and 'O',
        ///capture all regions that are 4-directionally surrounded by 'X'.
        ///A region is captured by flipping all 'O's into 'X's in that surrounded region.
        ///Tips: If 'O's connect to 4-direction edges , escape; or captured
        public void Solve(char[][] board)
        {

            int rowLen = board.Length;
            int colLen = board[0].Length;

            if (rowLen <= 1 || colLen <= 1)
                return;

            bool[][] visit = new bool[rowLen][];
            for (int i = 0; i < rowLen; i++)
            {
                visit[i] = new bool[colLen];
            }

            int[][] dxy = new int[4][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, -1 } };

            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (visit[i][j])
                        continue;

                    visit[i][j] = true;

                    if (board[i][j] == 'O')
                    {
                        List<int[]> list = new List<int[]>();

                        List<int[]> currList = new List<int[]>
                        {
                            new int[] { i, j }
                        };

                        bool capture = true;

                        while (currList.Count > 0)
                        {
                            list.AddRange(currList);

                            List<int[]> subList = new List<int[]>();

                            foreach (var cell in list)
                            {

                                foreach (var d in dxy)
                                {
                                    int r = cell[0] + d[0];
                                    int c = cell[1] + d[1];

                                    if (r >= 0 && r <= rowLen - 1
                                        && c >= 0 && c <= colLen - 1)
                                    {
                                        if (!visit[r][c])
                                        {
                                            visit[r][c] = true;

                                            if (board[r][c] == 'O')
                                            {
                                                subList.Add(new int[] { r, c });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        capture = false;
                                    }
                                }
                            }

                            currList = subList;
                        }

                        if (capture)
                        {
                            foreach (var cell in list)
                                board[cell[0]][cell[1]] = 'X';
                        }

                    }

                }
            }

        }

        ///131. Palindrome Partitioning, #Backtracking
        ///Given a string s, partition s such that every substring of the partition is a palindrome.
        ///Return all possible palindrome partitioning of s.
        ///A palindrome string is a string that reads the same backward as forward.
        ///1 <= s.length <= 16, lowercase
        public IList<IList<string>> Partition(string s)
        {
            var ans=new List<IList<string>>();
            var list = new List<string>();
            Partition_BackTracking(s, list, ans);
            return ans;
        }

        private void Partition_BackTracking(string s, IList<string> list, IList<IList<string>> ans)
        {
            for(int i=0; i<s.Length-1; i++)
            {
                var str=s.Substring(0,i+1);
                if (Partition_IsPalindrome(str))
                {
                    var sub = s.Substring(i + 1);
                    var nextList = new List<string>(list) { str };
                    Partition_BackTracking(sub, nextList, ans);
                }
            }

            if (Partition_IsPalindrome(s))
            {
                var nextList = new List<string>(list) { s };
                ans.Add(nextList);
            }
        }

        private bool Partition_IsPalindrome(string str)
        {
            int i = 0;
            while (i < str.Length / 2)
            {
                if (str[i] != str[str.Length - 1 - i]) return false;
                i++;
            }
            return true;
        }

        ///132. Palindrome Partitioning II,#DP
        ///Given a string s, partition s such that every substring of the partition is a palindrome.
        ///Return the minimum cuts needed for a palindrome partitioning of s.
        public int MinCut(string s)
        {
            if (s == null || s.Length <= 1)
            {
                return 0;
            }
            // dp
            int n = s.Length;
            int[] dp = new int[n]; // initial value: dp[i] = i
            for (int i = 0; i < n; i++)
                dp[i] = i;

            for (int mid = 1; mid < n; mid++)
            {
                // iterate through all chars as mid point of palindrome
                // CASE 1. odd len: center is at index mid, expand on both sides
                for (int start = mid, end = mid; start >= 0 && end < n && s[start] == s[end]; start--, end++)
                {
                    int newCutAtEnd = (start == 0) ? 0 : dp[start - 1] + 1;
                    dp[end] = Math.Min(dp[end], newCutAtEnd);
                }
                // CASE 2: even len: center is between [mid-1,mid], expand on both sides
                for (int start = mid - 1, end = mid; start >= 0 && end < n && s[start] == s[end]; start--, end++)
                {
                    int newCutAtEnd = (start == 0) ? 0 : dp[start - 1] + 1;
                    dp[end] = Math.Min(dp[end], newCutAtEnd);
                }
            }
            return dp[n - 1];
        }

        public int MinCut2(string s)
        {
            //cut[i] is the minimum of cut[j - 1] + 1 (j <= i), if [j, i] is palindrome.
            //If[j, i] is palindrome, [j +1, i - 1] is palindrome, and arr[j] == arr[i].
            char[] arr = s.ToArray();
            int n = arr.Length;
            int[] cut = new int[n];
            bool[,] pal = new bool[n, n];

            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = 0; j <= i; j++)
                {
                    if (arr[j] == arr[i] && (j + 1 > i - 1 || pal[j + 1, i - 1]))
                    {
                        pal[j, i] = true;
                        min = j == 0 ? 0 : Math.Min(min, cut[j - 1] + 1);
                    }
                }
                cut[i] = min;
            }
            return cut[n - 1];
        }

        /// 133. Clone Graph, #Graph, #DFS
        ///Given a reference of a node in a connected undirected graph.
        ///Return a deep copy(clone) of the graph.
        ///Each node in the graph contains a value(int) and a list(List[Node]) of its neighbors.
        public Node_Neighbors CloneGraph(Node_Neighbors node)
        {
            if (node == null) return null;
            Node_Neighbors ans = null;
            Dictionary<Node_Neighbors,int> dict= new Dictionary<Node_Neighbors, int>();
            List<Node_Neighbors> list= new List<Node_Neighbors>();
            ans=new Node_Neighbors(node.val);
            dict.Add(node, list.Count);
            list.Add(ans);
            ans.neighbors = CloneGraph_Recursion(node.neighbors, dict, list);
            return ans;
        }

        public IList<Node_Neighbors> CloneGraph_Recursion(IList<Node_Neighbors> neighbors, IDictionary<Node_Neighbors, int> dict, IList<Node_Neighbors> list)
        {
            if (neighbors == null) return null;
            if(neighbors.Count==0) return new List<Node_Neighbors>();
            var ans=new List<Node_Neighbors>();
            foreach(var node in neighbors)
            {
                if (dict.ContainsKey(node))
                {
                    ans.Add(list[dict[node]]);
                }
                else
                {
                    dict.Add(node, list.Count);
                    var clone = new Node_Neighbors(node.val);
                    list.Add(clone);
                    ans.Add(clone);
                    clone.neighbors = CloneGraph_Recursion(node.neighbors, dict, list);
                }
            }
            return ans;
        }

        /// 134. Gas Station
        ///  Given two integer arrays gas and cost, return the starting gas station's index
        ///  if you can travel around the circuit once in the clockwise direction,
        ///  otherwise return -1. If there exists a solution, it is guaranteed to be unique
        /// start/arrive at i-th, will get gas[i]; go to next will cost[i]

        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int ans = -1;

            for (int i = 0; i < gas.Length; i++)
            {
                if (gas[i] == 0 || gas[i] < cost[i])
                    continue;

                int sum = gas[i] - cost[i];
                int j = i == gas.Length - 1 ? 0 : i + 1;

                while (j != i)
                {
                    sum = sum + gas[j] - cost[j];
                    if (sum < 0)
                        break;
                    j = j == gas.Length - 1 ? 0 : j + 1;
                }

                if (j == i)
                    return i;
            }

            return ans;
        }
        ///135. Candy ,#DP, #DFS
        ///There are n children standing in a line. Each child is assigned a rating value given in the integer array ratings.
        ///Each child must have at least one candy, Children with a higher rating get more candies than their neighbors.
        ///Return the minimum number of candies you need to have to distribute the candies to the children.
        public int Candy(int[] ratings)
        {
            int n = ratings.Length;
            int[] dp=new int[n];
            List<int> list = new List<int>();

            for (int i=0;i<n;i++)
            {
                if((i==0 || ratings[i] <=ratings[i-1])
                    && (i == n-1 || ratings[i] <= ratings[i + 1]))
                {
                    list.Add(i);
                    dp[i] = 1;
                }
            }

            while (list.Count > 0)
            {
                var next = new List<int>();
                foreach(var i in list)
                {
                    if(i>0 && ratings[i - 1] > ratings[i])
                    {
                        dp[i - 1] = Math.Max(dp[i - 1], dp[i] + 1);
                        next.Add(i - 1);
                    }
                    if (i < n-1 && ratings[i + 1] > ratings[i])
                    {
                        dp[i + 1] = Math.Max(dp[i + 1], dp[i] + 1);
                        next.Add(i + 1);
                    }
                }
                list = next;
            }
            return dp.Sum();
        }
        /// 136. Single Number
        /// Given a non - empty array of integers nums, every element appears twice except for one.Find that single one.
        /// You must implement a solution with a linear runtime complexity and use only constant extra space.
        public int SingleNumber_136(int[] nums)
        {
            return nums.Aggregate((x, y) => x ^ y);
        }

        ///137. Single Number II
        ///Given an integer array nums where every element appears three times except for one, which appears exactly once.
        ///Find the single element and return it.
        public int SingleNumber_137(int[] nums)
        {
            Dictionary<int,int> dict=new Dictionary<int, int>();
            foreach(var n in nums)
            {
                if (dict.ContainsKey(n))
                {
                    if(dict[n] ==2)dict.Remove(n);
                    else dict[n]++;
                }
                else
                {
                    dict.Add(n, 1);
                }
            }
            var ans = dict.FirstOrDefault(x => x.Value != 3).Key;
            return ans;
        }

        ///138. Copy List with Random Pointer
        ///Random may pointer to itself
        public Node CopyRandomList_My(Node head)
        {
            if (head == null) return null;
            Node brand = null;
            Node tail = null;

            List<Node> inputs = new List<Node>();
            List<Node> list = new List<Node>();
            var node = head;
            while (node != null)
            {
                if(brand == null)
                {
                    brand = new Node(node.val);
                    tail = brand;

                    inputs.Add(node);
                    list.Add(tail);

                    if (node.random == null)
                    {
                        tail.random = null;
                    }
                    else
                    {
                        var rIndex = inputs.IndexOf(node.random);
                        if (rIndex == -1)
                        {
                            tail.random = new Node(node.random.val);
                            list.Add(tail.random);
                            inputs.Add(node.random);
                        }
                        else
                        {
                            tail.random = list[rIndex];
                        }
                    }
                }
                else
                {
                    var index = inputs.IndexOf(node);
                    if (index == -1)
                    {
                        tail.next =new Node(node.val);
                        tail = tail.next;
                        list.Add(tail);
                        inputs.Add(node);
                    }
                    else
                    {
                        tail.next = list[index];
                        tail = tail.next;
                    }

                    if (node.random == null)
                    {
                        tail.random = null;
                    }
                    else
                    {
                        var rIndex = inputs.IndexOf(node.random);
                        if(rIndex == -1)
                        {
                            tail.random = new Node(node.random.val);
                            list.Add(tail.random);
                            inputs.Add(node.random);
                        }
                        else
                        {
                            tail.random=list[rIndex];
                        }
                    }
                }
                node = node.next;
            }

            return brand;
        }

        public Node CopyRandomList(Node head)
        {
            Node iter = head, next;

            // First round: make copy of each node,
            // and link them together side-by-side in a single list.
            while (iter != null)
            {
                next = iter.next;

                Node copy1 = new Node(iter.val);
                iter.next = copy1;
                copy1.next = next;

                iter = next;
            }

            // Second round: assign random pointers for the copy nodes.
            iter = head;
            while (iter != null)
            {
                if (iter.random != null)
                {
                    iter.next.random = iter.random.next;
                }
                iter = iter.next.next;
            }

            // Third round: restore the original list, and extract the copy list.
            iter = head;
            Node pseudoHead = new Node(0);
            Node copy, copyIter = pseudoHead;

            while (iter != null)
            {
                next = iter.next.next;

                // extract the copy
                copy = iter.next;
                copyIter.next = copy;
                copyIter = copy;

                // restore the original list
                iter.next = next;

                iter = next;
            }

            return pseudoHead.next;
        }
        /// 139. Word Break, #DP, #Backtracking
        ///return true if s can be segmented into a space-separated sequence of one or more dictionary words.
        ///Note that the same word in the dictionary may be reused multiple times in the segmentation.
        public bool WordBreak139_DP(string s, IList<string> wordDict)
        {
            bool[] dp = new bool[s.Length + 1];
            dp[0] = true;
            for(int i = 0; i < s.Length; i++)
            {
                if (dp[i])
                {
                    foreach(var word in wordDict)
                    {
                        if (i + word.Length <= s.Length)
                        {
                            if (s.Substring(i, word.Length) == word)
                            {
                                dp[i + word.Length] = true;
                                if (i + word.Length == s.Length) return true;
                            }

                        }
                    }
                }
            }
            return false;
        }

        public bool WordBreak139_Backtracking(string s, IList<string> wordDict)
        {
            bool ans = false;
            WordBreak139_Backtracking(s, wordDict, new Dictionary<string, int>(), ref ans);
            return ans;
        }
        private void WordBreak139_Backtracking(string s, IList<string> wordDict, IDictionary<string, int> existLens, ref bool ans)
        {
            if (ans || existLens.ContainsKey(s)) return;
            existLens.Add(s, 1);
            if (s.Length == 0)
            {
                ans = true;
                return;
            }
            foreach (var w in wordDict)
            {
                if (s.StartsWith(w))
                {
                    WordBreak139_Backtracking(s.Substring(w.Length), wordDict, existLens, ref ans);
                }
            }
        }

        ///140. Word Break II, #Backtracking, #DP
        ///Given a string s and a dictionary of strings wordDict,
        ///add spaces in s to construct a sentence where each word is a valid dictionary word.
        ///Return all such possible sentences in any order.
        public IList<string> WordBreak_DP(string s, IList<string> wordDict)
        {
            bool[] dp = new bool[s.Length + 1];
            Dictionary<int, List<List<string>>> map = new Dictionary<int, List<List<string>>> ();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var w in wordDict)
            {
                if(!string.IsNullOrEmpty(w))
                    dict.Add(w, 1);
            }
            dp[0] = true;
            map.Add(0, new List<List<string>>() );
            for (int i = 0; i < s.Length; i++)
            {
                if (dp[i])
                {
                    foreach (var key in dict.Keys)
                    {
                        int index = i + key.Length;
                        if (index <= s.Length)
                        {
                            if (s.Substring(i, key.Length) == key)
                            {
                                dp[index] = true;
                                if (!map.ContainsKey(index))
                                    map.Add(index, new List<List<string>>());
                                if (i == 0)
                                {
                                    map[index].Add(new List<string>() { key });
                                }
                                else
                                {
                                    foreach(var list in map[i])
                                    {
                                        map[index].Add(new List<string>(list) { key });
                                    }
                                }
                            }
                        }
                    }
                }
                //if(map.ContainsKey(i))map.Remove(i);
            }
            if (map.ContainsKey(s.Length))
            {
                return map[s.Length].Select(x=>String.Join(" ",x)).ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        public IList<string> WordBreak140_Backtracking(string s, IList<string> wordDict)
        {
            var ans=new Dictionary<string, int>();
            var list=new List<string>();
            WordBreak140_Backtracking(s, wordDict, list, ans);
            return ans.Keys.ToList();
        }
        public void WordBreak140_Backtracking(string s, IList<string> wordDict, IList<string> list,IDictionary<string,int> ans)
        {
            if (s.Length == 0)
            {
                var str = string.Join(" ", list);
                if (!ans.ContainsKey(str))
                    ans.Add(str,1);
                return;
            }
            foreach (var w in wordDict)
            {
                if (s.StartsWith(w))
                {
                    var next = new List<string>(list) { w };
                    WordBreak140_Backtracking(s.Substring(w.Length), wordDict, next, ans);
                }
            }
        }

        /// 141. Linked List Cycle, #Two Pointers ->O(1) space
        /// Return true if there is a cycle in the linked list. Otherwise, return false.
        public bool HasCycle(ListNode head)
        {
            ListNode walker = head;
            ListNode runner = head;
            //if there is any cycle, runner will never end, then meet walker
            //walkerLen =  (lenBeforeCycle + lenInCycle)
            //runnerLen =  2* (lenBeforeCycle + lenInCycle)
            //runnerlen - walkerLen = lenBeforeCycle + lenInCycle = N* cycleLen
            while (runner.next != null && runner.next.next != null)
            {
                walker = walker.next;
                runner = runner.next.next;
                if (walker == runner) return true;
            }
            return false;
        }

        ///142. Linked List Cycle II, #Two Pointers ->O(1) space
        ///If there is no cycle, return null.
        public ListNode DetectCycle(ListNode head)
        {
            ListNode walker = head;
            ListNode runner = head;
            //if there is any cycle, runner will never end, then meet walker
            //walkerLen =  (lenBeforeCycle + lenInCycle)
            //runnerLen =  2* (lenBeforeCycle + lenInCycle)
            //runnerlen - walkerLen = lenBeforeCycle + lenInCycle = N* cycleLen
            while (runner.next != null && runner.next.next != null)
            {
                walker = walker.next;
                runner = runner.next.next;
                if (walker == runner)
                    break;
            }
            if (runner == null || runner.next == null)
            {
                return null;
            }
            else
            {
                //it will multiple of cycle length because it have to run cycle n-times till when slow will be equal to fast.
                // so it will be always some multiple of cycle for fast pointer.
                // nodeLen = lenBeforeCycle + X*cycleLen, if node is begin
                // walkerLen = N* cycleLen + lenBeforeCycle + X*cycleLen = lenBeforeCycle + lenInCycle + lenBeforeCycle + X*cycleLen
                // after N*cycleLen, they will meet
                var node = head;
                while (node != walker)
                {
                    node = node.next;
                    walker = walker.next;
                }
                return node;
            }
        }
        ///143. Reorder List
        ///Reorder the list: 1->2->3->4 to 1->4->2->3, 1->2->3->4->5 to 1->5->2->4->3
        public void ReorderList(ListNode head)
        {
            Stack<ListNode> stack = new Stack<ListNode>();
            var node=head;
            while(node != null)
            {
                stack.Push(node);
                node=node.next;
            }
            int i=stack.Count;
            node = head;
            if (i <= 2)
                return;
            while (i > 0)
            {
                var next = node.next;
                var pop=stack.Pop();
                pop.next = next;
                node.next = pop;
                //last = pop;
                node= next;
                i-=2;
                if (i == 2)
                {
                    node.next.next = null;
                    break;
                }
                if (i == 1)
                {
                    node.next = null;
                    break;
                }
            }
        }
        /// 144. Binary Tree Preorder Traversal, #BTree
        /// Node -> Left -> Right
        public IList<int> PreorderTraversal_Iteratively(TreeNode root)
        {
            var ans = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node == null) break;
                ans.Add(node.val);
                if (node.right != null) stack.Push(node.right);
                if (node.left != null) stack.Push(node.left);
            }
            return ans;
        }
        public IList<int> PreorderTraversal_Recursion(TreeNode root)
        {
            var result = new List<int>();
            PreorderTraversal_Recursion(root, result);
            return result;
        }

        public void PreorderTraversal_Recursion(TreeNode node, IList<int> list)
        {
            if (node == null) return;
            list.Add(node.val);
            PreorderTraversal_Recursion(node.left, list);
            PreorderTraversal_Recursion(node.right, list);
        }

        ///145. Binary Tree Postorder Traversal, #BTree
        /// Left -> Right -> Node
        public IList<int> PostorderTraversal_Iteratively(TreeNode root)
        {
            var ans = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node == null) { break; }
                var left = node.left;
                var right = node.right;
                node.left = null;
                node.right = null;
                if (left != null)
                {
                    if (right != null)
                    {
                        stack.Push(node);
                        stack.Push(right);
                        stack.Push(left);
                        continue;
                    }
                    else
                    {
                        stack.Push(node);
                        stack.Push(left);
                        continue;
                    }
                }
                else
                {
                    if (right != null)
                    {
                        stack.Push(node);
                        stack.Push(right);
                        continue;
                    }
                }
                ans.Add(node.val);
            }
            return ans;
        }

        public IList<int> PostorderTraversal_Recursion(TreeNode root)
        {
            var result = new List<int>();
            PostorderTraversal_Recursion(root, result);
            return result;
        }

        public void PostorderTraversal_Recursion(TreeNode node, IList<int> list)
        {
            if (node == null) return;
            PostorderTraversal_Recursion(node.left, list);
            PostorderTraversal_Recursion(node.right, list);
            list.Add(node.val);
        }

        ///146. LRU Cache, see LRUCache

        ///147. Insertion Sort List
        ///Given the head of a singly linked list, sort the list using insertion sort, and return the sorted list's head.
        public ListNode InsertionSortList(ListNode head)
        {
            ListNode res = null;
            var node = head;
            while(node != null)
            {
                var temp = node.next;
                if (res == null)
                {
                    res = node;
                    res.next = null;//remember set next of res to null to avoid endless loop
                }
                else
                {
                    if (node.val < res.val)
                    {
                        node.next = res;
                        res = node;
                    }
                    else
                    {
                        var prev = res;
                        while (prev != null && prev.next!=null && prev.next.val<node.val)
                        {
                            prev = prev.next;
                        }
                        node.next = prev.next;
                        prev.next = node;

                    }
                }
                node = temp;
            }
            return res;
        }

        /// 148. Sort List
        ///Given the head of a linked list, return the list after sorting it in ascending order.
        ///O(n logn) time and O(1) memory
        public ListNode SortList(ListNode head)
        {
            if (head == null) return null;
            ListNode ans = null;
            var node = head;
            List<ListNode> list=new List<ListNode> ();
            while(node != null)
            {
                list.Add (node);
                node = node.next;
            }
            list.Sort((x, y) =>
            {
                return x.val - y.val;
            });
            ans = list[0];
            for(int i = 0; i < list.Count; i++)
            {
                list[i].next = i < list.Count - 1 ? list[i + 1] : null;
            }
            return ans;
        }

        /// 149. Max Points on a Line
        ///return the maximum number of points that lie on the same straight line.
        ///1 <= points.length <= 300, -10^4 <= xi, yi <= 10^4, All the points are unique.
        public int MaxPoints(int[][] points)
        {
            if (points.Length <= 2)
                return points.Length;

            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
            Dictionary<int, List<int>> dictX = new Dictionary<int, List<int>>();
            //Dictionary<int, List<int>> dictY = new Dictionary<int, List<int>>();

            for (int i = 0; i < points.Length; i++)
            {
                if (i == 0)
                {
                    dictX.Add(points[i][0], new List<int>() { i});
                    continue;
                }

                List<string> findKeys = new List<string>();
                foreach(var d in dict.Keys)
                {
                    var strs = d.Split(',');
                    var a = double.Parse(strs[0]);
                    var b = double.Parse(strs[1]);

                    if (points[i][0] * a + b == points[i][1])
                    {
                        findKeys.Add(d);
                    }
                }

                List<int> skips = new List<int>();
                foreach (var findKey in findKeys)
                {
                    skips.AddRange(dict[findKey]);
                    dict[findKey].Add(i);
                }

                for (int j = 0; j < i; j++)
                {
                    if (skips.Contains(j))
                        continue;

                    if (points[i][0] == points[j][0])
                        continue;

                    var a = 1.0 * (points[i][1] - points[j][1]) / (points[i][0] - points[j][0]);
                    var b = 1.0 * (points[j][1]* points[i][0] - points[j][0]* points[i][1]) / (points[i][0] - points[j][0]);

                    string key = $"{a},{b}";

                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, new List<int> { j, i });
                    }
                    else
                    {
                        if(!dict[key].Contains(i))
                            dict[key].Add(i);
                        if (!dict[key].Contains(j))
                            dict[key].Add(j);
                    }
                }

                if (dictX.ContainsKey(points[i][0]))
                {
                    dictX[points[i][0]].Add(i);
                }
                else
                {
                    dictX.Add(points[i][0], new List<int>() { i });
                }
            }

            int count1 = 0;
            if (dict.Count > 0)
            {
                count1 = dict.Values.Select(x => x.Count).Max();
            }

            int count2 = 0;
            if (dictX.Count > 0)
            {
                count2 = dictX.Values.Select(x => x.Count).Max();
            }
            return Math.Max(count1,count2);
        }
    }
}