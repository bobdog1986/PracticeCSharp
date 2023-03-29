using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2600. K Items With the Maximum Sum
        //public int KItemsWithMaximumSum(int numOnes, int numZeros, int numNegOnes, int k)
        //{
        //    if (numOnes>=k) return k;
        //    else if (numZeros+numOnes>=k) return numOnes;
        //    else return numOnes-(k-numZeros-numOnes);
        //}

        ///2601. Prime Subtraction Operation, #Prime
        // Pick an index i that haven’t picked, and pick a prime p strictly less than nums[i], then subtract p from nums[i].
        // Return true if you can make nums a strictly increasing array using the above operation and false otherwise.
        public bool PrimeSubOperation(int[] nums)
        {
            int n = nums.Length;
            int max = nums.Max();
            bool[] arr = getPrime(max);

            int prev = 0;
            foreach (var i in nums)
            {
                if (i<=prev)
                    return false;
                prev = PrimeSubOperation(prev, i, arr);
            }
            return true;
        }

        private int PrimeSubOperation(int prev, int curr, bool[] primeArr)
        {
            for (int i = curr-1; i>=0; i--)
            {
                if (primeArr[i] == true) continue;
                if (curr-i<=prev) continue;
                curr-=i;
                break;
            }
            return curr;
        }

        ///2602. Minimum Operations to Make All Array Elements Equal, #BinarySearch
        //For the ith query, you want to make all of the elements of nums equal to queries[i].
        //You can perform the following operation on the array any number of times:
        // - Increase or decrease an element of the array by 1.
        //Return an array where answer[i] is the minimum number of operations to make all elements equal to queries[i].
        public IList<long> MinOperations_2602(int[] nums, int[] queries)
        {
            int n = nums.Length;
            Array.Sort(nums);
            var res = new List<long>();
            long[] prefix = new long[n];
            long sum = 0;
            for (int i = 0; i<n; i++)
            {
                sum+=nums[i];
                prefix[i] = sum;
            }

            foreach (var q in queries)
            {
                if (nums[0]>=q)
                {
                    long ans = prefix[n-1] - 1l*q*n;
                    res.Add(ans);
                }
                else if (nums[n-1]<=q)
                {
                    long ans = 1l*q*n - prefix[n-1];
                    res.Add(ans);
                }
                else
                {
                    int left = 0;
                    int right = n-1;
                    while (left < right)
                    {
                        int mid = (left+right+1)/2;
                        if (nums[mid]<=q)
                        {
                            left = mid;
                        }
                        else
                        {
                            right = mid-1;
                        }
                    }
                    long ans = 1l*q*(left+1) - prefix[left];
                    ans += prefix[n-1]- prefix[left] -1l*q*(n-1-(left+1)+1);
                    res.Add(ans);
                }
            }
            return res;
        }


    }
}
