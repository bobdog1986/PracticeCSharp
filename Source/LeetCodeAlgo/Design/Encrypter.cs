using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2227. Encrypt and Decrypt Strings, #Trie
    ///You are given a character array keys containing unique characters and a string array values
    /// containing strings of length 2. You are also given another string array dictionary that contains
    /// all permitted original strings after decryption. You should implement a data structure that can encrypt
    /// or decrypt a 0-indexed string.
    public class Encrypter
    {

        private readonly Dictionary<char, string> encryptDict;
        private readonly Dictionary<string, List<char>> decryptDict;
        private readonly HashSet<string> set;
        private readonly Dictionary<string, string> dict1;
        private readonly TrieItem root;

        public Encrypter(char[] keys, string[] values, string[] dictionary)
        {
            encryptDict = new Dictionary<char, string>();
            decryptDict = new Dictionary<string, List<char>>();
            for (int i = 0; i < keys.Length; i++)
            {
                encryptDict.Add(keys[i], values[i]);
                if (!decryptDict.ContainsKey(values[i])) decryptDict.Add(values[i], new List<char>());
                decryptDict[values[i]].Add(keys[i]);
            }
            set = new HashSet<string>(dictionary);
            dict1 = new Dictionary<string, string>();
            root = new TrieItem();
            buildTrieTreeInternal();
        }

        private void buildTrieTreeInternal()
        {
            foreach (var key in set)
            {
                var curr = root;
                foreach (var c in key)
                {
                    if (!curr.dict.ContainsKey(c)) curr.dict.Add(c, new TrieItem());
                    curr = curr.dict[c];
                }
                //only trie node with valid == true count 1 in Decrypt()
                curr.exist = true;
            }
        }

        public string Encrypt(string word1)
        {
            //cache encrypt keys to avoid TLE
            if (dict1.ContainsKey(word1)) return dict1[word1];

            StringBuilder sb = new StringBuilder();
            //string res = string.Empty;
            foreach (var c in word1)
                sb.Append(encryptDict[c]);
            var res = sb.ToString();
            dict1.Add(word1, res);
            return res;
        }

        public int Decrypt(string word2)
        {
            var list = new List<TrieItem>() { root };
            for (int i = 0; i < word2.Length; i += 2)
            {
                var str = word2.Substring(i, 2);
                if (!decryptDict.ContainsKey(str)) return 0;//cannot decrypt
                var next = new List<TrieItem>();
                foreach (var curr in list)
                {
                    foreach (var c in decryptDict[str])
                    {
                        if (curr.dict.ContainsKey(c))
                            next.Add(curr.dict[c]);
                    }
                }

                list = next;
                if (list.Count == 0) return 0;
            }
            return list.Where(x => x.exist).Count();
        }
    }

    public class Encrypter_Lee215
    {
        private readonly Dictionary<char, string> encryptDict;
        private readonly Dictionary<string, int> decryptDict;

        public Encrypter_Lee215(char[] keys, string[] values, string[] dictionary)
        {
            encryptDict = new Dictionary<char, string>();
            for (int i = 0; i < keys.Length; ++i)
                encryptDict.Add(keys[i], values[i]);

            decryptDict = new Dictionary<string, int>();
            foreach (var word in dictionary)
            {
                var encrypt = Encrypt(word);
                if (!decryptDict.ContainsKey(encrypt)) decryptDict.Add(encrypt, 1);
                else decryptDict[encrypt]++;
            }
        }

        public string Encrypt(string word1)
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < word1.Length; i++)
                res.Append(encryptDict[word1[i]]);
            return res.ToString();
        }

        public int Decrypt(string word2)
        {
            return decryptDict.ContainsKey(word2) ? decryptDict[word2] : 0;
        }
    }
}