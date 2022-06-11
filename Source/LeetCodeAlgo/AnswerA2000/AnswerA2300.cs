using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2300. Successful Pairs of Spells and Potions, #Binary Search
        //A spell and potion pair is considered successful if the product of their strengths >= success.
        //Return an integer array pairs of length n where pairs[i] is the number of potions that will form a successful pair with the ith spell.
        public int[] SuccessfulPairs(int[] spells, int[] potions, long success)
        {
            potions = potions.OrderBy(x => -x).ToArray();
            int m = potions.Length;
            long[] arr = new long[m];//must long, or overflow 10^10/1
            for (int i = 0; i < m; i++)
            {
                arr[i] = (long)Math.Ceiling(success * 1.0 / potions[i]);
            }

            int n = spells.Length;
            int[] res = new int[n];
            for (int i = 0; i < n; i++)
            {
                if (spells[i] < arr[0]) res[i] = 0;
                else if (spells[i] >= arr[m - 1]) res[i] = m;
                else
                {
                    int left = 0;
                    int right = m - 1;
                    while (left < right)
                    {
                        int mid = (left + right + 1) / 2;
                        if (spells[i] >= arr[mid])
                        {
                            left = mid;
                        }
                        else
                        {
                            right = mid - 1;
                        }
                    }
                    res[i] = left + 1;
                }
            }
            return res;
        }
    }
}
