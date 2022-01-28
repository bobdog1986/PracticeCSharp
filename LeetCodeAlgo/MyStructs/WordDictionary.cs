using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.MyStructs
{
    ///211. Design Add and Search Words Data Structure
    public class WordDictionary
    {

        private readonly Dictionary<int, HashSet<string>> dict;

        public WordDictionary()
        {
            dict = new Dictionary<int, HashSet<string>>();
        }

        public void AddWord(string word)
        {
            if (!dict.ContainsKey(word.Length))
            {
                dict.Add(word.Length, new HashSet<string>() { word });
            }
            else
            {
                if (!dict[word.Length].Contains(word))
                {
                    dict[word.Length].Add(word);
                }
            }
        }

        public bool Search(string word)
        {
            if (!dict.ContainsKey(word.Length))
                return false;

            if (word.Contains("."))
            {
                List<int> list = new List<int>();
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] != '.')
                        list.Add(i);
                }
                foreach (var key in dict[word.Length])
                {
                    bool find = true;
                    foreach (var i in list)
                    {
                        if (word[i] != key[i])
                        {
                            find = false;
                            break;
                        }
                    }
                    if (find)
                        return true;
                }
                return false;
            }
            else
            {
                return dict[word.Length].Contains(word);
            }
        }
    }
}
