using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2657. Find the Prefix Common Array of Two Arrays
        //public int[] FindThePrefixCommonArray(int[] A, int[] B)
        //{
        //    int n = A.Length;
        //    int[] arr = new int[n+1];
        //    int[] res = new int[n];
        //    int count = 0;
        //    for (int i = 0; i<n; i++)
        //    {
        //        arr[A[i]]++;
        //        if (arr[A[i]]==2) count++;
        //        arr[B[i]]++;
        //        if (arr[B[i]]==2) count++;
        //        res[i]=count;
        //    }

        //    return res;
        //}

        ///2658. Maximum Number of Fish in a Grid
        //public int FindMaxFish(int[][] grid)
        //{
        //    int max = 0;
        //    int m = grid.Length;
        //    int n = grid[0].Length;
        //    for (int i = 0; i<m; i++)
        //    {
        //        for (int j = 0; j<n; j++)
        //        {
        //            if (grid[i][j]>0)
        //            {
        //                int sum = 0;
        //                FindMaxFish(grid, i, j, ref sum);
        //                max=Math.Max(max, sum);
        //            }
        //        }
        //    }

        //    return max;
        //}

        //private void FindMaxFish(int[][] grid, int i, int j, ref int sum)
        //{
        //    int m = grid.Length;
        //    int n = grid[0].Length;
        //    if (grid[i][j]==0) return;
        //    sum = sum+grid[i][j];
        //    grid[i][j] = 0;
        //    int[][] dxy4 = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
        //    foreach (var x in dxy4)
        //    {
        //        int r = i+x[0];
        //        int c = j+x[1];
        //        if (r>=0&&r<m && c>=0 && c<n && grid[r][c]>0)
        //        {
        //            FindMaxFish(grid, r, c, ref sum);
        //        }
        //    }
        //}

        ///2679. Sum in a Matrix
        //public int MatrixSum(int[][] nums)
        //{
        //    int res = 0;
        //    int m = nums.Length;
        //    int n = nums[0].Length;
        //    for (int i = 0; i<m; i++)
        //    {
        //        nums[i]=nums[i].OrderBy(x => -x).ToArray();
        //    }

        //    for (int j = 0; j<n; j++)
        //    {
        //        int max = 0;
        //        for (int i = 0; i<m; i++)
        //        {
        //            max=Math.Max(max, nums[i][j]);
        //        }
        //        res+=max;
        //    }

        //    return res;
        //}

        ///2680. Maximum OR, #Prefix Sum
        //public long MaximumOr(int[] nums, int k)
        //{
        //    int n = nums.Length;
        //    int[] arr1 = new int[n];
        //    int or1 = 0;
        //    for (int i = 0; i<n; i++)
        //    {
        //        arr1[i]=or1;
        //        or1|=nums[i];
        //    }
        //    int[] arr2 = new int[n];
        //    int or2 = 0;
        //    for (int i = n-1; i>=0; i--)
        //    {
        //        arr2[i]=or2;
        //        or2|=nums[i];
        //    }
        //    long res = 0;
        //    for (int i = 0; i<n; i++)
        //    {
        //        long curr = ((long)nums[i])<<k;
        //        res=Math.Max(res, curr|arr1[i]|arr2[i]);
        //    }
        //    return res;
        //}

        ///2683. Neighboring Bitwise XOR
        //public bool DoesValidArrayExist(int[] derived)
        //{
        //    int n = derived.Length;
        //    int[] arr = new int[n];
        //    for (int i = 1; i<n; i++)
        //    {
        //        arr[i]= arr[i-1]^derived[i];
        //    }
        //    if (arr[0] == (arr[n-1]^derived[0])) return true;
        //    arr[0]=1;
        //    for (int i = 1; i<n; i++)
        //    {
        //        arr[i]= arr[i-1]^derived[i];
        //    }
        //    if (arr[0] == (arr[n-1]^derived[0])) return true;
        //    return false; ;
        //}
    }
}
