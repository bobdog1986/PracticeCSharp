using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    /// <summary>
    /// Impl of SegmentIntervalTree(Lazy-Build), for insert interval and count covered range
    /// </summary>
    public class SegmentIntervalTree
    {
        protected class SegmentNode
        {
            public readonly int start, end;
            public SegmentNode left, right;
            public long sum = 0;
            public int max = int.MinValue, min = int.MaxValue;
            public SegmentNode(int start, int end)
            {
                this.start = start;
                this.end = end;
            }
        }

        protected readonly SegmentNode root;
        //lazy build for merge intervals, 1_000_000_000 is too big for normal segment tree
        public SegmentIntervalTree(int start, int end)
        {
            this.root = new SegmentNode(start, end);
        }

        public void Insert(int left, int right)
        {
            insertInternal(root, Math.Max(root.start, left), Math.Min(root.end, right));
        }

        protected long insertInternal(SegmentNode node, int left, int right)
        {
            //all range of [start,end] already visited
            if (node.sum == node.end - node.start + 1)
                return node.sum;

            //if it will cover the whole range of this node
            //reduce memory cost,eg a 2^10 big node only cost 1, but 2^10 nodes with 1 will cost 2^10
            if (node.start == left && node.end == right)
            {
                node.sum = right - left + 1;
                return node.sum;
            }

            int mid = node.start + (node.end - node.start) / 2;
            if (node.left == null)//lazy build
            {
                node.left = new SegmentNode(node.start, mid);
                node.right = new SegmentNode(mid + 1, node.end);
            }
            if (right <= mid)
                insertInternal(node.left, left, right);
            else if (left>mid)
                insertInternal(node.right, left, right);
            else
            {
                insertInternal(node.left, left, mid);
                insertInternal(node.right, mid+1, right);
            }
            node.sum = node.left.sum + node.right.sum;
            return node.sum;
        }

        public int Count(int left = int.MinValue, int right = int.MaxValue)
        {
            if (left == int.MinValue)
                return (int)countInternal(root, root.start, root.end);
            else
                return (int)countInternal(root, Math.Max(root.start, left), Math.Min(root.end, right));
        }

        protected long countInternal(SegmentNode node, int left, int right)
        {
            if (node == null) return 0;
            //all range of [start,end] already visited
            if (node.sum == node.end - node.start + 1)
                return right -left+1;
            if (node.start == left && node.end == right)
            {
                return node.sum;
            }
            else
            {
                int mid = node.start + (node.end - node.start) / 2;
                if (mid + 1 <= left)
                {
                    return countInternal(node.right, left, right);
                }
                else if (mid >= right)
                {
                    return countInternal(node.left, left, right);
                }
                else
                {
                    return countInternal(node.left, left, mid)+ countInternal(node.right, mid+1, right);
                }
            }
        }
    }
}
