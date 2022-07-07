using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    //Common Trie for almost all scenarios
    public class TrieItem
    {
        //core definitions
        public Dictionary<char, TrieItem> dict = new Dictionary<char, TrieItem>();
        public string word = string.Empty;
        public bool exist = false;

        public int sum = 0;
        public int val = -1;
        public int index = -1;//index of whole word matched, default is -1

        public TrieItem[] arr10 = new TrieItem[10];
        public TrieItem[] arr26 = new TrieItem[26];
        public Dictionary<string, TrieItem> stringDict = new Dictionary<string, TrieItem>();
        public Dictionary<int, TrieItem> intDict = new Dictionary<int, TrieItem>();
        public List<string> list = new List<string>();
        public List<int> nums = new List<int>();
        public HashSet<int> set = new HashSet<int>();
    }

    public partial class Answer
    {
        //Prefix Trie
        public TrieItem initTrie(IEnumerable<string> words)
        {
            var root = new TrieItem();
            insertToTrie(root, words);
            return root;
        }

        public void insertToTrie(TrieItem root, IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                insertToTrie(root, word);
            }
        }

        public void insertToTrie(TrieItem root, string word)
        {
            var curr = root;
            foreach (var c in word)
            {
                if (!curr.dict.ContainsKey(c))
                    curr.dict.Add(c, new TrieItem());
                curr = curr.dict[c];
            }
            curr.word = word;
            curr.exist = true;
        }

        public bool searchPrefixInTrie(TrieItem root, string word)
        {
            var curr = root;
            foreach (var c in word)
            {
                if (!curr.dict.ContainsKey(c)) return false;
                curr = curr.dict[c];
            }
            return true;
        }

        public bool searchWholeWordInTrie(TrieItem root, string word)
        {
            var curr = root;
            foreach (var c in word)
            {
                if (!curr.dict.ContainsKey(c)) return false;
                curr = curr.dict[c];
            }
            return curr.exist || word == curr.word;
        }

        //Suffix Trie
        public TrieItem initSuffixTrie(IEnumerable<string> words)
        {
            var root = new TrieItem();
            insertToSuffixTrie(root, words);
            return root;
        }

        public void insertToSuffixTrie(TrieItem root, IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                insertToSuffixTrie(root, word);
            }
        }

        public void insertToSuffixTrie(TrieItem root, string word)
        {
            var curr = root;
            for (int i = word.Length - 1; i >= 0; i--)
            {
                if (!curr.dict.ContainsKey(word[i]))
                    curr.dict.Add(word[i], new TrieItem());
                curr = curr.dict[word[i]];
            }
            curr.word = word;
            curr.exist = true;
        }

        public bool searchSuffixInSuffixTrie(TrieItem root, string word)
        {
            var curr = root;
            for (int i = word.Length - 1; i >= 0; i--)
            {
                if (!curr.dict.ContainsKey(word[i])) return false;
                curr = curr.dict[word[i]];
            }
            return true;
        }

        public bool searchWholeWordInSuffixTrie(TrieItem root, string word)
        {
            var curr = root;
            for (int i = word.Length - 1; i >= 0; i--)
            {
                if (!curr.dict.ContainsKey(word[i])) return false;
                curr = curr.dict[word[i]];
            }
            return curr.exist || word == curr.word;
        }


    }
}
