using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitManipulation_fb
{
    class Program
    {
        static void Main(string[] args)
        {
            long result = reverse(3); 
        }

        public static long reverse(long a)
        {
            long ret = 0;
            int index = 0;
            int power = 31;
            while (a > 0 && index < 32)
            {
                int val = (int)(a & 1);

                if (val == 1)
                    // ret += (long)Math.Pow(2, power);
                    ret += (long)1 << power; 

                a = a >> 1; // right shift
                index++;
                power--;
            }

            return ret;
        }
    }
}
