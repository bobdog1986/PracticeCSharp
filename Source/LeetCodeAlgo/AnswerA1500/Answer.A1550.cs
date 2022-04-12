using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1551. Minimum Operations to Make Array Equal
        public int MinOperations(int n)
        {
            int res = 0;
            int left = 0;
            int right = n - 1;
            while(left < right)
                res += right-- - left++;
            return res;
        }
        /// 1557. Minimum Number of Vertices to Reach All Nodes, #Graph
        ///Given a directed acyclic graph, with n vertices numbered from 0 to n-1,
        ///and an array edges where edges[i] = [fromi, toi] represents a directed edge from node fromi to node toi.
        ///Find the smallest set of vertices from which all nodes in the graph are reachable.
        public IList<int> FindSmallestSetOfVertices(int n, IList<IList<int>> edges)
        {
            var ans = new List<int>();
            int[] reachableArr = new int[n];
            for (int i=0;i<edges.Count;i++)
            {
                reachableArr[edges[i][1]]++;
            }
            for (int i = 0; i < reachableArr.Length; i++)
            {
                if (reachableArr[i]==0)
                    ans.Add(i);
            }
            return ans;
        }
        /// 1567. Maximum Length of Subarray With Positive Product
        public int GetMaxLen(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0] > 0 ? 1 : 0;

            int max = 0;

            int count = 0;
            int negCount = 0;
            int negStart = -1;
            int negEnd = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    if (count <= max)
                    {
                    }
                    else
                    {
                        if (negCount % 2 == 0)
                        {
                            max = count;
                        }
                        else
                        {
                            var temp = Math.Max(Math.Max(count - negStart - 1, negStart), Math.Max(count - negEnd - 1, negEnd));
                            max = Math.Max(max, temp);
                        }
                    }

                    count = 0;
                    negCount = 0;
                    negStart = -1;
                    negEnd = 0;
                }
                else
                {
                    if (nums[i] > 0)
                    {
                    }
                    else
                    {
                        if (negStart == -1)
                            negStart = count;

                        negEnd = count;

                        negCount++;
                    }
                    count++;
                }
            }

            if (negCount % 2 == 0)
            {
                max = Math.Max(max, count);
            }
            else
            {
                var temp = Math.Max(Math.Max(count - negStart - 1, negStart), Math.Max(count - negEnd - 1, negEnd));

                max = Math.Max(max, temp);
            }
            return max;
        }

        ///1572. Matrix Diagonal Sum
        ///Given a square matrix mat, return the sum of the matrix diagonals.
        public int DiagonalSum(int[][] mat)
        {
            int res = 0;
            for(int i = 0; i < mat.Length; i++)
            {
                res += mat[i][i];
                res += mat[i][mat.Length - 1 - i];
            }
            if(mat.Length%2==1)
                res-=mat[mat.Length/2][mat.Length / 2];
            return res;
        }
        /// 1576. Replace All ?'s to Avoid Consecutive Repeating Characters
        /// replace ? to not same as previous or next char
        public string ModifyString(string s)
        {
            var arr = s.ToCharArray();
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '?')
                {
                    var c = 'a';
                    while (c <= 'z')
                    {
                        //if not same as previous or next char, update it
                        if ((i == 0 || arr[i - 1]!=c)
                            &&(i==arr.Length-1||arr[i+1]=='?'|| arr[i + 1] != c))
                        {
                            arr[i] = c;
                            break;
                        }
                        c++;
                    }
                }
            }
            return String.Join("", arr);
        }

        ///1588. Sum of All Odd Length Subarrays
        public int SumOddLengthSubarrays(int[] arr)
        {
            int res = 0, n = arr.Length;
            for (int i = 0; i < n; ++i)
            {
                var count= ((i + 1) * (n - i) + 1) / 2;
                res += count * arr[i];
            }
            return res;
        }
    }
}
