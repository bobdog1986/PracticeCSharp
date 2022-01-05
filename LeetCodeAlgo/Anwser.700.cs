using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //704. Binary Search

        public int Search_704(int[] nums, int target)
        {
            for(int i=0; i<nums.Length; i++)
            {
                if (nums[i] == target) { return i; }
                if(nums[i] > target) { return -1; }
            }

            return -1;
        }

        //728
        public IList<int> SelfDividingNumbers(int left, int right)
        {
            List<int> result = new List<int>();
			for(int i = left; i <= right; i++)
            {
                bool pass = false;
                int temp = i;
                while (temp > 0)
                {
                    int mod = temp % 10;
                    if (mod == 0|| i % mod != 0)
                    {
                        pass = true;
                        break;
                    }
                    temp /= 10;
                }
                if (pass) continue;

                result.Add(i);
            }

            return result;
        }

		public bool IsSelfDivided(int num)
        {
            List<int> divid = new List<int>();
            int full = num;
            while (full > 0)
            {
                divid.Add(full % 10);
                full /= 10;
            }

			foreach(var i in divid)
            {
                if (i == 0|| num % i != 0) return false;
            }
            return true;
        }

        //746. Min Cost Climbing Stairs
        public int MinCostClimbingStairs(int[] cost)
        {
            if (cost == null || cost.Length == 0) return 0;

            if (cost.Length == 1) return cost[0];

            int[] dp = new int[cost.Length];

            dp[0] = cost[0];
            dp[1] = cost[1];

            for (int i = 2; i < cost.Length; i++)
            {
                //every point cost must to last1 or last2
                dp[i] = cost[i] + Math.Min(dp[i - 2], dp[i - 1]);
            }

            //every solution must to last1 or last2
            return Math.Min(dp[cost.Length - 1], dp[cost.Length - 2]);
        }


        public int MinCostClimbingStairs_BruteForce(int[] cost)
        {
            if(cost == null || cost.Length == 0) return 0;

            if(cost.Length == 1)return cost[0];

            if(cost.Length ==2 )return Math.Min( cost[0],cost[1]);

            int minCost = int.MaxValue;

            //arr[0] as Index, arr[1] as totalCost
            Queue<int[]> nodes = new Queue<int[]>();
            nodes.Enqueue(new int[] { 0, cost[0] });
            nodes.Enqueue(new int[] { 1, cost[1] });

            while(nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                if (node[0] < cost.Length - 2)
                {
                    if((node[1] + cost[node[0] + 1])< minCost)
                    {
                        nodes.Enqueue(new int[] { node[0] + 1, node[1] + cost[node[0] + 1] });

                    }
                    if (node[1] + cost[node[0] + 2] < minCost)
                    {
                        nodes.Enqueue(new int[] { node[0] + 2, node[1] + cost[node[0] + 2] });

                    }
                }
                else
                {
                    if(node[1] < minCost)
                    {
                        minCost = node[1];
                    }
                }
            }

            return minCost;
        }

        public int GetMinCost(int n)
        {
            return Math.Min(n,int.MaxValue);
        }
    }
}
