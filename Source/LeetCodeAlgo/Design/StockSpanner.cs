using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///901. Online Stock Span, #Monotonic Stack
    //The span of the stock's price today is defined as the maximum number of consecutive days
    //(starting from today and going backward) for which the stock price was less than or equal to today's price.

    public class StockSpanner
    {
        private readonly Stack<int[]> stack;
        public StockSpanner()
        {
            stack = new Stack<int[]>();
        }
        public int Next(int price)
        {
            int span = 1;
            while (stack.Count>0 && price >= stack.Peek()[0])
            {
                // If the current price is greater than stack peek.
                span += stack.Pop()[1];
            }
            stack.Push(new int[] { price, span });
            return span;
        }
    }
}
