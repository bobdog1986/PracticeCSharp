using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy
{
    public partial class Easy
    {
        /// 1. Two Sum
        // return indices of the two numbers such that they add up to target.
        public int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(target - nums[i]))
                    return new int[2] { dict[target - nums[i]], i };
                if (!dict.ContainsKey(nums[i]))
                    dict.Add(nums[i], i);
            }
            return new int[2];
        }

        ///58. Length of Last Word
        public int LengthOfLastWord(string s)
        {
            var arr = s.Split(' ').Where(x => x.Length > 0).ToArray();
            if (arr.Length == 0) return 0;
            else return arr.Last().Length;
        }

        /// 136. Single Number
        // non-empty array nums, every element appears twice except for one.Find that single one.
        public int SingleNumber_136(int[] nums)
        {
            return nums.Aggregate((x, y) => x ^ y);
        }

        ///137. Single Number II
        //array nums where every element appears three times except for one, which appears exactly once.
        //Find the single element and return it.
        public int SingleNumber_137(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, 0);
                dict[n]++;
            }
            return dict.Keys.First(x => dict[x] != 3);
        }

        ///151. Reverse Words in a String
        //Given an input string s, reverse the order of the words.
        public string ReverseWords(string s)
        {
            return string.Join(' ', s.Split(' ').Where(x => x.Length > 0).Reverse());
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

        //203. Remove Linked List Elements
        public ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null) return null;
            if (head.val == val) return RemoveElements(head.next, val);
            var prev = head;
            var curr = head.next;
            while (curr != null)
            {
                if (curr.val == val)
                {
                    prev.next = curr.next;
                }
                else prev = curr;
                curr = curr.next;
            }
            return head;
        }

        ///205. Isomorphic Strings
        //Given two strings s and t, determine if they are isomorphic.
        //Two strings s and t are isomorphic if the characters in s can be replaced to get t.
        public bool IsIsomorphic(string s, string t)
        {
            Dictionary<char, char> dict = new Dictionary<char, char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]))
                {
                    if (dict[s[i]] != t[i]) return false;
                }
                else
                {
                    if (dict.ContainsValue(t[i])) return false;
                    else dict.Add(s[i], t[i]);
                }
            }
            return true;
        }

        ///215. Kth Largest Element in an Array, #PriorityQueue
        //return the kth largest element in the array. -10^4 <= nums[i] <= 10^4
        public int FindKthLargest(int[] nums, int k)
        {
            var pq = new PriorityQueue<int, int>();
            foreach (var n in nums)
            {
                if (pq.Count < k) pq.Enqueue(n, n);
                else
                {
                    if (n > pq.Peek())
                    {
                        pq.Enqueue(n, n);
                        pq.Dequeue();
                    }
                }
            }
            return pq.Peek();
        }

        ///222. Count Complete Tree Nodes, #BTree
        ///Given the root of a complete binary tree, return the number of the nodes in the tree.
        public int CountNodes_recur(TreeNode root)
        {
            if (root == null) return 0;
            return 1 + CountNodes_recur(root.left) + CountNodes_recur(root.right);
        }

        public int CountNodes(TreeNode root)
        {
            if (root == null) return 0;
            int res = 0;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count > 0)
            {
                var top = q.Dequeue();
                res++;
                if (top.left != null) q.Enqueue(top.left);
                if (top.right != null) q.Enqueue(top.right);
            }
            return res;
        }

        ///223. Rectangle Area
        public int ComputeArea(int ax1, int ay1, int ax2, int ay2, int bx1, int by1, int bx2, int by2)
        {
            if (ax1 > bx1)
                return ComputeArea(bx1, by1, bx2, by2, ax1, ay1, ax2, ay2);
            int res = ComputeArea_Rect(ax1, ay1, ax2, ay2) + ComputeArea_Rect(bx1, by1, bx2, by2);
            if (by1 >= ay2 || by2 <= ay1 || bx1 >= ax2)
            {
                //if no overlay
            }
            else
            {
                if (bx2 <= ax2)
                {
                    if (by1 <= ay1 && by2 >= ay2)
                    {
                        res -= ComputeArea_Rect(bx1, ay1, bx2, ay2);
                    }
                    else if (by1 <= ay1)
                    {
                        res -= ComputeArea_Rect(bx1, ay1, bx2, by2);
                    }
                    else if (by2 >= ay2)
                    {
                        res -= ComputeArea_Rect(bx1, by1, bx2, ay2);
                    }
                    else
                    {
                        res -= ComputeArea_Rect(bx1, by1, bx2, by2);
                    }
                }
                else
                {
                    if (by1 <= ay1 && by2 >= ay2)
                    {
                        res -= ComputeArea_Rect(bx1, ay1, ax2, ay2);
                    }
                    else if (by1 <= ay1)
                    {
                        res -= ComputeArea_Rect(bx1, ay1, ax2, by2);
                    }
                    else if (by2 >= ay2)
                    {
                        res -= ComputeArea_Rect(bx1, by1, ax2, ay2);
                    }
                    else
                    {
                        res -= ComputeArea_Rect(bx1, by1, ax2, by2);
                    }
                }
            }

            return res;
        }

        private int ComputeArea_Rect(int ax1, int ay1, int ax2, int ay2)
        {
            return (ax2 - ax1) * (ay2 - ay1);
        }

        /// 231. Power of Two
        //Given an int n, return true if it is a power of two.
        public bool IsPowerOfTwo(int n)
        {
            if (n <= 0)
                return false;
            while (n >= 1)
            {
                if (n == 1)
                    return true;
                if (n % 2 == 1)
                    return false;
                n = n / 2;
            }
            return false;
        }

        ///260. Single Number III
        //exactly two elements appear only once and all the other elements appear exactly twice.
        //Find the two elements that appear only once. You can return the answer in any order.
        public int[] SingleNumber(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, 0);
                dict[n]++;
            }
            return dict.Keys.Where(k => dict[k] == 1).ToArray();
        }

        ///268. Missing Number
        //Given an array nums containing n distinct numbers in the range [0, n],
        //return the only number in the range that is missing from the array.
        public int MissingNumber(int[] nums)
        {
            bool[] arr = new bool[nums.Length + 1];
            foreach (var num in nums)
                arr[num] = true;
            for (int i = 0; i < arr.Length; i++)
            {
                if (!arr[i])
                    return i;
            }
            return -1;
        }

        /// 278. First Bad Version, #Binary Search
        // 1 <= bad <= n <= 2^31 - 1,all the versions after a bad version are also bad.
        public int FirstBadVersion(int n)
        {
            int left = 1;
            int right = n;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (IsBadVersion(mid))
                    right = mid;
                else
                    left = mid + 1;
            }
            return left;
        }

        private bool IsBadVersion(int n)//API provides by leetcode
        {
            return (n >= 1702766719);
        }

        /// 287. Find the Duplicate Number
        //nums containing n + 1 integers where each integer is in the range [1, n] inclusive.
        //There is only one repeated number in nums, return this repeated number.
        public int FindDuplicate(int[] nums)
        {
            int[] arr = new int[nums.Length + 1];
            foreach (var n in nums)
            {
                if (arr[n] == 1) return n;
                arr[n] = 1;
            }
            return 0;
        }

        ///292. Nim Game
        //Given n, the number of stones in the heap, return true if you can win the game
        //On each turn, the person whose turn it is will remove 1 to 3 stones from the heap.
        //The one who removes the last stone is the winner.
        //assuming both you and your friend play optimally, otherwise return false.
        public bool CanWinNim(int n)
        {
            return n % 4 != 0;
        }

        /// 319. Bulb Switcher
        public int BulbSwitch(int n)
        {
            int res = 0;
            for (int i = 1; i * i <= n; i++)
                res++;
            return res;
        }

        /// 326. Power of Three
        //Given an integer n, return true if it is a power of three. Otherwise, return false.
        //An integer n is a power of three, if there exists an integer x such that n == 3x.
        public bool IsPowerOfThree(int n)
        {
            while (n >= 3)
            {
                if (n % 3 != 0)
                    return false;
                n /= 3;
            }
            return n == 1;
        }

        /// 344. Reverse String
        // You must do this by modifying the input array in-place with O(1) extra memory.
        public void ReverseString(char[] s)
        {
            for (int i = 0; i < s.Length / 2; i++)
            {
                char temp = s[i];
                s[i] = s[s.Length - 1 - i];
                s[s.Length - 1 - i] = temp;
            }
        }

        ///349. Intersection of Two Arrays
        //return an array of their intersection. Each element in the result must be unique
        public int[] Intersection(int[] nums1, int[] nums2)
        {
            HashSet<int> set1 = new HashSet<int>(nums1);
            HashSet<int> set2 = new HashSet<int>(nums2);
            return set2.Where(x => set1.Contains(x)).ToArray();
        }

        /// 367. Valid Perfect Square, #Binary Search
        /// Given a positive integer num, write a function which returns True if num is a perfect square else False.
        public bool IsPerfectSquare(int num)
        {
            long left = 1;
            long right = num;
            while (left <= right)
            {
                long mid = (left + right) / 2;
                long pow = mid * mid;
                if (pow == num) return true;
                else if (pow > num) right = mid - 1;
                else left = mid + 1;
            }
            return false;
        }

        ///383. Ransom Note
        //return true if ransomNote can be constructed from magazine and false otherwise.
        //Each letter in magazine can only be used once in ransomNote.
        public bool CanConstruct(string ransomNote, string magazine)
        {
            int[] arr = new int[26];
            foreach (var c in magazine)
                arr[c - 'a']++;
            foreach (var c in ransomNote)
                if (arr[c - 'a']-- == 0) return false;
            return true;
        }

        ///387. First Unique Character in a String
        //find the first non-repeating character in it and return its index. If it does not exist, return -1.
        public int FirstUniqChar(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i])) dict[s[i]] = -1;
                else dict.Add(s[i], i);
            }

            var indexes = dict.Keys.Where(x => dict[x] != -1).Select(x => dict[x]).OrderBy(x => x).ToList();
            if (indexes.Count == 0) return -1;
            else return indexes[0];
        }

        ///389. Find the Difference
        ///String t is generated by random shuffling string s and then add one more letter at a random position.
        ///Return the letter that was added to t.
        public char FindTheDifference(string s, string t)
        {
            int[] arr = new int[26];
            foreach (var c in s)
                arr[c - 'a']--;
            foreach (var c in t)
                arr[c - 'a']++;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                    return (char)(i + 'a');
            }
            return 'a';
        }

        ///412. Fizz Buzz
        ///Given an integer n, return a string array answer (1-indexed) where:
        ///answer[i] == "FizzBuzz" if i is divisible by 3 and 5.
        ///answer[i] == "Fizz" if i is divisible by 3.
        ///answer[i] == "Buzz" if i is divisible by 5.
        ///answer[i] == i(as a string) if none of the above conditions are true.
        public IList<string> FizzBuzz(int n)
        {
            var res = new string[n + 1];
            for (int i = 1; i <= n; i++)
            {
                if (i % 3 == 0 && i % 5 == 0) res[i] = "FizzBuzz";
                else if (i % 3 == 0) res[i] = "Fizz";
                else if (i % 5 == 0) res[i] = "Buzz";
                else res[i] = i.ToString();
            }
            return res.Skip(1).ToList();
        }

        ///414. Third Maximum Number,#PriorityQueue
        ///Given an integer array nums, return the third distinct maximum number in this array.
        ///If the third maximum does not exist, return the maximum number.
        public int ThirdMax(int[] nums)
        {
            var pq = new PriorityQueue<int, long>();
            var set = new HashSet<int>();
            foreach (var x in nums)
            {
                if (!set.Contains(x))
                    pq.Enqueue(x, 0l - x);
                set.Add(x);
            }
            if (set.Count < 3) return pq.Dequeue();
            pq.Dequeue();
            pq.Dequeue();
            return pq.Dequeue();
        }

        ///423. Reconstruct Original Digits from English
        //Given a string s = out-of-order English representation of digits 0-9, return the digits in ascending.
        public string OriginalDigits(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>()
            {
                {'e',0 },{'g',0 },{'f',0 },{'i',0 },{'h',0 },
                {'o',0 },{'n',0 },{'s',0 },{'r',0 },{'u',0 },
                {'t',0 },{'w',0 },{'v',0 },{'x',0 },{'z',0 },
            };
            foreach (var c in s)
                dict[c]++;
            List<char> res = new List<char>();
            //round1, eg only "six" contains 'x'
            OriginalDigits_Remove('x', '6', "six", dict, res);
            OriginalDigits_Remove('w', '2', "two", dict, res);
            OriginalDigits_Remove('g', '8', "eight", dict, res);
            OriginalDigits_Remove('u', '4', "four", dict, res);
            OriginalDigits_Remove('z', '0', "zero", dict, res);
            //round2, eg. both "six" and "seven" contains 's', but now only "seven" contains 's'
            OriginalDigits_Remove('s', '7', "seven", dict, res);
            OriginalDigits_Remove('t', '3', "three", dict, res);//'h','r'
            OriginalDigits_Remove('f', '5', "five", dict, res);//skip 'v'
            OriginalDigits_Remove('o', '1', "one", dict, res);
            //round3
            OriginalDigits_Remove('i', '9', "nine", dict, res);//n
            return new string(res.OrderBy(x => x).ToArray());
        }

        private void OriginalDigits_Remove(char c, char d, string str, Dictionary<char, int> dict, List<char> res)
        {
            if (dict[c] > 0)
            {
                int count = dict[c];
                var map = new Dictionary<char, int>();
                foreach (var i in str)
                {
                    if (!map.ContainsKey(i)) map.Add(i, 0);
                    map[i]++;
                }
                foreach (var k in map.Keys)
                    dict[k] -= map[k] * count;
                res.AddRange(Enumerable.Repeat(d, count));
            }
        }

        /// 434. Number of Segments in a String
        //return the number of segments , segment is a contiguous sequence of non-space characters.
        public int CountSegments(string s)
        {
            return s.Split(' ').Where(x => x.Length > 0).Count();
        }

        ///442. Find All Duplicates in an Array
        ///Given an integer array nums of length n where all of nums in the range [1, n]
        ///and each integer appears once or twice, return an array of all the integers that appears twice.
        public IList<int> FindDuplicates(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, 0);
                dict[n]++;
            }
            return dict.Keys.Where(k => dict[k] == 2).ToList();
        }

        ///447. Number of Boomerangs
        //Return the number of points (i, j, k) that the distance between i and j equal to i and k
        public int NumberOfBoomerangs(int[][] points)
        {
            int res = 0;
            int n = points.Length;
            Dictionary<int, int>[] mapArr = new Dictionary<int, int>[n];
            for (int i = 0; i < n; i++)
                mapArr[i] = new Dictionary<int, int>();
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int[] p1 = points[i];
                    int[] p2 = points[j];
                    int dist = (p1[0] - p2[0]) * (p1[0] - p2[0]) + (p1[1] - p2[1]) * (p1[1] - p2[1]);
                    var map1 = mapArr[i];
                    var map2 = mapArr[j];
                    if (!map1.ContainsKey(dist)) map1.Add(dist, 0);
                    map1[dist]++;
                    if (!map2.ContainsKey(dist)) map2.Add(dist, 0);
                    map2[dist]++;
                }
            }
            foreach (var map in mapArr)
            {
                foreach (var k in map.Keys)
                {
                    if (map[k] <= 1) continue;
                    res += map[k] * (map[k] - 1);
                }
            }
            return res;
        }

        ///451. Sort Characters By Frequency
        //Given a string s, sort it in decreasing order based on the frequency of the characters.
        //If there are multiple answers, return any of them.
        public string FrequencySort(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (!dict.ContainsKey(c)) dict.Add(c, 0);
                dict[c]++;
            }
            StringBuilder sb = new StringBuilder();
            var keys = dict.Keys.OrderBy(x => -dict[x]).ToArray();
            foreach (var k in keys)
            {
                sb.Append(new string(Enumerable.Repeat(k, dict[k]).ToArray()));
            }
            return sb.ToString();
        }

        ///453. Minimum Moves to Equal Array Elements
        //return the minimum number of moves required to make all array elements equal.
        //In one move, you can increment n - 1 elements of the array by 1.
        public int MinMoves(int[] nums)
        {
            // sum + m * (n - 1) = x * n
            // x = min + m;
            //sum+m*n-m = m*n+min*n=> m = sum- min*n;
            return nums.Sum() - nums.Min() * nums.Length;
        }

        ///455. Assign Cookies
        ///Each child i g[i] is the minimum size of a cookie that the child will be content with
        ///and each cookie j has a size s[j]. If s[j] >= g[i], we can assign the cookie j to the child i,
        ///Your goal is to maximize the number of your content children and output the maximum number.
        public int FindContentChildren(int[] g, int[] s)
        {
            Array.Sort(g);
            Array.Sort(s);
            int i = 0;
            int j = 0;
            while (i < g.Length && j < s.Length)
            {
                if (g[i] <= s[j++])
                    i++;
            }
            return i;
        }

        /// 461. Hamming Distance
        ///return the Hamming distance between two integers, count of bits are different.
        public int HammingDistance(int x, int y)
        {
            int count = 0;
            while (x != 0 && y != 0)
            {
                if ((x & 1) != (y & 1))
                    count++;
                x >>= 1;
                y >>= 1;
            }
            return count;
        }

        /// 476. Number Complement
        //The complement of an integer is flip all the 0's to 1's and all the 1's to 0's
        //Eg, The integer 5 is "101" in binary and its complement is "010" which is the integer 2.
        public int FindComplement(int num)
        {
            int res = 0;
            int bit = 1;
            while (num > 0)
            {
                if ((num & 1) == 0) res += bit;
                bit <<= 1;
                num >>= 1;
            }
            return res;
        }

        /// 485. Max Consecutive Ones
        //Given a binary array nums, return the maximum number of consecutive 1's in the array.
        public int FindMaxConsecutiveOnes(int[] nums)
        {
            int max = 0;
            int count = 0;
            foreach (var n in nums)
            {
                if (n == 1) count++;
                else count = 0;
                max = Math.Max(max, count);
            }
            return max;
        }
    }
}