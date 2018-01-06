using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //202
        private List<int> happyList = new List<int>();
        public bool IsHappy(int n)
        {
            if (n == 1) return true;
            if (happyList.Contains(n)) return false;
            happyList.Add(n);

            return IsHappy(GetDigitSquare(n));
        }
        public int GetDigitSquare(int n)
        {
            int result = 0;
            int last;
            while (n > 0)
            {
                last = n % 10;
                result += last * last;
                n /= 10;
            }
            return result;
        }
        //258
        public int AddDigits(int num)
        {
            if (num < 10) return num;

            int total = 0;
            while (num >= 10)
            {
                total+= num % 10;
                num /= 10;
            }
            total += num;

            return AddDigits(total);
        }
    }
}
