using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    /// 173. Binary Search Tree Iterator
    public class BSTIterator
    {
        private Stack<TreeNode> stack = new Stack<TreeNode>();

        public BSTIterator(TreeNode root)
        {
            pushAll(root);
        }

        /** @return whether we have a next smallest number */
        public bool HasNext()
        {
            return stack.Count>0;
        }

        /** @return the next smallest number */
        public int Next()
        {
            TreeNode top = stack.Pop();
            pushAll(top.right);
            return top.val;
        }

        private void pushAll(TreeNode node)
        {
            var curr = node;
            while (curr != null)
            {
                stack.Push(curr);
                curr = curr.left;
            }
        }
    }

    public class BSTIterator_My
    {

        private int index = -1;
        private readonly IList<int> list;

        public BSTIterator_My(TreeNode root)
        {
            list = InorderTraversal_Iteration(root);
        }

        public int Next()
        {
            index++;
            return list[index];
        }

        public bool HasNext()
        {
            return index < list.Count - 1;
        }

        private IList<int> InorderTraversal_Iteration(TreeNode root)
        {
            List<int> values = new List<int>();
            if (root == null) return values;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode node = root;
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
                    values.Add(item.val);
                    node = item.right;
                }
            }
            return values;
        }

    }
}
