using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeProblems
{
    public class QuadNode
    {
        public class Node
        {
            public bool val;
            public bool isLeaf;
            public Node topLeft;
            public Node topRight;
            public Node bottomLeft;
            public Node bottomRight;

            public Node()
            {
                val = false;
                isLeaf = false;
                topLeft = null;
                topRight = null;
                bottomLeft = null;
                bottomRight = null;
            }

            public Node(bool _val, bool _isLeaf)
            {
                val = _val;
                isLeaf = _isLeaf;
                topLeft = null;
                topRight = null;
                bottomLeft = null;
                bottomRight = null;
            }

            public Node(bool _val, bool _isLeaf, Node _topLeft, Node _topRight, Node _bottomLeft, Node _bottomRight)
            {
                val = _val;
                isLeaf = _isLeaf;
                topLeft = _topLeft;
                topRight = _topRight;
                bottomLeft = _bottomLeft;
                bottomRight = _bottomRight;
            }
        }

        ///427. Construct Quad Tree
        public Node Construct(int[][] grid)
        {
            return Construct_Recurr(grid, 0, 0, grid.Length);
        }

        private Node Construct_Recurr(int[][] grid, int x, int y, int len)
        {
            Node res = new Node();
            if (len==1)
            {
                res.val = grid[x][y] == 1 ? true : false;
                res.isLeaf = true;
            }
            else
            {
                var topLeft = Construct_Recurr(grid, x, y, len/2);
                var topRight = Construct_Recurr(grid, x, y+len/2, len/2);
                var bottomLeft = Construct_Recurr(grid, x+len/2, y, len/2);
                var bottomRight = Construct_Recurr(grid, x+len/2, y+len/2, len/2);
                if (topLeft.isLeaf &&topRight.isLeaf &&bottomLeft.isLeaf &&bottomRight.isLeaf
                     && topLeft.val == topRight.val && bottomLeft.val == bottomRight.val && topLeft.val == bottomLeft.val)
                {
                    res.isLeaf = true;
                    res.val =topLeft.val;
                }
                else
                {
                    res.isLeaf = false;
                    res.topLeft = topLeft;
                    res.topRight = topRight;
                    res.bottomLeft = bottomLeft;
                    res.bottomRight = bottomRight;

                }
            }
            return res;
        }
    }
}
