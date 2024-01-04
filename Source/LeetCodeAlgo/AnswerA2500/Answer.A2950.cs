using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2966. Divide Array Into Arrays With Max Difference
        //public int[][] DivideArray(int[] nums, int k)
        //{
        //    Array.Sort(nums);
        //    int n = nums.Length;
        //    List<int[]> res = new List<int[]>();
        //    for (int i = 0; i<n; i+=3)
        //    {
        //        if (nums[i+2]-nums[i]>k)
        //            return new int[0][];
        //        else
        //        {
        //            res.Add(new int[] { nums[i], nums[i+1], nums[i+2] });
        //        }
        //    }
        //    return res.ToArray();
        //}

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

        ///2980. Check if Bitwise OR Has Trailing Zeros
        //public bool HasTrailingZeros(int[] nums)
        //{
        //    int evens = 0;
        //    foreach (var i in nums)
        //    {
        //        if (i%2==0) evens++;
        //        if (evens>=2) return true;
        //    }
        //    return false;
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

        ///2982. Find Longest Special Substring That Occurs Thrice II
        //public int MaximumLength(string s)
        //{
        //    var dict = new Dictionary<char, Dictionary<int, int>>();

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

        //        dict[c][len]++;
        //    }

        //    foreach (var k in dict.Keys)
        //    {
        //        int i = 0;
        //        int max = dict[k].Keys.Max();
        //        while (max>0)
        //        {
        //            i+=dict[k][max];
        //            if (i>=3)
        //            {
        //                res=Math.Max(res, max);
        //                break;
        //            }
        //            max--;
        //        }
        //    }

        //    return res==0 ? -1 : res;
        //}
    }
}
