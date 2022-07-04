using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///297. Serialize and Deserialize Binary Tree
    public class Codec_297
    {

        public string serialize(TreeNode root)
        {
            var sb = new StringBuilder();
            return serializeInternal(sb, root).ToString();
        }

        // Generate preorder string
        private StringBuilder serializeInternal(StringBuilder sb, TreeNode root)
        {
            if (root == null) return sb.Append("#").Append(",");
            sb.Append(root.val).Append(",");
            serializeInternal(sb, root.left);
            serializeInternal(sb, root.right);
            return sb;
        }

        public TreeNode deserialize(string data)
        {
            if (data.Length > 0)
                data = data.Substring(0, data.Length - 1);
            var arr = data.Split(',');
            var queue = new Queue<string>();
            foreach(var i in arr)
            {
                queue.Enqueue(i);
            }
            return deserialInternal(queue);
        }

        // Use queue to simplify position move
        private TreeNode deserialInternal(Queue<string> q)
        {
            if (q.Count == 0) return null;
            var val = q.Dequeue();
            if (val=="#"|| val == "-" || val=="null"||val =="") return null;
            TreeNode root = new TreeNode(int.Parse(val));
            root.left = deserialInternal(q);
            root.right = deserialInternal(q);
            return root;
        }
    }
}
