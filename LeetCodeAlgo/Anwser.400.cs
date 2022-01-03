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
        //492
        public int[] ConstructRectangle(int area)
        {
            int[] result = new int[2] {area,1 };
            int min = (int)Math.Sqrt(area);
            for(int len = min; len < area; len++)
            {
                if (area % len == 0)
                {
                    if(len>= area / len)
                    {
                        return new int[2] { len, area / len };
                    }
                }
            }
            return result;
        }
        //495
        public int FindPoisonedDuration(int[] timeSeries, int duration)
        {
            if (timeSeries == null || timeSeries.Length == 0) return 0;

            Array.Sort(timeSeries);

            int begin = timeSeries[0];
            int expired = begin+duration;

            int total = 0;
            for(int i = 1; i < timeSeries.Length; i++)
            {
                if (timeSeries[i] < expired)
                {
                    expired = timeSeries[i] + duration;
                }
                else
                {
                    total += expired - begin;

                    begin = timeSeries[i];
                    expired = begin + duration;
                }
            }

            total+= expired - begin;
            return total;
        }
    }
}
