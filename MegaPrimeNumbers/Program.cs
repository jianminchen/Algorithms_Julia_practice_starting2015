using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaPrimeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tokens_first = Console.ReadLine().Split(' ');
            long first = Convert.ToInt64(tokens_first[0]);
            long last  = Convert.ToInt64(tokens_first[1]);
            
            // your code goes here
            Console.WriteLine(CalculateTotalNumberOfMegaPrimes(first, last)); 
        }

        /*
         * last - first <= 1000 * 1000 * 1000
         * 1 <= first <= last <= 10^15
         */
        public static long CalculateTotalNumberOfMegaPrimesInRange(long first, long last)
        {
            long number1 = CalculateTotalNumberOfMegaPrimesFrom1Up(first);
            long number2 = CalculateTotalNumberOfMegaPrimesFrom1Up(last);

            return number2 - number1; 
        }

        /*
         * Meageprime number from 1 to upperBound
         */
        public static long CalculateTotalNumberOfMegaPrimesFrom1Up(long upperBound)
        {

        }
    }
}
