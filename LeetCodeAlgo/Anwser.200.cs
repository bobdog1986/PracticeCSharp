using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///200. Number of Islands
        ///Given an m x n 2D binary grid grid which represents a map of '1's (land) and '0's (water), return the number of islands.
        ///An island is surrounded by water and is formed by connecting adjacent lands horizontally or vertically.
        ///You may assume all four edges of the grid are all surrounded by water.
        public int NumIslands(char[][] grid)
        {
            int ans = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        ans++;
                        NumIslands_RemoveAllConnected(grid,i, j);
                    }
                }
            }
            return ans;
        }

        public void NumIslands_RemoveAllConnected(char[][] grid,int r,int c)
        {
            grid[r][c] = '0';
            var q = new Queue<int[]>();
            q.Enqueue(new int[] { r, c });
            while(q.Count > 0)
            {
                var a = q.Dequeue();
                var up = new int[] { a[0] - 1, a[1] };
                var down = new int[] { a[0] + 1, a[1] };
                var left = new int[] { a[0], a[1]-1 };
                var right = new int[] { a[0], a[1]+1 };

                if(up[0]>=0 && grid[up[0]][up[1]] == '1')
                {
                    grid[up[0]][up[1]] = '0';
                    q.Enqueue(up);
                }
                if (down[0]<=grid.Length- 1 && grid[down[0]][down[1]] == '1')
                {
                    grid[down[0]][down[1]] = '0';
                    q.Enqueue(down);
                }
                if (left[1]>= 0 && grid[left[0]][left[1]] == '1')
                {
                    grid[left[0]][left[1]] = '0';
                    q.Enqueue(left);
                }
                if (right[1]<=grid[0].Length- 1 && grid[right[0]][right[1]] == '1')
                {
                    grid[right[0]][right[1]] = '0';
                    q.Enqueue(right);
                }
            }
        }

        char[][] _grid;
        int m, n, cnt;
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };
        private int NumIslands_1(char[][] grid)
        {
            _grid = grid;
            m = _grid.Length;
            n = _grid[0].Length;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (_grid[i][j] == '1')
                    {
                        cnt++;
                        MarkVisited(i, j);
                    }
                }
            }
            return cnt;
        }

        private void MarkVisited(int r, int c)
        {
            var q = new Queue<int[]>();
            _grid[r][c] = '0';
            q.Enqueue(new int[] { r, c });

            while (q.Count > 0)
            {
                var a = q.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    var nextRow = a[0] + dx[i];
                    var nextCol = a[1] + dy[i];
                    if (nextRow >= 0 && nextRow < m && nextCol >= 0 && nextCol < n && _grid[nextRow][nextCol] == '1')
                    {
                        _grid[nextRow][nextCol] = '0';
                        q.Enqueue(new int[] { nextRow, nextCol });
                    }
                }
            }
        }

        private int NumIslands_MyUgly(char[][] grid)
        {
            int ans = 0;

            int[][] mat = new int[grid.Length + 2][];
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i] = new int[grid[0].Length + 2];
                if (i > 0 && i < mat.Length - 1)
                {
                    for (int j = 1; j < mat[0].Length - 1; j++)
                    {
                        mat[i][j] = grid[i - 1][j - 1] == '1' ? 1 : 0;
                    }
                }
            }

            List<List<int>> islands = new List<List<int>>();

            for (int i = 1; i < mat.Length - 1; i++)
            {
                List<List<int>> islands2 = new List<List<int>>();
                foreach (var sub in islands)
                {
                    bool found = true;
                    List<int> sub2 = new List<int>();
                    foreach (var j in sub)
                    {
                        if (mat[i][j] != 0)
                        {
                            found = false;
                            sub2.Add(j);
                        }
                    }

                    if (found)
                    {
                        ans++;
                    }
                    else
                    {
                        islands2.Add(sub2);
                    }
                }

                List<int> exists = new List<int>();
                foreach (var island in islands2)
                {
                    exists.AddRange(island);
                }


                List<int> list = new List<int>();
                for (int j = 1; j < mat[i].Length; j++)
                {
                    if (j< mat[i].Length-1 && mat[i][j] == 1 && !exists.Contains(j))
                    {
                        list.Add(j);
                    }
                    else
                    {
                        if (list.Count > 0)
                        {
                            var first = islands2.FirstOrDefault(x => x.Contains(list[0] - 1));
                            var second = islands2.FirstOrDefault(x => x.Contains(list.Last() + 1));
                            if (first != null && second != null && first != second)
                            {
                                foreach(var k in list)
                                {
                                    if (!first.Contains(k))
                                        first.Add(k);

                                    foreach (var j2 in second)
                                    {
                                        if (!first.Contains(j2))
                                            first.Add(j2);
                                    }

                                    first.Sort();
                                    islands2.Remove(second);
                                }
                            }
                            else if (first != null)
                            {
                                foreach (var k in list)
                                {
                                    if (!first.Contains(k))
                                        first.Add(k);
                                }
                                first.Sort();
                            }
                            else if (second != null)
                            {
                                foreach (var k in list)
                                {
                                    if (!second.Contains(k))
                                        second.Add(k);
                                }
                                second.Sort();
                            }
                            else
                            {
                                islands2.Add(new List<int>(list));
                            }
                            list = new List<int>();
                        }
                    }
                }

                islands = islands2.OrderBy(x=>x[0]).ToList();

                if (i == mat.Length - 2)
                {
                    ans += islands.Count;
                    break;
                }
            }

            return ans;
        }

        /// 202
        private List<int> happyList = new List<int>();

        public bool IsHappy(int n)
        {
            if (n == 1) return true;
            if (happyList.Contains(n)) return false;
            happyList.Add(n);

            return IsHappy(GetDigitSquare(n));
        }

        public int GetDigitSquare(int n)
        {
            int result = 0;
            int last;
            while (n > 0)
            {
                last = n % 10;
                result += last * last;
                n /= 10;
            }
            return result;
        }

        //203. Remove Linked List Elements
        public ListNode RemoveElements(ListNode head, int val)
        {
            while (head != null)
            {
                if (val == head.val)
                {
                    head = head.next;
                }
                else
                {
                    break;
                }
            }

            if (head == null)
                return null;

            var current = head;

            while (current.next != null)
            {
                if (current.next.val == val)
                {
                    current.next = current.next.next;
                }
                else
                {
                    current = current.next;
                }
            }

            return head;
        }

        //206. Reverse Linked List

        public ListNode ReverseList(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            var stack = new Stack<ListNode>();
            while (head != null)
            {
                var next = head.next;
                head.next = null;
                stack.Push(head);
                head = next;
            }

            ListNode result = null;
            ListNode current = null;

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (result == null)
                {
                    result = node;
                    current = result;
                }
                else
                {
                    current.next = node;
                    current = node;
                }
            }

            return result;
        }

        ///209. Minimum Size Subarray Sum
        ///return the minimal length of a contiguous subarray of which the sum >= target.
        ///If there is no such subarray, return 0 instead.
        public int MinSubArrayLen(int target, int[] nums)
        {
            int min = nums.Length+1;

            for(int i = 0; i < nums.Length; i++)
            {
                int sum = 0;
                int j = i;
                while ( j < nums.Length && j-i+1<min)
                {
                    sum+=nums[j];

                    if (sum >= target)
                    {
                        min = j - i + 1;
                        break;
                    }
                    j++;
                }

                if (min == 1)
                    return 1;
            }

            return min== nums.Length + 1?0:min;
        }
        /// 213. House Robber II
        ///All houses at this place are arranged in a circle. N-1 is next to 0
        public int Rob(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int[] withoutFirst = new int[nums.Length - 1];
            for (int i = 0; i < nums.Length - 1; i++)
                withoutFirst[i] = nums[i];
            int[] withoutLast = new int[nums.Length - 1];
            for (int i = 0; i < nums.Length - 1; i++)
                withoutLast[i] = nums[i + 1];

            //Return maximum of two results
            return Math.Max(Rob_Line(withoutFirst), Rob_Line(withoutLast));
        }

        public int Rob_Line(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int[] dp = new int[nums.Length];

            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);

            for (int i = 2; i < nums.Length; i++)
            {
                int a = nums[i] + dp[i - 2];
                dp[i] = Math.Max(a, dp[i - 1]);
            }

            return dp[nums.Length - 1];
        }

        //217. Contains Duplicate
        public bool ContainsDuplicate(int[] nums)
        {
            var dist = nums.Distinct();
            return dist.Count() != nums.Length;
        }

        ///221. Maximal Square
        ///Given an m x n binary matrix filled with 0's and 1's,
        ///find the largest square containing only 1's and return its area.
        public int MaximalSquare(char[][] matrix)
        {
            //loop matrix from top to bottom, you can also try left to right
            //store count of last '1'
            int[] dp=new int[matrix[0].Length];

            //max len of square
            int len = 0;

            foreach(var row in matrix)
            {
                for (int i = 0; i < dp.Length; i++)
                {
                    if (row[i] == '0')
                    {
                        dp[i] = 0;
                    }
                    else
                    {
                        dp[i]++;
                    }
                }

                for (int i = 0; i < dp.Length; i++)
                {
                    if (dp[i] <= len)
                        continue;

                    //count of '1' on left side and right side
                    int left = 0;
                    int right = 0;
                    //width = me(1) + left + right;
                    int width = 1;

                    //j to avoid to death loop
                    int j = 0;
                    int count = Math.Max(i, dp.Length - 1 - i);

                    while (j < count && width <= dp[i] && (i - 1 - left >= 0 || i + 1 + right <= dp.Length - 1))
                    {
                        if(((i - 1 - left)<0 || dp[i - 1 - left] < dp[i])
                            &&((i + 1 + right> dp.Length -1) || dp[i + 1 + right] < dp[i]))
                        {
                            break;
                        }

                        if (i - 1 - left >= 0 && dp[i - 1 - left] >= dp[i])
                            left++;

                        if (i + 1 + right <= dp.Length-1 && dp[i + 1 + right] >= dp[i])
                            right++;

                        width = 1 + left + right;

                        j++;
                    }

                    len = Math.Max(Math.Min(width,dp[i]) , len);
                }
            }

            return len * len;
        }
        /// 226. Invert Binary Tree
        ///Given the root of a binary tree, invert the tree, and return its root.

        public TreeNode InvertTree(TreeNode root)
        {
            InvertTree_Recursion(root);
            return root;
        }

        public void InvertTree_Recursion(TreeNode node)
        {
            if (node == null)
                return;

            if (node.left == null && node.right == null)
                return;

            var temp = node.left;
            node.left = node.right;
            node.right = temp;

            InvertTree_Recursion(node.left);
            InvertTree_Recursion(node.right);
        }

        ///231. Power of Two
        ///Given an integer n, return true if it is a power of two.

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

        //232. Implement Queue using Stacks

        //232. Implement Queue using Stacks

        //240
        public bool SearchMatrix(int[,] matrix, int target)
        {
            if (matrix == null) return false;

            return SerachMatrix(matrix, 0, matrix.GetLength(0), target);
        }

        public bool SerachMatrix(int[,] matrix, int startRowIndex, int endColIndex, int target)
        {
            if (matrix == null) return false;
            int col = matrix.GetLength(0);
            int row = matrix.GetLength(1);

            for (int i = startRowIndex; i < row; i++)
            {
                for (int j = 0; j < endColIndex; j++)
                {
                    if (matrix[i, j] == target) return true;
                    if (matrix[i, j] > target) return SerachMatrix(matrix, i, j, target);
                }
            }
            return false;
        }

        ///235. Lowest Common Ancestor of a Binary Search Tree
        ///Given a binary search tree (BST), find the lowest common ancestor (LCA) of two given nodes in the BST.

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            TreeNode left = p.val < q.val ? p : q;
            TreeNode right = p.val < q.val ? q : p;

            return LowestCommonAncestor_Recursion(root, left, right);
        }

        public TreeNode LowestCommonAncestor_Recursion(TreeNode root, TreeNode left, TreeNode right)
        {
            if (left.val < root.val && right.val > root.val)
                return root;

            if (left.val == root.val)
                return left;

            if (right.val == root.val)
                return right;

            if (left.val < root.val && right.val < root.val)
            {
                return LowestCommonAncestor_Recursion(root.left, left, right);
            }
            else
            {
                return LowestCommonAncestor_Recursion(root.right, left, right);
            }
        }

        ///238. Product of Array Except Self
        ///return an array such that answer[i] = product of all the elements of nums except nums[i].
        ///O(n) time and without using the division operation.
        public int[] ProductExceptSelf(int[] nums)
        {
            int[] ans = new int[nums.Length];

            int[] left = new int[nums.Length];
            int[] right = new int[nums.Length];

            int product1 = 1;
            int product2 = 1;
            for (int i = 0; i < nums.Length; i++)
            {
                left[i] = product1;
                right[nums.Length - 1 - i] = product2;

                product1 *= nums[i];
                product2 *= nums[nums.Length - 1 - i];
            }

            for(int i = 0;i< nums.Length; i++)
            {
                ans[i]=left[i]*right[i];
            }

            return ans;
        }
        /// 240. Search a 2D Matrix II
        public bool SearchMatrix(int[][] matrix, int target)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;

            if (matrix[0][0] > target || matrix[rowLen - 1][colLen - 1] < target)
                return false;

            foreach(var arr in matrix)
            {
                if (arr[0] > target || arr[arr.Length - 1] < target)
                    continue;

                int col1 = 0;
                int col2 = colLen - 1;
                int col = (col2 - col1) / 2;

                while (col1 <= col2 && col <= col2 && col >= col1)
                {
                    if (col1 == col2)
                    {
                        if(arr[col1]==target)
                            return true;
                        break;
                    }

                    if (arr[col] == target)
                    {
                        return true;
                    }
                    else if (arr[col] > target)
                    {
                        col2 = col - 1;
                        col = (col2 - col1) / 2 + col1;
                    }
                    else
                    {
                        col1 = col + 1;
                        col = (col2 - col1) / 2 + col1;
                    }
                }


            }

            return false;
        }
        /// 242. Valid Anagram

        public bool IsAnagram(string s, string t)
        {
            var arr1 = s.ToArray().ToList();
            var arr2 = t.ToArray().ToList();
            while (arr1.Count > 0)
            {
                var j = arr2.IndexOf(arr1[0]);
                if (j >= 0)
                {
                    arr1.RemoveAt(0);
                    arr2.RemoveAt(j);
                }
                else
                {
                    return false;
                }
            }
            return arr2.Count == 0;
        }

        //258
        public int AddDigits(int num)
        {
            if (num < 10) return num;

            int total = 0;
            while (num >= 10)
            {
                total += num % 10;
                num /= 10;
            }
            total += num;

            return AddDigits(total);
        }

        //278. First Bad Version

        public int FirstBadVersion(int n)
        {
            return FirstBadVersion(1, n);
        }

        public int FirstBadVersion(int start, int end)
        {
            if (start == end) return start;

            int num = end - start + 1;
            int mid = num / 2 + start - 1;

            if (IsBadVersion(mid))
            {
                return FirstBadVersion(start, mid);
            }
            else
            {
                return FirstBadVersion(mid + 1, end);
            }
        }

        public bool IsBadVersion(int n)
        {
            int bad = 10;
            return (n >= bad);
        }

        //283. Move Zeroes
        public void MoveZeroes(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return;

            int zeroCount = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (i + zeroCount >= nums.Length - 1)
                    break;

                while (nums[i] == 0)
                {
                    for (int j = i; j < nums.Length - 1 - zeroCount; j++)
                    {
                        nums[j] = nums[j + 1];
                    }

                    nums[nums.Length - 1 - zeroCount] = 0;

                    zeroCount++;

                    if (i + zeroCount >= nums.Length - 1)
                        break;
                }
            }

            Console.WriteLine($"zer0Count = {zeroCount}");
        }

        ///290. Word Pattern
        ///Given a pattern and a string s, find if s follows the same pattern.
        ///pattern = "abba", s = "dog cat cat dog", return true
        ///pattern = "abba", s = "dog dog dog dog", return false
        public bool WordPattern(string pattern, string s)
        {
            var carr = pattern.ToCharArray();
            var words = s.Split(' ');

            if (carr.Length != words.Length)
                return false;

            Dictionary<char, string> dict = new Dictionary<char, string>();

            for (int i = 0; i < carr.Length; i++)
            {
                if (dict.ContainsKey(carr[i]))
                {
                    if (dict[carr[i]] != words[i])
                    {
                        return false;
                    }
                }
                else if (dict.ContainsValue(words[i]))
                {
                    return false;
                }
                else
                {
                    dict.Add(carr[i], words[i]);
                }
            }

            return true;
        }
    }
}