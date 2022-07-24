using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public class BinaryIndexedTree
    {
        private readonly int[] arr;
        public BinaryIndexedTree(int n)
        {
            arr = new int[n + 1];
        }

        public void update(int x)
        {
            while (x < arr.Length)
            {
                arr[x]++;
                x += x & -x;
            }
        }

        public int get(int x)
        {
            int res = 0;
            while (x > 0)
            {
                res += arr[x];
                x -= x & -x;
            }
            return res;
        }
    }

    public partial class Answer
    {

    }



}
