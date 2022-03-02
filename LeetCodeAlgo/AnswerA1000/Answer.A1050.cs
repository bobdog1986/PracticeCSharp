using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public partial class Answer
    {
        ///1071. Greatest Common Divisor of Strings
        ///Given two strings str1 and str2, return the largest string x such that x divides both str1 and str2.
        public string GcdOfStrings(string str1, string str2)
        {
            while(str1.Length>0 && str2.Length > 0 && str1 + str2 == str2 + str1)
            {
                if(str1 == str2)return str1;
                if (str1.Length > str2.Length)
                {
                    var mod = str1.Length % str2.Length;
                    if (mod == 0) mod = str2.Length;
                    str1 = str1.Substring(0, mod);
                }
                else
                {
                    var mod = str2.Length % str1.Length;
                    if (mod == 0) mod = str1.Length;
                    str2 = str2.Substring(0, mod);
                }
            }
            return "";
        }

        public string GcdOfStrings_My(string str1, string str2)
        {
            if (str1.Length < str2.Length)
            {
                var temp = str2;
                str2 = str1;
                str1 = temp;
            }
            int gcb = Gcb(str1.Length, str2.Length);
            for (int i = gcb; i >= 1; i--)
            {
                if (str1.Length%gcb==0 && str2.Length % gcb == 0)
                {
                    var s = str2.Substring(0, i);
                    if (GcdOfStrings_Check(str1, s) && GcdOfStrings_Check(str2, s))
                    {
                        return s;
                    }
                }
            }
            return "";
        }
        public bool GcdOfStrings_Check(string str1, string str2)
        {
            if (str1.Length % str2.Length != 0) return false;
            for(int i = 0; i < str1.Length; i += str2.Length)
            {
                if (str1.Substring(i, str2.Length) != str2) return false;
            }
            return true;
        }

    }
}
