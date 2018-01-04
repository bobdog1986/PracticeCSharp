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
            int i=0, j=1, k=2, l=3;
            for(i = 0; i < nums.Length - 3; i++)
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
                                int code = nums[i] + 10*nums[j] + 100*nums[k] + 1000*nums[l];

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

            return nums[0]+nums[1]*10+nums[2]*100+nums[3]*1000;
        }

        public bool IsSameFourIntArray(int[] first,int[] second)
        {
            for(int i = 0; i < first.Length; i++)
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
        //38
        public string CountAndSay(int n)
        {
            string s = n.ToString();

        }
        //53
        public int MaxSubArray(int[] nums)
        {
            //int result = nums[0], maxEndingHere = nums[0];
            //for (int i = 1; i < nums.Length; ++i)
            //{
            //    maxEndingHere = Math.Max(maxEndingHere + nums[i], nums[i]);
            //    result = Math.Max(result, maxEndingHere);
            //}
            //return result;

            int result = nums[0];
            int maxAdd = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                maxAdd = nums[i];
                result = Math.Max(result, maxAdd);
            }
            return result;

            ///
            //if (nums.Length == 1) return nums[0];
            //if (nums.Length == 2)
            //{
            //    return Math.Max(MaxHead(nums), MaxTail(nums));
            //}

            //int result = nums.Max();

            //for (int i =1; i < nums.Length-1; i++)
            //{
            //    int tail = MaxTail(nums.Slice(0, i));
            //    int head = MaxHead(nums.Slice(i, nums.Length - i));
            //    int added = 0;
            //    if (tail >= 0)
            //    {
            //        added = head > 0 ? tail + head : tail;
            //    }
            //    else
            //    {
            //        if (head >= 0)
            //        {
            //            added = head;
            //        }
            //        else
            //        {
            //            added = tail > head ? tail : head;
            //        }
            //    }

            //    result = added > result ? added : result;
            //}
            //return result;
        }

        public int MaxTail(int[] nums)
        {
            if (nums.Length == 1)
            {
                return nums[0];
            }

            return MaxTail(nums.Slice(0, nums.Length - 1)) > 0 ? nums[nums.Length - 1] + MaxTail(nums.Slice(0, nums.Length - 1)) : nums[nums.Length - 1];
        }

        public int MaxHead(int[] nums)
        {
            if (nums.Length == 1)
            {
                return nums[0];
            }

            return MaxHead(nums.Slice(1, nums.Length - 1)) > 0 ? nums[0] + MaxHead(nums.Slice(1, nums.Length - 1)) : nums[0];
        }

        public int Sum(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            return nums.Aggregate((x, y) => x + y);
        }
    }
}