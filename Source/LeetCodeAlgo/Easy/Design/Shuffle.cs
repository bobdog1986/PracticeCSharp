using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{


    ///384. Shuffle an Array
    public class Solution_384_Shuffle
    {
        private readonly int[] arr;
        private readonly int[] arr2;
        private bool shuffle = false;
        public Solution_384_Shuffle(int[] nums)
        {
            arr = nums;
            arr2 = new int[arr.Length];
        }

        public int[] Reset()
        {
            shuffle = false;
            return arr;
        }

        public int[] Shuffle()
        {
            if (!shuffle)
            {
                List<int> list = arr.ToList();
                var random = new Random();
                int j = 0;
                while (list.Count > 0)
                {
                    var i = random.Next(0, list.Count);
                    arr2[j] = list[i];
                    list.RemoveAt(i);
                    j++;
                }
                shuffle = true;
            }
            return arr2;
        }
    }
}
