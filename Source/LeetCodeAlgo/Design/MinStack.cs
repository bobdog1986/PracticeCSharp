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
        private Node_minStack head;

        public void Push(int x)
        {
            if (head == null)
                head = new Node_minStack(x, x, null);
            else
                head = new Node_minStack(x, Math.Min(x, head.min), head);
        }

        public void Pop()
        {
            head = head.next;
        }

        public int Top()
        {
            return head.val;
        }

        public int GetMin()
        {
            return head.min;
        }
    }

    public class Node_minStack
    {
        public int val;
        public int min;
        public Node_minStack next;

        public Node_minStack(int val, int min, Node_minStack next)
        {
            this.val = val;
            this.min = min;
            this.next = next;
        }
    }
}