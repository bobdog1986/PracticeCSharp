using System.Collections.Generic;
using System.Linq;
using System;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1422. Maximum Score After Splitting a String
        ///Split to 2 string,score is the number of zeros in the left + the number of ones in the right substring.
        public int MaxScore(string s)
        {
            Dictionary<int, int> dict=new Dictionary<int, int>();
            int numOf0 = 0;
            for(int i = 0; i < s.Length; i++)
            {
                if(s[i]=='0')
                    numOf0++;

                if(i<s.Length-1)
                    dict.Add(i, numOf0);
            }
            int max = 0;
            foreach(int key in dict.Keys)
                max=Math.Max(max, dict[key]+s.Length-numOf0-(key+1- dict[key]));
            return max;
        }

        ///1448. Count Good Nodes in Binary Tree
        ///a node X in the tree is named good if in the path from root to X there are no nodes with a value greater than X.
        public int GoodNodes(TreeNode root)
        {
            if (root == null)
                return 0;
            int ans = 1;
            int max = root.val;
            GoodNodes_Recursion(root.left, max, ref ans);
            GoodNodes_Recursion(root.right, max, ref ans);
            return ans;
        }
        public void GoodNodes_Recursion(TreeNode node,int max, ref int ans)
        {
            if (node == null)
                return;
            if (node.val >= max)
                ans++;
            max = Math.Max(node.val, max);
            GoodNodes_Recursion(node.left, max, ref ans);
            GoodNodes_Recursion(node.right, max, ref ans);
        }
    }
}