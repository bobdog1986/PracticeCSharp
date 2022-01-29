using System.Collections.Generic;
using System.Linq;
using System;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1422. Maximum Score After Splitting a String
        ///Split to 2 string,score is the number of zeros in the left + the number of ones in the right substring.
        public int MaxScore(string s)
        {
            Dictionary<int, int> dict=new Dictionary<int, int>();
            int numOf0 = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if(s[i]=='0')
                    numOf0++;

                if(i<s.Length-1)
                    dict.Add(i, numOf0);
            }
            int max = 0;
            foreach(int key in dict.Keys)
                max=Math.Max(max, dict[key]+s.Length-numOf0-(key+1- dict[key]));
            return max;
        }

        ///
    }
}