using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
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

        /// 611
        public int TriangleNumber(int[] nums)
        {
            Array.Sort(nums);
            int count = 0;
            int i, j, k;
            for (i = 0; i < nums.Length - 2; i++)
            {
                for (j = i + 1; j < nums.Length - 1; j++)
                {
                    for (k = j + 1; k < nums.Length; k++)
                    {
                        if (IsTriangle(nums[i], nums[j], nums[k]))
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return count;
        }

        public bool IsTriangle(int num1, int num2, int num3)
        {
            return num3 < num1 + num2;
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
            int ans = tasks.Length;
            if (n == 0) return ans;

            int[] arr=new int[26];
            foreach (var t in tasks)
                arr[t - 'A']++;



            return ans;

        }
        /// 653. Two Sum IV - Input is a BST
        ///return true if there exist two elements in the BST such that sum = target.

        public bool FindTarget(TreeNode root, int k)
        {
            return FindTarget(root, k, new List<int>());
        }

        public bool FindTarget(TreeNode root, int k, IList<int> list)
        {
            if (root == null)
                return false;

            if (list.Contains(root.val))
                return true;

            list.Add(k - root.val);

            return FindTarget(root.left, k, list) || FindTarget(root.right, k, list);
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
        /// 695. Max Area of Island

        public int MaxAreaOfIsland(int[][] grid)
        {
            int row = grid.Length;
            int col = grid[0].Length;

            List<List<int>> list = new List<List<int>>();
            for (int i = 0; i < row; i++)
            {
                List<int> list2 = new List<int>();
                for (int j = 0; j < col; j++)
                {
                    list2.Add(0);
                }
                list.Add(list2);
            }

            int max = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (grid[i][j] == 1 && list[i][j] == 0)
                    {
                        Queue<int[]> queue = new Queue<int[]>();

                        list[i][j] = 1;
                        int count = 1;
                        queue.Enqueue(new int[] { i, j });

                        while (queue.Count > 0)
                        {
                            var point = queue.Dequeue();

                            if ((point[0] > 0) && grid[point[0] - 1][point[1]] == 1 && list[point[0] - 1][point[1]] == 0)
                            {
                                list[point[0] - 1][point[1]] = 1;
                                queue.Enqueue(new int[] { point[0] - 1, point[1] });
                                count++;
                            }

                            if ((point[0] < row - 1) && (grid[point[0] + 1][point[1]] == 1) && list[point[0] + 1][point[1]] == 0)
                            {
                                list[point[0] + 1][point[1]] = 1;
                                queue.Enqueue(new int[] { point[0] + 1, point[1] });
                                count++;
                            }

                            if ((point[1] > 0) && (grid[point[0]][point[1] - 1] == 1) && list[point[0]][point[1] - 1] == 0)
                            {
                                list[point[0]][point[1] - 1] = 1;
                                queue.Enqueue(new int[] { point[0], point[1] - 1 });
                                count++;
                            }

                            if ((point[1] < col - 1) && (grid[point[0]][point[1] + 1] == 1) && list[point[0]][point[1] + 1] == 0)
                            {
                                list[point[0]][point[1] + 1] = 1;
                                queue.Enqueue(new int[] { point[0], point[1] + 1 });
                                count++;
                            }
                        }

                        max = Math.Max(max, count);
                    }
                }
            }

            return max;
        }

        //697
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
    }
}