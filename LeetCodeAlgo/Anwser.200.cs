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
        //240
        public bool SearchMatrix(int[,] matrix, int target)
        {
            if (matrix == null) return false;

            return SerachMatrix(matrix, 0, matrix.GetLength(0), target);
        }
        public bool SerachMatrix(int[,] matrix,int startRowIndex, int endColIndex, int target)
        {
            if (matrix == null) return false;
            int col = matrix.GetLength(0);
            int row = matrix.GetLength(1);

            for (int i = startRowIndex; i < row; i++)
            {
                for(int j = 0; j < endColIndex; j++)
                {
                    if (matrix[i, j] == target) return true;
                    if (matrix[i, j] > target) return SerachMatrix(matrix, i, j, target);
                }
            }
            return false;
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

        //278. First Bad Version

        public int FirstBadVersion(int n)
        {
            return FirstBadVersion(1,n);
        }

        public int FirstBadVersion(int start, int end)
        {
            if (start == end) return start;

            int num = end - start + 1;
            int mid = num / 2 + start - 1;

            if (IsBadVersion(mid))
            {
                return FirstBadVersion(start, mid);
            }
            else
            {
                return FirstBadVersion(mid + 1, end);
            }
        }

        public bool IsBadVersion(int n)
        {
            int bad = 10;
            return (n >= bad);
        }
    }
}
