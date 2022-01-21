using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///101. Symmetric Tree
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

        ///105
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            if (preorder == null || preorder.Length == 0) return null;

            if (preorder.Length == 1)
            {
                return new TreeNode(preorder[0]);
            }

            TreeNode root = new TreeNode(preorder[0]);
            int index = FindIndex(preorder[0], inorder);
            int[] leftPreorder = new int[index];
            int[] leftInorder = new int[index];
            int[] rightPreorder = new int[inorder.Length - index - 1];
            int[] rightInorder = new int[inorder.Length - index - 1];

            Array.Copy(inorder, 0, leftInorder, 0, index);
            Array.Copy(inorder, index + 1, rightInorder, 0, inorder.Length - index - 1);

            Array.Copy(preorder, 1, leftPreorder, 0, index);
            Array.Copy(preorder, index + 1, rightPreorder, 0, inorder.Length - index - 1);

            root.left = BuildTree(leftPreorder, leftInorder);
            root.right = BuildTree(rightPreorder, rightInorder);

            return root;
        }

        public int FindIndex(int find, int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (find == array[i]) return i;
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

        //116. Populating Next Right Pointers in Each Node

        /// 112. Path Sum
        /// Given the root of a binary tree and an integer targetSum,
        /// return true if the tree has a root-to-leaf path such that adding up all the values along the path equals targetSum.
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
                return false;

            var result = LoopPathSum(root, 0, targetSum);
            if (result)
                return true;

            return false;
        }

        public bool LoopPathSum(TreeNode node, int sum, int targetSum)
        {
            if (node == null)
                return false;

            sum += node.val;

            if (node.left == null && node.right == null && sum == targetSum)
            {
                return true;
            }

            var resultLeft = LoopPathSum(node.left, sum, targetSum);
            if (resultLeft)
                return true;
            var resultRight = LoopPathSum(node.right, sum, targetSum);
            if (resultRight)
                return true;

            return false;
        }

        public Node Connect(Node root)
        {
            if (root == null)
                return null;

            List<Node> list = new List<Node>();
            list.Add(root);
            while (list.Count != 0)
            {
                List<Node> subs = new List<Node>();

                bool hasSubs = list[0].left != null;
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].next = i == list.Count - 1 ? null : list[i + 1];
                    if (hasSubs)
                    {
                        subs.Add(list[i].left);
                        subs.Add(list[i].right);
                    }
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
        /// 6 5 7   = 2 3 5
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            if (triangle == null)
                return 0;

            int level = 1;
            List<int> minList = new List<int>();
            minList.Add(triangle[level - 1][0]);

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

        ///121. Best Time to Buy and Sell Stock
        public int MaxProfit_121(int[] prices)
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

        ///122. Best Time to Buy and Sell Stock II
        ///total max sum ,can trade many times
        public int MaxProfit(int[] prices)
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
                        continue;

                    if (i == prices.Length - 2)
                    {
                        if (prices[i] < prices[i + 1])
                        {
                            sum += prices[i + 1] - prices[i];
                        }
                        break;
                    }

                    isHold = true;
                    buy = prices[i];
                }
            }
            return sum;
        }

        //128
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

            //if (nums.Length == 1)
            //    return nums[0];

            //int a = nums[0];

            //for (int i = 1; i < nums.Length; i++)
            //{
            //    a ^= nums[i];
            //}

            //return a;
        }

        ///141. Linked List Cycle

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
                var exist=nodes.FirstOrDefault(x => x == current);
                if (exist!=null)
                    return exist;

                nodes.Add(current);
                current = current.next;
            }
            return null;
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

        //149

        public int MaxPoints(Point[] points)
        {
            if (points == null || points.Length == 0) return 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            Dictionary<int, int> dictY = new Dictionary<int, int>();
            foreach (var i in points)
            {
                if (dict.ContainsKey(i.x))
                {
                    dict[i.x]++;
                }
                else
                {
                    dict[i.x] = 1;
                }

                if (dictY.ContainsKey(i.y))
                {
                    dictY[i.y]++;
                }
                else
                {
                    dictY[i.y] = 1;
                }
            }
            return Math.Max(dict.Values.Max(), dictY.Values.Max());
        }

        ///152. Maximum Product Subarray
        ///Given an integer array nums, find a contiguous non-empty subarray within the array
        ///that has the largest product, and return the product.
        ///-10 <= nums[i] <= 10
        public int MaxProduct(int[] nums)
        {
            int max = nums[0];
            int min = nums[0];
            int result = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] < 0)
                {
                    int temp = max;
                    max = min;
                    min = temp;
                }

                max = Math.Max(nums[i], max * nums[i]);
                min = Math.Min(nums[i], min * nums[i]);
                result = Math.Max(max, result);
            }

            return result;
        }

        public int MaxProduct_1(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int max = -10;

            List<int> list = new List<int>();
            List<int> indexsOfNegative = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    max = Math.Max(max, 0);
                    max = Math.Max(max, GetMaxProduct(list, indexsOfNegative));

                    list.Clear();
                    indexsOfNegative.Clear();
                }
                else
                {
                    list.Add(nums[i]);
                    if (nums[i] < 0)
                    {
                        indexsOfNegative.Add(list.Count - 1);
                    }
                }
            }

            max = Math.Max(max, GetMaxProduct(list, indexsOfNegative));

            return max;
        }

        public int GetMaxProduct(List<int> list, List<int> indexsOfNegative = null)
        {
            if (list.Count == 0)
                return 0;
            if (list.Count == 1)
                return list[0];

            if (indexsOfNegative == null || indexsOfNegative.Count % 2 == 0)
            {
                return list.Aggregate((x, y) => x * y);
            }
            else
            {
                return Math.Max(
                    GetMaxProduct(list.GetRange(indexsOfNegative[0] + 1, list.Count - indexsOfNegative[0] - 1)),
                    GetMaxProduct(list.GetRange(0, indexsOfNegative[indexsOfNegative.Count - 1]))
                    );
            }
        }

        /// 153. Find Minimum in Rotated Sorted Array
        /// use binary search
        public int FindMin(int[] nums)
        {
            if(nums.Length==1)
                return nums[0];

            int start = 0;
            int end = nums.Length - 1;

            if(nums[start]<nums[end])
                return nums[start];

            int mid = (end - start) / 2 + start;

            while (start<=end)
            {
                if(start==end)
                    return nums[start];

                if (start + 1 == end)
                    return Math.Min(nums[start], nums[end]);

                if (nums[end] < nums[mid])
                {
                    start = mid;
                    mid= (end - start) / 2 + start;
                }
                else if(nums[mid] < nums[start])
                {
                    end = mid;
                    mid = (end - start) / 2 + start;
                }
            }

            return nums[start];
        }

        ///162. Find Peak Element
        ///return the index to any of the peaks which greater than its neighbors.
        ///nums[-1] = nums[n] = int.Min
        public int FindPeakElement(int[] nums)
        {
            if(nums.Length==1)
                return 0;

            bool left, right;
            for(int i = 0; i < nums.Length; i++)
            {
                left= i == 0 || nums[i] > nums[i - 1];
                right = i == nums.Length - 1 || nums[i] > nums[i + 1];
                if (left&&right)
                {
                    return i;
                }
                //else if(left)
                //{
                //    ;
                //}
                else if (right)
                {
                    i++;
                }
                //else
                //{
                //    i++;
                //}
            }

            return 0;
        }

        public int[] TwoSumII(int[] numbers, int target)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (i > 0)
                {
                    if (numbers[i - 1] == numbers[i]) { continue; }
                }

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[i] + numbers[j] != target) { continue; }

                    if (numbers[i] + numbers[j] == target)
                    {
                        return new int[2] { i + 1, j + 1 };
                    }
                }
            }
            throw new ArgumentOutOfRangeException();
        }



        ///169. Majority Element
        ///The majority element is the element that appears more than n/2 times.
        public int MajorityElement(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            int half = nums.Length % 2 == 1 ? nums.Length/2+1 : nums.Length / 2;

            //int time = 1;
            for(int i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(nums[i]))
                {
                    dict[nums[i]]++;
                }
                else
                {
                    dict.Add(nums[i], 1);
                }
            }

            var major=dict.Where(x => x.Value>=half).ToList();
            if (major == null)
                return -1;
            return major[0].Key;
        }
        /// 174 not done
        public int CalculateMinimumHP(int[,] dungeon)
        {
            if (dungeon == null || dungeon.Length == 0) return 0;

            int col = dungeon.GetLength(0);
            int row = dungeon.GetLength(1);

            int maxLost = 0;

            List<int> path = new List<int>();

            return maxLost > 0 ? 1 : (maxLost + 1);
        }

        //187
        public IList<string> FindRepeatedDnaSequences(string s)
        {
            Dictionary<string, int> dna = new Dictionary<string, int>();
            List<string> repeat = new List<string>();
            if (string.IsNullOrEmpty(s)) return repeat;
            if (s.Length <= 10) return repeat;

            for (int i = 0; i < s.Length - 10 + 1; i++)
            {
                string str = s.Substring(i, 10);
                if (dna.ContainsKey(str))
                {
                    dna[str]++;
                }
                else
                {
                    dna.Add(str, 1);
                }
            }

            repeat = dna.Where(o => o.Value > 1).Select(o => o.Key).ToList();

            return repeat;

            /*
            List<string> all = new List<string>();
            List<string> repeat = new List<string>();

            if (string.IsNullOrEmpty(s)) return repeat;
            if (s.Length <= 10) return repeat;

            for (int i = 0; i < s.Length - 10 + 1; i++)
            {
                string str = s.Substring(i, 10);
                if (all.Contains(str))
                {
                    if (!repeat.Contains(str))
                    {
                        repeat.Add(str);
                    }
                }
                else
                {
                    all.Add(str);
                }
            }

            return repeat;*/
        }

        //189. Rotate Array

        public void Rotate(int[] nums, int k)
        {
            k = k % nums.Length;

            if (k == 0) return;

            int[] temp = new int[k];
            for (int i = 0; i < k; i++)
            {
                temp[i] = nums[nums.Length - k + i];
            }

            for (int i = nums.Length - 1; i > k - 1; i--)
            {
                nums[i] = nums[i - k];
            }

            for (int i = 0; i < k; i++)
            {
                nums[i] = temp[i];
            }
        }

        ///190. Reverse Bits
        ///Reverse bits of a given 32 bits unsigned integer.
        ///Input: n =            00000010100101000001111010011100
        ///Output:    964176192 (00111001011110000010100101000000)
        public uint reverseBits(uint n)
        {
            if (n == 0) return 0;

            uint result = 0;
            uint a = uint.MaxValue / 2 + 1;
            uint c = 1;
            while (a > 0)
            {
                uint b = n / a;
                if (b == 1)
                {
                    n = n - a;

                    result += c;
                }
                a = a / 2;
                c = c * 2;
            }

            return result;
        }

        /// 191. Number of 1 Bits
        /// eg. 5=101, return count of 1 = 2;

        public int HammingWeight(uint n)
        {
            if (n == 0) return 0;
            uint a = uint.MaxValue / 2 + 1;

            int count = 0;
            while (n > 0)
            {
                var b = n / a;
                if (b == 1)
                {
                    n -= a;
                    count++;
                }

                a = a / 2;
            }

            return count;
        }

        ///198. House Robber
        ///cannot rob adjacent houses
        public int Rob_198(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];

            int[] dp = new int[nums.Length];

            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);

            for (int i = 2; i < nums.Length; i++)
            {
                dp[i] = Math.Max(nums[i] + dp[i - 2], dp[i - 1]);
            }

            return dp[nums.Length - 1];
        }
    }
}