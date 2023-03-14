using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2286. Booking Concert Tickets in Groups, #Segment Tree
    //A concert hall has n rows numbered from 0 to n - 1, each with m seats, numbered from 0 to m - 1.

    //int[] gather(int k, int maxRow) Returns an array of length 2 denoting the row and seat number
    //of the first seat being allocated to the k members of the group, who must sit together.
    //Aka. it returns the smallest r and c such that [c, c + k - 1] seats are valid in row r, and r <= maxRow.
    //Returns [] in case it is not possible to allocate seats to the group.

    //boolean scatter(int k, int maxRow) Returns true if all k members of the group can be allocated seats
    //in rows 0 to maxRow, who may or may not sit together.
    //If the seats can be allocated, it allocates k seats to the group with the smallest row numbers,
    //and the smallest possible seat numbers in each row. Otherwise, returns false.

    public class BookMyShow
    {
        private class BookSegmentTree:SegmentTree
        {
            public BookSegmentTree(int[] nums) : base(nums) { }

            public int[] Gather(int left, int right, int val, int m)
            {
                return gatherInternal(root, left, right, val, m);
            }

            private int[] gatherInternal(SegmentNode node, int left, int right, int val, int m)
            {
                if (node == null) return new int[] { };
                if (node.start > right || node.end < left) return new int[] { };
                if (node.min + val > m) return new int[] { };
                if (node.start == node.end)
                {
                    int[] res = new int[] { node.start, (int)node.sum };
                    node.sum = node.sum + val;
                    node.min = (int)node.sum;
                    node.max = (int)node.sum;
                    return res;
                }
                else
                {
                    int[] res = new int[] { };
                    if (node.left != null && node.left.min + val <= m)
                    {
                        res = gatherInternal(node.left, left, right, val, m);
                    }
                    else if (node.right != null && node.right.min + val <= m)
                    {
                        res = gatherInternal(node.right, left, right, val, m);
                    }
                    node.sum = node.left.sum + node.right.sum;
                    node.min = Math.Min(node.left.min, node.right.min);
                    node.max = Math.Max(node.left.max, node.right.max);
                    return res;
                }
            }

            public void Scatter(int left, int right, int k, int m)
            {
                scatterInternal(root, left, right, k, m);
            }

            private void scatterInternal(SegmentNode node, int left, int right, int k, int m)
            {
                if (node == null) return;
                if (node.start > right || node.end < left) return;
                if (node.start == node.end)
                {
                    int diff = Math.Min(k, m - (int)node.sum);
                    node.sum = node.sum + diff;
                    node.min = (int)node.sum;
                    node.max = (int)node.sum;
                }
                else
                {
                    long leftSum = node.left.sum;
                    long leftDiff = (long)m * (node.left.end - node.left.start + 1) - leftSum;
                    if (leftDiff >= k)
                    {
                        scatterInternal(node.left, left, right, k, m);
                    }
                    else
                    {
                        if (leftDiff > 0)
                            scatterInternal(node.left, left, right, (int)leftDiff, m);
                        scatterInternal(node.right, left, right, k - (int)leftDiff, m);
                    }
                    node.sum = node.left.sum + node.right.sum;
                    node.min = Math.Min(node.left.min, node.right.min);
                    node.max = Math.Max(node.left.max, node.right.max);
                }
            }

        }

        private readonly BookSegmentTree root;
        private readonly int seats;
        private readonly int rows;
        public BookMyShow(int n, int m)
        {
            rows = n;
            seats = m;
            int[] arr = new int[n];
            root = new BookSegmentTree(arr);
        }

        public int[] Gather(int k, int maxRow)
        {
            if (root.MinOfRange(0, maxRow) + k > seats)
                return new int[] { };
            return root.Gather(0, rows-1, k, seats);
        }

        public bool Scatter(int k, int maxRow)
        {
            long total = root.SumOfRange(0, maxRow);
            if (total + k > (maxRow + 1) * (long)seats)
                return false;
            root.Scatter(0, rows - 1, k, seats);
            return true;
        }
    }


}
