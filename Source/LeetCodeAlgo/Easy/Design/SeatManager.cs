using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///1845. Seat Reservation Manager, #PriorityQueue
    ///Design a system that manages the reservation state of n seats that are numbered from 1 to n.
    ///SeatManager(int n) Initializes a SeatManager object that will manage n seats numbered from 1 to n.
    ///int reserve() Fetches the smallest-numbered unreserved seat, reserves it, and returns its number.
    ///void unreserve(int seatNumber) Unreserves the seat with the given seatNumber.
    public class SeatManager
    {
        private readonly PriorityQueue<int, int> pq;

        public SeatManager(int n)
        {
            pq = new PriorityQueue<int, int>();
            for (int i = 1; i <= n; i++)
                pq.Enqueue(i, i);
        }

        public int Reserve()
        {
            return pq.Dequeue();
        }

        public void Unreserve(int seatNumber)
        {
            pq.Enqueue(seatNumber, seatNumber);
        }
    }
}
