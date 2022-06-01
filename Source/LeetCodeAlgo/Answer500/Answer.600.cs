using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///605. Can Place Flowers
        ///flowers cannot be planted in adjacent plots.
        ///[1,0,0,0,1], 0-empty, 1-planted, n=new flowers can be plant?
        public bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            if (n == 0)
                return true;

            for (int i = 0; i < flowerbed.Length; i++)
            {
                if (isMaxFlowersExceed(i, flowerbed.Length - 1, n))
                    return false;

                if (flowerbed[i] == 1 || (i > 0 && flowerbed[i - 1] == 1) || (i < flowerbed.Length - 1 && flowerbed[i + 1] == 1))
                {
                    continue;
                }
                else
                {
                    flowerbed[i] = 1;
                    n--;
                    if (n == 0)
                        return true;
                }
            }

            return false;
        }

        public bool isMaxFlowersExceed(int start, int end, int n)
        {
            int count = (end - start + 1);
            return count % 2 == 1 ? n > count / 2 + 1 : n > count / 2;
        }

        ///606. Construct String from Binary Tree, #BTree
        ///Given the root of a binary tree, construct a string consisting of parenthesis and
        ///integers from a binary tree with the preorder traversal way, and return it.
        ///Omit all the empty parenthesis pairs that do not affect the one-to-one mapping
        ///relationship between the string and the original binary tree.
        public string Tree2str(TreeNode root)
        {
            if (root == null) return String.Empty;
            string str = root.val.ToString();
            if (root.left == null && root.right == null)
            {
                return str;
            }
            else if (root.left == null)
            {
                return str + $"()({Tree2str(root.right)})";
            }
            else if (root.right == null)
            {
                return str + $"({Tree2str(root.left)})";
            }
            else
            {
                return str + $"({Tree2str(root.left)})" + $"({Tree2str(root.right)})";
            }
        }

        ///611. Valid Triangle Number, #Two Pointers, #Binary Search
        ///return the number of triplets chosen from the array that can make triangles.
        public int TriangleNumber(int[] nums)
        {
            Array.Sort(nums);
            int count = 0, n = nums.Length;
            for (int i = n - 1; i >= 2; i--)
            {
                int l = 0, r = i - 1;
                while (l < r)
                {
                    if (nums[l] + nums[r] > nums[i])
                    {
                        //all l in [l,r) will valid
                        count += r - l;
                        r--;
                    }
                    else l++;
                }
            }
            return count;
        }

        public int TriangleNumber_BinarySearch(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums);
            int count = 0;
            for (int i = 0; i < n - 2; i++)
            {
                for (int j = i + 1; j < n - 1; j++)
                {
                    int sum = nums[i] + nums[j];
                    // find the first number < sum, return the index
                    int index = TriangleNumber_BinarySearch(j + 1, sum, nums);
                    if (index != -1)
                    {
                        count += index - j;
                    }
                }
            }
            return count;
        }
        private int TriangleNumber_BinarySearch(int start,int target,int[] nums)
        {
            if (nums[start] >= target)
                return -1;

            if (nums[nums.Length - 1] < target)
                return nums.Length - 1;

            int right = nums.Length - 1;
            int left = start;
            while (left < right)
            {
                int mid = (left + right +1) / 2 ;//plus 1 will make mid as the right half center side
                if (nums[mid] < target)
                {
                    left = mid;
                }
                else
                {
                    right = mid-1;
                }
            }
            return left;
        }
        //617. Merge Two Binary Trees

        public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null)
                return null;

            var result = new TreeNode
            {
                val = (root1 != null ? root1.val : 0) + (root2 != null ? root2.val : 0)
            };

            if (root1 == null)
            {
                result.left = root2.left;
                result.right = root2.right;
            }
            else if (root2 == null)
            {
                result.left = root1.left;
                result.right = root1.right;
            }
            else
            {
                result.left = MergeTrees(root1.left, root2.left);
                result.right = MergeTrees(root1.right, root2.right);
            }

            return result;
        }

        ///621. Task Scheduler
        ///1 <= task.length <= 10^4, tasks[i] is upper-case English letter.
        public int LeastInterval(char[] tasks, int n)
        {
            if (n <= 0) { return tasks.Length; }
            int[] arr = new int[26];
            foreach (char t in tasks)
            {
                arr[t - 'A']++;
            }
            Array.Sort(arr);
            // count is the number of appending numbers in last "round"
            int count = 0;
            for (int i = 25; i >= 0; i--)
            {
                if (arr[i] == arr[25])
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return Math.Max(tasks.Length, (arr[25] - 1) * (n + 1) + count);
        }

        ///622. Design Circular Queue, see MyCircularQueue

        ///628. Maximum Product of Three Numbers
        //find three numbers whose product is maximum and return the maximum product.
        public int MaximumProduct(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums);
            return Math.Max(nums[0] * nums[1] * nums[n - 1], nums[n - 3] * nums[n - 2] * nums[n - 1]);
        }
        /// 633. Sum of Square Numbers
        ///Given a non-negative integer c, decide whether there're two integers a and b such that a2 + b2 = c.
        public bool JudgeSquareSum(int c)
        {
            HashSet<int> set = new HashSet<int>();
            for (long i = 0; i * i <= c; i++)
                set.Add((int)(i * i));

            foreach (var n in set)
                if (set.Contains(c - n)) return true;
            return false;
        }

        public bool JudgeSquareSum_TwoPointers(int c)
        {
            long left = 0;
            long right = (long)(Math.Sqrt(c));
            while (left <= right)
            {
                long cur = left * left + right * right;
                if (cur == c) return true;
                else if (cur < c)
                    left += 1;
                else
                    right -= 1;
            }

            return false;
        }

        /// 637. Average of Levels in Binary Tree, #BTree
        ///Given the root of a binary tree, return the average value of the nodes
        ///on each level in the form of an array. Answers within 10-5 of the actual answer will be accepted.
        public IList<double> AverageOfLevels(TreeNode root)
        {
            var ans = new List<double>();
            if (root == null) return ans;
            var nodes = new List<TreeNode>() { root };
            while (nodes.Count > 0)
            {
                var next = new List<TreeNode>();
                var list = new List<long>();
                foreach (TreeNode node in nodes)
                {
                    list.Add(node.val);
                    if (node.left != null) next.Add(node.left);
                    if (node.right != null) next.Add(node.right);
                }
                nodes = next;
                ans.Add(list.Sum() * 1.0 / list.Count);
            }
            return ans;
        }

        ///643. Maximum Average Subarray I
        public double FindMaxAverage(int[] nums, int k)
        {
            int sum=nums.Take(k).Sum();
            int max = sum;
            for (int i = k; i < nums.Length; i++)
            {
                sum += nums[i] - nums[i - k];
                max = Math.Max(max, sum);
            }
            return max * 1.0 / k;
        }
        /// 645. Set Mismatch
        ///[1,n] Find the number that occurs twice and the number that is missing and return them in the form of an array.
        public int[] FindErrorNums(int[] nums)
        {
            int[] arr = new int[nums.Length + 1];
            int sum = 0;
            int twice = 0;
            foreach (var n in nums)
            {
                sum += n;
                arr[n]++;
                if (arr[n] == 2) twice = n;
            }
            int miss = twice - (sum - (nums.Length + 1) * nums.Length / 2);
            return new int[] { twice, miss };
        }

        ///647. Palindromic Substrings
        ///Given a string s, return the number of palindromic substrings in it.
        ///A string is a palindrome when it reads the same backward as forward.
        ///A substring is a contiguous sequence of characters within the string.
        public int CountSubstrings(string s)
        {
            int res = 0;
            for(int i = 0; i < s.Length; i++)
            {
                res += CountSubstrings(s, i, i);
                res += CountSubstrings(s, i, i+1);
            }
            return res;
        }

        private int CountSubstrings(string s, int i, int j)
        {
            int count = 0;
            while(i>=0&&j<s.Length && s[i--] == s[j++])
            {
                count++;
            }
            return count;
        }


        /// 648. Replace Words
        ///Return the sentence after the replacement.
        public string ReplaceWords(IList<string> dictionary, string sentence)
        {
            var words = dictionary.OrderBy(x => x).ThenBy(x => x.Length).ToList();
            var list = sentence.Split(' ').Where(x => x.Length > 0).ToList();
            var res = new List<string>();
            foreach (var i in list)
            {
                bool match = false;
                foreach (var word in words)
                {
                    if (i.StartsWith(word))
                    {
                        match = true;
                        res.Add(word);
                        break;
                    }
                }
                if (!match)
                    res.Add(i);
            }
            return string.Join(" ", res);
        }
    }
}