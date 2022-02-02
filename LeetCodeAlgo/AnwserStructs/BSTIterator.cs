using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.AnwserStructs
{
    /// 173. Binary Search Tree Iterator
    public class BSTIterator
    {

        private int index = -1;
        private readonly IList<int> list;

        public BSTIterator(TreeNode root)
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
