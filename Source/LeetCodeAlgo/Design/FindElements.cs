using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1261. Find Elements in a Contaminated Binary Tree
    ///root.val == 0 ,treeNode.left.val == 2 * x + 1 , treeNode.right.val == 2 * x + 2
    public class FindElements
    {

        private readonly HashSet<int> set;

        public FindElements(TreeNode root)
        {
            set = new HashSet<int>();
            Build(root,0);
        }

        private void Build(TreeNode root,int val)
        {
            if (root == null) return;
            root.val = val;
            set.Add(root.val);
            Build(root.left, val * 2 + 1);
            Build(root.right, val * 2 + 2);
        }
        public bool Find(int target)
        {
            return set.Contains(target);
        }
    }

}
