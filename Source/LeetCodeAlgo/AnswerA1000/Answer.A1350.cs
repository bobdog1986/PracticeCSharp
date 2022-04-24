using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1351. Count Negative Numbers in a Sorted Matrix, #Binary Search
        ///Given a m x n matrix grid which is sorted in non-increasing order
        ///both row-wise and column-wise, return the number of negative numbers in grid.
        public int CountNegatives(int[][] grid)
        {
            int m = grid.Length;
            int n = grid[0].Length;
            int res = 0;
            int lastNeg = n - 1;
            for (int i = 0; i < m; i++)
            {
                //check edge cases - if first element is < 0 - all elements in row are negative
                if (grid[i][0] < 0)
                {
                    res += n;
                    continue;
                }
                //if last element is positive - it means there are no negative numbers in a row
                if (grid[i][n - 1] > 0)
                    continue;
                //binary search, left is the first negative
                int left = 0, right = lastNeg;
                while (left <= right)
                {
                    int mid = left + (right - left) / 2;
                    if (grid[i][mid] < 0)
                        right = mid - 1;
                    else
                        left = mid + 1;
                }
                res += (n - left);//from indexes of n-1 to left
                lastNeg = left;//update
            }
            return res;
        }

        /// 1356. Sort Integers by The Number of 1 Bits
        ///1 <= arr.length <= 500, 0 <= arr[i] <= 10^4
        public int[] SortByBits(int[] arr)
        {
            return arr.OrderBy(x=> SortByBits_BitsCount(x)).ThenBy(x => x).ToArray();
        }

        public int SortByBits_BitsCount(int n)
        {
            int res = 0;
            while (n > 0)
            {
                if ((n & 1) == 1) res++;
                n >>= 1;
            }
            return res;
        }

        /// 1359. Count All Valid Pickup and Delivery Options, #DP
        ///Given n orders, each order consist in pickup and delivery services.
        ///Count all valid pickup/delivery possible sequences such that delivery(i) is always after of pickup(i).
        ///Since the answer may be too large, return it modulo 10^9 + 7.
        public int CountOrders(int n)
        {
            if (n == 1) return 1;
            long dp = 1;
            long mod = 10_0000_0007;
            int i = 2;
            while (i <= n)
            {
                dp *= (i * (2 * i - 1));
                dp %= mod;
                i++;
            }
            return (int)dp;
        }


        ///1360. Number of Days Between Two Dates
        ///Write a program to count the number of days between two dates.
        ///The two dates are given as strings, their format is YYYY-MM-DD as shown in the examples.
        public int DaysBetweenDates(string date1, string date2)
        {
            return Math.Abs((DateTime.Parse(date1) - DateTime.Parse(date2)).Days);
        }
        /// 1365. How Many Numbers Are Smaller Than the Current Number
        ///for each nums[i] find out how many numbers in the array are smaller than it.
        ///0 <= nums[i] <= 100, 2 <= nums.length <= 500
        public int[] SmallerNumbersThanCurrent(int[] nums)
        {
            int[] arr=new int[101];
            Dictionary<int, List<int>> map=new Dictionary<int, List<int>>();
            for(int i = 0; i < nums.Length; i++)
            {
                arr[nums[i]]++;
                if(!map.ContainsKey(nums[i]))
                    map[nums[i]] = new List<int>();
                map[nums[i]].Add(i);
            }

            int count=nums.Length;
            int[] ans = new int[nums.Length];
            for(int i=arr.Length-1; i>=0 && count>0; i--)
            {
                if (arr[i] == 0) continue;
                count-=arr[i];
                foreach(var index in map[i])
                    ans[index] = count;
            }
            return ans;
        }
        /// 1366. Rank Teams by Votes
        ///Return a string of all teams sorted by the ranking system.
        public string RankTeams(string[] votes)
        {
            int len = votes[0].Length;//votes count in each vote string, eg "ABC" is 3
            int[][] matrix = new int[26][];//at most 26 teams from A to Z
            for(int i=0;i<26;i++)
                matrix[i] = new int[len];

            foreach(var vote in votes)
            {
                for(int i=0;i<vote.Length;i++)
                {
                    matrix[vote[i] - 'A'][i]++;//calculate all votes for every team
                }
            }
            var teams= votes[0].ToList();//get all teams
            teams.Sort((x, y) =>
            {
                //compare score from 0 index to len-1, higher votes win
                int i = 0;
                while (i < votes[0].Length)
                {
                    if(matrix[x-'A'][i]> matrix[y-'A'][ i])
                    {
                        return -1;
                    }
                    else if(matrix[x - 'A'][i]< matrix[y - 'A'][i])
                    {
                        return 1;
                    }
                    i++;
                }
                //if all same , sort by alphabetically
                return x -y;
            });
            return new string(teams.ToArray());
        }

        ///1367. Linked List in Binary Tree, #BTree
        ///Given a binary tree root and a linked list with head as the first node.
        ///Return True if all the elements in the linked list starting from the head
        ///correspond to some downward path connected in the binary tree otherwise return False.
        public bool IsSubPath(ListNode head, TreeNode root)
        {
            return IsSubPath_Recursion(head, root, head);
        }

        public bool IsSubPath_Recursion(ListNode head, TreeNode root, ListNode origin)
        {
            if (head == null) return true;
            if (root == null) return false;
            if (head.val == root.val)
            {
                var leftOk = IsSubPath_Recursion(head.next, root.left,origin);
                if (leftOk) return true;
                var rightOk = IsSubPath_Recursion(head.next, root.right, origin);
                if (rightOk) return true;
            }
            if(head == origin)
            {
                return IsSubPath_Recursion(head, root.left, origin) || IsSubPath_Recursion(head, root.right, origin);
            }
            else
            {
                return false;
            }
        }

        /// 1375. Number of Times Binary String Is Prefix-Aligned
        ///Return the number of times the binary string is prefix-aligned during the flipping process.
        public int NumTimesAllBlue(int[] flips)
        {
            int right = 0;
            int ans = 0;
            int index = 1;
            foreach(var n in flips)
            {
                right = Math.Max(right, n);
                if (right == index) ans++;
                index++;
            }
            return ans;
        }

        ///1376. Time Needed to Inform All Employees, #Graph, #BFS, #DFS
        ///A company has n employees from 0 to n - 1. The head of the company is the one with headID.
        ///Each employee has one direct manager given in the manager array where manager[i]
        ///is the direct manager of the i-th employee, manager[headID] = -1.
        ///Also, it is guaranteed that the subordination relationships have a tree structure.
        ///The i-th employee needs informTime[i] minutes to inform all of his direct subordinates
        ///(i.e., After informTime[i] minutes, all his direct subordinates can start spreading the news).
        ///Return the number of minutes needed to inform all the employees about the urgent news.

        public int NumOfMinutes_BFS(int n, int headID, int[] manager, int[] informTime)
        {
            List<int>[] graph =new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();

            for (int i = 0; i < n; i++)
                if (manager[i] != -1) graph[manager[i]].Add(i);

            Queue<int[]> q = new Queue<int[]>(); // Since it's a tree, we don't need `visited` array
            q.Enqueue(new int[] { headID, 0 });
            int ans = 0;
            while (q.Count>0)
            {
                int[] top = q.Dequeue();
                int u = top[0], w = top[1];
                ans = Math.Max(w, ans);
                foreach (int v in graph[u])
                    q.Enqueue(new int[] { v, w + informTime[u] });
            }
            return ans;
        }
        public int NumOfMinutes_DFS(int n, int headID, int[] manager, int[] informTime)
        {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();
            for (int i = 0; i < n; i++)
                if (manager[i] != -1) graph[manager[i]].Add(i);

            return NumOfMinutes_DFS(graph, headID, informTime);
        }
        public int NumOfMinutes_DFS(List<int>[] graph, int u, int[] informTime)
        {
            int ans = 0;
            foreach (int v in graph[u])
                ans = Math.Max(ans, NumOfMinutes_DFS(graph, v, informTime));
            return ans + informTime[u];
        }

        ///1379. Find a Corresponding Node of a Binary Tree in a Clone of That Tree
        public TreeNode GetTargetCopy(TreeNode original, TreeNode cloned, TreeNode target)
        {
            TreeNode res = null;
            GetTargetCopy(original, cloned, target, ref res);
            return res;
        }

        private void GetTargetCopy(TreeNode original, TreeNode cloned, TreeNode target,ref TreeNode found)
        {
            if (original==null) return;
            if (found!=null) return;
            if (original == target)
            {
                found = cloned;
                return;
            }
            GetTargetCopy(original.left, cloned.left, target, ref found);
            GetTargetCopy(original.right, cloned.right, target, ref found);
        }

        /// 1380. Lucky Numbers in a Matrix
        ///A lucky number is an element of the matrix such that it is the minimum element in its row and maximum in its column.
        ///1 <= n, m <= 50 , 1 <= matrix[i][j] <= 10^5.
        public IList<int> LuckyNumbers(int[][] matrix)
        {
            var rowLen = matrix.Length;
            var colLen=matrix[0].Length;
            int[] minOfRows=new int[rowLen];
            for(int i =0;i<rowLen;i++)
                minOfRows[i]=int.MaxValue;
            int[] maxOfCols=new int[colLen];
            for(int i=0;i<rowLen;i++)
                for(int j = 0; j < colLen; j++)
                {
                    minOfRows[i] = Math.Min(minOfRows[i], matrix[i][j]);
                    maxOfCols[j] = Math.Max(maxOfCols[j], matrix[i][j]);
                }
            var ans=new List<int>();
            for (int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                {
                    if(matrix[i][j] == minOfRows[i] && matrix[i][j] == maxOfCols[j])
                        ans.Add(matrix[i][j]);
                }
            return ans;
        }


        ///1382. Balance a Binary Search Tree, #BTree, #BST
        ///return a balanced binary search tree with the same node values. return any answer.
        ///A binary search tree is balanced if the depth of the two subtrees of every node never differs by more than 1.
        public TreeNode BalanceBST(TreeNode root)
        {
            var list = new List<int>();
            BalanceBST_Recur(root, list);
            return BalanceBST_Build(list, 0, list.Count - 1);
        }

        private void BalanceBST_Recur(TreeNode root, List<int> list)
        {
            if (root == null) return;
            BalanceBST_Recur(root.left, list);
            list.Add(root.val);
            BalanceBST_Recur(root.right, list);
        }

        private TreeNode BalanceBST_Build(List<int> list, int left, int right)
        {
            if (left > right) return null;
            else if (left == right) return new TreeNode(list[left]);
            else
            {
                var mid = (left + right) / 2;
                var leftTree = BalanceBST_Build(list, left, mid - 1);
                var rightTree = BalanceBST_Build(list, mid+1,right );
                return new TreeNode(list[mid],leftTree,rightTree);
            }
        }

        /// 1385. Find the Distance Value Between Two Arrays
        ///Given two integer arrays arr1 and arr2, and the integer d, return the distance value between the two arrays.
        ///The distance value is defined as the number of elements arr1[i] such that there is not any element arr2[j] where |arr1[i]-arr2[j]| <= d.
        public int FindTheDistanceValue(int[] arr1, int[] arr2, int d)
        {
            return arr1.Where(a => !arr2.Any(b => Math.Abs(a - b) <= d)).Count();
        }
        /// 1394. Find Lucky Integer in an Array
        ///Given an array of integers arr, a lucky integer is an integer that has a frequency in the array equal to its value.
        ///Return the largest lucky integer in the array.If there is no lucky integer return -1.
        public int FindLucky(int[] arr)
        {
            Dictionary<int,int> map = new Dictionary<int,int>();
            foreach(var n in arr)
            {
                if (map.ContainsKey(n)) map[n]++;
                else map.Add(n, 1);
            }
            var keys=map.Keys.OrderBy(x=>-x);
            foreach(var key in keys)
                if (map[key] == key) return key;
            return -1;
        }

        ///1396. Design Underground System, see UndergroundSystem

    }
}
