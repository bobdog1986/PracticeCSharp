using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///211. Design Add and Search Words Data Structure, #Trie
    //word may contain dots '.' where dots can be matched with any letter.
    public class WordDictionary
    {
        private Dictionary<int,TrieItem> dict = new Dictionary<int,TrieItem>();
        private HashSet<string> set = new HashSet<string>();
        private HashSet<string> visit = new HashSet<string>();

        public void AddWord(string word)
        {
            if (set.Contains(word)) return;

            if (!dict.ContainsKey(word.Length))
                dict.Add(word.Length, new TrieItem());
            var curr = dict[word.Length];
            foreach(var c in word)
            {
                if(!curr.dict.ContainsKey(c))
                    curr.dict.Add(c, new TrieItem());
                curr=curr.dict[c];
            }
            curr.exist = true;
            set.Add(word);
        }

        public bool Search(string word)
        {
            if(visit.Contains(word)) return true;

            if (!dict.ContainsKey(word.Length))
                return false;
            var curr = dict[word.Length];

            var res = searchInternal(curr, 0, word);
            if (res) visit.Add(word);
            return res;
        }

        private bool searchInternal(TrieItem root,int index, string word)
        {
            if (index == word.Length)
            {
                return root.exist;
            }
            else
            {
                var curr = root;
                for (int i = index; i < word.Length; i++)
                {
                    if (word[i] == '.')
                    {
                        foreach(var next in curr.dict.Values)
                        {
                            if (searchInternal(next, i + 1, word))
                                return true;
                        }
                        return false;
                    }
                    else
                    {
                        if (!curr.dict.ContainsKey(word[i])) return false;
                        curr= curr.dict[word[i]];
                    }
                }
                return curr.exist;
            }
        }
    }

}
