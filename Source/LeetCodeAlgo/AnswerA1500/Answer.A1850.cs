using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1854. Maximum Population Year
        public int MaximumPopulation(int[][] logs)
        {
            int start = 1950;
            int[] years = new int[101];
            foreach(var log in logs)
            {
                years[log[0] - start]++;
                years[log[1] - start]--;
            }

            int max = 0;
            int sum = 0;
            int res = -1;
            for(int i = 0; i < years.Length; i++)
            {
                sum += years[i];
                if (sum > max)
                {
                    max = sum;
                    res = i;
                }
            }
            return res + start;
        }
        /// 1855. Maximum Distance Between a Pair of Values
        ///You are given two non-increasing 0-indexed integer arrays nums1 and nums2​​​​​​
        ///i <= j and nums1[i] <= nums2[j]. The distance of the pair is j - i​​​​.
        ///Return the maximum distance of any valid pair(i, j). If there are no valid pairs, return 0.
        public int MaxDistance(int[] nums1, int[] nums2)
        {
            int i = 0;
            int j = 0;
            int res = 0;
            while(i<nums1.Length && j < nums2.Length)
            {
                if (i > j)
                {
                    j++;
                }
                else
                {
                    if (nums1[i] > nums2[j]) i++;
                    else
                    {
                        res = Math.Max(res, j - i);
                        j++;
                    }
                }
            }
            return res;
        }

        ///1856. Maximum Subarray Min-Product, #Monotonic Stack
        //The min-product is equal to the minimum value in the array multiplied by the array's sum.
        //return the maximum min-product of any non-empty subarray of nums.
        //Since the answer may be large, return it modulo 109 + 7.
        //Note that the min-product should be maximized before performing the modulo operation.
        //Testcases are generated such that the maximum min-product without modulo will fit in a 64-bit signed integer.
        public int MaxSumMinProduct(int[] nums)
        {
            long res = 0;
            long mod = 1_000_000_007;
            long[] prefixSum = initPrefixSum(nums);
            int[] leftArr = initMonotonicLeftSmallerArr(nums);
            int[] rightArr = initMonotonicRightSmallerArr(nums);

            for(int i = 0; i < nums.Length; i++)
            {
                int left = leftArr[i] + 1;
                int right = rightArr[i] - 1;
                long sum = prefixSum[right];
                if (left != 0)
                    sum -= prefixSum[left-1];

                res =Math.Max(res, sum * nums[i]);
            }
            return (int)(res % mod);
        }

        ///1859. Sorting the Sentence
        public string SortSentence(string s)
        {
            return string.Join(' ', s.Split(' ').OrderBy(x => x.Last()).Select(x => x.Substring(0, x.Length - 1)));
        }
        /// 1860. Incremental Memory Leak
        ///Return an array containing [crashTime, memory1crash, memory2crash]
        public int[] MemLeak(int memory1, int memory2)
        {
            int i = 1;
            while (Math.Max(memory1, memory2) >= i)
            {
                if (memory1 >= memory2) memory1 -= i;
                else memory2 -= i;
                i++;
            }
            return new int[] { i, memory1, memory2 };
        }

        ///1861. Rotating the Box
        ///A stone '#',A stationary obstacle '*',Empty '.'
        public char[][] RotateTheBox(char[][] box)
        {
            int m = box.Length;
            int n = box[0].Length;
            var res = new char[n][];
            for (int i = 0; i < n; i++)
                res[i] = new char[m];
            for (int i = 0; i < m; i++)
            {
                int r = 0;
                int stone = 0;
                for (int j = 0; j < n; j++)
                {
                    if (box[i][j] == '*')
                    {
                        while (stone> 0)
                        {
                            res[r++][m - 1 - i] = '#';
                            stone--;
                        }
                        res[r++][m - 1 - i] = '*';
                    }
                    else if (box[i][j] == '.')
                    {
                        res[r++][m - 1 - i] = '.';
                    }
                    else
                    {
                        stone++;
                    }
                }
                while (stone > 0)
                {
                    res[r++][m - 1 - i] = '#';
                    stone--;
                }
            }
            return res;
        }

        ///1863. Sum of All Subset XOR Totals, #Backtracking
        public int SubsetXORSum(int[] nums)
        {
            int res = 0;
            SubsetXORSum(nums, 0, 0, ref res);
            return res;
        }
        private void SubsetXORSum(int[] nums,int i,int curr, ref int res)
        {
            if (i == nums.Length)
            {
                res += curr;
            }
            else
            {
                SubsetXORSum(nums, i + 1, curr, ref res);
                SubsetXORSum(nums, i + 1, curr ^ nums[i], ref res);
            }
        }
        ///1864. Minimum Number of Swaps to Make the Binary String Alternating
        public int MinSwaps_1864(string s)
        {
            int n = s.Length;
            int zeros = s.Count(x => x == '0');
            if (n % 2 == 0)
            {
                if (zeros != n / 2) return -1;

                int diff0 = 0;
                int diff1 = 0;

                int seed = 0;
                for(int i = 0; i < n; i++)
                {
                    if (s[i] - '0' != seed) diff0++;
                    if (s[i] - '0' == seed) diff1++;
                    seed ^= 1;
                }
                return Math.Min(diff0, diff1) / 2;
            }
            else
            {
                if(zeros<n/2 || zeros>(n/2+1)) return -1;

                int seed = zeros == n / 2 ? 1 : 0;
                int diff = 0;
                for (int i = 0; i < n; i++)
                {
                    if (s[i] - '0' != seed) diff++;
                    seed ^= 1;
                }
                return diff / 2;
            }
        }
        /// 1865. Finding Pairs With a Certain Sum, see FindSumPairs

        ///1869. Longer Contiguous Segments of Ones than Zeros
        public bool CheckZeroOnes(string s)
        {
            int max0 = 0;
            int max1 = 0;
            int ones = 0;
            int zeros = 0;
            foreach(var c in s)
            {
                if (c == '0')
                {
                    zeros++;
                    ones = 0;
                }
                else
                {
                    ones++;
                    zeros = 0;
                }
                max0 = Math.Max(max0, zeros);
                max1 = Math.Max(max1, ones);
            }
            return max1 > max0;
        }
        /// 1870. Minimum Speed to Arrive on Time, #Binary Search
        //Return the minimum positive integer speed (in kilometers per hour) that all the trains
        //must travel at for you to reach the office on time, or -1 if it is impossible to be on time.
        // result must <= 10_000_000
        public int MinSpeedOnTime(int[] dist, double hour)
        {
            int left = 1;
            int right = 10_000_000;
            int n = dist.Length;
            if ((n - 1 + dist.Last() * 1.0 / right) > hour) return -1;

            while (left < right)
            {
                int mid = (left + right) / 2;
                double sum = 0;
                for (int i = 0; i < n; i++)
                {
                    if (i == n - 1)
                    {
                        sum += dist[i] * 1.0 / mid;
                    }
                    else
                    {
                        sum += (int)Math.Ceiling(dist[i] * 1.0 / mid);
                    }
                }

                if (sum <= hour)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return left;
        }

        /// 1876. Substrings of Size Three with Distinct Characters
        ///A string is good if there are no repeated characters.
        ///Given a string s,return the number of good substrings of length three in s.
        public int CountGoodSubstrings(string s)
        {
            int res = 0;
            Dictionary<char, int> dict = new Dictionary<char, int>();
            for (int i = 0; i < s.Length - 2; i++)
            {
                if (i == 0)
                {
                    dict.Add(s[0], 1);
                    if (dict.ContainsKey(s[1])) dict[s[1]]++;
                    else dict.Add(s[1], 1);
                }
                if (dict.ContainsKey(s[i + 2])) dict[s[i + 2]]++;
                else dict.Add(s[i + 2], 1);
                if (dict.Count == 3) res++;
                dict[s[i]]--;
                if (dict[s[i]] == 0) dict.Remove(s[i]);
            }
            return res;
        }

        ///1877. Minimize Maximum Pair Sum in Array
        ///Return the minimized maximum pair sum after optimally pairing up the elements.
        public int MinPairSum(int[] nums)
        {
            int max = int.MinValue;
            Array.Sort(nums);
            for (int i = 0; i < nums.Length / 2; i++)
                max = Math.Max(max, nums[i] + nums[nums.Length - 1 - i]);
            return max;
        }
        ///1880. Check if Word Equals Summation of Two Words
        public bool IsSumEqual(string firstWord, string secondWord, string targetWord)
        {
            return IsSumEqual(firstWord) + IsSumEqual(secondWord) == IsSumEqual(targetWord);
        }

        private int IsSumEqual(string str)
        {
            return int.Parse(new string(str.Select(x => (char)(x - 'a' + '0')).ToArray()));
        }

        ///1881. Maximum Value after Insertion, #Greedy
        //1 <= x <= 9, 1 <= n.length <= 10^5, n may negative startwith '-'
        public string MaxValue(string n, int x)
        {
            if (n[0] == '-')
            {
                n = n.Substring(1);
                if (x == 1) return $"-{x}{n}";
                else if (x == 9) return $"-{n}{x}";
                else
                {
                    int i = 0;
                    while (i < n.Length)
                    {
                        if (n[i] - '0' <= x) i++;
                        else break;
                    }
                    return $"-{n.Substring(0, i)}{x}{n.Substring(i)}";
                }
            }
            else
            {
                if (x == 1) return $"{n}{x}";
                else if (x == 9) return $"{x}{n}";
                else
                {
                    int i = 0;
                    while(i < n.Length)
                    {
                        if (n[i] - '0' >= x) i++;
                        else break;
                    }
                    return $"{n.Substring(0, i)}{x}{n.Substring(i)}";
                }
            }
        }

        /// 1884. Egg Drop With 2 Eggs and N Floors
        public int TwoEggDrop(int n)
        {
            int res = 0;
            int seed = 1;
            while (n > 0)
            {
                n -= seed++;
                res++;
            }
            return res;
        }
        /// 1886. Determine Whether Matrix Can Be Obtained By Rotation
        ///Given two n x n binary matrices mat and target, return true if it is possible to
        ///make mat equal to target by rotating mat in 90-degree increments, or false otherwise.
        public bool FindRotation(int[][] mat, int[][] target)
        {
            int n = mat.Length;
            bool[] rotate = new bool[4];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //every time rotate right 90 degree, row=j,col=n-1-i;
                    if (!rotate[0] && mat[i][j] != target[i][j]) rotate[0] = true;
                    if (!rotate[1] && mat[j][n - 1 - i] != target[i][j]) rotate[1] = true;
                    if (!rotate[2] && mat[n - 1 - i][n - 1 - j] != target[i][j]) rotate[2] = true;
                    if (!rotate[3] && mat[n - 1 - j][i] != target[i][j]) rotate[3] = true;
                    if (rotate[0] && rotate[1] && rotate[2] && rotate[3]) return false;
                }
            }
            return true;
        }

        ///1887. Reduction Operations to Make the Array Elements Equal
        public int ReductionOperations(int[] nums)
        {
            Dictionary<int,int> dict = new Dictionary<int,int>();
            foreach(var n in nums)
            {
                if (dict.ContainsKey(n)) dict[n]++;
                else dict.Add(n,1);
            }
            int res = 0;
            var keys = dict.Keys.OrderBy(x => x).ToList();
            for(int i=1;i<keys.Count;i++)
                res+=i*dict[keys[i]];
            return res;
        }
        ///1893. Check if All the Integers in a Range Are Covered
        public bool IsCovered(int[][] ranges, int left, int right)
        {
            var set = new HashSet<int>();
            for (int i = left; i <= right; i++)
                set.Add(i);

            foreach(var range in ranges)
            {
                int start = Math.Max(left, range[0]);
                int end = Math.Min(right, range[1]);
                for (int j = start; j <= end; j++)
                    if (set.Contains(j)) set.Remove(j);
                if (set.Count == 0) return true;
            }
            return set.Count == 0;
        }
        /// 1894. Find the Student that Will Replace the Chalk, #Binary Search
        ///Every time k-=chalk[i], return not enough for index i;
        public int ChalkReplacer(int[] chalk, int k)
        {
            long[] prefix = new long[chalk.Length];
            long sum = 0;
            for (int i = 0; i < chalk.Length; i++)
            {
                sum += chalk[i];
                prefix[i] = sum;
            }
            k = (int)(k % sum);
            int left = 0;
            int right = chalk.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (k == prefix[mid]) return mid + 1;
                else if (k < prefix[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return left;
        }

        ///1897. Redistribute Characters to Make All Strings Equal
        public bool MakeEqual(string[] words)
        {
            int[] arr = new int[26];
            foreach(var word in words)
                foreach (var c in word)
                    arr[c - 'a']++;
            return arr.Where(x => x % words.Length != 0).Count() == 0;
        }
        /// 1898. Maximum Number of Removable Characters, #Binary Search
        //removable[] is indexes of s tha can be removed
        //Return the maximum k [0,n] you can choose such that p is still a subsequence of s after the removals.
        public int MaximumRemovals(string s, string p, int[] removable)
        {
            int left = 0;
            int right = removable.Length;
            bool[] mask = new bool[s.Length];
            while (left < right)
            {
                int mid = (left + right+1) / 2;
                for (int i = left; i < mid; i++)
                    mask[removable[i]] = true;

                if (MaximumRemovals_IsSub(s, p, mask))
                {
                    left = mid;
                }
                else
                {
                    right = mid - 1;
                    for (int i = left; i < mid; i++)
                        mask[removable[i]] = false;
                }
            }
            return left;
        }

        private bool MaximumRemovals_IsSub(string s, string p, bool[] mask)
        {
            int i = 0, j = 0;
            for (; i < s.Length && j < p.Length; i++)
            {
                if (mask[i]) continue;
                if (s[i] == p[j]) j++;
            }
            return j == p.Length;
        }

    }
}