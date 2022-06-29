using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///300. Longest Increasing Subsequence, #DP, #Binary Search, #Patience Sort, #LIS
        /// Patience Sort, https://en.wikipedia.org/wiki/Longest_increasing_subsequence
        ///return the longest length ,by deleting some or no elements without changing the order.
        ///eg. [0,3,1,6,4,4,7].=>[0,1,4,7] or [0,3,4,6] or [0,1,6,7], O(n log(n))
        public int LengthOfLIS(int[] nums)
        {
            var ans =new List<int>() { nums[0]};
            for(int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > ans.Last())
                {
                    ans.Add(nums[i]);
                }
                else
                {
                    //a litter same like =>Q 334. Increasing Triplet Subsequence
                    //replace the correct index, still get the correct ans
                    LengthOfLIS_BinaryReplace(ans, nums[i]);
                }
            }
            return ans.Count;
        }

        private void LengthOfLIS_BinaryReplace(IList<int> list, int num)
        {
            int left = 0;
            int right = list.Count - 1;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if(list[mid] < num)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            list[right] = num;
        }
        ///301. Remove Invalid Parentheses, #Backtracking
        ///s contain letter and '(' and ')', remove the minimum number of invalid parentheses.
        ///Return all the possible results. You may return the answer in any order.
        public IList<string> RemoveInvalidParentheses(string s)
        {
            List<string> ans = new List<string>();
            //trim all invalid ')' from head and '(' from tail
            s = RemoveInvalidParentheses_TrimHeadAndTail(s);
            //if no need to remove any char
            if (RemoveInvalidParentheses_IsValid(s))
            {
                ans.Add(s);
                return ans;
            }

            int removeLeftCount = 0;
            int removeRightCount = 0;
            List<int> leftIndexes = new List<int>();
            List<int> rightIndexes = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    leftIndexes.Add(i);
                    removeLeftCount++;
                }
                else if (s[i] == ')')
                {
                    rightIndexes.Add(i);
                    if (removeLeftCount == 0) { removeRightCount++; }
                    else { removeLeftCount--; }
                }
            }

            //using backtracking to get all combines that remove n of '(' or ')'
            List<List<int>> combines=new List<List<int>>();
            if (removeRightCount == 0)
            {
                RemoveInvalidParentheses_Backtracking(leftIndexes, new List<int>(), removeLeftCount, 0, combines);
            }
            else if (removeLeftCount == 0)
            {
                RemoveInvalidParentheses_Backtracking(rightIndexes, new List<int>(), removeRightCount, 0, combines);
            }
            else
            {
                List<List<int>> combinesLeft = new List<List<int>>();
                RemoveInvalidParentheses_Backtracking(leftIndexes, new List<int>(), removeLeftCount, 0, combinesLeft);
                List<List<int>> combinesRight = new List<List<int>>();
                RemoveInvalidParentheses_Backtracking(rightIndexes, new List<int>(), removeRightCount, 0, combinesRight);
                combines = RemoveInvalidParentheses_GetCombines(combinesLeft, combinesRight);
            }
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (var combine in combines)
            {
                List<char> list1 = new List<char>();
                for (int i = 0; i < s.Length; i++)
                    if (!combine.Contains(i)) { list1.Add(s[i]); }

                var str = new string(list1.ToArray());
                if (!dict.ContainsKey(str) && RemoveInvalidParentheses_IsValid(str))
                    dict.Add(str, 1);
            }
            return dict.Keys.ToList();
        }
        public List<List<int>> RemoveInvalidParentheses_GetCombines(List<List<int>> leftCombines, List<List<int>> rightCombines)
        {
            var ans=new List<List<int>>();
            foreach(var left in leftCombines)
            {
                foreach(var right in rightCombines)
                {
                    var list=new List<int>(left);
                    list.AddRange(right);
                    ans.Add(list);
                }
            }
            return ans;
        }
        public string RemoveInvalidParentheses_TrimHeadAndTail(string s)
        {
            Dictionary<int, int> invalidMap = new Dictionary<int, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ')') invalidMap.Add(i, 1);
                else if (s[i] == '(') break;
            }
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == '(') invalidMap.Add(i, 1);
                else if (s[i] == ')') break;
            }
            List<char> valids = new List<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!invalidMap.ContainsKey(i)) valids.Add(s[i]);
            }
            return new string(valids.ToArray());
        }
        public bool RemoveInvalidParentheses_IsValid(string s)
        {
            if(s.Length == 0) return true;
            int count = 0;
            foreach(var c in s)
            {
                if (c == '(') { count++; }
                else if(c == ')')
                {
                    if (count == 0) return false;
                    count--;
                }
            }
            return count == 0;
        }
        public void RemoveInvalidParentheses_Backtracking(List<int> indexes,List<int> list, int count,int start, List<List<int>> combines)
        {
            if (indexes.Count-start < count)
            {
                return;
            }
            else
            {
                for (int i = start; i < indexes.Count-count+1; i++)
                {
                    var nextList = new List<int>(list) { indexes[i] };
                    if (count == 1) { combines.Add(nextList); }
                    else { RemoveInvalidParentheses_Backtracking(indexes, nextList, count - 1, i + 1, combines); }
                }
            }
        }

        ///303. Range Sum Query - Immutable, see NumArray_303

        /// 304. Range Sum Query 2D - Immutable, see NumMatrix

        ///306. Additive Number, #Backtracking
        ///A valid additive sequence should contain at least three numbers.
        ///Except for the first two numbers, each subsequent number must be the sum of the preceding two.
        public bool IsAdditiveNumber(string num)
        {
            for(int i = 1; i < num.Length-1; i++)
            {
                for(int j = i+1; j < num.Length; j++)
                {
                    var prev = num.Substring(0, i);
                    var curr = num.Substring(i, j-i);
                    if (IsAdditiveNumber(num.Substring(j), prev, curr)) return true;
                }
            }
            return false;
        }
        private bool IsAdditiveNumber(string num, string prev, string curr)
        {
            if (prev.Length > 1 && prev[0] == '0') return false;
            if (curr.Length > 1 && curr[0] == '0') return false;
            if (num.Length > 1 && num[0] == '0') return false;
            if (num.Length < prev.Length || num.Length < curr.Length) return false;

            var add = IsAdditiveNumber_Add(prev, curr);
            if (num == add) return true;
            else
            {
                if (num.StartsWith(add))
                {
                    return IsAdditiveNumber(num.Substring(add.Length), curr, add);
                }
                else return false;
            }
        }
        private string IsAdditiveNumber_Add(string s1,string s2)
        {
            List<char> list = new List<char>();
            int carry = 0;
            int i = 0;
            while(i < s1.Length || i < s2.Length)
            {
                int c1 = i < s1.Length ? s1[s1.Length - 1 - i] - '0' : 0;
                int c2 = i < s2.Length ? s2[s2.Length - 1 - i] - '0' : 0;
                int curr = c1 + c2 + carry;
                list.Insert(0, (char)(curr % 10 + '0'));
                carry = curr / 10;
                i++;
            }
            if(carry>0)
                list.Insert(0, (char)(carry % 10 + '0'));
            return new string(list.ToArray());
        }

        /// 307. Range Sum Query - Mutable, see NumArray

        /// 309. Best Time to Buy and Sell Stock with Cooldown, #DP
        ///After you sell your stock, you cannot buy stock on the next day (i.e., cooldown one day).
        ///0 <= prices[i] <= 1000, 1<=length<=5000
        public int MaxProfit_309(int[] prices)
        {
            int len = prices.Length;
            if (len <=1)
                return 0;

            //buy[i]: Max profit till index i. The series of transaction is ending with a buy.
            //sell[i]: Max profit till index i.The series of transaction is ending with a sell.
            int[] sell = new int[len];
            int[] buy = new int[len];
            //buy[i] = Math.Max(buy[i - 1], sell[i - 2] - prices[i]);
            //sell[i] = Math.Max(sell[i - 1], buy[i - 1] + prices[i]);

            buy[0] = -prices[0];
            buy[1] = Math.Max(buy[0], -prices[1]);
            sell[0] = 0;
            sell[1] = Math.Max(sell[0], buy[0] + prices[1]);

            for (int i = 2; i < len; i++)
            {
                buy[i] = Math.Max(buy[i - 1], sell[i - 2] - prices[i]);
                sell[i] = Math.Max(sell[i - 1], buy[i - 1] + prices[i]);
            }
            return sell[len - 1];
        }

        ///310. Minimum Height Trees, #Graph, #BFS
        public IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            if (n == 1) return new List<int>() { 0 };

            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; ++i)
                graph[i]=new List<int>();

            foreach (var edge in edges)
            {
                graph[edge[0]].Add(edge[1]);
                graph[edge[1]].Add(edge[0]);
            }

            List<int> leaves = new List<int>();
            for (int i = 0; i < n; i++)
                if (graph[i].Count == 1) leaves.Add(i);

            //eat leaves every loop, finally we got the center 1 or 2 nodes
            while (n > 2)
            {
                n -= leaves.Count;
                List<int> nextLeaves = new List<int>();
                foreach (int i in leaves)
                {
                    int j = graph[i][0];
                    graph[j].Remove(i);
                    if (graph[j].Count == 1) nextLeaves.Add(j);
                }
                leaves = nextLeaves;
            }
            return leaves;
        }


        /// 312. Burst Balloons, #DP, #Divide And Conquer
        ///You are given n balloons, indexed from 0 to n - 1. Each balloon is painted with a number
        ///on it represented by an array nums. You are asked to burst all the balloons.
        ///If you burst the ith balloon, you will get nums[i - 1] * nums[i] * nums[i + 1] coins.
        ///If i - 1 or i + 1 goes out of bounds of the array, then treat it as if there is a balloon with a 1 painted on it.
        ///Return the maximum coins you can collect by bursting the balloons wisely.
        public int MaxCoins_312(int[] nums)
        {
            int n = nums.Length;

            int[,] dp = new int[n,n];
            // build the dp table for length from 1 to n
            for (int k = 1; k <= n; k++)
                // check every subarray of lengh k
                for (int left = 0; left < n - k + 1; left++)
                {
                    int right = left + k - 1;
                    int max = 0;
                    // every element of the subarray could be the last balloon to burst
                    for (int i = left; i <= right; i++)
                    {
                        int leftNum = (left <= 0) ? 1 : nums[left - 1];
                        int rightNum = (right >= n - 1) ? 1 : nums[right + 1];

                        int leftSum = (i == left) ? 0 : dp[left,i - 1];
                        int rightSum = (i == right) ? 0 : dp[i + 1,right];
                        max = Math.Max(max, leftNum * nums[i] * rightNum + leftSum + rightSum);
                    }
                    dp[left,right] = max;
                }
            return dp[0,n - 1];
        }

        public int MaxCoins_Divide_Conquer(int[] nums)
        {
            int n = nums.Length+2;
            int[] arr = new int[n];
            for (int i = 1; i < n - 1; i++)
                arr[i] = nums[i - 1];
            arr[0] = 1;
            arr[n - 1] = 1;

            int[,] memo = new int[n, n];
            return MaxCoins_burst(memo, arr, 0, n-1);
        }

        public int MaxCoins_burst(int[,] memo, int[] nums, int left, int right)
        {
            if (left + 1 == right) return 0;
            if (memo[left,right] > 0) return memo[left,right];
            int ans = 0;
            for (int i = left + 1; i < right; ++i)
                ans = Math.Max(ans, nums[left] * nums[i] * nums[right]
                + MaxCoins_burst(memo, nums, left, i) + MaxCoins_burst(memo, nums, i, right));
            memo[left,right] = ans;
            return ans;
        }

        /// 315. Count of Smaller Numbers After Self - #Merge Sort
        ///You are given an integer array nums and you have to return a new counts array.
        ///The counts array has the property where counts[i] is the number of smaller elements to the right of nums[i].
        ///-10^4 <= nums[i] <= 10^4
        public IList<int> CountSmaller(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];
            int[] index = new int[n];
            for (int i = 0; i < n; i++)
            {
                index[i] = i;
            }
            CountSmaller_MergeSort(nums, index, 0, nums.Length - 1, res);
            return res.ToList();
        }
        private void CountSmaller_MergeSort(int[] nums, int[] index, int left, int right, int[] res)
        {
            if (left >= right)
            {
                return;
            }
            int mid = (left + right) / 2;
            CountSmaller_MergeSort(nums, index, left, mid, res);
            CountSmaller_MergeSort(nums, index, mid + 1, right, res);
            CountSmaller_Merge(nums, index, left, mid, mid + 1, right, res);
        }

        private void CountSmaller_Merge(int[] nums, int[] index, int l1, int r1, int l2, int r2, int[] res)
        {
            int start = l1;
            int[] tmp = new int[r2 - l1 + 1];
            int count = 0;
            int p = 0;
            while (l1 <= r1 || l2 <= r2)
            {
                if (l1 > r1)
                {
                    tmp[p++] = index[l2++];
                }
                else if (l2 > r2)
                {
                    res[index[l1]] += count;
                    tmp[p++] = index[l1++];
                }
                else if (nums[index[l1]] > nums[index[l2]])
                {
                    tmp[p++] = index[l2++];
                    count++;
                }
                else
                {
                    res[index[l1]] += count;
                    tmp[p++] = index[l1++];
                }
            }
            for (int i = 0; i < tmp.Length; i++)
            {
                index[start + i] = tmp[i];
            }
        }

        public IList<int> CountSmaller_My(int[] nums)
        {
            int ship = 10000;
            var arr = new int[2 * ship+1];
            int left = 2*ship;
            int right = 0;
            foreach (var n in nums)
            {
                arr[n + ship]++;
                left=Math.Min(left, n + ship);
                right=Math.Max(right, n + ship);
            }
            var ans = new List<int>();
            for (int i=0; i < nums.Length; i++)
            {
                int count = 0;
                int j = nums[i] + ship - 1;
                while (j >= left)
                {
                    count += arr[j];
                    j--;
                }
                ans.Add(count);
                arr[nums[i] + ship]--;
            }
            return ans;
        }

        ///316. Remove Duplicate Letters, #Greedy
        ///Given a string s, remove duplicate letters so that every letter appears once and only once.
        ///You must make sure your result is the smallest in lexicographical order among all possible results.
        public string RemoveDuplicateLetters(string s)
        {
            int[] arr = new int[26];
            int pos = 0; // the position for the smallest s[i]
            for (int i = 0; i < s.Length; i++)
                arr[s[i] - 'a']++;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i]< s[pos]) pos = i;
                if (--arr[s[i] - 'a'] == 0) break;
            }
            return s.Length == 0 ? "" : s[pos].ToString() + RemoveDuplicateLetters(s.Substring(pos + 1).Replace(s[pos].ToString(), ""));
        }


        /// 318. Maximum Product of Word Lengths
        ///return the max of length(word[i]) * length(word[j]) where the two words do not share common letters.
        public int MaxProduct(string[] words)
        {
            int[] arr=new int[words.Length];
            for(int i=0; i<arr.Length; i++)
            {
                int bit = 0;
                foreach(var c in words[i])
                {
                    bit|= 1<<( c - 'a');
                }
                arr[i] = bit;
            }

            int max = 0;
            for(int i=0;i<words.Length-1; i++)
            {
                for(int j = i + 1; j < words.Length; j++)
                {
                    if ((arr[i] & arr[j]) == 0)
                        max = Math.Max(max, words[i].Length * words[j].Length);
                }
            }
            return max;
        }

        /// 319. Bulb Switcher
        public int BulbSwitch(int n)
        {
            int ans = 0;
            for(int i=1; i*i <= n; i++)
                ans++;
            return ans;
        }
        /// 322. Coin Change, #DP
        ///array coins representing coins of different denominations and an integer amount representing a total amount of money.
        ///Return the fewest number of coins that you need to make up that amount. Or return -1. can reuse each kind of coin.
        ///1 <= coins[i] <= 2^31 - 1, 0 <= amount <= 10000
        public int CoinChange(int[] coins, int amount)
        {
            if (amount == 0)
                return 0;
            int[] dp = new int[amount + 1];
            for (int i = 0; i < dp.Length; i++)
                dp[i] = 10001;
            Array.Sort(coins);
            dp[amount] = 0;
            for (int i = amount; i>=0; i--)
            {
                for (int j=coins.Length-1;j>=0;j--)
                {
                    if (i - coins[j] < 0)
                        continue;
                    dp[i - coins[j]] = Math.Min(dp[i - coins[j]], dp[i] + 1);
                }
            }
            return dp[0] == 10001 ? -1 : dp[0];
        }

        ///324. Wiggle Sort II
        ///Given an integer array nums, reorder it such that nums[0] < nums[1] > nums[2] < nums[3]....
        ///1 <= nums.length <= 5 * 104, 0 <= nums[i] <= 5000, O(n) time and/or in-place with O(1) extra space?
        public void WiggleSort(int[] nums)
        {
            int arrlen = 5000;
            int[] arr=new int[arrlen + 1];
            int left = arrlen;
            int right = 0;
            foreach (var n in nums)
            {
                arr[n]++;
                left = Math.Min(left, n);
                right = Math.Max(right, n);
            }
            int half = (nums.Length + 1) / 2;
            int[] wiggleArr1 = new int[arrlen + 1];
            int count1 = 0;
            int mid = 0;
            for(int i=left;i<=right&&count1<half;i++)
            {
                if (arr[i] > 0)
                {
                    if (count1 + arr[i] < half)
                    {
                        wiggleArr1[i] = arr[i];
                        count1 += arr[i];
                        arr[i] = 0;
                    }
                    else
                    {
                        wiggleArr1[i] = half - count1;
                        arr[i] -= half - count1;
                        count1 = half;
                        mid = i;
                        break;
                    }
                }
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (i % 2 == 0)
                {
                    nums[i] = WiggleSort_Get(wiggleArr1, ref mid);
                }
                else
                {
                    nums[i] = WiggleSort_Get(arr, ref right);
                }
            }
        }

        public int WiggleSort_Get(int[] arr,ref int end)
        {
            for(int i= end; i>=0; i--)
            {
                if(arr[i] > 0)
                {
                    arr[i]--;
                    if (arr[i] == 0) end--;
                    return i;
                }
            }
            return -1;
        }

        /// 326. Power of Three
        ///Given an integer n, return true if it is a power of three. Otherwise, return false.
        ///An integer n is a power of three, if there exists an integer x such that n == 3x.
        public bool IsPowerOfThree(int n)
        {
            while (n >= 3)
            {
                if (n % 3 != 0)
                    return false;
                n /= 3;
            }
            return n == 1;
        }

        ///328. Odd Even Linked List
        //group all the nodes with odd indices together followed by the nodes with even indices, then return.
        //The first node is considered odd, and the second node is even, and so on.
        //head = [1,2,3,4,5] -> Output: [1,3,5,2,4], 0<=n<=10^4
        public ListNode OddEvenList(ListNode head)
        {
            if (head == null || head.next == null || head.next.next == null)
                return head;

            ListNode oddsHead = head;
            ListNode evensHead = head.next;
            ListNode currOdd = head;
            ListNode currEven = head.next;
            bool isOdd = true;
            var node = head.next.next;
            while(node != null)
            {
                if (isOdd)
                {
                    currOdd.next = node;
                    currOdd = node;
                }
                else
                {
                    currEven.next = node;
                    currEven = node;
                }
                isOdd = !isOdd;
                node = node.next;
            }
            currOdd.next = evensHead;
            currEven.next = null;
            return oddsHead;
        }

        ///329. Longest Increasing Path in a Matrix, #BFS
        ///Given an m x n integers matrix, return the length of the longest increasing path in matrix.
        ///From each cell, you can either move in four directions: left, right, up, or down.
        public int LongestIncreasingPath(int[][] matrix)
        {
            int ans = 1;

            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;
            int[][] dp = new int[rowLen][];
            for (int i = 0; i < rowLen; i++)
                dp[i] = new int[colLen];

            int[][] dxy = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
            int len = 1;
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    int neighboor = 0;
                    foreach (var d in dxy)
                    {
                        int r = i + d[0];
                        int c = j + d[1];
                        if (r >= 0 && r < rowLen && c >= 0 && c < colLen)
                        {
                            if (matrix[r][c] < matrix[i][j])
                            {
                                neighboor++;
                                break;
                            }
                        }
                    }

                    if (neighboor == 0)
                    {
                        dp[i][j] = len;
                        list.Add(new int[] { i,j});
                    }
                }
            }

            while(list.Count > 0)
            {
                len++;
                List<int[]> next = new List<int[]>();
                Dictionary<int, int> dict = new Dictionary<int, int>();
                foreach(var cell in list)
                {
                    foreach (var d in dxy)
                    {
                        int r = cell[0] + d[0];
                        int c = cell[1] + d[1];
                        if (r >= 0 && r < rowLen && c >= 0 && c < colLen)
                        {
                            if (matrix[r][c] > matrix[cell[0]][cell[1]])
                            {
                                dp[r][c] = len;
                                ans = len;
                                if(!dict.ContainsKey(r * 1000 + c))
                                {
                                    next.Add(new int[] { r, c });
                                    dict.Add(r * 1000 + c, 1);
                                }
                            }
                        }
                    }
                }
                list = next;
            }
            return ans;
        }
        /// 334. Increasing Triplet Subsequence, #Greedy
        ///using greedy to find i<j<k, nums[i]<nums[j]<nums[k]
        public bool IncreasingTriplet(int[] nums)
        {
            // start with two largest values, update them until find a num > both of them
            int small = int.MaxValue;
            int big = int.MaxValue;
            foreach (var n in nums)
            {
                if (n > big)
                {
                    //find a num > than the big
                    return true;
                }
                else
                {
                    if(n<= small)
                    {
                        //even when big has valid value(<int.Max), we still need update small for updating the big in future
                        //this will not change the big,  so if we found n>big in future, this algo still work
                        small = n;
                    }
                    else
                    {
                        //find n <= big && >small, then update the big
                        big = n;
                    }
                }
            }
            return false;
        }

        ///338. Counting Bits, #DP
        ///return an array ans of length n + 1 ,ans[i] is the number of 1's in the binary representation of i.
        /// O(n log n). Can you do it in linear time O(n) and possibly in a single pass?
        public int[] CountBits(int n)
        {
            var ans=new int[n+1];
            for(int i = 1; i <= n; i++)
            {
                int lastCount = ans[i - 1];
                var last = i - 1;
                int j = 1;
                while (j <= n)
                {
                    if ((last & j) == j)//1
                    {
                        lastCount--;
                        j <<= 1;
                    }
                    else
                    {
                        ans[i] = lastCount + 1;
                        break;
                    }
                }
            }
            return ans;
        }


        ///341. Flatten Nested List Iterator, see NestedIterator

        ///342. Power of Four
        ///Given an integer n, return true if it is a power of four. Otherwise, return false.
        ///An integer n is a power of four, if there exists an integer x such that n == 4x.
        public bool IsPowerOfFour_Math(int n)
        {
            return n>0 && (n & (n-1)) ==0 && (n & 0xAAAAAAAA)==0;
        }

        public bool IsPowerOfFour_My(int n)
        {
            if (n <= 0) return false;
            while (n > 0)
            {
                if (n == 1) return true;
                if ((n & 3) != 0) return false;
                n >>= 2;
            }
            return false;
        }
        /// 343. Integer Break, #DP
        ///find 3 as many as possible, but no 1; 2 <= n <= 58
        public int IntegerBreak(int n)
        {
            int[] dp = new int[n + 1];
            dp[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                for (int j = 1; j <= i / 2; j++)
                {
                    dp[i] = Math.Max(dp[i], Math.Max(j, dp[j]) * Math.Max(i - j, dp[i - j]));
                }
            }
            return dp[n];
        }
        public int IntegerBreak_Math(int n)
        {
            if (n <= 3)
                return n - 1;//1*(n-1)
            int ans = 1;
            while (n > 4)
            {
                ans *= 3;
                n -= 3;
            }
            return ans * n;
        }

        /// 344. Reverse String
        /// You must do this by modifying the input array in-place with O(1) extra memory.
        public void ReverseString(char[] s)
        {
            for (int i = 0; i < s.Length / 2; i++)
            {
                char temp = s[i];
                s[i] = s[s.Length - 1 - i];
                s[s.Length - 1 - i] = temp;
            }
        }

        ///345. Reverse Vowels of a String
        ///Given a string s, reverse only all the vowels in the string and return it.
        public string ReverseVowels(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>()
            {
                {'a',0 },{'e',0 },{'i',0 },{'o',0 },{'u',0 },
                {'A',0 },{'E',0 },{'I',0 },{'O',0 },{'U',0 },
            };
            List<int> list=new List<int>();
            for(int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i])) list.Add(i);
            }
            if (list.Count <=1) return s;
            var arr = s.ToCharArray();
            int left = 0;
            int right = list.Count-1;
            while (left < right)
            {
                var temp=arr[list[left]];
                arr[list[left++]] = arr[list[right]];
                arr[list[right--]] = temp;
            }
            return new string(arr);
        }

        /// 347. Top K Frequent Elements, #Bucket
        ///return the k most frequent elements. You may return the answer in any order.
        public int[] TopKFrequent_BucketSort(int[] nums, int k)
        {
            int n = nums.Length + 1;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            List<int>[] bucket = new List<int>[n];
            int[] res = new int[k];
            foreach (int num in nums)
            {
                if (!dict.ContainsKey(num)) dict.Add(num, 0);
                dict[num]++;
            }
            foreach (int key in dict.Keys)
            {
                int freq = dict[key];
                if (bucket[freq] == null)
                    bucket[freq] = new List<int>();
                bucket[freq].Add(key);
            }
            int j = 0;
            for (int pos = n- 1; pos > 0 && j<k; pos--)
            {
                if (bucket[pos] != null)
                {
                    for (int i = 0; i < bucket[pos].Count && j<k; i++)
                        res[j++]=bucket[pos][i];
                }
            }
            return res;
        }

        public int[] TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (dict.ContainsKey(n))
                    dict[n]++;
                else
                    dict.Add(n, 1);
            }
            return dict.OrderBy(x => -x.Value).Take(k).Select(x => x.Key).ToArray();
        }

        ///349. Intersection of Two Arrays
        ///return an array of their intersection. Each element in the result must be unique
        public int[] Intersection(int[] nums1, int[] nums2)
        {
            HashSet<int> res = new HashSet<int>();
            HashSet<int> set = new HashSet<int>();
            foreach (var n in nums1)
                set.Add(n);
            foreach(var n in nums2)
                if(set.Contains(n))res.Add(n);
            return res.ToArray();
        }
    }
}