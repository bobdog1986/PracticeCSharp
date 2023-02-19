using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///852. Peak Index in a Mountain Array, #Binary Search
        ///arr must be a mountain. Find arr[i] is the top
        public int PeakIndexInMountainArray(int[] arr)
        {
            int left = 0;
            int right = arr.Length - 1;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (arr[mid] < arr[mid + 1])
                    left = mid + 1;
                else
                    right = mid;
            }
            return left;
        }

        ///855. Exam Room, see ExamRoom


        /// 856. Score of Parentheses
        ///Given a balanced parentheses string s, return the score of the string.
        ///"()" has score 1.
        ///AB has score A + B, where A and B are balanced parentheses strings.
        ///(A) has score 2 * A, where A is a balanced parentheses string.
        public int ScoreOfParentheses(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            if (s.StartsWith("()")) return 1 + ScoreOfParentheses(s.Substring(2));
            if (s.StartsWith("()()")) return 2 + ScoreOfParentheses(s.Substring(4));
            if (s.StartsWith("(())")) return 2 + ScoreOfParentheses(s.Substring(4));

            int count = 0;//using a int var instead of stack, fast and simple
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(') count++;
                else count--;
                if (i != 0 && count == 0)
                {
                    return 2 * ScoreOfParentheses(s.Substring(1, i - 1)) + ScoreOfParentheses(s.Substring(i + 1));
                }
            }
            return 0;//never happen
        }

        ///858. Mirror Reflection
        public int MirrorReflection_My(int p, int q)
        {
            int res = -1;
            bool right = true;
            int sign = 1;
            int h = 0;
            while (true)
            {
                h += sign * q;
                if (right)
                {
                    if (h == p)
                    {
                        res = 1;
                        break;
                    }
                    else if (h > p)
                    {
                        h = p - (h - p);
                        sign = -1;
                        if (h == 0)
                        {
                            res = 0;
                            break;
                        }
                    }
                    else if (h == 0)
                    {
                        res = 0;
                        break;
                    }
                    else if (h < 0)
                    {
                        h = -h;
                        sign = 1;
                    }
                }
                else
                {
                    if (h == p)
                    {
                        res = 2;
                        break;
                    }
                    else if (h > p)
                    {
                        h = p - (h - p);
                        sign = -1;
                    }
                    else if (h < 0)
                    {
                        h = -h;
                        sign = 1;
                    }
                }
                right = !right;
            }
            return res;
        }

        public int MirrorReflection(int p, int q)
        {
            int m = 1, n = 1;
            while (m * p != n * q)
            {
                n++;
                m = n * q / p;
            }
            if (m % 2 == 0 && n % 2 == 1) return 0;
            if (m % 2 == 1 && n % 2 == 1) return 1;
            if (m % 2 == 1 && n % 2 == 0) return 2;
            return -1;
        }

        /// 859. Buddy Strings
        ///return true if you can swap two letters in s so the result is equal to goal, otherwise, return false.
        public bool BuddyStrings(string s, string goal)
        {
            if (s.Length != goal.Length) return false;
            int[] arr1 = new int[26];
            int[] arr2 = new int[26];
            int diff = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != goal[i]) diff++;
                if (diff > 2) return false;
                arr1[s[i] - 'a']++;
                arr2[goal[i] - 'a']++;
            }
            bool len2 = false;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i]) return false;
                if (arr1[i] >= 2) len2 = true;
            }
            if (diff == 0 && !len2) return false;
            else return true;
        }

        ///860. Lemonade Change
        ///Given an integer array bills where bills[i] is the bill the ith customer pays,
        ///return true if you can provide every customer with the correct change, or false otherwise.
        public bool LemonadeChange(int[] bills)
        {
            ///index 1 =5 , 2=10, 4=20
            int[] arr = new int[5];
            foreach (var bill in bills)
            {
                int i = bill / 5;
                arr[i--]++;// pay back bill-5 to customer
                //if customer pay 20, we pay back 10 if we have
                if (i > 2 && arr[2] > 0)
                {
                    arr[2]--;
                    i -= 2;
                }
                //if have 10, we still need pay back another 5;
                //if no 10, pay back 5+5+5
                while (i-- > 0)
                {
                    if (arr[1]-- == 0) return false;
                }
            }
            return true;
        }
        ///861. Score After Flipping Matrix, #Greedy
        ///A move consists of choosing any row or column and toggling each value in that row or column
        ///(i.e., changing all 0's to 1's, and all 1's to 0's).
        ///Return the highest possible score after making any number of moves (including zero moves).
        public int MatrixScore(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            int res = 0;
            res += (1 << (colLen - 1)) * rowLen;//flip all row to start at 1, aka grid[i][0]=1
            for (int j = 1; j < colLen; j++)
            {
                //then flip col [1,n-1] to max
                int cur = 0;
                for (int i = 0; i < rowLen; i++)
                    cur += grid[i][j] == grid[i][0] ? 1 : 0;
                //max of 1s or 0s, we can flip all 0s to 1s, then rotate left
                res += Math.Max(cur, rowLen - cur) * (1 << (colLen - j - 1));
            }
            return res;
        }
        ///867. Transpose Matrix
        public int[][] Transpose(int[][] matrix)
        {
            int m = matrix.Length;
            int n = matrix[0].Length;
            int[][] res = new int[n][];
            for (int i = 0; i < n; i++)
                res[i] = new int[m];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    res[j][i] = matrix[i][j];
                }
            }
            return res;
        }
        ///868. Binary Gap
        //return the longest distance between any two adjacent 1's of n. If no two adjacent 1's, return 0.
        public int BinaryGap(int n)
        {
            List<int> list = new List<int>();
            int index = 0;
            while (n > 0)
            {
                if ((n & 1) == 1) list.Add(index);
                index++;
                n >>= 1;
            }
            if (list.Count <= 1) return 0;
            else
            {
                int res = 0;
                for (int i = 0; i < list.Count - 1; i++)
                    res = Math.Max(res, list[i + 1] - list[i]);
                return res;
            }
        }
        ///869. Reordered Power of 2
        //You are given an integer n.We reorder the digits in any order (including the original order)
        //such that the leading digit is not zero.
        //Return true if and only if we can do this so that the resulting number is a power of two.
        public bool ReorderedPowerOf2(int n)
        {
            HashSet<string> set = new HashSet<string>();
            int a = 1;
            while (a <= 1_000_000_000)
            {
                var s = a.ToString().ToArray().OrderBy(x => x).ToArray();
                set.Add(new string(s));
                a <<= 1;
            }

            var curr = n.ToString().ToArray().OrderBy(x => x).ToArray();
            if (set.Contains(new string(curr))) return true;
            else return false;
        }

        /// 872. Leaf-Similar Trees, #BTree
        public bool LeafSimilar(TreeNode root1, TreeNode root2)
        {
            var list1 = new List<int>();
            var list2 = new List<int>();
            LeafSimilar(root1, list1);
            LeafSimilar(root2, list2);
            if (list1.Count != list2.Count) return false;
            else
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    if (list1[i] != list2[i]) return false;
                }
                return true;
            }
        }

        private void LeafSimilar(TreeNode root, List<int> list)
        {
            if (root == null) return;
            if (root.left == null && root.right == null)
            {
                list.Add(root.val);
            }
            else
            {
                LeafSimilar(root.left, list);
                LeafSimilar(root.right, list);
            }
        }
        /// 875. Koko Eating Bananas, #Binary Search
        ///There are n piles of bananas, the ith pile has piles[i] bananas.
        ///The guards have gone and will come back in h hours.
        ///Find min numb to eat all bananas in h hours. each time can only eat 1 index;
        public int MinEatingSpeed(int[] piles, int h)
        {
            if (piles.Length == h)
                return piles.Max();

            int low = 1, high = 1_000_000_000;
            while (low < high)
            {
                int mid = (low + high) / 2;
                int sum = 0;
                for (int i = 0; i < piles.Length; i++)
                    sum += (int)Math.Ceiling(1.0 * piles[i] / mid);

                if (sum > h)
                    low = mid + 1;
                else
                    high = mid;
            }
            return low;
        }

        /// 876. Middle of the Linked List
        public ListNode MiddleNode(ListNode head)
        {
            if (head == null || head.next == null)
                return head;
            var next = head.next;
            int count = 1;
            while (next != null)
            {
                count++;
                next = next.next;
            }
            int len = count / 2;
            while (len-- > 0)
            {
                head = head.next;
            }
            return head;
        }

        ///878. Nth Magical Number, #Binary Search
        ///A positive integer is magical if it is divisible by either a or b.
        ///Given the three integers n, a, and b, return the nth magical number.
        ///return it modulo 10^9 + 7. 1 <= n <= 10^9, 2 <= a, b <= 4 * 10^4
        public int NthMagicalNumber(int n, int a, int b)
        {
            int mod = 1_000_000_007;
            int c = getLCM(a, b);

            long low = 0;
            long high = (long)n * Math.Min(a, b);
            //high will alway >=n, increase low and decrease high to the edge!
            while (low < high)
            {
                long mid = low + (high - low) / 2;
                if (mid / a + mid / b - mid / c < n)
                    low = mid + 1;
                else
                    high = mid;
            }

            return (int)(low % mod);
        }

        ///881. Boats to Save People, #Two Pointers, #Greedy
        ///You are given an array people where people[i] is the weight of the ith person,
        ///and an infinite number of boats where each boat can carry a maximum weight of limit.
        ///Each boat carries at most two people at the same time, provided the sum of the weight of those people is at most limit.
        ///Return the minimum number of boats to carry every given person.
        public int NumRescueBoats_TwoPointers(int[] people, int limit)
        {
            Array.Sort(people);
            int res = 0;
            int start = 0;
            int end = people.Length - 1;
            while (start <= end)
            {
                res++;
                if (start == end) break; // last person on boat
                if (people[start] + people[end] <= limit) start++; // we can carry two people
                end--;
            }
            return res;
        }

        /// 884. Uncommon Words from Two Sentences
        ///Given two sentences s1 and s2, return a list of all the uncommon words. You may return the answer in any order.
        public string[] UncommonFromSentences(string s1, string s2)
        {
            Dictionary<string, int> sentences = new Dictionary<string, int>();
            var word1 = s1.Split(' ');
            var word2 = s2.Split(' ');
            foreach (var w1 in word1)
            {
                if (string.IsNullOrEmpty(w1)) continue;
                if (!sentences.ContainsKey(w1)) sentences.Add(w1, 1);
                else sentences[w1]++;
            }
            foreach (var w2 in word2)
            {
                if (string.IsNullOrEmpty(w2)) continue;
                if (!sentences.ContainsKey(w2)) sentences.Add(w2, 1);
                else sentences[w2]++;
            }

            return sentences.Where(x => x.Value == 1).Select(x => x.Key).ToArray();
        }
        ///886. Possible Bipartition, #Graph, #DFS, #BFS
        ///Given the integer n and the array dislikes where dislikes[i] = [ai, bi]
        ///indicates that the person labeled ai does not like the person labeled bi,
        ///return true if it is possible to split everyone into two groups in this way.
        public bool PossibleBipartition(int n, int[][] dislikes)
        {
            int[,] graph = new int[n + 1, n + 1];
            foreach (var dis in dislikes)
            {
                graph[dis[0], dis[1]] = 1;
                graph[dis[1], dis[0]] = 1;
            }
            ///visit[i] = 0 means node i hasn't been visited.
            ///visit[i] = 1 means node i has been grouped to 1.
            ///visit[i] = -1 means node i has been grouped to - 1.
            int[] visit = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                if (visit[i] == 0 && !PossibleBipartition_dfs(graph, visit, i, 1))
                {
                    return false;
                }
            }
            return true;
        }
        private bool PossibleBipartition_dfs(int[,] graph, int[] visit, int index, int slot)
        {
            visit[index] = slot;
            for (int i = 1; i < visit.Length; i++)
            {
                if (graph[index, i] == 1)
                {
                    if (visit[i] == slot)
                    {
                        return false;
                    }
                    if (visit[i] == 0 && !PossibleBipartition_dfs(graph, visit, i, -slot))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool PossibleBipartition_bfs(int n, int[][] dislikes)
        {
            int[,] graph = new int[n + 1, n + 1];
            foreach (var dis in dislikes)
            {
                graph[dis[0], dis[1]] = 1;
                graph[dis[1], dis[0]] = 1;
            }
            ///visit[i] = 0 means node i hasn't been visited.
            ///visit[i] = 1 means node i has been grouped to 1.
            ///visit[i] = -1 means node i has been grouped to - 1.
            int[] visit = new int[n + 1];
            for (int i = 1; i <= n; i++)
            {
                if (visit[i] == 0)
                {
                    visit[i] = 1;
                    Queue<int> q = new Queue<int>();
                    q.Enqueue(i);
                    while (q.Count > 0)
                    {
                        int cur = q.Dequeue();
                        for (int j = 1; j <= n; j++)
                        {
                            if (graph[cur, j] == 1)
                            {
                                if (visit[j] == 0)
                                {
                                    visit[j] = -visit[cur];
                                    q.Enqueue(j);
                                }
                                else
                                {
                                    if (visit[j] == visit[cur]) return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// 888. Fair Candy Swap
        ///Return an integer array answer where answer[0] is the number of candies in the box that Alice must exchange,
        ///and answer[1] is the number of candies in the box that Bob must exchange.
        public int[] FairCandySwap(int[] aliceSizes, int[] bobSizes)
        {
            Dictionary<int, int> aliceDict = new Dictionary<int, int>();
            int sumAlice = 0;
            Dictionary<int, int> bobDict = new Dictionary<int, int>();
            int sumBob = 0;
            for (int i = 0; i < aliceSizes.Length; i++)
            {
                if (!aliceDict.ContainsKey(aliceSizes[i])) { aliceDict.Add(aliceSizes[i], i); }
                sumAlice += aliceSizes[i];
            }
            for (int i = 0; i < bobSizes.Length; i++)
            {
                if (!bobDict.ContainsKey(bobSizes[i])) { bobDict.Add(bobSizes[i], i); }
                sumBob += bobSizes[i];
            }
            int diff = (sumBob - sumAlice) / 2;
            foreach (var key in aliceDict.Keys)
            {
                if (bobDict.ContainsKey(key + diff))
                    return new int[] { key, key + diff };
            }
            return null;
        }

        ///889. Construct Binary Tree from Preorder and Postorder Traversal, #BTree
        public TreeNode ConstructFromPrePost_My(int[] preorder, int[] postorder)
        {
            return BuildTree_PreorderAndPostorder(preorder, 0, preorder.Length - 1, postorder, 0, postorder.Length - 1);
        }

        private TreeNode BuildTree_PreorderAndPostorder(int[] preorder, int preL, int preR, int[] postorder, int postL, int postR)
        {
            if (preL > preR) return null;
            else if (preL == preR) return new TreeNode(preorder[preL]);
            else
            {
                var node = new TreeNode(preorder[preL]);
                int i = postL;
                for (; i < postR; i++)
                    if (postorder[i] == preorder[preL + 1]) break;
                int count = i - postL + 1;
                if (count == preR - preL)
                {
                    //assign all to left sub tree
                    node.left = BuildTree_PreorderAndPostorder(preorder, preL + 1, preR, postorder, postL, postR - 1);
                }
                else
                {
                    node.left = BuildTree_PreorderAndPostorder(preorder, preL + 1, preL + count, postorder, postL, postL + count - 1);
                    node.right = BuildTree_PreorderAndPostorder(preorder, preL + count + 1, preR, postorder, postL + count, postR - 1);
                }
                return node;
            }
        }

        public TreeNode ConstructFromPrePost_Lee215(int[] preorder, int[] postorder)
        {
            int preIndex = 0;
            int postIndex = 0;
            return ConstructFromPrePost_Lee215(preorder, postorder, ref preIndex, ref postIndex);
        }

        private TreeNode ConstructFromPrePost_Lee215(int[] preorder, int[] postorder, ref int preIndex, ref int postIndex)
        {
            //Create a node TreeNode(pre[preIndex]) as the root.
            //Becasue root node will be lastly iterated in post order,
            //if root.val == post[posIndex],
            //it means we have constructed the whole tree,

            //If we haven't completed constructed the whole tree,
            //So we recursively constructFromPrePost for left sub tree and right sub tree.

            //And finally, we'll reach the posIndex that root.val == post[posIndex].
            //We increment posIndex and return our root node.
            TreeNode root = new TreeNode(preorder[preIndex++]);
            if (root.val != postorder[postIndex])
                root.left = ConstructFromPrePost_Lee215(preorder, postorder, ref preIndex, ref postIndex);
            if (root.val != postorder[postIndex])
                root.right = ConstructFromPrePost_Lee215(preorder, postorder, ref preIndex, ref postIndex);
            postIndex++;
            return root;
        }

        public TreeNode ConstructFromPrePost(int[] preorder, int[] postorder)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode root = new TreeNode(preorder[0]);
            stack.Push(root);
            for (int i = 1, j = 0; i < preorder.Length; ++i)
            {
                TreeNode node = new TreeNode(preorder[i]);
                while (stack.Peek().val == postorder[j])
                {
                    stack.Pop();
                    j++;
                }
                if (stack.Peek().left == null)
                {
                    stack.Peek().left = node;
                }
                else
                {
                    stack.Peek().right = node;
                }
                stack.Push(node);
            }
            return root;
        }

        ///890. Find and Replace Pattern
        ///Given a list of strings words and a string pattern, return a list of words[i] that match pattern
        public IList<string> FindAndReplacePattern(string[] words, string pattern)
        {
            return words.Where(x => FindAndReplacePattern(x, pattern)).ToList();
        }

        private bool FindAndReplacePattern(string word, string pattern)
        {
            Dictionary<char, char> dict = new Dictionary<char, char>();
            for (int i = 0; i < pattern.Length; i++)
            {
                //if (!dict.ContainsKey(pattern[i]) && dict.ContainsValue(word[i])) return false;
                if (dict.ContainsKey(pattern[i]))
                {
                    if (dict[pattern[i]] != word[i]) return false;
                }
                else
                {
                    if (dict.ContainsValue(word[i])) return false;
                    dict.Add(pattern[i], word[i]);
                }
            }
            return true;
        }
        /// 893. Groups of Special-Equivalent Strings
        ///In one move, you can swap any two even indexed characters or any two odd indexed characters of a string words[i].
        public int NumSpecialEquivGroups(string[] words)
        {
            HashSet<string> set = new HashSet<string>();

            foreach (var w in words)
            {
                List<char> list1 = new List<char>();
                List<char> list2 = new List<char>();
                bool even = true;
                foreach (var c in w)
                {
                    if (even) list1.Add(c);
                    else list2.Add(c);
                    even = !even;
                }
                list1.Sort();
                list2.Sort();
                set.Add($"{new string(list1.ToArray())}{new string(list2.ToArray())}");
            }
            return set.Count;
        }
        ///894. All Possible Full Binary Trees, #BTree, #DP
        ///return a list of all possible full binary trees with n nodes.
        ///Each node of each tree in the answer must have Node.val == 0.
        ///A full binary tree is a binary tree where each node has exactly 0 or 2 children.

        public IList<TreeNode> AllPossibleFBT(int n)
        {
            //When n is divisible by 2, return an empty list
            List<TreeNode>[] dp = new List<TreeNode>[n + 1];
            //Base case, when N==1
            dp[1] = new List<TreeNode>() { new TreeNode(0) };

            for (int i = 3; i <= n; i += 2)
            {
                dp[i] = new List<TreeNode>();
                //Traverse all the possibilities of how to divide the nodes to left and right sides
                for (int j = 1; j < i - 1; j += 2)
                {
                    foreach (TreeNode l in dp[j])
                    {
                        foreach (TreeNode r in dp[i - j - 1])
                        {
                            TreeNode node = new TreeNode(0);
                            node.left = l;
                            node.right = r;
                            dp[i].Add(node);
                        }
                    }
                }
            }
            return dp[n] ?? new List<TreeNode>();
        }
        /// 895. Maximum Frequency Stack, see FreqStack

        /// 896. Monotonic Array
        ///An array is monotonic if it is either monotone increasing or monotone decreasing.
        ///An array nums is monotone increasing if for all i <= j, nums[i] <= nums[j].
        ///An array nums is monotone decreasing if for all i <= j, nums[i] >= nums[j].
        ///Given an integer array nums, return true if the given array is monotonic, or false otherwise.
        public bool IsMonotonic(int[] nums)
        {
            bool increasing = true;
            bool decreasing = true;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (!increasing && !decreasing) return false;
                if (increasing) increasing = nums[i] <= nums[i + 1];
                if (decreasing) decreasing = nums[i] >= nums[i + 1];
            }
            return increasing || decreasing;
        }

        ///899. Orderly Queue
        //You can choose one of the first k letters of s and append it at the end of the string..
        //Return the lexicographically smallest string you could have after applying any number of moves.
        //if k>1 equal to sort whole string, if k==1 just rotate
        public string OrderlyQueue(string s, int k)
        {
            if (k > 1) return new string(s.OrderBy(x => x).ToArray());
            string res = s;
            for (int i = 1; i < s.Length; i++)
            {
                string curr = s.Substring(i) + s.Substring(0, i);
                if (curr.CompareTo(res) < 0)
                    res = curr;
            }
            return res;
        }
    }
}
