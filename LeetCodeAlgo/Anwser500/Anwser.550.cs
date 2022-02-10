using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        /// 557. Reverse Words in a String III
        public string ReverseWords(string s)
        {
            var arr = s.Split(' ');
            if (arr.Length == 0)
                return s;

            for (int i = 0; i < arr.Length; i++)
            {
                var carr = arr[i].ToCharArray();
                ReverseString(carr);
                arr[i] = string.Join("", carr);
            }

            return string.Join(" ", arr);
        }

        ///559. Maximum Depth of N-ary Tree
        ///Given a n-ary tree, find its maximum depth.
        public int MaxDepth(N_ary root)
        {
            if(root == null)
                return 0;
            return 1 + MaxDepth(root.children);
        }

        public int MaxDepth(IList<N_ary> nodes)
        {
            if(nodes == null||nodes.Count==0)
                return 0;
            int max = 0;
            foreach (var node in nodes)
            {
                max = Math.Max(max, 1 + MaxDepth(node.children));
            }
            return max;
        }

        /// 560. Subarray Sum Equals K ,###Prefix Sum
        ///Given an array of integers nums and an integer k, return the total number of continuous subarrays whose sum equals to k.
        public int SubarraySum(int[] nums, int k)
        {
            Dictionary<int,int> dict=new Dictionary<int, int>();
            int ans = 0;
            int sum = 0;
            dict.Add(0, 1);
            for (int i = 0;i < nums.Length; i++)
            {
                sum +=nums[i];
                if (dict.ContainsKey(sum - k))
                {
                    ans += dict[sum - k];
                }

                if (dict.ContainsKey(sum))
                {
                    dict[sum]++;
                }
                else
                {
                    dict.Add(sum, 1);
                }
            }
            return ans;
        }
        /// 566. Reshape the Matrix
        public int[][] MatrixReshape(int[][] mat, int r, int c)
        {
            int row = mat.Length;
            int col = mat[0].Length;

            if (row * col != r * c)
                return mat;

            List<int[]> list = new List<int[]>();
            for (int i = 0; i < r; i++)
            {
                var list2 = new List<int>();
                for (int j = 0; j < c; j++)
                {
                    int index = i * c + j;
                    list2.Add(mat[index / col][index % col]);
                }
                list.Add(list2.ToArray());
            }

            return list.ToArray();
        }

        //567. Permutation in String

        public bool CheckInclusion(string s1, string s2)
        {
            if (s1.Length == 1)
                return s2.Contains(s1);

            int[] arr1 = new int[26];

            foreach (var c in s1)
            {
                arr1[c - 'a']++;
            }

            for (int i = 0; i <= s2.Length - s1.Length; i++)
            {
                if (arr1[s2[i] - 'a'] != 0)
                {
                    int[] arr2 = new int[26];

                    int j = 0;
                    while (j <= s1.Length - 1)
                    {
                        arr2[(s2[i + j] - 'a')]++;
                        j++;
                    }

                    if (IsTwoArrayEqual(arr1, arr2))
                        return true;
                }
            }

            return false;
        }

        public bool IsTwoArrayEqual(int[] arr1, int[] arr2)
        {
            if (arr2.Length != arr1.Length)
                return false;

            int i = 0;
            while (i < arr1.Length)
            {
                if (arr1[i] != arr2[i])
                    return false;
                i++;
            }
            return true;
        }

        ///572. Subtree of Another Tree
        ///there may same values of nodes in root
        public bool IsSubtree(TreeNode root, TreeNode subRoot)
        {
            var nodes = IsSubtree_Find(root, subRoot);
            if (nodes == null || nodes.Count == 0)
                return false;

            bool found = false;
            foreach (var node in nodes)
            {
                found = IsSubtree_Compare(node, subRoot);
                if (found)
                    return true;
            }

            return found;
        }

        public IList<TreeNode> IsSubtree_Find(TreeNode root, TreeNode subRoot)
        {
            List<TreeNode> ans = new List<TreeNode>();

            List<TreeNode> list = new List<TreeNode>() { root };
            while (list.Count > 0)
            {
                List<TreeNode> subs = new List<TreeNode>();
                foreach (var i in list)
                {
                    if (i.val == subRoot.val)
                        ans.Add(i);

                    if (i.left != null)
                        subs.Add(i.left);
                    if (i.right != null)
                        subs.Add(i.right);
                }

                list = subs;
            }

            return ans;
        }

        public bool IsSubtree_Compare(TreeNode root, TreeNode subRoot)
        {
            bool ans = true;
            List<TreeNode> list = new List<TreeNode>() { root };
            List<TreeNode> list2 = new List<TreeNode>() { subRoot };

            while (list.Count > 0 || list2.Count > 0)
            {
                if (list.Count != list2.Count)
                    return false;

                List<TreeNode> subs = new List<TreeNode>();
                List<TreeNode> subs2 = new List<TreeNode>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].val != list2[i].val)
                        return false;

                    if ((list[i].left == null && list2[i].left != null)
                        || (list[i].left != null && list2[i].left == null)
                        || (list[i].right == null && list2[i].right != null)
                        || (list[i].right != null && list2[i].right == null))
                        return false;

                    if (list[i].left != null)
                        subs.Add(list[i].left);
                    if (list[i].right != null)
                        subs.Add(list[i].right);

                    if (list2[i].left != null)
                        subs2.Add(list2[i].left);
                    if (list2[i].right != null)
                        subs2.Add(list2[i].right);
                }

                list = subs;
                list2 = subs2;
            }

            return ans;
        }

        ///583. Delete Operation for Two Strings
        ///return the minimum number of steps required to make word1 and word2 the same.
        ///In one step, you can delete exactly one character in either string.
        ///1 <= word1.length, word2.length <= 500, only lower case english
        public int MinDistance_583(string word1, string word2)
        {
            int[][] dp = new int[word1.Length + 1][];
            for (int i = 0; i < dp.Length; i++)
                dp[i] = new int[word2.Length + 1];

            //find how many matches
            for (int i = 1; i < dp.Length; i++)
                for (int j = 1; j < dp[0].Length; j++)
                    dp[i][j] = word1[i - 1] == word2[j - 1] ? dp[i - 1][j - 1] + 1 : Math.Max(dp[i - 1][j], dp[i][j - 1]);
            return word1.Length + word2.Length - 2 * dp[word1.Length][word2.Length];
        }
    }
}
