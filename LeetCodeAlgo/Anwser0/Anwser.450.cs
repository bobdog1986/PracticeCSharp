using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///450. Delete Node in a BST
        ///The number of nodes in the tree is in the range [0, 10^4].
        public TreeNode DeleteNode(TreeNode root, int key)
        {
            if(root == null)
                return null;
            if (root.val == key)
            {
                return DeleteNode(root);
            }
            else if(key>root.val)
            {
                root.right = DeleteNode(root.right, key);
            }
            else
            {
                root.left = DeleteNode(root.left, key);
            }
            return root;
        }

        public TreeNode DeleteNode(TreeNode root)
        {
            if (root.left == null)
                return root.right;
            if (root.right == null)
                return root.left;

            //inset the right subtree to left subtree
            var ans=root.left;
            var node = root.left;
            while (node.right != null)
            {
                node=node.right;
            }
            node.right = root.right;
            return ans;
        }
    }
}