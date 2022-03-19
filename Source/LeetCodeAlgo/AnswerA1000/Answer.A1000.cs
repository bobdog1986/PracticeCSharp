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

        ///1047. Remove All Adjacent Duplicates In String, #Two Pointer
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