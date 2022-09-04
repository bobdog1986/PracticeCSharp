using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        //2400. Number of Ways to Reach a Position After Exactly k Steps
        public int NumberOfWays(int startPos, int endPos, int k)
        {
            long mod = 1_000_000_007;
            Dictionary<int, long> dict = new Dictionary<int, long>();
            dict.Add(startPos, 1);
            while (k-- > 0)
            {
                var next = new Dictionary<int, long>();
                foreach (var x in dict.Keys)
                {
                    if (!next.ContainsKey(x + 1))
                        next.Add(x + 1, 0);
                    next[x + 1] += dict[x];//move right
                    next[x + 1] %= mod;

                    if (!next.ContainsKey(x - 1))
                        next.Add(x - 1, 0);
                    next[x - 1] += dict[x];//move left
                    next[x - 1] %= mod;
                }
                dict = next;
            }
            if (dict.ContainsKey(endPos)) return (int)dict[endPos];
            else return 0;
        }

        ///2401. Longest Nice Subarray, #Sliding Window
        //a subarray of nums nice if the bitwise AND of every pair of elements is equal to 0.
        public int LongestNiceSubarray(int[] nums)
        {
            int res = 1;
            int[] arr = new int[32];//store count of bit-1 of from bit0 to bit31
            int left = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                var curr = getBitArrayOfInt(nums[i]);
                for (int j = 0; j <= 30; j++)
                    arr[j] += curr[j];
                //not nice, remove the lefe most element
                while (arr.Max() > 1 && left < i)
                {
                    var prev = getBitArrayOfInt(nums[left++]);
                    for (int j = 0; j <= 30; j++)
                        arr[j] -= prev[j];
                }
                res = Math.Max(res, i - left + 1);
            }
            return res;
        }

        private int[] getBitArrayOfInt(int x)
        {
            int[] res = new int[32];
            for (int i = 0; i <= 30; i++)
            {
                if ((x & (1 << i)) != 0)
                    res[i] = 1;
            }
            return res;
        }

    }
}
