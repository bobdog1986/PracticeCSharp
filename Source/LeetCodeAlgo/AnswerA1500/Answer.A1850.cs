using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1876. Substrings of Size Three with Distinct Characters
        ///A string is good if there are no repeated characters.
        ///Given a string s,return the number of good substrings of length three in s.
        public int CountGoodSubstrings(string s)
        {
            int res = 0;
            Dictionary<char,int> dict=new Dictionary<char, int>();
            for(int i = 0; i < s.Length-2; i++)
            {
                if (i == 0)
                {
                    dict.Add(s[0],1);
                    if(dict.ContainsKey(s[1]))dict[s[1]]++;
                    else dict.Add(s[1],1);
                }
                if (dict.ContainsKey(s[i + 2])) dict[s[i + 2]]++;
                else dict.Add(s[i + 2], 1);
                if (dict.Count == 3) res++;
                dict[s[i]]--;
                if (dict[s[i]] == 0) dict.Remove(s[i]);
            }
            return res;
        }
    }
}
