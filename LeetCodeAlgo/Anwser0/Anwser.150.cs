using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///152. Maximum Product Subarray
        ///Given an integer array nums, find a contiguous non-empty subarray within the array
        ///that has the largest product, and return the product.
        ///-10 <= nums[i] <= 10, 1 <= nums.length <= 2 * 10^4
        public int MaxProduct_152(int[] nums)
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

        /// 153. Find Minimum in Rotated Sorted Array
        /// use binary search
        public int FindMin(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];

            int start = 0;
            int end = nums.Length - 1;

            if (nums[start] < nums[end])
                return nums[start];

            int mid = (end - start) / 2 + start;

            while (start <= end)
            {
                if (start == end)
                    return nums[start];

                if (start + 1 == end)
                    return Math.Min(nums[start], nums[end]);

                if (nums[end] < nums[mid])
                {
                    start = mid;
                    mid = (end - start) / 2 + start;
                }
                else if (nums[mid] < nums[start])
                {
                    end = mid;
                    mid = (end - start) / 2 + start;
                }
            }

            return nums[start];
        }

        ///155. Min Stack , see MinStack

        ///160. Intersection of Two Linked Lists, #Two Pointers
        ///return the node at which the two lists intersect. If not, return null.
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            var node1 = headA;
            var node2 = headB;

            int len1 = 0;
            int len2 = 0;
            while (node1 != null)
            {
                node1 = node1.next;
                len1++;
            }
            while (node2 != null)
            {
                node2 = node2.next;
                len2++;
            }

            node1 = headA;
            node2 = headB;

            if (len1 > len2)
            {
                while (len1 > len2)
                {
                    node1 = node1.next;
                    len1--;
                }
            }
            else if (len1 < len2)
            {
                while (len1 < len2)
                {
                    node2 = node2.next;
                    len2--;
                }
            }

            while (node1 != node2)
            {
                node1 = node1.next;
                node2 = node2.next;
            }

            return node1;
        }

        /// 162. Find Peak Element
        ///return the index to any of the peaks which greater than its neighbors.
        ///nums[-1] = nums[n] = int.Min
        public int FindPeakElement(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                bool left = i == 0 || nums[i] > nums[i - 1];
                bool right = i == nums.Length - 1 || nums[i] > nums[i + 1];
                if (left && right)
                {
                    return i;
                }
                else if (right)
                {
                    i++;
                }
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
            int half = nums.Length % 2 == 1 ? nums.Length / 2 + 1 : nums.Length / 2;
            for (int i = 0; i < nums.Length; i++)
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
            var major = dict.Where(x => x.Value >= half).ToList();
            if (major == null)
                return -1;
            return major[0].Key;
        }

        ///171. Excel Sheet Column Number
        ///return its corresponding column number.
        public int TitleToNumber(string columnTitle)
        {
            int ans = 0;
            int m = 1;
            for(int i = columnTitle.Length - 1; i >= 0; i--)
            {
                ans += m * (columnTitle[i] - 'A' + 1);
                m *= 26;
            }
            return ans;
        }

        /// 172. Factorial Trailing Zeroes
        ///Given an integer n, return the number of trailing zeroes in n!.
        ///0 <= n <= 10^4
        public int TrailingZeroes(int n)
        {
            //Step 1: 10 = 2*5, so every xxx5 will find a xxx2, that xxx5*xxx2 will add 1 tail zero
            //Step 2: 100 = 2*5 * 2*5 = 4*25, so every xxx25 will find a xxx4 that add 2 tail zeros, but one was already counted in step 1
            //...
            //Step N: 10^n = 2^n * 5^n, so every xxx5^n will find a xxx2^n that add n tail zeros, but n-1 were counted in step n-1
            int ans = 0;
            while (n > 0)
            {
                ans += n / 5;
                n /= 5;
            }
            return ans;
        }
        /// 173. Binary Search Tree Iterator, see BSTIterator

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

        ///187. Repeated DNA Sequences
        ///return all the 10-letter-long sequences (substrings) that occur more than once
        public IList<string> FindRepeatedDnaSequences(string s)
        {
            List<string> ans = new List<string>();

            if (string.IsNullOrEmpty(s)) return ans;
            if (s.Length <= 10) return ans;

            Dictionary<string, int> dict = new Dictionary<string, int>();

            for (int i = 0; i < s.Length - 10 + 1; i++)
            {
                string str = s.Substring(i, 10);
                if (dict.ContainsKey(str))
                {
                    dict[str]++;
                }
                else
                {
                    dict.Add(str, 1);
                }
            }

            ans = dict.Where(o => o.Value > 1).Select(o => o.Key).ToList();

            return ans;
        }

        ///189. Rotate Array
        ///Given an array, rotate the array to the right by k steps, where k is non-negative.
        ///Input: nums = [1,2,3,4,5,6,7], k = 3, Output: [5,6,7,1,2,3,4]
        public void Rotate(int[] nums, int k)
        {
            k = k % nums.Length;
            if (k == 0) return;
            int[] temp = new int[k];
            for (int i = 0; i < k; i++)
                temp[i] = nums[nums.Length - k + i];
            for (int i = nums.Length - 1; i > k - 1; i--)
                nums[i] = nums[i - k];
            for (int i = 0; i < k; i++)
                nums[i] = temp[i];
        }

        ///190. Reverse Bits
        ///Reverse bits of a given 32 bits unsigned integer.
        public uint reverseBits(uint n)
        {
            if (n == 0) return 0;
            uint result = 0;
            for (int i = 0; i < 32; i++)
            {
                result <<= 1;
                if ((n & 1) == 1)
                    result++;
                n >>= 1;
            }
            return result;
        }

        /// 191. Number of 1 Bits
        /// eg. 5=101, return count of 1 = 2;
        public int HammingWeight(uint n)
        {
            uint count = 0;
            while (n > 0)
            {
                count += n & 1;
                n >>= 1;
            }
            return (int)count;
        }

        ///198. House Robber, cannot rob adjacent houses
        ///The number of nodes in the tree is in the range [0, 100].
        public int Rob_198(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];
            int[] dp = new int[nums.Length];
            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);
            for (int i = 2; i < nums.Length; i++)
                dp[i] = Math.Max(nums[i] + dp[i - 2], dp[i - 1]);
            return dp[nums.Length - 1];
        }

        ///199. Binary Tree Right Side View
        ///return the right side values of the nodes you can see ordered from top to bottom.
        public IList<int> RightSideView(TreeNode root)
        {
            var ans = new List<int>();
            if (root == null)
                return ans;
            List<TreeNode> nodes = new List<TreeNode>() { root };
            while (nodes.Count > 0)
            {
                ans.Add(nodes.Last().val);
                List<TreeNode> nexts = new List<TreeNode>();
                foreach (var node in nodes)
                {
                    if (node.left != null)
                        nexts.Add(node.left);
                    if (node.right != null)
                        nexts.Add(node.right);
                }
                nodes = nexts;
            }
            return ans;
        }
    }
}