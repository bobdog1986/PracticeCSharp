using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1300. Sum of Mutated Array Closest to Target, #Binary Search
        //return the integer value such that when we change all > value to value,
        //the sum of the array gets as close as possible (in absolute difference) to target.
        public int FindBestValue(int[] arr, int target)
        {
            int n = arr.Length;
            Array.Sort(arr);
            int sum = arr.Sum();
            if (sum <= target) return arr[n - 1];
            // The answer would lie between 0 and maximum value in the array.
            int left = 0;
            int right = arr[n - 1];
            int res = 1;
            int diff = sum - target;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                sum = FindBestValue_GetSum(arr, mid);
                if (sum == target) return mid;
                else if (sum > target)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }

                // If current difference is less than diff;
                // or current difference==diff but mid < res.(choose the smaller one.)
                if (Math.Abs(sum - target) < diff || (Math.Abs(sum - target) == diff && mid < res))
                {
                    res = mid;
                    diff = Math.Abs(sum - target);
                }
            }
            return res;
        }

        private int FindBestValue_GetSum(int[] arr, int mid)
        {
            int sum = 0;
            int i = 0;
            for (; i < arr.Length; i++)
            {
                if (arr[i] >= mid) break;
                sum += arr[i];
            }
            return sum + (arr.Length - i) * mid;
        }
        /// 1302. Deepest Leaves Sum, #BTree
        public int DeepestLeavesSum(TreeNode root)
        {
            int res = 0;
            List<TreeNode> list = new List<TreeNode>() { root };

            while (list.Count > 0)
            {
                res = list.Sum(x => x.val);
                List<TreeNode> next = new List<TreeNode>();
                foreach (var node in list)
                {
                    if (node.left != null) next.Add(node.left);
                    if (node.right != null) next.Add(node.right);
                }
                list = next;
            }
            return res;
        }
        ///1304. Find N Unique Integers Sum up to Zero
        public int[] SumZero(int n)
        {
            var res = new int[n];
            int seed = 1;
            int i = 0;
            while (n > 0)
            {
                if (n == 1) res[i++] = 0;
                else
                {
                    res[i++] = seed;
                    res[i++] = -seed;
                    seed++;
                }
                n -= 2;
            }
            return res;

        }
        /// 1305. All Elements in Two Binary Search Trees
        //two binary search trees root1 and root2, return a list containing all the integers from both trees sorted in ascending order.
        public IList<int> GetAllElements(TreeNode root1, TreeNode root2)
        {
            List<int> ans = new List<int>();

            List<TreeNode> list = new List<TreeNode>() { root1, root2 };
            while (list.Count > 0)
            {
                List<TreeNode> next = new List<TreeNode>();

                foreach (var node in list)
                {
                    if (node != null)
                    {
                        ans.Add(node.val);
                        if (node.left != null)
                            next.Add(node.left);
                        if (node.right != null)
                            next.Add(node.right);
                    }
                }

                list = next;
            }

            ans.Sort();
            return ans;
        }

        ///1306. Jump Game III, #Graph, #BFS
        ///you can jump to i + arr[i] or i - arr[i], check if you can reach to any index with value 0.
        public bool CanReach(int[] arr, int start)
        {
            if (start >= 0 && start < arr.Length && arr[start] >= 0)
            {
                if (arr[start] == 0) return true;
                else
                {
                    arr[start] = -arr[start];
                    return CanReach(arr, start - arr[start]) || CanReach(arr, start + arr[start]);
                }
            }
            else return false;
        }

        public bool CanReach_My(int[] arr, int start)
        {
            bool[] visit = new bool[arr.Length];
            List<int> list = new List<int>() { start };
            visit[start] = true;
            while (list.Count > 0)
            {
                var next = new List<int>();
                foreach (var i in list)
                {
                    if (arr[i] == 0) return true;
                    int up = i + arr[i];
                    int down = i - arr[i];
                    if (up >= 0 && up < arr.Length && !visit[up])
                    {
                        visit[up] = true;
                        next.Add(up);
                    }
                    if (down >= 0 && down < arr.Length && !visit[down])
                    {
                        visit[down] = true;
                        next.Add(down);
                    }
                }
                list = next;
            }
            return false;
        }
        /// 1309. Decrypt String from Alphabet to Integer Mapping
        ///Characters ('a' to 'i') are represented by ('1' to '9') respectively.
        ///Characters('j' to 'z') are represented by('10#' to '26#') respectively.
        ///Return the string formed after mapping.
        public string FreqAlphabets(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            if (s.Length < 3 || s[2] != '#')
                return ((char)(s[0] - '1' + 'a')).ToString() + FreqAlphabets(s.Substring(1));
            else
                return ((char)(int.Parse(s.Substring(0, 2)) - 1 + 'a')).ToString() + FreqAlphabets(s.Substring(3));
        }
        /// 1313. Decompress Run-Length Encoded List
        ///Consider each adjacent pair of elements[freq, val] = [nums[2 * i], nums[2 * i + 1]](with i >= 0).
        /// Return the decompressed list.
        public int[] DecompressRLElist(int[] nums)
        {
            var ans = new List<int>();
            for (int i = 0; i < nums.Length; i = i + 2)
            {
                int j = nums[i];
                while (j-- > 0)
                {
                    ans.Add(nums[i + 1]);
                }
            }
            return ans.ToArray();
        }
        /// 1314. Matrix Block Sum
        ///return a matrix answer where each answer[i][j] is the sum of all elements mat[r][c] for:
        ///i - k <= r <= i + k,j - k <= c <= j + k, and(r, c) is a valid position in the matrix.
        public int[][] MatrixBlockSum(int[][] mat, int k)
        {
            int rowLen = mat.Length;
            int colLen = mat[0].Length;

            int[][] result = new int[rowLen][];
            for (int i = 0; i < rowLen; i++)
                result[i] = new int[colLen];

            int[] colSum = new int[colLen];

            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (i == 0)
                    {
                        int sum = 0;
                        for (int r = Math.Max(0, i - k); r <= i + k && r < rowLen; r++)
                            sum += mat[r][j];
                        colSum[j] = sum;
                    }
                    else
                    {
                        if (i + k < rowLen)
                            colSum[j] += mat[i + k][j];
                        if (i - k - 1 >= 0)
                            colSum[j] -= mat[i - k - 1][j];
                    }

                }

                for (int j = 0; j < colLen; j++)
                {
                    int sum = 0;
                    for (int c = Math.Max(0, j - k); c <= j + k && c < colLen; c++)
                    {
                        sum += colSum[c];
                    }
                    result[i][j] = sum;
                }

            }

            return result;
        }

        ///1315. Sum of Nodes with Even-Valued Grandparent
        public int SumEvenGrandparent(TreeNode root)
        {
            int res = 0;
            SumEvenGrandparent(root, ref res);
            return res;
        }

        private void SumEvenGrandparent(TreeNode root, ref int res)
        {
            if (root == null) return;
            if (root.val % 2 == 0)
            {
                if (root.left != null)
                {
                    if (root.left.left != null) res += root.left.left.val;
                    if (root.left.right != null) res += root.left.right.val;
                }
                if (root.right != null)
                {
                    if (root.right.left != null) res += root.right.left.val;
                    if (root.right.right != null) res += root.right.right.val;
                }
            }
            SumEvenGrandparent(root.left, ref res);
            SumEvenGrandparent(root.right, ref res);
        }

        ///1318. Minimum Flips to Make a OR b Equal to c, #Bit Manipulation
        public int MinFlips(int a, int b, int c)
        {
            //make a|b=c;
            int res = 0;
            for (int i = 0; i <= 30; i++)
            {
                int j = 1 << i;
                bool x = (a & j) != 0;
                bool y = (b & j) != 0;
                bool z = (c & j) != 0;
                if (z)
                {
                    if (!x && !y) res++;
                }
                else
                {
                    if (x) res++;
                    if (y) res++;
                }
            }
            return res;
        }
        ///1317. Convert Integer to the Sum of Two No-Zero Integers
        // public int[] GetNoZeroIntegers(int n)
        // {
        //     int i = 1;
        //     while (i < n)
        //     {
        //         int j = n - i;
        //         if (i.ToString().All(x => x != '0') && j.ToString().All(x => x != '0'))
        //             return new int[] { i, j };
        //         i++;
        //     }
        //     return null;
        // }

        /// 1319. Number of Operations to Make Network Connected, #Union-Find, #Graph, DFS, #BFS
        public int MakeConnected(int n, int[][] connections)
        {
            if (connections.Length < n - 1) return -1; // To connect all nodes need at least n-1 edges
            int[] parent = new int[n];
            for (int i = 0; i < n; i++) parent[i] = i;
            int components = n;
            foreach (var c in connections)
            {
                int p1 = MakeConnected_findParent(parent, c[0]);
                int p2 = MakeConnected_findParent(parent, c[1]);
                if (p1 != p2)
                {
                    parent[p1] = p2; // Union 2 component
                    components--;
                }
            }
            return components - 1; // Need (components-1) cables to connect components together
        }
        public int MakeConnected_findParent(int[] parent, int i)
        {
            if (i == parent[i]) return i;
            return parent[i] = MakeConnected_findParent(parent, parent[i]); // Path compression
        }
        public int MakeConnected_dfs(int n, int[][] connections)
        {
            if (connections.Length < n - 1) return -1; // To connect all nodes need at least n-1 edges
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++) graph[i] = new List<int>();
            foreach (int[] c in connections)
            {
                graph[c[0]].Add(c[1]);
                graph[c[1]].Add(c[0]);
            }

            int components = 0;
            bool[] visited = new bool[n];
            for (int v = 0; v < n; v++)
                components += MakeConnected_dfs(v, graph, visited);
            return components - 1; // Need (components-1) cables to connect components together
        }
        public int MakeConnected_dfs(int u, List<int>[] graph, bool[] visited)
        {
            if (visited[u]) return 0;
            visited[u] = true;
            foreach (int v in graph[u]) MakeConnected_dfs(v, graph, visited);
            return 1;
        }

        public int MakeConnected_bfs(int n, int[][] connections)
        {
            if (connections.Length < n - 1) return -1; // To connect all nodes need at least n-1 edges
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++) graph[i] = new List<int>();
            foreach (int[] c in connections)
            {
                graph[c[0]].Add(c[1]);
                graph[c[1]].Add(c[0]);
            }

            int components = 0;
            bool[] visited = new bool[n];
            for (int v = 0; v < n; v++)
                components += MakeConnected_bfs(v, graph, visited);
            return components - 1; // Need (components-1) cables to connect components together
        }
        public int MakeConnected_bfs(int src, List<int>[] graph, bool[] visited)
        {
            if (visited[src]) return 0;
            visited[src] = true;
            Queue<int> q = new Queue<int>();
            q.Enqueue(src);
            while (q.Count > 0)
            {
                int u = q.Dequeue();
                foreach (int v in graph[u])
                {
                    if (!visited[v])
                    {
                        visited[v] = true;
                        q.Enqueue(v);
                    }
                }
            }
            return 1;
        }

        ///1324. Print Words Vertically
        public IList<string> PrintVertically(string s)
        {
            List<string> res = new List<string>();
            var arr = s.Split(' ').ToArray();
            int n = arr.Max(x => x.Length);
            List<char> list = new List<char>();
            for (int i = 0; i < n; i++)
            {
                list.Clear();
                for (int j = 0; j < arr.Length; j++)
                {
                    if (i >= arr[j].Length) list.Add(' ');
                    else list.Add(arr[j][i]);
                }
                res.Add(new string(list.ToArray()).TrimEnd());//remove trailing spaces
            }
            return res;
        }

        ///1325. Delete Leaves With a Given Value, #BTree
        ///Given a binary tree root and an integer target, delete all the leaf nodes with value target.
        ///Note that once you delete a leaf node with value target,
        ///if its parent node becomes a leaf node and has the value target,
        ///it should also be deleted (you need to continue doing that until you cannot).
        public TreeNode RemoveLeafNodes(TreeNode root, int target)
        {
            RemoveLeafNodes(ref root, target);
            return root;
        }

        public bool RemoveLeafNodes(ref TreeNode root, int target)
        {
            if (root == null) return true;
            if (root.val != target)
            {
                RemoveLeafNodes(ref root.left, target);
                RemoveLeafNodes(ref root.right, target);
                return false;
            }
            else
            {
                bool leftEmpty = RemoveLeafNodes(ref root.left, target);
                bool rightEmpty = RemoveLeafNodes(ref root.right, target);
                if (leftEmpty && rightEmpty)
                {
                    root = null;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        ///1328. Break a Palindrome
        //replace one character so that the resulting string is the lexicographically smallest non-palindrome.
        public string BreakPalindrome(string palindrome)
        {
            int n = palindrome.Length;
            if (n == 1) return "";
            char[] arr = palindrome.ToCharArray();
            int mid = n % 2 == 1 ? (n - 1) / 2 : -1;
            for (int i = 0; i < n; i++)
            {
                if (i == mid) continue;
                if (i == n - 1)
                {
                    arr[i] = arr[i] == 'a' ? 'b' : 'a';
                    return new string(arr);
                }
                else
                {
                    if (arr[i] == 'a') continue;
                    arr[i] = 'a';
                    return new string(arr);
                }
            }
            return "";
        }

        ///1329. Sort the Matrix Diagonally
        public int[][] DiagonalSort(int[][] mat)
        {
            int rowLen = mat.Length;
            int colLen = mat[0].Length;
            for (int i = 0; i < colLen + rowLen - 1; i++)
            {
                int r = i < colLen ? 0 : i - (colLen - 1);
                int c = i < colLen ? colLen - 1 - i : 0;
                List<int> list = new List<int>();
                while (r < rowLen && c < colLen)
                    list.Add(mat[r++][c++]);

                list.Sort();

                r = i < colLen ? 0 : i - (colLen - 1);
                c = i < colLen ? colLen - 1 - i : 0;
                int j = 0;
                while (r < rowLen && c < colLen)
                    mat[r++][c++] = list[j++];
            }
            return mat;
        }
        /// 1331. Rank Transform of an Array
        ///Given an array of integers arr, replace each element with its rank.
        public int[] ArrayRankTransform(int[] arr)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in arr)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, 0);
                dict[n]++;
            }
            Dictionary<int, int> map = new Dictionary<int, int>();
            var keys = dict.Keys.OrderBy(x => -x).ToList();
            int rank = 1;
            for (int i = keys.Count - 1; i >= 0; i--)
            {
                map.Add(keys[i], rank);
                rank++;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = map[arr[i]];
            }
            return arr;
        }
        ///1332. Remove Palindromic Subsequences
        public int RemovePalindromeSub(string s)
        {
            return s.Length == 0 ? 0 : (new string(s.Reverse().ToArray()) == s ? 1 : 2);
        }

        // #Graph, #Dijkstra, #Floyd, #Bellman,
        ///1334. Find the City With the Smallest Number of Neighbors at a Threshold Distance
        //edges[i] = [fromi, toi, weighti] a bidirectional weighted edge between cities fromi and toi
        //Return the city with the smallest number of cities that are reachable with distance <= distanceThreshold
        //If there are multiple such cities, return the city with the greatest number.
        //2 <= n <= 100, 1 <= edges.length <= n* (n - 1) / 2, duplicate edges
        //1 <= weighti, distanceThreshold <= 10^4
        public int FindTheCity_Bellman(int n, int[][] edges, int distanceThreshold)
        {
            var dp = getBellmanCosts(n, edges);
            int res = -1;
            int count = n + 1;
            for (int i = 0; i < n; i++)
            {
                int curr = dp[i].Count(x => x <= distanceThreshold);
                if (curr <= count)
                {
                    count = curr;
                    res = i;
                }
            }
            return res;
        }

        public int FindTheCity_Floyd(int n, int[][] edges, int distanceThreshold)
        {
            var dp = getFloydCosts(n, edges);
            int res = -1;
            int count = n + 1;
            for (int i = 0; i < n; i++)
            {
                int curr = dp[i].Count(x => x <= distanceThreshold);
                if (curr <= count)
                {
                    count = curr;
                    res = i;
                }
            }
            return res;
        }

        public int FindTheCity_Dijkstra(int n, int[][] edges, int distanceThreshold)
        {
            List<int[]>[] graph = new List<int[]>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int[]>();
            foreach (var e in edges)
            {
                graph[e[0]].Add(new int[] { e[1], e[2] });
                graph[e[1]].Add(new int[] { e[0], e[2] });
            }
            int res = n - 1;
            int minCount = n + 1;//impossible count n+1
            for (int i = n - 1; i >= 0; i--)//from n-1 to 0 due to greatest number
            {
                //if >=minCount invalid, return -1
                int curr = FindTheCity_Dijkstra(graph, distanceThreshold, minCount, i);
                if (curr == -1) continue;
                else
                {
                    res = i;
                    minCount = curr;
                }
            }
            return res;
        }

        private int FindTheCity_Dijkstra(List<int[]>[] graph, int threshold, int maxCount, int src = 0)
        {
            int n = graph.Length;
            long[] dp = new long[n];//store cost
            Array.Fill(dp, long.MaxValue);
            dp[src] = 0;
            PriorityQueue<long[], long> pq = new PriorityQueue<long[], long>();
            //{index, cost} sort by cost-asc, visit shortest path first, it helps to skips longer paths later
            pq.Enqueue(new long[] { src, 0 }, 0);
            HashSet<long> set = new HashSet<long>();
            while (pq.Count > 0)
            {
                long[] top = pq.Dequeue();
                long u = top[0];
                long cost = top[1];
                if (cost <= threshold)
                {
                    set.Add(u);
                    if (set.Count >= maxCount) return -1;//>=maxCount, return -1;
                }
                if (cost > dp[u]) continue;//not shortest, skip it, donot use >= , this will skip first time call
                foreach (var v in graph[u])
                {
                    long nextCost = cost + v[1];
                    if (nextCost < dp[v[0]])
                    {
                        dp[v[0]] = nextCost;//shorter path found
                        pq.Enqueue(new long[] { v[0], nextCost }, nextCost);// re-visit again
                    }
                }
            }
            return set.Count;
        }

        /// 1337. The K Weakest Rows in a Matrix, #PriorityQueue,
        ///A row i is weaker than a row j if one of the following is true:
        ///The number of soldiers(1) in row i is less than the number of soldiers in row j.
        ///Both rows have the same number of soldiers and i<j.
        ///Return the indices of the k weakest rows in the matrix ordered from weakest to strongest.
        public int[] KWeakestRows(int[][] mat, int k)
        {
            PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) =>
            {
                var sumX = mat[x].Sum();
                var sumY = mat[y].Sum();
                if (sumX != sumY) return sumX - sumY;
                else return x - y;
            }));

            for (int i = 0; i < mat.Length; i++)
            {
                priorityQueue.Enqueue(i, i);
            }

            int[] res = new int[k];
            int j = 0;
            while (j < k)
            {
                res[j++] = priorityQueue.Dequeue();
            }

            return res;
        }

        ///1338. Reduce Array Size to The Half
        public int MinSetSize(int[] arr)
        {
            int res = 0;
            int count = 0;
            int n = arr.Length;
            var dict = new Dictionary<int, int>();
            foreach (var i in arr)
            {
                if (dict.ContainsKey(i)) dict[i]++;
                else dict.Add(i, 1);
            }
            var keys = dict.Keys.OrderBy(x => -dict[x]).ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                if (count >= (n + 1) / 2) break;
                count += dict[keys[i]];
                res++;
            }
            return res;
        }

        ///1342. Number of Steps to Reduce a Number to Zero
        ///In one step, if even divide it by 2, otherwise subtract 1 from it.
        public int NumberOfSteps(int num)
        {
            int res = 0;
            while (num > 0)
            {
                num = num % 2 == 0 ? num / 2 : num - 1;
                res++;
            }
            return res;
        }

        ///1343. Number of Sub-arrays of Size K and Average Greater than or Equal to Threshold
        public int NumOfSubarrays(int[] arr, int k, int threshold)
        {
            int res = 0;
            long m = (long)k * threshold;
            long sum = 0;
            int left = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
                if (i - left + 1 == k)
                {
                    if (sum >= m) res++;
                    sum -= arr[left++];
                }
            }
            return res;
        }

        /// 1345. Jump Game IV, #BFS, #Good
        ///Given an array of integers arr, you are initially positioned at the first index of the array.
        ///In one step you can jump from index i to index:
        ///i + 1 where: i + 1 < arr.length.
        ///i - 1 where: i - 1 >= 0.
        ///j where: arr[i] == arr[j] and i != j.ß
        ///Return the minimum number of steps to reach the last index of the array.
        public int MinJumps(int[] arr)
        {
            var dict = new Dictionary<int, List<int>>();
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                if (!dict.ContainsKey(arr[i]))
                    dict.Add(arr[i], new List<int>());
                dict[arr[i]].Add(i);
            }
            var q = new Queue<int>();
            q.Enqueue(0);
            bool[] visit = new bool[n];
            visit[0] = true;
            int steps = 0;
            var set = new HashSet<int>();
            while (q.Count > 0)
            {
                int size = q.Count;
                while (size-- > 0)
                {
                    var top = q.Dequeue();
                    if (top == n - 1) return steps;
                    if (top + 1 < n && !visit[top + 1])
                    {
                        visit[top + 1] = true;
                        q.Enqueue(top + 1);
                    }
                    if (top - 1 >= 0 && !visit[top - 1])
                    {
                        visit[top - 1] = true;
                        q.Enqueue(top - 1);
                    }
                    if (!set.Contains(arr[top]))//must avoid duplicate visit of same value list
                    {
                        set.Add(arr[top]);
                        foreach (var j in dict[arr[top]])
                        {
                            if (!visit[j])
                            {
                                visit[j] = true;
                                q.Enqueue(j);
                            }
                        }
                    }
                }
                steps++;
            }
            return -1;
        }

        ///1346. Check If N and Its Double Exist
        ///check if there exists two integers N and M such that N is the double of M ( i.e. N = 2 * M).
        public bool CheckIfExist(int[] arr)
        {
            HashSet<int> set = new HashSet<int>();
            foreach (var n in arr)
            {
                if (set.Contains(n + n) || (n % 2 == 0 && set.Contains(n / 2))) return true;
                set.Add(n);
            }
            return false;
        }

        ///1347. Minimum Number of Steps to Make Two Strings Anagram
        public int MinSteps_1347(string s, string t)
        {
            int[] arr1 = new int[26];
            foreach (var c in s)
                arr1[c - 'a']++;
            foreach (var c in t)
                arr1[c - 'a']--;
            return Math.Min(arr1.Where(x => x > 0).Sum(), -arr1.Where(x => x < 0).Sum());
        }
    }
}
