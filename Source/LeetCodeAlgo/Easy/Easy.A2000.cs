using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy
{
    public partial class Easy
    {
        /*
        ///2200. Find All K-Distant Indices in an Array
        //A k-distant index is index i,j such that |i - j| <= k and nums[j] == key.
        //Return a list of all k-distant indices sorted in increasing order.
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

        /// 2215. Find the Difference of Two Arrays
        //Given two 0-indexed integer arrays nums1 and nums2, return a list answer of size 2 where:
        //answer[0] is a list of all distinct integers in nums1 which are not present in nums2.
        //answer[1] is a list of all distinct integers in nums2 which are not present in nums1

        public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
        {
            var res = new List<IList<int>>();
            HashSet<int> set1 = new HashSet<int>(nums1);
            HashSet<int> set2 = new HashSet<int>(nums2);
            res.Add(set1.Where(x => !set2.Contains(x)).ToList());
            res.Add(set2.Where(x => !set1.Contains(x)).ToList());
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
                int[] next = new int[nums.Length - 1];
                for (int i = 0; i < next.Length; i++)
                    next[i] = (nums[i] + nums[i + 1]) % 10;
                nums = next;
            }
            return nums[0];
        }

        /// 2255. Count Prefixes of a Given String
        public int CountPrefixes(string[] words, string s)
        {
            return words.Where(x => s.StartsWith(x)).Count();
        }

        ///2269 Find the K-Beauty of a Number
        /// It has a length of k. It is a divisor of num.
        public int DivisorSubstrings(int num, int k)
        {
            int res = 0;
            string str = num.ToString();
            for (int i = 0; i < str.Length - k + 1; i++)
            {
                int curr = int.Parse(str.Substring(i, k));
                if (curr == 0) continue;
                if (num % curr == 0) res++;
            }
            return res;
        }

        ///2273. Find Resultant Array After Removing Anagrams
        public IList<string> RemoveAnagrams(string[] words)
        {
            var res = new List<string>();
            res.Add(words[0]); ;
            string prev = new string(words[0].ToArray().OrderBy(x => x).ToArray());

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

        ///2278. Percentage of Letter in String
        ///Given a string s and a character letter, return the percentage of characters
        ///in s that equal letter rounded down to the nearest whole percent.
        public int PercentageLetter(string s, char letter)
        {
            return s.Count(x => x == letter) * 100 / s.Length;
        }

        ///2283. Check if Number Has Equal Digit Count and Digit Value
        ///You are given a 0-indexed string num of length n consisting of digits.
        ///Return true if every index 0 <= i<n, the digit i occurs num[i] times in num,otherwise return false.

        public bool DigitCount(string num)
        {
            int[] arr = new int[10];
            foreach (var c in num)
            {
                arr[c - '0']++;
            }
            for (int i = 0; i < num.Length; i++)
            {
                if (arr[i] != (num[i] - '0'))
                    return false;
            }
            return true;
        }

        /// 2288. Apply Discount to Prices
        ///Return a string representing the modified sentence.
        public string DiscountPrices(string sentence, int discount)
        {
            var arr = sentence.Split(' ').Select(x =>
            {
                if (x.Length > 1 && x[0] == '$')
                {
                    string str = x.Substring(1);
                    if (!str.Any(c => !char.IsDigit(c)))
                    {
                        double a = -1;
                        double.TryParse(str, out a);
                        if (a >= 0)
                        {
                            return "$" + string.Format("{0:0.00}", Math.Round((a * (100.0 - discount) / 100), 2));
                        }
                    }
                }
                return x;
            }).ToArray();
            return string.Join(' ', arr);
        }

        ///2293. Min Max Game
        public int MinMaxGame(int[] nums)
        {
            while (nums.Length > 1)
            {
                int[] next = new int[nums.Length / 2];
                for (int i = 0; i < next.Length; i++)
                {
                    if (i % 2 == 0)
                        next[i] = Math.Min(nums[2 * i], nums[2 * i + 1]);
                    else
                        next[i] = Math.Max(nums[2 * i], nums[2 * i + 1]);
                }
                nums = next;
            }
            return nums[0];
        }

        ///2299. Strong Password Checker II
        //at least 8 characters, one lowercase, one uppercase, one digit, one special in "!@#$%^&*()-+".
        //It does not contain 2 of the same character in adjacent positions
        //Given a string password, return true if it is a strong password.Otherwise, return false.
        public bool StrongPasswordCheckerII(string password)
        {
            int n = password.Length;
            if (n < 8) return false;
            var set = new HashSet<char>("!@#$%^&*()-+");
            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpec = false;
            for (int i = 0; i < n; i++)
            {
                if (char.IsUpper(password[i])) hasUpper = true;
                if (char.IsLower(password[i])) hasLower = true;
                if (char.IsDigit(password[i])) hasDigit = true;
                if (set.Contains(password[i])) hasSpec = true;
                if (i > 0 && password[i] == password[i - 1]) return false;
            }
            return hasUpper && hasLower && hasSpec && hasDigit;
        }

        ///2303. Calculate Amount Paid in Taxes
        //brackets[i] = [upperi, percenti] means that the ith tax bracket has an upper bound of upperi and
        //is taxed at a rate of percenti.
        //The brackets are sorted by upper bound (i.e. upperi-1 < upperi for 0 < i < brackets.length).
        public double CalculateTax(int[][] brackets, int income)
        {
            double res = 0;
            int prev = 0;
            foreach (var bracket in brackets)
            {
                int curr = Math.Min(bracket[0], income) - prev;
                res += curr * 1.0 * bracket[1] / 100;
                prev = bracket[0];
                if (bracket[0] >= income) break;
            }
            return res;
        }

        ///2315. Count Asterisks
        //Return the number of '*' in s, excluding the '*' between each pair of '|'.
        public int CountAsterisks(string s)
        {
            bool open = false;
            int res = 0;
            foreach (var c in s)
            {
                if (c == '|')
                    open = !open;
                else if (c == '*' && !open)
                    res++;
            }
            return res;
        }

        ///2319. Check if Matrix Is X-Matrix
        //All the elements in the diagonals of the matrix are non-zero. All other elements are 0.
        public bool CheckXMatrix(int[][] grid)
        {
            int n = grid.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    bool drag = i == j || n - 1 - i == j;
                    if ((drag && grid[i][j] == 0)
                        || (!drag && grid[i][j] != 0)) return false;
                }
            }
            return true;
        }

        ///2325. Decode the Message
        public string DecodeMessage(string key, string message)
        {
            var dict = new Dictionary<char, int>();
            foreach (var c in key)
            {
                if (dict.Count == 26) break;
                if (c == ' ') continue;
                if (dict.ContainsKey(c)) continue;
                dict.Add(c, dict.Count);
            }
            return new string(message.Select(x => x == ' ' ? ' ' : (char)(dict[x] + 'a')).ToArray());
        }

        ///2341. Maximum Number of Pairs in Array
        public int[] NumberOfPairs(int[] nums)
        {
            var dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (!dict.ContainsKey(n)) dict.Add(n, 0);
                dict[n]++;
            }
            var x = dict.Keys.Select(x => dict[x] / 2).Sum();
            var y = dict.Keys.Select(x => dict[x] % 2).Sum();
            return new int[] { x, y };
        }

        ///2347. Best Poker Hand
        public string BestHand(int[] ranks, char[] suits)
        {
            int[] arr1 = new int[14];
            foreach (var r in ranks)
                arr1[r]++;

            int[] arr2 = new int[4];
            foreach (var s in suits)
                arr2[s - 'a']++;

            if (arr2.Max() == 5) return "Flush";
            else if (arr1.Max() >= 3) return "Three of a Kind";
            else if (arr1.Max() == 2) return "Pair";
            else return "High Card";
        }

        ///2348. Number of Zero-Filled Subarrays
        public long ZeroFilledSubarray(int[] nums)
        {
            long res = 0;
            int count = 0;
            foreach (var n in nums)
            {
                if (n == 0) count++;
                else count = 0;
                res += count;
            }
            return res;
        }

        ///2351. First Letter to Appear Twice

        public char RepeatedCharacter(string s)
        {
            var set = new HashSet<char>();
            foreach (var c in s)
            {
                if (set.Contains(c)) return c;
                else set.Add(c);
            }
            return 'a';
        }

        ///2352. Equal Row and Column Pairs
        public int EqualPairs(int[][] grid)
        {
            var dict1 = new Dictionary<string, int>();
            var dict2 = new Dictionary<string, int>();
            for (int i = 0; i < grid.Length; i++)
            {
                var row = string.Join(',', grid[i]);
                if (dict1.ContainsKey(row)) dict1[row]++;
                else dict1.Add(row, 1);
            }

            for (int i = 0; i < grid[0].Length; i++)
            {
                int[] col = new int[grid[0].Length];
                for (int j = 0; j < grid.Length; j++)
                    col[j] = grid[j][i];
                var str = string.Join(',', col);
                if (dict2.ContainsKey(str)) dict2[str]++;
                else dict2.Add(str, 1);
            }

            int res = 0;
            foreach (var k1 in dict1.Keys)
            {
                if (dict2.ContainsKey(k1))
                    res += dict1[k1] * dict2[k1];
            }

            return res;
        }


        ///2363. Merge Similar Items

        */
    }
}