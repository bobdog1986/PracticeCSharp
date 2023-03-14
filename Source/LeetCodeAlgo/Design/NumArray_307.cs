using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design.NumArray_307
{
    ///307. Range Sum Query - Mutable, #Segment Tree
    ///Given an integer array nums, handle multiple queries of the following types:
    ///Update the value of an element in nums.
    ///Calculate the sum of the elements of nums between indices left and right inclusive where left <= right.
    public class NumArray
    {
        private readonly SegmentTree tree;
        public NumArray(int[] nums)
        {
            tree = new SegmentTree(nums);
        }

        public void Update(int index, int val)
        {
            tree.Update(index, val);
        }

        public int SumRange(int left, int right)
        {
            return (int)tree.SumOfRange(left, right);
        }
    }


}
