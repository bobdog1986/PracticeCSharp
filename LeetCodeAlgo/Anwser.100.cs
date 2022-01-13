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

        //116. Populating Next Right Pointers in Each Node

        public Node Connect(Node root)
        {
            if (root == null)
                return null;

            List<Node> list = new List<Node>();
            list.Add(root);
            while (list.Count!=0)
            {
                List<Node> subs = new List<Node>();

                bool hasSubs =list[0].left!= null;
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].next = i == list.Count - 1 ? null : list[i + 1];
                    if (hasSubs)
                    {
                        subs.Add(list[i].left);
                        subs.Add(list[i].right);
                    }
                }

                list = subs;
            }

            return root;

        }
        //118. Pascal's Triangle
        public IList<IList<int>> Generate(int numRows)
        {
            List<IList<int>> list=new List<IList<int>>();
            int i = 1;
            list.Add(new List<int>() { 1 });
            i++;
            while(i <= numRows)
            {
                var list2=new List<int>();
                int j = 1;
                list2.Add(1);
                j++;
                while (j < i)
                {
                    list2.Add(list[i-1-1][j-1-1]+list[i-1-1][j-1]);
                    j++;
                }
                list2.Add(1);
                list.Add(list2);
                i++;
            }

            return list;
        }

        //121. Best Time to Buy and Sell Stock

        public int MaxProfit_121(int[] prices)
        {
            if(prices == null || prices.Length<=1)
                return 0;

            if(prices.Length ==2)
            {
                if (prices[0] >= prices[1])
                    return 0;
                else
                    return prices[1] - prices[0];
            }

            int MinSoFar = prices[0];
            int maxProfit = 0;
            foreach (var currentPrice in prices)
            {
                MinSoFar = Math.Min(MinSoFar, currentPrice);
                maxProfit = Math.Max(maxProfit, currentPrice - MinSoFar);
            }
            return maxProfit;
        }

        //122
        public int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length <= 1)
                return 0;

            if (prices.Length == 2)
            {
                if (prices[0] >= prices[1])
                    return 0;
                else
                    return prices[1] - prices[0];
            }

            int sum = 0;
            bool isHold = false;
            int buy = 0;
            for (int i=0;i<prices.Length-1;i++)
            {
                if (isHold)
                {
                    if (i == prices.Length - 2)
                    {
                        sum+= Math.Max(prices[i + 1], prices[i]) - buy;
                    }
                    else
                    {
                        if(prices[i] <= prices[i + 1])
                        {
                            continue;
                        }
                        else
                        {
                            sum+=prices[i]-buy;
                            isHold = false;
                        }
                    }
                }
                else
                {
                    if (prices[i] >= prices[i + 1])
                        continue;

                    if (i == prices.Length - 2)
                    {
                        if (prices[i] < prices[i + 1])
                        {
                            sum += prices[i + 1] - prices[i];
                        }
                        break;
                    }

                    isHold = true;
                    buy = prices[i];

                }
            }
            return sum;
        }

        //128
        public int LongestConsecutive(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            Array.Sort(nums);
            int max = 1;
            int current = 1;
            int pre = nums[0];
            for(int i = 1; i < nums.Length; i++)
            {
                if (nums[i] <= pre + 1)
                {
                    if(nums[i]!=pre)current++;
                }
                else
                {
                    max = Math.Max(current, max);
                    current = 1;
                }
                pre = nums[i];
            }

            return max = Math.Max(current, max);
        }

        //141. Linked List Cycle

        public bool HasCycle(ListNode head)
        {
            if(head == null || head.next==null) return false;

            List<ListNode> list = new List<ListNode>();

            while (head!=null)
            {
                if(list.IndexOf(head) == -1)
                {
                    list.Add(head);
                    head = head.next;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        ///144. Binary Tree Preorder Traversal

        public IList<int> PreorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            PreorderTraversal(root, result);
            return result;
        }

        public void PreorderTraversal(TreeNode node, IList<int> list)
        {
            if(node == null)
                return;

            list.Add(node.val);
            PreorderTraversal(node.left, list);
            PreorderTraversal(node.right, list);

        }


        //149

        public int MaxPoints(Point[] points)
        {
            if (points == null || points.Length == 0) return 0;
            Dictionary<int,int> dict=new Dictionary<int, int>();
            Dictionary<int, int> dictY = new Dictionary<int, int>();
            foreach (var i in points)
            {
                if (dict.ContainsKey(i.x))
                {
                    dict[i.x]++;
                }
                else
                {
                    dict[i.x] = 1;
                }

                if (dictY.ContainsKey(i.y))
                {
                    dictY[i.y]++;
                }
                else
                {
                    dictY[i.y] = 1;
                }
            }
            return Math.Max(dict.Values.Max(), dictY.Values.Max());
        }
        //152. Maximum Product Subarray
        public int MaxProduct(int[] nums)
        {
            int max = nums[0], min = nums[0], ans = nums[0];
            int n = nums.Length;

            for (int i = 1; i < n; i++)
            {

                // Swapping min and max
                if (nums[i] < 0)
                {
                    int temp = max;
                    max = min;
                    min = temp;
                }



                max = Math.Max(nums[i], max * nums[i]);
                min = Math.Min(nums[i], min * nums[i]);


                ans = Math.Max(ans, max);
            }

            return ans;
        }

        public int MaxProduct_1(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int max = -10;

            List<int> list = new List<int>();
            List<int> indexsOfNegative = new List<int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    max=Math.Max(max,0);
                    max=Math.Max(max, GetMaxProduct(list, indexsOfNegative));

                    list.Clear();
                    indexsOfNegative.Clear();
                }
                else
                {
                    list.Add(nums[i]);
                    if(nums[i] < 0)
                    {
                        indexsOfNegative.Add(list.Count-1);
                    }
                }
            }

            max = Math.Max(max, GetMaxProduct(list, indexsOfNegative));

            return max;
        }

        public int GetMaxProduct(List<int> list,List<int> indexsOfNegative=null)
        {
            if (list.Count == 0)
                return 0;
            if(list.Count == 1)
                return list[0];

            if (indexsOfNegative==null || indexsOfNegative.Count % 2 == 0)
            {
                return list.Aggregate((x,y)=>x*y);
            }
            else
            {
                return Math.Max(
                    GetMaxProduct(list.GetRange(indexsOfNegative[0] + 1, list.Count - indexsOfNegative[0] - 1)),
                    GetMaxProduct(list.GetRange(0, indexsOfNegative[indexsOfNegative.Count - 1]))
                    );
            }
        }

        //wrong understand input is sorted nums
        //public int MaxProduct(int[] nums)
        //{
        //    if(nums == null || nums.Length == 0)
        //        return 0;
        //    if(nums.Length == 1)
        //        return nums[0];

        //    int max = -10;

        //    int start = nums[0];
        //    int end = nums[0];

        //    for (int i = 0; i < nums.Length-1; i++)
        //    {
        //        if (nums[i] + 1 == nums[i + 1])
        //        {
        //            end = nums[i + 1];
        //        }
        //        else
        //        {
        //            max = Math.Max(max, GetMaxProductFromContinuous(start,end));
        //            start = nums[i + 1];
        //            end = nums[i + 1];
        //        }
        //    }

        //    max = Math.Max(max, GetMaxProductFromContinuous(start, end));

        //    return max;
        //}

        //public int GetMaxProductFromContinuous(int start,int end)
        //{
        //    if (start == end)
        //    {
        //        return start;
        //    }
        //    else
        //    {
        //        if (start < 0 && end < 0)
        //        {
        //            if ((end - start + 1) % 2 == 1)
        //            {
        //                return Factorial(start, end-1);
        //            }
        //            else
        //            {
        //                return Factorial(start, end);
        //            }
        //        }
        //        else if(start >0 && end > 0)
        //        {
        //            return Factorial(start, end);
        //        }
        //        else if(start <0 && end > 0)
        //        {
        //            return Math.Max(((-start) % 2 == 1 && start < -1) ? Factorial(start, -2) : Factorial(start, -1), Factorial(1, end));
        //        }
        //        else if (start == 0)
        //        {
        //            return Factorial(1, end);
        //        }
        //        else
        //        {
        //            return ((-start) % 2 == 1 && start < -1) ? Factorial(start, -2) : Factorial(start, -1);
        //        }
        //    }
        //}

        //public int Factorial(int i, int j)
        //{
        //    int result = 1;
        //    while (i <= j)
        //    {
        //        result *= i;
        //        i++;
        //    }
        //    return result;
        //}

        // 167 Two Sum II - Input Array Is Sorted
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
                    if (numbers[i] + numbers[j] != target) { continue; }

                    if (numbers[i] + numbers[j] == target)
                    {
                        return new int[2] { i+1, j+1 };
                    }
                }
            }
            throw new ArgumentOutOfRangeException();
        }
        //174 not done
        public int CalculateMinimumHP(int[,] dungeon)
        {
            if (dungeon == null || dungeon.Length == 0) return 0;

            int col = dungeon.GetLength(0);
            int row = dungeon.GetLength(1);

            int maxLost = 0;

            List<int> path = new List<int>();


            return maxLost > 0?1:(maxLost+1);
        }
        //187
        public IList<string> FindRepeatedDnaSequences(string s)
        {
            Dictionary<string, int> dna = new Dictionary<string, int>();
            List<string> repeat = new List<string>();
            if (string.IsNullOrEmpty(s)) return repeat;
            if (s.Length <= 10) return repeat;

            for (int i = 0; i < s.Length - 10 + 1; i++)
            {
                string str = s.Substring(i, 10);
                if (dna.ContainsKey(str))
                {
                    dna[str]++;
                }
                else
                {
                    dna.Add(str, 1);
                }
            }

            repeat = dna.Where(o => o.Value > 1).Select(o => o.Key).ToList();

            return repeat;

            /*
            List<string> all = new List<string>();
            List<string> repeat = new List<string>();

            if (string.IsNullOrEmpty(s)) return repeat;
            if (s.Length <= 10) return repeat;

            for (int i = 0; i < s.Length - 10 + 1; i++)
            {
                string str = s.Substring(i, 10);
                if (all.Contains(str))
                {
                    if (!repeat.Contains(str))
                    {
                        repeat.Add(str);
                    }
                }
                else
                {
                    all.Add(str);
                }
            }

            return repeat;*/
        }

        //189. Rotate Array

        public void Rotate(int[] nums, int k)
        {
            k=k % nums.Length;

            if (k == 0) return;

            int[] temp = new int[k];
            for(int i = 0; i < k; i++)
            {
                temp[i] = nums[nums.Length-k+i];
            }

            for(int i = nums.Length-1; i >k-1; i--)
            {
                nums[i] = nums[i-k];
            }

            for(int i = 0;i < k; i++)
            {
                nums[i] = temp[i];
            }
        }


        //198. House Robber
        public int Rob_198(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if(nums.Length == 1)
                return nums[0];

            int[] dp = new int[nums.Length];

            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0],nums[1]);

            for(int i = 2; i < nums.Length; i++)
            {
                int a = nums[i] + dp[i - 2];
                dp[i] = Math.Max(a, dp[i - 1]);
            }

            return dp[nums.Length - 1];
        }
    }
}
