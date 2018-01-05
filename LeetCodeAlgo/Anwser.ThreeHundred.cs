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
            if (num < 0) return false;
            if (num <= 1) return true;

            int len = num.ToString().Length;
            
            int exp = len / 2;
            if (len % 2 == 0) { exp--; }

            int min = 1;
            while (exp > 0)
            {
                min *= 10;
                exp--;
            }

            int i= min;
            while (i>= min && i*i<=num)
            {
                if (i * i == num) return true;

                i++;
            }
            return false;
        }
    }
}
