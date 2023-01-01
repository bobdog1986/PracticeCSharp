using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.AnswerA2500
{
    public partial class Answer
    {


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


    }
}
