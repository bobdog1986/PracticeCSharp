using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///450. Delete Node in a BST
        ///The number of nodes in the tree is in the range [0, 10^4].
        public TreeNode DeleteNode(TreeNode root, int key)
        {
            if(root == null)
                return null;
            if (root.val == key)
            {
                return DeleteNode(root);
            }
            else if(key>root.val)
            {
                root.right = DeleteNode(root.right, key);
            }
            else
            {
                root.left = DeleteNode(root.left, key);
            }
            return root;
        }

        public TreeNode DeleteNode(TreeNode root)
        {
            if (root.left == null)
                return root.right;
            if (root.right == null)
                return root.left;

            //inset the right subtree to left subtree
            var ans=root.left;
            var node = root.left;
            while (node.right != null)
            {
                node=node.right;
            }
            node.right = root.right;
            return ans;
        }

        ///452. Minimum Number of Arrows to Burst Balloons
        ///points.Length = Balloons number, Balloons horizontal -231 <= xstart < xend <= 231 - 1
        public int FindMinArrowShots(int[][] points)
        {
            if (points.Length <= 1)
                return points.Length;

            var arr = points.OrderBy(p => p[1]).ToList();

            var shot = 1;
            int end = arr[0][1];

            for (int i = 1; i < arr.Count; i++)
            {
                if (end < arr[i][0])
                {
                    end = arr[i][1];
                    shot++;
                }
            }

            return shot;
        }

        ///457. Circular Array Loop
        ///If nums[i]>0, move nums[i] steps forward, If nums[i]<0 move nums[i] steps backward.
        ///Every nums[seq[j]] is either all positive or all negative.
        ///Return true if there is a cycle in nums, or false otherwise.
        public bool CircularArrayLoop(int[] nums)
        {
            bool[] visit = new bool[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                if (visit[i])
                    continue;
                bool forward = nums[i] > 0;
                visit[i] = true;
                List<int> list = new List<int>() { i };
                int j = 0;
                int next = (i + nums[i]) % nums.Length;
                if (next < 0)
                    next += nums.Length;
                while (j++ < nums.Length)
                {
                    if ((forward && nums[next] < 0) || (!forward && nums[next] > 0))
                        break;
                    if (list.Contains(next) && next != list.Last())
                        return true;
                    if (visit[next])
                        break;
                    list.Add(next);
                    visit[next] = true;
                    next = (next + nums[next]) % nums.Length;
                    if (next < 0)
                        next += nums.Length;
                }
            }
            return false;
        }
        /// 492
        public int[] ConstructRectangle(int area)
        {
            int[] result = new int[2] { area, 1 };
            int min = (int)Math.Sqrt(area);
            for (int len = min; len < area; len++)
            {
                if (area % len == 0)
                {
                    if (len >= area / len)
                    {
                        return new int[2] { len, area / len };
                    }
                }
            }
            return result;
        }

        //495
        public int FindPoisonedDuration(int[] timeSeries, int duration)
        {
            if (timeSeries == null || timeSeries.Length == 0) return 0;

            Array.Sort(timeSeries);

            int begin = timeSeries[0];
            int expired = begin + duration;

            int total = 0;
            for (int i = 1; i < timeSeries.Length; i++)
            {
                if (timeSeries[i] < expired)
                {
                    expired = timeSeries[i] + duration;
                }
                else
                {
                    total += expired - begin;

                    begin = timeSeries[i];
                    expired = begin + duration;
                }
            }

            total += expired - begin;
            return total;
        }
    }
}