using LeetCodeAlgo.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2550. Count Collisions of Monkeys on a Polygon
        public int MonkeyMove(int n)
        {
            //res = 2^n - 2;
            long res = 1;
            long powBase = 2;
            long mod = 1_000_000_007;
            while (n > 0)
            {
                if (n % 2 == 1)
                    res = res * powBase % mod;
                powBase = powBase * powBase % mod;
                n /= 2;
            }
            return (int)((res - 2 + mod) % mod);
        }

        ///2553. Separate the Digits in an Array
        // public int[] SeparateDigits(int[] nums)
        // {
        //     List<int> res = new List<int>();
        //     foreach (var i in nums)
        //     {
        //         res.AddRange(i.ToString().Select(x => x - '0'));
        //     }
        //     return res.ToArray();
        // }

        ///2554. Maximum Number of Integers to Choose From a Range I
        // public int MaxCount(int[] banned, int n, int maxSum)
        // {
        //     int res = 0;
        //     int i = 1;
        //     var set = banned.ToHashSet();
        //     int sum = 0;
        //     while (i <= n && sum <= maxSum)
        //     {
        //         if (i + sum > maxSum) break;
        //         if (set.Contains(i))
        //         {
        //             i++;
        //             continue;
        //         }
        //         else
        //         {
        //             res++;
        //             sum += i;
        //             i++;
        //         }
        //     }
        //     return res;
        // }

        ///2558. Take Gifts From the Richest Pile
        // public long PickGifts(int[] gifts, int k)
        // {
        //     long res = 0;
        //     var pq = new PriorityQueue<int, int>();
        //     foreach (var i in gifts)
        //     {
        //         pq.Enqueue(i, -i);
        //     }
        //     while (k-- > 0)
        //     {
        //         int top = pq.Dequeue();
        //         int next = (int)Math.Sqrt(top);
        //         pq.Enqueue(next, -next);
        //     }
        //     while (pq.Count > 0)
        //     {
        //         res += pq.Dequeue();
        //     }
        //     return res;
        // }

        ///2559. Count Vowel Strings in Ranges
        // public int[] VowelStrings(string[] words, int[][] queries)
        // {
        //     int m = words.Length;
        //     int[] prefix = new int[m];
        //     HashSet<char> set = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
        //     for (int i = 0; i < m; i++)
        //     {
        //         if (i > 0)
        //             prefix[i] = prefix[i - 1];
        //         if (set.Contains(words[i].First()) && set.Contains(words[i].Last()))
        //             prefix[i]++;
        //     }
        //     int n = queries.Length;
        //     int[] res = new int[n];
        //     for (int i = 0; i < n; i++)
        //     {
        //         int curr = prefix[queries[i][1]];
        //         if (queries[i][0] > 0)
        //             curr -= prefix[queries[i][0] - 1];
        //         res[i] = curr;
        //     }
        //     return res;
        // }

        ///2562. Find the Array Concatenation Value
        //public long FindTheArrayConcVal(int[] nums)
        //{
        //    long res = 0;
        //    int n = nums.Length;
        //    for (int i = 0; i<=n-1-i; i++)
        //    {
        //        if (i==n-1-i)
        //        {
        //            res+=nums[i];
        //        }
        //        else
        //        {
        //            res+= int.Parse($"{nums[i]}{nums[n-1-i]}");
        //        }
        //    }
        //    return res;
        //}

        ///2563. Count the Number of Fair Pairs, #Binary Search
        //Given a 0-indexed integer array nums of size n and two integers lower and upper,
        // return the number of fair pairs.
        // A pair (i, j) is fair if:
        // 0 <= i < j < n, and lower <= nums[i] + nums[j] <= upper
        public long CountFairPairs(int[] nums, int lower, int upper)
        {
            long res = 0;
            Array.Sort(nums);
            int n = nums.Length;
            for (int i = 0; i < n - 1; i++)
            {
                if (nums[i] + nums[i + 1] > upper) break;
                if (nums[i] + nums[n - 1] < lower) continue;
                int left = i + 1;
                int right = n - 1;
                while (left < right)
                {
                    int mid = (left + right) / 2;
                    if (nums[i] + nums[mid] < lower) left = mid + 1;
                    else right = mid;
                }
                int low = left;
                left = i + 1;
                right = n - 1;
                while (left < right)
                {
                    int mid = (left + right + 1) / 2;
                    if (nums[i] + nums[mid] > upper) right = mid - 1;
                    else left = mid;
                }
                int high = left;
                res += high - low + 1;
            }
            return res;
        }

        ///2564. Substring XOR Queries
        public int[][] SubstringXorQueries(string s, int[][] queries)
        {
            int n = queries.Length;
            var dict = new Dictionary<int, int[]>();
            for (int i = 0; i < s.Length; i++)
            {
                int len = 30;
                if (s[i] == '0') len++;//value must <= 10^9,if start at '0' can shift left 31 bits
                int curr = 0;
                for (int j = 0; j < len && j + i < s.Length; j++)
                {
                    curr <<= 1;
                    curr += s[i + j] - '0';
                    if (!dict.ContainsKey(curr))
                    {
                        dict.Add(curr, new int[] { i, i + j });
                    }
                    else if (dict[curr][1] - dict[curr][0] > j)//shorter than current
                    {
                        dict[curr] = new int[] { i, i + j };
                    }
                }
            }
            int[][] res = new int[n][];
            for (int i = 0; i < n; i++)
            {
                int q = queries[i][0] ^ queries[i][1];
                if (dict.ContainsKey(q)) res[i] = dict[q];
                else res[i] = new int[] { -1, -1 };
            }
            return res;
        }

        ///2566. Maximum Difference by Remapping a Digit
        //public int MinMaxDifference(int num)
        //{
        //    string s= num.ToString();
        //    int n = s.Length;
        //    Dictionary<char,List<int>> dict=new Dictionary<char, List<int>>();
        //    for(int i = 0; i<n; i++)
        //    {
        //        if (!dict.ContainsKey(s[i])) dict.Add(s[i], new List<int>());
        //        dict[s[i]].Add(i);
        //    }

        //    int max = num;
        //    for(int i = 0; i<n; i++)
        //    {
        //        if (s[i]!='9')
        //        {
        //            char[] arr1 = s.ToArray();
        //            foreach(var j in dict[s[i]])
        //            {
        //                arr1[j]='9';
        //            }
        //            max = int.Parse(new string(arr1));
        //            break;
        //        }
        //    }

        //    char[] arr2 = s.ToArray();
        //    foreach (var i in dict[s[0]])
        //    {
        //        arr2[i]='0';
        //    }
        //    int min = int.Parse(new string(arr2));
        //    return max-min;
        //}

        ///2567. Minimum Score by Changing Two Elements
        //public int MinimizeSum(int[] nums)
        //{
        //    int n = nums.Length;
        //    Array.Sort(nums);
        //    int res = int.MaxValue;
        //    res = Math.Min(res, nums[n-1-1-1]-nums[0]);//two largest
        //    res = Math.Min(res, nums[n-1-1]-nums[1]);//one largest, one smallest
        //    res = Math.Min(res, nums[n-1]-nums[2]);//two smallest
        //    return res;
        //}

        //2568. Minimum Impossible OR, #Bit, #Good
        //an integer is expressible if it can be written as the bitwise OR of some subsequence of nums.
        //Return the minimum positive non-zero integer that is not expressible from nums.
        public int MinImpossibleOR(int[] nums)
        {
            //if nums contain 1,2, then contain all less than 2^2, aka [1,3]
            //if nums contains [1,4], then contains all less than 4^2, aka [1,7]
            //so result must be 2^x, 2<<x
            var set = nums.ToHashSet();
            int i = 1;
            while (set.Contains(i))
            {
                i<<=1;
            }
            return i;
        }

        ///2570. Merge Two 2D Arrays by Summing Values
        public int[][] MergeArrays(int[][] nums1, int[][] nums2)
        {
            List<int[]> res = new List<int[]>();
            int i = 0;
            int j = 0;
            while (i<nums1.Length || j<nums2.Length)
            {
                if (i==nums1.Length)
                {
                    res.Add(nums2[j++]);
                }
                else if (j==nums2.Length)
                {
                    res.Add(nums1[i++]);
                }
                else
                {
                    if (nums1[i][0]<nums2[j][0])
                    {
                        res.Add(nums1[i++]);
                    }
                    else if (nums1[i][0]>nums2[j][0])
                    {
                        res.Add(nums2[j++]);
                    }
                    else
                    {
                        res.Add(new int[] { nums1[i][0], nums1[i][1] +nums2[j][1] });
                        i++;
                        j++;
                    }
                }
            }
            return res.ToArray();
        }

        ///2571. Minimum Operations to Reduce an Integer to 0, #Good , #Greedy
        //add or subtract 2^pow, return minimum ops
        public int MinOperations(int n)
        {
            int res = 0;
            while (n > 0)
            {
                int tail = n&3;
                //always try the best way
                if (tail == 0)
                {
                    //0bxxxx00, right shift 2 bits
                    n>>=2;
                }
                else if (tail ==1)
                {
                    //0bxxxx01, res++, right shift 2 bits
                    res++;
                    n>>=2;
                }
                else if (tail ==2)
                {
                    //0bxxxx10, right shift 1 bits
                    n>>=1;
                }
                else//==3
                {
                    //0bxxxx11
                    //after +1, 0bxxxx00, right shift 2 bits
                    n+=1;
                    res++;
                    n>>=2;
                }
            }
            return res;
        }

        ///2574. Left and Right Sum Differences
        //res[i]= abs(leftSum[i]-rightSum[i]);
        //public int[] LeftRigthDifference(int[] nums)
        //{
        //    int sum = nums.Sum();
        //    int curr = 0;
        //    int n = nums.Length;
        //    int[] res= new int[n];
        //    for(int i = 0; i<n; i++)
        //    {
        //        curr+=nums[i];
        //        res[i]=Math.Abs(curr-sum);
        //        sum-=nums[i];
        //    }
        //    return res;
        //}

        ///2575. Find the Divisibility Array of a String
        //div[i] = 1 if the numeric value of word[0,...,i] is divisible by m, or div[i] = 0 otherwise.
        //Return the divisibility array of word.
        //public int[] DivisibilityArray(string word, int m)
        //{
        //    int n=word.Length;
        //    int[] res = new int[n];
        //    long a = 0;
        //    for(int i = 0; i<n; i++)
        //    {
        //        a = a*10+(word[i]-'0');
        //        a = a % m;
        //        res[i] = a == 0?1:0;
        //    }
        //    return res;
        //}

        ///2576. Find the Maximum Number of Marked Indices
        //Pick two different unmarked indices i and j such that 2 * nums[i] <= nums[j], then mark i and j.
        //Return the maximum possible number of marked indices in nums using the above operation any number of times.
        //public int MaxNumOfMarkedIndices(int[] nums)
        //{
        //    Array.Sort(nums);
        //    int n = nums.Length;
        //    int n1 = (n+1)/2;
        //    int i = 0;
        //    int j = n1;
        //    int res = 0;
        //    while(i<n1 && j<n)
        //    {
        //        if (nums[i]*2<=nums[j])
        //        {
        //            i++;
        //            j++;
        //            res+=2;
        //        }
        //        else j++;
        //    }
        //    return res;
        //}

        ///2578. Split With Minimum Sum
        //public int SplitNum(int num)
        //{
        //    int[] digits = num.ToString().ToArray().Select(x => x-'0').OrderBy(x => x).ToArray();
        //    int a = 0;
        //    int b = 0;
        //    for (int i = 0; i<digits.Length; i++)
        //    {
        //        if (i%2==0)
        //        {
        //            a = a*10+digits[i];
        //        }
        //        else
        //        {
        //            b = b*10+digits[i];
        //        }
        //    }
        //    return a+b;
        //}

        ///2579. Count Total Number of Colored Cells
        //public long ColoredCells(int n)
        //{
        //    return 1l*n*(1+n)/2*4 - 3 - (n-1)*4l;
        //}

        ///2580. Count Ways to Group Overlapping Ranges
        //Any two overlapping ranges must belong to the same group.
        //assign ranges[i] to any of 2 group
        //Return the total number of ways to split ranges into two groups. return it modulo 109 + 7.
        //public int CountWays(int[][] ranges)
        //{
        //    ranges = ranges.OrderBy(x => x[0]).ThenBy(x => -x[1]).ToArray();
        //    //find how many groups
        //    int groups = 1;
        //    int end = ranges[0][1];
        //    for (int i = 1; i<ranges.Length; i++)
        //    {
        //        int[] curr = ranges[i];
        //        if (curr[0]>end)
        //        {
        //            groups++;
        //            end = curr[1];
        //        }
        //        else
        //        {
        //            end = Math.Max(end, curr[1]);
        //        }
        //    }
        //    long res = 1;
        //    long mod = 1_000_000_007;
        //    while (groups-->0)
        //    {
        //        res<<=1;
        //        res%=mod;
        //    }
        //    return (int)res;
        //}

        ///2581. Count Number of Possible Root Nodes, #DFS, #Memo
        //how many nodes can used as root, so at least k in guesses are correct!
        //using edges to build an undirected graph
        //using guesses to build a directed graph
        //For each node i, using it as root, then traverse the graph to find all correct guesses, if >=k, then i is a possible one
        //Must avoid duplicate traversal, we using memo to cache the result of [parent-node, child-node]
        //so we only need to traverse the whole graph(all edges) twice.
        public int RootCount(int[][] edges, int[][] guesses, int k)
        {
            int res = 0;
            int n = edges.Length+1;
            //build undirect graph by edges
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i<n; i++)
                graph[i]=new List<int>();

            foreach (var e in edges)
            {
                graph[e[0]].Add(e[1]);
                graph[e[1]].Add(e[0]);
            }

            //build direct graph by guesses
            HashSet<int>[] directGraph = new HashSet<int>[n];
            for (int i = 0; i<n; i++)
                directGraph[i]=new HashSet<int>();

            foreach (var e in guesses)
            {
                directGraph[e[0]].Add(e[1]);
            }

            //cache {parent, {child, count}}, count is all valid guesses traverse from parent to all childs

            var memo = new Dictionary<int, Dictionary<int, int>>();

            for (int i = 0; i<n; i++)
            {
                int count = RootCount(i, -1, graph, directGraph, memo);
                if (count>=k)
                    res++;
            }

            return res;
        }

        private int RootCount(int curr, int parent, List<int>[] graph, HashSet<int>[] set, Dictionary<int, Dictionary<int, int>> memo)
        {
            //already traversed from
            if (!memo.ContainsKey(parent))
                memo.Add(parent, new Dictionary<int, int>());

            if (memo[parent].ContainsKey(curr))
                return memo[parent][curr];
            else
            {
                int res = 0;
                foreach (var next in graph[curr])
                {
                    if (next == parent) continue;
                    if (set[curr].Contains(next)) res++;//{curr,next} is existed in guesses
                    res+=RootCount(next, curr, graph, set, memo);//DFS
                }
                memo[parent].Add(curr, res);
                return memo[parent][curr];
            }
        }

        ///2582. Pass the Pillow
        //public int PassThePillow(int n, int time)
        //{
        //    int a = time % (n-1);
        //    int b = time / (n-1);
        //    if(b % 2==0)
        //    {
        //        return a+1;
        //    }
        //    else
        //    {
        //        return n-a;
        //    }
        //}

        ///2583. Kth Largest Sum in a Binary Tree
        //public long KthLargestLevelSum(TreeNode root, int k)
        //{
        //    var dict = new Dictionary<int, long>();
        //    KthLargestLevelSum(root, 0, dict);
        //    if (dict.Keys.Count<k) return -1;

        //    var x = dict.Keys.OrderBy(x => -dict[x]).Skip(k-1).First();
        //    return dict[x];
        //}

        //private void KthLargestLevelSum(TreeNode root, int level, Dictionary<int, long> dict)
        //{
        //    if (root == null) return;
        //    if(!dict.ContainsKey(level))
        //        dict.Add(level, 0);
        //    dict[level]+=root.val;
        //    KthLargestLevelSum(root.left, level+1, dict);
        //    KthLargestLevelSum(root.right, level+1, dict);
        //}

        ///2585. Number of Ways to Earn Points, #DP
        //types[i] = [counti, marksi] indicates that there are counti questions of the ith type, and each one of them is worth marksi points.
        //Return the number of ways you can earn exactly target points in the exam.modulo 109 + 7.
        public int WaysToReachTarget(int target, int[][] types)
        {
            int n = types.Length;
            int[][] dp = new int[n+1][];
            for (int i = 0; i<dp.Length; i++)
                dp[i]=new int[target+1];

            dp[0][0]=1;
            int mod = 1_000_000_007;
            for (int i = 1; i<=n; i++)
            {
                int[] curr = types[i-1];
                for (int j = 0; j<=target; j++)
                {
                    dp[i][j] = (dp[i][j]+dp[i-1][j])% mod;
                    for (int k = 1; k<=curr[0] && j+k*curr[1]<=target; k++)
                    {
                        dp[i][j+k*curr[1]] = (dp[i][j+k*curr[1]]+dp[i-1][j])% mod;
                    }
                }
            }
            return dp[n][target];
        }

        ///2586. Count the Number of Vowel Strings in Range
        //public int VowelStrings(string[] words, int left, int right)
        //{
        //    int res = 0;
        //    var set = new HashSet<char>() {'a','e','i','o','u' };
        //    for(int i = left; i<=right; i++)
        //    {
        //        if (set.Contains(words[i].First())&&set.Contains(words[i].Last()))
        //            res++;
        //    }
        //    return res;
        //}

        ///2587. Rearrange Array to Maximize Prefix Score
        public int MaxScore(int[] nums)
        {
            nums = nums.OrderBy(x => -x).ToArray();
            int n = nums.Length;
            int res = 0;
            long sum = 0;
            for (int i = 0; i<n; i++)
            {
                sum+=nums[i];
                if (sum>0) res++;
                else break;
            }
            return res;
        }

        ///2588. Count the Number of Beautiful Subarrays,#HashMap
        //public long BeautifulSubarrays(int[] nums)
        //{
        //    long res = 0;
        //    int n = nums.Length;
        //    int curr = 0;
        //    var dict = new Dictionary<int, int>();
        //    dict.Add(0, 1);
        //    for(int i = 0; i<n; i++)
        //    {
        //        curr^=nums[i];
        //        if (dict.ContainsKey(curr))
        //        {
        //            res+=dict[curr];
        //            dict[curr]++;
        //        }
        //        else dict.Add(curr, 1);
        //    }
        //    return res;
        //}

        ///2591. Distribute Money to Maximum Children
        //- All money must be distributed.
        //- Everyone must receive at least 1 dollar.
        //- Nobody receives 4 dollars.
        //Return the maximum number of children who may receive exactly 8 dollars.
        //If there is no way to distribute the money, return -1.
        public int DistMoney(int money, int children)
        {
            if (money<children) return -1;
            int res = 0;
            while (children>0)
            {
                if (children ==1)
                {
                    if (money == 8) res++;
                    else if (money==4) res--;
                    break;
                }
                else
                {
                    if (money-8>=children-1)
                    {
                        money-=8;
                        children--;
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

        ///2592. Maximize Greatness of an Array, #Two Pointer
        //public int MaximizeGreatness(int[] nums)
        //{
        //    int n = nums.Length;
        //    nums = nums.OrderBy(x=>x).ToArray();
        //    int res = 0;
        //    int j = 0;
        //    for(int i=0;i<n; i++)
        //    {
        //        if (nums[i]>nums[j])
        //        {
        //            res++;
        //            j++;
        //        }
        //    }
        //    return res;
        //}

        ///2593. Find Score of an Array After Marking All Elements, #PriorityQueue
        //public long FindScore(int[] nums)
        //{
        //    long res = 0;
        //    var pq = new PriorityQueue<int[], int[]>(Comparer<int[]>.Create((x, y) =>
        //    {
        //        if (x[0]==y[0]) return x[1]-y[1];
        //        return x[0]-y[0];
        //    }));
        //    int n = nums.Length;
        //    bool[] marked = new bool[n];
        //    for(int i=0;i<n; i++)
        //    {
        //        var curr = new int[] { nums[i], i };
        //        pq.Enqueue(curr, curr);
        //    }

        //    while (pq.Count>0)
        //    {
        //        var top = pq.Dequeue();
        //        if (marked[top[1]]) continue;
        //        marked[top[1]]=true;
        //        res+=top[0];
        //        if (top[1]-1>=0) marked[top[1]-1]=true;
        //        if (top[1]+1<n) marked[top[1]+1]=true;
        //    }
        //    return res;
        //}

        ///2594. Minimum Time to Repair Cars, #Binary Search
        //A mechanic with a rank r can repair n cars in r * n2 minutes.
        //Return the minimum time taken to repair all the cars. All the mechanics can repair the cars simultaneously.
        public long RepairCars(int[] ranks, int cars)
        {
            long left = 1;
            long right = long.MaxValue;
            while (left<right)
            {
                long mid = left + (right-left)/2;
                long sum = 0;
                foreach (var r in ranks)
                {
                    long curr = (long)Math.Sqrt(mid/r);
                    sum+=curr;
                    if (sum>=cars) break;
                }
                if (sum>=cars)
                    right = mid;
                else
                    left = mid+1;
            }
            return left;
        }

        ///2595. Number of Even and Odd Bits
        //public int[] EvenOddBit(int n)
        //{
        //    var arr = Convert.ToString(n,2).Reverse().ToArray();
        //    int[] res = new int[2];
        //    for(int i = 0; i<arr.Length; i++)
        //    {
        //        if (i%2==0)
        //        {
        //            if (arr[i]=='1') res[0]++;
        //        }
        //        else
        //        {
        //            if (arr[i]=='1') res[1]++;
        //        }
        //    }
        //    return res;
        //}

        ///2596. Check Knight Tour Configuration
        //public bool CheckValidGrid(int[][] grid)
        //{
        //    int n = grid.Length;
        //    int[][] mat = new int[n*n][];
        //    for(int i = 0; i<n; i++)
        //    {
        //        for(int j = 0; j<n; j++)
        //        {
        //            mat[grid[i][j]] = new int[] { i, j};
        //        }
        //    }
        //    if (mat[0][0]!=0||mat[0][1]!=0) return false;
        //    int[] curr = new int[] { 0, 0 };
        //    for(int i = 1; i<mat.Length; i++)
        //    {
        //        int x = mat[i][0]-curr[0];
        //        int y = mat[i][1]-curr[1];
        //        if ((x==1&&y==2)||(x==1&&y==-2)||(x==-1&&y==2)||(x==-1&&y==-2)
        //            ||(x==2&&y==1)||(x==2&&y==-1)||(x==-2&&y==1)||(x==-2&&y==-1))
        //        {
        //            curr = mat[i];
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        ///2597. The Number of Beautiful Subsets, #Backtracking
        //A subset of nums is beautiful if it does not contain two integers with an absolute difference equal to k.
        //Return the number of non-empty beautiful subsets of the array nums.
        public int BeautifulSubsets(int[] nums, int k)
        {
            int res = 0;
            BeautifulSubsets(nums, 0, 0, k, new HashSet<int>(), ref res);
            return res;
        }

        private void BeautifulSubsets(int[] nums, int i, int count, int k, HashSet<int> set, ref int res)
        {
            int n = nums.Length;
            if (i==n)
            {
                if (count>0) res++;
            }
            else
            {
                BeautifulSubsets(nums, i+1, count, k, set, ref res);
                if (!set.Contains(nums[i]+k)&&!set.Contains(nums[i]-k))
                {
                    set.Add(nums[i]);
                    BeautifulSubsets(nums, i+1, count+1, k, set, ref res);
                    set.Remove(nums[i]);
                }
            }
        }

        ///2598. Smallest Missing Non-negative Integer After Operations
        //In one operation, you can add or subtract value from any element of nums.
        //The MEX (minimum excluded) of an array is the smallest missing non-negative integer in it.
        //Return the maximum MEX of nums after applying the mentioned operation any number of times.
        //public int FindSmallestInteger(int[] nums, int value)
        //{
        //    int[] arr = new int[value];
        //    for (int i = 0; i<nums.Length; i++)
        //    {
        //        int j = (nums[i]%value + value)%value;
        //        arr[j]++;
        //    }

        //    int min = int.MaxValue;
        //    int index = -1;
        //    for (int i = 0; i<arr.Length; i++)
        //    {
        //        if (arr[i] < min)
        //        {
        //            min= arr[i];
        //            index=i;
        //        }
        //    }
        //    return min*value + index;
        //}


    }
}