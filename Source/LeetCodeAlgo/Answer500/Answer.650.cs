using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///662. Maximum Width of Binary Tree
        ///The maximum width of a tree is the maximum width among all levels.
        ///The width of one level is defined as the length between the end-nodes(the leftmost and rightmost non-null nodes),
        ///where the null nodes between the end-nodes are also counted into the length calculation.
        ///The number of nodes in the tree is in the range [1, 3000].
        public int WidthOfBinaryTree(TreeNode root)
        {
            int ans = 1;
            Dictionary<int,TreeNode> dict = new Dictionary<int,TreeNode>();
            dict.Add(0, root);
            while (dict.Count > 0)
            {
                Dictionary<int, TreeNode> next =new Dictionary<int, TreeNode>();
                int start = -1;
                int end = -1;
                foreach(var key in dict.Keys)
                {
                    var node = dict[key];
                    if (node.left!=null)
                    {
                        if (start == -1) start = key * 2;
                        end = key * 2;
                        next.Add(key*2, node.left);
                    }
                    if (node.right != null)
                    {
                        if (start == -1) start = key * 2+1;
                        end = key * 2 + 1;
                        next.Add(key * 2+1, node.right);
                    }
                }
                ans = Math.Max(ans, end-start+1);
                dict = next;
            }
            return ans;
        }
        /// 698. Partition to K Equal Sum Subsets
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
