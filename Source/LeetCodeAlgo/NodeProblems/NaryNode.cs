using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeProblems
{
    public class NaryNode
    {
        public class Node
        {
            public int val;
            public IList<Node> children;

            public Node() { }

            public Node(int _val)
            {
                val = _val;
            }

            public Node(int _val, IList<Node> _children)
            {
                val = _val;
                children = _children;
            }
        }

        ///429. N-ary Tree Level Order Traversal
        ///Given an n-ary tree, return the level order traversal of its nodes' values.
        public IList<IList<int>> LevelOrder(Node root)
        {
            var ans = new List<IList<int>>();
            var nodes = new List<Node>() { root };
            while (nodes.Count > 0)
            {
                var next = new List<Node>();
                var list = new List<int>();
                foreach (var n in nodes)
                {
                    if (n == null) continue;
                    list.Add(n.val);
                    next.AddRange(n.children);
                }
                if (list.Count>0) ans.Add(list);
                nodes = next;
            }
            return ans;
        }

        ///559. Maximum Depth of N-ary Tree
        ///Given a n-ary tree, find its maximum depth.
        public int MaxDepth(Node root)
        {
            if (root == null)
                return 0;
            return 1 + MaxDepth(root.children);
        }

        private int MaxDepth(IList<Node> nodes)
        {
            if (nodes == null||nodes.Count==0)
                return 0;
            int max = 0;
            foreach (var node in nodes)
            {
                max = Math.Max(max, 1 + MaxDepth(node.children));
            }
            return max;
        }


        /// 589. N-ary Tree Preorder Traversal
        //Given the root of an n-ary tree, return the preorder traversal of its nodes' values.
        public IList<int> Preorder(Node root)
        {
            var ans = new List<int>();
            PreorderNaryTree(root, ans);
            return ans;
        }

        private void PreorderNaryTree(Node node, IList<int> ans)
        {
            if (node == null) return;
            ans.Add(node.val);
            foreach (var child in node.children)
            {
                if (child != null) PreorderNaryTree(child, ans);
            }
        }

        public IList<int> Preorder_Iteration(Node root)
        {
            List<int> list = new List<int>();
            if (root == null) return list;
            var stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count>0)
            {
                var node = stack.Pop();
                list.Add(node.val);
                if (node.children!=null && node.children.Count > 0)
                {
                    for (int i = node.children.Count - 1; i >= 0; i--)
                        stack.Push(node.children[i]);
                }
            }
            return list;
        }

        ///590. N-ary Tree Postorder Traversal
        //Given the root of an n-ary tree, return the postorder traversal of its nodes' values.
        public IList<int> Postorder(Node root)
        {
            var ans = new List<int>();
            PostorderNaryTree(root, ans);
            return ans;
        }

        private void PostorderNaryTree(Node node, IList<int> ans)
        {
            if (node == null) return;
            foreach (var child in node.children)
            {
                if (child != null) PostorderNaryTree(child, ans);
            }
            ans.Add(node.val);
        }

        public IList<int> Postorder_Iteration(Node root)
        {
            List<int> list = new List<int>();
            if (root == null)
                return list;

            var stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count>0)
            {
                var curr = stack.Pop();
                list.Insert(0, curr.val);
                foreach (var child in curr.children)
                {
                    stack.Push(child);
                }
            }
            return list;
        }

    }
}
