using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///150. Evaluate Reverse Polish Notation
        public int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();
            int res = 0;
            foreach (var str in tokens)
            {
                if (str == "+" || str == "-" || str == "*" || str == "/")
                {
                    var b = stack.Pop();
                    var a = stack.Pop();
                    if (str == "+") res = a + b;
                    else if (str == "-") res = a - b;
                    else if (str == "*") res = a * b;
                    else if (str == "/") res = a / b;
                    stack.Push(res);
                }
                else
                {
                    stack.Push(int.Parse(str));
                }
            }
            return stack.Count > 0 ? stack.Pop() :res;
        }


        ///151. Reverse Words in a String
        //Given an input string s, reverse the order of the words.
        public string ReverseWords(string s)
        {
            return string.Join(' ', s.Split(' ').Where(x => x.Length > 0).Reverse());
        }

        /// 152. Maximum Product Subarray, #DP
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

        /// 153. Find Minimum in Rotated Sorted Array, #Binary Search
        public int FindMin(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] < nums[right])
                    right = mid;
                else
                    left = mid + 1;
            }
            return nums[left];
        }

        ///154. Find Minimum in Rotated Sorted Array II, #Binary Search
        //may rotate and contain duplicates, return the min
        public int FindMin_154(int[] nums)
        {
            int n = nums.Length;
            int left = 0;
            int right = n - 1;
            while (left < right)
            {
                int mid = (left + right) / 2;
                int prev = mid == 0 ? n - 1 : mid - 1;
                int next = mid == n - 1 ? 0 : mid + 1;
                if (nums[mid] <= nums[next] && nums[mid] < nums[prev])
                    return nums[mid];
                else
                {
                    if (nums[mid]> nums[right])//rotated, in range [mid+1,right]
                    {
                        left = mid + 1;
                    }
                    else if(nums[mid]< nums[right])
                    {
                        right = mid;//must in range [left, mid]
                    }
                    else// nums[mid] == nums[right]
                    {
                        right--;//duplicate, move to the head
                    }
                }
            }
            return nums[left];
        }
        /// 155. Min Stack , see MinStack

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

        /// 162. Find Peak Element, #Binary Search
        //A peak element is an element that is strictly greater than its neighbors.
        //Given an integer array nums, find a peak element, and return its index any of the peaks..
        //You may imagine that nums[-1] = nums[n] = -∞. nums[i] != nums[i + 1] for all valid i.
        public int FindPeakElement(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (left < right)
            {
                int mid = (left + right) / 2;
                //there is no nums[mid] == nums[mid+1]
                if (nums[mid] < nums[mid + 1])
                {
                    //index n-1 must > n(invalid),so must exist in [mid+1,n-1]
                    left = mid + 1;

                }
                else if(nums[mid] > nums[mid + 1])
                {
                    //index 0 must > -1(invalid), so must exist in [0,mid]
                    right = mid;
                }
            }
            return left;
        }

        ///164. Maximum Gap, #Bucket Sort
        //return the maximum gap between two successive elements in sorted form.
        public int MaximumGap(int[] nums)
        {
            HashSet<int> set = new HashSet<int>();
            int min = int.MaxValue;
            int max = int.MinValue;
            foreach (var i in nums)
            {
                set.Add(i);
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }
            int n = set.Count();
            if (max - min + 1 <= n)
                return max == min ? 0 : 1;
            //bucket width, n bucket must cover [min, max] range
            int width = (max - min) / (n - 1);
            if ((max - min) % (n - 1) != 0)
                width++;
            int[] ceils = new int[n];
            int[] floors = new int[n];
            Array.Fill(ceils, -1);
            Array.Fill(floors, -1);
            foreach (var i in set)
            {
                int id = (i - min) / width;
                int mod = (i - min) % width;
                if (ceils[id] == -1)
                {
                    ceils[id] = mod;
                    floors[id] = mod;
                }
                else
                {
                    ceils[id] = Math.Max(ceils[id], mod);
                    floors[id] = Math.Min(floors[id], mod);
                }
            }
            int res = 0;
            int prevId = 0;
            for (int i = 0; i < n; i++)
            {
                if (ceils[i] != -1)
                {
                    res = Math.Max(res, (i - prevId) * width + floors[i] - ceils[prevId]);
                    prevId = i;
                }
            }
            return res;
        }

        ///165. Compare Version Numbers
                 ///If version1<version2, return -1. If version1 > version2, return 1. Otherwise, return 0.
        public int CompareVersion(string version1, string version2)
        {
            var list1 = version1.Split('.').Select(x => int.Parse(x)).ToList();
            var list2 = version2.Split('.').Select(x => int.Parse(x)).ToList();

            for (int i = 0; i < list1.Count && i < list2.Count; i++)
            {
                if (list1[i] > list2[i]) return 1;
                else if (list1[i] < list2[i]) return -1;
            }

            if (list1.Count > list2.Count)
            {
                for (int i = list2.Count; i < list1.Count; i++)
                {
                    if (list1[i] > 0) return 1;
                }
            }
            else if (list1.Count < list2.Count)
            {
                for (int i = list1.Count; i < list2.Count; i++)
                {
                    if (list2[i] > 0) return -1;
                }
            }
            return 0;
        }

        /// 166. Fraction to Recurring Decimal
        ///Given two integers representing the numerator and denominator of a fraction, return the fraction in string format.
        ///If the fractional part is repeating, enclose the repeating part in parentheses.
        ///If multiple answers are possible, return any of them.
        ///It is guaranteed that the length of the answer string is less than 104 for all the given inputs.
        public string FractionToDecimal(int numerator, int denominator)
        {
            if (numerator == 0) return "0";
            StringBuilder ans = new StringBuilder();
            // "+" or "-"
            ans.Append(((numerator > 0) ^ (denominator > 0)) ? "-" : "");
            long num = Math.Abs((long)numerator);
            long deno = Math.Abs((long)denominator);

            // integral part
            ans.Append(num / deno);
            num %= deno;
            if (num == 0)
                return ans.ToString();

            // fractional part
            ans.Append(".");
            Dictionary<long, int> dict = new Dictionary<long, int>();
            dict.Add(num, ans.Length);
            while (num != 0)
            {
                num *= 10;
                ans.Append(num / deno);
                num %= deno;
                if (dict.ContainsKey(num))
                {
                    ans.Insert(dict[num], "(");
                    ans.Append(")");
                    break;
                }
                else
                {
                    dict.Add(num, ans.Length);
                }
            }
            return ans.ToString();
        }

        ///167. Two Sum II - Input Array Is Sorted, #Two Pointers
        ///Given a 1-indexed array sorted in non-decreasing order, find two numbers add up to target number.
        //numbers[index1] and numbers[index2] where 1 <= index1<index2 <= numbers.length.
        ///Return the indices of the two numbers, index1 and index2, added by one as an integer array[index1, index2] of length 2.
        public int[] TwoSum167_TwoPointers(int[] numbers, int target)
        {
            int left = 0;
            int right = numbers.Length - 1;
            int sum = 0;
            while (left < right)
            {
                sum = numbers[left] + numbers[right];
                if (sum == target) return new int[] { left + 1, right + 1 };
                else if (sum < target) left++;
                else right--;
            }
            return new int[] { };
        }

        /// 168. Excel Sheet Column Title
        //Given an integer columnNumber, return its corresponding column title as it appears in an Excel sheet.
        public string ConvertToTitle(int columnNumber)
        {
            List<char> res = new List<char>();
            int n = columnNumber;
            while (n > 0)
            {
                var c = (char)((n - 1) % 26 + 'A');
                res.Insert(0, c);
                n = (n - 1) / 26;
            }
            return new string(res.ToArray());
        }

        /// 169. Majority Element
        //The majority element is the element that appears more than n/2 times.
        public int MajorityElement(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int half = nums.Length / 2 + 1;
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i]))
                    dict.Add(nums[i], 0);
                if (++dict[nums[i]] >= half) return nums[i];
            }
            return -1;
        }



        ///171. Excel Sheet Column Number
        //return its corresponding column number.
        public int TitleToNumber(string columnTitle)
        {
            int res = 0;
            int seed = 1;
            for (int i = columnTitle.Length - 1; i >= 0; i--)
            {
                res += seed * (columnTitle[i] - 'A' + 1);
                seed *= 26;
            }
            return res;
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

        ///179. Largest Number
        //Given a list of non-negative integers nums, arrange them form the largest number and return it.
        //Since the result may be very large, so you need to return a string instead of an integer.
        public string LargestNumber(int[] nums)
        {
            StringBuilder sb = new StringBuilder();
            Array.Sort(nums, (x, y) =>
            {
                return - LargestNumber_Compare(x, y);
            });

            int i = 0;
            for (; i < nums.Length; i++)
            {
                if (nums[i] == 0) break;
                sb.Append(nums[i]);
            }
            if (i != nums.Length)
            {
                if (sb.Length == 0) return "0";
                while (i++ < nums.Length)
                    sb.Append("0");
            }
            return sb.ToString();
        }

        private int LargestNumber_Compare(int x, int y)
        {
            string a = $"{x}{y}";
            string b = $"{y}{x}";
            return a.CompareTo(b);
        }

        ///183. Customers Who Never Order, see sql script

        /// 187. Repeated DNA Sequences
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

        /// 198. House Robber, cannot rob adjacent houses, #DP
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

        ///199. Binary Tree Right Side View, #BTree
        ///return the right side values of the nodes you can see ordered from top to bottom.
        public IList<int> RightSideView(TreeNode root)
        {
            var res = new List<int>();
            if (root == null)
                return res;
            List<TreeNode> nodes = new List<TreeNode>() { root };
            while (nodes.Count > 0)
            {
                res.Add(nodes.Last().val);
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
            return res;
        }
    }
}