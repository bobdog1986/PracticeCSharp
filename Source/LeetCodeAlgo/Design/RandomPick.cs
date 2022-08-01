using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design.RandomPick
{
    ///528. Random Pick with Weight, #Binary Search
    //w[i] describes the weight of the ith index.
    //pickIndex() randomly picks an index in the range[0, w.length - 1] and returns it.
    //The probability of picking an index i is w[i] / sum(w).
    //For example, if w = [1, 3], the probability of picking index 0 is 1 / (1 + 3) = 0.25 (i.e., 25%),
    //and the probability of picking index 1 is 3 / (1 + 3) = 0.75 (i.e., 75%).
    public class Solution
    {
        private readonly int[] weights;
        private readonly int sum = 0;

        public Solution(int[] w)
        {
            weights = new int[w.Length];
            for (int i = 0; i < w.Length; i++)
            {
                weights[i] = sum;
                sum += w[i];
            }
        }

        public int PickIndex()
        {
            var random = new Random();
            var target = random.Next(sum);
            int left = 0;
            int right = weights.Length - 1;
            while (left < right)
            {
                int mid = (left + right + 1) / 2;
                if (target == weights[mid])
                {
                    return mid;
                }
                else if (weights[mid] < target)
                {
                    left = mid;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return left;
        }
    }
}