using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2150. Find All Lonely Numbers in the Array
        ///You are given an integer array nums. A number x is lonely when it appears only once,
        ///and no adjacent numbers (i.e. x + 1 and x - 1) appear in the array.
        ///Return all lonely numbers in nums.You may return the answer in any order.
        ///0 <= nums[i] <= 10^6
        public IList<int> FindLonely(int[] nums)
        {
            List<int> res = new List<int>();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                if (dict.ContainsKey(n)) dict[n]++;
                else dict.Add(n, 1);
            }
            foreach (var key in dict.Keys)
            {
                if (dict[key] == 1 && !dict.ContainsKey(key - 1) && !dict.ContainsKey(key + 1))
                    res.Add(key);
            }
            return res;
        }

        ///2154. Keep Multiplying Found Values by Two
        ///If original is found in nums, multiply it by two (i.e., set original = 2 * original).
        ///Otherwise, stop the process.Repeat this process with the new number as long as you keep finding the number.
        ///Return the final value of original.
        public int FindFinalValue(int[] nums, int original)
        {
            HashSet<int> set = new HashSet<int>(nums);
            while (set.Contains(original))
                original += original;
            return original;
        }

        ///2155. All Divisions With the Highest Score of a Binary Array
        ///The division score of an index i is the sum of the number of 0's in numsleft and the number of 1's in numsright.
        ///Return all distinct indices that have the highest possible division score.
        public IList<int> MaxScoreIndices(int[] nums)
        {
            int[] arr = new int[nums.Length + 1];
            int zeros = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                arr[i] = zeros;
                if (nums[i] == 0) zeros++;
            }
            arr[nums.Length] = zeros;
            int ones = nums.Length - zeros;
            int max = 0;
            var res = new List<int>();

            for (int i = 0; i < arr.Length; i++)
            {
                int divide = arr[i] + ones - (i - arr[i]);
                if (divide == max) res.Add(i);
                else if (divide > max)
                {
                    res.Clear();
                    res.Add(i);
                    max = divide;
                }
            }

            return res;
        }

        ///2160. Minimum Sum of Four Digit Number After Splitting Digits
        ///You are given a positive integer num consisting of exactly four digits.
        ///Split num into two new integers new1 and new2 by using the digits found in num.
        ///Leading zeros are allowed in new1 and new2, and all the digits found in num must be used.
        ///Return the minimum possible sum of new1 and new2.
        public int MinimumSum(int num)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                var curr = num % 10;
                if (curr > 0)
                    list.Add(curr);
                num /= 10;
            }
            list.Sort();
            int sum1 = 0;
            int sum2 = 0;
            for (var i = 0; i < list.Count; i++)
            {
                sum1 = sum1 * 10 + list[i++];
                if (i >= list.Count)
                    break;
                sum2 = sum2 * 10 + list[i++];
            }
            return sum1 + sum2;
        }

        /// 2161. Partition Array According to Given Pivot
        ///smaller than pivot on left, same as pivot on mid, larger on right
        public int[] PivotArray(int[] nums, int pivot)
        {
            int left = 0;
            int mid = 0;
            int right = 0;
            int[] arr = new int[nums.Length];
            int[] res = new int[nums.Length];
            foreach (var n in nums)
            {
                if (n < pivot) res[left++] = n;
                else if (n == pivot) mid++;
                else arr[right++] = n;
            }
            while (mid-- > 0)
            {
                res[left++] = pivot;
            }
            int j = 0;
            while (j < right)
            {
                res[left + j] = arr[j];
                j++;
            }
            return res;
        }

        ///2162. Minimum Cost to Set Cooking Time
        public int MinCostSetTime(int startAt, int moveCost, int pushCost, int targetSeconds)
        {
            int minutes = targetSeconds / 60;
            int second = targetSeconds % 60;

            //if targetSeconds>=6000, should be 99:60 etc.
            if (minutes >= 100)
            {
                minutes--;
                second += 60;
            }

            int res = MinCostSetTime(startAt, moveCost, pushCost, minutes, second);
            //if exist another minutes:seconds combine
            if (minutes > 0 && second < 40)
                res = Math.Min(res, MinCostSetTime(startAt, moveCost, pushCost, minutes - 1, second + 60));
            return res;
        }

        private int MinCostSetTime(int startAt, int moveCost, int pushCost, int minutes, int seconds)
        {
            //if over flow ,return int.MaxValue
            if (minutes >= 100 || seconds >= 100) return int.MaxValue;
            int res = 0;
            if (minutes > 0)
            {
                if (minutes >= 10)
                {
                    if (startAt != minutes / 10) res += moveCost;
                    res += pushCost;
                    startAt = minutes / 10;
                    minutes %= 10;
                }

                if (startAt != minutes) res += moveCost;
                res += pushCost;
                startAt = minutes;

                if (startAt != seconds / 10) res += moveCost;
                res += pushCost;
                startAt = seconds / 10;
                seconds %= 10;

                if (startAt != seconds) res += moveCost;
                res += pushCost;
            }
            else
            {
                if (seconds > 0)
                {
                    if (seconds >= 10)
                    {
                        if (startAt != seconds / 10) res += moveCost;
                        res += pushCost;
                        startAt = seconds / 10;
                        seconds %= 10;
                    }
                    if (startAt != seconds) res += moveCost;
                    res += pushCost;
                }
                else
                {
                    if (startAt != seconds) res += moveCost;
                    res += pushCost;
                }
            }
            return res;
        }

        ///2164. Sort Even and Odd Indices Independently, #PriorityQueue, #Heap
        ///Sort the values at odd indices of nums in non-increasing order., Even indices in non-decreasing
        public int[] SortEvenOdd(int[] nums)
        {
            int[] res = new int[nums.Length];
            PriorityQueue<int, int> q1 = new PriorityQueue<int, int>();
            PriorityQueue<int, int> q2 = new PriorityQueue<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i % 2 == 0)
                    q2.Enqueue(nums[i], nums[i]);
                else
                    q1.Enqueue(nums[i], -nums[i]);
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (i % 2 == 0)
                    res[i] = q2.Dequeue();
                else
                    res[i] = q1.Dequeue();
            }

            return res;
        }

        /// 2165. Smallest Value of the Rearranged Number
        ///Rearrange the digits of num such that its value is minimized and it does not contain any leading zeros.
        ///Return the rearranged number with minimal value. the sign does not change after rearranging the digits.
        ///-10^15 <=x <= 10^15
        public long SmallestNumber(long num)
        {
            if (num <= 10 && num >= -10) return num;
            long res = 0;
            long sign = num < 0 ? -1 : 1;

            var str = Math.Abs(num).ToString();
            var arr = str.OrderBy(x => sign * x).ToArray();

            if (sign == 1)
            {
                int i = 0;
                while (i < arr.Length)
                {
                    if (arr[i] != '0') break;
                    i++;
                }

                var temp = arr[0];
                arr[0] = arr[i];
                arr[i] = temp;
            }
            res = long.Parse(new string(arr));
            return sign * res;
        }

        ///2169. Count Operations to Obtain Zero
        public int CountOperations(int num1, int num2)
        {
            int res = 0;
            while (num1 != 0 && num2 != 0)
            {
                if (num1 >= num2) num1 -= num2;
                else num2 -= num1;
                res++;
            }
            return res;
        }

        ///2170. Minimum Operations to Make the Array Alternating
        public int MinimumOperations(int[] nums)
        {
            Dictionary<int, int> dict1 = new Dictionary<int, int>();
            Dictionary<int, int> dict2 = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; ++i)
            {
                if (i % 2 == 0)
                {
                    if (dict1.ContainsKey(nums[i])) dict1[nums[i]]++;
                    else dict1.Add(nums[i], 1);
                }
                else
                {
                    if (dict2.ContainsKey(nums[i])) dict2[nums[i]]++;
                    else dict2.Add(nums[i], 1);
                }
            }

            int len1 = (nums.Length + 1) / 2;
            int len2 = nums.Length - len1;

            int res = int.MaxValue;
            foreach (var k1 in dict1.Keys)
            {
                foreach (var k2 in dict2.Keys)
                {
                    if (k1 == k2) continue;
                    int count = (len1 - dict1[k1]) + (len2 - dict2[k2]);
                    res = Math.Min(res, count);
                }
            }

            return res;
        }

        /// 2171. Removing Minimum Number of Magic Beans, #Prefix Sum
        public long MinimumRemoval(int[] beans)
        {
            Array.Sort(beans);
            long sum = 0;
            foreach (var bean in beans)
                sum += bean;

            long res = long.MaxValue;
            long m = beans.Length;
            for (int i = 0; i < beans.Length; i++, m--)
            {
                res = Math.Min(res, sum - m * beans[i]);
            }
            return res;
        }

        /// 2176. Count Equal and Divisible Pairs in an Array
        ///return the number of pairs (i, j) where 0 <= i < j < n, such that nums[i] == nums[j] and (i * j) is divisible by k.
        public int CountPairs(int[] nums, int k)
        {
            int res = 0;
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i])) dict.Add(nums[i], new List<int>());
                dict[nums[i]].Add(i);
            }
            foreach (var key in dict.Keys)
            {
                if (dict[key].Count >= 2)
                {
                    for (int i = 0; i < dict[key].Count - 1; i++)
                    {
                        for (int j = i + 1; j < dict[key].Count; j++)
                        {
                            if (dict[key][i] * dict[key][j] % k == 0) res++;
                        }
                    }
                }
            }
            return res;
        }

        ///2177. Find Three Consecutive Integers That Sum to a Given Number
        public long[] SumOfThree(long num)
        {
            return num % 3 == 0 ? new long[] { num / 3 - 1, num / 3, num / 3 + 1 } : new long[] { };
        }

        ///2178. Maximum Split of Positive Even Integers
        public IList<long> MaximumEvenSplit(long finalSum)
        {
            if (finalSum % 2 == 1) return new List<long>();

            var res = new HashSet<long>();
            int seed = 2;
            while (finalSum > 0)
            {
                if (finalSum >= seed)
                {
                    res.Add(seed);
                    finalSum -= seed;
                    seed += 2;
                }
                else
                {
                    res.Remove(seed - 2);
                    res.Add(finalSum + seed - 2);
                    break;
                }
            }
            return res.ToList();
        }

        /// 2180. Count Integers With Even Digit Sum, nums<=1000
        public int CountEven(int num)
        {
            ///10 = 2, 4, 6, 8(4)
            ///20 = 2, 4, 6, 8, 11, 13, 15, 17, 19, 20(10)
            ///30 = 2, 4, 6, 8, 11, 13, 15, 17, 19, 20, 22, 24, 26, and 28. (14)
            int temp = num, sum = 0;
            while (num > 0)
            {
                sum += num % 10;
                num /= 10;
            }
            return sum % 2 == 0 ? temp / 2 : (temp - 1) / 2;
        }

        ///2181. Merge Nodes in Between Zeros
        public ListNode MergeNodes(ListNode head)
        {
            if (head == null || head.next == null) return null;
            var node = head.next;
            int sum = 0;
            while (node != null && node.val != 0)
            {
                sum += node.val;
                node = node.next;
            }
            var res = new ListNode(sum);
            res.next = MergeNodes(node);
            return res;
        }

        ///2182. Construct String With Repeat Limit
        ///Return the lexicographically largest repeatLimitedString possible.
        public string RepeatLimitedString(string s, int repeatLimit)
        {
            int[] arr = new int[26];
            foreach (var c in s)
                arr[c - 'a']++;
            List<char> list = new List<char>();
            bool isStop = false;
            int maxIndex = 25;
            int minIndex = 0;
            while (!isStop)
            {
                bool existHigh = false;
                while (arr[maxIndex] == 0)
                    maxIndex--;
                while (arr[minIndex] == 0)
                    minIndex++;

                for (int i = maxIndex; i >= minIndex; i--)
                {
                    if (arr[i] == 0) continue;
                    if (existHigh)
                    {
                        list.Add((char)('a' + i));
                        arr[i]--;
                        break;
                    }
                    else
                    {
                        int j = Math.Min(repeatLimit, arr[i]);
                        arr[i] -= j;
                        while (j-- > 0)
                            list.Add((char)('a' + i));

                        existHigh = arr[i] > 0;
                        if (i == minIndex)
                        {
                            isStop = true;
                            break;
                        }
                    }
                }
                if (list.Count == s.Length)
                    isStop = true;
            }
            return new string(list.ToArray());
        }

        /// 2185. Counting Words With a Given Prefix
        public int PrefixCount(string[] words, string pref)
        {
            return words.Where(word => word.StartsWith(pref)).Count();
        }

        ///2186. Minimum Number of Steps to Make Two Strings Anagram II
        ///You are given two strings s and t. In one step, you can append any character to either s or t.
        ///Return the minimum number of steps to make s and t anagrams of each other.
        ///An anagram of a string is a string that contains the same characters with a different(or the same) ordering.
        public int MinSteps(string s, string t)
        {
            int[] arr = new int[26];
            foreach (var c in s)
                arr[c - 'a']++;
            foreach (var c in t)
                arr[c - 'a']--;
            return arr.Sum(x => Math.Abs(x));
        }

        ///2187. Minimum Time to Complete Trips, #Binary Search
        /// Return the minimum time required for all buses to complete at least totalTrips trips.
        public long MinimumTime(int[] time, int totalTrips)
        {
            long left = 1;
            long right = 100_000_000_000_000_000;
            while (left < right)
            {
                long mid = left + (right - left) / 2;
                double sum = time.Sum(x => (mid / x) * 1.0);
                if (sum >= totalTrips)
                    right = mid;
                else
                    left = mid + 1;
            }
            return left;
        }

        /// 2190. Most Frequent Number Following Key In an Array
        ///0 <= i <= n - 2, nums[i] == key and, nums[i + 1] == target.
        ///Return the target with the maximum count.
        public int MostFrequent(int[] nums, int key)
        {
            int max = 0;
            int res = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == key)
                {
                    if (map.ContainsKey(nums[i + 1])) map[nums[i + 1]]++;
                    else map.Add(nums[i + 1], 1);
                    if (map[nums[i + 1]] > max)
                    {
                        max = map[nums[i + 1]];
                        res = nums[i + 1];
                    }
                }
            }
            return res;
        }

        ///2191. Sort the Jumbled Numbers
        ///Transfer digits according to mapping array, then sort
        public int[] SortJumbled(int[] mapping, int[] nums)
        {
            return nums.OrderBy(x => SortJumbled(x, mapping)).ToArray();
        }

        public int SortJumbled(int n, int[] mapping)
        {
            if (n < 10) return mapping[n];//avoid n==0 issue
            int res = 0;
            int x = 1;
            while (n > 0)
            {
                res += mapping[n % 10] * x;
                n /= 10;
                x *= 10;
            }
            return res;
        }

        ///2192. All Ancestors of a Node in a Directed Acyclic Graph, #Graph, #DFS
        ///edges[i] = [fromi, toi] denotes that there is a unidirectional edge from fromi to toi in the graph.
        ///Return a list answer, where answer[i] is the list of ancestors of the ith node, sorted in ascending order.
        public IList<IList<int>> GetAncestors(int n, int[][] edges)
        {
            IList<int>[] res = new List<int>[n];
            bool[][] graph = new bool[n][];
            for (int i = 0; i < n; i++)
                graph[i] = new bool[n];

            foreach (var edge in edges)
                graph[edge[1]][edge[0]] = true;

            for (int i = 0; i < n; i++)
            {
                bool[] parent = new bool[n];
                GetAncestors_DFS(graph, i, parent, res);
                List<int> list = new List<int>();
                for (int j = 0; j < n; j++)
                {
                    if (parent[j])
                        list.Add(j);
                }
                res[i] = list;
            }
            return res.ToList();
        }

        private void GetAncestors_DFS(bool[][] graph, int index, bool[] parent, IList<int>[] res)
        {
            for (int i = 0; i < graph[index].Length; i++)
            {
                if (graph[index][i] && !parent[i])
                {
                    if (res[i] != null)
                    {
                        parent[i] = true;
                        foreach (var j in res[i])
                            parent[j] = true;
                    }
                    else
                    {
                        parent[i] = true;
                        GetAncestors_DFS(graph, i, parent, res);
                    }
                }
            }
        }

        ///2194. Cells in a Range on an Excel Sheet
        public IList<string> CellsInRange(string s)
        {
            var res = new List<string>();
            for (char c = s[0]; c <= s[3]; c++)
            {
                for (char i = s[1]; i <= s[4]; i++)
                {
                    res.Add($"{c}{i}");
                }
            }
            return res;
        }

        /// 2195. Append K Integers With Minimal Sum, #PriorityQueue, #Heap
        ///You are given an integer array nums and an integer k.
        ///Append k unique positive integers that do not appear in nums to nums such that the resulting total sum is minimum.
        ///Return the sum of the k integers appended to nums.
        public long MinimalKSum(int[] nums, int k)
        {
            long res = 0;
            PriorityQueue<int, int> priorityQueue = new PriorityQueue<int, int>();
            foreach (var n in nums)
                priorityQueue.Enqueue(n, n);//min heap

            int lastIndex = 0;
            while (priorityQueue.Count > 0)
            {
                int currIndex = priorityQueue.Dequeue();
                if (lastIndex == currIndex) continue;
                else
                {
                    //how many nums in range [lastIndex + 1,currIndex - 1], inclusive
                    int count = currIndex - 1 - (lastIndex + 1) + 1;
                    if (count >= k) count = k;//if exceed k, only append k nums
                    res += (lastIndex + 1 + lastIndex + count) * (long)count / 2;
                    k -= count;
                    lastIndex = currIndex;//update lastIndex
                }
            }

            if (k > 0)// if still k>0
            {
                res += (lastIndex + 1 + lastIndex + k) * (long)k / 2;
            }

            return res;
        }

        /// 2196. Create Binary Tree From Descriptions
        ///descriptions[i] = [parenti, childi, isLefti] indicates that parenti is the parent of childi in a binary tree of unique values. Furthermore,
        ///If isLefti == 1, then childi is the left child of parenti.
        ///If isLefti == 0, then childi is the right child of parenti.
        ///Construct the binary tree described by descriptions and return its root.
        public TreeNode CreateBinaryTree(int[][] descriptions)
        {
            //set contains all nodes,but only the root's value is true
            Dictionary<TreeNode, bool> set = new Dictionary<TreeNode, bool>();
            Dictionary<int, TreeNode> dict = new Dictionary<int, TreeNode>();
            foreach (var desc in descriptions)
            {
                TreeNode parent = null;
                if (!dict.ContainsKey(desc[0])) dict.Add(desc[0], new TreeNode(desc[0]));
                parent = dict[desc[0]];
                if (!set.ContainsKey(parent)) set.Add(parent, true);

                TreeNode child = null;
                if (!dict.ContainsKey(desc[1])) dict.Add(desc[1], new TreeNode(desc[1]));
                child = dict[desc[1]];
                if (!set.ContainsKey(child)) set.Add(child, false);
                else set[child] = false;

                if (desc[2] == 1) parent.left = child;
                else parent.right = child;
            }

            return set.FirstOrDefault(x => x.Value).Key;
        }
    }
}