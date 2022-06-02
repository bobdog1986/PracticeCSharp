using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///677. Map Sum Pairs, #Trie
    //Maps a string key to a given value.
    //Returns the sum of the values that have a key with a prefix equal to a given string.
    public class MapSum
    {
        private readonly Dictionary<string, int> dict;

        public MapSum()
        {
            dict = new Dictionary<string, int>();
        }

        public void Insert(string key, int val)
        {
            if (dict.ContainsKey(key)) dict[key] = val;
            else dict.Add(key, val);
        }

        public int Sum(string prefix)
        {
            int sum = 0;
            foreach(var key in dict.Keys)
            {
                if (key.StartsWith(prefix)) sum += dict[key];
            }
            return sum;
        }
    }

    public class MapSum_Trie
    {
        private Trie_MapSum root;
        public MapSum_Trie()
        {
            root = new Trie_MapSum();
        }

        public void Insert(string key, int val)
        {
            var curr = root;
            foreach(var c in key)
            {
                if (curr.Items[c - 'a'] == null)
                {
                    curr.Items[c - 'a'] = new Trie_MapSum();
                }
                curr = curr.Items[c - 'a'];
            }
            curr.sum = val;
        }

        public int Sum(string prefix)
        {
            var curr = root;
            foreach(var c in prefix)
            {
                if (curr == null) break;
                curr = curr.Items[c - 'a'];
            }

            int sum = 0;
            Sum(curr, ref sum);
            return sum;
        }

        private void Sum(Trie_MapSum root, ref int sum)
        {
            if (root == null) return;
            else
            {
                sum += root.sum;
                foreach (var i in root.Items)
                    Sum(i, ref sum);
            }
        }

        public class Trie_MapSum
        {
            public int sum = 0;
            public Trie_MapSum[] Items = new Trie_MapSum[26];
        }
    }
}
