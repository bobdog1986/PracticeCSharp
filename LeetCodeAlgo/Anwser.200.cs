using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //202
        private List<int> happyList = new List<int>();
        public bool IsHappy(int n)
        {
            if (n == 1) return true;
            if (happyList.Contains(n)) return false;
            happyList.Add(n);

            return IsHappy(GetDigitSquare(n));
        }
        public int GetDigitSquare(int n)
        {
            int result = 0;
            int last;
            while (n > 0)
            {
                last = n % 10;
                result += last * last;
                n /= 10;
            }
            return result;
        }
        //203. Remove Linked List Elements
        public ListNode RemoveElements(ListNode head, int val)
        {
            while(head != null)
            {
                if(val == head.val)
                {
                    head = head.next;
                }
                else
                {
                    break;
                }
            }

            if (head == null)
                return null;

            var current=head;

            while (current.next!= null)
            {
                if (current.next.val == val)
                {
                    current.next = current.next.next;
                }
                else
                {
                    current = current.next;
                }
            }

            return head;
        }

        //213. House Robber II
        public int Rob(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int[] withoutFirst = new int[nums.Length-1];
            for (int i = 0; i < nums.Length - 1; i++)
                withoutFirst[i] = nums[i];
            int[] withoutLast = new int[nums.Length - 1];
            for (int i = 0; i < nums.Length - 1; i++)
                withoutLast[i] = nums[i+1];

            //Return maximum of two results
            return Math.Max(Rob_Line(withoutFirst), Rob_Line(withoutLast));
        }

        public int Rob_Line(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int[] dp = new int[nums.Length];

            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);

            for (int i = 2; i < nums.Length; i++)
            {
                int a = nums[i] + dp[i - 2];
                dp[i] = Math.Max(a, dp[i - 1]);
            }

            return dp[nums.Length - 1];
        }

        //217. Contains Duplicate
        public bool ContainsDuplicate(int[] nums)
        {
            var dist= nums.Distinct();
            return dist.Count()!=nums.Length;
        }

        //240
        public bool SearchMatrix(int[,] matrix, int target)
        {
            if (matrix == null) return false;

            return SerachMatrix(matrix, 0, matrix.GetLength(0), target);
        }
        public bool SerachMatrix(int[,] matrix,int startRowIndex, int endColIndex, int target)
        {
            if (matrix == null) return false;
            int col = matrix.GetLength(0);
            int row = matrix.GetLength(1);

            for (int i = startRowIndex; i < row; i++)
            {
                for(int j = 0; j < endColIndex; j++)
                {
                    if (matrix[i, j] == target) return true;
                    if (matrix[i, j] > target) return SerachMatrix(matrix, i, j, target);
                }
            }
            return false;
        }

        //242. Valid Anagram

        public bool IsAnagram(string s, string t)
        {
            var arr1 = s.ToArray().ToList();
            var arr2 = t.ToArray().ToList();
            while (arr1.Count > 0)
            {
                var j = arr2.IndexOf(arr1[0]);
                if (j >= 0)
                {
                    arr1.RemoveAt(0);
                    arr2.RemoveAt(j);
                }
                else
                {
                    return false;
                }
            }
            return arr2.Count==0;
        }

        //258
        public int AddDigits(int num)
        {
            if (num < 10) return num;

            int total = 0;
            while (num >= 10)
            {
                total+= num % 10;
                num /= 10;
            }
            total += num;

            return AddDigits(total);
        }

        //278. First Bad Version

        public int FirstBadVersion(int n)
        {
            return FirstBadVersion(1,n);
        }

        public int FirstBadVersion(int start, int end)
        {
            if (start == end) return start;

            int num = end - start + 1;
            int mid = num / 2 + start - 1;

            if (IsBadVersion(mid))
            {
                return FirstBadVersion(start, mid);
            }
            else
            {
                return FirstBadVersion(mid + 1, end);
            }
        }

        public bool IsBadVersion(int n)
        {
            int bad = 10;
            return (n >= bad);
        }

        //283. Move Zeroes
        public void MoveZeroes(int[] nums)
        {
            if(nums == null||nums.Length==0)
                return;

            int zeroCount = 0;
            for(int i = 0; i < nums.Length-1; i++)
            {
                if (i + zeroCount >= nums.Length-1)
                    break;

                while(nums[i] == 0)
                {
                    for(int j = i;j < nums.Length - 1 -zeroCount; j++)
                    {
                        nums[j] = nums[j+1];
                    }

                    nums[nums.Length-1-zeroCount] = 0;

                    zeroCount++;

                    if (i + zeroCount >= nums.Length - 1)
                        break;
                }
            }

            Console.WriteLine($"zer0Count = {zeroCount}");
        }

    }
}
