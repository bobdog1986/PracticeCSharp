using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.MyStructs
{
    ///155. Min Stack
    public class MinStack
    {
        private int min = int.MaxValue;
        private List<int> list = new List<int>();
        public MinStack()
        {

        }

        public void Push(int val)
        {
            list.Insert(0, val);
            min = Math.Min(val, min);
        }

        public void Pop()
        {
            if (list.Count == 0)
                return;

            var a = list[0];

            list.RemoveAt(0);

            if (list.Count == 0)
            {
                min = int.MaxValue;
                return;
            }

            if (a == min)
            {
                min = list.Min();
            }
        }

        public int Top()
        {
            return list[0];
        }

        public int GetMin()
        {
            return min;
        }
    }
}
