using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1002. Find Common Characters
        ///return an array of all characters that show up in all strings within the words (including duplicates).
        public IList<string> CommonChars(string[] words)
        {
            var mat = new List<int[]>();
            foreach(var w in words)
            {
                var arr = new int[26];
                foreach (var c in w)
                    arr[c - 'a']++;
                mat.Add(arr);
            }
            List<string> res = new List<string>();
            for(int i = 0; i < 26; i++)
            {
                int min = 100;
                foreach(var arr in mat)
                {
                    if(arr[i] == 0)
                    {
                        min = 0;
                        break;
                    }
                    else
                    {
                        min = Math.Min(min, arr[i]);
                    }
                }
                while(min-- > 0)
                    res.Add(((char)('a' + i)).ToString());
            }
            return res;
        }
        /// 1004. Max Consecutive Ones III, #Sliding Window
        //return the maximum number of consecutive 1's in the array if you can flip at most k 0's.
        public int LongestOnes(int[] nums, int k)
        {
            int max = 0;
            int left = 0;//left index, i is right index
            int zeroCount = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    zeroCount++;
                    while (zeroCount > k && left <= i)
                    {
                        if (nums[left] == 0) { zeroCount--; }
                        left++;
                    }
                }
                max = Math.Max(max, i - left + 1);
            }
            return max;
        }

        ///1007. Minimum Domino Rotations For Equal Row
        ///Return the minimum number of rotations so that all the values in tops are the same,
        ///or all the values in bottoms are the same.If it cannot be done, return -1.
        public int MinDominoRotations(int[] tops, int[] bottoms)
        {
            int n = tops.Length;
            for (int i = 0, a = 0, b = 0; i < n && (tops[i] == tops[0] || bottoms[i] == tops[0]); ++i)
            {
                if (tops[i] != tops[0]) a++;
                if (bottoms[i] != tops[0]) b++;
                if (i == n - 1) return Math.Min(a, b);
            }
            for (int i = 0, a = 0, b = 0; i < n && (tops[i] == bottoms[0] || bottoms[i] == bottoms[0]); ++i)
            {
                if (tops[i] != bottoms[0]) a++;
                if (bottoms[i] != bottoms[0]) b++;
                if (i == n - 1) return Math.Min(a, b);
            }
            return -1;
        }
        /// 1014. Best Sightseeing Pair
        ///find max = values[i] + values[j] + i - j, 1 <= values[i] <= 1000
        public int MaxScoreSightseeingPair(int[] values)
        {
            //bestPoint =Max( value[i] + i)
            int maxv = Math.Max(values[0], values[1] + 1);
            int ans = values[1] + values[0] - 1;
            for (int i = 2; i < values.Length; i++)
            {
                ans = Math.Max(ans, maxv + values[i] - i);
                maxv = Math.Max(maxv, values[i] + i);
            }
            return ans;
        }

        ///1020. Number of Enclaves, #DFS, #Graph
        ///Return the number of land cells in grid for which we cannot walk off the boundary of the grid in any number of moves.
        public int NumEnclaves(int[][] grid)
        {
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            int ans = 0;
            bool[,] visit = new bool[rowLen, colLen];
            for (int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                {
                    if (grid[i][j] == 0 || visit[i, j]) continue;
                    Queue<int[]> q = new Queue<int[]>();
                    q.Enqueue(new int[] { i, j });
                    visit[i, j] = true;
                    int count = 1;
                    bool ignore = false;
                    while (q.Count > 0)
                    {
                        var p = q.Dequeue();
                        if (p[0] == 0 || p[0] == rowLen - 1 || p[1] == 0 || p[1] == colLen - 1)
                            ignore = true;
                        foreach (var d in dxy4)
                        {
                            var r = p[0] + d[0];
                            var c = p[1] + d[1];
                            if (r >= 0 && r < rowLen && c >= 0 && c < colLen && grid[r][c] == 1 && !visit[r, c])
                            {
                                visit[r, c] = true;
                                count++;
                                q.Enqueue(new int[] { r, c });
                            }
                        }
                    }
                    if (!ignore)
                        ans += count;
                }
            return ans;
        }

        ///1029. Two City Scheduling
        ///A company is planning to interview 2n people. Given the array costs where costs[i] = [aCosti, bCosti],
        ///the cost of flying the ith person to city a is aCosti, and the cost of flying the ith person to city b is bCosti.
        ///Return the minimum cost to fly every person to a city such that exactly n people arrive in each city.
        public int TwoCitySchedCost(int[][] costs)
        {
            int res = costs.Sum(x => x[0]);
            int[] diff = costs.Select(x=>x[1]-x[0]).ToArray();
            Array.Sort(diff);
            for (int i = 0; i < diff.Length / 2; i++)
                res += diff[i];
            return res;
        }
        ///1038. Binary Search Tree to Greater Sum Tree, #BTree
        public TreeNode BstToGst(TreeNode root)
        {
            BstToGst_Recursion(root, 0);
            return root;
        }
        private int BstToGst_Recursion(TreeNode root,int sum)
        {
            if (root == null)
                return sum;
            var rightVal = BstToGst_Recursion(root.right,sum);
            root.val += rightVal;
            return BstToGst_Recursion(root.left, root.val);
        }

        /// 1046. Last Stone Weight, #PriorityQueue
        /// x == y, both stones are destroyed, and
        ///If x != y, the stone of weight x is destroyed, and the stone of weight y has new weight y - x.
        ///Return the smallest possible weight of the left stone.If there are no stones left, return 0.
        public int LastStoneWeight(int[] stones)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            foreach (var n in stones)
                pq.Enqueue(n, -n);
            while (pq.Count >= 2)
            {
                int max1=pq.Dequeue();
                int max2=pq.Dequeue();
                if(max1>max2)
                    pq.Enqueue(max1-max2,max2-max1);
            }
            return pq.Count > 0 ? pq.Dequeue() : 0;
        }
        /// 1047. Remove All Adjacent Duplicates In String, #Two Pointer
        ///"caacbc"=>"bc"
        public string RemoveDuplicates_TwoPointer(string s)
        {
            int i = 0;
            char[] res = s.ToArray();
            for (int j = 0; j < s.Length; ++j, ++i)
            {
                res[i] = res[j];
                if (i > 0 && res[i - 1] == res[i]) // count = 2
                    i -= 2;
            }
            return new string(res, 0, i);
        }

        public string RemoveDuplicatesMy(string s)
        {
            int i = 0;
            var list = s.ToList();
            while (i < list.Count)
            {
                if (i > 0 && list[i] == list[i - 1])
                {
                    list.RemoveAt(i);
                    list.RemoveAt(i - 1);
                    i--;
                }
                else
                {
                    i++;
                }
            }
            return new string(list.ToArray());
        }
    }
}