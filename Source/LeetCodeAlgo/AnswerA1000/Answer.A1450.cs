using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1455. Check If a Word Occurs As a Prefix of Any Word in a Sentence
        public int IsPrefixOfWord(string sentence, string searchWord)
        {
            var arr=sentence.Split(' ');
            for(int i=0; i<arr.Length; i++)
            {
                if (arr[i].StartsWith(searchWord)) return i+1;
            }
            return -1;
        }
        /// 1464. Maximum Product of Two Elements in an Array
        ///Return the maximum value of (nums[i]-1)*(nums[j]-1).
        ///2 <= nums.length <= 500, 1 <= nums[i] <= 10^3
        public int MaxProduct(int[] nums)
        {
            int max1 = Math.Max(nums[0], nums[1]);
            int max2 = Math.Min(nums[0], nums[1]);
            for (int i = 2; i < nums.Length; i++)
            {
                if (nums[i] >= max1)
                {
                    max2 = max1;
                    max1 = nums[i];
                }
                else if (nums[i] > max2)
                {
                    max2 = nums[i];
                }
            }
            return (max1 - 1) * (max2 - 1);
        }
        ///1466. Reorder Routes to Make All Paths Lead to the City Zero, #Graph, #DFS
        public int MinReorder(int n, int[][] connections)
        {
            List<int>[] graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }
            foreach (var conn in connections)
            {
                graph[conn[0]].Add(conn[1]);
                graph[conn[1]].Add(-conn[0]);
            }
            var res = MinReorder_dfs(graph, new bool[n], 0);
            return res;
        }

        public int MinReorder_dfs(List<int>[] graph, bool[] visited, int from)
        {
            int change = 0;
            visited[from] = true;
            foreach (var to in graph[from])
                if (!visited[Math.Abs(to)])
                    change += MinReorder_dfs(graph, visited, Math.Abs(to)) + (to > 0 ? 1 : 0);
            return change;
        }

        ///1470. Shuffle the Array
        ///Given the array nums consisting of 2n elements in the form [x1,x2,...,xn,y1,y2,...,yn].
        ///Return the array in the form[x1, y1, x2, y2, ..., xn, yn].
        public int[] Shuffle(int[] nums, int n)
        {
            int[] res=new int[n*2];
            for(int i = 0; i < n ; i++)
            {
                res[i * 2] = nums[i];
                res[i * 2 + 1] = nums[i + n ];
            }
            return res;
        }

        ///1472. Design Browser History, see BrowserHistory

        /// 1476. Subrectangle Queries, see SubrectangleQueries

        ///1480. Running Sum of 1d Array
        public int[] RunningSum(int[] nums)
        {
            int sum = 0;
            for(int i = 0; i < nums.Length; ++i)
            {
                sum += nums[i];
                nums[i] = sum;
            }
            return nums;
        }
        /// 1482. Minimum Number of Days to Make m Bouquets , ### Binary Search
        ///You want to make m bouquets. To make a bouquet, you need to use k adjacent flowers from the garden.
        ///The garden consists of n flowers, the ith flower will bloom in the bloomDay[i] and then can be used in exactly one bouquet.
        ///Return the minimum number of days you need to wait to be able to make m bouquets from the garden.
        ///If it is impossible to make m bouquets return -1.
        ///1 <= bloomDay.length <= 10^5, 1 <= bloomDay[i] <= 10^9, 1 <= m <= 10^6, 1 <= k <= bloomDay.length
        public int MinDays(int[] bloomDay, int m, int k)
        {
            if (m * k > bloomDay.Length)
                return -1;
            if (m * k == bloomDay.Length)
                return bloomDay.Max();
            int left = 1;
            int right = 1000000000;
            while (left < right)
            {
                int mid = (left + right) / 2;
                int flowers = 0;
                int bouquet = 0;
                for (int i = 0; i < bloomDay.Length; i++)
                {
                    if (bloomDay[i] > mid)
                    {
                        flowers = 0;
                    }
                    else
                    {
                        flowers++;
                        if (flowers >= k)
                        {
                            bouquet++;
                            flowers = 0;
                        }
                    }
                }
                if (bouquet < m)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }
            return left;
        }
        ///1491. Average Salary Excluding the Minimum and Maximum Salary
        ///1000 <= salary[i] <= 10^6,
        public double Average(int[] salary)
        {
            int sum = 0;
            int max = 1000;
            int min = 1000000;
            foreach(var n in salary)
            {
                max = Math.Max(max, n);
                min = Math.Min(min, n);
                sum += n;
            }

            sum = sum - max - min;

            return sum*1.0 / (salary.Length - 2);
        }

        ///1492. The kth Factor of n
        ///You are given two positive integers n and k. A factor of an integer n is defined as an integer i where n % i == 0.
        ///Consider a list of all factors of n sorted in ascending order, return the kth factor in this list or return -1 if n has less than k factors.
        public int KthFactor(int n, int k)
        {
            var list = new List<int>();
            for (int i = 1; i <= n; i++)
                if (n % i == 0) list.Add(i);
            return list.Count >= k ? list[k - 1] : -1;
        }

    }
}
