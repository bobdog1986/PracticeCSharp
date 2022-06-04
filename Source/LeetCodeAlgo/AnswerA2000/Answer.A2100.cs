using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2101. Detonate the Maximum Bombs,#Graph, #DFS
        ///You may choose to detonate a single bomb. When a bomb is detonated,
        ///it will detonate all bombs that lie in its range. These bombs will
        ///further detonate the bombs that lie in their ranges.
        ///Given the list of bombs, return the maximum number of bombs that can be
        ///detonated if you are allowed to detonate only one bomb.
        public int MaximumDetonation(int[][] bombs)
        {
            int n = bombs.Length;
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i != j && MaximumDetonation_CanDetonated(i, j, bombs))
                        graph[i].Add(j);
                }
            }

            int max = 0;
            for (int i = 0; i < bombs.Length; i++)
            {
                max = Math.Max(max, MaximumDetonation_dfs(i, graph, new bool[n]));
            }
            return max;
        }

        private int MaximumDetonation_dfs(int index, List<int>[] graph, bool[] visit)
        {
            visit[index] = true;
            return 1 + graph[index].Where(x => !visit[x]).Select(x => MaximumDetonation_dfs(x, graph, visit)).Sum();
        }

        private bool MaximumDetonation_CanDetonated(int i, int j, int[][] bombs)
        {
            //true if i bomb detonates j bomb
            long x = bombs[i][0] - bombs[j][0];
            long y = bombs[i][1] - bombs[j][1];
            long r = bombs[i][2];
            return Math.Sqrt(x * x + y * y) <= r;
        }

        /// 2102. Sequentially Ordinal Rank Tracker, see SORTracker

        /// 2103. Rings and Rods
        ///Return the number of rods that have all three colors of rings on them.
        public int CountPoints(string rings)
        {
            int[,] mat = new int[10, 3];
            for (int i = 0; i < rings.Length; i += 2)
            {
                int index = rings[i + 1] - '0';
                if (rings[i] == 'R')
                {
                    mat[index, 0] = 1;
                }
                else if (rings[i] == 'G')
                {
                    mat[index, 1] = 1;
                }
                else if (rings[i] == 'B')
                {
                    mat[index, 2] = 1;
                }
            }

            int res = 0;
            for (int i = 0; i < 10; i++)
            {
                if (mat[i, 0] == 1 && mat[i, 1] == 1 && mat[i, 2] == 1) res++;
            }
            return res;
        }

        ///2104. Sum of Subarray Ranges, #Monotonic
        ///The range of a subarray of nums is the difference between the largest and smallest element in the subarray.
        ///Return the sum of all subarray ranges of nums. A subarray is a contiguous non-empty sequence.
        public long SubArrayRanges(int[] nums)
        {
            ///The idea is to divide sum(max - min) to sum(max) - sum(min), and focus one each number's counter when it is max and min.
            int n = nums.Length;
            int j, k;
            long res = 0;

            Stack<int> s = new Stack<int>();
            //why <=n? not <n
            for (int i = 0; i <= n; i++)
            {
                while (s.Count > 0 && nums[s.Peek()] > (i == n ? int.MinValue : nums[i]))
                {
                    j = s.Pop();
                    k = s.Count == 0 ? -1 : s.Peek();
                    res -= (long)nums[j] * (i - j) * (j - k);//how many times nums[j] works as min
                }
                s.Push(i);
            }

            s.Clear();
            for (int i = 0; i <= n; i++)
            {
                while (s.Count > 0 && nums[s.Peek()] < (i == n ? int.MaxValue : nums[i]))
                {
                    j = s.Pop();
                    k = s.Count == 0 ? -1 : s.Peek();
                    res += (long)nums[j] * (i - j) * (j - k);//how many times nums[j] works as max
                }
                s.Push(i);
            }
            return res;
        }

        ///2016. Maximum Difference Between Increasing Elements
        /// find the maximum of ( nums[j] - nums[i]), such that 0 <= i < j < n and nums[i] < nums[j].
        ///Return the maximum difference.If no such i and j exists, return -1
        public int MaximumDifference(int[] nums)
        {
            int res = -1;
            int n = nums.Length;
            int max = nums[n - 1];
            for(int i = n - 2; i >= 0; --i)
            {
                if(nums[i] < max && max - nums[i] > res)
                {
                    res = max - nums[i];
                }
                max = Math.Max(max, nums[i]);
            }
            return res;
        }
        /// 2108. Find First Palindromic String in the Array
        public string FirstPalindrome(string[] words)
        {
            foreach (var w in words)
                if (w == new string(w.Reverse().ToArray())) return w;
            return "";
        }

        ///2109. Adding Spaces to a String, #Two Pointers
        ///array spaces that describes the indices in the original string where spaces will be added.
        ///Each space should be inserted before the character at the given index.
        public string AddSpaces(string s, int[] spaces)
        {
            var sb = new StringBuilder();
            int i = 0, j = 0;
            for (; i < s.Length && j < spaces.Length; i++)
            {
                if (i == spaces[j])
                {
                    sb.Append(' ');
                    j++;
                }
                sb.Append(s[i]);
            }
            return sb.ToString() + s.Substring(i);
        }

        ///2110. Number of Smooth Descent Periods of a Stock
        public long GetDescentPeriods(int[] prices)
        {
            long res = 0;
            long left = 0;
            long right = 1;
            for(; right < prices.Length; right++)
            {
                if(prices[right] == prices[right - 1]-1)
                {
                    //
                }
                else
                {
                    res += GetDescentPeriods(right - left);
                    left = right;
                }
            }
            res += GetDescentPeriods(right - left);
            return res;
        }

        private long GetDescentPeriods(long i)
        {
            return (1 + i) * i / 2;
        }

        /// 2114. Maximum Number of Words Found in Sentences
        ///A sentence is a list of words that are separated by a single space with no leading or trailing spaces.
        ///Return the maximum number of words that appear in a single sentence.
        public int MostWordsFound(string[] sentences)
        {
            //return sentences.Max(x => x.Split(' ').Count());
            return sentences.Max(x => x.Where(x => x == ' ').Count()) + 1;
        }

        /// 2119. A Number After a Double Reversal
        ///eg. 1234 reverse to 4321, then again to 1234== origin 1234, return true
        public bool IsSameAfterReversals(int num)
        {
            return num == 0 || num % 10 != 0;
        }

        ///2120. Execution of All Suffix Instructions Staying in a Grid
        public int[] ExecuteInstructions(int n, int[] startPos, string s)
        {
            int[] res = new int[s.Length];
            for(int i = 0; i < s.Length; i++)
            {
                int r = startPos[0];
                int c= startPos[1];
                int count = 0;
                for(int j = i; j < s.Length; j++)
                {
                    if (s[j] == 'U') r--;
                    else if (s[j] == 'D') r++;
                    else if (s[j] == 'L') c--;
                    else if (s[j] == 'R') c++;
                    if (r < 0 || r >= n || c < 0 || c >= n) break;
                    count++;
                }
                res[i] = count;
            }
            return res;
        }
        /// 2124. Check if All A's Appears Before All B's
        public bool CheckString(string s)
        {
            int lastAIndex = -1;
            int lastBIndex = -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'a')
                {
                    if (lastBIndex != -1) return false;
                    lastAIndex = i;
                }
                else if (s[i] == 'b')
                {
                    lastBIndex = i;
                }
            }
            return true;
        }

        ///2125. Number of Laser Beams in a Bank
        public int NumberOfBeams(string[] bank)
        {
            int res = 0;
            int i = 0;
            while (i < bank.Length)
            {
                var count1 = bank[i].Count(x => x == '1');
                if (count1 == 0) i++;
                else
                {
                    int j = i + 1;
                    while (j < bank.Length)
                    {
                        var count2 = bank[j].Count(x => x == '1');
                        if (count2 > 0)
                        {
                            res += count1 * count2;
                            break;
                        }
                        j++;
                    }
                    i = j;
                }
            }
            return res;
        }

        ///2126. Destroying Asteroids
        public bool AsteroidsDestroyed(int mass, int[] asteroids)
        {
            Array.Sort(asteroids);
            long curr = mass;
            foreach(var n in asteroids)
            {
                if (curr >= n) curr += n;
                else return false;
                if (curr >= int.MaxValue) return true;
            }
            return true;
        }
        /// 2129. Capitalize the Title
        /// Capitalize the string by changing the capitalization of each word such that:
        ///If the length of the word is 1 or 2 letters, change all letters to lowercase.
        ///Otherwise, change the first letter to uppercase and the remaining letters to lowercase.

        public string CapitalizeTitle(string title)
        {
            var arr = title.Split(' ').Where(x => x.Length > 0).Select(x =>
             {
                 var str = x.ToLower();
                 if (x.Length <= 2) return str;
                 return str.Substring(0, 1).ToUpper() + str.Substring(1);
             });
            return string.Join(" ", arr);
        }

        ///2130. Maximum Twin Sum of a Linked List
        ///Given the head of a linked list with even length, return the maximum twin sum of the linked list.
        public int PairSum(ListNode head)
        {
            List<int> list = new List<int>();
            while (head != null)
            {
                list.Add(head.val);
                head = head.next;
            }

            int max = 0;
            for (int i = 0; i < list.Count / 2; i++)
            {
                max = Math.Max(max, list[i] + list[list.Count - 1 - i]);
            }
            return max;
        }

        ///2131. Longest Palindrome by Concatenating Two Letter Words
        public int LongestPalindrome(string[] words)
        {
            int res = 0;
            bool same = false;
            var dict=new Dictionary<string,int>();
            foreach(var word in words)
            {
                if (dict.ContainsKey(word)) dict[word]++;
                else dict.Add(word, 1);
            }
            foreach (var key in dict.Keys)
            {
                if (dict[key] == 0) continue;
                if (key[0] == key[1])
                {
                    int pair = dict[key] / 2;
                    res += pair * 4;
                    if (dict[key]%2==1)same = true;
                }
                else
                {
                    var mirror = $"{key[1]}{key[0]}";
                    if (dict.ContainsKey(mirror))
                    {
                        var pair = Math.Min(dict[key], dict[mirror]);
                        res += pair * 4;
                        dict[key] -= pair;
                        dict[mirror] -= pair;
                    }
                }
            }
            return same? res+2:res;
        }
        /// 2133. Check if Every Row and Column Contains All Numbers
        ///An n x n matrix is valid if every row and every column contains all the integers from 1 to n (inclusive).
        ///Given an n x n integer matrix matrix, return true if the matrix is valid. Otherwise, return false.
        public bool CheckValid(int[][] matrix)
        {
            int rowLen = matrix.Length;
            int colLen = matrix[0].Length;
            bool[,] colVisitMatrix = new bool[rowLen, colLen];
            for (int i = 0; i < rowLen; i++)
            {
                bool[] rowVisit = new bool[rowLen];
                for (int j = 0; j < colLen; j++)
                {
                    if (colVisitMatrix[j, matrix[i][j] - 1]) return false;
                    colVisitMatrix[j, matrix[i][j] - 1] = true;

                    if (rowVisit[matrix[i][j] - 1]) return false;
                    rowVisit[matrix[i][j] - 1] = true;
                }
            }
            return true;
        }

        ///2134. Minimum Swaps to Group All 1's Together II, #Sliding Window
        ///binary circular array nums, return the minimum number of swaps required to group all 1's together.
        public int MinSwaps(int[] nums)
        {
            int ones = nums.Sum();
            int n = nums.Length;
            int right = 0;
            int onesInWindow = 0;
            int count = 0;
            for (int i = 0; i < n; ++i)
            {
                while(right - i + 1 <= ones)
                {
                    count += nums[right%n];
                    right++;
                }
                onesInWindow = Math.Max(count, onesInWindow);
                count -= nums[i];
            }
            return ones - onesInWindow;
        }
        ///2135. Count Words Obtained After Adding a Letter
        public int WordCount(string[] startWords, string[] targetWords)
        {
            int res = 0;
            HashSet<string> set1= new HashSet<string>(startWords.Select(x => new string(x.OrderBy(o => o).ToArray())));
            //startWords = startWords.Select(x => new string(x.OrderBy(o => o).ToArray())).ToArray();
            //targetWords = targetWords.Select(x => new string(x.OrderBy(o => o).ToArray())).ToArray();

            foreach(var word in targetWords)
            {
                int[] arr = new int[26];
                foreach (var c in word)
                    arr[c - 'a']++;

                for(int i = 0; i < arr.Length; i++)
                {
                    if(arr[i] == 1)
                    {
                        arr[i]--;

                        var sb = new StringBuilder();
                        for (int j = 0; j < arr.Length; j++)
                        {
                            if (arr[j] > 0)
                            {
                                int k = arr[j];
                                while(k-->0)
                                    sb.Append((char)(j + 'a'));
                            }
                        }
                        if(set1.Contains(sb.ToString()))
                        {
                            res++;
                            break;
                        }
                        arr[i]++;
                    }
                }
            }

            return res;
        }
        /// 2136. Earliest Possible Day of Full Bloom, #Greedy
        ///Planting a seed takes time and so does the growth of a seed,plantTime and growTime, of length n each:
        ///plantTime[i] is the days it takes you to plant the ith seed.Every day,you can work on planting exactly one seed.
        ///You do not have to work on planting the same seed on consecutive days, but the planting of a seed is not complete until you have worked plantTime[i] days on planting it in total.
        ///growTime[i] is the number of full days it takes the ith seed to grow after being completely planted.
        ///Return the earliest possible day where all seeds are blooming.
        public int EarliestFullBloom(int[] plantTime, int[] growTime)
        {
            int n = plantTime.Length;
            int[][] times = new int[n][];
            for (int i = 0; i < n; i++)
            {
                times[i] = new int[2];
                times[i][0] = plantTime[i];
                times[i][1] = growTime[i];
            }

            Array.Sort(times, (x, y) =>
            {
                return y[1] - x[1];
            });

            int res = 0, curStart = 0;
            for (int i = 0; i < n; i++)
            {
                res = Math.Max(res, curStart + times[i][0] + times[i][1]);
                curStart += times[i][0];
            }

            return res;
        }
        /// 2138. Divide a String Into Groups of Size k
        ///If last str.length < k, pad right with char fill
        public string[] DivideString(string s, int k, char fill)
        {
            int n = s.Length / k;
            if (s.Length % k != 0)
                n++;
            var res = new string[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = s.Substring(i * k, Math.Min(k, s.Length - i * k));
            }
            if (s.Length % k != 0)
            {
                res[n - 1] = res[n - 1].PadRight(k, fill);
            }
            return res;
        }

        ///2139. Minimum Moves to Reach Target Score
        ///Increment the current integer by one (i.e., x = x + 1). Double the current integer(i.e., x = 2 * x).
        public int MinMoves(int target, int maxDoubles)
        {
            int res = 0;
            while (target > 1 && maxDoubles > 0)
            {
                res += 1 + target % 2;
                maxDoubles--;
                target >>= 1;
            }
            return target - 1 + res;
        }
        /// 2140. Solving Questions With Brainpower, #DP
        public long MostPoints_DP(int[][] questions)
        {
            int n = questions.Length;
            long[] dp = new long[n+1];
            for (int i = n - 1; i >= 0; i--)
            {
                int points = questions[i][0];
                int next = questions[i][1] +i + 1;
                if (next >= n)
                    next = n;

                dp[i] = Math.Max(points + dp[next], dp[i + 1]);
            }
            return dp[0];
        }

        public long MostPoints_DFS(int[][] questions)
        {
            long[] dp= new long[questions.Length];
            return MostPoints_DFS(questions, 0, dp);
        }

        private long MostPoints_DFS(int[][] questions, int i, long[] dp)
        {
            if (i >= questions.Length) return 0;
            if (dp[i] > 0)
                return dp[i];
            int points = questions[i][0];
            int next = questions[i][1]+i+1;
            dp[i] = Math.Max(MostPoints_DFS(questions, i + 1, dp), points + MostPoints_DFS(questions, next, dp));
            return dp[i];
        }

        /// 2144. Minimum Cost of Buying Candies With Discount
        ///For every two candies sold, the shop gives a third candy for free.
        ///return the minimum cost of buying all the candies.
        public int MinimumCost(int[] cost)
        {
            var arr=cost.OrderBy(x => -x).ToArray();
            int sum = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                sum += arr[i++];
                if (i >= arr.Length) break;
                sum += arr[i++];
            }
            return sum;
        }
        ///2145. Count the Hidden Sequences
        public int NumberOfArrays(int[] differences, int lower, int upper)
        {
            long sum = 0;
            long max = 0;
            long min = 0;
            foreach (var d in differences)
            {
                sum += d;
                max = Math.Max(max, sum);
                min = Math.Min(min, sum);
            }
            if ((upper - lower) < (max - min)) return 0;
            else return (int)((upper - lower) - (max - min) + 1);
        }
        /// 2148. Count Elements With Strictly Smaller and Greater Elements
        ///return the number of elements that have both a strictly smaller and a strictly greater element appear in nums.
        ///-100000 <= nums[i] <= 100000
        public int CountElements(int[] nums)
        {
            int ship = 100000;
            int[] arr = new int[ship * 2 + 1];
            int start = arr.Length - 1;
            int end = 0;
            foreach (var num in nums)
            {
                var index = num + ship;
                arr[index]++;
                start = Math.Min(start, index);
                end = Math.Max(end, index);
            }
            int sum = 0;
            for (int i = start + 1; i <= end - 1; i++)
                sum += arr[i];
            return sum;
        }

        ///2149. Rearrange Array Elements by Sign
        ///Every consecutive pair of integers have opposite signs.
        ///For all integers with the same sign, the order in which they were present in nums is preserved.
        ///The rearranged array begins with a positive integer
        public int[] RearrangeArray(int[] nums)
        {
            int len = nums.Length / 2;
            int[] arr1 = new int[len];
            int[] arr2 = new int[len];
            int i = 0, j = 0;
            foreach (var n in nums)
            {
                if (n > 0) arr1[i++] = n;
                else arr1[j++] = n;
            }
            for (int k = 0; k < len; k++)
            {
                nums[2 * k] = arr1[k];
                nums[2 * k + 1] = arr2[k];
            }
            return nums;
        }
    }
}