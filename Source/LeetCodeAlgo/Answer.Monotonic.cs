using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    //all monotonic impl
    public partial class Answer
    {
        //find cloest smaller element on left side and right side
        private int[] getLeftSmallerMonotonicArr(int[] nums)
        {
            int n = nums.Length;
            int[] arr = new int[n];
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < n; i++)
            {
                while (stack.Count > 0 && nums[stack.Peek()] >= nums[i])
                    stack.Pop();
                arr[i] = stack.Count == 0 ? -1 : stack.Peek();
                stack.Push(i);
            }
            return arr;
        }

        private int[] getRightSmallerMonotonicArr(int[] nums)
        {
            int n = nums.Length;
            int[] arr = new int[n];
            Stack<int> stack = new Stack<int>();
            for (int i = n - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && nums[stack.Peek()] >= nums[i])
                    stack.Pop();
                arr[i] = stack.Count == 0 ? n : stack.Peek();
                stack.Push(i);
            }
            return arr;
        }


        //find cloest bigger element on left side and right side
        //
        //
        //
        private int[] getLeftBiggerMonotonicArr(int[] nums)
        {
            int n = nums.Length;
            int[] arr = new int[n];
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < n; i++)
            {
                while (stack.Count > 0 && nums[stack.Peek()] <= nums[i])
                    stack.Pop();
                arr[i] = stack.Count == 0 ? -1 : stack.Peek();
                stack.Push(i);
            }
            return arr;
        }

        private int[] getRightBiggerMonotonicArr(int[] nums)
        {
            int n = nums.Length;
            int[] arr = new int[n];
            Stack<int> stack = new Stack<int>();
            for (int i = n - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && nums[stack.Peek()] <= nums[i])
                    stack.Pop();
                arr[i] = stack.Count == 0 ? n : stack.Peek();
                stack.Push(i);
            }
            return arr;
        }
    }
}
