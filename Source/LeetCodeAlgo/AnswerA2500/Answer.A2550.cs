using LeetCodeAlgo.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
	public partial class Answer
	{
        ///2550. Count Collisions of Monkeys on a Polygon
        public int MonkeyMove(int n)
        {
            //res = 2^n - 2;
            long res = 1;
            long powBase = 2;
            long mod = 1_000_000_007;
            while (n > 0)
            {
                if (n % 2 == 1)
                    res = res * powBase % mod;
                powBase = powBase * powBase % mod;
                n /= 2;
            }
            return (int)((res - 2 + mod) % mod);
        }


    }
}

