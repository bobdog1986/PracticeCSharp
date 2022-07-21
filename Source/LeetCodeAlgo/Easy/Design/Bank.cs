using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///2043. Simple Bank System
    public class Bank
    {
        private readonly long[] arr;
        public Bank(long[] balance)
        {
            arr = balance;
        }

        public bool Transfer(int account1, int account2, long money)
        {
            if (account1 > arr.Length || account2 > arr.Length) return false;
            if (arr[account1 - 1] >= money)
            {
                arr[account1 - 1] -= money;
                arr[account2 - 1] += money;
                return true;
            }
            else return false;
        }

        public bool Deposit(int account, long money)
        {
            if (account > arr.Length) return false;
            arr[account - 1] += money;
            return true;
        }

        public bool Withdraw(int account, long money)
        {
            if (account > arr.Length || arr[account - 1] < money) return false;
            else
            {
                arr[account - 1] -= money;
                return true;
            }
        }
    }

}
