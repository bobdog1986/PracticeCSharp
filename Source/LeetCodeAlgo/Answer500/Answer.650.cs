using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///652. Find Duplicate Subtrees, #BTree
        ///Given the root of a binary tree, return all duplicate subtrees.
        ///For each kind of duplicate subtrees, you only need to return the root node of any one of them.
        ///Two trees are duplicate if they have the same structure with the same node values.
        public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
        {
            var dict = new Dictionary<string, List<TreeNode>>();
            FindDuplicateSubtrees_Init(root, dict);
            return dict.Where(x => x.Value.Count >= 2).Select(x => x.Value[0]).ToList();
        }

        private string FindDuplicateSubtrees_Init(TreeNode root, Dictionary<string, List<TreeNode>> dict, int min = -201)
        {
            if (root == null) return min.ToString();
            var leftStr = FindDuplicateSubtrees_Init(root.left, dict);
            var rightStr = FindDuplicateSubtrees_Init(root.right, dict);
            var str = $"{root.val}_{leftStr}_{rightStr}";
            if (!dict.ContainsKey(str)) dict.Add(str, new List<TreeNode>());
            dict[str].Add(root);
            return str;
        }

        /// 653. Two Sum IV - Input is a BST
        ///return true if there exist two elements in the BST such that sum = target.
        public bool FindTarget(TreeNode root, int k)
        {
            return FindTarget(root, k, new Dictionary<int, int>());
        }

        public bool FindTarget(TreeNode root, int k, Dictionary<int, int> dict)
        {
            if (root == null) return false;
            if (dict.ContainsKey(root.val)) return true;
            dict.Add(k - root.val, 1);
            return FindTarget(root.left, k, dict) || FindTarget(root.right, k, dict);
        }

        ///654. Maximum Binary Tree, #BTree
        public TreeNode ConstructMaximumBinaryTree(int[] nums)
        {
            return ConstructMaximumBinaryTree_Recursion(nums, 0, nums.Length - 1);
        }

        private TreeNode ConstructMaximumBinaryTree_Recursion(int[] nums, int start, int end)
        {
            if (start > end) return null;
            if (start == end) return new TreeNode(nums[start]);

            int max = -1;
            int index = -1;
            for (int i = start; i <= end; i++)
            {
                if (nums[i] > max)
                {
                    max = nums[i];
                    index = i;
                }
            }

            var node = new TreeNode(nums[index]);
            node.left = ConstructMaximumBinaryTree_Recursion(nums, start, index - 1);
            node.right = ConstructMaximumBinaryTree_Recursion(nums, index + 1, end);
            return node;
        }

        /// 657. Robot Return to Origin
        ///Return true if the robot returns to the origin after it finishes all of its moves, or false otherwise.
        public bool JudgeCircle(string moves)
        {
            int x = 0;
            int y = 0;
            foreach (var c in moves)
            {
                if (c == 'R') x++;
                else if (c == 'L') x--;
                else if (c == 'U') y++;
                else if (c == 'D') y--;
            }
            return x == 0 && y == 0;
        }

        ///658. Find K Closest Elements, #Binary Search
        ///Given a sorted integer array arr, two integers k and x,
        ///return the k closest integers to x in the array. The result should also be sorted in ascending order.
        ///An integer a is closer to x than an integer b if:|a - x| < |b - x|, or |a - x| == |b - x| and a<b
        public IList<int> FindClosestElements_BinarySearch(int[] arr, int k, int x)
        {
            int left = 0, right = arr.Length - k;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (x - arr[mid] > arr[mid + k] - x)
                    left = mid + 1;
                else
                    right = mid;
            }
            return arr.Skip(left).Take(k).ToList();
        }

        public IList<int> FindClosestElements_PQ(int[] arr, int k, int x)
        {
            var res = new List<int>();
            var pq = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) =>
            {
                if (Math.Abs(arr[a] - x) == Math.Abs(arr[b] - x))
                {
                    return a - b;
                }
                else
                {
                    return Math.Abs(arr[a] - x) - Math.Abs(arr[b] - x);
                }
            }));

            for (int i = 0; i < arr.Length; i++)
            {
                pq.Enqueue(i, i);
            }

            while (k-- > 0)
                res.Add(pq.Dequeue());

            return res.OrderBy(o => o).Select(o => arr[o]).ToList();
        }

        ///661. Image Smoother
        public int[][] ImageSmoother(int[][] img)
        {
            int m = img.Length;
            int n = img[0].Length;

            int[][] res = new int[m][];
            for (int i = 0; i < m; i++)
                res[i] = new int[n];

            for(int i = 0; i < m; i++)
            {
                for(int j=0; j < n; j++)
                {
                    int sum = 0;
                    int count = 0;
                    for(int k = -1; k <= 1; k++)
                    {
                        for(int l = -1; l <= 1; l++)
                        {
                            int r = i + k;
                            int c = j + l;
                            if(r>=0 && r<m && c>=0 && c < n)
                            {
                                sum += img[r][c];
                                count++;
                            }
                        }
                    }
                    res[i][j] = sum/count;
                }
            }
            return res;
        }
        /// 662. Maximum Width of Binary Tree
        ///The maximum width of a tree is the maximum width among all levels.
        ///The width of one level is defined as the length between the end-nodes(the leftmost and rightmost non-null nodes),
        ///where the null nodes between the end-nodes are also counted into the length calculation.
        ///The number of nodes in the tree is in the range [1, 3000].
        public int WidthOfBinaryTree(TreeNode root)
        {
            int ans = 1;
            Dictionary<int, TreeNode> dict = new Dictionary<int, TreeNode>
            {
                { 0, root }
            };
            while (dict.Count > 0)
            {
                Dictionary<int, TreeNode> next = new Dictionary<int, TreeNode>();
                int start = -1;
                int end = -1;
                foreach (var key in dict.Keys)
                {
                    var node = dict[key];
                    if (node.left != null)
                    {
                        if (start == -1) start = key * 2;
                        end = key * 2;
                        next.Add(key * 2, node.left);
                    }
                    if (node.right != null)
                    {
                        if (start == -1) start = key * 2 + 1;
                        end = key * 2 + 1;
                        next.Add(key * 2 + 1, node.right);
                    }
                }
                ans = Math.Max(ans, end - start + 1);
                dict = next;
            }
            return ans;
        }

        ///665. Non-decreasing Array
        // check if it could become non-decreasing by modifying at most one element.
        public bool CheckPossibility(int[] nums)
        {
            int prev = int.MinValue;
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < prev)
                {
                    return CheckPossibility(nums, i - 1, i) || CheckPossibility(nums, i - 2, i - 1);
                }
                prev = nums[i];
            }
            return true;
        }

        private bool CheckPossibility(int[] nums,int index ,int skip)
        {
            int prev = int.MinValue;
            for (int i = Math.Max(0, index); i < nums.Length; i++)
            {
                if (i == skip) continue;
                if (nums[i] < prev) return false;
                prev = nums[i];
            }
            return true;
        }

        ///669. Trim a Binary Search Tree, #BTree, #BST
        ///Given the root of a binary search tree, trim the tree so that all elements in [low, high].
        public TreeNode TrimBST(TreeNode root, int low, int high)
        {
            if (root == null) return null;
            if (root.val > high)
            {
                return TrimBST(root.left, low, high);
            }
            else if (root.val < low)
            {
                return TrimBST(root.right, low, high);
            }
            else
            {
                root.left = TrimBST(root.left, low, high);
                root.right = TrimBST(root.right, low, high);
                return root;
            }
        }

        /// 670. Maximum Swap
        ///You can swap two digits at most once to get the maximum valued number. Return the max
        public int MaximumSwap(int num)
        {
            List<int> list = new List<int>();
            int n = num;
            while (n > 0)
            {
                list.Add(n % 10);
                n /= 10;
            }
            if (list.Count > 1)
            {
                int start = list.Count - 1;
                while (start > 0)
                {
                    var next = new List<int>();
                    for (int i = 0; i <= start; i++)
                        next.Add(list[i]);
                    if (list[start] != next.Max()) break;
                    start--;
                }
                if (start > 0)
                {
                    int end = 0;
                    var next = new List<int>();
                    for (int i = 0; i < start; i++)
                        next.Add(list[i]);
                    while (end < next.Count)
                    {
                        if (next[end] == next.Max()) break;
                        end++;
                    }
                    num += (int)((list[end] - list[start]) * Math.Pow(10, start) - (list[end] - list[start]) * Math.Pow(10, end));
                }
            }
            return num;
        }

        ///671. Second Minimum Node In a Binary Tree, #BTree
        public int FindSecondMinimumValue(TreeNode root)
        {
            HashSet<int> set = new HashSet<int>();
            FindSecondMinimumValue(root, set);
            if (set.Count > 1)
            {
                return set.OrderBy(x => x).ToList()[1];
            }
            else
            {
                return -1;
            }
        }

        private int FindSecondMinimumValue(TreeNode root, HashSet<int> set)
        {
            if (root.left == null && root.right == null)
            {
                set.Add(root.val);
                return root.val;
            }
            else
            {
                var leftMin = FindSecondMinimumValue(root.left, set);
                var rightMin = FindSecondMinimumValue(root.right, set);
                var min = Math.Min(leftMin, rightMin);
                set.Add(min);
                return min;
            }
        }

        /// 673. Number of Longest Increasing Subsequence - not understand
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

        ///674. Longest Continuous Increasing Subsequence
        public int FindLengthOfLCIS(int[] nums)
        {
            int max = 0;
            int prev = int.MinValue;
            int curr = 0;
            foreach(var n in nums)
            {
                if (n > prev)
                {
                    curr++;
                }
                else
                {
                    curr = 1;
                }
                max = Math.Max(max, curr);
                prev = n;
            }
            return max;
        }

        /// 676. Implement Magic Dictionary, see MagicDictionary

        /// 677. Map Sum Pairs, see MapSum

        /// 678. Valid Parenthesis String
        ///Given a string s containing only three types of characters: '(', ')' and '*', return true if s is valid.
        ///'*' could be treated as a single right parenthesis ')' or a single left parenthesis '(' or an empty string "".
        public bool CheckValidString(string s)
        {
            int cmin = 0, cmax = 0; // open parentheses count in range [cmin, cmax]
            foreach (var c in s)
            {
                if (c == '(')
                {
                    cmax++;
                    cmin++;
                }
                else if (c == ')')
                {
                    cmax--;
                    cmin--;
                }
                else if (c == '*')
                {
                    cmax++; // if `*` become `(` then openCount++
                    cmin--; // if `*` become `)` then openCount--
                            // if `*` become `` then nothing happens
                            // So openCount will be in new range [cmin-1, cmax+1]
                }
                if (cmax < 0) return false; // Currently, don't have enough open parentheses to match close parentheses-> Invalid
                                            // For example: ())(
                cmin = Math.Max(cmin, 0);   // It's invalid if open parentheses count < 0 that's why cmin can't be negative
            }
            return cmin == 0; // Return true if can found `openCount == 0` in range [cmin, cmax]
        }

        ///679. 24 Game, #Backtracking
        ///Return true if you can get such expression that evaluates to 24, and false otherwise.
        public bool JudgePoint24(int[] cards)
        {
            List<double> list = new List<double>();
            foreach (int n in cards)
                list.Add(n);
            bool res = false;
            JudgePoint24_BackTracking(list, ref res);
            return res;
        }

        private void JudgePoint24_BackTracking(IList<double> list, ref bool res, double diff = 0.01)
        {
            if (res) return;
            if (list.Count == 1)
            {
                if (Math.Abs(list[0] - 24) < diff)
                    res = true;
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    double p1 = list[i];
                    double p2 = list[j];

                    //all possible combine of '+','-','*','/' between p1 and p2
                    List<double> candidates = new List<double>() { p1 + p2, p1 - p2, p2 - p1, p1 * p2 };
                    if (Math.Abs(p2) > diff) candidates.Add(p1 / p2);
                    if (Math.Abs(p1) > diff) candidates.Add(p2 / p1);

                    list.RemoveAt(i);//remove p1 at i first, then p2 at j
                    list.RemoveAt(j);
                    foreach (var n in candidates)
                    {
                        List<double> next = new List<double>(list) { n };
                        JudgePoint24_BackTracking(next, ref res);
                    }
                    list.Insert(j, p2);//if failed recovery p2 at j then p1 at i
                    list.Insert(i, p1);
                }
            }
        }

        ///680. Valid Palindrome II
        ///Given a string s, return true if the s can be palindrome after deleting at most one character from it.
        public bool ValidPalindrome(string s)
        {
            return ValidPalindrome(s, 0, s.Length - 1, true);
        }

        public bool ValidPalindrome(string s, int left, int right, bool canSkip = true)
        {
            while (left < right)
            {
                if (s[left] == s[right])
                {
                    left++;
                    right--;
                }
                else
                {
                    if (canSkip)
                    {
                        return ValidPalindrome(s, left + 1, right, false) || ValidPalindrome(s, left, right - 1, false);
                    }
                    else return false;
                }
            }
            return true;
        }

        ///682. Baseball Game
        ///"+", add sum of last two; "C"-remove the last, "D"-double the last then add
        public int CalPoints(string[] ops)
        {
            int res = 0;
            List<int> list = new List<int>();
            foreach (var op in ops)
            {
                int val = 0;
                bool isData = int.TryParse(op, out val);
                if (isData)
                {
                    res += val;
                    list.Add(val);
                }
                else
                {
                    if (op == "C")
                    {
                        res -= list[list.Count - 1];
                        list.RemoveAt(list.Count - 1);
                    }
                    else if (op == "D")
                    {
                        res += list[list.Count - 1] * 2;
                        list.Add(list[list.Count - 1] * 2);
                    }
                    else if (op == "+")
                    {
                        res += list[list.Count - 1] + list[list.Count - 2];
                        list.Add(list[list.Count - 1] + list[list.Count - 2]);
                    }
                }
            }

            return res;
        }

        ///684. Redundant Connection, #Graph,#BFS
        public int[] FindRedundantConnection(int[][] edges)
        {
            int n = edges.Length;
            HashSet<int>[] graph = new HashSet<int>[n + 1];
            for (int i = 0; i < graph.Length; i++)
                graph[i] = new HashSet<int>();

            foreach (var edge in edges)
            {
                if (FindRedundantConnection_BFS(graph, new bool[n + 1], edge[0], edge[1])) return edge;
                else
                {
                    graph[edge[0]].Add(edge[1]);
                    graph[edge[1]].Add(edge[0]);
                }
            }
            return null;
        }

        private bool FindRedundantConnection_BFS(HashSet<int>[] graph, bool[] visit, int curr, int target)
        {
            if (visit[curr]) return false;
            if (graph[curr].Contains(target)) return true;
            visit[curr] = true;
            foreach (var i in graph[curr])
            {
                if (visit[i]) continue;
                if (FindRedundantConnection_BFS(graph, visit, i, target)) return true;
            }
            return false;
        }

        ///688. Knight Probability in Chessboard, #Memoization
        public double KnightProbability(int n, int k, int row, int column)
        {
            var dxy = new int[][] { new int[] {1,2 }, new int[] { 2, 1 }, new int[] { -1, 2 }, new int[] { -2, 1 },
                                    new int[] {1,-2 }, new int[] {2,-1 }, new int[] {-1,-2 }, new int[] {-2,-1 },};

            var total = Math.Pow(8, k);
            var dict = new Dictionary<int, double>();
            dict.Add(row * 100 + column, 1);
            while (k-- > 0 && dict.Count > 0)
            {
                var next = new Dictionary<int, double>();
                foreach (var p in dict.Keys)
                {
                    foreach (var d in dxy)
                    {
                        var r = p / 100 + d[0];
                        var c = p % 100 + d[1];
                        if (r >= 0 && r < n && c >= 0 && c < n)
                        {
                            if (next.ContainsKey(r * 100 + c)) next[r * 100 + c] += dict[p];
                            else next.Add(r * 100 + c, dict[p]);
                        }
                    }
                }
                dict = next;
            }
            return dict.Values.Sum() / total;
        }

        /// 692. Top K Frequent Words, #PriorityQueue
        ///Given an array of strings words and an integer k, return the k most frequent strings.
        ///Return the answer sorted by the frequency from highest to lowest.
        ///Sort the words with the same frequency by their lexicographical order.
        public IList<string> TopKFrequent(string[] words, int k)
        {
            List<string> res = new List<string>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (dict.ContainsKey(word)) dict[word]++;
                else dict.Add(word, 1);
            }

            PriorityQueue<string, string> queue = new PriorityQueue<string, string>(
                Comparer<string>.Create((s1, s2) => dict[s1] == dict[s2] ? s1.CompareTo(s2) : dict[s2] - dict[s1]));
            foreach (var key in dict.Keys)
                queue.Enqueue(key, key);

            while (k-- > 0)
                res.Add(queue.Dequeue());

            return res;
        }

        /// 693. Binary Number with Alternating Bits
        ///Given a positive integer, check whether it has alternating bits, adjacent bits always have different values.
        public bool HasAlternatingBits(int n)
        {
            bool last = (n & 1) == 0;
            while (n > 0)
            {
                bool curr = (n & 1) == 1;
                if (curr == last) return false;
                last = curr;
                n >>= 1;
            }
            return true;
        }

        /// 695. Max Area of Island, #Graph, #DFS
        /// Return the maximum area of an island in grid. If there is no island, return 0.
        public int MaxAreaOfIsland(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            int max = 0;
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        Queue<int[]> queue = new Queue<int[]>();
                        grid[i][j] = 0;
                        int count = 1;
                        queue.Enqueue(new int[] { i, j });
                        while (queue.Count > 0)
                        {
                            var curr = queue.Dequeue();
                            foreach (var d in dxy4)
                            {
                                int r = curr[0] + d[0];
                                int c = curr[1] + d[1];
                                if (r >= 0 && r < rowLen && c >= 0 && c < colLen && grid[r][c] == 1)
                                {
                                    grid[r][c] = 0;
                                    queue.Enqueue(new int[] { r, c });
                                    count++;
                                }
                            }
                        }
                        max = Math.Max(max, count);
                    }
                }
            }
            return max;
        }

        ///696. Count Binary Substrings
        //return the number of non-empty substrings that have the same number of 0's and 1's,
        //and all the 0's and all the 1's in these substrings are grouped consecutively.
        public int CountBinarySubstrings(string s)
        {
            //First, I count the number of 1 or 0 grouped consecutively.
            //For example "0110001111" will be[1, 2, 3, 4].
            //Second, for any possible substrings with 1 and 0 grouped consecutively,
            //the number of valid substring will be the minimum number of 0 and 1.
            //For example "0001111", will be min(3, 4) = 3, ("01", "0011", "000111")
            int curr = 1;
            int prev = 0;
            int res = 0;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == s[i - 1]) curr++;
                else
                {
                    res += Math.Min(curr, prev);
                    prev = curr;
                    curr = 1;
                }
            }
            res += Math.Min(curr, prev);
            return res;
        }

        /// 697
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

        /// 698. Partition to K Equal Sum Subsets
        ///return true if it is possible to divide this array into k non-empty subsets whose sums are all equal.
        ///1 <= k <= nums.length <= 16,1 <= nums[i] <= 10^4, The frequency of each element is in the range [1, 4].
        public bool CanPartitionKSubsets(int[] nums, int k)
        {
            if (k == 1)
                return true;

            if (k == nums.Length)
            {
                if (nums.Distinct().Count() != 1)
                {
                    return false;
                }
            }

            int sum = nums.Sum();
            if (sum % k != 0)
                return false;

            int goal = sum / k;

            bool[] visit = new bool[nums.Length];
            foreach (var n in nums)
            {
                if (n > goal)
                    return false;
            }

            nums = nums.OrderBy(x => -x).ToArray();
            bool found = false;
            CanPartitionKSubsets_Recursion(nums, visit, 0, k, goal, 0, ref found);
            return found;
        }

        public void CanPartitionKSubsets_Recursion(int[] nums, bool[] visit, int left, int k, int goal, int sum, ref bool found)
        {
            if (found == true)
                return;

            if (k == 1)
            {
                found = true;
                return;
            }

            for (int i = left; i < nums.Length; i++)
            {
                if (visit[i])
                    continue;

                if (nums[i] == goal - sum)
                {
                    var nextVisit = new bool[nums.Length];
                    Array.Copy(visit, nextVisit, visit.Length);
                    nextVisit[i] = true;
                    CanPartitionKSubsets_Recursion(nums, nextVisit, 0, k - 1, goal, 0, ref found);
                }
                else if (nums[i] < goal - sum)
                {
                    var nextVisit = new bool[nums.Length];
                    Array.Copy(visit, nextVisit, visit.Length);
                    nextVisit[i] = true;
                    CanPartitionKSubsets_Recursion(nums, nextVisit, i + 1, k, goal, sum + nums[i], ref found);
                }
            }
        }
    }
}