using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //344. Reverse String
        public void ReverseString(char[] s)
        {
            if (s == null || s.Length <= 1)
                return;

            int half=s.Length/2;
            char temp;
            for (int i = 0; i < half; i++)
            {
                temp = s[i];
                s[i] = s[s.Length - 1 - i];
                s[s.Length - 1 - i] = temp;
            }

        }

        //367 not pass
        public bool IsPerfectSquare(int num)
        {
            //leetcode pass anwser
            int i = 1;
            while (num > 0)
            {
                num -= i;
                i += 2;
            }
            return num == 0;
        }


    }
}
