using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1622. Fancy Sequence, #Segment Tree
    //Implement the Fancy class:
    //Fancy() Initializes the object with an empty sequence.
    //void append(val) Appends an integer val to the end of the sequence.
    //void addAll(inc) Increments all existing values in the sequence by an integer inc.
    //void multAll(m) Multiplies all existing values in the sequence by an integer m.
    //int getIndex(idx) Gets the current value at index idx (0-indexed) of the sequence modulo 109 + 7.
    //If the index is greater or equal than the length of the sequence, return -1.
    public class Fancy
    {
        private int mod = 1_000_000_007;
        private List<int> num = new List<int>();
        private List<long> mul = new List<long>();
        private List<long> sum = new List<long>();
        public Fancy()
        {
            mul.Add(1);
            sum.Add(0);
        }

        public void Append(int val)
        {
            num.Add(val);
            mul.Add(mul.Last());
            sum.Add(sum.Last());
        }

        public void AddAll(int inc)
        {
            sum[sum.Count - 1] = (sum.Last() + inc) % mod;
        }

        public void MultAll(int m)
        {
            mul[mul.Count - 1] = mul.Last() * m % mod;
            sum[sum.Count - 1] = sum.Last() * m % mod;
        }

        public int GetIndex(int idx)
        {
            if (idx >= num.Count)
                return -1;

            //Given Fermat Little Theorem
            //   1 ≡ a^(m-1) (mod m)
            //=> a^-1 ≡ a^(m-2) (mod m)
            //Let a = mul[idx], m = mod97
            //So mul.Last()/mul[idx]
            //=> mul.Last()*mul[idx]^-1
            //=> mul.Last()*mul[idx]^(mod-2)
            //=> mul.Last() * PowMod(mul[idx], mod97 - 2, mod97)
            long m = mul.Last() * PowMod(mul[idx], mod - 2, mod) % mod;
            long inc = sum.Last() + mod - sum[idx] * m % mod;
            return (int)((num[idx] * m + inc) % mod);
        }

        private long PowMod(long x, long y, long mod)
        {
            long res = 1;
            while (y > 0)
            {
                if ((y & 1) == 1)
                    res = res * x % mod;
                x = x * x % mod;
                y >>= 1;
            }

            return res;
        }

    }
}
