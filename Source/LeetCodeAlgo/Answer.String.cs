using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        private bool isSubSequence_forward(string s, string t)
        {
            if (t.Length > s.Length) return false;
            int n = s.Length;
            int j = 0;
            for(int i = 0; i < n && j<t.Length; i++)
            {
                if (s[i] == t[j])
                    j++;
            }
            return j == t.Length;
        }

        private bool isSubSequence_binarySearch(List<int>[] posArr, string s, string t)
        {
            int n = s.Length;
            int curr = 0;
            for (int i = 0; i < t.Length; i++)
            {
                if (curr >= n) return false;
                var list = posArr[t[i] - 'a'];
                if (list.Count == 0 || list.Last() < curr) return false;
                int left = 0;
                int right = list.Count - 1;
                while (left < right)
                {
                    int mid = (left + right) / 2;
                    if (list[mid] >= curr)
                        right = mid;
                    else
                        left = mid + 1;
                }
                curr = list[left] + 1;
            }
            return true;
        }


    }
}
