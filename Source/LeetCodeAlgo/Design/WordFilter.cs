using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///745. Prefix and Suffix Search, #Trie
    //Returns the index of the word in the dictionary, which has the prefix prefix and the suffix suffix.
    //If there is more than one valid index, return the largest of them.
    //If there is no such word in the dictionary, return -1.
    public class WordFilter
    {
        private readonly Dictionary<string, int> indexMap;
        private readonly Dictionary<string, int> visit;
        private readonly TrieItem prefixRoot;
        private readonly TrieItem suffixRoot;

        public WordFilter(string[] words)
        {
            this.indexMap = new Dictionary<string, int>();
            for(int i = 0; i < words.Length; i++)
            {
                if (indexMap.ContainsKey(words[i])) indexMap[words[i]] = i;
                else indexMap.Add(words[i], i);
            }

            prefixRoot = new TrieItem();
            suffixRoot = new TrieItem();
            buildTree();

            visit = new Dictionary<string, int>();
        }

        private void buildTree()
        {
            foreach(var k in indexMap.Keys)
            {
                var curr = prefixRoot;
                for (int j = 0; j < k.Length; j++)
                {
                    if (!curr.dict.ContainsKey(k[j]))
                        curr.dict.Add(k[j], new TrieItem());
                    curr = curr.dict[k[j]];
                    curr.set.Add(indexMap[k]);
                }
            }

            foreach (var k in indexMap.Keys)
            {
                var curr = suffixRoot;
                for (int j = k.Length - 1; j >= 0; j--)
                {
                    if (!curr.dict.ContainsKey(k[j]))
                        curr.dict.Add(k[j], new TrieItem());
                    curr = curr.dict[k[j]];
                    curr.set.Add(indexMap[k]);
                }
            }
        }

        public int F(string prefix, string suffix)
        {
            if (visit.ContainsKey(prefix + "_")) return -1;
            if (visit.ContainsKey("_"+ suffix)) return -1;
            if (visit.ContainsKey($"{prefix}_{suffix}"))
            {
                return visit[$"{prefix}_{suffix}"];
            }

            var currPrefix = prefixRoot;
            for(int i = 0; i < prefix.Length; i++)
            {
                if (currPrefix.dict.ContainsKey(prefix[i]))
                    currPrefix = currPrefix.dict[prefix[i]];
                else
                {
                    currPrefix = null;
                    break;
                }
            }

            if (currPrefix == null)
            {
                visit.Add(prefix+"_",-1);
                return -1;
            }
            var prefixSet = currPrefix.set;

            var currSuffix = suffixRoot;
            for (int i = suffix.Length-1; i >=0; i--)
            {
                if (currSuffix.dict.ContainsKey(suffix[i]))
                    currSuffix = currSuffix.dict[suffix[i]];
                else
                {
                    currSuffix = null;
                    break;
                }
            }

            if (currSuffix == null)
            {
                visit.Add("_"+ suffix, -1);
                return -1;
            }
            var suffixSet = currSuffix.set;

            var list = prefixSet.Where(x => suffixSet.Contains(x)).ToList();
            if (list.Count == 0)
            {
                visit.Add($"{prefix}_{suffix}", -1);
                return -1;
            }
            else
            {
                list.Sort((x, y) => -x.CompareTo(y));//the largest index
                visit.Add($"{prefix}_{suffix}", list[0]);
                return list[0];
            }
        }
    }


}
