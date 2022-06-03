using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///676. Implement Magic Dictionary

    public class MagicDictionary
    {
        private readonly Dictionary<int, List<string>> dict;
        public MagicDictionary()
        {
            dict = new Dictionary<int, List<string>>();
        }

        public void BuildDict(string[] dictionary)
        {
            foreach(var word in dictionary)
            {
                if (!dict.ContainsKey(word.Length)) dict.Add(word.Length, new List<string>());
                dict[word.Length].Add(word);
            }
        }

        public bool Search(string searchWord)
        {
            if (dict.ContainsKey(searchWord.Length))
            {
                foreach(var word in dict[searchWord.Length])
                {
                    int diff = 0;
                    for (int i = 0; i < searchWord.Length; i++)
                    {
                        if (searchWord[i] != word[i]) diff++;
                        if (diff > 1) break;
                    }
                    if(diff ==1)return true;
                }
                return false;
            }
            else return false;
        }
    }
}
