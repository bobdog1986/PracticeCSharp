using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    /// 895. Maximum Frequency Stack
    ///Design a stack-like data structure to push elements to the stack and pop the most frequent element from the stack.
    ///void push(int val) pushes an integer val onto the top of the stack.
    ///int pop() removes and returns the most frequent element in the stack.
    ///If there is a tie for the most frequent element, the element closest to the stack's top is removed and returned.
    public class FreqStack
    {

        Dictionary<int, int> freq = new Dictionary<int, int>();
        Dictionary<int, Stack<int>> dict = new Dictionary<int, Stack<int>>();
        int maxfreq = 0;

        public FreqStack()
        {

        }

        public void Push(int x)
        {
            if(freq.ContainsKey(x))freq[x]++;
            else freq.Add(x, 1);

            maxfreq = Math.Max(maxfreq, freq[x]);

            if (!dict.ContainsKey(freq[x]))
                dict.Add(freq[x], new Stack<int>());

            dict[freq[x]].Push(x);
        }

        public int Pop()
        {
            int x = dict[maxfreq].Pop();
            freq[x]= maxfreq - 1;
            if (dict[maxfreq].Count == 0) maxfreq--;
            return x;
        }
    }

}
