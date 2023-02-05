using LeetCodeAlgo.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///2550. Count Collisions of Monkeys on a Polygon
        public int MonkeyMove(int n)
        {
            //res = 2^n - 2;
            long res = 1;
            long powBase = 2;
            long mod = 1_000_000_007;
            while (n > 0)
            {
                if (n % 2 == 1)
                    res = res * powBase % mod;
                powBase = powBase * powBase % mod;
                n /= 2;
            }
            return (int)((res - 2 + mod) % mod);
        }

        ///2554. Maximum Number of Integers to Choose From a Range I
        // public int MaxCount(int[] banned, int n, int maxSum)
        // {
        //     int res = 0;
        //     int i = 1;
        //     var set = banned.ToHashSet();
        //     int sum = 0;
        //     while (i <= n && sum <= maxSum)
        //     {
        //         if (i + sum > maxSum) break;
        //         if (set.Contains(i))
        //         {
        //             i++;
        //             continue;
        //         }
        //         else
        //         {
        //             res++;
        //             sum += i;
        //             i++;
        //         }
        //     }
        //     return res;
        // }

        ///2559. Count Vowel Strings in Ranges
        // public int[] VowelStrings(string[] words, int[][] queries)
        // {
        //     int m = words.Length;
        //     int[] prefix = new int[m];
        //     HashSet<char> set = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
        //     for (int i = 0; i < m; i++)
        //     {
        //         if (i > 0)
        //             prefix[i] = prefix[i - 1];
        //         if (set.Contains(words[i].First()) && set.Contains(words[i].Last()))
        //             prefix[i]++;
        //     }
        //     int n = queries.Length;
        //     int[] res = new int[n];
        //     for (int i = 0; i < n; i++)
        //     {
        //         int curr = prefix[queries[i][1]];
        //         if (queries[i][0] > 0)
        //             curr -= prefix[queries[i][0] - 1];
        //         res[i] = curr;
        //     }
        //     return res;
        // }

    }
}

