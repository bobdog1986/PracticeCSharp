using System.Collections.Generic;

namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///2001. Number of Pairs of Interchangeable Rectangles
        public long InterchangeableRectangles(int[][] rectangles)
        {
            long sum = 0;
            Dictionary<string, long> pairs = new Dictionary<string, long>();
            foreach (var rect in rectangles)
            {
                var gcb = Gcb(rect[0], rect[1]);
                var key = rect[0] / gcb + ":" + rect[1] / gcb;
                if (pairs.ContainsKey(key))
                    pairs[key]++;
                else
                    pairs.Add(key, 1);
            }

            foreach (var pair in pairs)
            {
                if (pair.Value > 1)
                {
                    sum += pair.Value * (pair.Value - 1) / 2;
                }
            }
            return sum;
        }

        /// <summary>
        /// 找出最大公约数
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Gcb(int m, int n)
        {
            if (m < 1 || n < 1)
                return m > 0 ? m : n;
            if (m == 1 || n == 1)
                return 1;
            if (m % n == 0)
                return n;

            int remainder = m % n;
            m = n;
            n = remainder;
            return Gcb(m, n);
        }

        public long GcbLong(long m, long n)
        {
            if (m < 1 || n < 1)
                return m > 0 ? m : n;
            if (m == 1 || n == 1)
                return 1;
            if (m % n == 0)
                return n;

            long remainder = m % n;
            m = n;
            n = remainder;
            return GcbLong(m, n);
        }
    }
}