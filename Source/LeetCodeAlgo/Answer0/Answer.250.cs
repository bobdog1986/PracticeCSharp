using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///257. Binary Tree Paths
        ///Given the root of a binary tree, return all root-to-leaf paths in any order.A leaf is a node with no children.
        public IList<string> BinaryTreePaths(TreeNode root)
        {
            var ans=new List<string>();
            BinaryTreePaths(root, string.Empty, ans);
            return ans;
        }

        public void BinaryTreePaths(TreeNode node, string path, IList<string> ans)
        {
            if (node == null) return;
            if (path != String.Empty) path += "->" + node.val.ToString();
            else path = node.val.ToString();
            if (node.left == null && node.right == null)
            {
                ans.Add(path);
            }
            else
            {
                if (node.left != null) BinaryTreePaths(node.left, path, ans);
                if (node.right != null) BinaryTreePaths(node.right, path, ans);
            }
        }
        /// 258. Add Digits
        ///Given an integer num, repeatedly add all its digits until the result has only one digit, and return it.
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

        ///260. Single Number III
        ///exactly two elements appear only once and all the other elements appear exactly twice.
        ///Find the two elements that appear only once. You can return the answer in any order.
        public int[] SingleNumber(int[] nums)
        {
            HashSet<int> set = new HashSet<int>();
            foreach(var n in nums)
            {
                if(set.Contains(n))set.Remove(n);
                else set.Add(n);
            }
            return set.ToArray();
        }
        /// 263. Ugly Number
        ///An ugly number is a positive integer whose prime factors are limited to 2, 3, and 5.
        ///Given an integer n, return true if n is an ugly number.
        public bool IsUgly(int n)
        {
            if (n <= 0) return false;
            while (n > 1)
            {
                int last = n;
                if (n % 2 == 0) n /= 2;
                if (n % 3 == 0) n /= 3;
                if (n % 5 == 0) n /= 5;
                if(n==last) return false;
            }
            return true;
        }
        /// 264. Ugly Number II - NOT mine
        /// An ugly number is a positive integer whose prime factors are limited to 2, 3, and 5.
        /// Given an integer n, return the nth ugly number.
        /// (1) 1×2, 2×2, 3×2, 4×2, 5×2, …
        /// (2) 1×3, 2×3, 3×3, 4×3, 5×3, …
        /// (3) 1×5, 2×5, 3×5, 4×5, 5×5, …
        public int NthUglyNumber(int n)
        {
            int[] ugly = new int[n];
            ugly[0] = 1;
            int index2 = 0, index3 = 0, index5 = 0;
            int factor2 = 2, factor3 = 3, factor5 = 5;
            for (int i = 1; i < n; i++)
            {
                int min = Math.Min(Math.Min(factor2, factor3), factor5);
                ugly[i] = min;
                if (factor2 == min)
                    factor2 = 2 * ugly[++index2];
                if (factor3 == min)
                    factor3 = 3 * ugly[++index3];
                if (factor5 == min)
                    factor5 = 5 * ugly[++index5];
            }
            return ugly[n - 1];
        }
        ///268. Missing Number
        ///Given an array nums containing n distinct numbers in the range [0, n],
        ///return the only number in the range that is missing from the array.
        public int MissingNumber(int[] nums)
        {
            bool[] arr=new bool[nums.Length + 1];
            foreach(var num in nums)
                arr[num] = true;
            for (int i=0; i < arr.Length; i++)
            {
                if(!arr[i])
                    return i;
            }
            return -1;
        }
        ///274. H-Index
        ///Given an array of integers citations where citations[i] is the number of citations
        ///a researcher received for their ith paper, return compute the researcher's h-index.
        public int HIndex_274_O_N(int[] citations)
        {
            int n = citations.Length;
            int[] count = new int[n + 1];
            foreach (int c in citations)
            {
                if (c > n) count[n]++;//greater than count
                else count[c]++;
            }
            int total = 0;
            for (int i = n; i >= 0; i--)
            {
                total += count[i];
                if (total >= i)
                    return i;
            }
            return 0;
        }

        public int HIndex_274_NlogN(int[] citations)
        {
            Array.Sort(citations);
            int n = citations.Length;
            for (int i = 0; i <n;i++)
            {
                int count = n-i;
                if (citations[i] >= count) return count;
            }
            return 0;
        }

        ///275. H-Index II, #Binary Search
        public int HIndex(int[] citations)
        {
            int n = citations.Length;
            if (citations[n-1] == 0) return 0;
            int left = 0;
            int right=n-1;
            while (left < right)
            {
                int mid=(left+right)/2;
                int count = n - mid;
                if (citations[mid] >= count) right = mid;
                else left = mid + 1;
            }
            return n - right;
        }

        /// 278. First Bad Version, #Binary Search
        /// 1 <= bad <= n <= 2^31 - 1
        public int FirstBadVersion(int n)
        {
            int left = 1;
            int right = n;
            while(left< right)
            {
                int mid = left + (right - left) / 2;
                if (IsBadVersion(mid))
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return left;
        }

        public bool IsBadVersion(int n)//API provides by leetcode
        {
            return (n >= 1702766719);
        }

        ///279. Perfect Squares, #DP
        ///Given an integer n, return the least number of perfect square numbers that sum to n.
        ///A perfect square is an integer that is the square of an integer;
        public int NumSquares(int n)
        {
            int[] dp = new int[n + 1];
            for (int i = 1; i <= n; i++)
                dp[i] = int.MaxValue;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j * j <= i; j++)
                {
                    dp[i] = Math.Min(dp[i], dp[i - j * j] + 1);
                }
            }
            return dp[n];
        }

        /// 283. Move Zeroes
        /// move all 0's to the end of it while maintaining the relative order of the non-zero elements.
        public void MoveZeroes(int[] nums)
        {
            int i = 0;
            int[] temp=new int[nums.Length];
            foreach(var n in nums)
            {
                if(n != 0)
                    temp[i++] = n;
            }
            for (int j=0;j < nums.Length; j++)
            {
                nums[j] = j < i ? temp[j] : 0;
            }
        }

        ///284. Peeking Iterator， see PeekingIterator

        /// 287. Find the Duplicate Number
        ///Given an array of integers nums containing n + 1 integers where each integer is in the range [1, n] inclusive.
        ///There is only one repeated number in nums, return this repeated number.
        public int FindDuplicate(int[] nums)
        {
            int[] arr=new int[nums.Length+1];
            foreach(var n in nums)
            {
                if (arr[n] == 1) return n;
                arr[n] = 1;
            }
            return 0;
        }

        /// 289. Game of Life
        /// For 1, count of 1 in hor,ver , dragonal is 2 or 3 =>1
        /// For 0, count of 1 in hor,ver , dragonal is 3 =>1
        public void GameOfLife(int[][] board)
        {
            int rowLen= board.Length;
            int colLen= board[0].Length;
            bool[,] matrix=new bool[rowLen, colLen];

            for(int i = 0; i < rowLen; i++)
            {
                for(int j = 0; j < colLen; j++)
                {
                    int count = 0;
                    if (i > 0)
                    {
                        if (j > 0)
                            count += board[i-1][j - 1];
                        count += board[i-1][j];
                        if (j < colLen - 1)
                            count += board[i-1][j + 1];
                    }

                    if (j > 0)
                        count += board[i][j - 1];
                    if (j < colLen - 1)
                        count += board[i][j + 1];

                    if (i < rowLen - 1)
                    {
                        if (j > 0)
                            count += board[i + 1][j - 1];
                        count += board[i+1][j];
                        if (j < colLen - 1)
                            count += board[i + 1][j + 1];
                    }

                    if(board[i][j] == 0)
                    {
                        matrix[i, j] = count == 3;
                    }
                    else
                    {
                        matrix[i, j] = count == 2 || count == 3;
                    }
                }
            }

            for(int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                    board[i][j] = matrix[i, j] ? 1 : 0;
        }
        /// 290. Word Pattern
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

        ///292. Nim Game
        ///Given n, the number of stones in the heap, return true if you can win the game
        ///assuming both you and your friend play optimally, otherwise return false.
        public bool CanWinNim(int n)
        {
            return n%4!=0;
        }

        ///295. Find Median from Data Stream, See MedianFinder

        /// 297. Serialize and Deserialize Binary Tree, see Codec

        ///299. Bulls and Cows
        public string GetHint(string secret, string guess)
        {
            int[] arr1 = new int[10];
            int[] arr2 = new int[10];
            int bull = 0;
            for(int i = 0; i < secret.Length; i++)
            {
                if (secret[i] == guess[i]) bull++;
                else
                {
                    arr1[secret[i] - '0']++;
                    arr2[guess[i] - '0']++;
                }
            }

            int cow = 0;
            for(int i = 0; i < arr1.Length; i++)
            {
                cow += Math.Min(arr1[i], arr2[i]);
            }

            return $"{bull}A{cow}B";
        }

    }
}
