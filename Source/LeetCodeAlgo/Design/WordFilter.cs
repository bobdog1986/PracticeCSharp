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
        private class Trie
        {
            public Trie()
            {
                this._set = new HashSet<int>();
                this._dict = new Dictionary<char, Trie>();
            }
            public HashSet<int> _set;
            public Dictionary<char, Trie> _dict;
        }

        private readonly Dictionary<string, int> _indexMap;
        private readonly Dictionary<string, int> _cacheDict;
        private readonly Trie _prefixRoot;
        private readonly Trie _suffixRoot;
        private readonly HashSet<string> _invalidPrefixSet;
        private readonly HashSet<string> _invalidSuffixSet;

        public WordFilter(string[] words)
        {
            this._indexMap = new Dictionary<string, int>();
            for(int i = 0; i < words.Length; i++)
            {
                if (_indexMap.ContainsKey(words[i])) _indexMap[words[i]] = i;
                else _indexMap.Add(words[i], i);
            }

            _prefixRoot = new Trie();
            _suffixRoot = new Trie();
            buildTree();

            _cacheDict = new Dictionary<string, int>();
            _invalidPrefixSet = new HashSet<string>();
            _invalidSuffixSet = new HashSet<string>();
        }

        private void buildTree()
        {
            foreach(var k in _indexMap.Keys)
            {
                var curr = _prefixRoot;
                for (int j = 0; j < k.Length; j++)
                {
                    if (!curr._dict.ContainsKey(k[j]))
                        curr._dict.Add(k[j], new Trie());
                    curr = curr._dict[k[j]];
                    curr._set.Add(_indexMap[k]);
                }
            }

            foreach (var k in _indexMap.Keys)
            {
                var curr = _suffixRoot;
                for (int j = k.Length - 1; j >= 0; j--)
                {
                    if (!curr._dict.ContainsKey(k[j]))
                        curr._dict.Add(k[j], new Trie());
                    curr = curr._dict[k[j]];
                    curr._set.Add(_indexMap[k]);
                }
            }
        }

        public int F(string prefix, string suffix)
        {
            if (_invalidPrefixSet.Contains(prefix)) return -1;
            if (_invalidSuffixSet.Contains(suffix)) return -1;
            if (_cacheDict.ContainsKey($"{prefix}_{suffix}"))
            {
                return _cacheDict[$"{prefix}_{suffix}"];
            }

            var currPrefix = _prefixRoot;
            for(int i = 0; i < prefix.Length; i++)
            {
                if (currPrefix._dict.ContainsKey(prefix[i]))
                    currPrefix = currPrefix._dict[prefix[i]];
                else
                {
                    currPrefix = null;
                    break;
                }
            }

            if (currPrefix == null)
            {
                _invalidPrefixSet.Add(prefix);
                return -1;
            }
            var prefixSet = currPrefix._set;

            var currSuffix = _suffixRoot;
            for (int i = suffix.Length-1; i >=0; i--)
            {
                if (currSuffix._dict.ContainsKey(suffix[i]))
                    currSuffix = currSuffix._dict[suffix[i]];
                else
                {
                    currSuffix = null;
                    break;
                }
            }

            if (currSuffix == null)
            {
                _invalidSuffixSet.Add(suffix);
                return -1;
            }
            var suffixSet = currSuffix._set;

            var set = prefixSet.Where(x => suffixSet.Contains(x)).ToList();
            if (set.Count == 0)
            {
                _cacheDict.Add($"{prefix}_{suffix}", -1);
                return -1;
            }
            else
            {
                set.Sort((x, y) => -x.CompareTo(y));//the largest index
                _cacheDict.Add($"{prefix}_{suffix}", set[0]);
                return set[0];
            }
        }
    }


}
