using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
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
    }
}
