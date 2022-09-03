using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///950. Reveal Cards In Increasing Order
        public int[] DeckRevealedIncreasing(int[] deck)
        {
            int n = deck.Length;
            List<int> list = new List<int>();
            Array.Sort(deck);
            for (int i = n - 1; i >= 0; i--)
            {
                if (list.Count > 0)
                {
                    list.Insert(0, list.Last());
                    list.RemoveAt(list.Count - 1);
                }
                list.Insert(0, deck[i]);
            }
            return list.ToArray();
        }


        ///951. Flip Equivalent Binary Trees, #BTree
        public bool FlipEquiv(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null) return true;
            else if (root1 == null || root2 == null) return false;
            else
            {
                if (root1.val != root2.val) return false;
                return (FlipEquiv(root1.left, root2.left) && FlipEquiv(root1.right, root2.right))
                    || (FlipEquiv(root1.left, root2.right) && FlipEquiv(root1.right, root2.left));
            }
        }

        /// 953. Verifying an Alien Dictionary
        ///Given a sequence of words written in the alien language, and the order of the alphabet,
        ///return true if and only if the given words are sorted lexicographically in this alien language.
        public bool IsAlienSorted(string[] words, string order)
        {
            Dictionary<char,int> dict=new Dictionary<char, int>();
            for(int i = 0; i < order.Length; i++)
                dict.Add(order[i], i);
            for (int i=0; i < words.Length-1; i++)
                if (!IsAlienSorted(words[i], words[i + 1], dict)) return false;
            return true;
        }

        public bool IsAlienSorted(string s1,string s2, Dictionary<char, int> dict)
        {
            int i = 0;
            while(i< s1.Length && i < s2.Length)
            {
                if (dict[s1[i]] > dict[s2[i]]) return false;
                else if(dict[s1[i]] < dict[s2[i]]) return true;
                i++;
            }
            return i >= s1.Length;
        }

        ///959. Regions Cut By Slashes, #Union Find
        //An n x n grid is composed of 1 x 1 squares where each 1 x 1 square consists of a '/', '\', or blank space ' '.
        //These characters divide the square into contiguous regions.
        //Given the grid grid represented as a string array, return the number of regions.
        //Note that backslash characters are escaped, so a '\' is represented as '\\'.
        public int RegionsBySlashes(string[] grid)
        {
            //     0
            //  3     1
            //     2
            int n = grid.Length;
            var uf = new UnionFind(n * n * 4);

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    int up = RegionsBySlashes_getIndex(i, j, 0, n);
                    int right = RegionsBySlashes_getIndex(i, j, 1, n);
                    int bottom = RegionsBySlashes_getIndex(i, j, 2, n);
                    int left = RegionsBySlashes_getIndex(i, j, 3, n);

                    if (i > 0)
                    {
                        int prev = RegionsBySlashes_getIndex(i - 1, j, 2, n);
                        uf.Union(prev, up);
                    }
                    if (j > 0)
                    {
                        int prev = RegionsBySlashes_getIndex(i, j-1, 1, n);
                        uf.Union(prev, left);
                    }

                    if (grid[i][j]== '/')
                    {
                        uf.Union(up, left);
                        uf.Union(right, bottom);
                    }
                    else if(grid[i][j] == ' ')
                    {
                        uf.Union(up, left);
                        uf.Union(left, bottom);
                        uf.Union(bottom, right);
                    }
                    else
                    {
                        uf.Union(up, right);
                        uf.Union(left, bottom);
                    }
                }
            }
            return uf.GroupCount;
        }

        private int RegionsBySlashes_getIndex(int i, int j, int k,int n)
        {
            return (i * n + j)*4 + k;
        }

        ///965. Univalued Binary Tree, #BTree
        public bool IsUnivalTree(TreeNode root)
        {
            HashSet<int> set = new HashSet<int>();
            bool res = true;
            IsUnivalTree(root, set, ref res);
            return res;
        }

        private void IsUnivalTree(TreeNode root, HashSet<int> set, ref bool res)
        {
            if (root == null) return;
            if (!res) return;
            if(set.Count>0 && !set.Contains(root.val))
            {
                res = false;
                return;
            }
            else
            {
                set.Add(root.val);
                IsUnivalTree(root.left, set, ref res);
                IsUnivalTree(root.right, set, ref res);
            }
        }
        /// 966. Vowel Spellchecker
        ///Given a wordlist, we want to implement a spellchecker that converts a query word into a correct word.
        public string[] Spellchecker(string[] wordlist, string[] queries)
        {
            var ans = new List<string>();

            Dictionary<char, int> map = new Dictionary<char, int>()
            {
                {'a', 0},{'e', 0},{'i', 0},{'o', 0},{'u', 0},
                {'A', 0}, {'E', 0}, {'I', 0}, {'O', 0}, {'U', 0},
            };

            //Capitalization hashmap
            Dictionary<string, string> dict = new Dictionary<string, string>();
            //Vowel Errors hashmap
            Dictionary<int, Dictionary<string, Dictionary<string, string>>> vowelDict
                = new Dictionary<int, Dictionary<string, Dictionary<string, string>>>();

            foreach (var word in wordlist)
            {
                //store to Capitalization hashmap
                if (!dict.ContainsKey(word))
                    dict.Add(word, word);
                var capWord = word.ToUpper();
                if (!dict.ContainsKey(capWord))
                    dict.Add(capWord, word);

                //two parts, Vowel idnexes and other letters
                List<int> list = new List<int>();
                List<char> others = new List<char>();
                for (int i = 0; i < word.Length; i++)
                {
                    if (map.ContainsKey(word[i]))
                        list.Add(i);
                    else
                        others.Add(word[i]);
                }

                if (list.Count > 0)
                {
                    //using Length as 1st level key
                    if (!vowelDict.ContainsKey(word.Length))
                        vowelDict.Add(word.Length, new Dictionary<string, Dictionary<string, string>>());
                    var indexStr = string.Join("_", list);

                    //using indexStr string join "_" as 2nd level key
                    if (!vowelDict[word.Length].ContainsKey(indexStr))
                        vowelDict[word.Length].Add(indexStr, new Dictionary<string, string>());

                    //using otherStr as 3rd level key
                    var otherStr = new string(others.ToArray()).ToUpper();
                    if (!vowelDict[word.Length][indexStr].ContainsKey(otherStr))
                        vowelDict[word.Length][indexStr].Add(otherStr, word);
                }
            }

            foreach (var query in queries)
            {
                if (dict.ContainsKey(query))
                {
                    ans.Add(dict[query]);
                }
                else if (dict.ContainsKey(query.ToUpper()))
                {
                    ans.Add(dict[query.ToUpper()]);
                }
                else
                {
                    List<int> list = new List<int>();
                    List<char> others = new List<char>();
                    for (int i = 0; i < query.Length; i++)
                    {
                        if (map.ContainsKey(query[i]))
                            list.Add(i);
                        else
                            others.Add(query[i]);
                    }
                    if (list.Count > 0 && vowelDict.ContainsKey(query.Length))
                    {
                        var indexStr = string.Join("_", list);
                        var otherStr = new string(others.ToArray()).ToUpper();
                        if (vowelDict[query.Length].ContainsKey(indexStr)
                            && vowelDict[query.Length][indexStr].ContainsKey(otherStr))
                        {
                            ans.Add(vowelDict[query.Length][indexStr][otherStr]);
                        }
                        else
                        {
                            ans.Add(string.Empty);
                        }
                    }
                    else
                    {
                        ans.Add(string.Empty);
                    }
                }
            }
            return ans.ToArray();
        }

        ///967. Numbers With Same Consecutive Differences
        //Return all non-negative integers of length n that the absolute difference between every two consecutive digits is k.
        public int[] NumsSameConsecDiff(int n, int k)
        {
            List<int> res = new List<int>();
            for(int i = 1; i <= 9; i++)
                NumsSameConsecDiff(n - 1, k, i, i, res);
            return res.ToArray();
        }

        private void NumsSameConsecDiff(int n, int k, int prev, int curr, List<int> res)
        {
            if(n == 0)
                res.Add(curr);
            else
            {
                int up = prev + k;
                if (up <= 9)
                    NumsSameConsecDiff(n - 1, k, up, curr * 10 + up, res);

                int down = prev - k;
                if (up!=down && down >= 0)//skip k==0
                    NumsSameConsecDiff(n - 1, k, down, curr * 10 + down, res);
            }
        }

        ///968. Binary Tree Cameras, #Greedy, #DFS
        ///You are given the root of a binary tree. We install cameras on the tree nodes
        ///where each camera at a node can monitor its parent, itself, and its immediate children.
        ///Return the minimum number of cameras needed to monitor all nodes of the tree.
        public int MinCameraCover(TreeNode root)
        {
            int ans = 0;
            var leftLevel = MinCameraCover_Recursion(root.left, ref ans);
            var rightLevel = MinCameraCover_Recursion(root.right, ref ans);
            if (leftLevel == 0 || rightLevel == 0 || (leftLevel == 2 && rightLevel == 2))
            {
                ans++;
            }
            return ans;
        }

        public int MinCameraCover_Recursion(TreeNode node, ref int ans)
        {
            if (node == null) return 2;
            if (node.left == null && node.right == null) return 0;

            var leftVal = MinCameraCover_Recursion(node.left, ref ans);
            var rightVal = MinCameraCover_Recursion(node.right, ref ans);
            if (leftVal == 0 || rightVal == 0)
            {
                ans++;
                return 1;
            }
            else if (leftVal == 1 || rightVal == 1)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        /// 973. K Closest Points to Origin, #PriorityQueue,
        ///return the k closest points to the origin (0, 0).
        public int[][] KClosest(int[][] points, int k)
        {
            List<int[]> res=new List<int[]>();
            PriorityQueue<int[],int> priorityQueue = new PriorityQueue<int[],int>();
            foreach(var p in points)
            {
                priorityQueue.Enqueue(p, p[0] * p[0] + p[1] * p[1]);
            }
            while (k-- > 0)
            {
                res.Add(priorityQueue.Dequeue());
            }
            return res.ToArray();
        }
        ///976. Largest Perimeter Triangle
        /// return the largest sum of perimeter of a triangle, formed from three of these lengths. Or 0 if impossible
        public int LargestPerimeter(int[] nums)
        {
            Array.Sort(nums);
            for(int i=nums.Length-1; i>=2; i--)
            {
                if(nums[i] < nums[i-1] + nums[i-2])
                    return nums[i] + nums[i - 1] + nums[i - 2];
            }
            return 0;
        }
        /// 977. Squares of a Sorted Array
        public int[] SortedSquares(int[] nums)
        {
            return nums.Select(x => x * x).OrderBy(x => x).ToArray();
        }

        /// 980. Unique Paths III, #DFS
        ///1 representing the starting square.There is exactly one starting square.
        ///2 representing the ending square. There is exactly one ending square.
        ///0 representing empty squares we can walk over.
        ///-1 representing obstacles that we cannot walk over.
        ///Return the number of 4-directional walks from the starting square to the ending square,
        ///that walk over every non-obstacle square exactly once.
        public int UniquePathsIII(int[][] grid)
        {
            int total = 0;
            int res = 0;
            int startRow=0 ,startCol=0;
            for(int i=0; i<grid.Length; i++)
            {
                for(int j=0; j<grid[i].Length; j++)
                {
                    if(grid[i][j] == 1)
                    {
                        startRow = i;
                        startCol = j;
                    }
                    else if (grid[i][j] == 0)
                    {
                        total++;
                    }
                }
            }
            int[][] dxy = new int[4][] { new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, -1 } };
            UniquePathsIII_DFS(grid, startRow, startCol, total, dxy, ref res);
            return res;
        }

        private void UniquePathsIII_DFS(int[][] grid,int row, int col, int count,int[][] dxy, ref int res)
        {
            if (row < 0 || row >= grid.Length || col < 0 || col >= grid[0].Length) return;
            if (grid[row][col] == -1) return;
            if (grid[row][col] == 2)
            {
                if (count == 0) res++;
                return;
            }
            else
            {
                var temp = grid[row][col];
                grid[row][col] = -1;
                int nextCount = temp == 0 ? count - 1 : count;
                foreach(var d in dxy)
                {
                    var r = row + d[0];
                    var c = col + d[1];
                    if (r < 0 || r >= grid.Length || c < 0 || c >= grid[0].Length) continue;
                    if (grid[r][c] == -1) continue;
                    UniquePathsIII_DFS(grid, r, c, nextCount, dxy, ref res);
                }
                grid[row][col] = temp;
            }
        }
        ///981. Time Based Key-Value Storem ,see TimeMap

        /// 984. String Without AAA or BBB
        public string StrWithout3a3b(int a, int b)
        {
            var sb = new StringBuilder();
            while(a>0 && b > 0)
            {
                if (a > b)
                {
                    sb.Append("aa");
                    sb.Append('b');
                    a-=2;
                    b--;
                }
                else if (b > a)
                {
                    sb.Append("bb");
                    sb.Append('a');
                    a --;
                    b-=2;
                }
                else
                {
                    sb.Append('a');
                    sb.Append('b');
                    a--;
                    b--;
                }
            }

            while (a-- > 0)
                sb.Append('a');
            while (b-- > 0)
                sb.Append('b');
            return sb.ToString();
        }
        ///985. Sum of Even Numbers After Queries
        public int[] SumEvenAfterQueries(int[] nums, int[][] queries)
        {
            int n = queries.Length;
            int sum = nums.Sum(x => x % 2 == 0 ? x : 0);
            int[] res = new int[n];
            for (int i = 0; i < n; i++)
            {
                var q = queries[i];
                if (nums[q[1]] % 2 == 0)
                {
                    if (q[0] % 2 == 0)
                        sum += q[0];
                    else
                        sum -= nums[q[1]];
                }
                else
                {
                    if (q[0] % 2 != 0)
                        sum += nums[q[1]] + q[0];
                }
                nums[q[1]] += q[0];
                res[i] = sum;
            }
            return res;
        }
        /// 986. Interval List Intersections
        ///The intersection of two closed intervals is a set of real numbers that are either empty or represented as a closed interval.
        ///For example, the intersection of [1, 3] and [2, 4] is [2, 3].
        public int[][] IntervalIntersection(int[][] firstList, int[][] secondList)
        {
            List<int[]> list = new List<int[]>();

            if (firstList.Length == 0 || secondList.Length == 0)
                return list.ToArray();

            foreach (var first in firstList)
            {
                if (first[1] < secondList[0][0])
                    continue;

                if (first[0] > secondList[secondList.Length - 1][1])
                    break;

                foreach (var second in secondList)
                {
                    if (first[1] < second[0] || first[0] > second[1])
                        continue;

                    list.Add(new int[] { Math.Max(first[0], second[0]), Math.Min(first[1], second[1]) });
                }
            }

            return list.ToArray();

        }
        ///988. Smallest String Starting From Leaf, #BTree
        ///each node has a value in the range [0, 25] representing the letters 'a' to 'z'.
        ///Return the lexicographically smallest string that starts at a leaf of this tree and ends at the root.
        public string SmallestFromLeaf(TreeNode root)
        {
            string res = string.Empty;
            SmallestFromLeaf(root, string.Empty, ref res);
            return res;
        }

        private void SmallestFromLeaf(TreeNode root, string curr, ref string res)
        {
            if (root == null) return;
            if (root.left == null && root.right == null)
            {
                var str = ((char)(root.val + 'a')).ToString() + curr;
                if (string.IsNullOrEmpty(res)
                    || string.Compare(str, res, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    res = str;
                }
            }
            else
            {
                SmallestFromLeaf(root.left, ((char)(root.val + 'a')).ToString() + curr, ref res);
                SmallestFromLeaf(root.right, ((char)(root.val + 'a')).ToString() + curr, ref res);
            }
        }

        /// 989. Add to Array-Form of Integer
        ///The array-form of an integer num is an array representing its digits in left to right order.
        ///For example, for num = 1321, the array form is [1,3,2,1].
        ///Given num, the array-form of an integer, and an integer k, return the array-form of the integer num + k.
        public IList<int> AddToArrayForm(int[] num, int k)
        {
            int carry = 0;
            for(int i = num.Length - 1; i >= 0; i--)
            {
                int curr = num[i] + k + carry;
                k = 0;
                carry = curr / 10;
                num[i] = curr % 10;
            }
            var list = num.ToList();
            while (carry > 0)
            {
                list.Insert(0, carry % 10);
                carry /= 10;
            }
            return list;
        }

        ///991. Broken Calculator
        ///multiply the number on display by 2, or subtract 1 from the number on display.
        /// return the minimum number of operations needed to display target on the calculator.
        public int BrokenCalc(int startValue, int target)
        {
            int res = 0;
            while (target > startValue)
            {
                if (target % 2 == 0)
                    target = target / 2;
                else
                    target += 1;
                res++;
            }
            return res + (startValue - target);
        }

        /// 994. Rotting Oranges
        public int OrangesRotting(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;

            int totalCount = rowLen * colLen;

            int rottenCount = 0;
            int emptyCount = 0;

            bool isFirstLoop = true;

            Queue<int[]> queue = new Queue<int[]>();

            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == 0)
                    {
                        emptyCount++;
                    }
                    else if (grid[i][j] == 1)
                    {
                        queue.Enqueue(new int[] { i, j });
                    }
                    else
                    {
                        rottenCount++;
                    }
                }
            }

            if (rottenCount == totalCount)
                return 0;

            int lastRottenCount = rottenCount;

            int loop = 0;

            int lastCount = -1;
            Queue<int[]> q2;

            while (lastCount != queue.Count && queue.Count > 0)
            {
                lastCount = queue.Count;
                List<int[]> list = new List<int[]>();
                q2 = new Queue<int[]>();
                while (queue.Count > 0)
                {
                    var x = queue.Dequeue();
                    if ((x[0] > 0 && grid[x[0] - 1][x[1]] == 2)
                            || (x[0] < rowLen - 1 && grid[x[0] + 1][x[1]] == 2)
                            || (x[1] > 0 && grid[x[0]][x[1] - 1] == 2)
                            || (x[1] < colLen - 1 && grid[x[0]][x[1] + 1] == 2))
                    {
                        //grid[x[0]][x[1]] = 2;
                        list.Add(x);
                    }
                    else
                    {
                        q2.Enqueue(x);
                    }
                }

                if (list.Count == 0)
                {
                    return -1;
                }
                else
                {
                    queue = q2;
                    foreach (var i in list)
                    {
                        grid[i[0]][i[1]] = 2;
                    }
                }

                loop++;
            }

            return queue.Count == 0 ? loop : -1;
        }

        ///995. Minimum Number of K Consecutive Bit Flips, #Greedy
        ///Return the minimum number of k-bit flips required so that flip all 0s . If it is not possible, return -1.
        public int MinKBitFlips(int[] nums, int k)
        {
            int n = nums.Length, flipped = 0, res = 0;
            int[] isFlipped = new int[n];
            for (int i = 0; i < nums.Length; ++i)
            {
                if (i >= k)
                    flipped ^= isFlipped[i - k];
                if (flipped == nums[i])
                {
                    if (i + k > nums.Length)
                        return -1;
                    isFlipped[i] = 1;
                    flipped ^= 1;
                    res++;
                }
            }
            return res;
        }
        ///996. Number of Squareful Arrays, #Backtracking
        //An array is squareful if the sum of every pair of adjacent elements is a perfect square.
        //Given an integer array nums, return the number of permutations of nums that are squareful.
        //Two permutations perm1 and perm2 are different if there is some index i such that perm1[i] != perm2[i].
        public int NumSquarefulPerms(int[] nums)
        {
            Dictionary<int, int> cntMap = new Dictionary<int, int>();
            Dictionary<int, HashSet<int>> squareMap = new Dictionary<int, HashSet<int>>();
            int cnt = 0;

            foreach(int num in nums)
            {
                if (!cntMap.ContainsKey(num))
                {
                    cntMap.Add(num, 1);
                    squareMap.Add(num, new HashSet<int>());
                }
                else
                {
                    cntMap[num]++;
                }
            }

            foreach (int num1 in cntMap.Keys)
            {
                foreach (int num2 in cntMap.Keys)
                {
                    if (NumSquarefulPerms_IsSquare(num1 + num2))
                    {
                        squareMap[num1].Add(num2);
                        squareMap[num2].Add(num1);
                    }
                }
            }

            foreach (int num in cntMap.Keys)
            {
                NumSquarefulPerms_DFS(num, nums.Length - 1,cntMap, squareMap, ref cnt);
            }

            return cnt;
        }

        private void NumSquarefulPerms_DFS(int num, int left, Dictionary<int, int> cntMap, Dictionary<int, HashSet<int>> squareMap, ref int cnt)
        {
            cntMap[num]--;
            if (left == 0) { cnt++; }
            else
            {
                foreach (int next in squareMap[num])
                {
                    if (cntMap[next] != 0)
                    {
                        NumSquarefulPerms_DFS(next, left - 1,cntMap,squareMap,ref cnt);
                    }
                }
            }
            cntMap[num]++;
        }

        private bool NumSquarefulPerms_IsSquare(int num)
        {
            return (Math.Sqrt(num) % 1 == 0);
        }

        /// 997. Find the Town Judge, #Graph
        ///In a town, there are n people labeled from 1 to n. There is a rumor that one of these people is secretly the town judge.
        ///The town judge trusts nobody.Everybody (except for the town judge) trusts the town judge.
        ///Return the label of the town judge if the town judge exists and can be identified, or return -1 otherwise.
        public int FindJudge(int n, int[][] trust)
        {
            int[] beTrustedArr = new int[n+1];
            int[] trustArr = new int[n+1];
            foreach(var t in trust)
            {
                beTrustedArr[t[1]]++;
                trustArr[t[0]]++;
            }
            for(int i = 1; i <= n; i++)
            {
                if(beTrustedArr[i]==n-1 && trustArr[i]==0)
                    return i;
            }
            return -1;
        }

        ///999. Available Captures for Rook
        public int NumRookCaptures(char[][] board)
        {
            int m = board.Length;
            int n = board[0].Length;
            int r = 0, c = 0;
            for(int i=0;i< m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if(board[i][j] == 'R')
                    {
                        r = i;
                        c = j;
                    }
                }
            }
            int res = 0;
            for(int i = r + 1; i < m; i++)
            {
                if (board[i][c] == 'B') break;
                else if (board[i][c] == 'p')
                {
                    res++;
                    break;
                }
            }
            for (int i = r - 1; i >=0; i--)
            {
                if (board[i][c] == 'B') break;
                else if (board[i][c] == 'p')
                {
                    res++;
                    break;
                }
            }
            for (int j = c + 1; j < n; j++)
            {
                if (board[r][j] == 'B') break;
                else if (board[r][j] == 'p')
                {
                    res++;
                    break;
                }
            }
            for (int j = c - 1; j >=0; j--)
            {
                if (board[r][j] == 'B') break;
                else if (board[r][j] == 'p')
                {
                    res++;
                    break;
                }
            }
            return res;
        }
    }
}
