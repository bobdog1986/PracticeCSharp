using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    /// <summary>
    /// range 101-200
    /// </summary>
    public partial class Anwser
    {
        //121
        public int MaxProfit(int[] prices)
        {
            int result = 0;
            for (int i = 0; i < prices.Length-1; i++)
            {
                if (prices[i] >= prices[i + 1]) continue;
                int leftMax = GetMaxNumb(prices, i + 1);
                if (prices[i] < leftMax)
                {
                    int profit = leftMax - prices[i];
                    if (result < profit) result = profit;
                }
            }
            return result;
        }

        public int GetMaxNumb(int[] prices,int index)
        {
            int result = prices[index];
            for(int i = index+1; i < prices.Length; i++)
            {
                if (result < prices[i]) result = prices[i];
            }
            return result;
        }
        //105
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            if (preorder == null || preorder.Length == 0) return null;

            if (preorder.Length == 1)
            {
                return new TreeNode(preorder[0]);
            }

            TreeNode root = new TreeNode(preorder[0]);
            int index = FindIndex(preorder[0], inorder);
            int[] leftPreorder = new int[index];
            int[] leftInorder = new int[index];
            int[] rightPreorder = new int[inorder.Length - index-1];
            int[] rightInorder = new int[inorder.Length - index - 1];

            Array.Copy(inorder, 0, leftInorder, 0, index);
            Array.Copy(inorder, index + 1, rightInorder, 0, inorder.Length - index - 1);

            Array.Copy(preorder, 1, leftPreorder, 0, index);
            Array.Copy(preorder, index + 1, rightPreorder, 0, inorder.Length - index - 1);

            root.left = BuildTree(leftPreorder, leftInorder);
            root.right = BuildTree(rightPreorder, rightInorder);

            return root;
        }

        public int FindIndex(int find,int[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if (find == array[i]) return i;
            }
            throw new ArgumentOutOfRangeException();
        }
        //106
        //public TreeNode BuildTree(int[] inorder, int[] postorder)
        //{
        //    if (inorder == null || inorder.Length == 0) return null;

        //    if (inorder.Length == 1)
        //    {
        //        return new TreeNode(inorder[0]);
        //    }

        //    TreeNode root = new TreeNode(postorder[postorder.Length-1]);
        //    int index = FindIndex(postorder[postorder.Length - 1], inorder);

        //    int[] leftInorder = new int[index];
        //    int[] leftPostorder = new int[index];
        //    int[] rightInorder = new int[inorder.Length - index - 1];
        //    int[] rightPostorder = new int[inorder.Length - index - 1];

        //    Array.Copy(inorder, 0, leftInorder, 0, index);
        //    Array.Copy(inorder, index + 1, rightInorder, 0, inorder.Length - index - 1);

        //    Array.Copy(postorder, 0, leftPostorder, 0, index);
        //    Array.Copy(postorder, index, rightPostorder, 0, inorder.Length - index - 1);

        //    root.left = BuildTree(leftInorder,leftPostorder);
        //    root.right = BuildTree(rightInorder,rightPostorder);

        //    return root;
        //}
        // 167
        public int[] TwoSumII(int[] numbers, int target)
        {
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (i > 0)
                {
                    if (numbers[i - 1] == numbers[i]) { continue; }
                }

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[i] + numbers[j] > target) { continue; }

                    if (numbers[i] + numbers[j] == target)
                    {
                        return new int[2] { i+1, j+1 };
                    }
                }
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
