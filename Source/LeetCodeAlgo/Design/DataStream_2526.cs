using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2526. Find Consecutive Integers from a Data Stream
    public class DataStream
    {
        private int count;
        private int target;
        private Dictionary<int, int> dict;
        private Queue<int> q;

        public DataStream(int value, int k)
        {
            count=k;
            target = value;
            dict=new Dictionary<int, int>();
            q=new Queue<int>();
        }

        public bool Consec(int num)
        {
            if (q.Count>=count)
            {
                var top = q.Dequeue();
                dict[top]--;
            }
            if (!dict.ContainsKey(num))
                dict.Add(num, 0);
            q.Enqueue(num);
            dict[num]++;
            return dict.ContainsKey(target)&&dict[target]==count;
        }
    }
}