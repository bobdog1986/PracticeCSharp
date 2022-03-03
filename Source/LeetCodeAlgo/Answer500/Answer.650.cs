using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        /// 653. Two Sum IV - Input is a BST
        ///return true if there exist two elements in the BST such that sum = target.
        public bool FindTarget(TreeNode root, int k)
        {
            return FindTarget(root, k, new Dictionary<int,int>());
        }
        public bool FindTarget(TreeNode root, int k, Dictionary<int, int> dict)
        {
            if (root == null) return false;
            if (dict.ContainsKey(root.val)) return true;
            dict.Add(k - root.val,1);
            return FindTarget(root.left, k, dict) || FindTarget(root.right, k, dict);
        }

        ///662. Maximum Width of Binary Tree
        ///The maximum width of a tree is the maximum width among all levels.
        ///The width of one level is defined as the length between the end-nodes(the leftmost and rightmost non-null nodes),
        ///where the null nodes between the end-nodes are also counted into the length calculation.
        ///The number of nodes in the tree is in the range [1, 3000].
        public int WidthOfBinaryTree(TreeNode root)
        {
            int ans = 1;
            Dictionary<int, TreeNode> dict = new Dictionary<int, TreeNode>
            {
                { 0, root }
            };
            while (dict.Count > 0)
            {
                Dictionary<int, TreeNode> next = new Dictionary<int, TreeNode>();
                int start = -1;
                int end = -1;
                foreach (var key in dict.Keys)
                {
                    var node = dict[key];
                    if (node.left != null)
                    {
                        if (start == -1) start = key * 2;
                        end = key * 2;
                        next.Add(key * 2, node.left);
                    }
                    if (node.right != null)
                    {
                        if (start == -1) start = key * 2 + 1;
                        end = key * 2 + 1;
                        next.Add(key * 2 + 1, node.right);
                    }
                }
                ans = Math.Max(ans, end - start + 1);
                dict = next;
            }
            return ans;
        }

        ///673. Number of Longest Increasing Subsequence - not understand
        ///return the number of longest increasing subsequences. [1,3,5,4,7]->[1, 3, 4, 7] and [1, 3, 5, 7]. return 2
        public int FindNumberOfLIS(int[] nums)
        {
            int ans = 0, max_len = 0;
            int[] len = new int[nums.Length];
            int[] cnt = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                len[i] = cnt[i] = 1;
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        if (len[i] == len[j] + 1)
                            cnt[i] += cnt[j];
                        if (len[i] < len[j] + 1)
                        {
                            len[i] = len[j] + 1;
                            cnt[i] = cnt[j];
                        }
                    }
                }
                if (max_len == len[i])
                    ans += cnt[i];
                if (max_len < len[i])
                {
                    max_len = len[i];
                    ans = cnt[i];
                }
            }
            return ans;
        }

        ///693. Binary Number with Alternating Bits
        ///Given a positive integer, check whether it has alternating bits, adjacent bits always have different values.
        public bool HasAlternatingBits(int n)
        {
            bool last = (n & 1) == 0;
            while (n > 0)
            {
                bool curr= (n & 1) == 1;
                if (curr == last) return false;
                last = curr;
                n >>= 1;
            }
            return true;
        }


        /// 695. Max Area of Island, #Graph, #DFS
        /// Return the maximum area of an island in grid. If there is no island, return 0.
        public int MaxAreaOfIsland(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            int max = 0;
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        Queue<int[]> queue = new Queue<int[]>();
                        grid[i][j] = 0;
                        int count = 1;
                        queue.Enqueue(new int[] { i, j });
                        while (queue.Count > 0)
                        {
                            var curr = queue.Dequeue();
                            foreach(var d in dxy4)
                            {
                                int r = curr[0] + d[0];
                                int c = curr[1] + d[1];
                                if (r >= 0 && r < rowLen && c >= 0 && c < colLen && grid[r][c] == 1)
                                {
                                    grid[r][c] = 0;
                                    queue.Enqueue(new int[] { r, c });
                                    count++;
                                }
                            }
                        }
                        max = Math.Max(max, count);
                    }
                }
            }
            return max;
        }

        ///697
        public int FindShortestSubArray(int[] nums)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dictionary.ContainsKey(nums[i]))
                {
                    dictionary[nums[i]]++;
                }
                else
                {
                    dictionary.Add(nums[i], 1);
                }
            }

            int frequency = dictionary.Values.Max();
            var candidate = dictionary.Where(o => o.Value == frequency).Select(o => o.Key);
            int minLength = nums.Length;
            foreach (var i in candidate)
            {
                int length = GetLastIndex(nums, i) - GetFirstIndex(nums, i) + 1;
                minLength = length < minLength ? length : minLength;
            }
            return minLength;
        }

        public int GetFirstIndex(int[] nums, int key)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == key) return i;
            }
            throw new ArgumentOutOfRangeException();
        }

        public int GetLastIndex(int[] nums, int key)
        {
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                if (nums[i] == key) return i;
            }
            throw new ArgumentOutOfRangeException();
        }

        /// 698. Partition to K Equal Sum Subsets
        ///return true if it is possible to divide this array into k non-empty subsets whose sums are all equal.
        ///1 <= k <= nums.length <= 16,1 <= nums[i] <= 10^4, The frequency of each element is in the range [1, 4].
        public bool CanPartitionKSubsets(int[] nums, int k)
        {
            if (k == 1)
                return true;

            if (k == nums.Length)
            {
                if (nums.Distinct().Count() != 1)
                {
                    return false;
                }
            }

            int sum = nums.Sum();
            if (sum % k != 0)
                return false;

            int goal = sum / k;

            bool[] visit = new bool[nums.Length];
            foreach (var n in nums)
            {
                if (n > goal)
                    return false;
            }

            nums = nums.OrderBy(x => -x).ToArray();
            bool found = false;
            CanPartitionKSubsets_Recursion(nums, visit, 0, k, goal, 0, ref found);
            return found;
        }

        public void CanPartitionKSubsets_Recursion(int[] nums, bool[] visit, int left, int k, int goal, int sum, ref bool found)
        {
            if (found == true)
                return;

            if (k == 1)
            {
                found = true;
                return;
            }

            for (int i = left; i < nums.Length; i++)
            {
                if (visit[i])
                    continue;

                if (nums[i] == goal - sum)
                {
                    var nextVisit = new bool[nums.Length];
                    Array.Copy(visit, nextVisit, visit.Length);
                    nextVisit[i] = true;
                    CanPartitionKSubsets_Recursion(nums, nextVisit, 0, k - 1, goal, 0, ref found);
                }
                else if (nums[i] < goal - sum)
                {
                    var nextVisit = new bool[nums.Length];
                    Array.Copy(visit, nextVisit, visit.Length);
                    nextVisit[i] = true;
                    CanPartitionKSubsets_Recursion(nums, nextVisit, i + 1, k, goal, sum + nums[i], ref found);
                }
            }
        }

    }
}