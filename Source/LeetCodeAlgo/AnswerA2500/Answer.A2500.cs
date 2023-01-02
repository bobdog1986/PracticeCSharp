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
    }
}