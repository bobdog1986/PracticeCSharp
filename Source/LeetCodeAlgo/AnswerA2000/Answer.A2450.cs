using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2451. Odd String Difference
        //public string OddString(string[] words)
        //{
        //    Dictionary<string,List<string>> dict = new Dictionary<string, List<string>>();

        //    foreach(var w in words)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        for(int i = 0; i<w.Length-1; i++)
        //        {
        //            sb.Append($"{w[i+1]-w[i]},");
        //        }
        //        var key = sb.ToString();
        //        if (dict.ContainsKey(key))
        //            dict[key].Add(w);
        //        else
        //            dict.Add(key, new List<string>() { w });
        //    }

        //    foreach(var k in dict.Values)
        //    {
        //        if(k.Count==1)return k[0];
        //    }

        //    return null;
        //}

        ///2452. Words Within Two Edits of Dictionary

        //public IList<string> TwoEditWords(string[] queries, string[] dictionary)
        //{
        //    var res= new List<string>();
        //    var set = new HashSet<string>();
        //    var arr = dictionary.ToHashSet();
        //    foreach(var q in queries)
        //    {
        //        if(set.Contains(q))
        //        {
        //            res.Add(q);
        //        }
        //        else
        //        {
        //            if (arr.Any(x =>
        //                    {
        //                        if (x==q) return true;
        //                        int diff = 0;
        //                        for(int i = 0; i<q.Length; i++)
        //                        {
        //                            if (q[i]!=x[i]) diff++;
        //                        }
        //                        return diff<=2;
        //                    }))
        //            {
        //                res.Add(q);
        //                set.Add(q);
        //            }
        //        }
        //    }
        //    return res;
        //}

        ///2455. Average Value of Even Numbers That Are Divisible by Three
        //public int AverageValue(int[] nums)
        //{
        //    var arr = nums.Where(x => x%6==0).ToArray();
        //    if (arr.Length==0) return 0;
        //    else return arr.Sum()/arr.Length;
        //}

        ///2456. Most Popular Video Creator
        //public IList<IList<string>> MostPopularCreator(string[] creators, string[] ids, int[] views)
        //{
        //    var res = new List<IList<string>>();
        //    int max = -1;

        //    var dict = new Dictionary<string, Dictionary<string, int>>();
        //    var map = new Dictionary<string, int>();

        //    var set = new HashSet<string>();

        //    for (int i=0;i<creators.Length;i++)
        //    {
        //        //if (views[i]==0) continue;

        //        if (!dict.ContainsKey(creators[i]))
        //            dict.Add(creators[i], new Dictionary<string, int>());
        //        if (!dict[creators[i]].ContainsKey(ids[i]))
        //            dict[creators[i]].Add(ids[i], 0);
        //        dict[creators[i]][ids[i]]+=views[i];

        //        if (!map.ContainsKey(creators[i]))
        //            map.Add(creators[i], 0);
        //        map[creators[i]]+=views[i];

        //        if (map[creators[i]]>max)
        //        {
        //            set=new HashSet<string>() { creators[i] };
        //            max=map[creators[i]];
        //        }
        //        else if(map[creators[i]] == max)
        //        {
        //            set.Add(creators[i]);
        //        }
        //    }

        //    foreach(var i in set)
        //    {
        //        var a = dict[i].Keys.OrderBy(x => -dict[i][x]).ThenBy(x=>x).First();
        //        res.Add(new List<string>() { i, a });
        //    }

        //    return res;
        //}

        ///2460. Apply Operations to an Array
        //public int[] ApplyOperations(int[] nums)
        //{
        //    int n=nums.Length;
        //    int[] res= new int[n];
        //    int index = 0;
        //    for(int i=0;i< n-1;i++)
        //    {
        //        if (nums[i]==nums[i+1] && nums[i]!=0)
        //        {
        //            res[index++]=nums[i]*2;
        //            nums[i+1]=0;
        //        }
        //        else
        //        {
        //            if (nums[i]!=0)
        //                res[index++]=nums[i];
        //        }
        //    }

        //    if (nums[n-1]!=0)
        //        res[index++] = nums[n-1];

        //    return res;
        //}

        ///2461. Maximum Sum of Distinct Subarrays With Length K, #Sliding Window
        public long MaximumSubarraySum(int[] nums, int k)
        {
            long res = 0;
            int n=nums.Length;
            var dict=new Dictionary<int, int>();
            long sum = 0;
            for(int i=0;i<n;i++)
            {
                if (dict.ContainsKey(nums[i]))
                {
                    int start = i-dict.Count;
                    int end = dict[nums[i]];
                    while (start<= end)
                    {
                        dict.Remove(nums[start]);
                        sum-=nums[start];
                        start++;
                    }
                }
                dict.Add(nums[i], i);
                sum+=nums[i];
                if (dict.Count==k+1)
                {
                    dict.Remove(nums[i-k]);
                    sum-=nums[i-k];
                }

                if (dict.Count==k)
                    res=Math.Max(res, sum);
            }

            return res;
        }

        ///2465. Number of Distinct Averages
        //public int DistinctAverages(int[] nums)
        //{
        //    Array.Sort(nums);
        //    int n=nums.Length;
        //    HashSet<int> set= new HashSet<int>();
        //    for(int i=0;i<n/2;i++)
        //    {
        //        set.Add(nums[i]+nums[n-1-i]);
        //    }
        //    return set.Count;
        //}

        ///2466. Count Ways To Build Good Strings, #DP
        public int CountGoodStrings(int low, int high, int zero, int one)
        {
            int[] dp = new int[high + 1];
            int res = 0, mod = 1000000007;
            dp[0] = 1;
            for (int i = 1; i <= high; i++)
            {
                if (i >= zero)
                    dp[i] = (dp[i] + dp[i - zero]) % mod;
                if (i >= one)
                    dp[i] = (dp[i] + dp[i - one]) % mod;
                if (i >= low)
                    res = (res + dp[i]) % mod;
            }
            return res;
        }

        ///2469. Convert the Temperature
        //Kelvin = Celsius + 273.15
        //Fahrenheit = Celsius* 1.80 + 32.00
        //public double[] ConvertTemperature(double celsius)
        //{
        //    return new double[] { celsius+273.15, celsius*1.8+32 };
        //}

        ///2471. Minimum Number of Operations to Sort a Binary Tree by Level
        //public int MinimumOperations(TreeNode root)
        //{
        //    int res = 0;
        //    List<List<int>> matrix = new List<List<int>>();
        //    Queue<TreeNode> queue = new Queue<TreeNode>();
        //    queue.Enqueue(root);
        //    while(queue.Count > 0)
        //    {
        //        int size = queue.Count;
        //        var list =new List<int>();
        //        while(size-- >0)
        //        {
        //            var curr= queue.Dequeue();
        //            list.Add(curr.val);
        //            if(curr.left!=null)queue.Enqueue(curr.left);
        //            if(curr.right!=null)queue.Enqueue(curr.right);
        //        }
        //        matrix.Add(list);
        //    }

        //    foreach(var line in matrix)
        //    {
        //        var arr = line.OrderBy(x => x).ToList();
        //        int diff = 0;
        //        for(int i=0;i<arr.Count;i++)
        //        {
        //            if (arr[i]!=line[i])
        //            {
        //                diff++;
        //                int index= line.IndexOf(arr[i]);
        //                line[index] = line[i];
        //                line[i]=arr[i];
        //            }
        //        }
        //        res+= diff;
        //    }
        //    return res;
        //}

        ///2475. Number of Unequal Triplets in Array
        //public int UnequalTriplets(int[] nums)
        //{
        //    int res = 0;
        //    int n = nums.Length;
        //    for(int i = 0; i<n; i++)
        //    {
        //        for(int j = i+1; j<n; j++)
        //        {
        //            if (nums[i]==nums[j]) continue;
        //            for(int k = j+1; k<n; k++)
        //            {
        //                if (nums[i]!=nums[k] && nums[j]!=nums[k])
        //                    res++;
        //            }
        //        }
        //    }
        //    return res;
        //}

        ///2481. Minimum Cuts to Divide a Circle
        //public int NumberOfCuts(int n)
        //{
        //    if (n==1) return 0;
        //    if (n%2==0) return n/2;
        //    return n;
        //}

        ///2482. Difference Between Ones and Zeros in Row and Column
        //public int[][] OnesMinusZeros(int[][] grid)
        //{
        //    int m = grid.Length;
        //    int n= grid[0].Length;

        //    int[] onesRow = grid.Select(x => x.Count(o => o==1)).ToArray();
        //    int[] zerosRow = grid.Select(x => x.Count(o => o==0)).ToArray();

        //    int[] onesCol = new int[n];
        //    int[] zerosCol = new int[n];

        //    for(int j = 0; j<n; j++)
        //    {
        //        int ones = 0;
        //        int zeros = 0;
        //        for (int i = 0; i<m; i++)
        //        {
        //            if (grid[i][j]==0) zeros++;
        //            if (grid[i][j]==1) ones++;
        //        }
        //        onesCol[j]=ones;
        //        zerosCol[j]=zeros;
        //    }
        //    int[][] res = new int[m][];
        //    for (int i = 0; i<m; i++)
        //    {
        //        res[i]=new int[n];
        //        for(int j=0;j<n;j++)
        //            res[i][j]= onesRow[i]+onesCol[j]- zerosRow[i]-zerosCol[j];
        //    }
        //    return res;
        //}

        ///2483. Minimum Penalty for a Shop
        //public int BestClosingTime(string customers)
        //{
        //    int min = int.MaxValue;
        //    int res = -1;
        //    int n = customers.Length;
        //    int[] noArr = new int[n+1];
        //    int[] yesArr = new int[n+1];

        //    int noCount = 0;
        //    for(int i=0; i <= n; i++)
        //    {
        //        noArr[i] = noCount;
        //        if (i<n && customers[i]=='N')
        //            noCount++;
        //    }

        //    int yesCount = 0;
        //    for (int i = n; i >=0; i--)
        //    {
        //        if (i<n && customers[i]=='Y')
        //            yesCount++;
        //        yesArr[i] = yesCount;
        //    }

        //    for(int i=0;i<=n; i++)
        //    {
        //        if (yesArr[i]+noArr[i]<min)
        //        {
        //            min=yesArr[i]+noArr[i];
        //            res=i;
        //        }
        //    }
        //    return res;
        //}

        ///2485. Find the Pivot Integer
        //return x that sum of [1,x] == [x,n], if not exist, return -1
        //public int PivotInteger(int n)
        //{
        //    int left = 1;
        //    int right = n;
        //    while (left<=right)
        //    {
        //        int mid = (left+right)/2;
        //        int x = (1+mid)*mid/2;
        //        int y = (mid+n)*(n-mid+1)/2;
        //        if (x==y) return mid;
        //        else if (x<y)
        //        {
        //            left = mid+1;
        //        }
        //        else
        //        {
        //            right= mid-1;
        //        }
        //    }
        //    return -1;
        //}

        ///2486. Append Characters to String to Make Subsequence
        //Return the minimum chars that need to be appended to the end of s so that t becomes a subsequence of s.
        //public int AppendCharacters(string s, string t)
        //{
        //    int i = 0, j = 0;
        //    for(;i<s.Length && j<t.Length;)
        //    {
        //        if (s[i]==t[j])
        //        {
        //            i++;
        //            j++;
        //        }
        //        else
        //        {
        //            i++;
        //        }
        //    }
        //    return t.Length-j;
        //}

        ///2487. Remove Nodes From Linked List
        //Remove every node which has a node with a strictly greater value anywhere to the right side of it.
        public ListNode RemoveNodes_2487(ListNode head)
        {
            if (head==null||head.next==null)
                return head;
            ListNode[] arr = new ListNode[100000];
            int i = -1;
            while (head!=null)
            {
                if (i>=0)
                {
                    if (head.val>arr[i].val)
                    {
                        arr[i]=null;
                        i--;
                    }
                    else
                    {
                        arr[++i] = head;
                        head= head.next;
                    }
                }
                else
                {
                    arr[++i] = head;
                    head = head.next;
                }
            }

            var res = arr[0];
            for (int j = 0; j<=i; j++)
            {
                arr[j].next = arr[j+1];
            }
            arr[i].next=null;
            return res;
        }

        public ListNode RemoveNodes_2487_Recurr(ListNode head)
        {
            if (head==null) return null;
            var next = RemoveNodes_2487_Recurr(head.next);
            if (next == null) return head;
            else
            {
                if (next.val>head.val) return next;
                else
                {
                    head.next = next;
                    return head;
                }
            }
        }

        ///2490. Circular Sentence
        //public bool IsCircularSentence(string sentence)
        //{
        //    var arr = sentence.Split(' ').ToArray();
        //    for (int i = 0; i<arr.Length; i++)
        //    {
        //        int j = (i==arr.Length-1) ? 0 : i+1;
        //        if (arr[i].Last()!=arr[j].First())
        //            return false;
        //    }
        //    return true;
        //}

        ///2491. Divide Players Into Teams of Equal Skill
        //public long DividePlayers(int[] skill)
        //{
        //    Array.Sort(skill);
        //    int n = skill.Length;
        //    long res = 0;
        //    int sum = skill[0] + skill[n-1];
        //    for (int i = 0; i<n/2; i++)
        //    {
        //        if (skill[i] + skill[n-1-i] != sum)
        //            return -1;
        //        res+= skill[i] * skill[n-1-i];
        //    }
        //    return res;
        //}

        ///2492. Minimum Score of a Path Between Two Cities
        ///#TODO, timeout
        public int MinScore(int n, int[][] roads)
        {
            List<int[]>[] graph = new List<int[]>[n+1];
            for (int i = 0; i<graph.Length; i++)
                graph[i]=new List<int[]>();

            foreach (var r in roads)
            {
                graph[r[0]].Add(new int[] { r[1], r[2] });
                graph[r[1]].Add(new int[] { r[0], r[2] });
            }

            int res = int.MaxValue;

            return res;
        }
    }
}