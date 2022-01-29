using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///700. Search in a Binary Search Tree
        ///You are given the root of a binary search tree (BST) and an integer val.
        ///Find the node in the BST equals val and return the subtree rooted with that node.
        ///If such a node does not exist, return null.
        public TreeNode SearchBST(TreeNode root, int val)
        {
            if (root == null)
                return null;

            while (root != null)
            {
                if (val == root.val)
                    return root;

                root = val > root.val ? root.right : root.left;
            }

            return root;
        }

        ///701. Insert into a Binary Search Tree
        ///You are given the root node of a binary search tree (BST) and a value to insert into the tree.
        ///Return the root node of the BST after the insertion.
        ///It is guaranteed that the new value does not exist in the original BST.
        ///All the values Node.val are unique.
        ///It's guaranteed that val does not exist in the original BST.
        public TreeNode InsertIntoBST(TreeNode root, int val)
        {
            var add = new TreeNode(val);
            if (root == null)
                return add;

            var node = root;

            while (node != null)
            {
                if (val > node.val)
                {
                    if (node.right == null)
                    {
                        node.right = add;
                        break;
                    }
                    else
                    {
                        node = node.right;
                    }
                }
                else
                {
                    if (node.left == null)
                    {
                        node.left = add;
                        break;
                    }
                    else
                    {
                        node = node.left;
                    }
                }
            }

            return root;
        }

        /// 704. Binary Search  O(log n)
        ///Given an array of integers nums which is sorted in ascending order,
        ///and an integer target, write a function to search target in nums.
        ///If target exists, then return its index.Otherwise, return -1.
        ///All the integers in nums are unique.
        public int Search_704(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1 && nums[0] == target)
                return 0;

            int i = nums.Length / 2;
            int low = 0;
            int high = nums.Length - 1;

            int result = -1;

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

            return result;
        }

        ///706. Design HashMap, see MyHashMap

        ///707. Design Linked List, see MyLinkedList


        ///717. 1-bit and 2-bit Characters
        ///The first character can be represented by one bit 0.
        ///The second character can be represented by two bits (10 or 11).
        ///Given a binary array bits that ends with 0, return true if the last character must be a one-bit character.
        ///1 <= bits.length <= 1000
        public bool IsOneBitCharacter(int[] bits)
        {
            bool odd = true;
            for(int i = bits.Length - 2; i >= 0; i--)
            {
                if (odd && bits[i] == 0)
                    return true;
                if (!odd && bits[i] == 0)
                    return false;
                odd = !odd;
            }
            return odd;
        }
        /// 713. Subarray Product Less Than K
        /// Sliding-Window
        public int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            if (k <= 1)
                return 0;

            int ans = 0;
            int product = 1;
            int start = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                product *= nums[i];
                while (product >= k && start <= i && start >= 0)
                {
                    product = product / nums[start];
                    start++;
                }

                if (product < k)
                {
                    ans += (i - start + 1);
                }
            }

            return ans;
        }

        /// 728
        public IList<int> SelfDividingNumbers(int left, int right)
        {
            List<int> result = new List<int>();
            for (int i = left; i <= right; i++)
            {
                bool pass = false;
                int temp = i;
                while (temp > 0)
                {
                    int mod = temp % 10;
                    if (mod == 0 || i % mod != 0)
                    {
                        pass = true;
                        break;
                    }
                    temp /= 10;
                }
                if (pass) continue;

                result.Add(i);
            }

            return result;
        }

        public bool IsSelfDivided(int num)
        {
            List<int> divid = new List<int>();
            int full = num;
            while (full > 0)
            {
                divid.Add(full % 10);
                full /= 10;
            }

            foreach (var i in divid)
            {
                if (i == 0 || num % i != 0) return false;
            }
            return true;
        }

        //733. Flood Fill

        public int[][] FloodFill(int[][] image, int sr, int sc, int newColor)
        {
            int color = image[sr][sc];
            if (color == newColor)
                return image;
            int row = image.Length;
            int col = image[0].Length;

            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { sr, sc });
            image[sr][sc] = newColor;
            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                if ((point[0] > 0) && (image[point[0] - 1][point[1]] == color))
                {
                    image[point[0] - 1][point[1]] = newColor;
                    queue.Enqueue(new int[] { point[0] - 1, point[1] });
                }

                if ((point[0] < row - 1) && (image[point[0] + 1][point[1]] == color))
                {
                    image[point[0] + 1][point[1]] = newColor;
                    queue.Enqueue(new int[] { point[0] + 1, point[1] });
                }

                if ((point[1] > 0) && (image[point[0]][point[1] - 1] == color))
                {
                    image[point[0]][point[1] - 1] = newColor;
                    queue.Enqueue(new int[] { point[0], point[1] - 1 });
                }

                if ((point[1] < col - 1) && (image[point[0]][point[1] + 1] == color))
                {
                    image[point[0]][point[1] + 1] = newColor;
                    queue.Enqueue(new int[] { point[0], point[1] + 1 });
                }
            }

            return image;
        }

        ///740. Delete and Earn
        ///eat nums[i] will earn all nums[i] but delete nums[i]+1 and nums[i]-1, return the max of Earn
        ///1 <= nums[i] <= 10^4
        public int DeleteAndEarn(int[] nums)
        {
            int[] arr = new int[10001];
            int[] dp = new int[10001];
            int max = 0;
            int min = 10000;
            for (int i = 0; i < nums.Length; i++)
            {
                arr[nums[i]]++;
                max = Math.Max(max, nums[i]);
                min = Math.Min(min, nums[i]);
            }
            if (max == min)
                return arr[max] * max;

            int j = min;
            dp[j] = arr[j] * j;
            dp[j + 1] = Math.Max(dp[j], arr[j + 1] * (j + 1));
            j += 2;
            while (j <= max)
            {
                dp[j] = Math.Max(dp[j - 1], dp[j - 2] + arr[j] * (j));
                j++;
            }
            return dp[max];
        }

        ///744. Find Smallest Letter Greater Than Target
        public char NextGreatestLetter(char[] letters, char target)
        {
            foreach (char letter in letters)
            {
                if(letter > target)
                    return letter;
            }
            return letters.Last();
        }
        /// 746. Min Cost Climbing Stairs
        ///cost[i] is the cost of ith step on a staircase.
        ///You can either start from the step with index 0, or the step with index 1
        ///target stair is N-th, out of arr
        public int MinCostClimbingStairs(int[] cost)
        {
            int[] dp = new int[cost.Length];

            dp[0] = cost[0];
            dp[1] = cost[1];

            for (int i = 2; i < cost.Length; i++)
            {
                //every point cost must to last1 or last2
                dp[i] = cost[i] + Math.Min(dp[i - 2], dp[i - 1]);
            }

            //every solution must to last1 or last2
            return Math.Min(dp[cost.Length - 1], dp[cost.Length - 2]);
        }

        ///763. Partition Labels
        ///s consists of lowercase English letters.
        public IList<int> PartitionLabels(string s)
        {
            if (s.Length == 1)
                return new List<int>() { 1 };

            IList<int> ans = new List<int>();

            Dictionary<char, List<int>> dict = new Dictionary<char, List<int>>();

            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]))
                {
                    dict[s[i]].Add(i);
                }
                else
                {
                    dict.Add(s[i], new List<int>() { i });
                }
            }

            List<char> list = new List<char>();
            int len = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (!list.Contains(s[i]))
                    list.Add(s[i]);

                bool canPartition = !list.Any(c => dict[c].Any(x => x > i));

                if (canPartition)
                {
                    list.Clear();

                    int len2 = i + 1;
                    ans.Add(len2 - len);
                    len = len2;
                }

            }

            return ans;
        }
        /// 784. Letter Case Permutation
        ///ref 77 Combines()
        ///Given a string s, you can transform every letter individually to be lowercase or uppercase to create another string.
        ///Return a list of all possible strings we could create.Return the output in any order.
        /// 1 <= s.length <= 12
        /// s consists of lowercase English letters, uppercase English letters, and digits.

        public IList<string> LetterCasePermutation(string s)
        {
            var result = new List<string>();

            var carrIndexs = new List<int>();

            var carr = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsLetter(carr[i]))
                {
                    carrIndexs.Add(i);
                }
            }

            if (carrIndexs.Count == 0)
            {
                result.Add(s);
            }
            else
            {
                IList<IList<int>> combines = new List<IList<int>>
                {
                    new List<int>()
                };
                for (int i = 1; i <= carrIndexs.Count; i++)
                {
                    var comb2 = Combine(carrIndexs.Count, i);
                    foreach (var j in comb2)
                        combines.Add(j);
                }

                foreach (var c in combines)
                {
                    var arr = s.ToCharArray();
                    for (int i = 0; i < carrIndexs.Count; i++)
                    {
                        if (c.Contains(i + 1))
                        {
                            arr[carrIndexs[i]] = char.ToUpper(arr[carrIndexs[i]]);
                        }
                        else
                        {
                            arr[carrIndexs[i]] = char.ToLower(arr[carrIndexs[i]]);
                        }
                    }

                    result.Add(string.Join("", arr));
                }

                return result;
            }
            return result;
        }

        ///797. All Paths From Source to Target
        ///only 0 to N-1
        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            var ans = new List<IList<int>>();

            int len = graph.Length;

            //only 0 to N-1
            for (int i = 0; i < 1; i++)
            {
                var curr = new List<IList<int>>();
                var all = new List<IList<int>>();

                var list = new List<int>() { i };

                curr.Add(list);

                while (curr.Count > 0)
                {
                    var sub = new List<IList<int>>();

                    foreach (var item in curr)
                    {
                        var nexts = AllPathsSourceTarget_Add(graph, item);
                        if (nexts.Count > 0)
                        {
                            foreach (var next in nexts)
                            {
                                if (next.Last() == len - 1)
                                {
                                    all.Add(next);
                                }
                                else
                                {
                                    sub.Add(next);
                                }
                            }
                        }
                    }

                    curr = sub;
                }


                if (all.Count > 0)
                {
                    foreach (var j in all)
                    {
                        if (j.Last() == len - 1)
                            ans.Add(j);
                    }
                }
            }

            return ans;
        }

        public IList<IList<int>> AllPathsSourceTarget_Add(int[][] graph, IList<int> list)
        {
            var ans = new List<IList<int>>();

            if (list.Count > 0 && graph[list.Last()].Length > 0)
            {
                foreach (var j in graph[list.Last()])
                {
                    var sub = new List<int>(list)
                    {
                        j
                    };
                    ans.Add(sub);
                }
            }

            return ans;
        }

    }
}