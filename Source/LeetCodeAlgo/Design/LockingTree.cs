using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1993. Operations on Tree
    public class LockingTree
    {
        private readonly List<int>[] toChildGraph;
        private readonly int[] _parent;
        private readonly int[] visit;
        private readonly int[] lockedChilds;
        private readonly int[] lockedParents;
        public LockingTree(int[] parent)
        {
            _parent = parent;
            int n = parent.Length;
            toChildGraph=new List<int>[n];
            visit = new int[n];
            lockedChilds = new int[n];
            lockedParents = new int[n];
            for (int i=0; i < n; i++)
            {
                toChildGraph[i]=new List<int>();
            }

            for (int i=0; i < n; i++)
            {
                if (i>0)
                {
                    toChildGraph[parent[i]].Add(i);
                }
            }
        }

        public bool Lock(int num, int user)
        {
            if (visit[num]==0)
            {
                visit[num]=user;
                ToChild(num, 1);
                ToParent(num, 1);
                return true;
            }
            else return false;
        }

        private void ToChild(int num,int change)
        {
            var q = new Queue<int>();
            q.Enqueue(num);
            while(q.Count > 0)
            {
                var top = q.Dequeue();
                foreach(var i in toChildGraph[top])
                {
                    lockedParents[i]+=change;
                    q.Enqueue(i);
                }
            }
        }
        private void ResetChild(int num)
        {
            var q = new Queue<int>();
            q.Enqueue(num);
            while (q.Count > 0)
            {
                var top = q.Dequeue();

                foreach (var i in toChildGraph[top])
                {
                    lockedParents[i]=1;
                    lockedChilds[i]=0;
                    visit[i]=0;
                    q.Enqueue(i);
                }
            }
        }

        private void ToParent(int num, int change)
        {
            var p = _parent[num];
            while (p>=0)
            {
                lockedChilds[p]+=change;
                p=_parent[p];
            }
        }

        public bool Unlock(int num, int user)
        {
            if (visit[num]==user)
            {
                visit[num]=0;
                ToChild(num, -1);
                ToParent(num, -1);
                return true;
            }
            else return false;
        }

        public bool Upgrade(int num, int user)
        {
            if (visit[num]==0)
            {
                if (lockedChilds[num]==0) return false;
                if (lockedParents[num]>0) return false;
                int childs = lockedChilds[num];
                ToParent(num, -childs+1);
                ResetChild(num);
                visit[num]=user;
                return true;
            }
            else return false;
        }

    }
}
