using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculationOf1ofn
{
    class Program
    {
        static void Main(string[] args)
        {

            double value=0;
            for (int i = 1; i <= 50; i++)
                value += 1.0 / i;

            double final = value; 

        }
    }
}
