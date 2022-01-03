using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //611
        public int TriangleNumber(int[] nums)
        {
            Array.Sort(nums);
            int count = 0;
            int i, j, k;
            for (i = 0; i < nums.Length-2; i++)
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

        public bool IsTriangle(int num1,int num2,int num3)
        {
            return num3 < num1 + num2;
        }
        //697
        public int FindShortestSubArray(int[] nums)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dictionary.ContainsKey(nums[i]))
                {
                    dictionary[nums[i]]++;
                }
                else
                {
                    dictionary.Add(nums[i], 1);
                }
            }

            int frequency = dictionary.Values.Max();
            var candidate = dictionary.Where(o => o.Value == frequency).Select(o=>o.Key);
            int minLength = nums.Length;
            foreach(var i in candidate)
            {
                int length = GetLastIndex(nums, i)- GetFirstIndex(nums, i)+1;
                minLength = length < minLength ? length : minLength;
            }
            return minLength;
        }
        public int GetFirstIndex(int[] nums,int key)
        {
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == key) return i;
            }
            throw new ArgumentOutOfRangeException();
        }
        public int GetLastIndex(int[] nums, int key)
        {
            for (int i = nums.Length-1; i >=0; i--)
            {
                if (nums[i] == key) return i;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
