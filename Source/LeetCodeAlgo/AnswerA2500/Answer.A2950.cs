using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        /// 2974. Minimum Number Game
        //public int[] NumberGame(int[] nums)
        //{
        //    int len = nums.Length;
        //    int[] arr = new int[len];
        //    Array.Sort(nums);
        //    for (int i = 0; i<len; i+=2)
        //    {
        //        arr[i+1]=nums[i];
        //        arr[i]=nums[i+1];
        //    }
        //    return arr;
        //}

        ///2981. Find Longest Special Substring That Occurs Thrice I
        //public int MaximumLength(string s)
        //{
        //    Dictionary<char, Dictionary<int, int>> dict = new Dictionary<char, Dictionary<int, int>>();
        //    int res = 0;
        //    int len = 0;
        //    char c = ' ';
        //    for (int i = 0; i<s.Length; i++)
        //    {
        //        if (s[i]==c)
        //        {
        //            len++;
        //        }
        //        else
        //        {
        //            c=s[i];
        //            len=1;
        //        }
        //        if (!dict.ContainsKey(c))
        //            dict.Add(c, new Dictionary<int, int>());
        //        if (!dict[c].ContainsKey(len))
        //            dict[c].Add(len, 0);

        //        for (int j = 1; j<=len; j++)
        //            dict[c][j]++;
        //    }


        //    foreach (var i in dict.Values)
        //    {
        //        foreach (var k in i.Keys)
        //        {
        //            if (i[k]>=3)
        //            {
        //                res=Math.Max(res, k);
        //            }
        //        }
        //    }
        //    return res==0 ? -1 : res;
        //}
    }
}
