using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    /// <summary>
    /// Range 1-100
    /// </summary>
    public partial class Anwser
    {
        // 1
        public int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length - 1; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[2] { i, j };
                    }
                }
            }
            throw new ArgumentOutOfRangeException();
        }
        //8
        public int MyAtoi(string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;
            str = str.Trim();
            if (string.IsNullOrEmpty(str)) return 0;
            try
            {
                return int.Parse(str);
            }
            catch
            {
                return 0;
            }
        }

        //12
        public string IntToRoman(int num)
        {
            if ((num < 0) || (num > 3999)) throw new ArgumentOutOfRangeException();
            if (num >= 1000) return "M" + IntToRoman(num - 1000);
            if (num >= 900) return "CM" + IntToRoman(num - 900);
            if (num >= 500) return "D" + IntToRoman(num - 500);
            if (num >= 400) return "CD" + IntToRoman(num - 400);
            if (num >= 100) return "C" + IntToRoman(num - 100);
            if (num >= 90) return "XC" + IntToRoman(num - 90);
            if (num >= 50) return "L" + IntToRoman(num - 50);
            if (num >= 40) return "XL" + IntToRoman(num - 40);
            if (num >= 10) return "X" + IntToRoman(num - 10);
            if (num >= 9) return "IX" + IntToRoman(num - 9);
            if (num >= 5) return "V" + IntToRoman(num - 5);
            if (num >= 4) return "IV" + IntToRoman(num - 4);
            if (num > 1) return "I" + IntToRoman(num - 1);
            if (num == 1) return "I";
            return string.Empty;
        }

        //13
        public int RomanToInt(string s)
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>()
            {
                { 'I', 1},
                { 'V', 5},
                { 'X', 10},
                { 'L', 50},
                { 'C', 100},
                { 'D', 500},
                { 'M', 1000}
            };

            int number = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (i + 1 < s.Length && dictionary[s[i]] < dictionary[s[i + 1]])
                {
                    number -= dictionary[s[i]];
                }
                else
                {
                    number += dictionary[s[i]];
                }
            }
            return number;
        }

        //18
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            Array.Sort(nums);
            List<IList<int>> result = new List<IList<int>>();
            List<int> codeList = new List<int>();
            int i = 0, j = 1, k = 2, l = 3;
            for (i = 0; i < nums.Length - 3; i++)
            {
                //if (i>0 && nums[i] == nums[i - 1] && i+1 < j) { continue; }
                for (j = i + 1; j < nums.Length - 2; j++)
                {
                    //if (j > 1 && nums[j] == nums[j - 1] && j+1  < k) { continue; }
                    for (k = j + 1; k < nums.Length - 1; k++)
                    {
                        //if (k > 2 && nums[k] == nums[k - 1] && k+1  < l) { continue; }
                        for (l = k + 1; l < nums.Length; l++)
                        {
                            if (nums[i] + nums[j] + nums[k] + nums[l] > target)
                            {
                                break;
                            }

                            if (nums[i] + nums[j] + nums[k] + nums[l] == target)
                            {
                                //int code= GetCode(current);
                                int code = nums[i] + 10 * nums[j] + 100 * nums[k] + 1000 * nums[l];

                                if (!codeList.Contains(code))
                                {
                                    codeList.Add(code);
                                    result.Add(new int[] { nums[i], nums[j], nums[k], nums[l] });
                                }

                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }
        public int GetCode(int[] nums)
        {

            return nums[0] + nums[1] * 10 + nums[2] * 100 + nums[3] * 1000;
        }

        public bool IsSameFourIntArray(int[] first, int[] second)
        {
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i]) return false;
            }
            return true;
        }
        //26
        public int RemoveDuplicates(int[] nums)
        {
            int find = 0;
            for (int i = 0; i < nums.Length - 1;)
            {
                if (nums[i] == nums[i + 1])
                {
                    Array.Copy(nums, i + 1, nums, i, nums.Length - i - 1 - find);

                    find++;
                }
                else
                {
                    i++;
                }
                if (i + find >= nums.Length - 1) break;
            }

            nums = nums.ToList().GetRange(0, nums.Length - find).ToArray();

            return nums.Length;
        }

        //27
        public int RemoveElement(int[] nums, int val)
        {
            int find = 0;
            for (int i = 0; i < nums.Length;)
            {
                if (nums[i] == val)
                {
                    Array.Copy(nums, i + 1, nums, i, nums.Length - i - 1 - find);

                    find++;
                }
                else
                {
                    i++;
                }
                if (i + find >= nums.Length) break;
            }
            nums = nums.ToList().GetRange(0, nums.Length - find).ToArray();

            return nums.Length;
        }

        //35. Search Insert Position

        public int SearchInsert(int[] nums, int target)
        {
            return SearchInsert(nums, target, 0, nums.Length - 1);
        }

        public int SearchInsert(int[] nums, int target, int start, int end)
        {
            if (start == end)
            {
                if (nums[start] >= target)
                {
                    return start;
                }
                else
                {
                    return start + 1;
                }
            }

            if (start + 1 == end)
            {
                if (nums[start] >= target)
                {
                    return start;
                }
                else if (nums[end] < target)
                {
                    return end + 1;
                }
                else
                {
                    return start + 1;
                }
            }

            int num = end - start + 1;
            int mid = num / 2 + start;
            if (nums[mid] == target)
            {
                return SearchInsert(nums, target, start, mid);
            }
            else if (nums[mid] < target)
            {
                return SearchInsert(nums, target, mid + 1, end);
            }
            else
            {
                return SearchInsert(nums, target, start, mid - 1);
            }
        }


        //38
        public string CountAndSay(int n)
        {
            string s = "1";
            if (n == 1) return s;

            for (int i = 0; i < n - 1; i++)
            {
                s = GetCountAndSay(s);
            }
            return s;
        }

        public string GetCountAndSay(string s)
        {
            var arr = s.ToCharArray();
            List<char> result = new List<char>();
            char pre = arr[0];
            int occured = 1;
            char current;
            for (int i = 1; i < arr.Length; i++)
            {
                current = arr[i];
                if (current == pre)
                {
                    occured++;
                }
                else
                {
                    result.AddRange(occured.ToString().ToCharArray());
                    result.Add(pre);
                    pre = current;
                    occured = 1;
                }

            }
            result.AddRange(occured.ToString().ToCharArray());
            result.Add(pre);
            return new string(result.ToArray());
        }

        //53
        public int MaxSubArray(int[] nums)
        {
            int sum = 0;
            int max = nums.Max();

            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (sum <= 0)
                {
                    sum = 0;
                }
                else
                {
                    max = Math.Max(max, sum);
                }
            }

            return max;
        }


        //56
        public IList<Interval> Merge(IList<Interval> intervals)
        {
            if (intervals == null || intervals.Count <= 1) return intervals;

            IList<Interval> result = new List<Interval>();

            for (int i = 0; i < intervals.Count; i++)
            {
                Interval current = intervals[i];
                result.Add(current);
                result = TrimIntervalFromEnd(result);
            }
            return result;
        }
        public IList<Interval> TrimIntervalFromEnd(IList<Interval> list)
        {
            if (list == null || list.Count <= 1) return list;
            while (list.Count > 1)
            {
                Interval current = list[list.Count - 1];
                Interval pre = list[list.Count - 2];
                if (pre.end < current.start)
                {
                    break;
                }
                else
                {
                    if (pre.start <= current.start)
                    {
                        pre.start = Math.Min(pre.start, current.start);
                        pre.end = Math.Max(pre.end, current.end);
                        list.Remove(current);
                    }
                    else
                    {
                        SwapIntervalNode(ref pre, ref current);
                        list.Remove(current);
                        list = TrimIntervalFromEnd(list);
                        list.Add(current);
                    }
                }
            }

            return list;
        }
        public void SwapIntervalNode(ref Interval a, ref Interval b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
        public Interval MergeIntervalNodes(Interval current, Interval next)
        {
            return new Interval(Math.Min(current.start, next.start), Math.Max(current.end, next.end));
        }
        //57 not pass
        public IList<Interval> Insert(IList<Interval> intervals, Interval newInterval)
        {

            return intervals;
        }
        //65
        public bool IsNumber(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            s = s.TrimStart();
            s = s.TrimEnd();
            if (string.IsNullOrEmpty(s)) return false;

            try
            {
                double d = double.Parse(s);
                return true;
            }
            catch
            {
                if (s.Contains(" ")) return false;

                if (s.Contains("e"))
                {
                    int idx = s.IndexOf('e');
                    if (idx == s.LastIndexOf('e') && (idx > 0 && idx < s.Length - 1))
                    {
                        var arr = s.Split('e');
                        if (arr.Length != 2) return false;
                        double part;
                        long exp;
                        try
                        {
                            part = double.Parse(arr[0]);
                            exp = long.Parse(arr[1]);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
            }

            return false;
        }

        //69
        public int MySqrt(int x)
        {
            long r = x;
            while (r * r > x)
                r = (r + x / r) / 2;
            return (int)r;
        }
        //70. Climbing Stairs
        public int ClimbStairs(int n)
        {
            //bottom to up
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 2;

            int seed1 = 1;
            int seed2 = 1;
            for(int i=n -2 ; i >0 ;i--)
            {
                int temp = seed1;
                seed1 = seed2;
                seed2 = seed2 + temp;
            }

            return seed1 + seed2;


            //brute force, out of memory
            if (n == 0) return 0;
            int totalCount = 0;

            int root = 0;

            Queue<int> nodes = new Queue<int>();
            nodes.Enqueue(root);

            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                var left = node + 1;
                var right = node + 2;

                if (left < n)
                {
                    nodes.Enqueue(left);
                }
                else if (left == n)
                {
                    totalCount++;
                }

                if (right < n)
                {
                    nodes.Enqueue(right);
                }
                else if (right == n)
                {
                    totalCount++;
                }
            }

            return totalCount;
        }

        public int ClimbStairsN(int n)
        {
            if(n==0) return 0;
            if(n == 1) return 1;
            if(n==2) return 2;

            return ClimbStairsN(n-1)+ClimbStairsN(n-2);

            return 0;
        }


        //88. Merge Sorted Array
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (nums1 == null || nums1.Length==0||m==0)
            {
                if (nums2 == null || nums2.Length == 0 || n == 0)
                {
                    //
                }
                else
                {
                    for(int a = 0; a < nums1.Length; a++)
                    {
                        if (a < m+n)
                        {
                            nums1[a] = nums2[a];

                        }
                        else
                        {
                            nums1[a] = 0;
                        }
                    }
                }
            }
            else
            {
                if(nums2 == null || nums2.Length == 0 || n == 0)
                {
                    for (int a = 0; a < nums1.Length; a++)
                    {
                        if (a < m + n)
                        {
                            nums1[a] = nums1[a];

                        }
                        else
                        {
                            nums1[a] = 0;
                        }
                    }
                }
                else
                {
                    int[] result=new int[m+n];
                    int k = 0;
                    int j = 0;
                    int i = 0;
                    while (i<m && j<n )
                    {
                        if (nums1[i] <= nums2[j])
                        {
                            result[k] = nums1[i];
                            k++;
                            i++;
                        }
                        else
                        {
                            result[k] = nums2[j];
                            k++;
                            j++;
                        }
                    }

                    while (k < m + n)
                    {
                        if (i < m)
                        {
                            result[k]=nums1[i];
                            k++;
                            i++;
                        }

                        if (j < n)
                        {
                            result[k] = nums2[j];
                            k++;
                            j++;
                        }
                    }


                    for(int a=0; a<nums1.Length; a++)
                    {
                        if (a < k)
                        {
                            nums1[a] =result[a];

                        }
                        else
                        {
                            nums1[a] = 0;
                        }
                    }
                }
            }
            Console.WriteLine($"nums1 = {string.Join(",",nums1)}");

        }

        //94
        public IList<int> InorderTraversal(TreeNode root)
        {
            List<int> values = new List<int>();

            if (root == null) return values;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode node = root;

            while (node != null || stack.Any())
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }
                else
                {
                    var item = stack.Pop();
                    values.Add(item.val);
                    node = item.right;
                }
            }
            return values;
        }

        public IList<int> LayerTraversal(TreeNode root)
        {
            List<int> values = new List<int>();

            if (root == null) return values;

            IList<TreeNode> nodes = new List<TreeNode> { root };

            while (nodes != null && nodes.Count > 0)
            {
                nodes = GetInorderAndReturnSubNodes(nodes, values);
            }

            return values;
        }
        public IList<TreeNode> GetInorderAndReturnSubNodes(IList<TreeNode> nodes,List<int> values)
        {
            if (nodes == null||nodes.Count==0) return null;
            IList<TreeNode> subNodes = new List<TreeNode>();
            foreach(var n in nodes)
            {
                values.Add(n.val);
                if (n.left != null) { subNodes.Add(n.left); }
                if (n.right != null) { subNodes.Add(n.right); }
            }
            return subNodes;
        }
        //98
        public bool IsValidBST(TreeNode root)
        {
            if (root == null) return true;
            if (root.left == null && root.right == null) return true;
            return IsValidNode(root, (long)int.MaxValue+1, (long)int.MinValue-1);
            //return (root.left != null && ((root.left.val < root.val) && IsValidBST(root.left))) &&
            //    (root.right != null && ((root.right.val > root.val) && IsValidBST(root.right)));

            //return (root.left == null || ((root.left.val < root.val) && IsValidBST(root.left))) &&
            //    (root.right == null || ((root.right.val > root.val) && IsValidBST(root.right)));
        }

        public bool IsValidNode(TreeNode node, long maxlimit, long minlimit)
        {
            if (node == null) return true;
            if (node.val >= maxlimit || node.val <= minlimit) return false;

            return IsValidNode(node.left, Math.Min(node.val, maxlimit), minlimit) &&
                IsValidNode(node.right, maxlimit, Math.Max(node.val, minlimit));

        }
    }
}