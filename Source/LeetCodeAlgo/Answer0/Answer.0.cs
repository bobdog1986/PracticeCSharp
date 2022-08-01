using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        /// 1. Two Sum

        ///2. Add Two Numbers

        /// 3. Longest Substring Without Repeating Characters, #Sliding Window
        /// Given a string s, find the length of the longest substring without repeating characters.
        /// s consists of English letters, digits, symbols and spaces.
        public int LengthOfLongestSubstring(string s)
        {
            if (s.Length <= 1) return s.Length;
            Dictionary<char, int> map = new Dictionary<char, int>();
            int max = 0;
            int left = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                if (map.ContainsKey(s[i]))
                {
                    left = Math.Max(left, map[s[i]] + 1);
                    map[s[i]] = i;
                }
                else map.Add(s[i], i);
                max = Math.Max(max, i - left + 1);
            }
            return max;
        }

        ///4. Median of Two Sorted Arrays, #Two Pointers

        ///5. Longest Palindromic Substring
        //Given a string s, return the longest palindromic substring in s.
        //Input: s = "babad" Output: "bab"
        public string LongestPalindrome(string s)
        {
            if (s.Length <= 1)
                return s;
            string ans = string.Empty;
            int len = 0;
            for (int i = 0; i < s.Length; i++)
            {
                //find all aba pattern, loop only all possible index
                if (i >= (len - 1) / 2 + 1 && 2 * (s.Length - 1 - i) + 1 > len)
                {
                    int count = 0;
                    while (i - 1 - count >= 0 && i + 1 + count <= s.Length - 1)
                    {
                        if (s[i - 1 - count] == s[i + 1 + count])
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (count + count + 1 > len)
                    {
                        len = count + count + 1;
                        ans = s.Substring((i - count), len);
                    }
                }

                //find all abba pattern, loop only all possible index
                if (i >= len / 2 && i < s.Length - 1 && 2 * (s.Length - 1 - i) > len)
                {
                    int count = 0;
                    while (i - count >= 0 && i + 1 + count <= s.Length - 1)
                    {
                        if (s[i - count] == s[i + 1 + count])
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (count + count > len)
                    {
                        len = count + count;
                        ans = s.Substring((i - count + 1), len);
                    }
                }
            }
            return ans;
        }

        ///6. Zigzag Conversion

        ///7. Reverse Integer
        ///Given a signed 32-bit integer x, return x with its digits reversed.
        ///If reversing x causes the value to go outside the signed 32-bit integer range [-2^31, 2^31 - 1], then return 0.
        ///Assume the environment does not allow you to store 64-bit integers (signed or unsigned).
        public int Reverse(int x)
        {
            if (x == 0 || x == int.MinValue)
                return 0;
            bool isNeg = x < 0;
            if (x < 0)
                x = -x;
            int res = 0;
            while (x > 0)
            {
                if (res > (int.MaxValue / 10))
                    return 0;
                res *= 10;
                if (int.MaxValue - x % 10 < res)
                    return 0;
                res += x % 10;
                x = x / 10;
            }
            return isNeg ? -res : res;
        }

        /// 8. String to Integer (atoi)
        ///Implement the myAtoi(string s) function, which converts a string to a int (similar to C/C++'s atoi function).
        ///0 <= s.length <= 200, s consists of English letters(lower-case and upper-case), digits(0-9), ' ', '+', '-', and '.'.
        public int MyAtoi(string s)
        {
            s = s.Trim();
            int sign = 0;
            int res = 0;
            bool anyDigit = false;
            bool isOverFlow = false;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    anyDigit = true;
                    if (isOverFlow) continue;
                    int d = (s[i] - '0');
                    if (sign == -1)
                    {
                        if (res > int.MaxValue / 10 || int.MinValue + res * 10 > -d)
                            isOverFlow = true;
                    }
                    else
                    {
                        if (res > int.MaxValue / 10 || int.MaxValue - res * 10 < d)
                            isOverFlow = true;
                    }
                    if (!isOverFlow)
                        res = res * 10 + d;
                    else
                        break;
                }
                else
                {
                    if (anyDigit) break;
                    if (sign == 0)
                    {
                        if (s[i] == '+')
                            sign = 1;
                        else if (s[i] == '-')
                            sign = -1;
                        else
                            return 0;
                    }
                    else
                        return 0;
                }
            }
            if (!anyDigit) return 0;
            else if (isOverFlow)
                return sign == -1 ? int.MinValue : int.MaxValue;
            else
                return sign == -1 ? -res : res;
        }

        ///9. Palindrome Number
        ///An integer is a palindrome when it reads the same backward as forward.
        ///For example, 121 is a palindrome while 123 is not.
        /// -121 is not
        public bool IsPalindrome(int x)
        {
            if (x < 0) return false;
            if (x < 10) return true;
            if (x % 10 == 0) return false;
            int n = x;
            List<int> digits = new List<int>();
            while (n > 0)
            {
                digits.Add(n % 10);
                n = n / 10;
            }
            for (int i = 0; i < digits.Count / 2; i++)
            {
                if (digits[i] != digits[digits.Count - 1 - i])
                    return false;
            }
            return true;
        }

        ///10. Regular Expression Matching, #DP
        ///Given an input string s and a pattern p, implement regular expression matching with support for '.' and '*'
        ///'.' Matches any single character​​​​, '*' Matches zero or more of the preceding element.
        ///1 <= s.length <= 20, 1 <= p.length <= 30,
        ///s contains only lowercase English letters.p contains only lowercase English letters, '.', and '*'.
        ///It is guaranteed for each appearance of the character '*', there will be a previous valid character to match.
        public bool IsMatch_10(string s, string p)
        {
            //All posibilities:
            //1. p[pIndex] == '*'
            //    1.1 p[pIndex] == s[sIndex]
            //        1.1.1 '*' can use 0 times: remove 'x*' dp[i, j - 2]
            //        1.1.1 '*' can use 1~n times: keep 'x*' dp[i - 1, j]
            //    1.2 p[pIndex] != s[sIndex]
            //        1.2.1 '*' can use 0 times.
            //2. p[pIndex] == '.'
            //3. p[pIndex] == a~z
            //    3.1 p[pIndex] == s[sIndex]
            //    3.2 p[pIndex] != s[sIndex]
            var sLen = s.Length;
            var pLen = p.Length;

            var dp = new bool[sLen + 1, pLen + 1];
            // init - only s: "" and p: "" => true, the other all false
            dp[0, 0] = true;
            for (int i = 0; i <= sLen; i++)
            {
                var sIndex = i - 1;
                for (int j = 1; j <= pLen; j++)
                {
                    var pIndex = j - 1;

                    if (p[pIndex] == '*')
                    {
                        if (i > 0 && (p[pIndex - 1] == s[sIndex] || p[pIndex - 1] == '.'))
                        {
                            // aa    * = 0 (dp[i, j-2])   aa   * = 1~n (dp[i - 1, j])
                            //  a*                         a*
                            dp[i, j] = dp[i, j - 2] || dp[i - 1, j];
                        }
                        else
                        {
                            // ab   * = 0 (dp[i, j-2])
                            //  a*
                            dp[i, j] = dp[i, j - 2];
                        }
                    }
                    else if (i > 0 && p[pIndex] == '.')
                    {
                        // abc
                        // ab.
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        if (i > 0 && p[pIndex] == s[sIndex])
                        {
                            // ab
                            // ab
                            dp[i, j] = dp[i - 1, j - 1];
                        }
                    }
                }
            }
            return dp[sLen, pLen];
        }

        /// 11. Container With Most Water, #Two Pointers
        /// max value of (j-i)*min(arr[i],arr[j])
        public int MaxArea(int[] height)
        {
            int max = 0;
            int left = 0;
            int right = height.Length - 1;
            while (left < right)
            {
                while (height[left] == 0 && left<right)
                    left++;
                while (height[right] == 0 && left<right)
                    right--;
                if (left < right)
                {
                    max = Math.Max(max, (right - left) * Math.Min(height[left], height[right]));
                    if (height[left] < height[right])
                    {
                        left++;
                    }
                    else if(height[left] > height[right])
                    {
                        right--;
                    }
                    else// if equal
                    {
                        left++;
                        right--;
                    }
                }
            }
            return max;
        }

        /// 12. Integer to Roman
        /// Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.
        /// I             1
        /// V             5
        /// X             10
        /// L             50
        /// C             100
        /// D             500
        /// M             1000
        /// CM            900
        /// CD            400
        /// XC            90
        /// XL            40
        /// IX            9
        /// IV            4
        public string IntToRoman(int num)
        {
            if (num >= 1000) return "M" + IntToRoman(num - 1000);
            if (num >= 900) return "CM" + IntToRoman(num - 900);
            if (num >= 500) return "D" + IntToRoman(num - 500);
            if (num >= 400) return "CD" + IntToRoman(num - 400);
            if (num >= 100) return "C" + IntToRoman(num - 100);
            if (num >= 90) return "XC" + IntToRoman(num - 90);
            if (num >= 50) return "L" + IntToRoman(num - 50);
            if (num >= 40) return "XL" + IntToRoman(num - 40);
            if (num >= 10) return "X" + IntToRoman(num - 10);
            if (num >= 9) return "IX" + IntToRoman(num - 9);
            if (num >= 5) return "V" + IntToRoman(num - 5);
            if (num >= 4) return "IV" + IntToRoman(num - 4);
            if (num > 1) return "I" + IntToRoman(num - 1);
            if (num == 1) return "I";
            return string.Empty;
        }

        ///13. Roman to Integer
        ///Roman numerals are represented by seven different symbols: I, V, X, L, C, D and M.
        ///It is guaranteed that s is a valid roman numeral in the range [1, 3999]
        public int RomanToInt(string s)
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>()
            {
                { 'I', 1},
                { 'V', 5},
                { 'X', 10},
                { 'L', 50},
                { 'C', 100},
                { 'D', 500},
                { 'M', 1000}
            };

            int number = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (i + 1 < s.Length && dictionary[s[i]] < dictionary[s[i + 1]])
                {
                    number -= dictionary[s[i]];
                }
                else
                {
                    number += dictionary[s[i]];
                }
            }
            return number;
        }

        ///14. Longest Common Prefix

        ///15. 3Sum, #Two Pointer, #BinarySearch
        ///nums[i] + nums[j] + nums[k] == 0. no same triplets
        ///0 <= nums.length <= 3000, -10^5 <= nums[i] <= 10^5
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            //using binary search
            Array.Sort(nums);
            int n = nums.Length;
            List<IList<int>> res = new List<IList<int>>();

            int prev = int.MinValue;
            for(int i = 0; i < n-2; i++)
            {
                if (nums[i] + nums[i + 1] + nums[i + 2] > 0) break;//min is overflow
                if (nums[i] == prev) continue;//skip duplicate
                if (nums[i] + nums[n-1] + nums[n-2] < 0) continue;//max is underflow
                for (int j = i + 1; j < n - 1; j++)
                {
                    if (j > i + 1 && nums[j] == nums[j - 1]) continue;//skip duplicate
                    if (nums[i] + nums[j] + nums[j + 1] > 0) break;//min is overflow
                    if (nums[i] + nums[j] + nums[n-1] < 0) continue;//max is underflow
                    int left = j + 1;
                    int right = n - 1;
                    while (left < right)
                    {
                        int mid = (left + right) / 2;
                        if (nums[i] + nums[j] + nums[mid] == 0)
                        {
                            left = mid;
                            break;
                        }
                        else if (nums[i] + nums[j] + nums[mid] > 0)
                            right = mid - 1;
                        else
                            left = mid + 1;
                    }
                    if (nums[i] + nums[j] + nums[left] == 0)
                        res.Add(new List<int>() { nums[i] , nums[j] , nums[left] });
                }
                prev = nums[i];
            }
            return res;
        }

        public IList<IList<int>> ThreeSum_TwoPointers(int[] nums)
        {
            Array.Sort(nums);
            int n = nums.Length;
            List<IList<int>> res = new List<IList<int>>();

            int prev = int.MinValue;
            for (int i = 0; i < n - 2; i++)
            {
                if (nums[i] == prev) continue;
                int left = i + 1;
                int right = n - 1;
                while (left < right)
                {
                    while (left > i + 1 && left < right && nums[left] == nums[left - 1])
                        left++;
                    while (right < n - 1 && left < right && nums[right] == nums[right + 1])
                        right--;

                    if (left < right)
                    {
                        int sum = nums[i] + nums[left] + nums[right];
                        if (sum == 0)
                        {
                            res.Add(new List<int> { nums[i], nums[left], nums[right] });
                            left++;
                            right--;
                        }
                        else if (sum > 0)
                            right--;
                        else
                            left++;
                    }
                }
                prev = nums[i];
            }
            return res;
        }

        ///16. 3Sum Closest
        ///return three integers nums such that the sum is closest to target.
        ///-1000 <= nums[i] <= 1000, len = [3,1000], -10000<=target<=10000
        public int ThreeSumClosest(int[] nums, int target)
        {
            int closestSum = nums[0] + nums[1] + nums[2];
            Array.Sort(nums);
            // Go through nums
            for (int i = 0; i < nums.Length - 2; i++)
            {
                // do 2sum modified
                int right = nums.Length - 1;
                int left = i + 1;
                while (left < right)
                {
                    int sum = nums[i] + nums[left] + nums[right];
                    if (sum == target)
                        return sum;
                    if (Math.Abs(target - sum) < Math.Abs(target - closestSum))
                        closestSum = sum;
                    if (sum > target)
                        right--;
                    else
                        left++;
                }
            }
            return closestSum;
        }

        ///17. Letter Combinations of a Phone Number, #Backtracking
        ///digits[i] is a digit in the range['2', '9']. 0 <= digits.length <= 4
        /// 2- ABC, 3-DEF, 4-GHI, 5-JKL,6-MNO,7-PQRS,8-TUV 9-WXYZ
        public IList<string> LetterCombinations(string digits)
        {
            if (digits.Length == 0)
                return new List<string>();
            Dictionary<char, string> dict = new Dictionary<char, string>
            {
                { '2', "abc" } ,
                { '3', "def" } ,
                { '4', "ghi" } ,
                { '5', "jkl" } ,
                { '6', "mno" } ,
                { '7', "pqrs" } ,
                { '8', "tuv" } ,
                { '9', "wxyz" } ,
            };
            var q = new Queue<string>();
            q.Enqueue("");
            foreach (var d in digits)
            {
                int size = q.Count;
                while (size-- > 0)
                {
                    var curr = q.Dequeue();
                    foreach (char c in dict[d])
                        q.Enqueue(curr + c);
                }
            }
            return q.ToList();
        }

        /// 18. 4Sum, #Two Pointers
        ///Given an array nums of n integers, return an array of all the unique
        ///quadruplets [nums[a], nums[b], nums[c], nums[d]] sum to target
        ///Input: nums = [2,2,2,2,2], target = 8
        ///Output: [[2,2,2,2]]
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            List<IList<int>> res = new List<IList<int>>();
            int n = nums.Length;
            Array.Sort(nums);
            for (int i = 0; i < n - 3; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;//skip all duplicates
                for (int j = i + 1; j < n - 2; j++)
                {
                    if (j > i + 1 && nums[j] == nums[j - 1]) continue;//skip all duplicates
                    int left = j + 1;
                    int right = n - 1;
                    while (left < right)
                    {
                        while (left < right && left > j + 1 && nums[left] == nums[left - 1])
                            left++;
                        while (left < right && right <n-1 && nums[right] == nums[right + 1])
                            right--;
                        if (left < right)
                        {
                            //convert to long, avoid overflow of int sum
                            long sum = (long)nums[left] + nums[right] + nums[i] + nums[j];
                            if (sum == target)
                            {
                                res.Add(new List<int> { nums[i], nums[j], nums[left], nums[right] });
                                left++;
                                right--;
                            }
                            else if (sum < target)
                                left++;
                            else
                                right--;
                        }
                    }
                }
            }
            return res;
        }

        ///19. Remove Nth Node From End of List
        ///Given the head of a linked list, remove the nth node from the end of the list and return its head.
        ///1 <= n <= size, 1 <= size <= 30
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            int count = 0;
            var node = head;
            while (node != null)
            {
                count++;
                node = node.next;
            }
            if (count <= 1)
                return null;
            if (n == count)
                return head.next;
            node = head;
            int index = count - n - 1;
            while (index > 0)
            {
                node = node.next;
                index--;
            }
            node.next = node.next.next;
            return head;
        }

        ///20. Valid Parentheses
        ///s containing just '(', ')', '{', '}', '[' and ']', determine if the input string is valid.
        public bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (var c in s)
            {
                if (c == '[') stack.Push(']');
                if (c == '(') stack.Push(')');
                if (c == '{') stack.Push('}');
                if (c == ']' || c == '}' || c == ')')
                {
                    if (stack.Count == 0 || c != stack.Pop())
                        return false;
                }
            }
            return stack.Count == 0;
        }

        ///21. Merge Two Sorted Lists, #Two Pointers
        ///Merge two sorted list in a one sorted list.Return the head
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null || list2 == null)
                return list1 ?? list2;

            ListNode head = null;
            if (list1.val <= list2.val)
            {
                head = list1;
                list1 = list1.next;
            }
            else
            {
                head = list2;
                list2 = list2.next;
            }
            ListNode prev = head;
            while (list1 != null || list2 != null)
            {
                if (list1 == null)
                {
                    prev.next = list2;
                    break;
                }
                else if (list2 == null)
                {
                    prev.next = list1;
                    break;
                }
                else
                {
                    if (list1.val <= list2.val)
                    {
                        prev.next = list1;
                        list1 = list1.next;
                    }
                    else
                    {
                        prev.next = list2;
                        list2 = list2.next;
                    }
                    prev = prev.next;
                }
            }
            return head;
        }

        ///22. Generate Parentheses, #Backtracking
        ///Given n pairs of parentheses, write a function to generate all combinations of well-formed parentheses.
        ///1 <= n <= 8, if n=3, return ["((()))","(()())","(())()","()(())","()()()"]
        public List<string> GenerateParenthesis(int n)
        {
            List<string> ans = new List<string>();
            GenerateParenthesis_Backtracking(ans, "", 0, 0, n);
            return ans;
        }

        ///string is immutable , best for this question
        private void GenerateParenthesis_Backtracking(IList<string> list, string str, int left, int right, int count)
        {
            if (left == count && right == count)
            {
                list.Add(str);
                return;
            }
            if (left < count)
            {
                GenerateParenthesis_Backtracking(list, str + "(", left + 1, right, count);
            }
            if (right < left)
            {
                GenerateParenthesis_Backtracking(list, str + ")", left, right + 1, count);
            }
        }

        ///23. Merge k Sorted Lists
        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0)
                return null;

            var nodes = lists.Where(x => x != null).ToList();

            if (nodes == null || nodes.Count == 0)
                return null;

            if (nodes.Count == 1)
                return nodes[0];

            return nodes.Aggregate((x, y) => MergeKLists_Merge2(x, y));
        }

        private ListNode MergeKLists_Merge2(ListNode node1, ListNode node2)
        {
            while (node2 != null)
            {
                var next = node2.next;
                if (node2.val <= node1.val)
                {
                    var temp = node1;
                    node2.next = temp;
                    node1 = node2;
                }
                else
                {
                    var node = node1;
                    while (node != null)
                    {
                        if (node.val < node2.val && (node.next == null || node.next.val >= node2.val))
                        {
                            var temp = node.next;
                            node.next = node2;
                            node2.next = temp;
                            break;
                        }
                        node = node.next;
                    }
                }
                node2 = next;
            }
            return node1;
        }

        ///24. Swap Nodes in Pairs
        ///swap every two adjacent nodes and return its head. head = [1,2,3,4] => Output: [2,1,4,3]
        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            var node = head;
            //var pre=node.next;
            var biNext = node.next.next;
            var node1 = node;
            var node2 = node.next;
            node1.next = biNext;
            node2.next = node1;
            var pre = node2.next;
            head = node2;
            node = biNext;
            while (node != null && node.next != null)
            {
                biNext = node.next.next;
                pre.next = node.next;
                pre.next.next = node;
                node.next = biNext;
                pre = node;
                node = biNext;
            }

            return head;
        }

        public ListNode SwapPairs_Recursion(ListNode head)
        {
            if (head == null || head.next == null)
                return head;
            ListNode next = head.next;
            head.next = SwapPairs_Recursion(head.next.next);
            next.next = head;
            return next;
        }

        ///25. Reverse Nodes in k-Group
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (k == 1)
                return head;
            ListNode pre = null;
            ListNode node = head;
            while (node != null)
            {
                int i = 0;
                Stack<ListNode> stack = new Stack<ListNode>();
                //stack nodes to reverse
                while (i < k)
                {
                    if (node == null)
                        break;
                    stack.Push(node);
                    node = node.next;
                    i++;
                }
                //if can reverse
                if (i == k)
                {
                    ListNode subNode = stack.Pop();
                    ListNode curr = subNode;
                    while (stack.Count != 0)
                    {
                        var pop = stack.Pop();
                        curr.next = pop;
                        curr = curr.next;
                    }
                    curr.next = node;
                    //if first loop, update head
                    if (pre == null)
                    {
                        head = subNode;
                    }
                    else
                    {
                        pre.next = subNode;
                    }
                    pre = curr;
                }
                else
                {
                    //if not enough nodes to reverse
                    break;
                }
            }
            return head;
        }

        ///26. Remove Duplicates from Sorted Array, #Two Pointers
        ///nums sorted in non-decreasing order, remove the duplicates that each unique element appears only once.
        ///-100 <= nums[i] <= 100
        public int RemoveDuplicates_26_OnlyOnce(int[] nums)
        {
            int prev = int.MinValue;
            int begin = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != prev)
                {
                    nums[begin++] = nums[i];
                }
                prev = nums[i];
            }
            return begin;
        }

        ///27. Remove Element, #Two Pointers
        ///Do not allocate extra space for another array.
        ///You must do this by modifying the input array in-place with O(1) extra memory.
        public int RemoveElement(int[] nums, int val)
        {
            int begin = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != val)
                    nums[begin++] = nums[i];
            }
            return begin;
        }

        ///28. Implement strStr(), #KMP
        ///Return the index of the first occurrence of needle in haystack, or -1 if needle is not part of haystack.
        ///0 <= haystack.length, needle.length <= 5 * 10^4
        public int StrStr(string haystack, string needle)
        {
            for (int i = 0; i < haystack.Length - needle.Length + 1; i++)
            {
                if (haystack.Substring(i, needle.Length) == needle)
                    return i;
            }
            return -1;
        }



        ///29. Divide Two Integers
        ///-2^31 <= dividend, divisor <= 2^31 - 1, divisor!=0
        public int Divide(int dividend, int divisor)
        {
            if (dividend == 0) return 0;
            if (dividend == int.MinValue && divisor == -1)
                return int.MaxValue;

            if (divisor == 1) return dividend;
            if (divisor == -1) return -dividend;
            if (divisor == int.MinValue) return dividend == int.MinValue ? 1 : 0;
            if (divisor == int.MaxValue)
            {
                if (dividend == int.MaxValue) return 1;
                if (dividend <= int.MinValue + 1) return -1;
                return 0;
            }

            int ans = 0;
            int sign = dividend > 0 ^ divisor > 0 ? -1 : 1;
            long did = Math.Abs((long)dividend);
            long div = Math.Abs((long)divisor);
            // 15/3 = 15/12 + 3/3, 12=3<<2, ans+=1<<2=4, ans+=1<<0=1,
            while (did >= div)
            {
                long temp = div;
                long m = 1;
                while (temp << 1 <= did && m != 1 << 30)
                {
                    temp <<= 1;
                    m <<= 1;
                }
                did -= temp;
                ans += (int)m;
            }
            return sign == 1 ? ans : -ans;
        }

        ///30. Substring with Concatenation of All Words
        ///return index of substring == all words, s = "barfoothefoobarman", words = ["foo","bar"], return [0,9]
        ///1 <= s.length <= 104,1 <= words.length <= 5000,1 <= words[i].length <= 30, all lower-case letter
        public IList<int> FindSubstring(string s, string[] words)
        {
            List<int> ans = new List<int>();
            int len = words.Select(x => x.Length).Sum();
            List<string> exists = new List<string>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string word in words)
            {
                if (dict.ContainsKey(word)) dict[word]++;
                else dict.Add(word, 1);
            }
            for (int i = 0; i < s.Length + 1 - len; i++)
            {
                if (exists.Where(x => x == s.Substring(i, len)).Count() > 0)
                {
                    ans.Add(i);
                    continue;
                }
                Dictionary<string, int> dict2 = new Dictionary<string, int>();
                foreach (var d in dict)
                    dict2.Add(d.Key, d.Value);

                bool find = FindSubstring(s, words, i, dict2);
                if (find)
                {
                    ans.Add(i);
                    exists.Add(s.Substring(i, len));
                }
            }
            return ans;
        }

        public bool FindSubstring(string s, string[] words, int index, Dictionary<string, int> dict)
        {
            if (words.Length == 0)
                return true;
            if (words.Length == 1)
            {
                return s.Substring(index, words[0].Length) == words[0];
            }
            var pools = dict.Where(x => x.Value > 0).Select(x => x.Key).ToList();
            var matchs = pools.Where(x => x == s.Substring(index, x.Length)).ToList();
            if (matchs.Count > 0)
            {
                foreach (var match in matchs)
                {
                    List<string> list = new List<string>(words);
                    list.Remove(match);
                    dict[match]--;
                    bool find = FindSubstring(s, list.ToArray(), index + match.Length, dict);
                    if (find)
                        return true;
                }
            }
            return false;
        }

        ///31. Next Permutation
        ///For example, the next permutation of arr = [1,2,3] is [1,3,2].
        ///Find last num that nums[i+1]>nums[i], then find the last nums[j]>nums[i],
        ///if cannot find num, is the last sequence, then sort() it;
        public void NextPermutation(int[] nums)
        {
            if (nums.Length == 1)
                return;
            int i = 0;
            //Find first no in nums which is smaller than its previous no.
            for (i = nums.Length - 1; i > 0; i--)
            {
                if (nums[i - 1] < nums[i])
                    break;
            }
            if (i != 0)
            {
                //Find first greater than i-1 and swap i-1 and j
                for (int j = nums.Length - 1; j >= i; j--)
                {
                    if (nums[i - 1] < nums[j])
                    {
                        //swap
                        int temp = nums[i - 1];
                        nums[i - 1] = nums[j];
                        nums[j] = temp;
                        break;
                    }
                }
            }
            //why????  reverse everything from i to end
            NextPermutation_Reverse(nums, i, nums.Length - 1);
        }

        private void NextPermutation_Reverse(int[] nums, int start, int end)
        {
            for (int i = start, j = end; i < j; i++, j--)
            {
                int temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;
            }
        }

        ///32. Longest Valid Parentheses
        ///Given a string containing just the characters '(' and ')',
        ///find the length of the longest valid (well-formed) parentheses substring.
        public int LongestValidParentheses(string s)
        {
            if (s.Length <= 1)
                return 0;
            int maxLen = 0;
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < s.Length - 1 - maxLen; i++)
            {
                if (s[i] == '(')
                {
                    int j = i;
                    int len = 0;
                    int count = 0;
                    stack.Clear();
                    while (j < s.Length)
                    {
                        if (s[j] == '(')
                        {
                            stack.Push(s[j]);
                        }
                        else
                        {
                            if (stack.Count == 0)
                            {
                                break;
                            }
                            else
                            {
                                var c = stack.Pop();
                                if (c == '(')
                                {
                                    if (stack.Count == 0)
                                    {
                                        count++;
                                        len += 2 * count;
                                        count = 0;
                                    }
                                    else
                                    {
                                        count++;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        j++;
                    }
                    if (stack.Count == 0)
                        i += len;
                    maxLen = Math.Max(maxLen, len);
                }
            }
            return maxLen;
        }

        ///33. Search in Rotated Sorted Array, #Binary Search
        /// original array [1,2,3,4,5] sorted in ascending order (with distinct values).
        /// then possibly rotated to eg. [3,4,5,1,2]
        /// return the index of target if it is in nums or -1
        public int Search_33_BinarySearch(int[] nums, int target)
        {
            int n = nums.Length;
            int left = 0, right = n - 1;
            // find the index of the smallest value using binary search.
            // Loop will terminate since mid < hi, and lo or hi will shrink by at least 1.
            // Proof by contradiction that mid < hi: if mid==hi, then lo==hi and loop would have been terminated.
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (nums[mid] > nums[right]) left = mid + 1;
                else right = mid;
            }
            // lo==hi is the index of the smallest value and also the number of places rotated.
            int rot = left;
            left = 0; right = n - 1;
            // The usual binary search and accounting for rotation.
            while (left <= right)
            {
                int mid = (left + right) / 2;
                int realmid = (mid + rot) % n;
                if (nums[realmid] == target) return realmid;
                if (nums[realmid] < target) left = mid + 1;
                else right = mid - 1;
            }
            return -1;
        }

        /// 34. Find First and Last Position of Element in Sorted Array, #Binary Search
        ///[5,7,7,8,8,10], target = 8, return [3,4], if not found return [-1,-1]
        public int[] SearchRange(int[] nums, int target)
        {
            int[] result = new int[] { -1, -1 };
            int left = 0;
            int right = nums.Length - 1;
            int found = -1;
            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (nums[mid] == target)
                {
                    found = mid;
                    break;
                }
                else if (nums[mid] > target)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            if (found != -1)
            {
                int start = found;
                while (start > 0 && nums[start - 1] == target)
                {
                    start--;
                }

                int end = found;
                while (end < nums.Length - 1 && nums[end + 1] == target)
                {
                    end++;
                }
                return new int[] { start, end };
            }

            return result;
        }

        /// 35. Search Insert Position, #Binary Search
        /// Given a sorted array of distinct integers and a target value, return the index if the target is found.
        /// If not, return the index where it would be if it were inserted in order.
        public int SearchInsert(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (nums[mid] == target) return mid;
                else if (target > nums[right]) return right + 1;
                else if (target < nums[left]) return left;
                else if (nums[mid] > target) right = mid - 1;
                else left = mid + 1;
            }
            return -1;//never go here
        }

        /// 36. Valid Sudoku
        public bool IsValidSudoku(char[][] board)
        {
            int[,] arrRow = new int[9, 9];
            int[,] arrCol = new int[9, 9];
            int[,] arrBlock = new int[9, 9];
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == '.')
                        continue;
                    int k = board[i][j] - '1';
                    int cell = i / 3 * 3 + j / 3;
                    if (arrRow[i, k] == 0 && arrCol[j, k] == 0 && arrBlock[cell, k] == 0)
                    {
                        arrRow[i, k] = 1;
                        arrCol[j, k] = 1;
                        arrBlock[cell, k] = 1;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// 37. Sudoku Solver, #Backtracking
        public void SolveSudoku(char[][] board)
        {
            if (board == null || board.Length == 0)
                return;
            SolveSudoku_BackTracking(board);
        }

        private bool SolveSudoku_BackTracking(char[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[0].Length; j++)
                {
                    if (board[i][j] == '.')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (SolveSudoku_isValid(board, i, j, c))
                            {
                                board[i][j] = c; //Put c for this cell

                                if (SolveSudoku_BackTracking(board))
                                    return true; //If it's the solution return true
                                else
                                    board[i][j] = '.'; //Otherwise go back
                            }
                        }

                        return false;
                    }
                }
            }
            return true;
        }

        private bool SolveSudoku_isValid(char[][] board, int row, int col, char c)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[i][col] != '.' && board[i][col] == c) return false; //check row
                if (board[row][i] != '.' && board[row][i] == c) return false; //check column
                if (board[3 * (row / 3) + i / 3][3 * (col / 3) + i % 3] != '.' &&
                    board[3 * (row / 3) + i / 3][3 * (col / 3) + i % 3] == c)
                    return false; //check 3*3 block
            }
            return true;
        }

        /// 38. Count and Say, #DP
        ///countAndSay(n) is the way you would "say" the digit string from countAndSay(n-1),countAndSay(1) = "1"
        ///eg. countAndSay(2)=say 'one' '1'= 11,countAndSay(3)=two '1' =21, countAndSay(3)=one '2' one '1'=1211
        public string CountAndSay(int n)
        {
            string ans = "1";
            int i = 1;
            while (i++ < n)
                ans = CountAndSay(ans);
            return ans;
        }

        public string CountAndSay(string s)
        {
            List<char> ans = new List<char>();
            char c = s[0];
            int count = 1;
            for (int i = 1; i < s.Length; i++)
            {
                if (c == s[i])
                {
                    count++;
                }
                else
                {
                    if (count != 0)
                    {
                        ans.Add((char)(count + '0'));
                        ans.Add(c);
                    }
                    count = 1;
                    c = s[i];
                }
            }
            if (count != 0)
            {
                ans.Add((char)(count + '0'));
                ans.Add(c);
            }
            return string.Join("", ans);
        }

        ///39. Combination Sum
        ///Given an array of distinct integers candidates and a target integer target,
        ///return a list of all unique combinations of candidates where the chosen numbers sum to target.
        ///The same number may be chosen from candidates an unlimited number of times.
        ///count of return <= 150, 1 <= candidates[i] <= 200, 1 <= target <= 500
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var ans = new List<IList<int>>();
            var nums = candidates.OrderBy(x => -x).ToList();
            var llist = new List<IList<int>>();
            bool first = true;
            for (int i = 0; i < nums.Count; i++)
            {
                if (nums[i] > target)
                    continue;
                var next = new List<IList<int>>();
                if (first)
                {
                    first = false;
                    int n = target / nums[i];
                    int m = target % nums[i];
                    if (m == 0)
                    {
                        var comb = new List<int>();
                        for (int j = 0; j < n; j++)
                            comb.Add(nums[i]);
                        ans.Add(comb);
                        n--;
                    }
                    //remember add empty to next index
                    int k = 0;
                    while (k <= n)
                    {
                        var sub = new List<int>();
                        for (int j = 0; j < k; j++)
                            sub.Add(nums[i]);
                        next.Add(sub);
                        k++;
                    }
                }
                else
                {
                    foreach (var list in llist)
                    {
                        int goal = target - list.Sum();
                        if (goal < nums[i])
                        {
                            //add to next index
                            next.Add(list);
                            continue;
                        }
                        int n = goal / nums[i];
                        int m = goal % nums[i];
                        if (m == 0)
                        {
                            var comb = new List<int>(list);
                            for (int j = 0; j < n; j++)
                                comb.Add(nums[i]);
                            ans.Add(comb);
                            n--;
                        }
                        //remember add empty to next index
                        int k = 0;
                        while (k <= n)
                        {
                            var sub = new List<int>(list);
                            for (int j = 0; j < k; j++)
                                sub.Add(nums[i]);
                            next.Add(sub);
                            k++;
                        }
                    }
                }
                llist = next;
            }
            return ans;
        }

        ///40. Combination Sum II
        ///Each number in candidates may only be used once in the combination.
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            var ans = new List<IList<int>>();
            var nums = candidates.OrderBy(x => -x).ToList();

            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            for (int j = 0; j < nums.Count; j++)
            {
                if (dict.ContainsKey(nums[j]))
                {
                    dict[nums[j]].Add(j);
                }
                else
                {
                    dict.Add(nums[j], new List<int>() { j });
                }
            }

            var llist = new List<IList<int>>();
            bool first = true;
            for (int i = 0; i < nums.Count; i++)
            {
                if (nums[i] > target)
                    continue;
                var next = new List<IList<int>>();
                if (first)
                {
                    first = false;
                    int n = target / nums[i];
                    if (target == nums[i])
                    {
                        var comb = new List<int>() { nums[i] };
                        ans.Add(comb);
                        n--;
                    }

                    //remember add empty to next index
                    int k = 0;
                    while (k <= Math.Min(n, 1))
                    {
                        var sub = new List<int>();
                        for (int j = 0; j < k; j++)
                            sub.Add(nums[i]);
                        next.Add(sub);
                        k++;
                    }
                }
                else
                {
                    foreach (var list in llist)
                    {
                        if (dict[nums[i]].Count > 1)
                        {
                            var idx = dict[nums[i]].IndexOf(i);
                            int count = list.Where(x => x == nums[i]).Count();
                            if (count != idx)
                            {
                                //add to next index
                                next.Add(list);
                                continue;
                            }
                        }

                        int goal = target - list.Sum();
                        if (goal < nums[i])
                        {
                            //add to next index
                            next.Add(list);
                            continue;
                        }

                        int n = goal / nums[i];
                        if (goal == nums[i])
                        {
                            var comb = new List<int>(list)
                            {
                                nums[i]
                            };
                            ans.Add(comb);
                            n--;
                        }
                        //remember add empty to next index
                        int k = 0;
                        while (k <= Math.Min(n, 1))
                        {
                            var sub = new List<int>(list);
                            for (int j = 0; j < k; j++)
                                sub.Add(nums[i]);
                            next.Add(sub);
                            k++;
                        }
                    }
                }
                llist = next;
            }
            return ans;
        }

        /// 41. First Missing Positive
        /// Given an unsorted integer array nums, return the smallest missing positive integer. O(n) time
        /// 1 <= nums.length <= 5 * 10^5, -2^31 <= nums[i] <= 2^31 - 1
        public int FirstMissingPositive(int[] nums)
        {
            int n = nums.Length;
            int[] arr = new int[n + 1];
            foreach (var i in nums)
            {
                if (i >= 1 && i <= n) arr[i]++;
            }
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == 0) return i;
            }
            return n + 1;
        }

        ///42. Trapping Rain Water, #Two Pointers
        ///Given n non-negative integers representing an elevation map where the width of each bar is 1,
        ///compute how much water it can trap after raining.
        ///0 <= height[i] <= 10^5, 1 <= height.Length <= 2 * 10^4
        public int Trap(int[] height)
        {
            if (height == null || height.Length == 0)
            {
                return 0;
            }
            int left = 0; int right = height.Length - 1;
            int maxLeft = 0; int maxRight = 0;

            int totalWater = 0;
            while (left < right)
            {
                if (height[left] < height[right])
                {
                    if (height[left] >= maxLeft)
                    {
                        maxLeft = height[left];
                    }
                    else
                    {
                        totalWater += maxLeft - height[left];
                    }
                    left++;
                }
                else
                {
                    if (height[right] >= maxRight)
                    {
                        maxRight = height[right];
                    }
                    else
                    {
                        totalWater += maxRight - height[right];
                    }
                    right--;
                }
            }
            return totalWater;
        }

        /// 43. Multiply Strings
        ///Given two non-negative integers num1 and num2 represented as strings,
        ///return the product of num1 and num2, also represented as a string.
        public string Multiply(string num1, string num2)
        {
            int[] digits1 = new int[num1.Length];
            for (int i = 0; i < digits1.Length; i++)
            {
                digits1[digits1.Length - 1 - i] = num1[i] - '0';
            }

            int[] digits2 = new int[num2.Length];
            for (int i = 0; i < digits2.Length; i++)
            {
                digits2[digits2.Length - 1 - i] = num2[i] - '0';
            }

            int[] result = new int[digits1.Length + digits2.Length];

            int carry;

            for (int i = 0; i < digits1.Length; i++)
            {
                for (int j = 0; j < digits2.Length; j++)
                {
                    result[i + j] += digits1[i] * digits2[j];
                    carry = result[i + j] / 10;
                    result[i + j] %= 10;
                    if (carry > 0)
                    {
                        result[i + j + 1] += carry;
                        carry = 0;
                    }
                }
            }

            int len = result.Length;

            int a = result.Length - 1;
            while (a >= 1)
            {
                if (result[a] == 0)
                {
                    len--;
                    a--;
                }
                else
                {
                    break;
                }
            }

            char[] ans = new char[len];

            for (int i = 0; i < len; i++)
            {
                ans[i] = (char)(result[len - 1 - i] + '0');
            }

            return string.Join("", ans);
        }

        ///44. Wildcard Matching
        ///Given an input string (s) and a pattern(p), implement wildcard pattern matching with support for '?' and '*' where:
        ///'?' Matches any single character. '*' Matches any sequence of characters (including the empty sequence).
        ///0 <= s.length, p.length <= 2000.
        public bool IsMatch_1(string s, string p)
        {
            int sCount = 0, pCount = 0, match = 0, starIdx = -1;
            while (sCount < s.Length)
            {
                // advancing both pointers
                if (pCount < p.Length && (p[pCount] == '?' || s[sCount] == p[pCount]))
                {
                    sCount++;
                    pCount++;
                }
                // * found, only advancing pattern pointer
                else if (pCount < p.Length && p[pCount] == '*')
                {
                    starIdx = pCount;
                    match = sCount;
                    pCount++;
                }
                // last pattern pointer was *, advancing string pointer
                else if (starIdx != -1)
                {
                    pCount = starIdx + 1;
                    match++;
                    sCount = match;
                }
                //current pattern pointer is not star, last patter pointer was not *
                //characters do not match
                else return false;
            }

            //check for remaining characters in pattern
            while (pCount < p.Length && p[pCount] == '*')
                pCount++;

            return pCount == p.Length;
        }

        /// 45. Jump Game II, #Greedy
        public int Jump(int[] nums)
        {
            int position = nums.Length - 1;
            int steps = 0;
            while (position != 0)
            {
                for (int j = 0; j < position; j++)
                {
                    if (nums[j] >= position - j)
                    {
                        position = j;
                        steps++;
                        break;
                    }
                }
            }
            return steps;
        }

        ///46. Permutations, #Backtracking
        ///1 <= nums.length <= 6, -10 <= nums[i] <= 10
        ///All the integers of nums are unique.
        public IList<IList<int>> Permute(int[] nums)
        {
            var res = new List<IList<int>>();
            Permute_Backtracking(new HashSet<int>(), nums, res);
            return res;
        }

        private void Permute_Backtracking(HashSet<int> set, int[] nums, List<IList<int>> res)
        {
            if (set.Count == nums.Length)
            {
                res.Add(set.ToList());
                return;
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (set.Contains(nums[i])) continue;
                set.Add(nums[i]);
                Permute_Backtracking(set, nums, res);
                set.Remove(nums[i]);
            }
        }

        ///47. Permutations II
        ///1 <= nums.length <= 8, -10 <= nums[i] <= 10
        ///Given a collection of numbers that might contain duplicates, return all possible unique permutations in any order.
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            var idxList = new List<IList<int>>();

            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();

            for (int j = 0; j < nums.Length; j++)
            {
                if (dict.ContainsKey(nums[j]))
                {
                    dict[nums[j]].Add(j);
                }
                else
                {
                    dict.Add(nums[j], new List<int>() { j });
                }
            }

            int i = 0;
            while (i < nums.Length)
            {
                var next = new List<IList<int>>();

                if (i == 0)
                {
                    for (int j = 0; j < nums.Length; j++)
                    {
                        if (dict[nums[j]].IndexOf(j) > 0)
                            continue;
                        next.Add(new List<int>() { j });
                    }
                }
                else
                {
                    for (int k = 0; k < idxList.Count; k++)
                    {
                        for (int j = 0; j < nums.Length; j++)
                        {
                            if (idxList[k].Contains(j))
                                continue;

                            if (dict[nums[j]].Count > 1)
                            {
                                var idx = dict[nums[j]].IndexOf(j);
                                int count = idxList[k].Where(x => dict[nums[j]].Contains(x)).Count();

                                if (count != idx)
                                    continue;
                            }

                            var sub = new List<int>(idxList[k])
                            {
                                j
                            };
                            next.Add(sub);
                        }
                    }
                }

                idxList = next;
                i++;
            }

            var ans = new List<IList<int>>();

            foreach (var list in idxList)
            {
                var data = new List<int>();
                foreach (var j in list)
                    data.Add(nums[j]);
                ans.Add(data);
            }

            return ans;
        }

        /// 48. Rotate Image, #Array, #Matrix
        ///You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees (clockwise).
        ///You have to rotate the image in-place, which means you have to modify the input 2D matrix directly.
        ///|1|2|3| --> |7|4|1|
        ///|4|5|6| --> |8|5|2|
        ///|7|8|9| --> |9|6|3|
        public void Rotate(int[][] matrix)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;
            int[,] cache = new int[rowLen, colLen];

            for (int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                    cache[i, j] = matrix[rowLen - 1 - j][i];

            for (int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                    matrix[i][j] = cache[i, j];
        }

        ///49. Group Anagrams, #HashMap
        //Given an array of strings strs, group the anagrams together.
        //An Anagram is a word or phrase formed by rearranging the letters of a different word or phrase,
        //eg. ["ate","eat","tea"]
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            List<IList<string>> res = new List<IList<string>>();
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
            for (int i = 0; i < strs.Length; i++)
            {
                var word = new string(strs[i].ToArray().OrderBy(c => c).ToArray());
                if (!dict.ContainsKey(word))
                    dict.Add(word, new List<int>());
                dict[word].Add(i);
            }
            foreach (var list in dict.Values)
            {
                var words = list.Select(x => strs[x]).ToList();
                res.Add(words);
            }
            return res;
        }
    }
}