using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///1652. Defuse the Bomb
        ///Given the circular array code and an integer key k, return the decrypted code to defuse the bomb!
        public int[] Decrypt(int[] code, int k)
        {
            var ans=new int[code.Length];
            for(int i=0; i<code.Length; i++)
            {
                if (k > 0)
                {
                    for (int j = 1; j <= k; j++)
                    {
                        var index = (i + j) % code.Length;
                        ans[i] += code[index];
                    }
                }
                else if (k < 0)
                {
                    for (int j = -1; j >= k; j--)
                    {
                        var index = (i + j) % code.Length;
                        if (index < 0)
                            index += code.Length;
                        ans[i] += code[index];
                    }
                }
            }
            return ans;
        }
    }
}
