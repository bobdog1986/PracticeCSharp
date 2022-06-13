using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///911. Online Election, #Binary Search
    public class TopVotedCandidate
    {
        private readonly int[] leads;
        private int n;
        private readonly int[] timeArr;
        public TopVotedCandidate(int[] persons, int[] times)
        {
            n = persons.Length;
            timeArr = times;
            leads = new int[n];
            int max = 0;
            var ticks = new int[n];
            int winner = -1;
            for(int i = 0; i < n; i++)
            {
                ticks[persons[i]]++;
                if(ticks[persons[i]] >= max)
                {
                    max= ticks[persons[i]];
                    winner = persons[i];
                }
                leads[i] = winner;
            }
        }

        public int Q(int t)
        {
            if (t == timeArr[0]) return leads[0];
            else if (t >= timeArr[n-1])return leads[n-1];
            else
            {
                int left = 0;
                int right = n - 1;
                while (left < right)
                {
                    int mid = (left + right+1) / 2;
                    if (timeArr[mid] == t) return leads[mid];
                    else if(timeArr[mid] > t)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid;
                    }
                }
                return leads[left];
            }
        }
    }
}
