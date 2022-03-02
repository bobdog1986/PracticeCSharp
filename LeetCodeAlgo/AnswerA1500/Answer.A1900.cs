using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1922. Count Good Numbers- not pass,support to 10^8, but input is 10^15
        ///even indices are even and the digits at odd indices are prime (2, 3, 5, or 7).
        public int CountGoodNumbers(long n)
        {
            long even = n / 2;

            long odd = n - even;

            var c = (power1922(5, odd)%Modulo) * (power1922(4, even) % Modulo) % Modulo;
            return (int)c;
        }

        public long Modulo = 1000000007;

        public long power1922(long seed, long pow)
        {
            if (pow == 0)
                return 1;

            if (pow == 1)
                return seed;

            var half = pow / 2;

            if (pow % 2 == 0)
            {
                return (power1922(seed, half) % Modulo) * (power1922(seed, half) % Modulo)% Modulo;
            }
            else
            {
                return (seed * (power1922(seed, half) % Modulo) * (power1922(seed, half) % Modulo)) % Modulo;
            }

        }
    }
}
