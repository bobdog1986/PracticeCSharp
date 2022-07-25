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
        private readonly SegmentTree root;
        private readonly int seats;
        private readonly int rows;
        public BookMyShow(int n, int m)
        {
            rows = n;
            seats = m;
            root = new SegmentTree();
            int[] arr = new int[n];
            root.Build(arr);
        }

        public int[] Gather(int k, int maxRow)
        {
            if (root.MinRange(0, maxRow) + k > seats)
                return new int[] { };
            return root.Gather(0, rows-1, k, seats);
        }

        public bool Scatter(int k, int maxRow)
        {
            long total = root.SumRange(0, maxRow);
            if (total + k > (maxRow + 1) * (long)seats)
                return false;
            root.Scatter(0, rows - 1, k, seats);
            return true;
        }
    }
}
