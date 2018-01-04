using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        //443
        public int Compress(char[] chars)
        {
            if (chars.Length == 1) return chars.Length;
            List<char> result = new List<char>();
            char pre = chars[0];
            int occured = 1;
            char current;
            for(int i = 1; i < chars.Length; i++)
            {
                current = chars[i];
                if (current == pre)
                {
                    occured++;
                }
                else
                {
                    result.Add(pre);
                    if (occured > 1) { result.AddRange(occured.ToString().ToCharArray()); }

                    pre = current;
                    occured = 1;
                }

                if (i == chars.Length - 1)
                {
                    result.Add(pre);
                    if (occured > 1) { result.AddRange(occured.ToString().ToCharArray()); }
                }
            }

            foreach(var i in result)
            {
                Console.WriteLine(i);
            }
            //chars = result.ToArray();
            Array.Copy(result.ToArray(), chars, result.Count);
            return result.Count;
        }
        public char GetSingleNumChar(int num)
        {
            return (char)(num + 0x30);
        }
        public char[] GetNumCharArray(int num)
        {
            return num.ToString().ToCharArray();
        }
    }
}
