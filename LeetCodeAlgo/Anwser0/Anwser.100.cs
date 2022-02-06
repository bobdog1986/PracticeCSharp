using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///100. Same Tree
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
                return true;
            if (p == null || q == null)
                return false;

            List<TreeNode> list1 = new List<TreeNode>() { p};
            List<TreeNode> list2 = new List<TreeNode>() { q};

            while(list1.Count>0|| list2.Count > 0)
            {
                if (list1.Count != list2.Count)
                    return false;

                List<TreeNode> next1 = new List<TreeNode>();
                List<TreeNode> next2 = new List<TreeNode>();
                for (int i=0; i<list1.Count; i++)
                {
                    if (list1[i].val != list2[i].val)
                        return false;

                    if ((list1[i].left == null && list2[i].left != null)
                        || (list1[i].left != null && list2[i].left == null)
                        || (list1[i].right == null && list2[i].right != null)
                        || (list1[i].right != null && list2[i].right == null))
                        return false;

                    if (list1[i].left != null)
                    {
                        next1.Add(list1[i].left);
                        next2.Add(list2[i].left);
                    }
                    if (list1[i].right != null)
                    {
                        next1.Add(list1[i].right);
                        next2.Add(list2[i].right);
                    }
                }

                list1 = next1;
                list2 = next2;
            }
            return true;
        }
        /// 101. Symmetric Tree
        ///Given the root of a binary tree, check whether it is a mirror of itself (i.e., symmetric around its center).
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null)
                return true;

            List<TreeNode> lefts = new List<TreeNode>();
            List<TreeNode> rights = new List<TreeNode>();

            if (root.left != null)
                lefts.Add(root.left);

            if (root.right != null)
                rights.Add(root.right);

            return IsSymmetric(lefts, rights);
        }

        public bool IsSymmetric(IList<TreeNode> lefts, IList<TreeNode> rights)
        {
            if (lefts.Count == 0 && rights.Count == 0)
                return true;

            if (lefts.Count != rights.Count)
                return false;

            List<TreeNode> sub1 = new List<TreeNode>();
            List<TreeNode> sub2 = new List<TreeNode>();

            for (int i = 0; i < lefts.Count; i++)
            {
                var left = lefts[i];
                var right = rights[lefts.Count - 1 - i];

                if (left.val != right.val)
                    return false;

                if (left.left == null && right.right != null
                    || left.left != null && right.right == null
                    || left.right == null && right.left != null
                    || left.right != null && right.left == null)
                    return false;

                if (left.left != null)
                    sub1.Add(left.left);
                if (left.right != null)
                    sub1.Add(left.right);

                if (right.right != null)
                    sub2.Insert(0, right.right);
                if (right.left != null)
                    sub2.Insert(0, right.left);
            }

            return IsSymmetric(sub1, sub2);
        }

        /// 102. Binary Tree Level Order Traversal
        /// Given the root of a binary tree, return the level order traversal of its nodes' values.
        /// (i.e., from left to right, level by level).
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var result = new List<IList<int>>();
            if (root == null)
                return result;

            var nodes = new List<TreeNode>() { root };

            while (nodes.Count > 0)
            {
                var subs = new List<TreeNode>();
                var list = new List<int>();
                foreach (TreeNode node in nodes)
                {
                    if (node == null)
                        continue;

                    list.Add(node.val);

                    if (node.left != null)
                        subs.Add(node.left);
                    if (node.right != null)
                        subs.Add(node.right);
                }

                nodes = subs;
                result.Add(list);
            }

            return result;
        }

        ///103. Binary Tree Zigzag Level Order Traversal
        ///return the zigzag level order traversal of its nodes' values.
        ///(i.e., from left to right, then right to left for the next level and alternate between).
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            var ans=new List<IList<int>>();
            if (root == null)
                return ans;

            var nodes=new List<TreeNode>() { root};
            bool forward = true;
            while (nodes.Count > 0)
            {
                var nexts = new List<TreeNode>();
                var vals = new List<int>();

                foreach(var node in nodes)
                {
                    vals.Add(node.val);
                    if (forward)
                    {
                        if (node.left != null)
                            nexts.Insert(0, node.left);
                        if (node.right != null)
                            nexts.Insert(0, node.right);
                    }
                    else
                    {
                        if (node.right != null)
                            nexts.Insert(0, node.right);
                        if (node.left != null)
                            nexts.Insert(0, node.left);
                    }
                }
                forward = !forward;
                nodes = nexts;
                ans.Add(vals);
            }
            return ans;
        }

        /// 104. Maximum Depth of Binary Tree
        /// Given the root of a binary tree, return its maximum depth.
        ///A binary tree's maximum depth is the number of nodes along the longest path
        ///from the root node down to the farthest leaf node.
        public int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;

            int deep = 0;
            var nodes = new List<TreeNode>() { root };
            while (nodes.Count > 0)
            {
                deep++;
                var subs = new List<TreeNode>();
                foreach (var node in nodes)
                {
                    if (node == null)
                        continue;

                    if (node.left != null)
                        subs.Add(node.left);

                    if (node.right != null)
                        subs.Add(node.right);
                }

                nodes = subs;
            }

            return deep;
        }

        ///105. Construct Binary Tree from Preorder and Inorder Traversal
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            return BuildTree(preorder, inorder, 0, preorder.Length - 1, 0, preorder.Length - 1);
        }
        public TreeNode BuildTree(int[] preorder, int[] inorder, int preLeft, int preRight, int inLeft, int inRight)
        {
            if (preLeft > preRight||inLeft>inRight)
                return null;

            if(preLeft == preRight || inLeft == inRight)
            {
                return new TreeNode(preorder[preLeft]);
            }

            TreeNode node = new TreeNode(preorder[preLeft]);
            var index = FindIndex(preorder[preLeft], inorder, inLeft,inRight);
            var countLeft = index - inLeft;
            var countRight = inRight-index;

            if (countLeft > 0)
            {
                node.left = BuildTree(preorder, inorder, preLeft + 1, preLeft + 1+countLeft-1, inLeft, index - 1);
            }

            if (countRight > 0)
            {
                node.right = BuildTree(preorder, inorder,
                    preLeft + 1 + countLeft - 1+1, preLeft + 1 + countLeft - 1 + 1 + countRight-1,
                    index+1, inRight);
            }
            return node;
        }

        public int FindIndex(int target, int[] array, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (target == array[i]) return i;
            }
            throw new ArgumentOutOfRangeException();
        }

        //106
        //public TreeNode BuildTree(int[] inorder, int[] postorder)
        //{
        //    if (inorder == null || inorder.Length == 0) return null;

        //    if (inorder.Length == 1)
        //    {
        //        return new TreeNode(inorder[0]);
        //    }

        //    TreeNode root = new TreeNode(postorder[postorder.Length-1]);
        //    int index = FindIndex(postorder[postorder.Length - 1], inorder);

        //    int[] leftInorder = new int[index];
        //    int[] leftPostorder = new int[index];
        //    int[] rightInorder = new int[inorder.Length - index - 1];
        //    int[] rightPostorder = new int[inorder.Length - index - 1];

        //    Array.Copy(inorder, 0, leftInorder, 0, index);
        //    Array.Copy(inorder, index + 1, rightInorder, 0, inorder.Length - index - 1);

        //    Array.Copy(postorder, 0, leftPostorder, 0, index);
        //    Array.Copy(postorder, index, rightPostorder, 0, inorder.Length - index - 1);

        //    root.left = BuildTree(leftInorder,leftPostorder);
        //    root.right = BuildTree(rightInorder,rightPostorder);

        //    return root;
        //}


        ///108. Convert Sorted Array to Binary Search Tree
        public TreeNode SortedArrayToBST(int[] nums)
        {
            Array.Sort(nums);
            return SortedArrayToBST_Recursion(nums, 0, nums.Length - 1);
        }

        public TreeNode SortedArrayToBST_Recursion(int[] nums, int start, int end)
        {
            if (start > end)
                return null;
            if (start == end)
                return new TreeNode(nums[start]);
            var mid = (start + end) / 2;
            var node = new TreeNode(nums[mid]);
            if (mid - 1 >= start)
            {
                node.left = SortedArrayToBST_Recursion(nums, start, mid - 1);
            }
            if (mid + 1 <= end)
            {
                node.right = SortedArrayToBST_Recursion(nums, mid + 1, end);
            }
            return node;
        }
        ///110. Balanced Binary Tree
        ///Given a binary tree, determine if it is height-balanced.
        ///a binary tree in which the left and right subtrees of every node differ in height by no more than 1.
        public bool IsBalanced(TreeNode root)
        {
            if (root == null)
                return true;
            var leftDeep = IsBalanced_Deep(root.left, 1);
            var rightDeep = IsBalanced_Deep(root.right, 1);
            if (leftDeep == -1 || rightDeep == -1)
            {
                return false;
            }
            else
            {
                if(leftDeep-rightDeep>=-1 && leftDeep - rightDeep <= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int IsBalanced_Deep(TreeNode root, int deep)
        {
            if (root == null)
                return deep;
            if(root.left == null && root.right == null)
            {
                return deep + 1;
            }
            if (root.left == null)
            {
                if (root.right.left != null || root.right.right != null)
                    return -1;
                return IsBalanced_Deep(root.right, deep+1);
            }
            else if (root.right == null)
            {
                if (root.left.left != null || root.left.right != null)
                    return -1;
                return IsBalanced_Deep(root.left, deep + 1);
            }
            else
            {
                var leftDeep = IsBalanced_Deep(root.left, deep + 1);
                var rightDeep = IsBalanced_Deep(root.right, deep + 1);
                if (leftDeep == -1 || rightDeep == -1)
                {
                    return -1;
                }
                else
                {
                    if (leftDeep - rightDeep >= -1 && leftDeep - rightDeep <= 1)
                    {
                        return Math.Max(leftDeep,rightDeep);
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
        /// 112. Path Sum
        /// Given the root of a binary tree and an integer targetSum,
        /// return true if the tree has a root-to-leaf path such that adding up all the values along the path equals targetSum.
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
                return false;

            var result = HasPathSum_Recursion(root, 0, targetSum);
            if (result)
                return true;

            return false;
        }

        public bool HasPathSum_Recursion(TreeNode node, int sum, int targetSum)
        {
            if (node == null)
                return false;

            sum += node.val;

            if (node.left == null && node.right == null && sum == targetSum)
            {
                return true;
            }

            var resultLeft = HasPathSum_Recursion(node.left, sum, targetSum);
            if (resultLeft)
                return true;
            var resultRight = HasPathSum_Recursion(node.right, sum, targetSum);
            if (resultRight)
                return true;

            return false;
        }

        ///113. Path Sum II
        ///return all root-to-leaf paths where the sum of the node values in the path equals targetSum
        public IList<IList<int>> PathSum(TreeNode root, int targetSum)
        {
            var ans=new List<IList<int>>();
            PathSum_Recursion(root, targetSum, new List<int>(), ans);
            return ans;
        }

        public void PathSum_Recursion(TreeNode node, int targetSum, IList<int> list, IList<IList<int>> ans)
        {
            if (node == null)
                return;
            list.Add(node.val);
            if (node.left == null && node.right == null)
            {
                if (list.Sum() == targetSum)
                    ans.Add(list);
                return;
            }
            PathSum_Recursion(node.left, targetSum, new List<int>(list), ans);
            PathSum_Recursion(node.right, targetSum, new List<int>(list), ans);
        }

        /// 117. Populating Next Right Pointers in Each Node II
        /// Populate each next pointer to point to its next right node.
        /// If there is no next right node, the next pointer should be set to NULL.
        public Node Connect(Node root)
        {
            if (root == null)
                return null;

            List<Node> list = new List<Node>
            {
                root
            };
            while (list.Count != 0)
            {
                List<Node> subs = new List<Node>();

                for (int i = 0; i < list.Count; i++)
                {
                    list[i].next = i == list.Count - 1 ? null : list[i + 1];
                    if (list[i].left != null)
                        subs.Add(list[i].left);
                    if (list[i].right != null)
                        subs.Add(list[i].right);
                }

                list = subs;
            }

            return root;
        }

        ///118. Pascal's Triangle
        ///Given an integer numRows, return the first numRows of Pascal's triangle.
        ///    1
        ///   1 1
        ///  1 2 1
        /// 1 3 3 1
        public IList<IList<int>> Generate(int numRows)
        {
            List<IList<int>> list = new List<IList<int>>();
            int i = 1;
            list.Add(new List<int>() { 1 });
            i++;
            while (i <= numRows)
            {
                var list2 = new List<int>();
                int j = 1;
                list2.Add(1);
                j++;
                while (j < i)
                {
                    list2.Add(list[i - 1 - 1][j - 1 - 1] + list[i - 1 - 1][j - 1]);
                    j++;
                }
                list2.Add(1);
                list.Add(list2);
                i++;
            }

            return list;
        }

        ///119. Pascal's Triangle II
        ///Given an integer rowIndex, return the rowIndexth (0-indexed) row of the Pascal's triangle.
        ///In Pascal's triangle, each number is the sum of the two numbers directly above it as shown:
        public IList<int> GetRow(int rowIndex)
        {
            List<int> currentRow = new List<int>() { 1 };
            int lastRowIndex = 0;
            while (lastRowIndex < rowIndex)
            {
                var list2 = new List<int>();
                int j = 0;
                list2.Add(1);
                j++;
                while (j < lastRowIndex + 1)
                {
                    list2.Add(currentRow[j - 1] + currentRow[j]);
                    j++;
                }
                list2.Add(1);
                currentRow = list2;
                lastRowIndex++;
            }

            return currentRow;
        }

        ///120. Triangle
        ///Given a triangle array, return the minimum path sum from top to bottom.
        ///   2
        ///  3 4
        /// 6 5 7   => 2 3 5
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            if (triangle == null)
                return 0;

            int level = 1;
            List<int> minList = new List<int>
            {
                triangle[level - 1][0]
            };

            while (++level <= triangle.Count)
            {
                List<int> subList = new List<int>();

                for (int i = 0; i < level; i++)
                {
                    if (i == 0)
                    {
                        subList.Add(triangle[level - 1][0] + minList[0]);
                    }
                    else if (i > 0 && i < level - 1)
                    {
                        subList.Add(Math.Min(triangle[level - 1][i] + minList[i - 1], triangle[level - 1][i] + minList[i]));
                    }
                    else
                    {
                        subList.Add(triangle[level - 1][level - 1] + minList[level - 1 - 1]);
                    }
                }

                minList = subList;
            }

            return minList.Min();
        }

        ///121. Best Time to Buy and Sell Stock - trade 1 time
        public int MaxProfit_121_OnlyOneTrade(int[] prices)
        {
            if (prices == null || prices.Length <= 1)
                return 0;

            if (prices.Length == 2)
            {
                if (prices[0] >= prices[1])
                    return 0;
                else
                    return prices[1] - prices[0];
            }

            int MinSoFar = prices[0];
            int maxProfit = 0;
            foreach (var currentPrice in prices)
            {
                MinSoFar = Math.Min(MinSoFar, currentPrice);
                maxProfit = Math.Max(maxProfit, currentPrice - MinSoFar);
            }
            return maxProfit;
        }

        ///122. Best Time to Buy and Sell Stock II - can trade many times
        public int MaxProfit_122_TradeManyTimes(int[] prices)
        {
            if (prices == null || prices.Length <= 1)
                return 0;

            if (prices.Length == 2)
            {
                if (prices[0] >= prices[1])
                    return 0;
                else
                    return prices[1] - prices[0];
            }

            int sum = 0;
            bool isHold = false;
            int buy = 0;
            for (int i = 0; i < prices.Length - 1; i++)
            {
                if (isHold)
                {
                    if (i == prices.Length - 2)
                    {
                        sum += Math.Max(prices[i + 1], prices[i]) - buy;
                    }
                    else
                    {
                        if (prices[i] <= prices[i + 1])
                        {
                            continue;
                        }
                        else
                        {
                            sum += prices[i] - buy;
                            isHold = false;
                        }
                    }
                }
                else
                {
                    if (prices[i] >= prices[i + 1])
                    {
                        continue;
                    }
                    else
                    {
                        if (i == prices.Length - 2)
                        {
                            sum += prices[i + 1] - prices[i];
                            break;
                        }
                        isHold = true;
                        buy = prices[i];
                    }
                }
            }
            return sum;
        }

        ///125. Valid Palindrome
        ///after converting all uppercase letters into lowercase letters and removing all non-alphanumeric characters,
        ///a-z, A-Z, 0-9
        public bool IsPalindrome(string s)
        {
            Stack<char> stack = new Stack<char>();

            for(int i=0; i<s.Length; i++)
            {
                if ((s[i] >= 'a' && s[i] <= 'z') || (s[i] >= '0' && s[i] <= '9'))
                {
                    stack.Push(s[i]);
                }
                else if(s[i] >= 'A' && s[i] <= 'Z')
                {
                    stack.Push((char)(s[i]+32));
                }
            }

            if (stack.Count == 0)
                return true;

            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i] >= 'a' && s[i] <= 'z') || (s[i] >= '0' && s[i] <= '9'))
                {
                    if (s[i] != stack.Pop())
                        return false;
                }
                else if (s[i] >= 'A' && s[i] <= 'Z')
                {
                    if ((char)(s[i] + 32) != stack.Pop())
                        return false;
                }
            }
            return true;
        }
        /// 128
        public int LongestConsecutive(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            Array.Sort(nums);
            int max = 1;
            int current = 1;
            int pre = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] <= pre + 1)
                {
                    if (nums[i] != pre) current++;
                }
                else
                {
                    max = Math.Max(current, max);
                    current = 1;
                }
                pre = nums[i];
            }

            return max = Math.Max(current, max);
        }

        /// 130. Surrounded Regions
        ///Given an m x n matrix board containing 'X' and 'O',
        ///capture all regions that are 4-directionally surrounded by 'X'.
        ///A region is captured by flipping all 'O's into 'X's in that surrounded region.
        ///Tips: If 'O's connect to 4-direction edges , escape; or captured
        public void Solve(char[][] board)
        {

            int rowLen = board.Length;
            int colLen = board[0].Length;

            if (rowLen <= 1 || colLen <= 1)
                return;

            bool[][] visit = new bool[rowLen][];
            for (int i = 0; i < rowLen; i++)
            {
                visit[i] = new bool[colLen];
            }

            int[][] dxy = new int[4][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, -1 } };

            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (visit[i][j])
                        continue;

                    visit[i][j] = true;

                    if (board[i][j] == 'O')
                    {
                        List<int[]> list = new List<int[]>();

                        List<int[]> currList = new List<int[]>
                        {
                            new int[] { i, j }
                        };

                        bool capture = true;

                        while (currList.Count > 0)
                        {
                            list.AddRange(currList);

                            List<int[]> subList = new List<int[]>();

                            foreach (var cell in list)
                            {

                                foreach (var d in dxy)
                                {
                                    int r = cell[0] + d[0];
                                    int c = cell[1] + d[1];

                                    if (r >= 0 && r <= rowLen - 1
                                        && c >= 0 && c <= colLen - 1)
                                    {
                                        if (!visit[r][c])
                                        {
                                            visit[r][c] = true;

                                            if (board[r][c] == 'O')
                                            {
                                                subList.Add(new int[] { r, c });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        capture = false;
                                    }
                                }
                            }

                            currList = subList;
                        }

                        if (capture)
                        {
                            foreach (var cell in list)
                                board[cell[0]][cell[1]] = 'X';
                        }

                    }

                }
            }

        }

        /// 134. Gas Station
        ///  Given two integer arrays gas and cost, return the starting gas station's index
        ///  if you can travel around the circuit once in the clockwise direction,
        ///  otherwise return -1. If there exists a solution, it is guaranteed to be unique
        /// start/arrive at i-th, will get gas[i]; go to next will cost[i]

        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int ans = -1;

            for (int i = 0; i < gas.Length; i++)
            {
                if (gas[i] == 0 || gas[i] < cost[i])
                    continue;

                int sum = gas[i] - cost[i];
                int j = i == gas.Length - 1 ? 0 : i + 1;

                while (j != i)
                {
                    sum = sum + gas[j] - cost[j];
                    if (sum < 0)
                        break;
                    j = j == gas.Length - 1 ? 0 : j + 1;
                }

                if (j == i)
                    return i;
            }

            return ans;
        }
        /// 136. Single Number
        /// Given a non - empty array of integers nums, every element appears twice except for one.Find that single one.
        /// You must implement a solution with a linear runtime complexity and use only constant extra space.
        /// Input: nums = [2,2,1]
        /// Output: 1
        public int SingleNumber(int[] nums)
        {
            return nums.Aggregate((x, y) => x ^ y);
        }

        ///139. Word Break
        ///return true if s can be segmented into a space-separated sequence of one or more dictionary words.
        ///Note that the same word in the dictionary may be reused multiple times in the segmentation.
        public bool WordBreak(string s, IList<string> wordDict)
        {
            var arr = s.ToArray();
            List<int> list = new List<int> { 0 };
            List<int> exist = new List<int> { 0 };
            while (list.Count > 0)
            {
                List<int> next = new List<int>();
                foreach (var i in list)
                {
                    for (int j = 0; j < wordDict.Count; j++)
                    {
                        if (arr.Length - i < wordDict[j].Length)
                            continue;
                        bool canBreak = true;
                        int k = 0;
                        while (k < wordDict[j].Length)
                        {
                            if (wordDict[j][k] != arr[i + k])
                            {
                                canBreak = false;
                                break;
                            }
                            k++;
                        }
                        if (canBreak)
                        {
                            var idx = i + wordDict[j].Length;
                            if (idx == arr.Length)
                                return true;
                            if (!exist.Contains(idx))
                            {
                                next.Add(idx);
                                exist.Add(idx);
                            }
                        }
                    }
                }
                list = next;
            }
            return false;
        }
        /// 141. Linked List Cycle
        public bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null) return false;

            List<ListNode> list = new List<ListNode>();

            while (head != null)
            {
                if (list.IndexOf(head) == -1)
                {
                    list.Add(head);
                    head = head.next;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        ///142. Linked List Cycle II
        ///If there is no cycle, return null.
        public ListNode DetectCycle(ListNode head)
        {
            List<ListNode> nodes = new List<ListNode>();
            var current = head;
            while (current != null)
            {
                var exist = nodes.FirstOrDefault(x => x == current);
                if (exist != null)
                    return exist;
                nodes.Add(current);
                current = current.next;
            }
            return null;
        }
        ///143. Reorder List
        ///Reorder the list: 1->2->3->4 to 1->4->2->3, 1->2->3->4->5 to 1->5->2->4->3
        public void ReorderList(ListNode head)
        {
            Stack<ListNode> stack = new Stack<ListNode>();
            var node=head;
            while(node != null)
            {
                stack.Push(node);
                node=node.next;
            }
            int i=stack.Count;
            node = head;
            if (i <= 2)
                return;
            while (i > 0)
            {
                var next = node.next;
                var pop=stack.Pop();
                pop.next = next;
                node.next = pop;
                //last = pop;
                node= next;
                i-=2;
                if (i == 2)
                {
                    node.next.next = null;
                    break;
                }
                if (i == 1)
                {
                    node.next = null;
                    break;
                }
            }
        }
        /// 144. Binary Tree Preorder Traversal
        public IList<int> PreorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            PreorderTraversal(root, result);
            return result;
        }

        public void PreorderTraversal(TreeNode node, IList<int> list)
        {
            if (node == null)
                return;

            list.Add(node.val);
            PreorderTraversal(node.left, list);
            PreorderTraversal(node.right, list);
        }

        //145. Binary Tree Postorder Traversal
        public IList<int> PostorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            PostorderTraversal(root, result);
            return result;
        }

        public void PostorderTraversal(TreeNode node, IList<int> list)
        {
            if (node == null)
                return;

            PostorderTraversal(node.left, list);
            PostorderTraversal(node.right, list);
            list.Add(node.val);
        }

        ///149. Max Points on a Line
        ///return the maximum number of points that lie on the same straight line.
        ///1 <= points.length <= 300, -10^4 <= xi, yi <= 10^4, All the points are unique.
        public int MaxPoints(int[][] points)
        {
            if (points.Length <= 2)
                return points.Length;

            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
            Dictionary<int, List<int>> dictX = new Dictionary<int, List<int>>();
            //Dictionary<int, List<int>> dictY = new Dictionary<int, List<int>>();

            for (int i = 0; i < points.Length; i++)
            {
                if (i == 0)
                {
                    dictX.Add(points[i][0], new List<int>() { i});
                    continue;
                }

                List<string> findKeys = new List<string>();
                foreach(var d in dict.Keys)
                {
                    var strs = d.Split(',');
                    var a = double.Parse( strs[0]);
                    var b = double.Parse(strs[1]);

                    if (points[i][0] * a + b == points[i][1])
                    {
                        findKeys.Add(d);
                    }
                }

                List<int> skips = new List<int>();
                foreach (var findKey in findKeys)
                {
                    skips.AddRange(dict[findKey]);
                    dict[findKey].Add(i);
                }

                for (int j = 0; j < i; j++)
                {
                    if (skips.Contains(j))
                        continue;

                    if (points[i][0] == points[j][0])
                        continue;

                    var a = 1.0 * (points[i][1] - points[j][1]) / (points[i][0] - points[j][0]);
                    var b = 1.0 * (points[j][1]* points[i][0] - points[j][0]* points[i][1]) / (points[i][0] - points[j][0]);

                    string key = $"{a},{b}";

                    if (!dict.ContainsKey(key))
                    {
                        dict.Add(key, new List<int> { j, i });
                    }
                    else
                    {
                        if(!dict[key].Contains(i))
                            dict[key].Add(i);
                        if (!dict[key].Contains(j))
                            dict[key].Add(j);
                    }
                }

                if (dictX.ContainsKey(points[i][0]))
                {
                    dictX[points[i][0]].Add(i);
                }
                else
                {
                    dictX.Add(points[i][0], new List<int>() { i });
                }
            }

            int count1 = 0;
            if (dict.Count > 0)
            {
                var list1 = dict.Values.OrderBy(x => -x.Count).ToList();
                count1 = list1[0].Count;
            }
            int count2 = 0;
            if (dictX.Count > 0)
            {
                var list2 = dictX.Values.OrderBy(x => -x.Count).ToList();
                count2 = list2[0].Count;
            }
            return Math.Max(count1,count2);
        }
    }
}