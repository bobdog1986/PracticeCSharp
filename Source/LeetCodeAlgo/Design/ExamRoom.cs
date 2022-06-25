using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///855. Exam Room
    //When a student enters the room, they must sit in the seat that maximizes the distance to the closest person.
    //If there are multiple such seats, they sit in the seat with the lowest number.
    //If no one is in the room, then the student sits at seat number 0.
    public class ExamRoom
    {
        private readonly List<int> seats=new List<int>();
        private readonly int len;
        public ExamRoom(int n)
        {
            len = n;
            seats = new List<int>();
        }

        public int Seat()
        {
            if (seats.Count == 0)
            {
                seats.Add(0);
                return 0;
            }

            int max = seats[0] - 0;
            int index = -1;
            for (int i = 0; i < seats.Count - 1; i++)
            {
                int curr = (seats[i + 1] - seats[i]) / 2;
                if (curr > max)
                {
                    max = curr;
                    index = i;
                }
            }

            if (len - 1 - seats.Last() > max)
            {
                index = len;
            }

            if (index == -1)
            {
                seats.Insert(0, 0);
                return 0;
            }
            else if(index == len)
            {
                seats.Add(len-1);
                return len-1;
            }
            else
            {
                var next = (seats[index + 1] + seats[index]) / 2;
                seats.Insert(index + 1, next);
                return next;
            }
        }

        public void Leave(int p)
        {
            seats.Remove(p);
        }
    }
}
