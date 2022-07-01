using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1952. Three Divisors
        ///n has exactly 3 divisors, 1 <= n <= 10^4
        public bool IsThree(int n)
        {
            //get primes <=100, https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes
            int[] arr = new int[101];
            for(int i = 2; i*i <= 100; i++)
            {
                int j = i+i;
                while (j <= 100)
                {
                    arr[j] = 1;
                    j += i;
                }
            }
            if (n > 2)
            {
                var sqrt = (int)Math.Sqrt(n);
                if(arr[sqrt]==0 && sqrt* sqrt==n)return true;
                else return false;
            }
            else return false;
        }
        ///1957. Delete Characters to Make Fancy String
        ///A fancy string is a string where no three consecutive characters are equal.
        public string MakeFancyString(string s)
        {
            List<char> list = new List<char>();
            char c = s[0];
            int count = 1;
            list.Add(c);
            for(int i=1; i<s.Length; i++)
            {
                if(s[i] == c)
                {
                    count++;
                }
                else
                {
                    c = s[i];
                    count = 1;
                }
                if (count <= 2) list.Add(c);
            }
            return new string(list.ToArray());
        }


        /// 1961. Check If String Is a Prefix of Array
        ///Given a string s and an array of strings words, determine whether s is a prefix string of words.
        ///A string s is a prefix string of words if s can be made by concatenating the first k strings in words
        public bool IsPrefixString(string s, string[] words)
        {
            foreach(var w in words)
            {
                if (s.StartsWith(w)) s = s.Substring(w.Length);
                else return false;
                if (s.Length == 0) return true;
            }
            return false;
        }

        ///1962. Remove Stones to Minimize the Total, , #PriorityQueue
        public int MinStoneSum(int[] piles, int k)
        {
            int sum = 0;
            int total = 0;
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            foreach (var pile in piles)
            {
                pq.Enqueue(pile, -pile);
                sum += pile;
            }
            while (k-- > 0)
            {
                var curr = pq.Dequeue();
                var remove = curr / 2;
                total += remove;
                pq.Enqueue(curr - remove, -curr + remove);
            }
            return sum - total;
        }
        /// 1963. Minimum Number of Swaps to Make the String Balanced
        ///Return the minimum number of swaps to make s balanced.
        public int MinSwaps(string s)
        {
            //from middle of string, ]][[ need 1 swap
            int stack_size = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '[')
                    stack_size++;
                else
                {
                    if (stack_size > 0)
                        stack_size--;
                }
            }
            return (stack_size + 1) / 2;//both ]][[, ][ need 1 swap
        }
        ///1967. Number of Strings That Appear as Substrings in Word
        public int NumOfStrings(string[] patterns, string word)
        {
            return patterns.Where(x => word.Contains(x)).Count();
        }
        ///1968. Array With Elements Not Equal to Average of Neighbors
        public int[] RearrangeArray_1968(int[] nums)
        {
            int n = nums.Length;
            int[] res = new int[n];
            Array.Sort(nums);
            int i = 0;
            int left = 0;
            int right = n - 1;
            while(i < n)
            {
                if (i % 2 == 0)
                {
                    res[i++] = nums[left++];
                }
                else
                {
                    res[i++] = nums[right--];
                }
            }
            return res;
        }
        ///1971. Find if Path Exists in Graph,#BFS
        public bool ValidPath(int n, int[][] edges, int source, int destination)
        {
            if (source == destination) return true;
            List<int>[] graph=new List<int>[n];
            for (int i = 0; i < n; i++)
                graph[i] = new List<int>();
            foreach(var e in edges)
            {
                graph[e[0]].Add(e[1]);
                graph[e[1]].Add(e[0]);
            }

            bool[] visit = new bool[n];
            var list = new List<int>() { source};
            visit[source] = true;
            while (list.Count > 0)
            {
                var next=new List<int>();
                foreach(var i in list)
                {
                    foreach(var j in graph[i])
                    {
                        if (j == destination) return true;
                        if (visit[j]) continue;
                        visit[j] = true;
                        next.Add(j);
                    }
                }
                list = next;
            }
            return false;
        }
        /// 1974. Minimum Time to Type Word Using Special Typewriter
        ///Given a string word, return the minimum number of seconds to type out the characters in word.
        public int MinTimeToType(string word)
        {
            int res = 0;
            char c = 'a';
            for(int i=0; i<word.Length; i++)
            {
                res+= Math.Min(Math.Abs(word[i] -c), 26 - Math.Abs(word[i] - c));
                res++;
                c=word[i];
            }
            return res;
        }
        ///1975. Maximum Matrix Sum
        ///You can do any times:Choose any two adjacent elements of matrix and multiply each of them by -1.
        ///Return the maximum sum of the matrix's elements using the operation mentioned above.
        public long MaxMatrixSum(int[][] matrix)
        {
            long res = 0;
            int min = int.MaxValue;
            int count = 0;

            foreach(var m in matrix)
                foreach(var n in m)
                {
                    if (n >= 0)
                    {
                        res += n;
                        min = Math.Min(min, n);
                    }
                    else
                    {
                        res -= n;
                        min = Math.Min(min, -n);
                        count++;
                    }
                }

            return count%2==0?res:res-min;
        }
        /// 1979. Find Greatest Common Divisor of Array
        ///return the greatest common divisor of the smallest number and largest number in nums.
        ///2 <= nums.length <= 1000,1 <= nums[i] <= 1000
        public int FindGCD(int[] nums)
        {
            int max = 1;
            int min = 1000;
            foreach(var n in nums)
            {
                max=Math.Max(max, n);
                min=Math.Min(min, n);
            }
            return getGCD(max, min);
        }

        ///1980. Find Unique Binary String
        ///Given an array of strings nums containing n unique binary strings each of length n,
        ///return a binary string of length n that does not appear in nums. If there are multiple
        ///answers, you may return any of them.
        public string FindDifferentBinaryString(string[] nums)
        {
            HashSet<string> set =new HashSet<string>();

            FindDifferentBinaryString(nums[0].Length, "", set);
            HashSet<string> map = new HashSet<string>(nums);


            foreach (var n in set)
            {
                if (!map.Contains(n)) return n;
            }

            return "";
        }
        public void FindDifferentBinaryString(int count, string curr, HashSet<string> res)
        {
            if (count == 0) res.Add(curr);
            else
            {
                FindDifferentBinaryString(count - 1, curr + "0", res);
                FindDifferentBinaryString(count - 1, curr + "1", res);
            }
        }

        /// 1984. Minimum Difference Between Highest and Lowest of K Scores
        ///You are given a 0-indexed integer array nums, where nums[i] represents the score of the ith student.
        ///You are also given an integer k. Pick the scores of any k students from the array
        ///so that the difference between the highest and the lowest of the k scores is minimized.Return it;
        public int MinimumDifference(int[] nums, int k)
        {
            if (k == 1) return 0;
            Array.Sort(nums);
            int min = int.MaxValue;
            for(int i=0; i<nums.Length-k+1; i++)
            {
                min = Math.Min(min, nums[i + k - 1] - nums[i]);
            }
            return min;
        }

        ///1985. Find the Kth Largest Integer in the Array
        //1 <= k <= nums.length, 1 <= nums[i].length <= 100,nums[i] only digits withour leading 0
        public string KthLargestNumber(string[] nums, int k)
        {
            Array.Sort(nums, (x, y) =>
            {
                if (x.Length == y.Length)
                {
                    for(int i = 0; i < x.Length; i++)
                    {
                        if (x[i] > y[i]) return -1;
                        else if (x[i] < y[i]) return 1;
                    }
                    return 0;
                }
                else return y.Length - x.Length;
            });
            return nums[k - 1];
        }

        ///1991. Find the Middle Index in Array
        public int FindMiddleIndex(int[] nums)
        {
            int sum = nums.Sum();
            int curr = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if (curr == sum - curr - nums[i]) return i;
                curr += nums[i];
            }
            return -1;
        }
        /// 1992. Find All Groups of Farmland, #BFS
        ///Return a 2D array containing the 4-length arrays for each group of farmland in land(1s rectangle area.).
        public int[][] FindFarmland(int[][] land)
        {
            List<int[]> res = new List<int[]>();
            int rowLen = land.Length;
            int colLen = land[0].Length;
            int[][] dxy = new int[2][] { new int[] { 1, 0 }, new int[] { 0, 1 }};
            for (int i = 0; i < rowLen; i++)
            {
                for(int j=0; j<colLen; j++)
                {
                    if (land[i][j] == 0) continue;
                    int maxRow = i;
                    int maxCol = j;
                    int[] curr = new int[4] { i, j, maxRow, maxCol };
                    land[i][j] = 0;
                    List<int[]> list = new List<int[]>() { new int[] { i, j } };
                    while (list.Count > 0)
                    {
                        List<int[]> next = new List<int[]>();
                        foreach(var p in list)
                        {
                            foreach(var d in dxy)
                            {
                                var r = p[0] + d[0];
                                var c = p[1] + d[1];

                                if (r >= 0 && r < rowLen && c >= 0 && c < colLen && land[r][c] == 1)
                                {
                                    land[r][c] = 0;
                                    next.Add(new int[] { r, c });
                                    maxRow = Math.Max(maxRow, r);
                                    maxCol = Math.Max(maxCol, c);
                                }
                            }
                        }
                        list = next;
                    }

                    curr[2] = maxRow;
                    curr[3] = maxCol;
                    res.Add(curr);
                }
            }

            return res.ToArray();
        }

        ///1995. Count Special Quadruplets
        //nums[a] + nums[b] + nums[c] == nums[d], and a<b<c<d
        public int CountQuadruplets(int[] nums)
        {
            int res = 0;
            for(int i = 0; i < nums.Length - 3; i++)
            {
                for(int j=i+1; j < nums.Length-2; j++)
                {
                    for(int k = j + 1; k < nums.Length - 1; k++)
                    {
                        for(int m = k + 1; m < nums.Length; m++)
                        {
                            if (nums[i] + nums[j] + nums[k] == nums[m]) res++;
                        }
                    }
                }
            }
            return res;
        }

    }
}
