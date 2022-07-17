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

        ///609. Find Duplicate File in System
        public IList<IList<string>> FindDuplicate(string[] paths)
        {
            var dict = new Dictionary<string, IList<string>>();

            foreach(var p in paths)
            {
                var arr = p.Split(' ');
                var root = arr[0];

                for(int i=1;i<arr.Length; i++)
                {
                    var j = arr[i].IndexOf('(');

                    var file = arr[i].Substring(0, j);
                    var content = arr[i].Substring(j+1, arr[i].Length-j-1);
                    if (!dict.ContainsKey(content)) dict.Add(content, new List<string>());
                    dict[content].Add(root + "/" + file);
                }
            }
            return dict.Values.Where(x=>x.Count>1).ToList();
        }
        /// 611. Valid Triangle Number, #Two Pointers, #Binary Search
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


        ///629. K Inverse Pairs Array, #DP
        //an inverse pair is a pair of integers [i, j] where 0 <= i < j < nums.length and nums[i] > nums[j].
        //Given two integers n and k, return the number of different arrays consist of numbers from 1 to n
        //such that there are exactly k inverse pairs.return it modulo 10^9 + 7.
        public int KInversePairs(int n, int k)
        {
            long mod = 10_0000_0007;
            if (k > n * (n - 1) / 2 || k < 0)
                return 0;
            if (k == 0 || k == n * (n - 1) / 2)
                return 1;

            long[][] dp = new long[n + 1][];
            for (int i = 0; i < dp.Length; i++)
                dp[i] = new long[k + 1];

            dp[2][0] = 1;
            dp[2][1] = 1;
            for (int i = 3; i <= n; i++)
            {
                dp[i][0] = 1;
                for (int j = 1; j <= Math.Min(k, i * (i - 1) / 2); j++)
                {
                    dp[i][j] = dp[i][j - 1] + dp[i - 1][j];
                    if (j >= i) dp[i][j] -= dp[i - 1][j - i];
                    dp[i][j] = (dp[i][j] + mod) % mod;
                }
            }
            return (int)dp[n][k];
        }

        ///630. Course Schedule III, #PriorityQueue
        //courses[i] = [durationi, lastDayi] indicate that the ith course should be taken continuously
        //for durationi days and must be finished before or on lastDayi.
        //You will start on the 1st day and you cannot take two or more courses simultaneously.
        //Return the maximum number of courses that you can take.
        public int scheduleCourse(int[][] courses)
        {
            courses = courses.OrderBy(x => x[1]).ToArray();//sort by lastDayi
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            int time = 0;
            foreach (var  c in courses)
            {
                time += c[0];
                pq.Enqueue(c[0], -c[0]); // add current course to max heap
                //If time exceeds, drop the previous course which costs the most time. (That must be the best choice!)
                if (time > c[1]) time -= pq.Dequeue();
            }
            return pq.Count;
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

        ///640. Solve the Equation
        //Input: equation = "x+5-3+x=6+x-2" Output: "x=2"
        //Input: equation = "x=x" Output: "Infinite solutions"
        public string SolveEquation(string equation)
        {
            var arr = equation.Split('=');
            string leftStr = arr[0];
            string rightStr = arr[1];
            int xLeft = 0;
            int sumLeft = 0;
            int xRight = 0;
            int sumRight = 0;
            int sign = 1;
            int start = 0;
            int end = 0;
            string str = "";
            for(; end < leftStr.Length; end++)
            {
                if(leftStr[end] == '+' || leftStr[end] == '-')
                {
                    str = leftStr.Substring(start, end - start);// [start,end-1]
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (str.Last() == 'x')
                        {
                            xLeft += (str.Length == 1 ? 1 : int.Parse(str.Substring(0, str.Length - 1))) * sign;
                        }
                        else
                        {
                            sumLeft += int.Parse(str) * sign;
                        }
                    }
                    sign = leftStr[end] == '-' ? -1 : 1;
                    start = end + 1;
                }
            }
            str = leftStr.Substring(start, end - start);// [start,end-1]
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Last() == 'x')
                {
                    xLeft += (str.Length == 1 ? 1 : int.Parse(str.Substring(0, str.Length - 1))) * sign;
                }
                else
                {
                    sumLeft += int.Parse(str) * sign;
                }
            }

            start = 0;
            end = 0;
            sign = 1;
            for (; end < rightStr.Length; end++)
            {
                if (rightStr[end] == '+' || rightStr[end] == '-')
                {
                    str = rightStr.Substring(start, end - start);// [start,end-1]
                    if (!string.IsNullOrEmpty(str))
                    {
                        if (str.Last() == 'x')
                        {
                            xRight += (str.Length == 1 ? 1 : int.Parse(str.Substring(0, str.Length - 1))) * sign;
                        }
                        else
                        {
                            sumRight += int.Parse(str) * sign;
                        }
                    }
                    sign = rightStr[end] == '-' ? -1 : 1;
                    start = end + 1;
                }
            }
            str = rightStr.Substring(start, end - start);// [start,end-1]
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Last() == 'x')
                {
                    xRight += (str.Length == 1 ? 1 : int.Parse(str.Substring(0, str.Length - 1))) * sign;
                }
                else
                {
                    sumRight += int.Parse(str) * sign;
                }
            }

            int xCount = xLeft - xRight;
            int sum = sumRight - sumLeft;
            if (xCount == 0)
            {
                if (sum != 0) return "No solution";
                else return "Infinite solutions";
            }
            return $"x={sum/xCount}";
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

        /// 648. Replace Words, #Trie
        // when the root "an" is followed by the successor word "other", we can form a new word "another".
        //If a successor can be replaced by more than one root, replace it with the root that has the shortest length.
        //Return the sentence after the replacement.
        public string ReplaceWords(IList<string> dictionary, string sentence)
        {
            var root = new TrieItem();
            foreach(var word in dictionary)
            {
                var curr = root;
                foreach(var c in word)
                {
                    if (!curr.dict.ContainsKey(c)) curr.dict.Add(c, new TrieItem());
                    curr = curr.dict[c];
                }
                curr.exist = true;
                curr.word = word;
            }

            var list = sentence.Split(' ').Where(x => x.Length > 0).ToList();
            var res = new List<string>();
            foreach (var word in list)
            {
                var curr = root;
                foreach(var c in word)
                {
                    if (!string.IsNullOrEmpty(curr.word)) break;
                    if (!curr.dict.ContainsKey(c)) break;
                    curr = curr.dict[c];
                }
                if (!string.IsNullOrEmpty(curr.word)) res.Add(curr.word);
                else res.Add(word);
            }
            return string.Join(" ", res);
        }

    }
}