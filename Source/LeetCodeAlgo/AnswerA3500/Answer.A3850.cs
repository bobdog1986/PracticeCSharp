using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///3870. Count Commas in Range
        public int CountCommas(int n)
        {
            if (n>=1000)
            {
                return (n/1000-1)*1000 + (n%1000+1);
            }
            else return 0;
        }

        ///3871. Count Commas in Range II
        public long CountCommas(long n)
        {
            long res = 0;

            long a = 1000;
            long b = 1;
            while (a<=n && a<=1_000_000_000_000_000)
            {
                long m = n>=a*1000 ? a*1000-1 : n;
                res += (m/a -1)*a*b + (m%a+1)*b;

                a*=1000;
                b++;
            }

            return res;
        }

        ///3875. Construct Uniform Parity Array I
        ///3876. Construct Uniform Parity Array II
        //public bool UniformArray(int[] nums1)
        //{
        //    Array.Sort(nums1);
        //    int n = nums1.Length;

        //    int[] arr = new int[n];
        //    int odds = 0;
        //    for (int i = 0; i<n; i++)
        //    {
        //        arr[i]=odds;
        //        if (nums1[i]%2!=0)
        //        {
        //            odds++;
        //        }
        //    }
        //    if (odds ==n ||odds==0) return true;
        //    for (int i = 0; i<n; i++)
        //    {
        //        if (nums1[i]%2==0)
        //        {
        //            if (arr[i]==0) return false;
        //        }
        //    }
        //    return true;
        //}

    }
}
