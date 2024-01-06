using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2957. Remove Adjacent Almost-Equal Characters
        //public int RemoveAlmostEqualCharacters(string word)
        //{
        //    int res = 0;
        //    for (int i = 1; i<word.Length; i++)
        //    {
        //        if (word[i]-word[i-1]<=1 &&word[i]-word[i-1]>=-1)
        //        {
        //            res++;
        //            i++;
        //        }
        //    }
        //    return res;
        //}

        ///2958. Length of Longest Subarray With at Most K Frequency, #Two Pointers
        //public int MaxSubarrayLength(int[] nums, int k)
        //{
        //    int res = 0;
        //    int left = 0;
        //    var dict = new Dictionary<int, int>();
        //    for (int i = 0; i<nums.Length; i++)
        //    {
        //        if (dict.ContainsKey(nums[i])) dict[nums[i]]++;
        //        else dict.Add(nums[i], 1);
        //        while (left<=i&&dict[nums[i]]>k)
        //        {
        //            dict[nums[left]]--;
        //            left++;
        //        }
        //        res = Math.Max(res, i-left+1);
        //    }

        //    return res;
        //}

        ///2961. Double Modular Exponentiation
        //public IList<int> GetGoodIndices(int[][] variables, int target)
        //{
        //    var res = new List<int>();
        //    for (int i = 0; i<variables.Length; i++)
        //    {
        //        int val = 1;
        //        while (variables[i][1]>0)
        //        {
        //            val = (val * variables[i][0])%10;
        //            variables[i][1]--;
        //        }
        //        int ans = 1;
        //        while (variables[i][2]>0)
        //        {
        //            ans = (ans * val)%variables[i][3];
        //            variables[i][2]--;
        //        }
        //        if (ans == target)
        //            res.Add(i);
        //    }
        //    return res;
        //}

        ///2962. Count Subarrays Where Max Element Appears at Least K Times, #Sliding Window
        //Return the number of subarrays where the maximum element of nums appears at least k times in that subarray.
        //A subarray is a contiguous sequence of elements within an array.
        public long CountSubarrays(int[] nums, int k)
        {
            long res = 0;
            int max = nums.Max();
            int n = nums.Length;
            int left = 0;
            int count = 0;
            for (int i = 0; i<n; i++)
            {
                if (nums[i]==max) count++;
                while (count>=k)
                {
                    if (nums[left]==max) count--;
                    left++;
                };
                //window [left,i] contains k-1 max elements
                //all subarrays start in [0,left-1] , end at i, match the rule, so add (left-1-0+1)
                res+= left;
            }
            return res;
        }

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
