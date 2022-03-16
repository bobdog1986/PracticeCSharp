using System;
using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        /// BTree PreorderTraversal Iteratively, Node->Left->Right, #BTree
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

        /// BTree InorderTraversal Iteratively, Left->Node->Right, #BTree
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

        /// BTree PostorderTraversal Iteratively, Left->Right->Node, #BTree
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

        /// BTree PreorderTraversal Recursion, #BTree
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

        /// BTree InorderTraversal Recursion, #BTree
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

        /// BTree PostorderTraversal Recursion, #BTree
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
        ///See 101. Symmetric Tree

        ///See 102. Binary Tree Level Order Traversal

        ///See 104. Maximum Depth of Binary Tree

        ///See 105. Construct Binary Tree from Preorder and Inorder Traversal, #BTree

        ///
    }
}