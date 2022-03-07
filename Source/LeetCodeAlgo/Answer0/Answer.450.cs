﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///450. Delete Node in a BST
        ///The number of nodes in the tree is in the range [0, 10^4].
        public TreeNode DeleteNode(TreeNode root, int key)
        {
            if(root == null)
                return null;
            if (root.val == key)
            {
                return DeleteNode(root);
            }
            else if(key>root.val)
            {
                root.right = DeleteNode(root.right, key);
            }
            else
            {
                root.left = DeleteNode(root.left, key);
            }
            return root;
        }

        public TreeNode DeleteNode(TreeNode root)
        {
            if (root.left == null)
                return root.right;
            if (root.right == null)
                return root.left;

            //inset the right subtree to left subtree
            var ans=root.left;
            var node = root.left;
            while (node.right != null)
            {
                node=node.right;
            }
            node.right = root.right;
            return ans;
        }

        ///451. Sort Characters By Frequency
        ///Given a string s, sort it in decreasing order based on the frequency of the characters.
        ///The frequency of a character is the number of times it appears in the string.
        public string FrequencySort(string s)
        {
            Dictionary<char, List<char>> dict = new Dictionary<char, List<char>>();
            foreach(var c in s)
            {
                if (dict.ContainsKey(c))
                {
                    dict[c].Add(c);
                }
                else
                {
                    dict.Add(c, new List<char>() { c});
                }
            }
            return String.Join("", dict.OrderBy(x=>-x.Value.Count).Select(x=> string.Join("", x.Value)));
        }
        /// 452. Minimum Number of Arrows to Burst Balloons
        ///points.Length = Balloons number, Balloons horizontal -231 <= xstart < xend <= 231 - 1
        public int FindMinArrowShots(int[][] points)
        {
            if (points.Length <= 1)
                return points.Length;

            var arr = points.OrderBy(p => p[1]).ToList();

            var shot = 1;
            int end = arr[0][1];

            for (int i = 1; i < arr.Count; i++)
            {
                if (end < arr[i][0])
                {
                    end = arr[i][1];
                    shot++;
                }
            }

            return shot;
        }

        ///454. 4Sum II -- O(n^4) --> O(n^2), using Dictionary
        ///Given four integer arrays nums1, nums2, nums3, and nums4 all of length n,
        ///return the number of tuples (i, j, k, l) such that:nums1[i] + nums2[j] + nums3[k] + nums4[l] == 0
        public int FourSumCount(int[] nums1, int[] nums2, int[] nums3, int[] nums4)
        {
            int ans = 0;
            Dictionary<int, int> dictSumOf34 = new Dictionary<int, int>();
            for(int i = 0; i < nums3.Length; i++)
            {
                for(int j=0;j<nums4.Length; j++)
                {
                    int sum=nums3[i]+nums4[j];
                    if (dictSumOf34.ContainsKey(sum))
                    {
                        dictSumOf34[sum]++;
                    }
                    else
                    {
                        dictSumOf34.Add(sum, 1);
                    }
                }
            }
            for (int i = 0; i < nums1.Length; i++)
            {
                for (int j = 0; j < nums2.Length; j++)
                {
                    int sum = nums1[i] + nums2[j];
                    if (dictSumOf34.ContainsKey(-sum))
                    {
                        ans+= dictSumOf34[-sum];
                    }
                }
            }
            return ans;
        }
        /// 457. Circular Array Loop
        ///If nums[i]>0, move nums[i] steps forward, If nums[i]<0 move nums[i] steps backward.
        ///Every nums[seq[j]] is either all positive or all negative.
        ///Return true if there is a cycle in nums, or false otherwise.
        public bool CircularArrayLoop(int[] nums)
        {
            bool[] visit = new bool[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                if (visit[i])
                    continue;
                bool forward = nums[i] > 0;
                visit[i] = true;

                bool[] currVisit=new bool[nums.Length];
                currVisit[i] = true;
                int lastVisit = i;

                int j = 0;
                int next = (i + nums[i]) % nums.Length;
                if (next < 0)
                    next += nums.Length;

                while (j++ < nums.Length)
                {
                    if ((forward && nums[next] < 0) || (!forward && nums[next] > 0))
                        break;
                    if (currVisit[next] && next != lastVisit)
                        return true;
                    if (visit[next])
                        break;
                    visit[next] = true;
                    currVisit[next] = true;
                    lastVisit = next;
                    next = (next + nums[next]) % nums.Length;
                    if (next < 0)
                        next += nums.Length;
                }
            }
            return false;
        }
        ///461. Hamming Distance
        ///return the Hamming distance between two integers, is the number of positions at which the corresponding bits are different.
        public int HammingDistance(int x, int y)
        {
            int count = 0;
            while(x!=0 && y != 0)
            {
                if ((x & 1) != (y & 1))
                {
                    count++;
                }
                x>>= 1;
                y>>= 1;
            }
            return count;
        }
        ///463. Island Perimeter, #BFS
        ///Determine the perimeter of the island.
        public int IslandPerimeter(int[][] grid)
        {
            int ans = 0;
            int rowLen = grid.Length;
            int colLen = grid[0].Length;
            int row = -1;
            int col = -1;
            for(int i = 0; i < rowLen; i++)
            {
                for(int j = 0; j < colLen; j++)
                {
                    if(grid[i][j] == 1)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                if (row != -1) break;
            }
            int[][] dxy4 = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            bool[,] visit = new bool[rowLen, colLen];
            Queue<int[]> queue= new Queue<int[]>();
            queue.Enqueue(new int[] { row, col });
            visit[row, col] = true;
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                int count = 0;
                foreach(var d in dxy4)
                {
                    var r = p[0] + d[0];
                    var c = p[1] + d[1];
                    if (r >= 0 && r < rowLen && c >= 0 && c < colLen && grid[r][c] == 1)
                    {
                        if(!visit[r, c])
                        {
                            visit[r, c] = true;
                            queue.Enqueue(new int[] { r, c });
                        }
                    }
                    else
                    {
                        count++;
                    }
                }
                ans += count;
            }
            return ans;
        }
        ///472. Concatenated Words
        ///Given an array of strings words (without duplicates), return all the concatenated words in the given list of words.
        ///A concatenated word is defined as a string that is comprised entirely of at least two shorter words in the given array.

        public IList<string> FindAllConcatenatedWordsInADict(string[] words)
        {
            HashSet<string> res = new HashSet<string>();
            HashSet<string> map = new HashSet<string>();
            var list = words.OrderBy(x => x.Length).ToList();

            int count = 0;
            if (list.Count > 0 && list[0].Length == 0)
            {
                count++;
                list.RemoveAt(0);
            }

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Length <= list[0].Length || list[i].Length < 2 * list[0].Length) continue;
                var records = new List<string>() { list[i] };
                FindAllConcatenatedWordsInADict_BackTracking(list, records, list[i], list[i], res, map);
            }
            return res.ToList();
        }

        public void FindAllConcatenatedWordsInADict_BackTracking(IList<string> words, IList<string> records, string origin, string curr, HashSet<string> res, HashSet<string> map)
        {
            if (map.Contains(curr)||curr.Length==0)
            {
                if(!res.Contains(origin))res.Add(origin);

                foreach(var r in records)
                {
                    if(!map.Contains(r))map.Add(r);
                }
                return;
            }

            for (int i = 0; i < words.Count; i++)
            {
                if (words[i] == origin) continue;
                if (words[i].Length > curr.Length) break;
                if (curr.StartsWith(words[i]))
                {
                    var nextRecords = new List<string>(records) { words[i] };
                    FindAllConcatenatedWordsInADict_BackTracking(words, nextRecords, origin, curr.Substring(words[i].Length), res,map);
                }
            }
        }
        /// 485. Max Consecutive Ones
        ///Given a binary array nums, return the maximum number of consecutive 1's in the array.
        public int FindMaxConsecutiveOnes(int[] nums)
        {
            var max = 0;
            int count = 0;
            foreach(var n in nums)
            {
                if (n == 1) count++;
                else
                {
                    max = Math.Max(max, count);
                    count = 0;
                }
            }
            max = Math.Max(max, count);
            return max;
        }

        /// 492
        public int[] ConstructRectangle(int area)
        {
            int[] result = new int[2] { area, 1 };
            int min = (int)Math.Sqrt(area);
            for (int len = min; len < area; len++)
            {
                if (area % len == 0)
                {
                    if (len >= area / len)
                    {
                        return new int[2] { len, area / len };
                    }
                }
            }
            return result;
        }

        //495
        public int FindPoisonedDuration(int[] timeSeries, int duration)
        {
            if (timeSeries == null || timeSeries.Length == 0) return 0;

            Array.Sort(timeSeries);

            int begin = timeSeries[0];
            int expired = begin + duration;

            int total = 0;
            for (int i = 1; i < timeSeries.Length; i++)
            {
                if (timeSeries[i] < expired)
                {
                    expired = timeSeries[i] + duration;
                }
                else
                {
                    total += expired - begin;

                    begin = timeSeries[i];
                    expired = begin + duration;
                }
            }

            total += expired - begin;
            return total;
        }
        ///496. Next Greater Element I
        ///Return an array ans of length nums1.length such that ans[i] is the next greater element as described above.
        public int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for(int i = 0; i < nums2.Length; i++)
                dict.Add(nums2[i], i);

            int[] ans = new int[nums1.Length];
            for(int i = 0; i < nums1.Length; i++)
            {
                int k = -1;
                for (int j = dict[nums1[i]] + 1; j < nums2.Length; j++)
                {
                    if(nums2[j] > nums1[i])
                    {
                        k = nums2[j];
                        break;
                    }
                }
                ans[i] = k;
            }
            return ans;
        }
    }
}