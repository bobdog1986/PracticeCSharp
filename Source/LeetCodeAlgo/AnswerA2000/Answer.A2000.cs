using System.Collections.Generic;
using System;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2000. Reverse Prefix of Word
        ///reverse the segment of word that starts at index 0 and ends at the index of the first occurrence of ch (inclusive).
        ///If the character ch does not exist in word, do nothing.
        public string ReversePrefix(string word, char ch)
        {
            List<char> list = new List<char>();
            for (int i = 0; i < word.Length; i++)
            {
                list.Add(word[i]);
                if (ch == word[i])
                {
                    list.Reverse();
                    return new string(list.ToArray()) + word.Substring(i + 1);
                }
            }
            return word;
        }

        /// 2001. Number of Pairs of Interchangeable Rectangles
        public long InterchangeableRectangles(int[][] rectangles)
        {
            long sum = 0;
            Dictionary<string, long> pairs = new Dictionary<string, long>();
            foreach (var rect in rectangles)
            {
                var gcb = getGcb(rect[0], rect[1]);
                var key = rect[0] / gcb + ":" + rect[1] / gcb;
                if (pairs.ContainsKey(key))
                    pairs[key]++;
                else
                    pairs.Add(key, 1);
            }
            foreach (var pair in pairs)
            {
                if (pair.Value > 1)
                {
                    sum += pair.Value * (pair.Value - 1) / 2;
                }
            }
            return sum;
        }

        ///2006. Count Number of Pairs With Absolute Difference K
        ///return the number of pairs (i, j) where i < j such that |nums[i] - nums[j]| == k.
        ///1 <= k <= 99, 1 <= nums[i] <= 100, 1 <= nums.length <= 200
        public int CountKDifference(int[] nums, int k)
        {
            if (nums.Length <= 1)
                return 0;
            int[] arr = new int[101];
            int start = 100;
            int end = 1;
            foreach (var num in nums)
            {
                arr[num]++;
                start = Math.Min(start, num);
                end = Math.Max(end, num);
            }
            int ans = 0;
            for (int i = start; i <= end - k; i++)
            {
                if (arr[i] > 0 && arr[i + k] > 0)
                {
                    ans += arr[i] * arr[i + k];
                }
            }
            return ans;
        }

        ///2011. Final Value of Variable After Performing Operations
        public int FinalValueAfterOperations(string[] operations)
        {
            return operations.Sum(x => x.Contains("++") ? 1 : -1);
        }

        ///2018. Check if Word Can Be Placed In Crossword
        public bool PlaceWordInCrossword(char[][] board, string word)
        {
            int m = board.Length;
            int n = board[0].Length;
            int len = word.Length;
            for (int i = 0; i < m; i++)
            {
                for(int j=0;j< n; j++)
                {
                    if (board[i][j] == '#') continue;
                    if (i == 0 || board[i - 1][j] == '#')
                    {
                        int l = 0;
                        for (int k=i;k< m && l<len;k++)
                        {
                            if (board[k][j] == '#') break;
                            if (board[k][j] != word[l] && board[k][j] != ' ') break;
                            l++;
                            if (l==len && k!=m-1 && board[k+1][j]!='#')
                            {
                                l = -1;
                                break;
                            }
                        }
                        if (l == len) return true;
                    }

                    if (i == m-1 || board[i + 1][j] == '#')
                    {
                        int l = 0;
                        for (int k = i; k >=0 && l < len; k--)
                        {
                            if (board[k][j] == '#') break;
                            if (board[k][j] != word[l] && board[k][j] != ' ') break;
                            l++;
                            if (l == len && k != 0 && board[k - 1][j] != '#')
                            {
                                l = -1;
                                break;
                            }
                        }
                        if (l == len) return true;
                    }

                    if (j == 0 || board[i][j-1] == '#')
                    {
                        int l = 0;
                        for (int k = j; k < n && l < len; k++)
                        {
                            if (board[i][k] == '#') break;
                            if (board[i][k] != word[l] && board[i][k] != ' ') break;
                            l++;
                            if (l == len && k != n-1 && board[i][k+1] != '#')
                            {
                                l = -1;
                                break;
                            }
                        }
                        if (l == len) return true;

                    }

                    if (j == n-1 || board[i][j + 1] == '#')
                    {
                        int l = 0;
                        for (int k = j; k >=0 && l < len; k--)
                        {
                            if (board[i][k] == '#') break;
                            if (board[i][k] != word[l] && board[i][k] != ' ') break;
                            l++;
                            if (l == len && k !=0 && board[i][k - 1] != '#')
                            {
                                l = -1;
                                break;
                            }
                        }
                        if (l == len) return true;
                    }
                }
            }

            return false;
        }
        /// 2022. Convert 1D Array Into 2D Array
        public int[][] Construct2DArray(int[] original, int m, int n)
        {
            if (original.Length != m * n)
                return new int[0][] { };
            var ans = new int[m][];
            for (int i = 0; i < m * n; i++)
            {
                if (i % n == 0)
                    ans[i / n] = new int[n];
                ans[i / n][i % n] = original[i];
            }
            return ans;
        }

        ///2023. Number of Pairs of Strings With Concatenation Equal to Target
        ///Given an array of digit strings nums and a digit string target,
        ///return the number of pairs of indices (i, j) (where i != j) such that the concatenation of nums[i] + nums[j] equals target.
        public int NumOfPairs(string[] nums, string target)
        {
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i])) dict.Add(nums[i], new List<int>());
                dict[nums[i]].Add(i);
            }
            int res = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (target.StartsWith(nums[i]))
                {
                    var str = target.Substring(nums[i].Length, target.Length - nums[i].Length);
                    if (dict.ContainsKey(str))
                    {
                        res += dict[str].Where(x => x != i).Count();
                    }
                }
            }
            return res;
        }

        /// 2024. Maximize the Confusion of an Exam, #Sliding Window ,#Binary Search
        ///See 424. Longest Repeating Character Replacement
        ///Change the answer key for any question to 'T' or 'F' (i.e., set answerKey[i] to 'T' or 'F').
        ///Return the maximum number of consecutive 'T's or 'F's in the answer key after performing the operation at most k times.
        ///n == answerKey.length, 1 <= n <= 5 * 10^4, answerKey[i] is either 'T' or 'F', 1 <= k <= n
        public int MaxConsecutiveAnswers(string answerKey, int k)
        {
            int maxfreq = 0;
            int left = 0;
            int[] arr = new int[26];
            for (int i = 0; i < answerKey.Length; i++)
            {
                maxfreq = Math.Max(maxfreq, ++arr[answerKey[i] - 'A']);
                if (i - left + 1 > maxfreq + k)
                {
                    arr[answerKey[left] - 'A']--;
                    left++;
                }
            }
            return answerKey.Length - left;
        }

        public int MaxConsecutiveAnswers_BinarySearch(string answerKey, int k)
        {
            int n = answerKey.Length;
            if (n == k)
                return n;

            List<int[]> list = new List<int[]>();
            int countT = 0;
            int countF = 0;
            list.Add(new int[] { 0, 0 });
            for (int i = 0; i < n; i++)
            {
                if (answerKey[i] == 'T')
                {
                    countT++;
                }
                else
                {
                    countF++;
                }
                list.Add(new int[] { countT, countF });
            }

            int left = k + 1;
            int right = n;

            int mid = (left + right + 1) / 2;

            while (left < right)
            {
                bool exist = false;
                for (int i = 0; i < n - mid + 1; i++)
                {
                    var a = list[i];
                    var b = list[i + mid];

                    int count1 = b[0] - a[0];
                    int count2 = b[1] - a[1];
                    if (count1 + k >= mid || count2 + k >= mid)
                    {
                        exist = true;
                        break;
                    }
                }

                if (exist)
                {
                    left = mid;
                    mid = (left + right + 1) / 2;
                }
                else
                {
                    right = mid - 1;
                    mid = (left + right + 1) / 2;
                }
            }

            return left;
        }

        ///2027. Minimum Moves to Convert String
        public int MinimumMoves(string s)
        {
            int res = 0;
            for (int i = 0; i < s.Length;)
            {
                if (s[i] == 'X')
                {
                    res++;
                    i += 3;
                }
                else i++;
            }
            return res;
        }

        /// 2032. Two Out of Three
        ///return a distinct array containing all the values that in at least two out of the three arrays.
        public IList<int> TwoOutOfThree(int[] nums1, int[] nums2, int[] nums3)
        {
            Dictionary<int, HashSet<int>> dict = new Dictionary<int, HashSet<int>>();
            foreach (var n in nums1)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, new HashSet<int>() { 0 });
            }
            foreach (var n in nums2)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, new HashSet<int>() { 1 });
                else dict[n].Add(1);
            }

            foreach (var n in nums3)
            {
                if (dict.ContainsKey(n))
                    dict[n].Add(2);
            }

            return dict.Keys.Where(x => dict[x].Count >= 2).ToList();
        }

        /// 2037. Minimum Number of Moves to Seat Everyone
        /// Return the minimum number of moves required to move each student to a seat
        /// such that no two students are in the same seat.
        /// Note that there may be multiple seats or students in the same position at the beginning.
        public int MinMovesToSeat(int[] seats, int[] students)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var seat in seats)
            {
                if (dict.ContainsKey(seat)) dict[seat]++;
                else dict.Add(seat, 1);
            }

            int res = 0;

            var keys = dict.Keys.OrderBy(x => x).ToList();
            Array.Sort(students);

            var i = 0;
            foreach (var student in students)
            {
                if (student == keys[i])
                {
                }
                else
                {
                    res += Math.Abs(student - keys[i]);
                }

                dict[keys[i]]--;
                if (dict[keys[i]] == 0) i++;
            }

            return res;
        }

        ///2042. Check if Numbers Are Ascending in a Sentence
        public bool AreNumbersAscending(string s)
        {
            var arr = s.Split(" ").Where(x => char.IsDigit(x[0])).Select(x => int.Parse(x)).ToList();
            for (int i = 0; i < arr.Count - 1; i++)
                if (arr[i] >= arr[i + 1]) return false;
            return true;
        }

        ///2043. Simple Bank System, see Bank

        /// 2044. Count Number of Maximum Bitwise-OR Subsets,#Backtracking
        ///find the maximum possible bitwise OR of a subset of nums and return the number of different non-empty subsets with the maximum bitwise OR.
        ///An array a is a subset of an array b if a can be obtained from b by deleting some(possibly zero) elements of b.
        public int CountMaxOrSubsets(int[] nums)
        {
            int res = 0;
            int maxOr = 0;
            foreach (var n in nums)
                maxOr |= n;
            CountMaxOrSubsets_BackTracking(nums, 0, 0, maxOr, ref res);
            return res;
        }

        private void CountMaxOrSubsets_BackTracking(int[] nums, int i, int currOr, int maxOr, ref int res)
        {
            if (currOr == maxOr && i == nums.Length)
            {
                res++;
                return;
            }

            if (i >= nums.Length) return;
            CountMaxOrSubsets_BackTracking(nums, i + 1, currOr, maxOr, ref res);
            CountMaxOrSubsets_BackTracking(nums, i + 1, currOr | nums[i], maxOr, ref res);
        }

        ///2047. Number of Valid Words in a Sentence

        public int CountValidWords(string sentence)
        {
            int res = 0;
            var arr = sentence.Split(' ').ToArray();
            foreach (var str in arr)
            {
                if (string.IsNullOrEmpty(str)) continue;
                bool find = true;
                int hyphen = 0;
                int punctuation = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == ' ' || char.IsDigit(str[i]))
                    {
                        find = false;
                        break;
                    }
                    else
                    {
                        if (str[i] == '-')
                        {
                            hyphen++;
                            if (hyphen > 1)
                            {
                                find = false;
                                break;
                            }
                            if (i == 0 || i == str.Length - 1 || !char.IsLetter(str[i - 1]) || !char.IsLetter(str[i + 1]))
                            {
                                find = false;
                                break;
                            }
                        }
                        else if (str[i] == '!' || str[i] == ',' || str[i] == '.')
                        {
                            if (i != str.Length - 1)
                            {
                                find = false;
                                break;
                            }
                        }
                    }
                }

                if (find)
                    res++;
            }

            return res;
        }
    }
}