using System;
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


        public IList<IList<int>> LevelOrder(TreeNode root)
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

    }
}
