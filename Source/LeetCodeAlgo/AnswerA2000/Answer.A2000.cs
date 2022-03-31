using System.Collections.Generic;
using System;
using System.Linq;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2000. Reverse Prefix of Word
        ///reverse the segment of word that starts at index 0 and ends at the index of the first occurrence of ch (inclusive).
        ///If the character ch does not exist in word, do nothing.
        public string ReversePrefix(string word, char ch)
        {
            List<char> list=new List<char>();
            for(int i = 0; i < word.Length; i++)
            {
                list.Add(word[i]);
                if (ch == word[i])
                {
                    list.Reverse();
                    return new string(list.ToArray()) + word.Substring(i+1);
                }
            }
            return word;
        }

        /// 2001. Number of Pairs of Interchangeable Rectangles
        public long InterchangeableRectangles(int[][] rectangles)
        {
            long sum = 0;
            Dictionary<string, long> pairs = new Dictionary<string, long>();
            foreach (var rect in rectangles)
            {
                var gcb = getGcb(rect[0], rect[1]);
                var key = rect[0] / gcb + ":" + rect[1] / gcb;
                if (pairs.ContainsKey(key))
                    pairs[key]++;
                else
                    pairs.Add(key, 1);
            }
            foreach (var pair in pairs)
            {
                if (pair.Value > 1)
                {
                    sum += pair.Value * (pair.Value - 1) / 2;
                }
            }
            return sum;
        }

        ///2006. Count Number of Pairs With Absolute Difference K
        ///return the number of pairs (i, j) where i < j such that |nums[i] - nums[j]| == k.
        ///1 <= k <= 99, 1 <= nums[i] <= 100, 1 <= nums.length <= 200
        public int CountKDifference(int[] nums, int k)
        {
            if (nums.Length <= 1)
                return 0;
            int[] arr=new int[101];
            int start = 100;
            int end = 1;
            foreach(var num in nums)
            {
                arr[num]++;
                start = Math.Min(start, num);
                end= Math.Max(end, num);
            }
            int ans = 0;
            for (int i = start; i <= end - k; i++)
            {
                if(arr[i] > 0 && arr[i + k] > 0)
                {
                    ans += arr[i] * arr[i + k];
                }
            }
            return ans;
        }

        ///2022. Convert 1D Array Into 2D Array
        public int[][] Construct2DArray(int[] original, int m, int n)
        {
            if (original.Length != m * n)
                return new int[0][] { };
            var ans = new int[m][];
            for (int i = 0; i < m * n; i++)
            {
                if (i % n == 0)
                    ans[i / n] = new int[n];
                ans[i / n][i % n] = original[i];
            }
            return ans;
        }

        ///2023. Number of Pairs of Strings With Concatenation Equal to Target
        ///Given an array of digit strings nums and a digit string target,
        ///return the number of pairs of indices (i, j) (where i != j) such that the concatenation of nums[i] + nums[j] equals target.
        public int NumOfPairs(string[] nums, string target)
        {
            Dictionary<string, List<int>> dict = new Dictionary<string, List<int>>();
            for(int i = 0; i < nums.Length; i++)
            {
                if(!dict.ContainsKey(nums[i]))dict.Add(nums[i], new List<int>());
                dict[nums[i]].Add(i);
            }
            int res = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if (target.StartsWith(nums[i]))
                {
                    var str = target.Substring(nums[i].Length, target.Length - nums[i].Length);
                    if (dict.ContainsKey(str))
                    {
                        res+= dict[str].Where(x=>x!=i).Count();
                    }
                }
            }
            return res;
        }
        /// 2024. Maximize the Confusion of an Exam, #Sliding Window ,#Binary Search
        ///See 424. Longest Repeating Character Replacement
        ///Change the answer key for any question to 'T' or 'F' (i.e., set answerKey[i] to 'T' or 'F').
        ///Return the maximum number of consecutive 'T's or 'F's in the answer key after performing the operation at most k times.
        ///n == answerKey.length, 1 <= n <= 5 * 10^4, answerKey[i] is either 'T' or 'F', 1 <= k <= n
        public int MaxConsecutiveAnswers(string answerKey, int k)
        {
            int maxfreq = 0;
            int left = 0;
            int[] arr = new int[26];
            for (int i = 0; i < answerKey.Length; i++)
            {
                maxfreq = Math.Max(maxfreq, ++arr[answerKey[i] - 'A']);
                if (i - left + 1 > maxfreq + k)
                {
                    arr[answerKey[left] - 'A']--;
                    left++;
                }
            }
            return answerKey.Length - left;
        }

        public int MaxConsecutiveAnswers_BinarySearch(string answerKey, int k)
        {
            int n=answerKey.Length;
            if (n == k)
                return n;

            List<int[]> list=new List<int[]>();
            int countT = 0;
            int countF = 0;
            list.Add(new int[] { 0, 0 });
            for (int i = 0; i < n; i++)
            {
                if(answerKey[i] == 'T')
                {
                    countT++;
                }
                else
                {
                    countF++;
                }
                list.Add(new int[] { countT, countF });
            }

            int left = k+1;
            int right = n;

            int mid = (left + right + 1) / 2;

            while (left < right)
            {
                bool exist = false;
                for(int i = 0; i < n-mid+1; i++)
                {
                    var a = list[i];
                    var b = list[i + mid];

                    int count1 = b[0] - a[0];
                    int count2 = b[1] - a[1];
                    if(count1+k>=mid || count2 + k >= mid)
                    {
                        exist = true;
                        break;
                    }
                }

                if (exist)
                {
                    left = mid;
                    mid= (left + right + 1) / 2;
                }
                else
                {
                    right= mid-1;
                    mid = (left + right + 1) / 2;
                }
            }

            return left;
        }
    }
}