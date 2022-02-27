using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///2068. Check Whether Two Strings are Almost Equivalent
        ///Given two strings word1 and word2, each of length n, return true if word1 and word2 are almost equivalent or false
        ///differences between the frequencies of each letter from 'a' to 'z' between word1 and word2 is at most 3.

        public bool CheckAlmostEquivalent(string word1, string word2)
        {
            int[] arr=new int[26];
            for(int i=0; i<word1.Length; i++)
            {
                arr[word1[i] - 'a']++;
                arr[word2[i] - 'a']--;
            }
            foreach(var x in arr)
                if (x > 3 || x < -3) return false;
            return true;
        }
        /// 2086. Minimum Number of Buckets Required to Collect Rainwater from Houses
        ///H is house, . is space
        ///The rainwater from a house at index i is collected if a bucket is placed at index i - 1 and/or index i + 1.
        ///Return the minimum number of buckets for every house at least one bucket collecting rainwater, or -1 if it is impossible.
        ///1 <= street.length <= 105
        public int MinimumBuckets(string street)
        {
            int ans = 0;
            var arr = street.ToCharArray();
            bool[] buckets=new bool[arr.Length];
            for(int i=0; i<arr.Length; i++)
            {
                //if start with 'HH...' or end with '...HH' or contain 'HHH', return -1
                if (arr[i]=='H'
                    &&(i==0 || arr[i-1]=='H')
                    &&(i==arr.Length-1 || arr[i+1]=='H'))
                    return -1;

                if (arr[i] == 'H')
                {
                    if (i >0 && buckets[i-1])
                    {
                        continue;
                    }
                    ans++;
                    if (i < arr.Length - 1)
                    {
                        buckets[i+1] = true;
                    }
                }
            }
            return ans;
        }
    }
}
