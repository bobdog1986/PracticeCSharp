using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    public interface NestedInteger
    {
        // @return true if this NestedInteger holds a single integer, rather than a nested list.
        bool IsInteger();

        // @return the single integer that this NestedInteger holds, if it holds a single integer
        // Return null if this NestedInteger holds a nested list
        int GetInteger();

        // @return the nested list that this NestedInteger holds, if it holds a nested list
        // Return null if this NestedInteger holds a single integer
        IList<NestedInteger> GetList();
    }

    ///341. Flatten Nested List Iterator
    ///You are given a nested list of integers nestedList.
    ///Each element is either an integer or a list whose elements may also be integers or other lists.
    ///Implement an iterator to flatten it.
    public class NestedIteratorMy
    {
        private List<int> list = new List<int>();

        public NestedIteratorMy(IList<NestedInteger> nestedList)
        {
            foreach (var nested in nestedList)
                Add(nested);
        }

        private void Add(NestedInteger nestedInteger)
        {
            if (nestedInteger.IsInteger())
            {
                list.Add(nestedInteger.GetInteger());
            }
            else
            {
                foreach (var i in nestedInteger.GetList())
                {
                    Add(i);
                }
            }
        }

        public bool HasNext()
        {
            return list.Count > 0;
        }

        public int Next()
        {
            var res = list[0];
            list.RemoveAt(0);
            return res;
        }
    }

    public class NestedIterator
    {
        private Stack<IEnumerator<NestedInteger>> stack;

        public NestedIterator(IList<NestedInteger> nestedList)
        {
            stack = new Stack<IEnumerator<NestedInteger>>();
            stack.Push(nestedList.GetEnumerator());
        }

        public bool HasNext()
        {
            while (stack.Count > 0)
            {
                if (stack.Peek().MoveNext())
                {
                    if (stack.Peek().Current.IsInteger())
                    {
                        return true;
                    }
                    stack.Push(stack.Peek().Current.GetList().GetEnumerator());
                }
                else
                {
                    stack.Pop();
                }
            }
            return false;
        }

        public int Next()
        {
            return stack.Peek().Current.GetInteger();
        }
    }
}