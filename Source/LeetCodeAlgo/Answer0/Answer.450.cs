using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///450. Delete Node in a BST, #BST
        ///The number of nodes in the tree is in the range [0, 10^4].
        public TreeNode DeleteNode(TreeNode root, int key)
        {
            if (root == null)
                return null;
            if (root.val == key)
                return DeleteNode(root);
            else if (key > root.val)
                root.right = DeleteNode(root.right, key);
            else
                root.left = DeleteNode(root.left, key);
            return root;
        }

        private TreeNode DeleteNode(TreeNode root)
        {
            if (root.left == null)
                return root.right;
            if (root.right == null)
                return root.left;

            //insert the right subtree as the rightmost of left subtree
            //var res = root.left;
            //var node = root.left;
            //while (node.right != null)
            //{
            //    node = node.right;
            //}
            //node.right = root.right;
            //return res;

            //or, reverse, insert the left subtree as the leftmost of right subtree
            var res = root.right;
            var node = root.right;
            while (node.left != null)
            {
                node = node.left;
            }
            node.left = root.left;
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


        /// 452. Minimum Number of Arrows to Burst Balloons, #DP
        ///points.Length = Balloons number, Balloons horizontal -231 <= xstart < xend <= 231 - 1
        public int FindMinArrowShots(int[][] points)
        {
            if (points.Length <= 1)
                return points.Length;

            var arr = points.OrderBy(p => p[1]).ToList();

            var shot = 1;
            int end = arr[0][1];

            for (int i = 1; i < arr.Count; i++)
            {
                if (end < arr[i][0])
                {
                    end = arr[i][1];
                    shot++;
                }
            }

            return shot;
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
        /// 454. 4Sum II -- O(n^4) --> O(n^2), using Dictionary
        ///Given four integer arrays nums1, nums2, nums3, and nums4 all of length n,
        ///return the number of tuples (i, j, k, l) such that:nums1[i] + nums2[j] + nums3[k] + nums4[l] == 0
        public int FourSumCount(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
        {
            int ans = 0;
            Dictionary<int, int> dictSumOf34 = new Dictionary<int, int>();
            for (int i = 0; i < nums3.Length; i++)
            {
                for (int j = 0; j < nums4.Length; j++)
                {
                    int sum = nums3[i] + nums4[j];
                    if (dictSumOf34.ContainsKey(sum))
                    {
                        dictSumOf34[sum]++;
                    }
                    else
                    {
                        dictSumOf34.Add(sum, 1);
                    }
                }
            }
            for (int i = 0; i < nums1.Length; i++)
            {
                for (int j = 0; j < nums2.Length; j++)
                {
                    int sum = nums1[i] + nums2[j];
                    if (dictSumOf34.ContainsKey(-sum))
                    {
                        ans += dictSumOf34[-sum];
                    }
                }
            }
            return ans;
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

        ///456. 132 Pattern, #Monotonic Stack
        ///nums[i] < nums[k] < nums[j]. Return true if there is a 132 pattern in nums, otherwise, return false.
        public bool Find132pattern(int[] nums)
        {
            Stack<int> stack = new Stack<int>();
            int thirdElement = int.MinValue;
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                if (nums[i] < thirdElement)
                    return true;
                while (stack.Count > 0 && stack.Peek() < nums[i])
                    thirdElement = stack.Pop();
                stack.Push(nums[i]);
            }
            return false;
        }

        /// 457. Circular Array Loop
        ///If nums[i]>0, move nums[i] steps forward, If nums[i]<0 move nums[i] steps backward.
        ///Every nums[seq[j]] is either all positive or all negative.
        ///Return true if there is a cycle in nums, or false otherwise.
        public bool CircularArrayLoop(int[] nums)
        {
            bool[] visit = new bool[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                if (visit[i])
                    continue;
                bool forward = nums[i] > 0;
                visit[i] = true;

                bool[] currVisit = new bool[nums.Length];
                currVisit[i] = true;
                int lastVisit = i;

                int j = 0;
                int next = (i + nums[i]) % nums.Length;
                if (next < 0)
                    next += nums.Length;

                while (j++ < nums.Length)
                {
                    if ((forward && nums[next] < 0) || (!forward && nums[next] > 0))
                        break;
                    if (currVisit[next] && next != lastVisit)
                        return true;
                    if (visit[next])
                        break;
                    visit[next] = true;
                    currVisit[next] = true;
                    lastVisit = next;
                    next = (next + nums[next]) % nums.Length;
                    if (next < 0)
                        next += nums.Length;
                }
            }
            return false;
        }

        ///458. Poor Pigs
        public int PoorPigs(int buckets, int minutesToDie, int minutesToTest)
        {
            //if 1 bucket, return 0
            //int pigs = 0;
            //while (Math.Pow(minutesToTest / minutesToDie + 1, pigs) < buckets)
            //{
            //    pigs += 1;
            //}
            //return pigs;
            return (int)Math.Ceiling(Math.Log( buckets, minutesToTest / minutesToDie + 1));
        }

        ///459. Repeated Substring Pattern
        ///check if s can be constructed by taking a substring of it and appending multiple copies of the substring together.
        public bool RepeatedSubstringPattern(string s)
        {
            for (int i = 1; i <= s.Length / 2; i++)
            {
                if (s.Length % i != 0) continue;
                var sub = s.Substring(0, i);
                if (!s.StartsWith(sub)) return false;
                var curr = s.Substring(i);
                while (curr.Length >= i)
                {
                    if (curr == sub) return true;
                    if (curr.StartsWith(sub)) curr = curr.Substring(i);
                    else break;
                }
            }
            return false;
        }

        ///460. LFU Cache, see LFUCache

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

        ///462. Minimum Moves to Equal Array Elements II
        //move to median
        public int MinMoves2(int[] nums)
        {
            int n = nums.Length;
            Array.Sort(nums);
            int median = n % 2 == 0 ? (nums[n / 2] + nums[n / 2 + 1]) / 2 : nums[n / 2];
            return nums.Select(x => Math.Abs(x - median)).Sum();
        }

        public int MinMoves2_TwoPointers(int[] nums)
        {
            Array.Sort(nums);
            int i = 0, j = nums.Length - 1;
            int count = 0;
            while (i < j)
            {
                count += nums[j] - nums[i];
                i++;
                j--;
            }
            return count;
        }

        ///463. Island Perimeter, #BFS
        ///Determine the perimeter of the island.
        public int IslandPerimeter(int[][] grid)
        {
            int ans = 0;
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            int row = -1;
            int col = -1;
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                if (row != -1) break;
            }
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            bool[,] visit = new bool[rowLen, colLen];
            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { row, col });
            visit[row, col] = true;
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                int count = 0;
                foreach (var d in dxy4)
                {
                    var r = p[0] + d[0];
                    var c = p[1] + d[1];
                    if (r >= 0 && r < rowLen && c >= 0 && c < colLen && grid[r][c] == 1)
                    {
                        if (!visit[r, c])
                        {
                            visit[r, c] = true;
                            queue.Enqueue(new int[] { r, c });
                        }
                    }
                    else
                    {
                        count++;
                    }
                }
                ans += count;
            }
            return ans;
        }

        ///467. Unique Substrings in Wraparound String, #DP
        //We define the string s to be the infinite wraparound string of "abcdefghijklmnopqrstuvwxyz",
        //so s will look like this: "...zabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcd....".
        //Given a string p, return the number of unique non-empty substrings of p are present in s.
        public int FindSubstringInWraproundString(string p)
        {
            int[] dp = new int[26];//store longest length of strings which endWith char i+'a'
            int prev = p[0]-'a'-1;
            int count = 0;
            for(int i = 0; i < p.Length; i++)
            {
                int curr = p[i] - 'a';
                if(curr== (prev + 1) % 26)
                    count++;
                else
                    count = 1;
                dp[curr] = Math.Max(dp[curr], count);
                prev = curr;
            }
            return dp.Sum();
        }

        ///468. Validate IP Address
        public string ValidIPAddress(string queryIP)
        {
            if (IsIPv4Address(queryIP))
                return "IPv4";
            if (IsIPv6Address(queryIP))
                return "IPv6";
            return "Neither";
        }

        public bool IsIPv4Address(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            //must be digit char or dot(.)
            if (str.Any(x => x != '.' && !char.IsDigit(x)))
                return false;

            var arr = str.Split('.');
            //must 4 divs
            if (arr.Length != 4)
                return false;

            //every div must 1-3 digits, nor leading zero '0'
            if (arr.Any(x => x.Length == 0 || x.Length > 3 || (x.Length > 1 && x[0] == '0')))
                return false;

            //must in range [0,255]
            if (
                arr.Any(x =>
                {
                    var i = int.Parse(x);
                    return i < 0 || i > 255;
                })
            )
                return false;

            return true;
        }

        public bool IsIPv6Address(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            var allHexSet = new HashSet<char>()
            {
                '0', '1','2','3','4','5','6','7','8','9',
                'A','B','C','D','E','F',
                'a','b','c','d','e','f',
            };

            //must be hex char or :
            if (str.Any(x => x != ':' && !allHexSet.Contains(x)))
                return false;

            var arr = str.Split(':');
            //must 8 divs
            if (arr.Length != 8)
                return false;

            //every div must 1-4 chars
            if (arr.Any(x => x.Length == 0 || x.Length > 4))
                return false;

            return true;
        }

        ///472. Concatenated Words, #DP, #BFS
        ///Given an array of strings words (without duplicates), return all the concatenated words in the given list of words.
        ///A concatenated word is defined as a string that is comprised entirely of at least two shorter words in the given array.

        public IList<string> FindAllConcatenatedWordsInADict_DP(string[] words)
        {
            words = words.OrderBy(x => x.Length).Where(x => x.Length > 0).ToArray();

            List<string> ans = new List<string>();
            HashSet<string> set = new HashSet<string>();
            foreach (var word in words)
            {
                bool[] dp = new bool[word.Length + 1];
                dp[0] = true;
                for (int i = 1; i <= word.Length; i++)
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (dp[j] && set.Contains(word.Substring(j, i - j)))
                        {
                            dp[i] = true;
                            break;
                        }
                    }
                }
                if (dp[word.Length]) ans.Add(word);
                set.Add(word);
            }
            return ans;
        }

        public List<string> FindAllConcatenatedWordsInADict(string[] words)
        {
            words = words.OrderBy(x => x.Length).Where(x => x.Length > 0).ToArray();
            List<string> ans = new List<string>();
            HashSet<string> set = new HashSet<string>(words);
            foreach (var word in words)
            {
                if (FindAllConcatenatedWordsInADict_dfs(word, word, set, ""))
                    ans.Add(word);
            }
            return ans;
        }

        private bool FindAllConcatenatedWordsInADict_dfs(string word, string origin, HashSet<string> set, string previous)
        {
            if (!string.IsNullOrEmpty(previous)) set.Add(previous);
            if (word != origin && set.Contains(word)) return true;

            for (int i = 1; i < word.Length; i++)
            {
                string prefix = word.Substring(0, i);
                if (set.Contains(prefix) &&
                    FindAllConcatenatedWordsInADict_dfs(word.Substring(i, word.Length - i), origin, set, previous + prefix))
                {
                    return true;
                }
            }
            return false;
        }

        ///473. Matchsticks to Square, #Backtracking
        //matchsticks.Length <=15 , split to 4 equal subarray
        public bool Makesquare(int[] matchsticks)
        {
            //skip invalid as soon as possible by sort descending
            matchsticks = matchsticks.OrderBy(x => -x).ToArray();
            int sum = matchsticks.Sum();
            if (sum % 4 != 0) return false;
            return Makesquare_dfs(matchsticks, new int[4], 0, sum / 4);
        }

        private bool Makesquare_dfs(int[] matchsticks, int[] arr , int index, int target)
        {
            if (index == matchsticks.Length)
            {
                return !arr.Any(x => x != target);
            }
            else
            {
                for(int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] + matchsticks[index] > target) continue;
                    arr[i] += matchsticks[index];
                    if (Makesquare_dfs(matchsticks, arr, index + 1, target))
                        return true;
                    arr[i] -= matchsticks[index];
                }
                return false;
            }
        }

        ///474. Ones and Zeroes, #DP
        ///Return the size of largest subset of strs such that at most m 0's and n 1's in the subset.
        public int FindMaxForm(string[] strs, int m, int n)
        {
            int[,] dp = new int[m + 1, n + 1];
            foreach (var s in strs)
            {
                int count0 = s.Count(x => x == '0');
                int count1 = s.Length - count0;
                for (int i = m; i >= count0; i--)
                {
                    for (int j = n; j >= count1; j--)
                    {
                        dp[i, j] = Math.Max(dp[i, j], 1 + dp[i - count0, j - count1]);
                    }
                }
            }
            return dp[m, n];
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
        ///477. Total Hamming Distance
        //The Hamming distance between two integers is the number of positions at which the corresponding bits are different.
        //return the sum of Hamming distances between all the pairs of the integers in nums.
        //0 <= nums[i] <= 109
        public int TotalHammingDistance(int[] nums)
        {
            int res = 0;
            int[] ones = new int[32];
            foreach (var n in nums)
            {
                for (int i = 0; i < 31; i++)
                {
                    if ((n & (1 << i)) != 0) ones[i]++;
                }
            }

            for (int i = 0; i < 31; i++)
                res += ones[i] * (nums.Length - ones[i]);
            return res;
        }

        /// 482. License Key Formatting
        ///Return the reformatted license key.
        public string LicenseKeyFormatting(string s, int k)
        {
            List<char> list = new List<char>();
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] != '-')
                {
                    list.Insert(0, char.ToUpper(s[i]));
                    if (list.Count % (k + 1) == k) list.Insert(0, '-');
                }
            }
            if (list.Count > 0 && list[0] == '-') list.RemoveAt(0);
            return new string(list.ToArray());
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

        /// 492
        public int[] ConstructRectangle(int area)
        {
            int[] result = new int[2] { area, 1 };
            int min = (int)Math.Sqrt(area);
            for (int len = min; len < area; len++)
            {
                if (area % len == 0)
                {
                    if (len >= area / len)
                    {
                        return new int[2] { len, area / len };
                    }
                }
            }
            return result;
        }

        ///494. Target Sum, #HashSet, #BackTracking
        ///build an expression out of nums by adding one of the symbols '+' and '-'
        ///before each integer in nums and then concatenate all the integers.
        ///Return the number of different expressions that you can build, which evaluates to target.
        public int FindTargetSumWays(int[] nums, int target)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            dict.Add(0, 1);//seed data = 1
            foreach (var n in nums)
            {
                Dictionary<int, int> next = new Dictionary<int, int>();
                foreach (var k in dict.Keys)
                {
                    if (next.ContainsKey(k + n)) next[k + n] += dict[k];
                    else next.Add(k + n, dict[k]);

                    if (next.ContainsKey(k - n)) next[k - n] += dict[k];
                    else next.Add(k - n, dict[k]);
                }
                dict = next;
            }
            return dict.ContainsKey(target) ? dict[target] : 0;
        }

        public int FindTargetSumWays_BackTracking(int[] nums, int target)
        {
            int res = 0;
            FindTargetSumWays_BackTracking(nums, target, 0, ref res);
            return res;
        }

        private void FindTargetSumWays_BackTracking(int[] nums, int target, int index, ref int res)
        {
            if (index == nums.Length)
            {
                if (target == 0) res++;
                return;
            }
            FindTargetSumWays_BackTracking(nums, target + nums[index], index + 1, ref res);
            FindTargetSumWays_BackTracking(nums, target - nums[index], index + 1, ref res);
        }

        //495
        public int FindPoisonedDuration(int[] timeSeries, int duration)
        {
            if (timeSeries == null || timeSeries.Length == 0) return 0;

            Array.Sort(timeSeries);

            int begin = timeSeries[0];
            int expired = begin + duration;

            int total = 0;
            for (int i = 1; i < timeSeries.Length; i++)
            {
                if (timeSeries[i] < expired)
                {
                    expired = timeSeries[i] + duration;
                }
                else
                {
                    total += expired - begin;

                    begin = timeSeries[i];
                    expired = begin + duration;
                }
            }

            total += expired - begin;
            return total;
        }

        ///496. Next Greater Element I
        ///Return an array ans of length nums1.length such that ans[i] is the next greater element as described above.
        public int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums2.Length; i++)
                dict.Add(nums2[i], i);

            int[] ans = new int[nums1.Length];
            for (int i = 0; i < nums1.Length; i++)
            {
                int k = -1;
                for (int j = dict[nums1[i]] + 1; j < nums2.Length; j++)
                {
                    if (nums2[j] > nums1[i])
                    {
                        k = nums2[j];
                        break;
                    }
                }
                ans[i] = k;
            }
            return ans;
        }

        ///498. Diagonal Traverse
        public int[] FindDiagonalOrder(int[][] mat)
        {
            int m = mat.Length;
            int n = mat[0].Length;
            int[] res = new int[m * n];
            int row = 0, col = 0, d = 0;
            int[][] dirs = new int[][] { new int[] { -1, 1 }, new int[] { 1, -1 } };
            for (int i = 0; i < m * n; i++)
            {
                res[i] = mat[row][col];
                row += dirs[d][0];
                col += dirs[d][1];
                if (col >= n)
                {
                    col = n - 1;
                    row += 2;
                    d = 1 - d;
                }
                if (row >= m)
                {
                    row = m - 1;
                    col += 2;
                    d = 1 - d;
                }
                if (row < 0)
                {
                    row = 0;
                    d = 1 - d;
                }
                if (col < 0)
                {
                    col = 0;
                    d = 1 - d;
                }
            }
            return res;
        }
    }
}