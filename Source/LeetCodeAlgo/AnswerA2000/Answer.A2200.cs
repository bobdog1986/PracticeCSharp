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
                for(int i=0;i<nums.Length-1;i++)
                    arr[i]=(nums[i]+nums[i+1])%10;
                nums = arr;
            }
            return nums[0];
        }


    }
}