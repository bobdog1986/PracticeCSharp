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
        private readonly TrieOfEncrypt root;
        public Encrypter(char[] keys, string[] values, string[] dictionary)
        {
            encryptDict=new Dictionary<char, string>();
            decryptDict=new Dictionary<string, List<char>>();
            for(int i=0; i<keys.Length; i++)
            {
                encryptDict.Add(keys[i], values[i]);
                if(!decryptDict.ContainsKey(values[i])) decryptDict.Add(values[i],new List<char>());
                decryptDict[values[i]].Add(keys[i]);
            }
            set = new HashSet<string>(dictionary);
            dict1 = new Dictionary<string, string>();
            root = new TrieOfEncrypt();
            BuildTrieTree();
        }

        private void BuildTrieTree()
        {
            foreach(var key in set)
            {
                var curr = root;
                foreach(var c in key)
                {
                    if (!curr.childs.ContainsKey(c)) curr.childs.Add(c, new TrieOfEncrypt());
                    curr = curr.childs[c];
                }
                //only trie node with valid == true count 1 in Decrypt()
                curr.valid = true;
            }
        }

        public string Encrypt(string word1)
        {
            //cache encrypt keys to avoid TLE
            if(dict1.ContainsKey(word1))return dict1[word1];

            StringBuilder sb= new StringBuilder();
            //string res = string.Empty;
            foreach (var c in word1)
                sb.Append(encryptDict[c]);
            var res = sb.ToString();
            dict1.Add(word1, res);
            return res;
        }

        public int Decrypt(string word2)
        {
            var list = new List<TrieOfEncrypt>() { root };
            for(int i = 0; i < word2.Length; i += 2)
            {
                var str=word2.Substring(i, 2);
                if (!decryptDict.ContainsKey(str)) return 0;//cannot decrypt
                var next = new List<TrieOfEncrypt>();
                foreach (var curr in list)
                {
                    foreach (var c in decryptDict[str])
                    {
                        if (curr.childs.ContainsKey(c))
                            next.Add(curr.childs[c]);
                    }
                }

                list = next;
                if (list.Count == 0) return 0;
            }
            return list.Where(x=>x.valid).Count();
        }
    }

    public class TrieOfEncrypt
    {
        //indicate if current trie node is valid
        public bool valid = false;
        public readonly Dictionary<char, TrieOfEncrypt> childs;
        public TrieOfEncrypt()
        {
            childs = new Dictionary<char, TrieOfEncrypt>();
        }
    }
}
