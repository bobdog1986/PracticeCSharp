using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {

        /// 1752. Check if Array Is Sorted and Rotated
        ///Given an array nums, return true if the array sorted in non-decreasing order, then rotated some
        ///[1,2,3,3,4],[2,3,4,1],[3,4,5,1,2]=>true, [2,1,3,4]=>false
        public bool Check_1752(int[] nums)
        {
            bool ans = true;
            bool isRotate = false;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] < nums[i - 1])
                {
                    if (isRotate)
                        return false;
                    isRotate = true;
                }
            }

            if (isRotate)
            {
                if (nums.Last() > nums.FirstOrDefault())
                {
                    ans = false;
                }
            }
            else
            {
                if (nums.Last() < nums.FirstOrDefault())
                {
                    ans = false;
                }
            }

            return ans;
        }
        /// 1758. Minimum Changes To Make Alternating Binary String
        /// Return the minimum number of operations needed to make s alternating. 010101 or 101010
        public int MinOperations(string s)
        {
            int dp0 = 0;
            //int dp1 = 0;
            //char c0 = '0';
            //char c1 = '1';
            for(int i=0; i<s.Length; i++)
            {
                if(s[i]-'0'!=i%2)
                    dp0 ++;
                //if (s[i] != c1)
                    //dp1++;
                //var temp = c0;
                //c0 = c1;
                //c1 = temp;
            }
            return Math.Min(dp0, s.Length-dp0);
        }
    }
}
