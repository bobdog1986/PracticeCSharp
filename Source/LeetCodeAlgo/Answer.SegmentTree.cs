﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    /// <summary>
    /// standard segment tree for update and range query of sum/min/max
    /// </summary>
    public class SegmentTree
    {
        protected class SegmentNode
        {
            public readonly int start, end;
            public SegmentNode left, right;
            public long sum = 0;
            public int max = int.MinValue, min = int.MaxValue;
            public long m = 1, inc = 0;
            public SegmentNode(int start, int end)
            {
                this.start = start;
                this.end = end;
            }
        }

        protected readonly SegmentNode root;
        public int[] arr;

        public SegmentTree(int[] nums)
        {
            this.arr=nums;
            this.root = buildInternal(nums, 0, nums.Length-1);
        }

        protected SegmentNode buildInternal(int[] nums, int start, int end)
        {
            if (start > end)
                return null;

            var node = new SegmentNode(start, end);
            if(start == end)
            {
                node.sum = nums[start];
                node.max= nums[start];
                node.min = nums[start];
            }
            else
            {
                int mid = start + (end - start)/2;
                node.left = buildInternal(nums, start, mid);
                node.right = buildInternal(nums, mid+1, end);
                node.sum = node.left.sum + node.right.sum;
                node.max = Math.Max(node.left.max, node.right.max);
                node.min = Math.Min(node.left.min, node.right.min);
            }
            return node;
        }

        public void Update(int index,int val)
        {
            updateInternal(root, index, val);
        }

        protected void updateInternal(SegmentNode node, int index ,int val)
        {
            if (node == null) return;
            if(index>=node.start && index <= node.end)
            {
                if(node.start == node.end)
                {
                    node.sum = val;
                    node.min = val;
                    node.max = val;
                    this.arr[node.start]=val;
                }
                else
                {
                    int mid = node.start + ( node.end - node.start) / 2;
                    if (node.left == null)//lazy build
                    {
                        node.left = new SegmentNode(node.start, mid);
                        node.right = new SegmentNode(mid + 1, node.end);
                    }
                    if (index <= mid)
                    {
                        updateInternal(node.left, index, val);
                    }
                    else
                    {
                        updateInternal(node.right, index, val);
                    }
                    node.sum = node.left.sum + node.right.sum;
                    node.max = Math.Max(node.left.max, node.right.max);
                    node.min = Math.Min(node.left.min, node.right.min);
                }
            }
        }

        public long SumOfRange(int left,int right)
        {
            return sumOfRangeInternal(root, Math.Max(root.start, left), Math.Min(root.end, right));
        }

        protected long sumOfRangeInternal(SegmentNode node, int left,int right)
        {
            if (node == null) return 0;
            if (node.start > right || node.end < left) return 0;
            if(node.start == left && node.end == right)
            {
                return node.sum;
            }
            else
            {
                int mid = node.start + (node.end - node.start) / 2;
                if (node.left == null)//lazy build
                {
                    node.left = new SegmentNode(node.start, mid);
                    node.right = new SegmentNode(mid + 1, node.end);
                }
                if (mid < left)
                {
                    return sumOfRangeInternal(node.right, left, right);
                }
                else if(mid>= right)
                {
                    return sumOfRangeInternal(node.left, left, right);
                }
                else
                {
                    return sumOfRangeInternal(node.left, left, mid) + sumOfRangeInternal(node.right, mid+1, right);
                }
            }
        }

        public int MinOfRange(int left, int right)
        {
            return minRangeInternal(root, Math.Max(root.start, left), Math.Min(root.end, right));
        }

        protected int minRangeInternal(SegmentNode node, int left, int right)
        {
            if (node == null) return int.MaxValue;
            if (node.start > right || node.end < left) return int.MaxValue;
            if (node.start == left && node.end == right)
            {
                return node.min;
            }
            else
            {
                int mid = node.start + (node.end - node.start) / 2;
                if (node.left == null)//lazy build
                {
                    node.left = new SegmentNode(node.start, mid);
                    node.right = new SegmentNode(mid + 1, node.end);
                }
                if (mid < left)
                {
                    return minRangeInternal(node.right, left, right);
                }
                else if (mid >= right)
                {
                    return minRangeInternal(node.left, left, right);
                }
                else
                {
                    return Math.Min(minRangeInternal(node.left, left, mid), minRangeInternal(node.right, mid + 1, right));
                }
            }
        }

        public int MaxOfRange(int left, int right)
        {
            return maxOfRangeInternal(root, Math.Max(root.start, left), Math.Min(root.end, right));
        }

        private int maxOfRangeInternal(SegmentNode node, int left, int right)
        {
            if (node == null) return int.MinValue;
            if (node.start > right || node.end < left) return int.MinValue;
            if (node.start == left && node.end == right)
            {
                return node.max;
            }
            else
            {
                int mid = node.start + (node.end - node.start) / 2;
                if (node.left == null)//lazy build
                {
                    node.left = new SegmentNode(node.start, mid);
                    node.right = new SegmentNode(mid + 1, node.end);
                }
                if (mid < left)
                {
                    return maxOfRangeInternal(node.right, left, right);
                }
                else if (mid >= right)
                {
                    return maxOfRangeInternal(node.left, left, right);
                }
                else
                {
                    return Math.Max(maxOfRangeInternal(node.left, left, mid), maxOfRangeInternal(node.right, mid + 1, right));
                }
            }
        }

    }

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
            insertInternal(root, Math.Max(root.start, left),Math.Min(root.end, right));
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
            if(node.left == null)//lazy build
            {
                node.left = new SegmentNode(node.start, mid);
                node.right = new SegmentNode(mid + 1, node.end);
            }
            if (right <= mid)
                insertInternal(node.left, left, right);
            else if(left>mid)
                insertInternal(node.right, left, right);
            else
            {
                insertInternal(node.left, left, mid);
                insertInternal(node.right, mid+1, right);
            }
            node.sum = node.left.sum + node.right.sum;
            return node.sum;
        }

        public int Count(int left=int.MinValue,int right=int.MaxValue)
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

    public class SegmentLongestSubstrTree
    {
        protected class SegmentNode
        {
            public readonly int start, end;
            public SegmentNode left, right;
            public long sum = 0;
            public int max = int.MinValue, min = int.MaxValue;
            public List<char> listStr = new List<char>();
            public SegmentNode(int start, int end)
            {
                this.start = start;
                this.end = end;
            }
        }

        protected readonly SegmentNode root;

        /// <summary>
        /// Lazy Build
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public SegmentLongestSubstrTree(string s)
        {
            this.root = buildInternal(s, 0, s.Length - 1);
        }

        protected SegmentNode buildInternal(string s, int start, int end)
        {
            if (start > end)
                return null;

            var node = new SegmentNode(start, end);
            if (start == end)
            {
                node.listStr.Add(s[start]);
                node.max = 1;
            }
            else
            {
                int mid = start + (end - start) / 2;
                node.left = buildInternal(s, start, mid);
                node.right = buildInternal(s, mid + 1, end);

                node.listStr.AddRange(node.left.listStr);
                node.listStr.AddRange(node.right.listStr);
                int max = 1;
                if(node.left.listStr.Last() == node.right.listStr.First())
                {
                    char c = node.left.listStr.Last();
                    int n1 = node.left.listStr.Count;
                    int n2 = node.right.listStr.Count;
                    int i = n1 - 1;
                    while (i >= 0 && node.left.listStr[i] == c)
                        i--;
                    int j = 0;
                    while (j < n2 && node.right.listStr[j] == c)
                        j++;
                    max = n1-1 - (i + 1) + 1 + j - 1 - 0 + 1;
                }
                node.max = Math.Max(max,Math.Max( node.left.max, node.right.max));
            }
            return node;
        }

        public void Update(int index, char val)
        {
            updateInternal(root, index, val);
        }

        protected void updateInternal(SegmentNode node, int index, char val)
        {
            if (node == null) return;
            if (index >= node.start && index <= node.end)
            {
                if (node.start == node.end)
                {
                    node.listStr[0] = val;
                    node.max = 1;
                }
                else
                {
                    int mid = node.start + (node.end - node.start) / 2;
                    if (index <= mid)
                    {
                        updateInternal(node.left, index, val);
                    }
                    else
                    {
                        updateInternal(node.right, index, val);
                    }
                    node.listStr[index-node.start]=val;
                    int max = 1;
                    if (node.left.listStr.Last() == node.right.listStr.First())
                    {
                        char c = node.left.listStr.Last();
                        int n1 = node.left.listStr.Count;
                        int n2 = node.right.listStr.Count;
                        int i = n1 - 1;
                        while (i >= 0 && node.left.listStr[i] == c)
                            i--;
                        int j = 0;
                        while (j < n2 && node.right.listStr[j] == c)
                            j++;
                        max = n1-1 - (i + 1) + 1 + j - 1 - 0 + 1;
                    }
                    node.max = Math.Max(max, Math.Max(node.left.max, node.right.max));
                }
            }
        }

        public int Max()
        {
            return this.root.max;
        }
    }

}
