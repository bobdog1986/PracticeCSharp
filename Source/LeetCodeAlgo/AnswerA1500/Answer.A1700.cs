using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1700. Number of Students Unable to Eat Lunch
        //student[i] = 1 want 1, or =0 want 0. if cannot goback to end of queue
        //return how many students cannot get sanwiches
        public int CountStudents(int[] students, int[] sandwiches)
        {
            int n = students.Length;
            int squares = students.Sum();
            int circulars = n-squares;
            var q = new Queue<int>();
            foreach (var student in students)
                q.Enqueue(student);

            int i = 0;
            for (; i < n; i++)
            {
                if (q.Peek() == sandwiches[i])
                {
                    var top = q.Dequeue();
                    if (top == 0) circulars--;
                    else squares--;
                }
                else
                {
                    if (sandwiches[i] == 0 && squares == q.Count) break;
                    if (sandwiches[i] == 1 && circulars == q.Count) break;
                    while(q.Peek()!= sandwiches[i])
                    {
                        q.Enqueue(q.Dequeue());
                    }
                    var top = q.Dequeue();
                    if (top == 0) circulars--;
                    else squares--;
                }
            }
            return n - i;
        }

        ///1701. Average Waiting Time
        ///Only 1 chef, customers[i] = [arrivali, timei], Return the average waiting time of all customers.
        public double AverageWaitingTime(int[][] customers)
        {
            double total = 0;
            int n = customers.Length;
            double start = 0;
            for (int i = 0; i < n; ++i)
            {
                start = Math.Max(start, customers[i][0]);
                double end = start + customers[i][1];
                total += end - customers[i][0];
                start = end;
            }
            return total / n;
        }

        ///1704. Determine if String Halves Are Alike
        public bool HalvesAreAlike(string s)
        {
            int res = 0;
            Dictionary<char, int> dict = new Dictionary<char, int>()
            {
                { 'a', 0 },{ 'e', 0 },{ 'i', 0 },{ 'o', 0 },{ 'u', 0 },
                { 'A', 0 },{ 'E', 0 },{ 'I', 0 },{ 'O', 0 },{ 'U', 0 },
            };
            for(int i = 0; i < s.Length; ++i)
            {
                if (dict.ContainsKey(s[i]))
                    res += i < s.Length / 2 ? 1 : -1;
            }
            return res == 0;
        }
        /// 1706. Where Will the Ball Fall
        ///You have a 2-D grid of size m x n representing a box, and you have n balls.
        ///The box is open on the top and bottom sides.
        ///A board = 1, that redirects the ball to the right spans the top-left to the bottom-right
        ///A board =-1, that redirects the ball to the left spans the top-right to the bottom-left
        ///Return an array answer of size n where answer[i] is the column that the ball falls out of
        ///at the bottom after dropping the ball from the ith column at the top, or -1 if the ball gets stuck in the box.
        public int[] FindBall(int[][] grid)
        {
            var rowLen = grid.Length;
            var colLen = grid[0].Length;
            var ans = new int[colLen];
            for (int i = 0; i < colLen; i++)
            {
                int col = i;
                int row = 0;
                while (row < rowLen)
                {
                    int adjacentCol = col + grid[row][col];
                    if (adjacentCol < 0 || adjacentCol >= colLen)
                    {
                        col = -1;
                        break;
                    }
                    else if (adjacentCol + grid[row][adjacentCol] == col)
                    {
                        col = -1;
                        break;
                    }
                    else
                    {
                        col = adjacentCol;
                        row++;
                    }
                }
                ans[i] = col;
            }
            return ans;
        }

        ///1710. Maximum Units on a Truck
        public int MaximumUnits(int[][] boxTypes, int truckSize)
        {
            int res = 0;
            boxTypes = boxTypes.OrderBy(x => -x[1]).ToArray();
            foreach (var i in boxTypes)
            {
                int count = Math.Min(truckSize, i[0]);
                res += count * i[1];
                truckSize -= count;
                if (truckSize <= 0) break;
            }
            return res;
        }

        ///1712. Ways to Split Array Into Three Subarrays, #Binary Search
        //split into three non-empty contiguous subarrays - named left, mid, right, sum of left<=mid<=right,
        //return the number of good ways to split nums, modulo 10^9 + 7.
        //3 <= nums.length <= 10^5,0 <= nums[i] <= 10^4
        public int WaysToSplit(int[] nums)
        {
            int n = nums.Length;
            int[] arr = new int[n];//create prefixSum array
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                sum += nums[i];
                arr[i] = sum;
            }
            long res = 0;
            long mod = 10_0000_0007;
            //find index i and j , than sum of [0,i]<= (i,j]<=(j,n-1]
            //equal to prefix array : arr[i]<=arr[j]-arr[i]<=arr[n-1]-arr[j]
            for (int i = 0; i < n - 2; i++)
            {
                //skip three invalid condition
                if (arr[i] > sum / 3)
                    break;
                if (arr[i + 1] - arr[i] > sum - arr[i + 1])
                    break;
                if (arr[i] > arr[n - 2] - arr[i])
                    break;

                int left1 = i + 1;
                int right1 = n - 2;
                while (left1 < right1)
                {
                    int mid1 = (left1 + right1) / 2;
                    if (arr[mid1] - arr[i] >= arr[i])
                    {
                        right1 = mid1;
                    }
                    else
                    {
                        left1 = mid1 + 1;
                    }
                }
                //search valid index left1, that arr[left1]-arr[i]>=arr[i]
                if (arr[left1] - arr[i] < arr[i]) continue;

                int left2 = i + 1;
                int right2 = n - 2;
                while (left2 < right2)
                {
                    int mid2 = (left2 + right2 + 1) / 2;//select the right side center
                    if (sum - arr[mid2] >= arr[mid2] - arr[i])
                    {
                        left2 = mid2;
                    }
                    else
                    {
                        right2 = mid2 - 1;
                    }
                }
                //valid index left2 meet the condition: sum-arr[left2] < arr[left2] - arr[i]
                if (sum - arr[left2] < arr[left2] - arr[i]) continue;

                res += left2 - left1 + 1;// add range count [left1,left2]
                res %= mod;
            }
            return (int)(res % mod);
        }

        ///1716. Calculate Money in Leetcode Bank
        public int TotalMoney(int n)
        {
            int weeks = n / 7;
            int days = n % 7;
            return days * (1 + days) / 2 + days * weeks + 7 * (1 + 7) / 2 * weeks + 7 * weeks * (weeks - 1) / 2;
        }

        ///1720. Decode XORed Array
        //encoded[i]= res[i]^res[i+1]
        public int[] Decode(int[] encoded, int first)
        {
            int n = encoded.Length;
            int[] res = new int[n + 1];
            res[0] = first;
            for(int i=1; i < res.Length; i++)
            {
                res[i] = encoded[i - 1] ^ res[i-1];
            }
            return res;
        }
        /// 1721. Swapping Nodes in a Linked List
        //You are given the head of a linked list, and an integer k.
        //Return the head of the linked list after swapping the values of the kth node
        //from the beginning and the kth node from the end(the list is 1-indexed).
        public ListNode SwapNodes(ListNode head, int k)
        {
            List<ListNode> list = new List<ListNode>();
            var node = head;
            while (node != null)
            {
                list.Add(node);
                node = node.next;
            }
            if (k > list.Count / 2)
                k = list.Count + 1 - k;
            if (list.Count % 2 == 1 && k == list.Count / 2 + 1)
                return head;

            var temp = list[k - 1];
            list[k - 1] = list[list.Count - k];
            list[list.Count - k] = temp;
            if (k == 1 || k == list.Count)
            {
                list[0].next = list[1];

                list[list.Count - 2].next = list[list.Count - 1];
                list[list.Count - 1].next = null;
            }
            else
            {
                list[k - 2].next = list[k - 1];
                list[k - 1].next = list[k];

                list[list.Count - k - 1].next = list[list.Count - k];
                list[list.Count - k].next = list[list.Count - k + 1];
            }
            return list[0];
        }

        ///1725. Number Of Rectangles That Can Form The Largest Square
        public int CountGoodRectangles(int[][] rectangles)
        {
            int max = 0;
            int res = 0;
            foreach(var rect in rectangles)
            {
                var len = Math.Min(rect[0], rect[1]);
                if (len > max) res = 1;
                else if (len == max) res++;
                max=Math.Max(max, len);
            }
            return res;
        }

        ///1726. Tuple with Same Product
        //nums[i] distinct, i!=j!=k!=l, nums[i]*nums[j]==nums[k]*nums[l], each count 8;
        public int TupleSameProduct(int[] nums)
        {
            int res = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    var product = nums[i] * nums[j];
                    if (dict.ContainsKey(product)) res += 8 * dict[product]++;
                    else dict.Add(product, 1);
                }
            }
            return res;
        }

        ///1732. Find the Highest Altitude
        public int LargestAltitude(int[] gain)
        {
            int res = 0;
            int curr = 0;
            foreach (var g in gain)
            {
                curr += g;
                res = Math.Max(res, curr);
            }
            return res;
        }

        ///1736. Latest Time by Replacing Hidden Digits
        public string MaximumTime(string time)
        {
            //The valid times are those inclusively between 00:00 and 23:59.
            for(int i = 0; i < 5; i++)
            {
                if(time[i] == '?')
                {
                    if (i == 0)
                    {
                        if (time[1] == '?')
                        {
                            time = "23" + time.Substring(2);
                        }
                        else
                        {
                            if (time[1] > '3')
                            {
                                time = "1" + time.Substring(1);
                            }
                            else
                            {
                                time = "2"+ time.Substring(1);
                            }
                        }
                    }
                    else if (i == 1)
                    {
                        if (time[0] == '2')
                        {
                            time = time[0] + "3" + time.Substring(2);
                        }
                        else
                        {
                            time = time[0] + "9" + time.Substring(2);
                        }
                    }
                    else if (i == 3)
                    {
                        time = time.Substring(0, 3) + "5" + time[4];
                    }
                    else if (i == 4)
                    {
                        time = time.Substring(0, 4) + "9";
                    }
                }
            }
            return time;
        }
        /// 1742. Maximum Number of Balls in a Box
        ///For example, the ball number 321 will be put in the box number 3 + 2 + 1 = 6 and
        ///the ball number 10 will be put in the box number 1 + 0 = 1.
        ///Given two integers lowLimit and highLimit, return the number of balls in the box with the most balls.
        public int CountBalls(int lowLimit, int highLimit)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int ans = 0;
            for (int i = lowLimit; i <= highLimit; i++)
            {
                var boxIndex = CountBalls_GetBoxIndex(i);
                if (dict.ContainsKey(boxIndex))
                    dict[boxIndex]++;
                else
                    dict.Add(boxIndex, 1);
                ans = Math.Max(ans, dict[boxIndex]);
            }
            return ans;
        }

        public int CountBalls_GetBoxIndex(int ball)
        {
            int ans = 0;
            while (ball > 0)
            {
                ans += ball % 10;
                ball /= 10;
            }
            return ans;
        }

        ///1743. Restore the Array From Adjacent Pairs
        public int[] RestoreArray(int[][] adjacentPairs)
        {
            int n = adjacentPairs.Length + 1;
            int[] res = new int[n];
            var dict = new Dictionary<int, List<int>>();
            foreach(var adj in adjacentPairs)
            {
                if (!dict.ContainsKey(adj[0])) dict.Add(adj[0], new List<int>());
                if (!dict.ContainsKey(adj[1])) dict.Add(adj[1], new List<int>());
                dict[adj[0]].Add(adj[1]);
                dict[adj[1]].Add(adj[0]);
            }

            var prev = dict.Keys.Where(x => dict[x].Count == 1).First();
            var curr = dict[prev].First();
            res[0] = prev;
            for(int i=1; i < n; i++)
            {
                res[i] = curr;
                if (dict[curr].First() == prev)
                {
                    prev = curr;
                    curr = dict[curr].Last();
                }
                else
                {
                    prev = curr;
                    curr = dict[curr].First();
                }
            }
            return res;
        }

        ///1745. Palindrome Partitioning IV, #DP
        ///return true if split the string s into three non-empty palindromic substrings.

        public bool CheckPartitioning(string s)
        {
            int n = s.Length;
            bool[,] dp = new bool[n, n];
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i >= j)
                        dp[i, j] = true;
                    else if (s[i] == s[j])
                        dp[i, j] = dp[i + 1, j - 1];
                }
            }
            for (int i = 1; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (dp[0, i - 1] && dp[i, j - 1] && dp[j, n - 1])
                        return true;
            return false;
        }

        public bool CheckPartitioning_MyOn3(string s)
        {
            bool ans = false;
            for (int i = 0; i < s.Length - 2; i++)
            {
                if (CheckPartitioning(s, 0, i))
                {
                    for (int j = i + 1; j < s.Length - 1; j++)
                    {
                        if (CheckPartitioning(s, i + 1, j) && CheckPartitioning(s, j + 1, s.Length - 1))
                        {
                            return true;
                        }
                    }
                }
            }
            return ans;
        }

        public bool CheckPartitioning(string s, int start, int end)
        {
            while (start < end)
            {
                if (s[start++] != s[end--]) return false;
            }
            return true;
        }

        ///1748. Sum of Unique Elements
        public int SumOfUnique(int[] nums)
        {
            var dict = new Dictionary<int, int>();
            int sum = 0;
            foreach (var n in nums)
            {
                if (dict.ContainsKey(n))
                {
                    if (dict[n] == 1) sum -= n;
                    dict[n]++;
                }
                else
                {
                    sum += n;
                    dict.Add(n, 1);
                }
            }
            return sum;
        }


        ///1749. Maximum Absolute Sum of Any Subarray, #Kadane
        public int MaxAbsoluteSum(int[] nums)
        {
            return Math.Max(Math.Abs(MaxSubArray(nums)), Math.Abs(MinSubArray(nums)));
        }

        public int MinSubArray(int[] nums)
        {
            int sum = 0;
            int min = int.MaxValue; //if all positive num ,return nums.Min()
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                min = Math.Min(min, sum);
                if (sum >= 0) sum = 0;
            }
            return min;
        }
    }
}