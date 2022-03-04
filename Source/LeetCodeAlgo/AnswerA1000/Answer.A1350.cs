using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1365. How Many Numbers Are Smaller Than the Current Number
        ///for each nums[i] find out how many numbers in the array are smaller than it.
        ///0 <= nums[i] <= 100, 2 <= nums.length <= 500
        public int[] SmallerNumbersThanCurrent(int[] nums)
        {
            int[] arr=new int[101];
            Dictionary<int, List<int>> map=new Dictionary<int, List<int>>();
            for(int i = 0; i < nums.Length; i++)
            {
                arr[nums[i]]++;
                if(!map.ContainsKey(nums[i]))
                    map[nums[i]] = new List<int>();
                map[nums[i]].Add(i);
            }

            int count=nums.Length;
            int[] ans = new int[nums.Length];
            for(int i=arr.Length-1; i>=0 && count>0; i--)
            {
                if (arr[i] == 0) continue;
                count-=arr[i];
                foreach(var index in map[i])
                    ans[index] = count;
            }
            return ans;
        }
        /// 1366. Rank Teams by Votes
        ///Return a string of all teams sorted by the ranking system.
        public string RankTeams(string[] votes)
        {
            int[,] matrix = new int[26, votes[0].Length];
            foreach(var vote in votes)
            {
                for(int i=0;i<vote.Length;i++)
                {
                    matrix[vote[i] - 'A', i]++;
                }
            }
            var teams= votes[0].ToList();
            teams.Sort((x, y) =>
            {
                int i = 0;
                while (i < votes[0].Length)
                {
                    if(matrix[x-'A',i]> matrix[y-'A', i])
                    {
                        return -1;
                    }
                    else if(matrix[x - 'A', i]< matrix[y - 'A', i])
                    {
                        return 1;
                    }
                    i++;
                }
                //if all same , sort by alphabetically
                return x -y;
            });
            return new string(teams.ToArray());
        }

        ///1375. Number of Times Binary String Is Prefix-Aligned
        ///Return the number of times the binary string is prefix-aligned during the flipping process.
        public int NumTimesAllBlue(int[] flips)
        {
            int right = 0;
            int ans = 0;
            int index = 1;
            foreach(var n in flips)
            {
                right = Math.Max(right, n);
                if (right == index) ans++;
                index++;
            }
            return ans;
        }

        ///1380. Lucky Numbers in a Matrix
        ///A lucky number is an element of the matrix such that it is the minimum element in its row and maximum in its column.
        ///1 <= n, m <= 50 , 1 <= matrix[i][j] <= 10^5.
        public IList<int> LuckyNumbers(int[][] matrix)
        {
            var rowLen = matrix.Length;
            var colLen=matrix[0].Length;
            int[] minOfRows=new int[rowLen];
            for(int i =0;i<rowLen;i++)
                minOfRows[i]=int.MaxValue;
            int[] maxOfCols=new int[colLen];
            for(int i=0;i<rowLen;i++)
                for(int j = 0; j < colLen; j++)
                {
                    minOfRows[i] = Math.Min(minOfRows[i], matrix[i][j]);
                    maxOfCols[j] = Math.Max(maxOfCols[j], matrix[i][j]);
                }
            var ans=new List<int>();
            for (int i = 0; i < rowLen; i++)
                for (int j = 0; j < colLen; j++)
                {
                    if(matrix[i][j] == minOfRows[i] && matrix[i][j] == maxOfCols[j])
                        ans.Add(matrix[i][j]);
                }
            return ans;
        }

        ///1394. Find Lucky Integer in an Array
        ///Given an array of integers arr, a lucky integer is an integer that has a frequency in the array equal to its value.
        ///Return the largest lucky integer in the array.If there is no lucky integer return -1.
        public int FindLucky(int[] arr)
        {
            Dictionary<int,int> map = new Dictionary<int,int>();
            foreach(var n in arr)
            {
                if (map.ContainsKey(n)) map[n]++;
                else map.Add(n, 1);
            }
            var keys=map.Keys.OrderBy(x=>-x);
            foreach(var key in keys)
                if (map[key] == key) return key;
            return -1;
        }

    }
}
