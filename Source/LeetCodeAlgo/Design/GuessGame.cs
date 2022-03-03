using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///374. Guess Number Higher or Lower, #Binaray Search
    public class GuessGame
    {
        private int val=6;
        public GuessGame()
        {

        }

        public int GuessNumber(int n)
        {
            int left = 1;
            int right = n;
            while(left <= right)
            {
                var mid = left+(right-left)/2;
                if (guess(mid) == 0)
                {
                    return mid;
                }
                else if(guess(mid) == -1)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return left;
        }

        public int guess(int num)
        {
            if (num == val) return 0;
            else if (num > val) return -1;
            else return 1;
        }
    }
}
