using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design.Codec_449
{
    ///449. Serialize and Deserialize BST
    public class Codec
    {
        private const string SEP = ",";
        private const string EMPTY = "-";
        // Encodes a tree to a single string.
        public string serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            if (root == null) return EMPTY;
            //traverse it recursively if you want to, I am doing it iteratively here
            Stack<TreeNode> st = new Stack<TreeNode>();
            st.Push(root);
            while (st.Count>0)
            {
                root = st.Pop();
                sb.Append(root.val).Append(SEP);
                if (root.right != null) st.Push(root.right);
                if (root.left != null) st.Push(root.left);
            }
            return sb.ToString();
        }

        // Decodes your encoded data to tree.
        // pre-order traversal
        public TreeNode deserialize(string data)
        {
            if (data==EMPTY) return null;
            string[] strs = data.Split(SEP);
            Queue<int> q = new Queue<int>();
            foreach (var s in strs)
            {
                if(s.Length>0)
                    q.Enqueue(int.Parse(s));
            }
            return getNode(q);
        }

        // some notes:
        //   5
        //  3 6
        // 2   7
        private TreeNode getNode(Queue<int> q)
        {
            //q: 5,3,2,6,7
            if (q.Count==0) return null;
            TreeNode root = new TreeNode(q.Dequeue());//root (5)
            Queue<int> samllerQueue = new Queue<int>();
            while (q.Count>0 && q.Peek() < root.val)
            {
                samllerQueue.Enqueue(q.Dequeue());
            }
            //smallerQueue : 3,2   storing elements smaller than 5 (root)
            root.left = getNode(samllerQueue);
            //q: 6,7   storing elements bigger than 5 (root)
            root.right = getNode(q);
            return root;
        }
    }
}
