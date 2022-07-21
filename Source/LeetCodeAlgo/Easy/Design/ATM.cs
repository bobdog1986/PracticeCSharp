using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///2241. Design an ATM Machine

    public class ATM
    {
        private readonly long[] arr;
        private readonly int[] coin;

        public ATM()
        {
            arr = new long[5];
            coin = new int[5] { 20, 50, 100, 200, 500 };
        }

        public void Deposit(int[] banknotesCount)
        {
            for (int i = 0; i < banknotesCount.Length; i++)
                arr[i] += banknotesCount[i];
        }

        public int[] Withdraw(int amount)
        {
            var res = new int[5];

            for (int i = coin.Length - 1; i >= 0; i--)
            {
                if (amount == 0) break;
                long count = Math.Min(amount / coin[i], arr[i]);
                res[i] = (int)count;
                amount -= res[i] * coin[i];
            }

            if (amount == 0)
            {
                for (int i = 0; i < arr.Length; i++)
                    arr[i] -= res[i];
                return res;
            }
            else
            {
                return new int[] { -1 };
            }

        }
    }
}
