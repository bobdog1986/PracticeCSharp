using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
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
