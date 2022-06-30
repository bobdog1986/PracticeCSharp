using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///208. Implement Trie (Prefix Tree), #Trie
    //A trie (pronounced as "try") or prefix tree is a tree data structure
    //used to efficiently store and retrieve keys in a dataset of strings.
    //There are various applications of this data structure, such as autocomplete and spellchecker.
    public class Trie
    {
        private TrieItem root = new TrieItem();

        public void Insert(string word)
        {
            var curr = root;
            foreach(var c in word)
            {
                if (!curr.dict.ContainsKey(c))
                    curr.dict.Add(c, new TrieItem());
                curr = curr.dict[c];
            }
            curr.exist = true;
        }

        public bool Search(string word)
        {
            var curr = root;
            foreach (var c in word)
            {
                if (!curr.dict.ContainsKey(c)) return false;
                curr = curr.dict[c];
            }
            return curr.exist;
        }

        public bool StartsWith(string prefix)
        {
            var curr = root;
            foreach (var c in prefix)
            {
                if (!curr.dict.ContainsKey(c)) return false;
                curr = curr.dict[c];
            }
            return true;
        }
    }
}
