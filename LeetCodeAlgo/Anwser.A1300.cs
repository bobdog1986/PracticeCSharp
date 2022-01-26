using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1314. Matrix Block Sum
        public int[][] MatrixBlockSum(int[][] mat, int k)
        {
            int rowLen = mat.Length;
            int colLen = mat[0].Length;

            int[][] result = new int[rowLen][];
            for (int i = 0; i < rowLen; i++)
                result[i] = new int[colLen];

            int[] colSum = new int[colLen];

            for(int i=0; i< rowLen; i++)
            {
                for(int j=0; j< colLen; j++)
                {
                    if (i == 0)
                    {
                        int sum = 0;
                        for (int r = Math.Max(0, i - k); r <= i + k && r < rowLen; r++)
                            sum += mat[r][j];
                        colSum[j] = sum;
                    }
                    else
                    {
                        if (i + k < rowLen)
                            colSum[j] += mat[i + k][j];
                        if (i - k - 1 >= 0)
                            colSum[j] -= mat[i - k - 1][j];
                    }

                }

                for (int j=0; j< colLen; j++)
                {
                    int sum = 0;
                    for(int c = Math.Max(0,j-k); c<=j+k && c< colLen; c++)
                    {
                        sum+=colSum[c];
                    }
                    result[i][j] = sum;
                }

            }

            return result;
        }

        ///1305. All Elements in Two Binary Search Trees
        //two binary search trees root1 and root2, return a list containing all the integers from both trees sorted in ascending order.
        public IList<int> GetAllElements(TreeNode root1, TreeNode root2)
        {
            List<int> ans = new List<int>();

            List<TreeNode> list = new List<TreeNode>() { root1,root2 };
            while(list.Count > 0)
            {
                List<TreeNode> next = new List<TreeNode>();

                foreach(var node in list)
                {
                    if (node != null)
                    {
                        ans.Add(node.val);
                        if(node.left != null)
                            next.Add(node.left);
                        if(node.right != null)
                            next.Add(node.right);
                    }
                }

                list = next;
            }

            ans.Sort();
            return ans;
        }

    }
}
