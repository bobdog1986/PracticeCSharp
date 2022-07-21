using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Easy.Design
{
    ///1603. Design Parking System
    ///carType: big, medium, or small, which are represented by 1, 2, and 3 respectively.
    ///A car can only park in a parking space of its carType.
    ///If there is no space available, return false, else park the car in that size space and return true.
    public class ParkingSystem
    {
        private int big;
        private int small;
        public int medium;
        public ParkingSystem(int big, int medium, int small)
        {
            this.big = big;
            this.medium = medium;
            this.small = small;
        }

        public bool AddCar(int carType)
        {
            if (carType == 1) return big-- > 0;
            else if (carType == 2) return medium-- > 0;
            return small-- > 0;
        }
    }
}
