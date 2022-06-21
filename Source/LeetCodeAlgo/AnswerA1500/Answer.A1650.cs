using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1652. Defuse the Bomb
        ///Given the circular array code and an integer key k, return the decrypted code to defuse the bomb!
        public int[] Decrypt(int[] code, int k)
        {
            var ans=new int[code.Length];
            for(int i=0; i<code.Length; i++)
            {
                if (k > 0)
                {
                    for (int j = 1; j <= k; j++)
                    {
                        var index = (i + j) % code.Length;
                        ans[i] += code[index];
                    }
                }
                else if (k < 0)
                {
                    for (int j = -1; j >= k; j--)
                    {
                        var index = (i + j) % code.Length;
                        if (index < 0)
                            index += code.Length;
                        ans[i] += code[index];
                    }
                }
            }
            return ans;
        }
        ///1654. Minimum Jumps to Reach Home, #Graph, #BFS
        public int MinimumJumps(int[] forbidden, int a, int b, int x)
        {
            HashSet<int> skips=new HashSet<int>(forbidden);
            HashSet<int> visits = new HashSet<int>();
            int max = x + a + b;
            foreach(var f in forbidden)
            {
                max = Math.Max(max, f + a + b);
            }
            int res = 0;
            List<int[]> list = new List<int[]>() { new int[] {0,0 } };
            visits.Add(0);
            while (list.Count > 0)
            {
                var next = new List<int[]>();
                foreach(var i in list)
                {
                    if (i[0] == x) return res;
                    var m = i[0] + a;
                    if(m >0 && m<= max && !skips.Contains(m) && !visits.Contains(m))
                    {
                        next.Add(new int[] {m,0});
                        visits.Add(m);
                    }

                    var n = i[0] - b;
                    if(i[1]==0 && n > 0 && n <= max && !skips.Contains(n) && !visits.Contains(n))
                    {
                        next.Add(new int[] { n, 1 });
                        //visits.Add(n);
                    }
                }
                res++;
                list = next;
            }
            return -1;
        }

        ///1656. Design an Ordered Stream, see OrderedStream

        ///1657. Determine if Two Strings Are Close
        public bool CloseStrings(string word1, string word2)
        {
            if (word1.Length != word2.Length)
                return false;

            Dictionary<char, int> dict1 = new Dictionary<char, int>();
            Dictionary<char, int> dict2 = new Dictionary<char, int>();

            foreach(var c in word1)
            {
                if (dict1.ContainsKey(c)) dict1[c]++;
                else dict1.Add(c, 1);
            }

            foreach (var c in word2)
            {
                if (dict2.ContainsKey(c)) dict2[c]++;
                else dict2.Add(c, 1);
            }

            if (dict1.Count != dict2.Count)
                return false;

            foreach (var k in dict1.Keys)
            {
                if (!dict2.ContainsKey(k)) return false;
            }

            var list1 = dict1.Values.OrderBy(x => x).ToList();
            var list2 = dict2.Values.OrderBy(x => x).ToList();

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i]) return false;
            }

            return true;
        }
        /// 1658. Minimum Operations to Reduce X to Zero, #Sliding Window
        ///Return the minimum number of operations to reduce x to exactly 0 if it is possible, otherwise, return -1.
        ///This problem is equivalent to finding the longest subarray whose sum is == totalSum - x
        public int MinOperations(int[] nums, int x)
        {
            int sum = nums.Sum() - x;
            if (sum < 0) return -1;
            if (sum == 0) return nums.Length;

            int left = 0;
            int windowSum = 0;
            int len = -1;
            for (int right = 0; right < nums.Length; right++)
            {
                windowSum += nums[right];
                while (windowSum >= sum)
                {
                    if (windowSum == sum)
                        len = Math.Max(len, right - left + 1);
                    windowSum -= nums[left++];
                }
            }
            return len == -1 ? -1 : nums.Length - len;
        }

        ///1662. Check If Two String Arrays are Equivalent
        public bool ArrayStringsAreEqual(string[] word1, string[] word2)
        {
            return string.Join("", word1) == string.Join("", word2);
        }
        /// 1663. Smallest String With A Given Numeric Value
        //The numeric value of a lowercase character is defined as its position (1-indexed) in the alphabet, a is 1,etc...
        //Return the lexicographically smallest string with length equal to n and numeric value equal to k.
        public string GetSmallestString(int n, int k)
        {
            List<char> list = new List<char>();
            while (n > 0)
            {
                int c = 0;
                if (k <= (n - 1) * 26 + 1)
                {
                    c = 1;
                }
                else
                {
                    c = k - ((n - 1) * 26 + 1) +1;
                }

                list.Add((char)(c + 'a' - 1));
                k -= c;
                n--;
            }
            return new string(list.ToArray());
        }
        /// 1669. Merge In Between Linked Lists
        ///Remove list1's nodes from the ath node to the bth node, and put list2 in their place.
        public ListNode MergeInBetween(ListNode list1, int a, int b, ListNode list2)
        {
            int i = 0;
            var node = list1;
            ListNode tail1 = null;
            ListNode nextHead1 = null;
            while (node != null)
            {
                i++;
                if (i == a) { tail1 = node; }
                node = node.next;
                if (i == b)
                {
                    nextHead1 = node.next;
                    break;
                }
            }
            node = list2;
            while (node.next != null)
            {
                node = node.next;
            }
            node.next=nextHead1;
            tail1.next = list2;
            return list1;
        }

        ///1672.Richest Customer Wealth
        public int MaximumWealth(int[][] accounts)
        {
            return accounts.Select(x => x.Sum()).Max();
        }

        ///1678. Goal Parser Interpretation
        ///Given the string command, return the Goal Parser's interpretation of command.
        public string Interpret(string command)
        {
            if (string.IsNullOrEmpty(command)) return string.Empty;
            else if (command.StartsWith("G")) return "G" + Interpret(command.Substring(1));
            else if (command.StartsWith("()")) return "o" + Interpret(command.Substring(2));
            else if (command.StartsWith("(al)")) return "al" + Interpret(command.Substring(4));
            else return command;
        }
        ///1679. Max Number of K-Sum Pairs
        ///how many pairs n1+n2 = k
        public int MaxOperations(int[] nums, int k)
        {
            int res = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach(var n in nums)
            {
                if(map.ContainsKey(k-n) && map[k-n] > 0)
                {
                    map[k - n]--;
                    res++;
                }
                else
                {
                    if (map.ContainsKey(n)) map[n]++;
                    else map.Add(n, 1);
                }
            }
            return res;
        }
        /// 1684. Count the Number of Consistent Strings
        ///Return the number of consistent strings in the array words.
        public int CountConsistentStrings(string allowed, string[] words)
        {
            HashSet<char> map = new HashSet<char>();
            foreach(var c in allowed)
                if (!map.Contains(c)) map.Add(c);

            int res = 0;
            foreach(var word in words)
            {
                bool valid = true;
                foreach(var w in word)
                {
                    if (!map.Contains(w))
                    {
                        valid = false;
                        break;
                    }
                }
                if(valid) res++;
            }
            return res;
        }

        ///1685. Sum of Absolute Differences in a Sorted Array, #Prefix Sum
        //nums sorted in non-decreasing order.
        //result[i] is equal to sum(|nums[i]-nums[j]|) where 0 <= j < nums.length and j != i (0-indexed).
        public int[] GetSumAbsoluteDifferences(int[] nums)
        {
            int n = nums.Length;
            int sum = 0;
            int[] arr = new int[n];
            for(int i = 0; i < n; i++)
            {
                sum += nums[i];
                arr[i] = sum;
            }

            int[] res = new int[n];
            for(int i = 0; i < n; i++)
            {
                int head = (i+1) * nums[i] - arr[i];
                int tail = (sum - arr[i]) - (n - (i + 1)) * nums[i];
                res[i] = head + tail;
            }
            return res;
        }
        ///1688. Count of Matches in Tournament
        public int NumberOfMatches(int n)
        {
            int res = 0;
            while (n > 1)
            {
                if (n % 2 == 0)
                {
                    res += n / 2;
                    n /= 2;
                }
                else
                {
                    res += n / 2;
                    n = (n + 1) / 2;
                }
            }
            return res;
        }
        /// 1689. Partitioning Into Minimum Number Of Deci-Binary Numbers
        public int MinPartitions(string n)
        {
            int res = 0;
            foreach(var c in n)
                res = Math.Max(res, c - '0');
            return res;
        }
        /// 1694. Reformat Phone Number
        ///number consists of digits, spaces ' ', and/or dashes '-'.
        ///Firstly, remove all spaces and dashes.
        ///Then, group the digits from left to right into blocks of length 3 until there are 4 or fewer digits.
        ///The final digits are then grouped as follows:
        /// 2 digits: A single block of length 2.
        /// 3 digits: A single block of length 3.
        /// 4 digits: Two blocks of length 2 each.
        public string ReformatNumber(string number)
        {
            List<char> list = new List<char>();
            foreach (var n in number)
            {
                if (n >= '0' && n <= '9')
                {
                    list.Add(n);
                }
            }
            return ReformatNumber(list, list.Count);
        }

        private string ReformatNumber(List<char> list, int count)
        {
            if (count == 2)
            {
                return new String(new char[] { list[list.Count - 2], list[list.Count - 1] });
            }
            else if (count == 3)
            {
                return new String(new char[] { list[list.Count - 3], list[list.Count - 2], list[list.Count - 1] });
            }
            else if (count == 4)
            {
                return new String(new char[] { list[list.Count - 4], list[list.Count - 3], '-', list[list.Count - 2], list[list.Count - 1] });
            }
            else
            {
                return new String(new char[] { list[list.Count - count], list[list.Count - count + 1], list[list.Count - count + 2] })
                    + "-" + ReformatNumber(list, count - 3);
            }
        }

        ///1695. Maximum Erasure Value,#Sliding Window
        //positive integers nums and want to erase a subarray containing unique elements.
        //The score you get by erasing the subarray is equal to the sum of its elements.
        //Return the maximum score you can get by erasing exactly one subarray.
        public int MaximumUniqueSubarray(int[] nums)
        {
            int res = 0;
            int left = 0;
            var dict=new Dictionary<int,int>();
            int sum = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(nums[i]))
                {
                    while (left <= dict[nums[i]])
                        sum -= nums[left++];
                    dict[nums[i]] = i;
                }
                else dict.Add(nums[i], i);
                sum += nums[i];
                res=Math.Max(sum, res);
            }
            return res;
        }
    }
}
