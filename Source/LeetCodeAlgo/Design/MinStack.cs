using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///155. Min Stack
    ///Design a stack that supports push, pop, top, and retrieving the minimum element in constant time.
    public class MinStack
    {
        private long min;
        private Stack<long> stack;

        public MinStack()
        {
            stack = new Stack<long>();
        }

        public void Push(int x)
        {
            if (stack.Count == 0)
            {
                stack.Push(0);
                min = x;
            }
            else
            {
                //if >min, positive
                stack.Push(x - min);
                if (x < min) min = x;//update the min value
            }
        }

        public void Pop()
        {
            if (stack.Count == 0)
                return;
            long pop = stack.Pop();
            if (pop < 0) min = min - pop;//If negative, increase the min value
        }

        public int Top()
        {
            long top = stack.Peek();
            if (top > 0)
            {
                return (int)(top + min);
            }
            else
            {
                return (int)(min);
            }
        }

        public int GetMin()
        {
            return (int)min;
        }
    }

    public class MinStack_Node
    {
        private class Node_minStack
        {
            public int val;
            public int min;
            public Node_minStack prev=null;

            public Node_minStack(int val, int min)
            {
                this.val = val;
                this.min = Math.Min(val, min);
            }
        }

        private Node_minStack top=null;

        public void Push(int x)
        {
            if (top == null)
            {
                top = new Node_minStack(x, x);
            }
            else
            {
                var next = new Node_minStack(x, top.min);
                next.prev = top;
                top = next;
            }
        }

        public void Pop()
        {
            top = top.prev;
        }

        public int Top()
        {
            return top.val;
        }

        public int GetMin()
        {
            return top.min;
        }
    }


}