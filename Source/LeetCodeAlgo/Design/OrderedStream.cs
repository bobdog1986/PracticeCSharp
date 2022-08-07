using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1656. Design an Ordered Stream
    //Design a stream that returns the values in increasing order of their IDs
    //by returning a chunk (list) of values after each insertion.
    //The concatenation of all the chunks should result in a list of the sorted values.
    // Inserts the pair (idKey, value) into the stream, then returns the largest possible
    // chunk of currently inserted values that appear next in the order.
    public class OrderedStream
    {
        private readonly Dictionary<int, string> dict;
        private int index;
        public OrderedStream(int n)
        {
            index = 1;
            dict = new Dictionary<int, string>();
        }

        public IList<string> Insert(int idKey, string value)
        {
            var res = new List<string>();
            if (!dict.ContainsKey(idKey))
                dict.Add(idKey, value);
            while (index <= 1000)
            {
                if (!dict.ContainsKey(index)) break;
                res.Add(dict[index++]);
            }
            return res;
        }
    }
}
