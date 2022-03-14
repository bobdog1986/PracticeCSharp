using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {


        ///1305. All Elements in Two Binary Search Trees
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
            if(start>=0 && start< arr.Length && arr[start] >= 0)
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
            List<int> list = new List<int>() { start};
            visit[start] = true;
            while(list.Count > 0)
            {
                var next =new List<int>();
                foreach(var i in list)
                {
                    if (arr[i] == 0) return true;
                    int up = i + arr[i];
                    int down = i - arr[i];
                    if(up>=0 && up<arr.Length && !visit[up])
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
            if (s.Length < 3 || s[2]!='#' )
                return ((char)(s[0]-'1'+'a')).ToString()+ FreqAlphabets(s.Substring(1));
            else
                return ((char)(int.Parse(s.Substring(0,2))-1+'a')).ToString() + FreqAlphabets(s.Substring(3));
        }
        /// 1313. Decompress Run-Length Encoded List
        ///Consider each adjacent pair of elements[freq, val] = [nums[2 * i], nums[2 * i + 1]](with i >= 0).
        /// Return the decompressed list.
        public int[] DecompressRLElist(int[] nums)
        {
            var ans=new List<int>();
            for(int i = 0; i < nums.Length; i = i + 2)
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

        ///1319. Number of Operations to Make Network Connected, #Union-Find, #Graph, DFS, #BFS
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
            while (q.Count>0)
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


        /// 1345. Jump Game IV
        ///Given an array of integers arr, you are initially positioned at the first index of the array.
        ///In one step you can jump from index i to index:
        ///i + 1 where: i + 1 < arr.length.
        ///i - 1 where: i - 1 >= 0.
        ///j where: arr[i] == arr[j] and i != j.
        ///Return the minimum number of steps to reach the last index of the array.
        ///Notice that you can not jump outside of the array at any time.

        public int MinJumps(int[] arr)
        {
            if (arr == null || arr.Length <= 1)
                return 0;

            if (arr.Length == 2 || arr[0] == arr[arr.Length - 1])
                return 1;

            if (arr[0] == arr[arr.Length - 2] || arr[1] == arr[arr.Length - 1])
                return 2;

            //len>=3

            Dictionary<int, List<int>> valueIndexDict = new Dictionary<int, List<int>>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (valueIndexDict.ContainsKey(arr[i]))
                {
                    valueIndexDict[arr[i]].Add(i);
                }
                else
                {
                    valueIndexDict.Add(arr[i], new List<int>() { i });
                }
            }

            if (valueIndexDict.Count == arr.Length)
            {
                return arr.Length - 1;
            }

            int specIndex = arr.Length - 1;

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (valueIndexDict[arr[i]].Count > 1)
                {
                    specIndex = i;
                    break;
                }
            }

            var stepToSpecIndex = arr.Length - 1 - specIndex;

            if (arr[0] == arr[specIndex])
            {
                return arr.Length - 1 - specIndex + 1;
            }

            if (arr[1] == arr[specIndex])
            {
                return arr.Length - 1 - specIndex + 1 + 1;
            }

            List<int> visitIndexList = new List<int>() { 0 };

            foreach (var pair in valueIndexDict)
            {
                if (pair.Value.Count > 1)
                {
                    var list = new List<int>(pair.Value);

                    for (int i = 1; i < list.Count - 1; i++)
                    {
                        if (arr[list[i]] == arr[list[i] - 1] && arr[list[i]] == arr[list[i] + 1])
                        {
                            visitIndexList.Add(list[i]);
                            pair.Value.Remove(list[i]);
                        }
                    }
                }
            }

            //List<int> visitValueList = new List<int>() {  };

            List<List<int>> allPath = new List<List<int>>
            {
                new List<int>() { 0 }
            };

            while (true)
            {
                var list = new List<int>();

                var lastList = allPath.Last();

                foreach (var i in lastList)
                {
                    if (specIndex < arr.Length - 1)
                    {
                        if (arr[i] == arr[specIndex])
                        {
                            if (i != specIndex)
                            {
                                stepToSpecIndex++;
                            }
                            return allPath.Count - 1 + stepToSpecIndex;
                        }

                        if (i < arr.Length - 1 && arr[i + 1] == arr[specIndex])
                        {
                            stepToSpecIndex++;
                            if (i + 1 != specIndex)
                            {
                                stepToSpecIndex++;
                            }
                            return allPath.Count - 1 + stepToSpecIndex;
                        }

                        if (i > 0 && arr[i - 1] == arr[specIndex])
                        {
                            stepToSpecIndex++;
                            if (i - 1 != specIndex)
                                stepToSpecIndex++;

                            return allPath.Count - 1 + stepToSpecIndex;
                        }
                    }

                    foreach (var j in valueIndexDict[arr[i]])
                    {
                        if (!visitIndexList.Contains(j) && i != j)
                        {
                            list.Add(j);

                            visitIndexList.Add(j);
                            //visitValueList.Add(arr[j]);
                        }
                    }

                    if (i > 0)
                    {
                        if (!visitIndexList.Contains(i - 1))
                        {
                            list.Add(i - 1);

                            visitIndexList.Add(i - 1);
                            //visitValueList.Add(arr[i - 1]);
                        }
                    }

                    if (i < arr.Length - 1)
                    {
                        if (!visitIndexList.Contains(i + 1))
                        {
                            list.Add(i + 1);

                            visitIndexList.Add(i + 1);
                            //visitValueList.Add(arr[i + 1]);
                        }
                    }
                }

                allPath.Add(list);

                if (list.Contains(arr.Length - 1))
                    break;
            }

            return allPath.Count - 1;
        }
    }
}
