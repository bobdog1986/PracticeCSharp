using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///374. Guess Number Higher or Lower, #Binary Search
    //1 <= n <= 231 - 1, 1 <= pick <= n, -1 if guess higher, 1 if guess lower
    public class GuessGame
    {
        private readonly int val = 6;

        public int GuessNumber(int n)
        {
            int left = 1;
            int right = n;
            while (left < right)
            {
                int mid = left + (right - left) / 2;//avoid overflow
                if (guess(mid) == 0) return mid;
                else if (guess(mid) == 1) left = mid + 1;//lower
                else right = mid - 1;//higher
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