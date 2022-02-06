using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        /// find target in array from [left,right], return index or -1
        public int binarySearch(int[] nums, int target, int left=-1, int right=-1)
        {
            if (left == -1)
                left = 0;
            if (right == -1)
                right = nums.Length - 1;

            if (left < 0 || left >= nums.Length || right < 0 || right >= nums.Length)
                throw new ArgumentOutOfRangeException("Index Out of Array");

            if (left == right && nums[left] == target)
                return left;

            int low = left;
            int high = right;
            int i = low + (high - low) / 2;

            while (i >= low && i <= high && (high - low) >= 1)
            {
                if (target < nums[low] || target > nums[high])
                    return -1;

                if (target == nums[low])
                    return low;
                if (target == nums[high])
                    return high;

                if (nums[i] == target)
                {
                    return i;
                }
                else if (nums[i] > target)
                {
                    high = i - 1;
                    i = low + (high - low) / 2;
                }
                else
                {
                    low = i + 1;

                    i = low + (high - low) / 2;
                }
            }

            return -1;
        }

        public int getFactorial(int n)
        {
            if (n == 0 || n == 1) return 1;
            int r = 1;
            while (n >= 1)
            {
                r *= n;
                n--;
            }
            return r;
        }
        public int getFactorial(int n, int count)
        {
            int r = 1;

            while (count > 0 && n > 0)
            {
                r *= n;
                n--;
                count--;
            }

            return r;
        }

        public int getCombines(int n, int count)
        {
            return getFactorial(n, count) / getFactorial(count);
        }



        public int[] createArray(int len, int seed = int.MinValue)
        {
            int[] arr = new int[len];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = seed;
            return arr;
        }

        /// 找出最大公约数
        public int Gcb(int m, int n)
        {
            if (m < 1 || n < 1)
                return m > 0 ? m : n;
            if (m == 1 || n == 1)
                return 1;
            if (m % n == 0)
                return n;

            int remainder = m % n;
            m = n;
            n = remainder;
            return Gcb(m, n);
        }

        public long GcbLong(long m, long n)
        {
            if (m < 1 || n < 1)
                return m > 0 ? m : n;
            if (m == 1 || n == 1)
                return 1;
            if (m % n == 0)
                return n;

            long remainder = m % n;
            m = n;
            n = remainder;
            return GcbLong(m, n);
        }


        ///ListNode, build and print
        public void printListNode(ListNode listNode)
        {
            List<int> list = new List<int>();
            while (listNode != null)
            {
                list.Add(listNode.val);
                listNode = listNode.next;
            }

            Console.WriteLine($"ListNode is [{string.Join(",", list)}]");
        }

        public ListNode buildListNode(int[] arr)
        {
            ListNode head = new ListNode(arr[0]);
            var current = head;
            for (int i = 1; i < arr.Length; i++)
            {
                current.next = new ListNode(arr[i]);
                current = current.next;
            }

            return head;
        }

        // Encodes a tree to a single string. eg. 1,2,3,,,4,5, Null will be ""
        public string serializeTree(TreeNode root, int invalid=1000001)
        {
            if (root == null)
                return string.Empty;

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
        public void printTree(TreeNode root)
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
            for(int i = 0; i < allLevels.Count; i++)
            {
                var strs = allLevels[i].Select(x => x == null ? "null" : x.val.ToString()).
                    Select(s => String.Format("{0,"+$"{-align2}"+"}", String.Format("{0," + ((align2 + s.Length) / 2).ToString() + "}", s)));

                var str1 = string.Join("", strs);

                var str2= String.Format("{0,"+$"{-align1-(deep-1)}"+"}",
                String.Format("{0," + ((align1+(deep-1) + str1.Length) / 2).ToString() + "}", str1));

                Console.WriteLine(str2);
                //deep *= 2;
                align2 /= 2;
            }


        }

        // Decodes your encoded data to tree.
        public TreeNode deserializeTree(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            data = data.Replace("null", "").Replace("-", "").Replace(" ", "");
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