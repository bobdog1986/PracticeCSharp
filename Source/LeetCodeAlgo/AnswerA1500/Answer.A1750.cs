using System;
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
        /// 1758. Minimum Changes To Make Alternating Binary String
        /// Return the minimum number of operations needed to make s alternating. 010101 or 101010
        public int MinOperations(string s)
        {
            int dp0 = 0;
            //int dp1 = 0;
            //char c0 = '0';
            //char c1 = '1';
            for(int i=0; i<s.Length; i++)
            {
                if(s[i]-'0'!=i%2)
                    dp0 ++;
                //if (s[i] != c1)
                    //dp1++;
                //var temp = c0;
                //c0 = c1;
                //c1 = temp;
            }
            return Math.Min(dp0, s.Length-dp0);
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
                    ans+= dict[count];
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
        public void CountHomogenous(long count, Dictionary<long,long> dict)
        {
            long ans = 0;
            long seed = 0;
            int i = 0;
            while (i<=count)
            {
                ans += seed;
                if (!dict.ContainsKey(i)) dict.Add(i, ans);
                i++ ;
                seed++;
            }
        }

        ///1768. Merge Strings Alternately
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

        ///1790. Check if One String Swap Can Make Strings Equal
        ///Return true if make s2==s1 by performing at most one string swap(swap 2 chars in s2). Or return false.
        public bool AreAlmostEqual(string s1, string s2)
        {
            int[] arr1 = new int[26];
            int[] arr2 = new int[26];
            int diff = 0;
            for(int i=0; i < s1.Length; i++)
            {
                if (s1[i] != s2[i]) diff++;
                if (diff >= 3) return false;
                arr1[s1[i] - 'a']++;
                arr2[s2[i] - 'a']++;
            }
            for(int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] != arr2[i]) return false;
            }
            return true;
        }

        ///1797
    }
}
