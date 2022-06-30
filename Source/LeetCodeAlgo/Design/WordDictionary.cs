using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///211. Design Add and Search Words Data Structure
    //word may contain dots '.' where dots can be matched with any letter.
    public class WordDictionary
    {

        private readonly Dictionary<int, HashSet<string>> dict=new Dictionary<int, HashSet<string>>();

        public void AddWord(string word)
        {
            if (!dict.ContainsKey(word.Length))
                dict.Add(word.Length, new HashSet<string>());
            dict[word.Length].Add(word);
        }

        public bool Search(string word)
        {
            if (!dict.ContainsKey(word.Length))
                return false;
            if(!word.Any(x=>x=='.'))return dict[word.Length].Contains(word);

            return dict[word.Length].Any(x =>
            {
                int i = 0;
                for (; i < word.Length; i++)
                {
                    if (word[i] != x[i] && word[i] != '.') break;
                }
                return i == word.Length;
            });
        }
    }
}
