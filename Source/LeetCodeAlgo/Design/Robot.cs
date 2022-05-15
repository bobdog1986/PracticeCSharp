using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design
{
    ///2069. Walking Robot Simulation II
    public class Robot
    {
        private int width, height;
        private int x, y;
        private int direct;
        public Robot(int width, int height)
        {
            x = 0;
            y = 0;
            direct = 0;
            this.width = width;
            this.height = height;
        }

        public void Step(int num)
        {
            num %= (width + height) * 2 - 4;
            //at begining, (0,0) facing east
            //after a whole loop, (0,0) facing south
            if (num == 0) num = (width + height) * 2 - 4;

            if (direct == 0)
            {
                if(x+num>= width - 1)
                {
                    num -= width - 1 - x;
                    direct = 1;
                    x = width - 1;
                    Step(num);
                }
                else
                {
                    x += num;
                }
            }
            else if(direct == 1)
            {
                if (y + num >= height - 1)
                {
                    num -= height - 1 - y;
                    direct = 2;
                    y= height - 1;
                    Step(num);
                }
                else
                {
                    y += num;
                }
            }
            else if (direct == 2)
            {
                if (x - num <=0)
                {
                    num -= x;
                    direct = 3;
                    x = 0;
                    Step(num);
                }
                else
                {
                    x -= num;
                }
            }
            else
            {
                if (y - num <= 0)
                {
                    num -= y;
                    direct = 0;
                    y = 0;
                    Step(num);
                }
                else
                {
                    y -= num;
                }
            }

        }

        public int[] GetPos()
        {
            return new int[] { x, y };
        }

        public string GetDir()
        {
            if (direct == 0) return "East";
            else if (direct == 1) return "North";
            else if (direct == 2) return "West";
            else return "South";
        }
    }
}
