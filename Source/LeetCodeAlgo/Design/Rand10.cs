using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeAlgo.Design.Rand10
{
    /// 470. Implement Rand10() Using Rand7(), #Rejection Sampling
    public class SolBase
    {
        private readonly Random random= new Random();
        public int Rand7()
        {
            return random.Next(1,8);
        }
    }

    public class Solution : SolBase
    {
        public int Rand10()
        {
            int row, col, res;
            //generate a 7x7 table, only 1-40 are valid
            do
            {
                row =Rand7();
                col =Rand7();
                res = col + (row-1)*7;
                if (res<=40)//[1,40]
                    break;
                //[41,49]
                //row = Rand7();
                col = Rand7();
                res = col + (res-40-1)*7;//[1,63]
                if (res<=60)//[1,60]
                    break;
                //[61,63]
                col = Rand7();
                res = col + (res-60-1)*7;//[1,21]
                if(res<=20)
                    break;
                //only [21]
            } while (true);

            return 1+(res-1)%10;
        }
    }
}
