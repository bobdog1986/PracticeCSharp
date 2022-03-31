using System;
using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        /// BTree InorderTraversal Iteratively, Left->Node->Right, #BTree


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

    }
}