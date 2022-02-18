using System;
using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        /// BTree PreorderTraversal Iteratively, Node->Left->Right
        public IList<int> PreorderTraversal_Iteratively(TreeNode root)
        {
            var ans = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node == null) { break; }
                ans.Add(node.val);
                if (node.right != null)
                    stack.Push(node.right);
                if (node.left != null)
                    stack.Push(node.left);
            }
            return ans;
        }

        /// BTree InorderTraversal Iteratively, Left->Node->Right
        public IList<int> InorderTraversal_Iteratively(TreeNode root)
        {
            var ans = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node == null) { break; }
                if (node.left != null)
                {
                    var left = node.left;
                    node.left = null;
                    stack.Push(node);
                    stack.Push(left);
                    continue;
                }
                ans.Add(node.val);
                if (node.right != null)
                    stack.Push(node.right);
            }
            return ans;
        }

        /// BTree PostorderTraversal Iteratively, Left->Right->Node
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

        /// BTree PreorderTraversal Recursion
        public IList<int> PreorderTraversal_Recursion(TreeNode root)
        {
            var ans = new List<int>() ;
            PreorderTraversal_Recursion_Add(root, ans);
            return ans;
        }

        public void PreorderTraversal_Recursion_Add(TreeNode node, IList<int> list)
        {
            if (node == null) return;
            list.Add(node.val);
            PreorderTraversal_Recursion_Add(node.left, list);
            PreorderTraversal_Recursion_Add(node.right, list);
        }

        /// BTree InorderTraversal Recursion
        public IList<int> InorderTraversal_Recursion(TreeNode root)
        {
            var ans = new List<int>();
            InorderTraversal_Recursion_Add(root, ans);
            return ans;
        }

        public void InorderTraversal_Recursion_Add(TreeNode node, IList<int> list)
        {
            if (node == null) return;
            InorderTraversal_Recursion_Add(node.left, list);
            list.Add(node.val);
            InorderTraversal_Recursion_Add(node.right, list);
        }

        /// BTree PostorderTraversal Recursion
        public IList<int> PostorderTraversal_Recursion(TreeNode root)
        {
            var ans = new List<int>();
            PostorderTraversal_Recursion_Add(root, ans);
            return ans;
        }

        public void PostorderTraversal_Recursion_Add(TreeNode node, IList<int> list)
        {
            if (node == null) return;
            PostorderTraversal_Recursion_Add(node.left, list);
            PostorderTraversal_Recursion_Add(node.right, list);
            list.Add(node.val);
        }

        /// BTree LevelOrder Iteratively
        public IList<IList<int>> LevelOrder_1(TreeNode root)
        {
            var ans = new List<IList<int>>();
            if (root == null)
                return ans;
            var nodes = new Queue<TreeNode>();
            nodes.Enqueue(root);

            while (nodes.Count > 0)
            {
                var list = new List<int>();
                var subs = new Queue<TreeNode>();

                while (nodes.Count > 0)
                {
                    var node = nodes.Dequeue();
                    list.Add(node.val);

                    if (node.left != null)
                    {
                        subs.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        subs.Enqueue(node.right);
                    }
                }

                ans.Add(list);

                nodes = subs;
            }

            return ans;
        }

        private int maxDepth = 0;

        public int MaxDepth_1(TreeNode root)
        {
            if (root == null)
                return maxDepth;

            maxDepth++;

            if (root.left != null)
            {
                LoopTree(root.left, 1);
            }

            if (root.right != null)
            {
                LoopTree(root.right, 1);
            }

            return maxDepth;
        }

        public void LoopTree(TreeNode node, int depth)
        {
            if (node == null)
                return;

            depth++;

            maxDepth = Math.Max(maxDepth, depth);

            if (node.left != null)
            {
                LoopTree(node.left, depth);
            }

            if (node.right != null)
            {
                LoopTree(node.right, depth);
            }
        }

        ///101. Symmetric Tree
        public bool IsSymmetric_101(TreeNode root)
        {
            return true;
        }

        /// 112 HasPathSum, see other file
        /// Given the root of a binary tree and an integer targetSum,
        /// return true if the tree has a root-to-leaf path such that adding up all the values along the path equals targetSum.
        public void HasPathSum_112()
        { }

        ///
    }
}