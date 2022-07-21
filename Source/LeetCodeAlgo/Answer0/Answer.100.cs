using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///100. Same Tree, #BTree
        public bool IsSameTree_Recursion(TreeNode p, TreeNode q)
        {
            if (p == null && q == null) return true;
            if (p == null || q == null) return false;
            if (p.val != q.val) return false;
            return IsSameTree_Recursion(p.left, q.left) && IsSameTree_Recursion(p.right, q.right);
        }

        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            var q1 = new Queue<TreeNode>();
            var q2 = new Queue<TreeNode>();
            q1.Enqueue(p);
            q2.Enqueue(q);
            while (q1.Count > 0)
            {
                var size = q1.Count;
                while (size-- > 0)
                {
                    var n1 = q1.Dequeue();
                    var n2 = q2.Dequeue();
                    if (n1 == null && n2 == null) continue;
                    if (n1 == null || n2 == null) return false;
                    if (n1.val != n2.val) return false;
                    q1.Enqueue(n1.left);
                    q1.Enqueue(n1.right);
                    q2.Enqueue(n2.left);
                    q2.Enqueue(n2.right);
                }
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
            var ans = new List<IList<int>>();
            if (root == null) return ans;

            var nodes = new List<TreeNode>() { root };
            bool forward = true;
            while (nodes.Count > 0)
            {
                var nexts = new List<TreeNode>();
                var vals = new List<int>();
                foreach (var node in nodes)
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
        public TreeNode BuildTree_My(int[] preorder, int[] inorder)
        {
            return BuildTree_PreorderAndInorder(preorder, 0, preorder.Length - 1, inorder, 0, preorder.Length - 1);
        }

        private TreeNode BuildTree_PreorderAndInorder(int[] preorder, int preLeft, int preRight, int[] inorder, int inLeft, int inRight)
        {
            if (preLeft == preRight || inLeft == inRight)
                return new TreeNode(preorder[preLeft]);

            TreeNode node = new TreeNode(preorder[preLeft]);
            int i = inLeft;
            for (; i <= inRight; i++)
                if (preorder[preLeft] == inorder[i]) break;

            int countLeft = i - 1 - inLeft + 1;
            if (i > inLeft)
            {
                node.left = BuildTree_PreorderAndInorder(preorder, preLeft + 1, preLeft + countLeft, inorder, inLeft, i - 1);
            }
            if (i < inRight)
            {
                node.right = BuildTree_PreorderAndInorder(preorder, preLeft + countLeft + 1, preRight, inorder, i + 1, inRight);
            }
            return node;
        }

        public TreeNode BuildTree_Iteration_PreorderAndInorder(int[] preorder, int[] inorder)
        {
            if (preorder.Length == 0) return null;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode root = new TreeNode(preorder[0]);
            TreeNode curr = root;
            for (int i = 1, j = 0; i < preorder.Length; i++)
            {
                if (curr.val != inorder[j])
                {
                    curr.left = new TreeNode(preorder[i]);
                    stack.Push(curr);
                    curr = curr.left;
                }
                else
                {
                    j++;
                    while (stack.Count > 0 && stack.Peek().val == inorder[j])
                    {
                        curr = stack.Pop();
                        j++;
                    }
                    curr.right = new TreeNode(preorder[i]);
                    curr = curr.right;
                }
            }
            return root;
        }

        ///106. Construct Binary Tree from Inorder and Postorder Traversal, #BTree
        public TreeNode BuildTree_InorderAndPostOrder(int[] inorder, int[] postorder)
        {
            return BuildTree_InorderAndPostOrder(inorder, postorder, 0, inorder.Length - 1, 0, postorder.Length - 1);
        }

        private TreeNode BuildTree_InorderAndPostOrder(int[] inorder, int[] postorder, int inLeft, int inRight, int postL, int postR)
        {
            if (inLeft > inRight) return null;
            else if (inLeft == inRight) return new TreeNode(inorder[inLeft]);
            else
            {
                var node = new TreeNode(postorder[postR]);
                int i = inLeft;
                for (; i <= inRight; i++)
                    if (inorder[i] == node.val) break;
                int count = i - inLeft;
                node.left = BuildTree_InorderAndPostOrder(inorder, postorder, inLeft, i - 1, postL, postL + count - 1);
                node.right = BuildTree_InorderAndPostOrder(inorder, postorder, i + 1, inRight, postL + count, postR - 1);
                return node;
            }
        }

        public TreeNode BuildTree_Iteration_InorderAndPostOrder(int[] inorder, int[] postorder)
        {
            if (inorder.Length == 0) return null;
            int j = postorder.Length - 1;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode root = new TreeNode(postorder[j--]);
            stack.Push(root);
            TreeNode node = null;
            for (int i = inorder.Length - 1; j >= 0; j--)
            {
                TreeNode curr = new TreeNode(postorder[j]);
                while (stack.Count > 0 && stack.Peek().val == inorder[i])
                {
                    node = stack.Pop();
                    i--;
                }
                if (node != null)
                {
                    node.left = curr;
                    node = null;
                }
                else
                {
                    stack.Peek().right = curr;
                }
                stack.Push(curr);
            }
            return root;
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
                ans.Insert(0, list);
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
                while (node != null)
                {
                    count++;
                    node = node.next;
                }
            }

            if (count == 1)
            {
                return new TreeNode(head.val);
            }
            else if (count == 2)
            {
                return new TreeNode(head.next.val, new TreeNode(head.val));
            }
            else if (count == 3)
            {
                return new TreeNode(head.next.val, new TreeNode(head.val), new TreeNode(head.next.next.val));
            }
            else
            {
                var mid = count / 2;
                var node = head;
                while (mid > 0)
                {
                    node = node.next;
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
            var leftDeep = IsBalanced_Depth(root.left, 1);
            var rightDeep = IsBalanced_Depth(root.right, 1);
            if (leftDeep == -1 || rightDeep == -1)
            {
                return false;
            }
            else
            {
                return leftDeep - rightDeep >= -1 && leftDeep - rightDeep <= 1;
            }
        }

        private int IsBalanced_Depth(TreeNode root, int depth)
        {
            if (root == null)
                return depth;
            int leftDeep = IsBalanced_Depth(root.left, depth + 1);
            int rightDeep = IsBalanced_Depth(root.right, depth + 1);
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
            List<TreeNode> list = new List<TreeNode>() { root };
            while (list.Count > 0)
            {
                ans++;
                List<TreeNode> next = new List<TreeNode>();
                foreach (var node in list)
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
            var ans = new List<IList<int>>();
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
        // You are given a perfect binary tree where all leaves are on the same level
        // Populate each next pointer to point to its next right node.
        // If there is no next right node, the next pointer should be set to NULL.
        public Node_Next Connect_116(Node_Next root)
        {
            if (root == null)
                return null;
            List<Node_Next> list = new List<Node_Next> { root };
            while (list.Count != 0)
            {
                List<Node_Next> subs = new List<Node_Next>();
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
        public Node_Next Connect(Node_Next root)
        {
            if (root == null)
                return null;
            List<Node_Next> list = new List<Node_Next> { root };
            while (list.Count != 0)
            {
                List<Node_Next> subs = new List<Node_Next>();
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
            List<int> list = new List<int>() { 1 };
            int lastRowIndex = 0;
            while (lastRowIndex++ < rowIndex)
            {
                var next = new List<int>
                {
                    1
                };
                for (int i = 0; i < list.Count - 1; i++)
                    next.Add(list[i] + list[i + 1]);
                next.Add(1);
                list = next;
            }
            return list;
        }

        public IList<int> GetRow_Recursion(int rowIndex)
        {
            return GetRow_Recursion(new List<int>() { 1 }, rowIndex);
        }

        private IList<int> GetRow_Recursion(List<int> list, int rowIndex)
        {
            if (rowIndex == list.Count - 1) return list;
            var next = new List<int>
            {
                1
            };
            for (int i = 0; i < list.Count - 1; i++)
                next.Add(list[i] + list[i + 1]);
            next.Add(1);
            return GetRow_Recursion(next, rowIndex);
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

            for (int i = 1; i < prices.Length; i++)
            {
                if (isHold)
                {
                    if (i < prices.Length - 1)
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
                        if (prices[i] > buy)
                            sum += prices[i] - buy;
                    }
                }
                else
                {
                    buy = prices[i];
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

            Dictionary<string, HashSet<string>> graph = new Dictionary<string, HashSet<string>>
            {
                { beginWord, new HashSet<string>() }
            };
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

            var res = new List<IList<string>>
            {
                new List<string>() { beginWord }
            };
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
                foreach (var list in next)
                {
                    visit.Add(list.Last());
                }
                if (visit.Contains(endWord)) break;
            }
            return res.Where(x => x.Last() == endWord).ToList();
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
                foreach (var curr in list)
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

        /// 128. Longest Consecutive Sequence, #HashMap, #Union Find
        /// unsorted array nums, return the length of the longest consecutive elements sequence. O(n) time.
        /// 0 <= nums.length <= 105, -10^9 <= nums[i] <= 10^9
        public int LongestConsecutive(int[] nums)
        {
            int res = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (int n in nums)
            {
                if (!dict.ContainsKey(n))
                {
                    int left = dict.ContainsKey(n - 1) ? dict[n - 1] : 0;
                    int right = dict.ContainsKey(n + 1) ? dict[n + 1] : 0;
                    int sum = left + right + 1;
                    dict.Add(n, sum);
                    res = Math.Max(res, sum);
                    // extend the length to the boundary(s) of the sequence
                    if (left > 0)
                        dict[n - left] = sum;
                    if(right>0)
                        dict[n + right] = sum;
                }
            }
            return res;
        }

        public int LongestConsecutive_UnionFind(int[] nums)
        {
            int res = 0;
            int n = nums.Length;
            var uf = new UnionFind(n);
            var dict = new Dictionary<int, int>();
            for(int i = 0; i < n; i++)
            {
                if (dict.ContainsKey(nums[i])) continue;
                dict.Add(nums[i], i);
                if (dict.ContainsKey(nums[i] - 1))
                    uf.Union(uf.Find( dict[nums[i] - 1]), i);
                if (dict.ContainsKey(nums[i] + 1))
                    uf.Union(uf.Find(dict[nums[i] + 1]), i);
            }

            var map = new Dictionary<int, int>();
            for(int i = 0; i < n; i++)
            {
                var p = uf.Find(i);
                if (!map.ContainsKey(p)) map.Add(p, 0);
                res = Math.Max(res, ++map[p]);
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

        /// 130. Surrounded Regions, #Union Find
        ///m x n matrix board containing 'X' and 'O', capture all regions that 4-directionally surrounded by 'X'.
        ///A region is captured by flipping all 'O's into 'X's in that surrounded region.
        ///Tips: If 'O's connect to 4-direction edges ,escape ; or captured
        public void Solve(char[][] board)
        {
            int m = board.Length;
            int n = board[0].Length;
            bool[][] visit = new bool[m][];
            for (int i = 0; i < m; i++)
                visit[i] = new bool[n];

            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, -1 } };
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (visit[i][j]) continue;
                    visit[i][j] = true;
                    if (board[i][j] == 'O')
                    {
                        List<int[]> list = new List<int[]>();
                        Queue<int[]> queue = new Queue<int[]>();
                        queue.Enqueue(new int[] { i, j });
                        bool capture = true;
                        while (queue.Count > 0)
                        {
                            int size = queue.Count;
                            while (size-- > 0)
                            {
                                var top = queue.Dequeue();
                                list.Add(top);
                                foreach (var d in dxy4)
                                {
                                    int r = top[0] + d[0];
                                    int c = top[1] + d[1];
                                    if (r >= 0 && r <= m - 1 && c >= 0 && c <= n - 1)
                                    {
                                        if (!visit[r][c])
                                        {
                                            visit[r][c] = true;
                                            if (board[r][c] == 'O')
                                                queue.Enqueue(new int[] { r, c });
                                        }
                                    }
                                    else
                                    {
                                        capture = false;
                                    }
                                }
                            }
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

        public void Solve_UnionFind(char[][] board)
        {
            int m = board.Length;
            int n = board[0].Length;
            var uf = new UnionFind(m * n + 1);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i][j] == 'O')
                    {
                        if (i == 0 || i == m - 1 || j == 0 || j == n - 1)
                        {
                            uf.Union(i * n + j, m * n);
                        }
                        else
                        {
                            if (board[i - 1][j] == 'O') uf.Union(i * n + j, (i - 1) * n + j);
                            if (board[i + 1][j] == 'O') uf.Union(i * n + j, (i + 1) * n + j);
                            if (board[i][j - 1] == 'O') uf.Union(i * n + j, i * n + j - 1);
                            if (board[i][j + 1] == 'O') uf.Union(i * n + j, i * n + j + 1);
                        }
                    }
                }
            }
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (!uf.IsConnected(i * n + j, m * n))
                        board[i][j] = 'X';
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
            var ans = new List<IList<string>>();
            var list = new List<string>();
            Partition_BackTracking(s, list, ans);
            return ans;
        }

        private void Partition_BackTracking(string s, IList<string> list, IList<IList<string>> ans)
        {
            for (int i = 0; i < s.Length - 1; i++)
            {
                var str = s.Substring(0, i + 1);
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
            Node_Neighbors res = null;
            Dictionary<Node_Neighbors, int> dict = new Dictionary<Node_Neighbors, int>();
            List<Node_Neighbors> list = new List<Node_Neighbors>();
            res = new Node_Neighbors(node.val);
            dict.Add(node, list.Count);
            list.Add(res);
            res.neighbors = CloneGraph_DFS(node.neighbors, dict, list);
            return res;
        }

        private IList<Node_Neighbors> CloneGraph_DFS(IList<Node_Neighbors> neighbors, IDictionary<Node_Neighbors, int> dict, IList<Node_Neighbors> list)
        {
            if (neighbors == null) return null;
            if (neighbors.Count == 0) return new List<Node_Neighbors>();
            var res = new List<Node_Neighbors>();
            foreach (var node in neighbors)
            {
                if (dict.ContainsKey(node))
                {
                    res.Add(list[dict[node]]);
                }
                else
                {
                    dict.Add(node, list.Count);
                    var clone = new Node_Neighbors(node.val);
                    list.Add(clone);
                    res.Add(clone);
                    clone.neighbors = CloneGraph_DFS(node.neighbors, dict, list);
                }
            }
            return res;
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

        ///135. Candy ,#DP
        ///There are n children standing in a line. Each child is assigned a rating value given in the integer array ratings.
        ///Each child must have at least one candy, Children with a higher rating get more candies than their neighbors.
        ///Return the minimum number of candies you need to have to distribute the candies to the children.
        public int Candy(int[] ratings)
        {
            int n = ratings.Length;
            int[] dp = new int[n];
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < n; i++)
            {
                if ((i == 0 || ratings[i] <= ratings[i - 1])
                    && (i == n - 1 || ratings[i] <= ratings[i + 1]))
                {
                    queue.Enqueue(i);
                    dp[i] = 1;
                }
            }
            while (queue.Count > 0)
            {
                int size = queue.Count;
                while (size-- > 0)
                {
                    var i = queue.Dequeue();
                    if (i > 0 && ratings[i - 1] > ratings[i])
                    {
                        dp[i - 1] = Math.Max(dp[i - 1], dp[i] + 1);
                        queue.Enqueue(i - 1);
                    }
                    if (i < n - 1 && ratings[i + 1] > ratings[i])
                    {
                        dp[i + 1] = Math.Max(dp[i + 1], dp[i] + 1);
                        queue.Enqueue(i + 1);
                    }
                }
            }
            return dp.Sum();
        }


        /// 136. Single Number, in Easy

        ///137. Single Number II, in Easy


        ///138. Copy List with Random Pointer, #Good!
        ///Random may pointer to itself
        public Node_Random CopyRandomList(Node_Random head)
        {
            Node_Random curr = head;
            Node_Random next = null;
            // First round: make copy of each node,
            // and link them together side-by-side in a single list.
            while (curr != null)
            {
                next = curr.next;

                Node_Random copy1 = new Node_Random(curr.val);
                curr.next = copy1;
                copy1.next = next;

                curr = next;
            }

            // Second round: assign random pointers for the copy nodes.
            curr = head;
            while (curr != null)
            {
                if (curr.random != null)
                {
                    //every copy node is the next of prev node in origin sequence
                    curr.next.random = curr.random.next;
                }
                curr = curr.next.next;
            }

            // Third round: restore the original list, and extract the copy list.
            curr = head;
            Node_Random pseudoHead = new Node_Random(0);
            Node_Random copyPrev = pseudoHead;
            while (curr != null)
            {
                next = curr.next.next;//real next in origin sequence

                // extract the copy
                copyPrev.next = curr.next;
                copyPrev = copyPrev.next;

                // restore the original list
                curr.next = next;

                curr = next;
            }
            return pseudoHead.next;
        }

        /// 139. Word Break, #DP, #Backtracking, #Trie
        ///return true if s can be segmented into a space-separated sequence of one or more dictionary words.
        ///Note that the same word in the dictionary may be reused multiple times in the segmentation.
        public bool WordBreak139_DP(string s, IList<string> wordDict)
        {
            bool[] dp = new bool[s.Length + 1];
            dp[0] = true;
            for (int i = 0; i < s.Length; i++)
            {
                if (dp[i])
                {
                    foreach (var word in wordDict)
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

        public bool WordBreak139_Trie(string s, IList<string> wordDict)
        {
            var root = new TrieItem();
            foreach(var word in wordDict)
            {
                var curr = root;
                foreach(var c in word)
                {
                    if (!curr.dict.ContainsKey(c)) curr.dict.Add(c, new TrieItem());
                    curr = curr.dict[c];
                }
                curr.exist = true;
            }
            var visit= new HashSet<int>();
            return WordBreak139_Trie(s, 0, visit, root);
        }

        private bool WordBreak139_Trie(string s,int index,HashSet<int> visit, TrieItem root)
        {
            if (visit.Contains(index)) return false;
            visit.Add(index);
            var curr = root;
            List<int> list = new List<int>();
            int i = index;
            for (; i < s.Length; i++)
            {
                if (!curr.dict.ContainsKey(s[i])) break;
                curr = curr.dict[s[i]];
                if (curr.exist)
                    list.Add(i);
            }
            if (i == s.Length && curr.exist) return true;
            for(int j=list.Count-1;j>=0;j--)
            {
                if (WordBreak139_Trie(s, list[j] + 1, visit, root)) return true;
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

        ///140. Word Break II, #Backtracking, #DP, #Trie
        ///Given a string s and a dictionary of strings wordDict,
        ///add spaces in s to construct a sentence where each word is a valid dictionary word.
        ///Return all such possible sentences in any order.
        public IList<string> WordBreak_DP(string s, IList<string> wordDict)
        {
            bool[] dp = new bool[s.Length + 1];
            Dictionary<int, List<List<string>>> map = new Dictionary<int, List<List<string>>>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var w in wordDict)
            {
                if (!string.IsNullOrEmpty(w))
                    dict.Add(w, 1);
            }
            dp[0] = true;
            map.Add(0, new List<List<string>>());
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
                                    foreach (var list in map[i])
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
                return map[s.Length].Select(x => string.Join(" ", x)).ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        public IList<string> WordBreak140_Trie(string s, IList<string> wordDict)
        {
            var res=new List<string>();
            var root = new TrieItem();
            foreach (var word in wordDict)
            {
                var curr = root;
                foreach (var c in word)
                {
                    if (!curr.dict.ContainsKey(c)) curr.dict.Add(c, new TrieItem());
                    curr = curr.dict[c];
                }
                curr.word = word;
            }
            WordBreak140_Trie(s, 0, "", root, res);
            return res;
        }

        private void WordBreak140_Trie(string s, int index, string currStr,TrieItem root, IList<string> res)
        {
            if (index == s.Length)
            {
                res.Add(currStr.Trim());
            }
            else
            {
                var curr = root;
                for(int i = index; i < s.Length; i++)
                {
                    if (!curr.dict.ContainsKey(s[i])) return;
                    curr=curr.dict[s[i]];
                    if (!string.IsNullOrEmpty(curr.word))
                        WordBreak140_Trie(s, i + 1, currStr +" "+ curr.word, root, res);
                }
            }
        }

        public IList<string> WordBreak140_Backtracking(string s, IList<string> wordDict)
        {
            HashSet<string> set = new HashSet<string>();
            WordBreak140_Backtracking(s, wordDict, new List<string>(), set);
            return set.ToList();
        }

        private void WordBreak140_Backtracking(string s, IList<string> wordDict, IList<string> list, HashSet<string> set)
        {
            if (s.Length == 0)
            {
                set.Add(string.Join(" ", list));
            }
            else
            {
                foreach (var w in wordDict)
                {
                    if (s.StartsWith(w))
                    {
                        var next = new List<string>(list) { w };
                        WordBreak140_Backtracking(s.Substring(w.Length), wordDict, next, set);
                    }
                }
            }
        }

        /// 141. Linked List Cycle, #Two Pointers ->O(1) space
        // Return true if there is a cycle in the linked list. Otherwise, return false.
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
            ListNode slow = head;
            ListNode fast = head;
            while (fast != null && fast.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
                if (fast == slow)
                {
                    ListNode slow2 = head;
                    while (slow2 != slow)
                    {
                        slow = slow.next;
                        slow2 = slow2.next;
                    }
                    return slow;
                }
            }
            return null;
        }

        ///143. Reorder List
        ///Reorder the list: 1->2->3->4 to 1->4->2->3, 1->2->3->4->5 to 1->5->2->4->3
        public void ReorderList(ListNode head)
        {
            Stack<ListNode> stack = new Stack<ListNode>();
            var node = head;
            while (node != null)
            {
                stack.Push(node);
                node = node.next;
            }
            int i = stack.Count;
            node = head;
            if (i <= 2)
                return;
            while (i > 0)
            {
                var next = node.next;
                var pop = stack.Pop();
                pop.next = next;
                node.next = pop;
                //last = pop;
                node = next;
                i -= 2;
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
            while (node != null)
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
                        while (prev != null && prev.next != null && prev.next.val < node.val)
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

        /// 148. Sort List， #Merge Sort, #Two Pointers
        ///Given the head of a linked list, return the list after sorting it in ascending order.
        ///O(n logn) time and O(1) memory
        public ListNode SortList(ListNode head)
        {
            //MergeSort, divide and conquer
            if (head == null || head.next == null)
                return head;
            // step 1. cut the list to two halves
            ListNode prev = null, slow = head, fast = head;
            while (fast != null && fast.next != null)
            {
                prev = slow;
                slow = slow.next;
                fast = fast.next.next;
            }
            prev.next = null;
            // step 2. sort each half
            ListNode l1 = SortList(head);
            ListNode l2 = SortList(slow);

            // step 3. merge l1 and l2
            return SortList_Merge(l1, l2);
        }

        private ListNode SortList_Merge(ListNode l1, ListNode l2)
        {
            ListNode root = new ListNode(0), prev = root;
            while (l1 != null && l2 != null)
            {
                if (l1.val < l2.val)
                {
                    prev.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    prev.next = l2;
                    l2 = l2.next;
                }
                prev = prev.next;
            }
            if (l1 != null)
                prev.next = l1;
            if (l2 != null)
                prev.next = l2;
            return root.next;
        }

        public ListNode SortList_My(ListNode head)
        {
            if (head == null) return null;
            ListNode ans = null;
            var node = head;
            List<ListNode> list = new List<ListNode>();
            while (node != null)
            {
                list.Add(node);
                node = node.next;
            }
            list.Sort((x, y) =>
            {
                return x.val - y.val;
            });
            ans = list[0];
            for (int i = 0; i < list.Count; i++)
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
                    dictX.Add(points[i][0], new List<int>() { i });
                    continue;
                }

                List<string> findKeys = new List<string>();
                foreach (var d in dict.Keys)
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
                    var b = 1.0 * (points[j][1] * points[i][0] - points[j][0] * points[i][1]) / (points[i][0] - points[j][0]);

                    string key = $"{a},{b}";

                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, new List<int> { j, i });
                    }
                    else
                    {
                        if (!dict[key].Contains(i))
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
            return Math.Max(count1, count2);
        }
    }
}