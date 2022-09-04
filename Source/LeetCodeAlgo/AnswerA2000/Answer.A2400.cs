﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        //2400. Number of Ways to Reach a Position After Exactly k Steps
        public int NumberOfWays(int startPos, int endPos, int k)
        {
            long mod = 1_000_000_007;
            Dictionary<int, long> dict = new Dictionary<int, long>();
            dict.Add(startPos, 1);
            while (k-- > 0)
            {
                var next = new Dictionary<int, long>();
                foreach (var x in dict.Keys)
                {
                    if (!next.ContainsKey(x + 1))
                        next.Add(x + 1, 0);
                    next[x + 1] += dict[x];//move right
                    next[x + 1] %= mod;

                    if (!next.ContainsKey(x - 1))
                        next.Add(x - 1, 0);
                    next[x - 1] += dict[x];//move left
                    next[x - 1] %= mod;
                }
                dict = next;
            }
            if (dict.ContainsKey(endPos)) return (int)dict[endPos];
            else return 0;
        }

        ///2401. Longest Nice Subarray, #Sliding Window
        //a subarray of nums nice if the bitwise AND of every pair of elements is equal to 0.
        public int LongestNiceSubarray(int[] nums)
        {
            int res = 1;
            int[] arr = new int[32];//store count of bit-1 of from bit0 to bit31
            int left = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                var curr = getBitArrayOfInt(nums[i]);
                for (int j = 0; j <= 30; j++)
                    arr[j] += curr[j];
                //not nice, remove the lefe most element
                while (arr.Max() > 1 && left < i)
                {
                    var prev = getBitArrayOfInt(nums[left++]);
                    for (int j = 0; j <= 30; j++)
                        arr[j] -= prev[j];
                }
                res = Math.Max(res, i - left + 1);
            }
            return res;
        }

        private int[] getBitArrayOfInt(int x)
        {
            int[] res = new int[32];
            for (int i = 0; i <= 30; i++)
            {
                if ((x & (1 << i)) != 0)
                    res[i] = 1;
            }
            return res;
        }

        ///2402. Meeting Rooms III, #PriorityQueue
        //There are n rooms numbered from 0 to n - 1.
        //meetings where meetings[i] = [starti, endi] half-closed time interval [starti, endi)
        //Return the number of the room that held the most meetings.
        //If there are multiple rooms, return the room with the lowest number.
        public int MostBooked(int n, int[][] meetings)
        {
            int res = 0;
            int[] arr = new int[n];
            meetings = meetings.OrderBy(x => x[0]).ToArray();//sort by start time
            //pq of room, store {index, endTime}, return min startTime with min index
            PriorityQueue<int[], int[]> pq = new PriorityQueue<int[], int[]>(
                Comparer<int[]>.Create((a, b) =>
                {
                    if (a[1] != b[1])
                        return a[1] - b[1];
                    else
                        return a[0] - b[0];
                }));

            for (int i = 0; i < n; i++)
            {
                int[] curr = new int[] { i, 0 };//init pq, all element from 0
                pq.Enqueue(curr, curr);
            }

            foreach (var m in meetings)
            {
                //if any element's entTime < curr meeting's startTime, update it
                while (pq.Peek()[1] < m[0])
                {
                    var curr = pq.Dequeue();
                    curr[1] = m[0];
                    pq.Enqueue(curr, curr);
                }

                var top = pq.Dequeue();
                arr[top[0]]++;
                top[1] += m[1] - m[0];
                pq.Enqueue(top, top);
            }

            int max = -1;
            for (int i = 0; i < n; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                    res = i;
                }
            }
            return res;
        }
    }
}