using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2255. Count Prefixes of a Given String
        public int CountPrefixes(string[] words, string s)
        {
            return words.Where(x => s.StartsWith(x)).Count();
        }


        ///2256. Minimum Average Difference
        ///The average difference of the index i is the absolute difference between the
        ///average of the first i + 1 elements of nums and the average of the last n - i - 1 elements.
        ///Both averages should be rounded down to the nearest integer.
        ///Return the index with the minimum average difference.If there are multiple such indices, return the smallest one.
        public int MinimumAverageDifference(int[] nums)
        {
            int res = 0;
            int min = int.MaxValue;

            long sum = 0;
            foreach (var x in nums)
                sum += x;

            int n = nums.Length;
            long curr = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                curr += nums[i];
                sum -= nums[i];

                long left = curr / (i + 1);
                long right = i < nums.Length - 1 ? sum / (n - i - 1) : 0;
                var diff = (int)Math.Abs(left - right);
                if (diff < min)
                {
                    min = diff;
                    res = i;
                }
            }
            return res;
        }
        ///2257. Count Unguarded Cells in the Grid
        ///A guard can see every cell in the four cardinal directions (north, east, south, or west) unless obstructed by a wall or another guard.
        ///Return the number of unoccupied cells that are not guarded.
        public int CountUnguarded(int m, int n, int[][] guards, int[][] walls)
        {
            int count = 0;
            bool[,] visit = new bool[m, n];
            int[,] matrix = new int[m, n];
            foreach (var w in walls)
                matrix[w[0], w[1]] = 1;
            foreach (var g in guards)
                matrix[g[0], g[1]] = 2;

            foreach (var g in guards)
            {
                visit[g[0], g[1]] = true;
                count++;

                for (int i = g[0] - 1; i >= 0; i--)
                {
                    if (matrix[i, g[1]] != 0) break;
                    if (!visit[i, g[1]]) count++;
                    visit[i, g[1]] = true;
                }
                for (int i = g[0] + 1; i < m; i++)
                {
                    if (matrix[i, g[1]] != 0) break;
                    if (!visit[i, g[1]]) count++;
                    visit[i, g[1]] = true;
                }
                for (int i = g[1] - 1; i >= 0; i--)
                {
                    if (matrix[g[0], i] != 0) break;
                    if (!visit[g[0], i]) count++;
                    visit[g[0], i] = true;
                }
                for (int i = g[1] + 1; i < n; i++)
                {
                    if (matrix[g[0], i] != 0) break;
                    if (!visit[g[0], i]) count++;
                    visit[g[0], i] = true;
                }
            }
            return m * n - count - walls.Length;
        }
        /// 2259. Remove Digit From Number to Maximize Result
        public string RemoveDigit(string number, char digit)
        {
            string res = string.Empty;
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == digit)
                {
                    var str = number.Substring(0, i) + number.Substring(i + 1);
                    if (string.IsNullOrEmpty(str) || string.Compare(res, str) < 0)
                        res = str;
                }
            }
            return res;
        }

        ///2260. Minimum Consecutive Cards to Pick Up
        ///Return the minimum number of consecutive cards have a pair of matching cards.
        ///If it is impossible to have matching cards, return -1.
        public int MinimumCardPickup(int[] cards)
        {
            int res = int.MaxValue;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < cards.Length; i++)
            {
                if (dict.ContainsKey(cards[i]))
                {
                    res = Math.Min(res, i - dict[cards[i]] + 1);
                    dict[cards[i]] = i;
                }
                else
                {
                    dict.Add(cards[i], i);
                }
            }
            return res == int.MaxValue ? -1 : res;
        }

        ///2261. K Divisible Elements Subarrays
        ///return the number of distinct subarrays which have at most k elements divisible by p.
        public int CountDistinct(int[] nums, int k, int p)
        {
            int count = 0;
            int left = 0;
            HashSet<string> set = new HashSet<string>();
            var list = nums.ToList();
            int i = 0;
            for (; i < list.Count; i++)
            {
                if (list[i] % p == 0)
                {
                    if (count == k)
                    {
                        for (int j = left; j <= i - 1; j++)
                        {
                            for (int x = j; x <= i - 1; x++)
                            {
                                var str = string.Join('_', list.GetRange(j, x - j + 1));
                                set.Add(str);
                            }
                        }
                        while (left <= i)
                        {
                            if (list[left++] % p == 0)
                                break;
                        }
                        count--;
                    }
                    count++;
                }
            }
            for (int j = left; j <= i - 1; j++)
            {
                for (int x = j; x <= i - 1; x++)
                {
                    var str = string.Join('_', list.GetRange(j, x - j + 1));
                    set.Add(str);
                }
            }
            return set.Count;
        }

        ///2262. Total Appeal of A String, #DP
        ///The appeal of a string is the number of distinct characters found in the string.
        ///Given a string s, return the total appeal of all of its substrings.
        public long AppealSum(string s)
        {
            int[] last = new int[26];
            long res = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                last[s[i] - 'a'] = i + 1;
                res += last.Sum(x => (long)x);
            }
            return res;
        }


        ///2264. Largest 3-Same-Digit Number in String
        public string LargestGoodInteger(string num)
        {
            char max = ' ';
            int count = 1;
            char prev = num[0];
            for (int i = 1; i < num.Length; ++i)
            {
                if (num[i] == prev) count++;
                else
                {
                    prev = num[i];
                    count = 1;
                }
                if (count >= 3) max = (char)Math.Max(max, prev);
                if (max == '9') break;
            }
            return max == ' ' ? "" : max.ToString() + max.ToString() + max.ToString();
        }

        ///2265. Count Nodes Equal to Average of Subtree, BTree

        public int AverageOfSubtree(TreeNode root)
        {
            int res = 0;
            AverageOfSubtree(root, ref res);
            return res;
        }

        private int[] AverageOfSubtree(TreeNode root, ref int res)
        {
            var arr = new int[2];
            if (root == null) return arr;
            var left = AverageOfSubtree(root.left, ref res);
            var right = AverageOfSubtree(root.right, ref res);

            int subSum = left[0] + right[0];
            int subCount = left[1] + right[1];
            if ((subSum + root.val) / (subCount + 1) == root.val)
                res++;

            arr[0] = subSum + root.val;
            arr[1] = subCount + 1;
            return arr;
        }

        ///2266. Count Number of Texts, #DP
        ///Given a string pressedKeys representing the string received by Bob,
        ///return the total number of possible text messages Alice could have sent.
        public int CountTexts(string pressedKeys)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>()
            {
                {'2',3 },
                {'3',3 },
                {'4',3 },
                {'5',3 },
                {'6',3 },
                {'7',4 },
                {'8',3 },
                {'9',4 },
            };

            long[] dp = new long[pressedKeys.Length+1];
            dp[0] = 1;
            for (int i = 1; i < pressedKeys.Length+1; i++)
            {
                int count = 0;
                for (int j = i - 1; j >= 0 && count< dict[pressedKeys[i - 1]]; j--)
                {
                    if (pressedKeys[j] == pressedKeys[i-1])
                    {
                        dp[i] += dp[j];
                        dp[i] %= 10_0000_0007;
                        count++;
                    }
                    else
                    {
                        if (count == 0)
                        {
                            dp[i] += dp[j];
                            dp[i] %= 10_0000_0007;
                        }
                        break;
                    }
                }
            }
            return (int)(dp.Last());
        }

        ///2269 Find the K-Beauty of a Number
        /// It has a length of k. It is a divisor of num.
        public int DivisorSubstrings(int num, int k)
        {
            int res = 0;
            var str = num.ToString();
            for(int i = 0; i < str.Length - k + 1; i++)
            {
                var curr =int.Parse( str.Substring(i, k));
                if (curr == 0) continue;
                if (num % curr == 0) res++;
            }
            return res;
        }

        ///2270. Number of Ways to Split Array, #Prefix Sum
        ///The sum of the first [0,i] elements is >= the sum of the last [i+1,n-1] elements.
        ///There is at least one element to the right of i.That is, 0 <= i<n - 1.
        public int WaysToSplitArray(int[] nums)
        {
            long total = 0;
            foreach (var n in nums)
                total += n;

            int res = 0;
            long curr = 0;
            for(int i = 0; i < nums.Length-1; i++)
            {
                curr += nums[i];
                if (curr >= total - curr)
                    res++;
            }
            return res;
        }

        ///2273. Find Resultant Array After Removing Anagrams
        public IList<string> RemoveAnagrams(string[] words)
        {
            var res=new List<string>();
            res.Add(words[0]);;
            string prev =new string(words[0].ToArray().OrderBy(x => x).ToArray());

            for (int i = 1; i < words.Length; i++)
            {
                string curr = new string(words[i].ToArray().OrderBy(x => x).ToArray());
                if (curr != prev)
                {
                    prev = curr;
                    res.Add(words[i]);
                }
            }
            return res;
        }

        ///2274. Maximum Consecutive Floors Without Special Floors
        ///Return the maximum number of consecutive floors without a special floor.
        public int MaxConsecutive(int bottom, int top, int[] special)
        {
            int res = 0;
            Array.Sort(special);
            for (int i = 0; i < special.Length - 1; i++)
            {
                res = Math.Max(special[i + 1] - 1 - special[i], res);
            }
            res = Math.Max(res, special[0] - bottom);
            res = Math.Max(res, top - special.Last());
            return res;
        }

        ///2275. Largest Combination With Bitwise AND Greater Than Zero
        public int LargestCombination(int[] candidates)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach(var candidate in candidates)
            {
                int seed = 1;
                while (seed <= candidate)
                {
                    if ((candidate & seed) != 0)
                    {
                        if(dict.ContainsKey(seed))dict[seed]++;
                        else dict.Add(seed, 1);
                    }
                    seed <<= 1;
                }
            }
            return dict.Values.Max();
        }

        ///2276. Count Integers in Intervals, see CountIntervals

    }

}
