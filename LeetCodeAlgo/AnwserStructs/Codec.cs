using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.AnwserStructs
{
    ///297. Serialize and Deserialize Binary Tree
    public class Codec
    {
        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            if (root == null)
                return string.Empty;

            const int invalid = 1001;
            List<int> ans = new List<int>();
            List<TreeNode> nodes = new List<TreeNode>() { root };
            while (nodes.Count > 0)
            {
                int count = 0;
                List<TreeNode> nexts = new List<TreeNode>();
                foreach (TreeNode node in nodes)
                {
                    if (node != null)
                    {
                        ans.Add(node.val);
                        if (node.left != null)
                            count++;
                        if (node.right != null)
                            count++;
                        nexts.Add(node.left);
                        nexts.Add(node.right);

                    }
                    else
                    {
                        ans.Add(invalid);
                        //too many nodes in list, will out of memory
                        //nexts.Add(null);
                        //nexts.Add(null);
                    }
                }
                nodes = nexts;
                if (count == 0)
                    break;
            }

            var str = string.Join(",", ans);
            return str.Replace("1001", "");
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            var arr = data.Split(',').Select(x => x == string.Empty ? 1001 : int.Parse(x)).ToList();
            int i = 0;
            var root = new TreeNode(arr[i]);
            List<TreeNode> list = new List<TreeNode>() { root };
            i++;
            while (i < arr.Count)
            {
                List<TreeNode> next = new List<TreeNode>();
                foreach (var node in list)
                {
                    if (node == null)
                    {
                        i += 2;
                        next.Add(null);
                        next.Add(null);
                    }
                    else
                    {
                        if (arr[i] > 1000)
                        {
                            node.left = null;
                            //too many nodes in list, will out of memory
                            //next.Add(null);
                            i++;
                        }
                        else
                        {
                            node.left = new TreeNode(arr[i]);
                            next.Add(node.left);
                            i++;
                        }
                        if (arr[i] > 1000)
                        {
                            node.right = null;
                            //too many nodes in list, will out of memory
                            //next.Add(null);
                            i++;
                        }
                        else
                        {
                            node.right = new TreeNode(arr[i]);
                            next.Add(node.right);
                            i++;
                        }
                    }
                }
                list = next;
            }
            return root;
        }
    }
}
