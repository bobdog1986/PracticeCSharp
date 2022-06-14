using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2166. Design Bitset
    public class Bitset
    {
        private readonly bool[] arr;
        private bool flip;
        private int count;
        public Bitset(int size)
        {
            arr=new bool[size];
            flip=false;
            count = 0;
        }

        public void Fix(int idx)
        {
            if (flip == arr[idx]) count++;
            arr[idx] = !flip;
        }

        public void Unfix(int idx)
        {
            if (flip != arr[idx]) count--;
            arr[idx] = flip;
        }

        public void Flip()
        {
            flip = !flip;
            count = arr.Length - count;
        }

        public bool All()
        {
            return Count()==arr.Length;
        }

        public bool One()
        {
            return Count()>0;
        }

        public int Count()
        {
            return count;
        }

        public string ToString()
        {
            var sb = new StringBuilder();
            foreach(var i in arr)
            {
                if(flip ^ i) sb.Append("1");
                else sb.Append("0");
            }
            return sb.ToString();
        }
    }
}
