using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///1845. Seat Reservation Manager, #PriorityQueue, 
    ///Design a system that manages the reservation state of n seats that are numbered from 1 to n.
    ///SeatManager(int n) Initializes a SeatManager object that will manage n seats numbered from 1 to n.
    ///int reserve() Fetches the smallest-numbered unreserved seat, reserves it, and returns its number.
    ///void unreserve(int seatNumber) Unreserves the seat with the given seatNumber.
    public class SeatManager
    {

        private PriorityQueue<int, int> _queue;

        public SeatManager(int n)
        {
            _queue = new PriorityQueue<int, int>();
            for(int i = 1; i <= n; i++)
            {
                _queue.Enqueue(i,i);
            }
        }

        public int Reserve()
        {
            return _queue.Dequeue();
        }

        public void Unreserve(int seatNumber)
        {
            _queue.Enqueue(seatNumber, seatNumber);
        }
    }
}
