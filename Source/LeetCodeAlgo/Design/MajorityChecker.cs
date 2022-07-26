using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1157. Online Majority Element In Subarray, #Binary Search, #HashMap
    //Design a data structure that efficiently finds the majority element of a given subarray.
    //The majority element of a subarray is an element that occurs >= threshold times.
    //int query(int left, int right, int threshold)
    //returns the majority element in the subarray arr[left...right] or -1 if no such element exists.
    public class MajorityChecker
    {
        private readonly Dictionary<int, List<int>> dict;
        private readonly Dictionary<int, int[]> visit;

        public MajorityChecker(int[] arr)
        {
            visit = new Dictionary<int, int[]>();//cache query results to avoid TLE
            dict = new Dictionary<int, List<int>>();//store {value, indexes} pair
            for (int i = 0; i < arr.Length; i++)
            {
                if (!dict.ContainsKey(arr[i]))
                    dict.Add(arr[i], new List<int>());
                dict[arr[i]].Add(i);
            }
        }

        public int Query(int left, int right, int threshold)
        {
            //0 <= left <= right < arr.length [1,20000], we use x as key of visited query
            int x = left * 20000 + right;
            if (visit.ContainsKey(x))
            {
                //visit[x] =new int[3]{ k , validThreshold , invalidThreshold}
                //if validThreshold >= threshold return k
                //else if invalidThreshold<=threshold return -1
                if (visit[x][1] >= threshold) return visit[x][0];
                else if (visit[x][2] <= threshold) return -1;
            }
            else visit.Add(x, new int[3] { 0, int.MinValue, int.MaxValue });

            foreach (var k in dict.Keys)
            {
                int n = dict[k].Count;
                if (n < threshold) continue;//skip invalid k
                if (dict[k][0] > right || dict[k][n - 1] < left) continue;//skip invalid k
                //find lowest index which >=left
                int low1 = 0;
                int high1 = dict[k].Count - 1;
                while (low1 < high1)
                {
                    int mid = (low1 + high1) / 2;
                    if (dict[k][mid] >= left)
                    {
                        high1 = mid;
                    }
                    else
                    {
                        low1 = mid + 1;
                    }
                }
                //find highest index which <=right
                int low2 = 0;
                int high2 = dict[k].Count - 1;
                while (low2 < high2)
                {
                    int mid = (low2 + high2 + 1) / 2;
                    if (dict[k][mid] <= right)
                    {
                        low2 = mid;
                    }
                    else
                    {
                        high2 = mid - 1;
                    }
                }
                //total count in [low1,low2] is low2-low1+1
                if (low2 - low1 + 1 >= threshold)
                {
                    //if found , we need update k and validThreshold
                    visit[x][0] = k;
                    visit[x][1] = threshold;
                    return k;
                }
            }
            //update invalidThreshold
            visit[x][2] = threshold;
            return -1;
        }
    }
}