using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1622. Fancy Sequence, #Segment Tree, #Good
    //Implement the Fancy class:
    //Fancy() Initializes the object with an empty sequence.
    //void append(val) Appends an integer val to the end of the sequence.
    //void addAll(inc) Increments all existing values in the sequence by an integer inc.
    //void multAll(m) Multiplies all existing values in the sequence by an integer m.
    //int getIndex(idx) Gets the current value at index idx (0-indexed) of the sequence modulo 109 + 7.
    //If the index is greater or equal than the length of the sequence, return -1.

    public class FancySegmentTreeLazy
    {
        private readonly long mod = 1_000_000_007;
        public SegmentNode root = null;
        public int index=-1;
        private readonly int[] arr;
        public FancySegmentTreeLazy(int[] nums)
        {
            this.arr = nums;
            root = new SegmentNode(0, arr.Length - 1);
        }

        public void Append(int val)
        {
            arr[++index] = val;
        }

        public void AddAll(int inc)
        {
            if (index == -1 || inc == 0) return;
            addAllInternal(root, index, inc);
        }

        private void addAllInternal(SegmentNode node, int index, int inc)
        {
            if (index >= node.start && index <= node.end)
            {
                if (node.start == node.end)
                {
                    node.inc = (node.inc + inc) % mod;
                }
                else
                {
                    int mid = node.start + (node.end - node.start) / 2;
                    if(node.left == null)
                    {
                        node.left = new SegmentNode(node.start, mid);
                        node.right = new SegmentNode(mid + 1, node.end);
                    }
                    if (index <= mid)
                    {
                        addAllInternal(node.left, index, inc);
                    }
                    else
                    {
                        addAllInternal(node.right, index, inc);
                    }
                    node.m = node.left.m * node.right.m % mod;
                    node.inc = (node.left.inc * node.right.m + node.right.inc) % mod;
                }
            }
        }

        public void MultiplyAll(int m)
        {
            if (index == -1 || m == 1) return;
            multiplyInternal(root, index, m);
        }

        private void multiplyInternal(SegmentNode node, int index, int val)
        {
            if (index >= node.start && index <= node.end)
            {
                if (node.start == node.end)
                {
                    node.m = node.m * val % mod;
                    node.inc = node.inc * val % mod;
                }
                else
                {
                    int mid = node.start + (node.end - node.start) / 2;
                    if (node.left == null)
                    {
                        node.left = new SegmentNode(node.start, mid);
                        node.right = new SegmentNode(mid + 1, node.end);
                    }
                    if (index <= mid)
                    {
                        multiplyInternal(node.left, index, val);
                    }
                    else
                    {
                        multiplyInternal(node.right, index, val);
                    }
                    node.m = node.left.m * node.right.m % mod;
                    node.inc = (node.left.inc * node.right.m + node.right.inc) % mod;
                }
            }
        }

        public int GetIndex(int idx)
        {
            if (idx > index) return -1;
            return (int)getIndexInternal(root, idx,index, arr[idx]);
        }

        private long getIndexInternal(SegmentNode node, int left, int right, long val)
        {
            if(node.start == left && node.end == right)
            {
                return (node.m * val + node.inc) % mod;
            }
            else
            {

                int mid = node.start + (node.end - node.start) / 2;
                if (node.left == null)
                {
                    node.left = new SegmentNode(node.start, mid);
                    node.right = new SegmentNode(mid + 1, node.end);
                }
                if (mid >= right)
                    return getIndexInternal(node.left, left, right, val);
                else if (mid < left)
                    return getIndexInternal(node.right, left, right,val);
                else
                {
                    var leftVal = getIndexInternal(node.left, left, mid, val);
                    return getIndexInternal(node.right, mid+1, right, leftVal);
                }
            }
        }
    }

    //MUST using Lazy Build to avoid possible TLE
    public class Fancy
    {
        private readonly FancySegmentTreeLazy root;
        public Fancy()
        {
            int[] arr = new int[100000 + 1];
            root = new FancySegmentTreeLazy(arr);
        }

        public void Append(int val)
        {
            root.Append(val);
        }

        public void AddAll(int inc)
        {
            root.AddAll(inc);
        }

        public void MultAll(int m)
        {
            root.MultiplyAll(m);
        }

        public int GetIndex(int idx)
        {
            return root.GetIndex(idx);
        }
    }
}
