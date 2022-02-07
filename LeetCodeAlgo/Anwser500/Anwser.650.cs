using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///698. Partition to K Equal Sum Subsets
        ///return true if it is possible to divide this array into k non-empty subsets whose sums are all equal.
        ///1 <= k <= nums.length <= 16,1 <= nums[i] <= 10^4, The frequency of each element is in the range [1, 4].
        public bool CanPartitionKSubsets(int[] nums, int k)
        {
            if (k == 1)
                return true;

            if (k == nums.Length)
            {
                if (nums.Distinct().Count() != 1)
                {
                    return false;
                }
            }

            int sum = nums.Sum();
            if (sum % k != 0)
                return false;

            int goal = sum / k;

            bool[] visit=new bool[nums.Length];
            foreach(var n in nums)
            {
                if (n > goal)
                    return false;
            }

            nums=nums.OrderBy(x => -x).ToArray();
            bool found = false;
            CanPartitionKSubsets_Recursion(nums, visit, 0, k, goal, 0, ref found);
            return found;
        }

        public void CanPartitionKSubsets_Recursion(int[] nums, bool[] visit, int left, int k, int goal, int sum, ref bool found)
        {
            if (found == true)
                return;

            if (k == 1)
            {
                found = true;
                return;
            }

            for (int i = left; i < nums.Length; i++)
            {
                if (visit[i])
                    continue;

                if (nums[i] == goal-sum)
                {
                    var nextVisit=new bool[nums.Length];
                    Array.Copy(visit, nextVisit, visit.Length);
                    nextVisit[i] = true;
                    CanPartitionKSubsets_Recursion(nums, nextVisit, 0, k - 1, goal, 0, ref found);
                }
                else if(nums[i] < goal-sum)
                {
                    var nextVisit = new bool[nums.Length];
                    Array.Copy(visit, nextVisit, visit.Length);
                    nextVisit[i] = true;
                    CanPartitionKSubsets_Recursion(nums, nextVisit, i+1, k, goal, sum+ nums[i], ref found);
                }
            }
        }

    }
}
