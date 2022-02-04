using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///509. Fibonacci Number
        ///0 <= n <= 30, F(0) = 0, F(1) = 1, F(n) = F(n - 1) + F(n - 2), for n > 1.
        public int Fib(int n)
        {
            if (n <= 1)
                return n;
            int dp = 0;
            int a1 = 0;
            int a2 = 1;
            int i = 2;
            while (i <= n)
            {
                dp = a1 + a2;
                a1 = a2;
                a2 = dp;
                i++;
            }
            return dp;
        }
        public int Fib_Recursion(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            return Fib_Recursion(n - 1) + Fib_Recursion(n - 2);
        }

        ///516. Longest Palindromic Subsequence
        ///Given a string s, find the longest palindromic subsequence's length in s.
        ///A subsequence is a sequence that can be derived from another sequence
        ///by deleting some or no elements without changing the order of the remaining elements.
        public int LongestPalindromeSubseq(string s)
        {
            int[,] dp = new int[s.Length,s.Length];

            for (int i = s.Length - 1; i >= 0; i--)
            {
                dp[i,i] = 1;
                for (int j = i + 1; j < s.Length; j++)
                {
                    if (s[i] == s[j])
                    {
                        dp[i,j] = dp[i + 1,j - 1] + 2;
                    }
                    else
                    {
                        dp[i,j] = Math.Max(dp[i + 1,j], dp[i,j - 1]);
                    }
                }
            }
            return dp[0,s.Length - 1];
        }

        /// 520. Detect Capital
        ///3 pattern: all UpCase, all LowerCase, only first char UpCase others lower
        ///Given a string word, return true if the usage of capitals in it is right.
        public bool DetectCapitalUse(string word)
        {
            if (word.Length == 1)
                return true;

            bool lastIsUpper = char.IsUpper(word[word.Length - 1]);

            bool ans = true;

            if (lastIsUpper)
            {
                for (int i = 0; i < word.Length - 1; i++)
                {
                    if (!char.IsUpper(word[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 1; i < word.Length - 1; i++)
                {
                    if (char.IsUpper(word[i]))
                    {
                        return false;
                    }
                }
            }
            return ans;

        }
        ///525. Contiguous Array
        ///Given a binary array nums, return the maximum length of a contiguous subarray with an equal number of 0 and 1.
        ///1 <= nums.length <= 105
        ///nums[i] is either 0 or 1.
        public int FindMaxLength(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int sum = 0;
            //assume 0 is -1, then find diff of index between two dict[sum]
            //dict[0] should inited as -1, because [0,1] will add dict[0]-index1,
            dict.Add(0, -1);
            int max = 0;
            for (int i=0; i<nums.Length; i++)
            {
                //assume 0 is -1, then find diff of index between two dict[sum]
                if (nums[i] == 0)
                {
                    sum -= 1;
                }
                else
                {
                    sum += 1;
                }

                if (dict.ContainsKey(sum))
                {
                    max = Math.Max(max, i - dict[sum]);
                }
                else
                {
                    dict.Add(sum, i);
                }
            }
            return max;
        }

        /// 542. 01 Matrix
        /// Given an m x n binary matrix mat, return the distance of the nearest 0 for each cell.
        /// The distance between two adjacent cells is 1.
        public int[][] UpdateMatrix(int[][] mat)
        {
            int rowLen = mat.Length;
            int colLen = mat[0].Length;

            int[][] result = new int[rowLen][];
            for (int i = 0; i < rowLen; i++)
            {
                result[i] = new int[colLen];
            }

            //init every cell as max value, assuming the only 0 at furthest corner cell
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    result[i][j] = Math.Max(i, rowLen - i - 1) + Math.Max(j, colLen - j - 1);
                }
            }

            //left-top to right-bottom
            int MAX_DISTANCE = (rowLen - 1) + (colLen - 1);

            //dp value
            int distance;

            //loop rows first, forward sequence then backward
            for (int r = 0; r < rowLen; r++)
            {
                if (colLen == 1)
                    break;

                distance = MAX_DISTANCE;
                for (int c = 0; c < colLen; c++)
                {
                    if (mat[r][c] == 0)
                    {
                        distance = 0;
                    }
                    else
                    {
                        if (distance != MAX_DISTANCE)
                        {
                            distance++;
                        }
                    }
                    result[r][c] = Math.Min(result[r][c], distance);
                }

                distance = MAX_DISTANCE;
                for (int c = colLen - 1; c >= 0; c--)
                {
                    if (mat[r][c] == 0)
                    {
                        distance = 0;
                    }
                    else
                    {
                        if (distance != MAX_DISTANCE)
                        {
                            distance++;
                        }
                    }
                    result[r][c] = Math.Min(result[r][c], distance);
                }
            }

            //then loop cols
            for (int c = 0; c < colLen; c++)
            {
                if (rowLen == 1)
                    break;

                distance = MAX_DISTANCE;
                for (int r = 0; r < rowLen; r++)
                {
                    if (mat[r][c] == 0)
                    {
                        distance = 0;
                    }
                    else
                    {
                        if (distance != MAX_DISTANCE)
                        {
                            distance++;
                        }
                    }
                    result[r][c] = Math.Min(result[r][c], distance);
                }

                distance = MAX_DISTANCE;
                for (int r = rowLen - 1; r >= 0; r--)
                {
                    if (mat[r][c] == 0)
                    {
                        distance = 0;
                    }
                    else
                    {
                        if (distance != MAX_DISTANCE)
                        {
                            distance++;
                        }
                    }
                    result[r][c] = Math.Min(result[r][c], distance);
                }
            }

            if (rowLen == 1 || colLen == 1)
                return result;

            //every cell value is min of itself and all adjacents(left,right,top,bottom)
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (j > 0)
                    {
                        result[i][j] = Math.Min(result[i][j], result[i][j - 1] + 1);
                    }

                    if (j < colLen - 1)
                    {
                        result[i][j] = Math.Min(result[i][j], result[i][j + 1] + 1);
                    }

                    if (i > 0)
                    {
                        result[i][j] = Math.Min(result[i][j], result[i - 1][j] + 1);
                    }

                    if (i < rowLen - 1)
                    {
                        result[i][j] = Math.Min(result[i][j], result[i + 1][j] + 1);
                    }
                }
            }

            return result;
        }

        public int[] UpdateMatrixRow(int[] data)
        {
            if (data == null)
                return null;
            if (data.Length == 0)
                return data;

            int[] result = new int[data.Length];

            int left = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 0)
                {
                    left = 0;
                }
                else
                {
                    left++;
                }
                result[i] = left;
            }

            int right = 0;
            for (int i = data.Length - 1; i >= 0; i--)
            {
                if (data[i] == 0)
                {
                    right = 0;
                }
                else
                {
                    right++;
                }
                result[i] = Math.Min(result[i], right);
            }

            return result;
        }

        ///547. Number of Provinces
        ///BFS/DFS, same to 200
        public int FindCircleNum(int[][] isConnected)
        {
            int[] arr = new int[isConnected.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 1;
            }
            int ans = 0;
            for (int i = 0; i < isConnected.Length; i++)
            {
                if (arr[i] == 0)
                {
                    continue;
                }
                else
                {
                    ans++;
                    arr[i] = 0;
                }

                for (int j = 0; j < isConnected[i].Length; j++)
                {
                    if (i != j && isConnected[i][j] == 1)
                    {
                        FindCircleNum_RemoveAllConnected(isConnected, arr, i, j);
                    }
                }
            }
            return ans;
        }

        public void FindCircleNum_RemoveAllConnected(int[][] isConnected, int[] arr, int r, int c)
        {
            if (arr[c] == 0)
                return;

            arr[c] = 0;

            for (int j = 0; j < isConnected[c].Length; j++)
            {
                if (c != j && isConnected[c][j] == 1)
                {
                    FindCircleNum_RemoveAllConnected(isConnected, arr, c, j);
                }
            }
        }
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

        ///560. Subarray Sum Equals K, O(n^2)
        ///Given an array of integers nums and an integer k, return the total number of continuous subarrays whose sum equals to k.
        public int SubarraySum(int[] nums, int k)
        {
            int ans = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                int sum = 0;
                int j = i;
                while (j >= 0)
                {
                    sum += nums[j];
                    if (sum == k)
                        ans++;
                    j--;
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
            for(int i = 0; i < dp.Length; i++)
                dp[i] = new int[word2.Length + 1];

            //find how many matches
            for (int i = 1; i < dp.Length; i++)
                for (int j = 1; j < dp[0].Length; j++)
                    dp[i][j] = word1[i - 1] == word2[j - 1] ? dp[i - 1][j - 1] + 1 : Math.Max(dp[i - 1][j], dp[i][j - 1]);
            return word1.Length + word2.Length - 2 * dp[word1.Length][word2.Length];
        }


    }
}