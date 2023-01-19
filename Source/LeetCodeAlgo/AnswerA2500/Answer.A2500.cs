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
        ///2500. Delete Greatest Value in Each Row
        //public int DeleteGreatestValue(int[][] grid)
        //{
        //    int m = grid.Length;
        //    int n = grid[0].Length;
        //    int res = 0;
        //    for (int i = 0; i<m; i++)
        //    {
        //        Array.Sort(grid[i]);
        //    }

        //    for (int j = 0; j<n; j++)
        //    {
        //        int curr = int.MinValue;
        //        for (int i = 0; i<m; i++)
        //            curr=Math.Max(curr, grid[i][j]);
        //        res+=curr;
        //    }
        //    return res;
        //}

        ///2506. Count Pairs Of Similar Strings
        public int SimilarPairs(string[] words)
        {
            int res = 0;
            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach(var w in words)
            {
                var s = string.Join("", w.ToHashSet().OrderBy(x => x).ToArray());
                if (!dict.ContainsKey(s))
                    dict.Add(s, 1);
                else
                    dict[s]++;
            }

            foreach(var k in dict.Keys)
            {
                res+=dict[k]*(dict[k]-1)/2;
            }
            return res;
        }


        ///2507. Smallest Value After Replacing With Sum of Prime Factors, #Prime
        //public int SmallestValue(int n)
        //{
        //    int res = n;
        //    bool[] arr = new bool[n+1];
        //    for(int i=2;i<=n;i++)
        //    {
        //        if (arr[i]==false)
        //        {
        //            for(int j = 2; i*j<=n; j++)
        //            {
        //                arr[j*i]=true;
        //            }
        //        }
        //    }

        //    while (arr[res])
        //    {
        //        int sum = 0;
        //        int k = res;
        //        for(int i= 2; i<=k; i++)
        //        {
        //            while (k%i==0)
        //            {
        //                sum+=i;
        //                k/=i;
        //            }
        //        }
        //        if (res==sum) break;
        //        else res=sum;
        //    }

        //    return res;
        //}

        ///2512. Reward Top K Students
        //public IList<int> TopStudents(string[] positive_feedback, string[] negative_feedback, string[] report, int[] student_id, int k)
        //{
        //    List<int> res = new List<int>();
        //    Dictionary<int, int> dict = new Dictionary<int, int>();
        //    HashSet<string> positiveSet = positive_feedback.ToHashSet();
        //    HashSet<string> negativeSet = negative_feedback.ToHashSet();

        //    for (int i = 0; i<student_id.Length;i++)
        //    {
        //        if (!dict.ContainsKey(student_id[i]))
        //            dict.Add(student_id[i], 0);

        //        int score = 0;
        //        var arr = report[i].Split(' ').ToArray();
        //        foreach(var c in arr)
        //        {
        //            if (positiveSet.Contains(c)) score+=3;
        //            if(negativeSet.Contains(c)) score-=1;
        //        }
        //        dict[student_id[i]] += score;
        //    }

        //    var keys = dict.Keys.OrderBy(x => -dict[x]).ThenBy(x => x).ToArray();
        //    for(int i = 0; i<k&&i<keys.Length; i++)
        //    {
        //        res.Add(keys[i]);
        //    }

        //    return res;
        //}

        ///2515. Shortest Distance to Target String in a Circular Array
        //public int ClosetTarget(string[] words, string target, int startIndex)
        //{
        //    List<int> list = new List<int>();
        //    for(int i=0;i<words.Length;i++)
        //    {
        //        if (words[i] == target)
        //        {
        //            list.Add(i);
        //        }
        //    }
        //    if (list.Count ==0) return -1;
        //    return list.Min(x => Math.Min(Math.Abs(x-startIndex), words.Length -Math.Abs(x-startIndex)));
        //}


        ///2517. Maximum Tastiness of Candy Basket, #Binary Search
        //The store sells baskets of k distinct candies. The tastiness of a candy basket is the smallest absolute difference
        //of the prices of any two candies in the basket.Return the maximum tastiness of a candy basket.
        //public int MaximumTastiness(int[] price, int k)
        //{
        //    Array.Sort(price);
        //    int left = 0;
        //    int right = 1_000_000_000;
        //    while (left < right)
        //    {
        //        int mid = (left + right+1) / 2;
        //        int prev = price[0];
        //        int count = 1;
        //        for (int i = 1; i<price.Length && count <k; i++)
        //        {
        //            if (price[i]-prev>=mid)
        //            {
        //                prev= price[i];
        //                count++;
        //            }
        //        }

        //        if (count>=k)
        //        {
        //            left=mid;
        //        }
        //        else
        //        {
        //            right = mid-1;
        //        }
        //    }
        //    return right;
        //}

        ///2520. Count the Digits That Divide a Number
        //public int CountDigits(int num)
        //{
        //    string s = num.ToString();
        //    return s.Count(x => num%(x-'0')==0);
        //}

        ///2521. Distinct Prime Factors of Product of Array, #Prime
        //public int DistinctPrimeFactors(int[] nums)
        //{
        //    int res = 0;
        //    HashSet<int> set = new HashSet<int>();
        //    int max = 0;
        //    foreach (var i in nums)
        //    {
        //        if (i>1)
        //        {
        //            set.Add(i);
        //            max=Math.Max(max, i);
        //        }
        //    }

        //    bool[] arr = new bool[max+1];
        //    for (int i = 2; i<=max; i++)
        //    {
        //        if (arr[i]==false)
        //        {
        //            for (int j = 2; j*i<=max; j++)
        //            {
        //                arr[j*i]=true;
        //            }
        //        }
        //    }
        //    for (int i = 2; i<=max; i++)
        //    {
        //        if (set.Count ==0) break;
        //        if (arr[i] == false)
        //        {
        //            if (set.Any(x => x>1 && x%i==0))
        //            {
        //                res++;
        //                HashSet<int> next = new HashSet<int>();
        //                foreach (var j in set)
        //                {
        //                    int k = j;
        //                    while (k>1 && k%i==0)
        //                    {
        //                        k=k/i;
        //                    }
        //                    if (k>1)
        //                        next.Add(k);
        //                }
        //                set=next;
        //            }
        //        }
        //    }
        //    return res;
        //}

        ///2522. Partition String Into Substrings With Values at Most K, #Sliding Window
        //1 <= s.length <= 10^5, s[i] is a digit from '1' to '9',1 <= k <= 10^9
        //public int MinimumPartition(string s, int k)
        //{
        //    int res = 0;
        //    long curr = s[0]-'0';

        //    for(int i = 1; i<s.Length; i++)
        //    {
        //        if (curr>k)
        //            return -1;
        //        else
        //        {
        //            if (curr*10+s[i]-'0'>k)
        //            {
        //                curr = s[i]-'0';
        //                res++;
        //            }
        //            else
        //            {
        //                curr = curr*10+s[i]-'0';
        //            }
        //        }
        //    }
        //    if (curr>k)
        //        return -1;
        //    res++;

        //    return res;
        //}

        ///2523. Closest Prime Numbers in Range, #Prime
        //public int[] ClosestPrimes(int left, int right)
        //{
        //    int[] res = new int[] { -1, -1 };
        //    if (left==right)
        //        return res;
        //    bool[] arr = new bool[right+1];
        //    for(int i = 2; i<=right; i++)
        //    {
        //        if (arr[i]) continue;
        //        for(int j = 2; j*i<=right; j++)
        //        {
        //            arr[j*i]=true;
        //        }
        //    }

        //    int diff = int.MaxValue;
        //    int prev = -1;
        //    for(int i = left; i<=right; i++)
        //    {
        //        if (i==1) continue;
        //        if (arr[i]) continue;
        //        if (prev!=-1)
        //        {
        //            if (i-prev<diff)
        //            {
        //                res= new int[] { prev, i };
        //                diff=i-prev;
        //            }
        //        }
        //        prev=i;
        //    }

        //    return res;
        //}

        ///2526. Find Consecutive Integers from a Data Stream, see DataStream2526


        ///2527. Find Xor-Beauty of Array
        //public int XorBeauty(int[] nums)
        //{
        //    return nums.Aggregate((x, y) => x^y);
        //}


        ///2529. Maximum Count of Positive Integer and Negative Integer
        //public int MaximumCount(int[] nums)
        //{
        //    int pos = 0;
        //    int neg = 0;
        //    foreach (var i in nums)
        //    {
        //        if (i > 0) pos++;
        //        if (i < 0) neg++;
        //    }
        //    return Math.Max(neg, pos);
        //}

        ///2535. Difference Between Element Sum and Digit Sum of an Array
        //public int DifferenceOfSum(int[] nums)
        //{
        //    int sum = nums.Sum();
        //    int digitSum = nums.Sum(x =>
        //    {
        //        int res = 0;
        //        while (x>0)
        //        {
        //            res+=x%10;
        //            x/=10;
        //        }
        //        return res;
        //    });
        //    return Math.Abs(sum-digitSum);
        //}

        ///2537. Count the Number of Good Subarrays, #Sliding Window
        //A subarray arr is good if it there are at least k pairs of indices (i, j) such that i < j and arr[i] == arr[j].
        //A subarray is a contiguous non-empty sequence of elements within an array.
        public long CountGood(int[] nums, int k)
        {
            long res = 0;
            int left = 0;
            int n = nums.Length;
            Dictionary<int,int> dict = new Dictionary<int,int>();
            long curr = 0;
            for(int i = 0; i<n; i++)
            {
                if (!dict.ContainsKey(nums[i]))
                    dict.Add(nums[i], 0);
                dict[nums[i]]++;
                curr+=dict[nums[i]]-1;
                while (left<=i&&curr>=k)
                {
                    long right = n-1-i+1;
                    res+=right;
                    dict[nums[left]]--;
                    curr-=dict[nums[left]];
                    left++;
                }
            }
            return res;
        }

    }
}