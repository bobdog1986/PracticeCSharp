using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LeetCodeAlgo.Design
{
    ///225. Implement Stack using Queues
    public class MyStack
    {
        private readonly Queue<int> q1;
        private  readonly Queue<int> q2;
        private int top=0;
        public MyStack()
        {
            q1=new Queue<int>();
            q2=new Queue<int>();
        }

        public void Push(int x)
        {
            if(q2.Count>0) EnqueueInternal(q2,x);
            else EnqueueInternal(q1,x);
        }

        private void EnqueueInternal(Queue<int> q, int x)
        {
            q.Enqueue(x);
            top=x;
        }

        public int Pop()
        {
            if(q2.Count>0)
            {
                while(q2.Count>1)
                    EnqueueInternal(q1,q2.Dequeue());
                return q2.Dequeue();
            }
            else
            {
                while(q1.Count>1)
                    EnqueueInternal(q2,q1.Dequeue());
                return q1.Dequeue(); 
            }
        }

        public int Top()
        {
            return top;
        }

        public bool Empty()
        {
            return q1.Count==0 && q2.Count==0;
        }
    }
}
