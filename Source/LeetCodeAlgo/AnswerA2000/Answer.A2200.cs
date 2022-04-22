using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2200. Find All K-Distant Indices in an Array
        ///A k-distant index is an index i of nums for which there exists at least one index j
        ///such that |i - j| <= k and nums[j] == key. Return a list of all k-distant indices sorted in increasing order.
        public IList<int> FindKDistantIndices(int[] nums, int key, int k)
        {
            var set = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == key)
                {
                    for (int j = Math.Max(0, i - k); j <= i + k && j < nums.Length; j++)
                        set.Add(j);
                }
            }
            return set.ToList();
        }

        /// 2201. Count Artifacts That Can Be Extracted
        ///Given a 0-indexed 2D integer array dig where dig[i] = [ri, ci] indicates that
        ///you will excavate the cell (ri, ci), return the number of artifacts that you can extract.
        public int DigArtifacts(int n, int[][] artifacts, int[][] dig)
        {
            int res = 0;

            bool[,] matrix = new bool[n, n];
            foreach (var d in dig)
            {
                matrix[d[0], d[1]] = true;
            }
            foreach (var arti in artifacts)
            {
                bool canDig = true;
                for (int i = arti[0]; i <= arti[2]; i++)
                {
                    for (int j = arti[1]; j <= arti[3]; j++)
                    {
                        if (!matrix[i, j])
                        {
                            canDig = false;
                            break;
                        }
                    }
                }
                if (canDig) res++;
            }
            return res;
        }

        /// 2206. Divide Array Into Equal Pairs
        public bool DivideArray(int[] nums)
        {
            HashSet<int> set = new HashSet<int>();
            foreach (var n in nums)
            {
                if (set.Contains(n)) set.Remove(n);
                else set.Add(n);
            }
            return set.Count == 0;
        }

        ///2208. Minimum Operations to Halve Array Sum, #PriorityQueue, #Heap
        ///In one operation, you can choose any number from nums and reduce it to exactly half the number.
        ///(Note that you may choose this reduced number in future operations.)
        ///Return the minimum number of operations to reduce the sum of nums by at least half.
        public int HalveArray(int[] nums)
        {
            int res = 0;
            PriorityQueue<double, double> pq = new PriorityQueue<double, double>();
            double sum = 0;
            foreach(var n in nums)
            {
                pq.Enqueue(n, -n);
                sum += n;
            }
            double diff = 0;
            while (diff < sum / 2)
            {
                res++;
                var val =pq.Dequeue();
                diff += val / 2;
                pq.Enqueue(val / 2, -val / 2);
            }
            return res;
        }

        /// 2210. Count Hills and Valleys in an Array, #Two Pointers, #Greedy
        ///An index i is part of a hill in nums if the closest non-equal neighbors of i are smaller than nums[i].
        ///Similarly, an index i is part of a valley in nums if the closest non-equal neighbors of i are larger than nums[i].
        ///Adjacent indices i and j are part of the same hill or valley if nums[i] == nums[j].
        ///Note that for an index to be part of a hill or valley, it must have a non-equal neighbor on both the left and right sides.
        ///Return the number of hills and valleys in nums.
        public int CountHillValley(int[] nums)
        {
            int res = 0;
            int left = -1;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == nums[i + 1]) continue;
                int right = nums[i + 1];
                if (left != -1)
                {
                    if (nums[i] > left && nums[i] > right) res++;//hill
                    else if (nums[i] < left && nums[i] < right) res++;//valley
                }
                left = nums[i];
            }
            return res;
        }

        /// 2215. Find the Difference of Two Arrays
        ///Given two 0-indexed integer arrays nums1 and nums2, return a list answer of size 2 where:
        ///answer[0] is a list of all distinct integers in nums1 which are not present in nums2.
        ///answer[1] is a list of all distinct integers in nums2 which are not present in nums1

        public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
        {
            var res = new List<IList<int>>();
            HashSet<int> set1 = new HashSet<int>(nums1);
            HashSet<int> set2 = new HashSet<int>(nums2);

            res.Add(set1.Where(x => !set2.Contains(x)).ToList());
            res.Add(set2.Where(x => !set1.Contains(x)).ToList());
            return res;
        }

        ///2216. Minimum Deletions to Make Array Beautiful, #Two Pointers
        ///The array nums is beautiful if: nums.length is even. nums[i] != nums[i + 1] for all i % 2 == 0.
        ///When you delete an element,all the elements to the right of the deleted element will be shifted one unit
        ///Return the minimum number of elements to delete from nums to make it beautiful.
        public int MinDeletion(int[] nums)
        {
            int res = 0;
            int i = 0;
            int j = 1;
            while (j < nums.Length)
            {
                if (nums[i] == nums[j])
                {
                    //if left == right , delete right, and move right-index j to next
                    res++;
                    j++;
                }
                else
                {
                    //update left to next of current right, then update right to next of left
                    i = j + 1;
                    j = i + 1;
                }
            }
            if ((nums.Length - res) % 2 == 1) res++;//if odd count of elements still in nums, delete the last one
            return res;
        }

        /// 2217. Find Palindrome With Fixed Length
        ///Given an integer array queries and a positive integer intLength, return an array answer where answer[i]
        ///is either the queries[i]th smallest positive palindrome of length intLength or -1 if no such palindrome exists.
        ///A palindrome is a number that reads the same backwards and forwards.Palindromes cannot have leading zeros.
        public long[] KthPalindrome(int[] queries, int intLength)
        {
            long[] res = new long[queries.Length];
            //maxCount for each intLength from 1 to 15
            Dictionary<int, long> maxCountDict = new Dictionary<int, long>();
            for (int i = 1; i <= 15; i++)
            {
                long count = 9;
                int j = i - 1;
                while (j-- > 0)
                    count *= 10;
                maxCountDict.Add(i, count);
            }
            //how many palindrome pairs
            int pair = (intLength + 1) / 2;
            for (int i = 0; i < queries.Length; i++)
            {
                //out of range, assign -1
                if (queries[i] > maxCountDict[pair]) res[i] = -1;
                else
                {
                    //from left/right to center
                    long[] list = new long[intLength];
                    long curr = queries[i];
                    for (int j = 0; j < pair; j++)
                    {
                        //get current index by divide to next total count of 10^(pair - j - 1)
                        long count = (long)Math.Pow(10, pair - j - 1);
                        long k = curr / count;
                        if (curr % count == 0) k--;//if mod is 0, need k--
                        curr -= k * count;
                        list[j] = k;
                        list[intLength - 1 - j] = k;//work for both odd and even intLength
                    }

                    //remove leading zero
                    list[0] = list[0] + 1;
                    list[intLength - 1] = list[0];//this will work for intLength=1, avoid duplicate +1
                    res[i] = long.Parse(String.Join("", list));
                }
            }
            return res;
        }

        ///2218. Maximum Value of K Coins From Piles, #DP, #Memo
        ///There are n piles of coins on a table. Each pile consists of a positive number of coins of assorted denominations.
        ///In one move, you can choose any coin on top of any pile, remove it, and add it to your wallet.
        ///Given a list piles, where piles[i] is a list of integers denoting the composition of the ith pile from top to bottom,
        ///and a positive integer k, return the maximum total value of coins you can have in your wallet
        ///if you choose exactly k coins optimally.
        public int MaxValueOfCoins(IList<IList<int>> piles, int k)
        {
            //dp[i,k] means picking k elements from pile[i] to pile[n-1].
            //We can pick 0,1,2,3... elements from the current pile[i] one by one.
            //It asks for the maximum total value of coins we can have,
            //so we need to return max of all the options.
            //Complexity:Time O(nm),Space O(nk)
            int[,] memo = new int[piles.Count + 1, k + 1];
            return MaxValueOfCoins_DP(piles, memo, 0, k);
        }

        private int MaxValueOfCoins_DP(IList<IList<int>> piles, int[,] memo, int i, int k)
        {
            if (k == 0 || i == piles.Count) return 0;
            if (memo[i, k] != 0) return memo[i, k];

            int res = MaxValueOfCoins_DP(piles, memo, i + 1, k);
            int sum = 0;

            for (int j = 0; j < Math.Min(piles[i].Count, k); j++)
            {
                sum += piles[i][j];
                res = Math.Max(res, sum + MaxValueOfCoins_DP(piles, memo, i + 1, k - j - 1));
            }
            memo[i, k] = res;
            return res;
        }

        ///2220. Minimum Bit Flips to Convert Number
        ///Given two integers start and goal, return the minimum number of bit flips to convert start to goal.
        public int MinBitFlips(int start, int goal)
        {
            int x = start ^ goal;
            int res = 0;
            while (x > 0)
            {
                if ((x & 1) == 1) res++;
                x >>= 1;
            }
            return res;
        }

        ///2221. Find Triangular Sum of an Array
        ///Return the triangular sum of nums.
        public int TriangularSum(int[] nums)
        {
            while (nums.Length > 1)
            {
                var arr = new int[nums.Length - 1];
                for (int i = 0; i < nums.Length - 1; i++)
                    arr[i] = (nums[i] + nums[i + 1]) % 10;
                nums = arr;
            }
            return nums[0];
        }

        ///2222. Number of Ways to Select Buildings
        ///Find all combine of 010 or 101
        public long NumberOfWays(string s)
        {
            long ans = 0;
            int len = s.Length;

            long totOnes = 0;
            for (int i = 0; i < len; i++)
                totOnes += s[i] - '0';

            long totZeros = len - totOnes;
            long currZeros = s[0] == '0' ? 1 : 0;
            long currOnes = s[0] == '1' ? 1 : 0;

            for (int i = 1; i < len; i++)
            {
                if (s[i] == '0')
                {
                    ans += (currOnes * (totOnes - currOnes));
                    currZeros++;
                }
                else
                {
                    ans += (currZeros * (totZeros - currZeros));
                    currOnes++;
                }
            }
            return ans;
        }

        ///2224. Minimum Number of Operations to Convert Time
        ///In one operation you can increase the time current by 1, 5, 15, or 60 minutes.
        ///You can perform this operation any number of times. Return the minimum number of operations to convert current to correct.
        public int ConvertTime(string current, string correct)
        {
            DateTime start = DateTime.ParseExact(current, "HH:mm", null);
            DateTime end = DateTime.ParseExact(correct, "HH:mm", null);
            int diff = (int)((end - start).TotalMinutes);
            int res = 0;
            while (diff > 0)
            {
                res++;
                if (diff >= 60) diff -= 60;
                else if (diff >= 15) diff -= 15;
                else if (diff >= 5) diff -= 5;
                else if (diff >= 1) diff -= 1;
            }
            return res;
        }

        ///2225. Find Players With Zero or One Losses
        ///You are given an integer array matches where matches[i] = [winneri, loseri] indicates that
        ///the player winneri defeated player loseri in a match. Return a list answer of size 2 where:
        ///answer[0] is a list of all players that have not lost any matches.
        ///answer[1] is a list of all players that have lost exactly one match.
        ///The values in the two lists should be returned in increasing order.
        public IList<IList<int>> FindWinners(int[][] matches)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var match in matches)
            {
                if (!dict.ContainsKey(match[0])) dict.Add(match[0], 0);
                if (!dict.ContainsKey(match[1])) dict.Add(match[1], 0);
                dict[match[1]]++;
            }

            var res = new List<IList<int>>();
            var list1 = dict.Keys.Where(x => dict[x] == 0).OrderBy(x => x).ToList();
            var list2 = dict.Keys.Where(x => dict[x] == 1).OrderBy(x => x).ToList();
            res.Add(list1);
            res.Add(list2);
            return res;
        }

        ///2226. Maximum Candies Allocated to K Children, #Binary Search
        ///Each candy can split to any parts, return the maximum number of candies each child can get.
        public int MaximumCandies(int[] candies, long k)
        {
            //left inited as 0, if invalid then return 0
            int left = 0, right = 10_000_000;
            ///Tip1.left < right Vs left <= right
            ///keep using left < right ,never bother thinking it anymore.
            while (left < right)
            {
                long sum = 0;
                ///Tip2.mid = (left + right + 1) / 2 Vs mid = (left + right) / 2
                ///mid = (left + right) / 2 to find first element valid
                ///mid = (left + right + 1) / 2 to find last element valid
                int mid = (left + right + 1) / 2;
                foreach (int candy in candies)
                {
                    sum += candy / mid;
                }
                if (k > sum)
                    right = mid - 1;
                else
                    left = mid;//make left always valid
            }
            return left;
        }

        ///2227. Encrypt and Decrypt Strings, see Encrypter

        ///2231. Largest Number After Digit Swaps by Parity
        ///You are given a positive integer num. You may swap any two digits of num that have the same parity
        ///(i.e. both odd digits or both even digits).
        ///Return the largest possible value of num after any number of swaps.
        public int LargestInteger(int num)
        {
            List<int> evens = new List<int>();
            List<int> odds = new List<int>();
            List<bool> parities = new List<bool>();

            while (num > 0)
            {
                int mod = num % 10;
                if (mod % 2 == 0)
                    evens.Add(mod);
                else
                    odds.Add(mod);
                parities.Insert(0, mod % 2 == 0);
                num /= 10;
            }
            evens.Sort();
            odds.Sort();
            int i = evens.Count - 1;
            int j = odds.Count - 1;
            int res = 0;
            foreach (var parity in parities)
            {
                res *= 10;
                if (parity)
                    res += evens[i--];
                else
                    res += odds[j--];
            }
            return res;
        }

        ///2232. Minimize Result by Adding Parentheses to Expression
        ///You are given a 0-indexed string expression of the form "<num1>+<num2>" where <num1> and <num2> represent positive integers.
        ///Insert "()" to get the minimium value, return the final expression string
        public string MinimizeResult(string expression)
        {
            var arr = expression.Split("+");
            int res = int.MaxValue;
            int x = 0;
            int y = 0;
            for (int i = 0; i < arr[0].Length; i++)
            {
                for (int j = 0; j < arr[1].Length; j++)
                {
                    int val1 = i == 0 ? 1 : int.Parse(arr[0].Substring(0, i));
                    int val2 = int.Parse(arr[0].Substring(i)) + int.Parse(arr[1].Substring(0, j + 1));
                    int val3 = j == arr[1].Length - 1 ? 1 : int.Parse(arr[1].Substring(j + 1));
                    if (val1 * val2 * val3 < res)
                    {
                        res = val1 * val2 * val3;
                        x = i;
                        y = j;
                    }
                }
            }
            return arr[0].Insert(x,"(")+"+"+arr[1].Insert(y+1,")");
        }

        ///2233. Maximum Product After K Increments, #PriorityQueue, #Heap
        ///You are given an array of non-negative integers nums and an integer k.
        ///In one operation, you may choose any element from nums and increment it by 1.
        ///Return the maximum product of nums after at most k operations.Since the answer may be very large, return it modulo 109 + 7
        public int MaximumProduct(int[] nums, int k)
        {
            if (nums.Length == 1) return nums[0] + k;
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            foreach (var n in nums)
                pq.Enqueue(n, n);

            while (k-- > 0)
            {
                var min = pq.Dequeue();
                min++;
                pq.Enqueue(min,min);
            }
            long res = 1;
            while (pq.Count > 0)
            {
                res*=pq.Dequeue();
                res %= 10_0000_0007;
            }
            return (int)res;
        }

        ///2235. Add Two Integers
        //public int Sum(int num1, int num2)
        //{
        //    return num1 + num2;
        //}

        ///2236. Root Equals Sum of Children
        //public bool CheckTree(TreeNode root)
        //{
        //    return root.val == root.left.val + root.right.val;
        //}


        ///2240. Number of Ways to Buy Pens and Pencils
        ///Return the number of distinct ways you can buy some number of pens and pencils.
        public long WaysToBuyPensPencils(int total, int cost1, int cost2)
        {
            long res = 0;
            //including 0 combines
            for (int i = 0; i <= total / cost1; i++)
            {
                var curr = total - i * cost1;
                res += curr / cost2 + 1;
            }
            return res;
        }
    }
}