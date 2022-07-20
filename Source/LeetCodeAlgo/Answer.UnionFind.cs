using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    //Impl of UnionFind
    public class UnionFind
    {
        public int[] parent;
        public int[] rank;
        public int GroupCount;
        public UnionFind(int n)
        {
            GroupCount = n;
            parent = new int[n];
            rank = new int[n];
            for(int i = 0; i < n; i++)
                parent[i] = i;
        }

        public int Find(int i)
        {
            while (parent[i] != i)
                i = parent[i];
            return i;
        }

        public bool IsConnected(int x, int y)
        {
            return Find(x) == Find(y);
        }

        public bool Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);
            if (rootP == rootQ) return false;
            if (rank[rootQ] > rank[rootP])
            {
                parent[rootP] = rootQ;
            }
            else
            {
                parent[rootQ] = rootP;
                if (rank[rootP] == rank[rootQ])
                    rank[rootP]++;
            }
            GroupCount--;
            return true;
        }
    }

}
