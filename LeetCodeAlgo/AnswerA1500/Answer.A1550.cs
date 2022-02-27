using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1557. Minimum Number of Vertices to Reach All Nodes
        ///Given a directed acyclic graph, with n vertices numbered from 0 to n-1,
        ///and an array edges where edges[i] = [fromi, toi] represents a directed edge from node fromi to node toi.
        ///Find the smallest set of vertices from which all nodes in the graph are reachable.
        public IList<int> FindSmallestSetOfVertices(int n, IList<IList<int>> edges)
        {
            //find all unreachable points
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

        ///1576. Replace All ?'s to Avoid Consecutive Repeating Characters
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
    }
}
