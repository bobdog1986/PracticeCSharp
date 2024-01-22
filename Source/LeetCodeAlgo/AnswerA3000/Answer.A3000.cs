using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///3002. Maximum Size of a Set After Removals
        //public int MaximumSetSize(int[] nums1, int[] nums2)
        //{
        //    int n = nums1.Length;
        //    var dict1= new Dictionary<int, int>();
        //    var dict2= new Dictionary<int, int>();
        //    for(int i = 0; i < n; i++)
        //    {
        //        if (!dict1.ContainsKey(nums1[i]))
        //            dict1.Add(nums1[i], 1);
        //        else dict1[nums1[i]]++;
        //    }
        //    for (int i = 0; i < n; i++)
        //    {
        //        if (!dict2.ContainsKey(nums2[i]))
        //            dict2.Add(nums2[i], 1);
        //        else dict2[nums2[i]]++;
        //    }

        //    int only1 = 0;
        //    int only2 = 0;
        //    foreach(var k in dict1.Keys)
        //    {
        //        if (!dict2.ContainsKey(k))
        //            only1++;
        //    }
        //    only2 = dict2.Keys.Count- (dict1.Keys.Count - only1);

        //    int res = 0;
        //    int half = n/2;
        //    if(only1 >= half)
        //    {
        //        res+=half;
        //        if (dict2.Keys.Count>=half)
        //            res+=half;
        //        else
        //            res+=dict2.Keys.Count;
        //    }
        //    else
        //    {
        //        res+=only1;
        //        if((half - only1)>=(dict1.Keys.Count-only1))
        //        {
        //            res+=dict1.Keys.Count-only1;
        //            if (dict2.Keys.Count - (dict1.Keys.Count-only1) >= half)
        //                res+=half;
        //            else
        //                res+= dict2.Keys.Count - (dict1.Keys.Count-only1);
        //        }
        //        else
        //        {
        //            res+=(half - only1);
        //            if (dict2.Keys.Count - (half - only1)>=half)
        //                res+=half;
        //            else
        //                res+=(dict2.Keys.Count-(half - only1));
        //        }
        //    }
        //    return res;

        //}


        ///3012. Minimize Length of Array Using Operations
        //public int MinimumArrayLength(int[] nums)
        //{
        //    int min = nums.Min();
        //    int count = 0;
        //    foreach (var i in nums)
        //    {
        //        if (i%min>0) return 1;
        //        if (i==min) count++;
        //    }

        //    return (count+1)/2;
        //}

        ///3016. Minimum Number of Pushes to Type Word II
        //public int MinimumPushes(string word)
        //{
        //    int[] arr = new int[26];
        //    foreach (var i in word)
        //    {
        //        arr[i-'a']++;
        //    }
        //    int res = 0;
        //    arr=arr.OrderBy(x => -x).ToArray();
        //    for (int i = 0; i<arr.Length; i++)
        //    {
        //        if (arr[i]==0) break;
        //        res+=arr[i]*(i/8+1);
        //    }
        //    return res;
        //}


    }
}
