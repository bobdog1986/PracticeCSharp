using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1366. Rank Teams by Votes
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
