using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///605. Can Place Flowers
        ///flowers cannot be planted in adjacent plots.
        ///[1,0,0,0,1], 0-empty, 1-planted, n=new flowers can be plant?
        public bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            if (n == 0)
                return true;

            for (int i = 0; i < flowerbed.Length; i++)
            {
                if (isMaxFlowersExceed(i, flowerbed.Length - 1, n))
                    return false;

                if (flowerbed[i] == 1 || (i > 0 && flowerbed[i - 1] == 1) || (i < flowerbed.Length - 1 && flowerbed[i + 1] == 1))
                {
                    continue;
                }
                else
                {
                    flowerbed[i] = 1;
                    n--;
                    if (n == 0)
                        return true;
                }
            }

            return false;
        }

        public bool isMaxFlowersExceed(int start, int end, int n)
        {
            int count = (end - start + 1);
            return count % 2 == 1 ? n > count / 2 + 1 : n > count / 2;
        }

        /// 611
        public int TriangleNumber(int[] nums)
        {
            Array.Sort(nums);
            int count = 0;
            int i, j, k;
            for (i = 0; i < nums.Length - 2; i++)
            {
                for (j = i + 1; j < nums.Length - 1; j++)
                {
                    for (k = j + 1; k < nums.Length; k++)
                    {
                        if (IsTriangle(nums[i], nums[j], nums[k]))
                        {
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return count;
        }

        public bool IsTriangle(int num1, int num2, int num3)
        {
            return num3 < num1 + num2;
        }

        //617. Merge Two Binary Trees

        public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null)
                return null;

            var result = new TreeNode
            {
                val = (root1 != null ? root1.val : 0) + (root2 != null ? root2.val : 0)
            };

            if (root1 == null)
            {
                result.left = root2.left;
                result.right = root2.right;
            }
            else if (root2 == null)
            {
                result.left = root1.left;
                result.right = root1.right;
            }
            else
            {
                result.left = MergeTrees(root1.left, root2.left);
                result.right = MergeTrees(root1.right, root2.right);
            }

            return result;
        }

        ///621. Task Scheduler
        ///1 <= task.length <= 10^4, tasks[i] is upper-case English letter.
        public int LeastInterval(char[] tasks, int n)
        {
            if (n <= 0) { return tasks.Length; }
            int[] arr = new int[26];
            foreach (char t in tasks)
            {
                arr[t - 'A']++;
            }
            Array.Sort(arr);
            // count is the number of appending numbers in last "round"
            int count = 0;
            for (int i = 25; i >= 0; i--)
            {
                if (arr[i] == arr[25])
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return Math.Max(tasks.Length, (arr[25] - 1) * (n + 1) + count);
        }

        ///648. Replace Words
        ///Return the sentence after the replacement.
        public string ReplaceWords(IList<string> dictionary, string sentence)
        {
            var words = dictionary.OrderBy(x => x).ThenBy(x => x.Length).ToList();
            var list = sentence.Split(' ').Where(x=>x.Length>0).ToList();
            var res =new List<string>();
            foreach(var i in list)
            {
                bool match = false;
                foreach(var word in words)
                {
                    if (i.StartsWith(word))
                    {
                        match = true;
                        res.Add(word);
                        break;
                    }
                }
                if (!match)
                    res.Add(i);
            }
            return string.Join(" ", res);
        }
    }
}