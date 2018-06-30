using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMultiplcation
{
    class Program
    {
        static void Main(string[] args)
        {
            long sum = 5;
            int count = 31;

            long module = 100;

            long res = multiplication(sum, count, module); 
        }

        /*
        * start: 3:26pm
        * Test case: 
        * sum * count 
        * Need to figure out how to shorten the time to do calculation
        * Test the funnction
        */
        private static long multiplicationWithCare(long sum, long count)
        {
            long module = 1000 * 1000 * 1000 + 7;
            long MAX = long.MaxValue;
            int step = (int)(MAX / sum);

            long result = 0;

            if (sum * count < MAX)
                result = (sum * count) % module;
            else
            {
                long no = count / step;
                long small = count - (no * step);

                result = (sum * step) % module;
                result = (result * no) % module;
                result += (sum * small);
                result = result % module;
            }

            return result;
        }


    }
}
