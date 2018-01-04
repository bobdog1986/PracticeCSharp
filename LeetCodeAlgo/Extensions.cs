using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public static class Extensions
    {
        public static T[] Slice<T>(this T[] source, int start, int count)
        {
            T[] result = new T[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = ((start + i) < source.Length) ? source[start + i] : default(T);
            }
            return result;
        }
    }
}
