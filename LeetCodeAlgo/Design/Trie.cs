using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///208. Implement Trie (Prefix Tree)
    ///A trie (pronounced as "try") or prefix tree is a tree data structure used to efficiently store and retrieve keys in a dataset of strings.
    ///There are various applications of this data structure, such as autocomplete and spellchecker.
    public class Trie
    {
        private TrieItem root;
        public Trie()
        {
            root=new TrieItem();
        }

        public void Insert(string word)
        {
            var curr = root;
            foreach(var c in word)
            {
                curr.AddChild(c);
                curr = curr.GetChild(c);
            }
            curr.AddChild('\0');
        }

        public bool Search(string word)
        {
            var curr = root;
            foreach (var c in word)
            {
                if (!curr.ContainChild(c)) return false;
                curr = curr.GetChild(c);
            }
            return curr.ContainChild('\0');
        }

        public bool StartsWith(string prefix)
        {
            var curr = root;
            foreach (var c in prefix)
            {
                if (!curr.ContainChild(c)) return false;
                curr = curr.GetChild(c);
            }
            return true;
        }

        public class TrieItem
        {
            public Dictionary<char, TrieItem> map;
            public TrieItem()
            {
                map=new Dictionary<char, TrieItem>();
            }

            public bool ContainChild(char c)
            {
                return map.ContainsKey(c);
            }

            public void AddChild(char c)
            {
                if (map.ContainsKey(c)) return;
                map.Add(c, new TrieItem());
            }

            public TrieItem GetChild(char c)
            {
                if(map.ContainsKey(c)) return map[c];
                else return null;
            }

            public int ChildCount()
            {
                return map.Count;
            }
        }
    }
}
