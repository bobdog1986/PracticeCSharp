using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2053. Kth Distinct String in an Array
        ///Given an array of strings arr, and an integer k, return the kth distinct string present in arr.
        ///If there are fewer than k distinct strings, return an empty string "".
        public string KthDistinct(string[] arr, int k)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach(var w in arr)
            {
                if(dict.ContainsKey(w))dict[w] ++;
                else dict.Add(w,1);
            }
            foreach(var d in dict)
            {
                if (d.Value > 1) continue;
                if (--k == 0) return d.Key;
            }
            return string.Empty;
        }
        ///2057. Smallest Index With Equal Value
        ///Given a 0-indexed integer array nums, return the smallest index i of nums such that
        ///i mod 10 == nums[i], or -1 if such index does not exist.
        public int SmallestEqual(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
                if (nums[i] == i % 10) return i;
            return -1;
        }

        ///2058. Find the Minimum and Maximum Number of Nodes Between Critical Points
        ///A critical point in a linked list is defined as either a local maxima or a local minima.
        public int[] NodesBetweenCriticalPoints(ListNode head)
        {
            int min = int.MaxValue;
            ListNode prev = null;
            List<int> list=new List<int>();
            int index = 1;
            var node=head;
            while(node != null && node.next != null)
            {
                if (prev != null)
                {
                    if((node.val>prev.val && node.val>node.next.val)
                        ||(node.val < prev.val && node.val < node.next.val))
                    {
                        if (list.Count > 0) min = Math.Min(min, index - list.Last());
                        list.Add(index);
                    }
                }
                index++;
                prev = node;
                node = node.next;
            }
            return list.Count >= 2 ? new int[] { min, list.Last() - list.First() } : new int[] { -1, -1 };
        }
        /// 2063. Vowels of All Substrings, O(n)
        ///Given a string word, return the sum of the number of vowels ('a', 'e', 'i', 'o', and 'u') in every substring of word.
        public long CountVowels(string word)
        {
            //For each vowels s[i], it could be in the substring starting at s[x] and ending at s[y],
            //where 0 <= x <= i and i <= y < n ,that is (i + 1) choices for x and (n - i) choices for y.
            //So there are(i + 1) * (n - i) substrings containing s[i]
            HashSet<char> set = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
            long res = 0, n = word.Length;
            for (int i = 0; i < n; i++)
                if (set.Contains(word[i]))
                    res += (i + 1) * (n - i);
            return res;

        }

        /// 2068. Check Whether Two Strings are Almost Equivalent
        ///Given two strings word1 and word2, each of length n, return true if word1 and word2 are almost equivalent or false
        ///differences between the frequencies of each letter from 'a' to 'z' between word1 and word2 is at most 3.

        public bool CheckAlmostEquivalent(string word1, string word2)
        {
            int[] arr=new int[26];
            for(int i=0; i<word1.Length; i++)
            {
                arr[word1[i] - 'a']++;
                arr[word2[i] - 'a']--;
            }
            foreach(var x in arr)
                if (x > 3 || x < -3) return false;
            return true;
        }
        ///2078. Two Furthest Houses With Different Colors
        ///There are n houses evenly lined up on the street, and each house is beautifully painted.
        ///You are given a 0-indexed integer array colors of length n, where colors[i] represents
        ///the color of the ith house.Return the maximum distance between two houses with different colors.
        ///The distance between the ith and jth houses is abs(i - j), where abs(x) is the absolute value of x.
        public int MaxDistance_2078_O_n(int[] colors)
        {
            ///Find the last house with different color of the fisrt house.
            ///Find the first house with different color of the last house.
            ///Return the max distance of these two options.
            int n = colors.Length, i = 0, j = n - 1;
            while (colors[0] == colors[j]) j--;
            while (colors[n - 1] == colors[i]) i++;
            return Math.Max(n - 1 - i, j);
        }

        public int MaxDistance_2078(int[] colors)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int res = 0;
            for(int i = 0; i < colors.Length; i++)
            {
                if(!dict.ContainsKey(colors[i]))dict.Add(colors[i], i);
                foreach(var key in dict.Keys)
                {
                    if (key != colors[i])
                        res = Math.Max(res, i - dict[key]);
                }
            }
            return res;
        }

        /// 2085. Count Common Words With One Occurrence
        ///return the number of strings that appear exactly once in each of the two arrays.
        public int CountWords(string[] words1, string[] words2)
        {
            Dictionary<string, int> dict1 = new Dictionary<string, int>();
            Dictionary<string, int> dict2 = new Dictionary<string, int>();
            foreach(var w1 in words1)
            {
                if (dict1.ContainsKey(w1)) dict1[w1]++;
                else dict1.Add(w1,1);
            }
            foreach (var w2 in words2)
            {
                if (dict2.ContainsKey(w2)) dict2[w2]++;
                else dict2.Add(w2, 1);
            }
            var keys1=dict1.Where(x=>x.Value==1).Select(x=>x.Key).ToList();
            int count = 0;
            foreach(var key in keys1)
            {
                if (dict2.ContainsKey(key) && dict2[key] == 1) count++;
            }
            return count;
        }

        /// 2086. Minimum Number of Buckets Required to Collect Rainwater from Houses
        ///H is house, . is space
        ///The rainwater from a house at index i is collected if a bucket is placed at index i - 1 and/or index i + 1.
        ///Return the minimum number of buckets for every house at least one bucket collecting rainwater, or -1 if it is impossible.
        ///1 <= street.length <= 105
        public int MinimumBuckets(string street)
        {
            int ans = 0;
            var arr = street.ToCharArray();
            bool[] buckets=new bool[arr.Length];
            for(int i=0; i<arr.Length; i++)
            {
                //if start with 'HH...' or end with '...HH' or contain 'HHH', return -1
                if (arr[i]=='H'
                    &&(i==0 || arr[i-1]=='H')
                    &&(i==arr.Length-1 || arr[i+1]=='H'))
                    return -1;

                if (arr[i] == 'H')
                {
                    if (i >0 && buckets[i-1])
                    {
                        continue;
                    }
                    ans++;
                    if (i < arr.Length - 1)
                    {
                        buckets[i+1] = true;
                    }
                }
            }
            return ans;
        }

        ///2089. Find Target Indices After Sorting Array
        public IList<int> TargetIndices(int[] nums, int target)
        {
            var ans=new List<int>();
            Array.Sort(nums);
            for(int i=0; i<nums.Length; i++)
            {
                if (nums[i] > target) break;
                if (nums[i] == target) ans.Add(i);
            }
            return ans;
        }

        ///2094. Finding 3-Digit Even Numbers
        ///if the given digits were [1, 2, 3], integers 132 and 312 follow the requirements.
        ///Return a sorted array of the unique integers.
        public int[] FindEvenNumbers(int[] digits)
        {
            List<int> ans=new List<int>();
            int[] arr=new int[10];
            foreach (var d in digits)
                arr[d]++;

            for(int i = 100; i <= 998; i += 2)
            {
                int[] curr = new int[10];
                curr[i / 100]++;
                curr[i / 10 % 10]++;
                curr[i % 10]++;
                bool valid = true;
                for(int j = 0; j < curr.Length; j++)
                {
                    if(curr[j] > arr[j])
                    {
                        valid = false;
                        break;
                    }
                }
                if(valid) ans.Add(i);
            }
            return ans.ToArray();
        }

        ///2095. Delete the Middle Node of a Linked List
        ///You are given the head of a linked list. Delete the middle node, and return the head of the modified linked list.
        ///The middle node of a linked list of size n is the ⌊n / 2⌋th node from the start using 0-based indexing,
        ///where ⌊x⌋ denotes the largest integer less than or equal to x.
        public ListNode DeleteMiddle(ListNode head)
        {
            int count = 0;
            var node = head;
            while (node != null)
            {
                count++;
                node = node.next;
            }
            if (count == 1) return null;
            int i = (count) / 2;
            node = head;
            while (--i > 0)
                node = node.next;
            node.next = node.next.next;
            return head;
        }
    }
}
