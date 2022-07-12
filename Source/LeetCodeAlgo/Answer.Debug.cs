using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///Matrix build
        public int[][] buildMatrix(string str)
        {
            str = str.Replace(" ", "");
            if (str[0] == '[') str = str.Substring(1);
            if (str[str.Length - 1] == ']') str = str.Substring(0, str.Length - 1);
            var arr = str.Split("],[").Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
            var res = new List<int[]>();
            foreach (var a in arr)
            {
                res.Add(buildArray(a));
            }
            return res.ToArray();
        }

        public int[] buildArray(string str)
        {
            str = str.Replace(" ", "");
            if (str[0] == '[') str = str.Substring(1);
            if (str[str.Length - 1] == ']') str = str.Substring(0, str.Length - 1);
            var arr = str.Split(",").Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
            var res = new List<int>();
            foreach (var a in arr)
            {
                res.Add(int.Parse(a));
            }
            return res.ToArray();
        }

        public long[][] buildLongMatrix(string str)
        {
            str = str.Replace(" ", "");
            if (str[0] == '[') str = str.Substring(1);
            if (str[str.Length - 1] == ']') str = str.Substring(0, str.Length - 1);
            var arr = str.Split("],[").Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
            var res = new List<long[]>();
            foreach (var a in arr)
            {
                res.Add(buildLongArray(a));
            }
            return res.ToArray();
        }

        public long[] buildLongArray(string str)
        {
            str = str.Replace(" ", "");
            if (str[0] == '[') str = str.Substring(1);
            if (str[str.Length - 1] == ']') str = str.Substring(0, str.Length - 1);
            var arr = str.Split(",").Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
            var res = new List<long>();
            foreach (var a in arr)
            {
                res.Add(long.Parse(a));
            }
            return res.ToArray();
        }

        public IList<string>[] buildStringMatrix(string str)
        {
            //str = str.Replace(" ", "");
            str = str.Trim();
            if (str[0] == '[') str = str.Substring(1);
            if (str[str.Length - 1] == ']') str = str.Substring(0, str.Length - 1);
            var arr = str.Split("],[")
                //.Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x)).ToList();
            var res = new List<IList<string>>();
            foreach (var a in arr)
            {
                res.Add(buildStringArray(a));
            }
            return res.ToArray();
        }

        public string[] buildStringArray(string str)
        {
            //str = str.Replace(" ", "");
            str = str.TrimStart();
            str = str.TrimEnd();
            var n = str.Length;
            //str = str.Replace("\"\"", "!@#$%");//keep string.Empty
            str = str.Replace("\"", "");
            var n2 = str.Length;
            if (str[0] == '[') str = str.Substring(1);
            if (str[str.Length - 1] == ']') str = str.Substring(0, str.Length - 1);
            var arr = str.Split(",").Where(x => !string.IsNullOrEmpty(x))
                            //.Select(x => x.Trim())
                            .Select(x => x == "!@#$%" ? "" : x).ToList();
            return arr.ToArray();
        }

        //leetcode using "" wrap char, just move them
        public char[][] buildCharMatrix(string str)
        {
            str = str.Replace("\"", "");//remove all double quote "
            str = str.Replace(" ", "");
            if (str[0] == '[') str = str.Substring(1);
            if (str[str.Length - 1] == ']') str = str.Substring(0, str.Length - 1);
            var arr = str.Split("],[").Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
            var res = new List<char[]>();
            foreach (var a in arr)
            {
                res.Add(buildCharArray(a));
            }
            return res.ToArray();
        }

        public char[] buildCharArray(string str)
        {
            str = str.Replace(" ", "");
            if (str[0] == '[') str = str.Substring(1);
            if (str[str.Length - 1] == ']') str = str.Substring(0, str.Length - 1);
            var arr = str.Split(",").Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();
            var res = new List<char>();
            foreach (var a in arr)
            {
                res.Add(a[0]);
            }
            return res.ToArray();
        }

        ///ListNode, build and print
        public static void printListNode(ListNode listNode, int maxLen = 30)
        {
            if (listNode == null)
            {
                Console.WriteLine("ListNode is []");
                return;
            }
            List<int> list = new List<int>();
            while (listNode != null && list.Count <= maxLen)
            {
                list.Add(listNode.val);
                listNode = listNode.next;
            }
            var str = $"ListNode is [{string.Join(",", list)}";
            str += listNode == null ? "]" : "......]";
            Console.WriteLine(str);
        }

        public static void printRandomNode(Node_Random node, int maxLen = 20)
        {
            if (node == null)
            {
                Console.WriteLine("node is []");
                return;
            }
            List<int> list = new List<int>();
            List<Node_Random> rList = new List<Node_Random>();
            while (node != null && list.Count <= maxLen)
            {
                list.Add(node.val);
                rList.Add(node.random);
                node = node.next;
            }

            Console.WriteLine($"Node is [{string.Join(",", list)}]");

            StringBuilder sb = new StringBuilder();
            sb.Append("Rand is [");
            foreach (var n in rList)
            {
                if (n == null)
                {
                    sb.Append("-,");
                }
                else
                {
                    sb.Append(n.val + ",");
                }
            }
            sb.Append("]");
            Console.WriteLine(sb.ToString());

        }
        public ListNode buildListNode(string arrStr)
        {
            if (string.IsNullOrEmpty(arrStr))
            {
                Console.WriteLine("Cannot build listnode, due to arrStr is empty!");
                return null;
            }
            else
            {
                return buildListNode(buildArray(arrStr));
            }
        }

        public ListNode buildListNode(int[] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                Console.WriteLine("buildListNode() input length =0");
                return null;
            }

            ListNode head = new ListNode(arr[0]);
            var current = head;
            for (int i = 1; i < arr.Length; i++)
            {
                current.next = new ListNode(arr[i]);
                current = current.next;
            }

            Answer.printListNode(head);
            return head;
        }

        public static void printTree(TreeNode root)
        {
            if (root == null)
            {
                Console.WriteLine("!!!Tree is null");
                return;
            }
            List<List<TreeNode>> allLevels = new List<List<TreeNode>>();
            List<TreeNode> nodes = new List<TreeNode>() { root };
            allLevels.Add(nodes);
            while (nodes.Count > 0)
            {
                int count = 0;
                List<TreeNode> nexts = new List<TreeNode>();
                foreach (TreeNode node in nodes)
                {
                    if (node != null)
                    {
                        if (node.left != null)
                            count++;
                        if (node.right != null)
                            count++;
                        nexts.Add(node.left);
                        nexts.Add(node.right);

                    }
                    else
                    {
                        nexts.Add(null);
                        nexts.Add(null);
                    }
                }
                nodes = nexts;

                if (count == 0)
                    break;
                allLevels.Add(nodes);
            }

            int align1 = 64;
            int align2 = 64;
            int deep = 1;
            for (int i = 0; i < allLevels.Count; i++)
            {
                var strs = allLevels[i].Select(x => x == null ? "null" : x.val.ToString()).
                    Select(s => String.Format("{0," + $"{-align2}" + "}", String.Format("{0," + ((align2 + s.Length) / 2).ToString() + "}", s)));

                var str1 = string.Join("", strs);

                var str2 = String.Format("{0," + $"{-align1 - (deep - 1)}" + "}",
                String.Format("{0," + ((align1 + (deep - 1) + str1.Length) / 2).ToString() + "}", str1));

                Console.WriteLine(str2);
                //deep *= 2;
                align2 /= 2;
            }


        }

        // Decodes your encoded data to tree. Leetcode origin data is level-traversal
        public TreeNode deserializeTree(string data, int invalid = int.MinValue)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            data = data.Replace("null", "").Replace("-", "").Replace(" ", "").Replace("[", "").Replace("]", "");
            var arr = data.Split(',').Select(x => x == string.Empty ? int.MinValue : int.Parse(x)).ToList();
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
                        if (i >= arr.Count) break;
                        if (arr[i] == invalid)
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
                        if (i >= arr.Count) break;
                        if (arr[i] == invalid)
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
            Answer.printTree(root);
            return root;
        }
    }
}
