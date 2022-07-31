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
        /// 1752. Check if Array Is Sorted and Rotated
        ///Given an array nums, return true if the array sorted in non-decreasing order, then rotated some
        ///[1,2,3,3,4],[2,3,4,1],[3,4,5,1,2]=>true, [2,1,3,4]=>false
        public bool Check_1752(int[] nums)
        {
            bool ans = true;
            bool isRotate = false;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] < nums[i - 1])
                {
                    if (isRotate)
                        return false;
                    isRotate = true;
                }
            }

            if (isRotate)
            {
                if (nums.Last() > nums.FirstOrDefault())
                {
                    ans = false;
                }
            }
            else
            {
                if (nums.Last() < nums.FirstOrDefault())
                {
                    ans = false;
                }
            }

            return ans;
        }

        ///1757. Recyclable and Low Fat Products, see sql script

        /// 1758. Minimum Changes To Make Alternating Binary String
        /// Return the minimum number of operations needed to make s alternating. 010101 or 101010
        public int MinOperations(string s)
        {
            int dp0 = 0;
            //int dp1 = 0;
            //char c0 = '0';
            //char c1 = '1';
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] - '0' != i % 2)
                    dp0++;
                //if (s[i] != c1)
                //dp1++;
                //var temp = c0;
                //c0 = c1;
                //c1 = temp;
            }
            return Math.Min(dp0, s.Length - dp0);
        }

        ///1759. Count Number of Homogenous Substrings
        ///Given a string s, return the number of homogenous substrings of s.
        ///Since the answer may be too large, return it modulo 109 + 7.
        ///A string is homogenous if all the characters of the string are the same.
        public int CountHomogenous(string s)
        {
            long ans = 0;
            long mod = 10_0000_0007;
            Dictionary<long, long> dict = new Dictionary<long, long>();
            char c = s[0];
            long count = 1;
            for (int i = 1; i < s.Length; i++)
            {
                if (c == s[i]) { count++; }
                else
                {
                    if (!dict.ContainsKey(count)) { CountHomogenous(count, dict); }
                    ans += dict[count];
                    ans %= mod;
                    c = s[i];
                    count = 1;
                }
            }
            if (!dict.ContainsKey(count)) { CountHomogenous(count, dict); }
            ans += dict[count];
            ans %= mod;
            return (int)(ans % mod);
        }

        public void CountHomogenous(long count, Dictionary<long, long> dict)
        {
            long ans = 0;
            long seed = 0;
            int i = 0;
            while (i <= count)
            {
                ans += seed;
                if (!dict.ContainsKey(i)) dict.Add(i, ans);
                i++;
                seed++;
            }
        }

        ///1760. Minimum Limit of Balls in a Bag, #Binary Search
        //Take any bag of balls and divide it into two new bags with a positive number of balls
        //penalty is max of nums, Return the minimum possible penalty after performing the operations.
        public int MinimumSize(int[] nums, int maxOperations)
        {
            //The number of operation we need is sum of (n - 1) / mid
            //If the total operation > max operations,the size of bag is too small, set left = mid + 1
            //Otherwise,this size of bag is big enough, we set right = mid
            int left = 1, right = 1_000_000_000;
            while (left < right)
            {
                int mid = (left + right) / 2;
                int count = 0;
                foreach (var n in nums)
                    count += (n - 1) / mid;

                if (count > maxOperations)
                    left = mid + 1;
                else
                    right = mid;
            }
            return left;
        }

        ///1763. Longest Nice Substring, #Divide And Conquer
        //A string s is nice if, for every letter of the alphabet that s contains,
        //it appears both in uppercase and lowercase.
        //return the earliest longest nice substring of s or empty, 1 <= s.length <= 100
        public string LongestNiceSubstring(string s)
        {
            if (s.Length < 2) return string.Empty;
            var set = new HashSet<char>(s);
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (set.Contains(char.ToUpper(c)) && set.Contains(char.ToLower(c))) continue;
                var sub1 = LongestNiceSubstring(s.Substring(0, i));
                var sub2 = LongestNiceSubstring(s.Substring(i + 1));
                return sub1.Length >= sub2.Length ? sub1 : sub2;
            }
            return s;
        }

        public string LongestNiceSubstring_BruteForce(string s)
        {
            int[] res = new int[2] { 0, 0 };//store {startIndex,len} of result
            int max = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int[][] mat = new int[26][];
                for (int j = 0; j < mat.Length; j++)
                    mat[j] = new int[2] { 0, 0 };
                for (int j = i; j < s.Length; j++)
                {
                    int index = char.IsUpper(s[j]) ? 0 : 1;
                    mat[char.ToLower(s[j]) - 'a'][index]++;
                    bool nice = !mat.Any(x => (x[0] == 0 && x[1] != 0) || (x[0] != 0 && x[1] == 0));
                    if (nice && j - i + 1 > max)
                    {
                        max = j - i + 1;
                        res = new int[] { i, j - i + 1 };
                    }
                }
            }
            return res[1] == 0 ? string.Empty : s.Substring(res[0], res[1]);
        }

        /// 1764. Form Array by Concatenating Subarrays of Another Array
        public bool CanChoose(int[][] groups, int[] nums)
        {
            int m = groups.Length;
            int r = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if (groups[r][0] == nums[i])
                {
                    int j = 0;
                    for (; j < groups[r].Length && i+j<nums.Length; j++)
                    {
                        if (groups[r][j] != nums[i + j]) break;
                    }
                    if(j == groups[r].Length)
                    {
                        i += j-1;
                        r++;
                        if (r == m) break;
                    }
                }
            }
            return r == m;
        }
        /// 1765. Map of Highest Peak, #BFS
        public int[][] HighestPeak(int[][] isWater)
        {
            int m = isWater.Length;
            int n = isWater[0].Length;
            int[][] res = new int[m][];
            for (int i = 0; i < m; i++)
            {
                res[i] = new int[n];
                Array.Fill(res[i], -1);
            }
            int[][] dxy = new int[4][] { new int[] { 0, 1 }, new int[] { 0, -1 }, new int[] { -1, 0 }, new int[] { 1, 0 }};
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (isWater[i][j] == 1)
                    {
                        list.Add(new int[] { i, j });
                        res[i][j] = 0;
                    }
                }
            }
            int height = 1;
            while (list.Count > 0)
            {
                var next = new List<int[]>();
                foreach(var p in list)
                {
                    foreach(var d in dxy)
                    {
                        var r = p[0] + d[0];
                        var c = p[1] + d[1];
                        if(r>=0&&r<m&&c>=0&&c<n && res[r][c]==-1)
                        {
                            next.Add(new int[] { r,c});
                            res[r][c] = height;
                        }
                    }
                }
                height++;
                list = next;
            }
            return res;
        }
        /// 1768. Merge Strings Alternately
        ///Merge the strings by adding letters in alternating order, starting with word1.
        public string MergeAlternately(string word1, string word2)
        {
            List<char> list = new List<char>();
            int i = 0;
            while (i < word1.Length && i < word2.Length)
            {
                list.Add(word1[i]);
                list.Add(word2[i]);
                i++;
            }

            if (i < word1.Length)
            {
                return new string(list.ToArray()) + word1.Substring(i);
            }
            else if (i < word2.Length)
            {
                return new string(list.ToArray()) + word2.Substring(i);
            }
            else
            {
                return new string(list.ToArray());
            }
        }

        ///1769. Minimum Number of Operations to Move All Balls to Each Box
        ///Operations of moving all 1 to every index
        public int[] MinOperations_1769(string boxes)
        {
            List<int> list = new List<int>();
            for(int i = 0; i < boxes.Length; i++)
                if (boxes[i] == '1') list.Add(i);

            int[] res=new int[boxes.Length];
            for(int i = 0; i < res.Length; i++)
                res[i] = list.Sum(x => Math.Abs(x - i));

            return res;
        }

        ///1773. Count Items Matching a Rule
        public int CountMatches(IList<IList<string>> items, string ruleKey, string ruleValue)
        {
            return items.Count(x =>
                                (ruleKey== "type" && ruleValue == x[0])
                            || (ruleKey == "color" && ruleValue == x[1])
                            || (ruleKey == "name" && ruleValue == x[2]));
        }
        /// 1774. Closest Dessert Cost, #PriorityQueue
        //must has 1 baseCosts, can has any types of topping,each type can select [0,2]
        //return closest to target, if same return the smaller
        public int ClosestCost(int[] baseCosts, int[] toppingCosts, int target)
        {
            PriorityQueue<int,double> pq = new PriorityQueue<int,double>();
            foreach(var bCost in baseCosts)
            {
                var set = new HashSet<int>() { bCost };
                foreach(var topping in toppingCosts)
                {
                    var next = new HashSet<int>();
                    foreach(var i in set)
                    {
                        next.Add(i);
                        next.Add(i + topping);
                        next.Add(i + topping * 2);
                    }
                    set = next;
                    foreach(var i in set)
                    {
                        if (i == target) return i;
                        else if (i < target) pq.Enqueue(i, target - i);
                        else pq.Enqueue(i,i- target+0.5);
                    }
                }
            }
            return pq.Peek();
        }
        /// 1779. Find Nearest Point That Has the Same X or Y Coordinate
        public int NearestValidPoint(int x, int y, int[][] points)
        {
            int min = int.MaxValue;
            int index = -1;
            for (int i = 0; i < points.Length; i++)
            {
                var p = points[i];
                if (p[0] == x || p[1] == y)
                {
                    var distance = Math.Abs(p[0] - x) + Math.Abs(p[1] - y);
                    if (distance >= min) continue;
                    else
                    {
                        min = distance;
                        index = i;
                    }
                }
            }
            return index;
        }

        ///1780. Check if Number is a Sum of Powers of Three
        public bool CheckPowersOfThree(int n)
        {
            int seed = 0;
            while (true)
            {
                if (Math.Pow(3, seed) < n) seed++;
                else break;
            }
            int curr = (int)Math.Pow(3, seed);
            while (n > 0 && curr>0)
            {
                if (curr <= n) n -= curr;
                curr/=3 ;
            }
            return n == 0;
        }
        /// 1784. Check if Binary String Has at Most One Segment of Ones
        public bool CheckOnesSegment(string s)
        {
            int total = s.Count(x => x == '1');
            int head = 0;
            int i = 0;
            while (i < s.Length)
            {
                if (s[i++] == '0') break;
                else head++;
            }
            return head == total;
        }
        ///1785. Minimum Elements to Add to Form a Given Sum
        public int MinElements(int[] nums, int limit, int goal)
        {
            long diff = goal - nums.Sum(x=>(long)x);
            if (diff == 0) return 0;
            if (diff < 0) diff = -diff;
            int count =(int)( diff / limit);
            if (diff % limit != 0) count++;
            return count;
        }
        /// 1790. Check if One String Swap Can Make Strings Equal
        ///Return true if make s2==s1 by performing at most one string swap(swap 2 chars in s2). Or return false.
        public bool AreAlmostEqual(string s1, string s2)
        {
            int[] arr1 = new int[26];
            int[] arr2 = new int[26];
            int diff = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i]) diff++;
                if (diff >= 3) return false;
                arr1[s1[i] - 'a']++;
                arr2[s2[i] - 'a']++;
            }
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i]) return false;
            }
            return true;
        }

        ///1791. Find Center of Star Graph
        public int FindCenter(int[][] edges)
        {
            int n = edges.Length + 1;
            int[] arr = new int[n + 1];
            foreach (var edge in edges)
            {
                if (++arr[edge[0]] == n - 1) return edge[0];
                if (++arr[edge[1]] == n - 1) return edge[1];
            }
            return -1;
        }

        ///1792. Maximum Average Pass Ratio, #PriorityQueue
        //classes[i] = [passi, totali], max ratio = (sum of all ratio)/n
        public double MaxAverageRatio(int[][] classes, int extraStudents)
        {
            int n = classes.Length;
            var pq = new PriorityQueue<int, double>();
            for (int i = 0; i < n; i++)
            {
                //delta = x+1/y+1 - x/y, max heap , equal to minHeap of x/y -x+1/y+1
                if (classes[i][0] < classes[i][1])
                    pq.Enqueue(i, 1.0 * (classes[i][0]) / (classes[i][1]) - 1.0*(classes[i][0]+1)/ (classes[i][1] + 1));
            }

            if(pq.Count==0)
                return 1;

            while (extraStudents-- > 0)
            {
                var i = pq.Dequeue();
                classes[i][0]++;
                classes[i][1]++;
                pq.Enqueue(i, 1.0 * (classes[i][0]) / (classes[i][1]) - 1.0 * (classes[i][0] + 1) / (classes[i][1] + 1));
            }
            return classes.Sum(x => 1.0 * x[0] / x[1]) / n;
        }

        ///1793. Maximum Score of a Good Subarray, #Monotonic Stack
        //The score of a subarray(i, j) is defined as min(nums[i..j]) * (j - i + 1).
        //A good subarray is a subarray where i <= k <= j.
        //Return the maximum possible score of a good subarray.
        public int MaximumScore(int[] nums, int k)
        {
            int res = 0;
            int n = nums.Length;
            int[] leftArr = initMonotonicLeftSmallerArr(nums);
            int[] rightArr = initMonotonicRightSmallerArr(nums);
            for (int i = 0; i < n; i++)
            {
                int left = leftArr[i] + 1;
                int right = rightArr[i] - 1;
                if (left <= k && right >= k)
                {
                    res = Math.Max(res, nums[i] * (right - left + 1));//current num[i] is min in [left,right]
                }
            }
            return res;
        }
        ///1796. Second Largest Digit in a String
        public int SecondHighest(string s)
        {
            var arr = s.Where(x => !char.IsLetter(x)).Select(x => x - '0').OrderBy(x => x).Distinct().ToArray();
            return arr.Length<=1?-1:arr[arr.Length-2];
        }

        /// 1797. Design Authentication Manager, see AuthenticationManager

        ///1798. Maximum Number of Consecutive Values You Can Make
        public int GetMaximumConsecutive(int[] coins)
        {
            //"Return the maximum number ... you can make with your coins starting from and including 0"
            //this equals to"Return the minimum number that you can not make .."

            Array.Sort(coins);
            int res = 1;
            foreach (var coin in coins)
            {
                if (coin > res) break;
                res += coin;
            }
            return res;
        }

    }
}