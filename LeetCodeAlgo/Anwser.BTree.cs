﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        public IList<int> PreorderTraversal_Iteratively(TreeNode root)
        {
            if (root == null)
                return new List<int>();

            var list = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();

            stack.Push(root);

            while (stack.Count > 0)
            {
                var node=stack.Pop();
                if(node == null) { break; }

                list.Add(node.val);
                if(node.right != null)
                {
                    stack.Push(node.right);
                }

                if (node.left != null)
                {
                    stack.Push(node.left);
                }
            }


            return list;
        }

        public IList<int> InorderTraversal_Iteratively(TreeNode root)
        {
            if (root == null)
                return new List<int>();

            var list = new List<int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();

            stack.Push(root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node == null) { break; }

                if (node.left != null)
                {
                    var left=node.left;

                    node.left = null;
                    stack.Push(node);

                    stack.Push(left);
                    continue;
                }

                list.Add(node.val);

                if (node.right != null)
                {
                    stack.Push(node.right);
                }

            }


            return list;
        }

        public IList<int> PostorderTraversal_Iteratively(TreeNode root)
        {
            if (root == null)
                return new List<int>();

            var list = new List<int>();
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


                if(left != null)
                {
                    if(right != null)
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
                    if(right != null)
                    {
                        stack.Push(node);
                        stack.Push(right);
                        //stack.Push(left);
                        continue;
                    }
                }

                list.Add(node.val);
            }


            return list;
        }


        public IList<int> PreorderTraversal_Recursion(TreeNode root)
        {
            if (root == null)
                return new List<int>();

            var list=new List<int>();
            list.Add(root.val);
            PreorderTraversal_Recursion_Add(root.left, list);
            PreorderTraversal_Recursion_Add(root.right, list);

            return list;
        }


        public void PreorderTraversal_Recursion_Add(TreeNode node,IList<int> list)
        {
            if (node == null)
                return;

            list.Add(node.val);
            PreorderTraversal_Recursion_Add(node.left, list);
            PreorderTraversal_Recursion_Add(node.right, list);
        }


        public IList<int> InorderTraversal_Recursion(TreeNode root)
        {
            if (root == null)
                return new List<int>();

            var list = new List<int>();
            InorderTraversal_Recursion_Add(root.left, list);
            list.Add(root.val);
            InorderTraversal_Recursion_Add(root.right, list);

            return list;
        }


        public void InorderTraversal_Recursion_Add(TreeNode node, IList<int> list)
        {
            if (node == null)
                return;

            InorderTraversal_Recursion_Add(node.left, list);
            list.Add(node.val);
            InorderTraversal_Recursion_Add(node.right, list);
        }

        public IList<int> PostorderTraversal_Recursion(TreeNode root)
        {
            if (root == null)
                return new List<int>();

            var list = new List<int>();
            PostorderTraversal_Recursion_Add(root.left, list);
            PostorderTraversal_Recursion_Add(root.right, list);
            list.Add(root.val);

            return list;
        }

        public void PostorderTraversal_Recursion_Add(TreeNode node, IList<int> list)
        {
            if (node == null)
                return;

            PostorderTraversal_Recursion_Add(node.left, list);
            PostorderTraversal_Recursion_Add(node.right, list);
            list.Add(node.val);

        }


        public IList<IList<int>> LevelOrder_1(TreeNode root)
        {
            var result =new List<IList<int>>();
            if (root == null)
                return result;
            var nodes=new Queue<TreeNode>();
            nodes.Enqueue(root);

            while(nodes.Count>0)
            {
                var list=new List<int>();
                var subs = new Queue<TreeNode>();

                while (nodes.Count > 0)
                {
                    var node = nodes.Dequeue();
                    list.Add((int)node.val);

                    if(node.left != null)
                    {
                        subs.Enqueue(node.left);
                    }

                    if(node.right != null)
                    {
                        subs.Enqueue(node.right);
                    }
                }

                result.Add(list);

                nodes = subs;
            }

            return result;
        }


        private int maxDepth = 0;

        public int MaxDepth_1(TreeNode root)
        {
            if(root == null)
                return maxDepth;

            maxDepth++;

            if (root.left != null)
            {
                LoopTree(root.left, 1);
            }

            if(root.right != null)
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

            if(node.left != null)
            {
                LoopTree(node.left, depth);

            }

            if(node.right != null)
            {
                LoopTree(node.right, depth);
            }
        }

        private bool isSymm = true;
        public bool IsSymmetric_1(TreeNode root)
        {
            if (root == null)
                return isSymm;

            //IsSymmetric(root.left,root.right);

            List<TreeNode> leftNodes = new List<TreeNode>();
            List<TreeNode> rightNodes = new List<TreeNode>();
            if(root.left!=null) leftNodes.Add(root.left);
            if(root.right!=null) rightNodes.Add(root.right);

            while (true)
            {
                if (!isSymm)
                    break;

                if (leftNodes.Count == 0 && rightNodes.Count == 0)
                    break;

                if(leftNodes.Count != rightNodes.Count)
                {
                    isSymm = false;
                    break;
                }

                List<TreeNode> subLeftNodes = new List<TreeNode>();
                List<TreeNode> subRigthNodes = new List<TreeNode>();
                for (int i=0; i<leftNodes.Count; i++)
                {
                    if(leftNodes[i].val != rightNodes[i].val)
                    {
                        isSymm = false;
                        break;
                    }

                    if(leftNodes[i].left == null && rightNodes[i].right != null)
                    {
                        isSymm=false;
                        break;
                    }

                    if (leftNodes[i].right == null && rightNodes[i].left != null)
                    {
                        isSymm = false;
                        break;
                    }

                    if (leftNodes[i].left != null && rightNodes[i].right == null)
                    {
                        isSymm = false;
                        break;
                    }

                    if (leftNodes[i].right != null && rightNodes[i].left == null)
                    {
                        isSymm = false;
                        break;
                    }

                    if (leftNodes[i].left != null) subLeftNodes.Add(leftNodes[i].left);
                    if (leftNodes[i].right != null) subLeftNodes.Add(leftNodes[i].right);
                    if (rightNodes[i].right != null) subRigthNodes.Add(rightNodes[i].right);
                    if (rightNodes[i].left != null) subRigthNodes.Add(rightNodes[i].left);

                }

                leftNodes = subLeftNodes;
                rightNodes = subRigthNodes;
            }


            return isSymm;

        }

        public void IsSymmetric(TreeNode left, TreeNode right)
        {
            if (!isSymm)
                return;

            if (left == null && right == null)
                return;

            if(left == null||right == null)
            {
                isSymm = false;
                return;
            }

            if(left.val != right.val)
            {
                isSymm = false;
                return;
            }

            IsSymmetric(left.left, right.right);
            IsSymmetric(left.right, right.left);

        }

        private bool hasPathSum = false;
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
                return hasPathSum;

            int sum = root.val;

            if (root.left == null && root.right == null)
            {
                if (sum == targetSum)
                {
                    hasPathSum = true;
                }
                return hasPathSum;
            }

            LoopPathSum(root.left, sum, targetSum);
            LoopPathSum(root.right, sum, targetSum);

            return hasPathSum;
        }

        public void LoopPathSum(TreeNode node,int sum, int targetSum)
        {
            if (hasPathSum)
                return;

            if (node == null)
                return;

            sum+=node.val;

            if(node.left == null && node.right == null)
            {
                if (sum == targetSum)
                {
                    hasPathSum = true;
                }
                return;
            }

            LoopPathSum(node.left, sum, targetSum);
            LoopPathSum(node.right, sum, targetSum);

        }

    }
}
