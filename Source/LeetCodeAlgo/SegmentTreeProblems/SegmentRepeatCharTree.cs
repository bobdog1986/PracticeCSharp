using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo
{
    public class SegmentRepeatCharTree
    {
        private class SegmentRepeatCharNode
        {
            public char leftChar, rightChar;
            public int start, end;
            public int leftLen, rightLen;
            public int max;
            public int count => end-start+1;
            public bool IsSame => this.max==this.count;
            public SegmentRepeatCharNode leftNode, rightNode;
        }

        private readonly SegmentRepeatCharNode root;
        private readonly char[] arr;
        public SegmentRepeatCharTree(string s)
        {
            int n = s.Length;
            this.arr = new char[n];
            this.root = buildInternal(s, 0, n-1);
        }
        private SegmentRepeatCharNode buildInternal(string s, int start, int end)
        {
            var node = new SegmentRepeatCharNode();
            node.start= start;
            node.end=end;
            node.leftChar=s[start];
            node.rightChar=s[end];
            if (start == end)
            {
                this.arr[start]=s[start];

                node.leftLen=1;
                node.rightLen=1;
                node.max = 1;
            }
            else
            {
                int mid = start + (end - start) / 2;
                node.leftNode = buildInternal(s, start, mid);
                node.rightNode = buildInternal(s, mid + 1, end);
                mergeChildsInternal(node);
            }
            return node;
        }
        private void mergeChildsInternal(SegmentRepeatCharNode node)
        {
            node.leftChar = node.leftNode.leftChar;
            node.rightChar= node.rightNode.rightChar;
            if (node.leftNode.IsSame && node.rightNode.IsSame)
            {
                if (node.leftNode.leftChar == node.rightNode.leftChar)
                {
                    node.max = node.count;
                    node.leftLen =node.count;
                    node.rightLen = node.count;
                }
                else
                {
                    //no merge
                    node.leftLen = node.leftNode.leftLen;
                    node.rightLen = node.rightNode.rightLen;
                    node.max = Math.Max(node.leftNode.max, node.rightNode.max);
                }
            }
            else if (node.leftNode.IsSame)
            {
                if (node.leftNode.leftChar == node.rightNode.leftChar)
                {
                    node.leftLen = node.leftNode.count+node.rightNode.leftLen;
                    node.rightLen = node.rightNode.rightLen;
                    node.max = Math.Max(node.leftLen, node.rightNode.max);
                }
                else
                {
                    //no merge
                    node.leftLen = node.leftNode.leftLen;
                    node.rightLen = node.rightNode.rightLen;
                    node.max = Math.Max(node.leftNode.max, node.rightNode.max);
                }
            }
            else if (node.rightNode.IsSame)
            {
                if (node.leftNode.rightChar == node.rightNode.leftChar)
                {
                    node.leftLen = node.leftNode.leftLen;
                    node.rightLen = node.leftNode.rightLen+ node.rightNode.count;
                    node.max = Math.Max(node.leftNode.max, node.rightLen);
                }
                else
                {
                    //no merge
                    node.leftLen = node.leftNode.leftLen;
                    node.rightLen = node.rightNode.rightLen;
                    node.max = Math.Max(node.leftNode.max, node.rightNode.max);
                }
            }
            else
            {
                node.leftLen = node.leftNode.leftLen;
                node.rightLen = node.rightNode.rightLen;
                if (node.leftNode.rightChar != node.rightNode.leftChar)
                {
                    node.max = Math.Max(node.leftNode.max, node.rightNode.max);
                }
                else
                {
                    node.max =Math.Max(node.leftNode.rightLen+node.rightNode.leftLen, Math.Max(node.leftNode.max, node.rightNode.max));
                }
            }
        }

        public int Update(int index, char c)
        {
            if (this.arr[index]!=c)
                updateInternal(root, index, c);
            return this.root.max;
        }

        private void updateInternal(SegmentRepeatCharNode node, int index, char c)
        {
            if (node.start == node.end)
            {
                this.arr[index] = c;
                node.leftChar=c;
                node.rightChar=c;
            }
            else
            {
                int mid = node.start + (node.end - node.start) / 2;
                if (index <= mid)
                {
                    updateInternal(node.leftNode, index, c);
                }
                else
                {
                    updateInternal(node.rightNode, index, c);
                }

                mergeChildsInternal(node);
            }
        }

        public int Max()
        {
            return this.root.max;
        }
    }
}
