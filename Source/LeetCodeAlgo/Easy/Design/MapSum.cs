using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///677. Map Sum Pairs, #Trie
    //Maps a string key to a given value.
    //Returns the sum of the values that have a key with a prefix equal to a given string.
    public class MapSum
    {
        private readonly TrieItem root = new TrieItem();
        //this will duplicate update exist key's value
        public void Insert(string key, int val)
        {
            var curr = root;
            foreach (var c in key)
            {
                if (!curr.dict.ContainsKey(c))
                    curr.dict.Add(c, new TrieItem());
                curr = curr.dict[c];
            }
            curr.val = val;
        }

        public int Sum(string prefix)
        {
            var curr = root;
            foreach (var c in prefix)
            {
                if (!curr.dict.ContainsKey(c)) return 0;
                curr = curr.dict[c];
            }
            return sumInternal(curr);
        }

        private int sumInternal(TrieItem root)
        {
            if (root == null) return 0;
            int sum = root.val;
            foreach (var i in root.dict.Values)
            {
                sum += sumInternal(i);
            }
            return sum;
        }
    }
}
