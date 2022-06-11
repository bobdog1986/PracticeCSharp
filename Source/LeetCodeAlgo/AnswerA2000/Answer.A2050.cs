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
        ///2059. Minimum Operations to Convert Number, #BFS
        //if 0 <= x <= 1000, x+nums[i], x-nums[i] , x^nums[i], any time
        public int MinimumOperations(int[] nums, int start, int goal)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            HashSet<int> set = new HashSet<int>();
            int op = 0;
            while(queue.Count > 0)
            {
                int size = queue.Count;
                while (size-- > 0)
                {
                    var top = queue.Dequeue();
                    if (top == goal) return op;
                    if (top < 0 || top > 1000) continue;
                    if (set.Contains(top)) continue;
                    set.Add(top);
                    foreach(var n in nums)
                    {
                        queue.Enqueue(top + n);
                        queue.Enqueue(top - n);
                        queue.Enqueue(top ^ n);
                    }
                }
                op++;
            }
            return -1;
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
        ///2069. Walking Robot Simulation II, see Robot

        /// 2070. Most Beautiful Item for Each Query, #Binary Search
        /// items[i] = [pricei, beautyi] denotes the price and beauty of an item respectively.
        ///For each queries[j], you want to determine the maximum beauty of an item whose price is less than or equal to queries[j].
        ///If no such item exists, then the answer to this query is 0.
        ///Return an array answer of the same length as queries where answer[j] is the answer to the jth query.
        public int[] MaximumBeauty(int[][] items, int[] queries)
        {
            items = items.OrderBy(x => x[0]).ToArray();

            int[][] arr = new int[items.Length][];
            int beauty = int.MinValue; ;
            for(int i = 0; i < items.Length; i++)
            {
                beauty = Math.Max(beauty, items[i][1]);
                arr[i] = new int[] { items[i][0], beauty };
            }

            var res = new int[queries.Length];
            for (int i=0;i< queries.Length; i++)
            {
                if (queries[i] < arr[0][0])
                {
                    res[i] = 0;
                    continue;
                }
                if (queries[i] >= arr[items.Length - 1][0])
                {
                    res[i] = arr[items.Length - 1][1];
                    continue;
                }

                int left = 0;
                int right = items.Length - 1;
                while (left < right)
                {
                    int mid = (left + right+1) / 2;
                    if (arr[mid][0] > queries[i])
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid;
                    }
                }
                res[i] = arr[left][1];
            }
            return res;
        }
        /// 2073. Time Needed to Buy Tickets
        public int TimeRequiredToBuy(int[] tickets, int k)
        {
            int res = 0;
            int count = tickets[k];
            for(int i = 0; i < tickets.Length; i++)
            {
                if (i <= k)
                {
                    res += Math.Min(tickets[i], count);
                }
                else
                {
                    res += Math.Min(tickets[i], count - 1);
                }
            }
            return res;
        }
        /// 2078. Two Furthest Houses With Different Colors
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

        ///2079. Watering Plants
        ///plants[i] is the amount of water the ith plant needs, capacity representing the watering capacity
        ///return the number of steps needed to water all the plants.
        public int WateringPlants(int[] plants, int capacity)
        {
            int res = 0;
            int index = -1;
            int curr = capacity;
            for (int i = 0; i < plants.Length; i++)
            {
                res += i - index;
                curr -= plants[i];
                index = i;
                if (i < plants.Length - 1 && curr < plants[i + 1])
                {
                    index = -1;
                    curr = capacity;
                    res += i + 1;
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

        ///2091. Removing Minimum and Maximum From Array
        ///A deletion is defined as either removing an element from the front of
        ///the array or removing an element from the back of the array.
        ///Return the minimum of deletions to remove both the minimum and maximum element from the array.
        public int MinimumDeletions(int[] nums)
        {
            int max = int.MinValue;
            int min = int.MaxValue;
            int maxIndex = 0;
            int minIndex = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if(nums[i] > max)
                {
                    max = nums[i];
                    maxIndex = i;
                }
                if (nums[i] < min)
                {
                    min = nums[i];
                    minIndex = i;
                }
            }

            int left = Math.Min(maxIndex, minIndex);
            int right = Math.Max(maxIndex, minIndex);

            return Math.Min(right + 1, Math.Min(nums.Length - left, left+1+ nums.Length - right));
        }
        /// 2094. Finding 3-Digit Even Numbers
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

        ///2099. Find Subsequence of Length K With the Largest Sum, #PriorityQueue
        public int[] MaxSubsequence(int[] nums, int k)
        {
            PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
            for(int i = 0; i < nums.Length; i++)
                pq.Enqueue(i, -nums[i]);

            var list = new List<int>();
            while (k-- > 0)
                list.Add(pq.Dequeue());

            return list.OrderBy(x => x).Select(x => nums[x]).ToArray();

        }


        ///2100. Find Good Days to Rob the Bank, #Prefix Sum
        ///The ith day is a good day to rob the bank if:
        ///There are at least time days before and after the ith day,
        ///The number of guards at the bank for the time days before i are non-increasing, and
        ///The number of guards at the bank for the time days after i are non-decreasing.
        public IList<int> GoodDaysToRobBank(int[] security, int time)
        {
            int n = security.Length;
            //store how many count of continuous days meet the requires
            int[] left = new int[n];
            int[] right = new int[n];
            var res = new List<int>();

            for (int i = 1; i < n; i++)
            {
                left[i] = security[i] <= security[i - 1] ? left[i - 1] + 1 : 0;
            }

            for (int i = n - 2; i >= 0; i--)
            {
                right[i] = security[i] <= security[i + 1] ? right[i + 1] + 1 : 0;
            }

            for (int i = time; i < n - time; i++)
            {
                // both left and right bounds are time indices away
                if (left[i] >= time && right[i] >= time)
                    res.Add(i);
            }

            return res;
        }
    }
}
