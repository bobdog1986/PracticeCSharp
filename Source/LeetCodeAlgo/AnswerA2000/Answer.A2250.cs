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


        ///2278. Percentage of Letter in String
        ///Given a string s and a character letter, return the percentage of characters
        ///in s that equal letter rounded down to the nearest whole percent.
        public int PercentageLetter(string s, char letter)
        {
            return s.Count(x => x == letter) * 100 / s.Length;
        }

        ///2279. Maximum Bags With Full Capacity of Rocks
        public int MaximumBags(int[] capacity, int[] rocks, int additionalRocks)
        {
            int n = rocks.Length;
            int[] arr = new int[n];
            for(int i=0;i<n; i++)
            {
                arr[i] = capacity[i] - rocks[i];
            }
            Array.Sort(arr);
            int res = 0;
            foreach(var i in arr)
            {
                if (i == 0)
                {
                    res++;
                }
                else
                {
                    if (additionalRocks >= i)
                    {
                        additionalRocks -= i;
                        res++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return res;
        }


        ///2280. Minimum Lines to Represent a Line Chart
        public int MinimumLines(int[][] stockPrices)
        {
            if (stockPrices.Length == 1) return 0;//edge case
            int res = 0;
            stockPrices = stockPrices.OrderBy(x => x[0]).ToArray();//sort prices by day
            int n = stockPrices.Length;
            var prev = MinimumLines_GetLine(stockPrices[0], stockPrices[1]);
            res++;
            for (int i = 1; i < n-1; i++)
            {
                var curr = MinimumLines_GetLine(stockPrices[i], stockPrices[i+1]);
                if (curr[0] != prev[0] || curr[1] != prev[1])
                {
                    res++;
                    prev = curr;
                }
            }
            return res;
        }

        private double[] MinimumLines_GetLine(int[] p1, int[] p2)
        {
            if (p1[1] == p2[1])
            {
                return new double[] { 0, p1[1] };
            }
            else
            {
                double a = 1.0 * (p1[1] - p2[1]) / (p1[0] - p2[0]);
                double b = 1.0 * (p2[0] * p1[1] - p1[0] * p2[1]) / (p2[0] - p1[0]);
                return new double[] { a, b };
            }
        }


        ///2281. Sum of Total Strength of Wizards, #Prefix Sum, #Monotonic
        ///strength[i] denotes the strength of the ith wizard.For a contiguous group of wizards,two values:
        /// - The strength of the weakest wizard in the group.
        /// - The total of all the individual strengths of the wizards in the group.
        ///Return the sum of the total strengths of all contiguous groups of wizards. return it modulo 109 + 7.
        public int TotalStrength(int[] strength)
        {
            int mod = 10_0000_0007;
            int n = strength.Length;

            int[] right = new int[n];
            Array.Fill(right, n);
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < n; i++)
            {
                while (stack.Count >0 && strength[stack.Peek()] > strength[i])
                {
                    right[stack.Pop()] = i;
                }
                stack.Push(i);
            }

            int[] left = new int[n];
            Array.Fill(left, -1);
            stack.Clear();
            for (int i = n - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && strength[stack.Peek()] >= strength[i])
                {
                    left[stack.Pop()] = i;
                }
                stack.Push(i);
            }

            long res = 0;
            long[] preSum = new long[n];
            for (int i = 0; i < n; i++)
            {
                preSum[i] = strength[i];
                if (i > 0)
                {
                    preSum[i] = (preSum[i] + preSum[i - 1]) % mod;
                }
            }

            long[] preSumPreSum = new long[n + 1];
            for (int i = 1; i < n + 1; i++)
            {
                preSumPreSum[i] = (preSumPreSum[i - 1] + preSum[i - 1]) % mod;
            }

            for (int i = 0; i < n; i++)
            {
                int l = left[i];
                int r = right[i];
                long lSum = preSumPreSum[i] - preSumPreSum[Math.Max(l, 0)];
                long rSum = preSumPreSum[r] - preSumPreSum[i];
                res = (res + strength[i] * (rSum * (i - l) % mod - lSum * (r - i) % mod)) % mod;
            }
            return (int)(res % mod);
        }


        ///2283. Check if Number Has Equal Digit Count and Digit Value
        ///You are given a 0-indexed string num of length n consisting of digits.
        ///Return true if every index 0 <= i<n, the digit i occurs num[i] times in num,otherwise return false.

        public bool DigitCount(string num)
        {
            int[] arr = new int[10];
            foreach(var n in num)
            {
                arr[n - '0']++;
            }
            for(int i = 0; i < num.Length; i++)
            {
                if (arr[i] !=( num[i]-'0'))
                    return false;
            }
            return true;
        }


        ///2284. Sender With Largest Word Count
        ///Return the sender with the largest word count or lexicographically largest name.
        public string LargestWordCount(string[] messages, string[] senders)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            for(int i = 0; i < senders.Length; i++)
            {
                if (!dict.ContainsKey(senders[i]))
                    dict.Add(senders[i], 0);
                dict[senders[i]] += messages[i].Split(' ').Count();
            }

            int max = 0;
            var list = new List<string>();
            foreach(var k in dict.Keys)
            {
                if (dict[k] > max)
                {
                    list = new List<string>() { k };
                    max = dict[k];
                }
                else if (dict[k] == max)
                {
                    list.Add(k);
                }
            }
            list.Sort((x, y) => string.CompareOrdinal(x, y));
            return list.Last();
        }

        ///2285. Maximum Total Importance of Roads
        ///Return the maximum total importance of all roads possible after assigning the values optimally.
        public long MaximumImportance(int n, int[][] roads)
        {
            long res = 0;
            int[] graph = new int[n];
            foreach(var road in roads)
            {
                graph[road[0]]++;
                graph[road[1]]++;
            }
            Array.Sort(graph);
            for(int i = n; i >= 1; i--)
            {
                res+= graph[i - 1] * (long)i;
            }
            return res;
        }


        ///2287. Rearrange Characters to Make Target String
        ///You can take some letters from s and rearrange them to form new strings.
        ///Return the maximum number of copies of target that can be formed by taking letters from s and rearranging them.
        public int RearrangeCharacters(string s, string target)
        {
            var dict1=new Dictionary<char, int>();
            foreach(var c in target)
            {
                if (dict1.ContainsKey(c)) dict1[c]++;
                else dict1.Add(c, 1);
            }

            var dict2 = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (dict2.ContainsKey(c)) dict2[c]++;
                else dict2.Add(c, 1);
            }

            int res = int.MaxValue;
            foreach(var k in dict1.Keys)
            {
                if (dict2.ContainsKey(k))
                {
                    res = Math.Min(res, dict2[k] / dict1[k]);
                }
                else
                {
                    res = 0;
                    break;
                }
            }
            return res;
        }
    }

}
