using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

﻿namespace LeetCodeAlgo
{
    public partial class Anwser
    {
        ///2119. A Number After a Double Reversal
        ///1234 reverse to 4321, then again to 1234== origin 1234, return true
        public bool IsSameAfterReversals(int num)
        {
            return num == 0 || num % 10 != 0;
        }
    }
}