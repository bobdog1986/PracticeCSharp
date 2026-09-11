using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        /// 3483. Unique 3-Digit Even Numbers
        public int TotalNumbers(int[] digits)
        {
            var set = new HashSet<int>();
            int n = digits.Length;
            for (int i = 0; i<n; i++)
            {
                if (digits[i]==0) continue;
                for (int j = 0; j<n; j++)
                {
                    if (j==i) continue;
                    for (int k = 0; k<n; k++)
                    {
                        if (i==k||j==k) continue;
                        int a = digits[i]*100+digits[j]*10+digits[k];
                        if (a%2==0)
                            set.Add(a);
                    }
                }
            }

            return set.Count();
        }
    }
}
