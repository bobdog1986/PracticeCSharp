using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy
{
    public partial class Easy
    {
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

        ///2331. Evaluate Boolean Binary Tree
        //Leaf nodes have either the value 0 or 1, where 0 represents False and 1 represents True.
        //Non-leaf nodes have either the value 2 or 3, where 2 represents the boolean OR and 3 represents the boolean AND
        public bool EvaluateTree(TreeNode root)
        {
            if (root.val == 0)
                return false;
            else if (root.val == 1)
                return true;
            else if (root.val == 2)
                return EvaluateTree(root.left) || EvaluateTree(root.right);
            else
                return EvaluateTree(root.left) && EvaluateTree(root.right);
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
    }
}
