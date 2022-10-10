using System;
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

        ///2404. Most Frequent Even Element
        public int MostFrequentEven(int[] nums)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var n in nums)
            {
                if (n % 2 == 0)
                {
                    if (dict.ContainsKey(n)) dict[n]++;
                    else dict.Add(n, 1);
                }
            }

            if (dict.Keys.Count == 0) return -1;
            int res = -1;
            int max = 0;
            foreach(var k in dict.Keys)
            {
                if (dict[k] > max)
                {
                    max = dict[k];
                    res = k;
                }
                else if (dict[k] == max)
                {
                    if (k < res)
                    {
                        res = k;
                    }
                }
            }
            return res;
        }

        ///2405. Optimal Partition of String
        public int PartitionString(string s)
        {
            int res = 0;
            HashSet<char> set = new HashSet<char>();
            res++;
            foreach(var c in s)
            {
                if (set.Contains(c))
                {
                    res++;
                    set = new HashSet<char>();
                }
                set.Add(c);
            }
            return res;
        }

        ///2406. Divide Intervals Into Minimum Number of Groups, #PriorityQueue
        public int MinGroups(int[][] intervals)
        {
            intervals = intervals.OrderBy(x => x[0]).ThenBy(x => x[1]).ToArray();
            var pq = new PriorityQueue<int, int>();
            foreach(var i in intervals)
            {
                if (pq.Count >0 && pq.Peek() < i[0])
                    pq.Dequeue();
                pq.Enqueue(i[1], i[1]);
            }
            return pq.Count;
        }


        ///2413. Smallest Even Multiple
        //public int SmallestEvenMultiple(int n)
        //{
        //    int i = 1;
        //    while (true)
        //    {
        //        if (i*2 % n == 0) return i*2;
        //        i++;
        //    }
        //    return -1;
        //}

        ///2414. Length of the Longest Alphabetical Continuous Substring
        public int LongestContinuousSubstring(string s)
        {
            int res = 1;
            char prev = s[0];
            int curr = 1;
            for(int i = 1; i<s.Length; i++)
            {
                if (s[i] - prev ==1) curr++;
                else curr =1;
                prev = s[i];
                res = Math.Max(res, curr);
            }
            return res;
        }


        ///2433. Find The Original Array of Prefix Xor
        //pref[i] = arr[0] ^ arr[1] ^ ... ^ arr[i].
        public int[] FindArray(int[] pref)
        {
            int n = pref.Length;
            int[] res = new int[n];
            for(int i = n-1; i>=0; i--)
            {
                if (i==0) res[i]=pref[i]^0;
                else res[i]=pref[i]^pref[i-1];
            }
            return res;
        }
    }
}
