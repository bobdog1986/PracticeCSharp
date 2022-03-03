using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///225. Implement Stack using Queues
    public class MyStack
    {
        private List<int> list=new List<int>();

        public MyStack()
        {

        }

        public void Push(int x)
        {
            list.Insert(0, x);
        }

        public int Pop()
        {
            var x = list[0];
            list.RemoveAt(0);
            return x;
        }

        public int Top()
        {
            var x = list[0];
            return x;
        }

        public bool Empty()
        {
            return list.Count == 0;
        }
    }
}
