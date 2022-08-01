using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy
{
    public partial class Easy
    {
        /*
        ///504. Base 7
        ///Given an integer num, return a string of its base 7 representation.
        public string ConvertToBase7(int num)
        {
            if (num == 0) return "0";
            bool sign = num > 0;
            num = Math.Abs(num);
            string res = "";
            while (num > 0)
            {
                res = num % 7 + res;
                num /= 7;
            }
            return sign ? res : "-" + res;
        }

        ///521. Longest Uncommon Subsequence I
        public int FindLUSlength(string a, string b)
        {
            return a == b ? -1 : Math.Max(a.Length, b.Length);
        }



        ///791. Custom Sort String
        //Permute the characters of s so that they match the order that order was sorted.
        //order = "cba", s = "abcd" => Output: "cbad"
        public string CustomSortString(string order, string s)
        {
            StringBuilder sb = new StringBuilder();
            int n = order.Length;
            Dictionary<char, int> dict = new Dictionary<char, int>();
            for (int i = 0; i < n; i++)
                dict.Add(order[i], i);
            int[] arr = new int[n];
            foreach (var c in s)
            {
                if (dict.ContainsKey(c)) arr[dict[c]]++;
                else sb.Append(c);
            }
            for (int i = 0; i < n; i++)
            {
                while (arr[i]-- > 0)
                    sb.Append(order[i]);
            }
            return sb.ToString();
        }

        ///951. Flip Equivalent Binary Trees, #BTree
        public bool FlipEquiv(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null) return true;
            else if (root1 == null || root2 == null) return false;
            else
            {
                if (root1.val != root2.val) return false;
                return (FlipEquiv(root1.left, root2.left) && FlipEquiv(root1.right, root2.right))
                    || (FlipEquiv(root1.left, root2.right) && FlipEquiv(root1.right, root2.left));
            }
        }
        */
    }
}
