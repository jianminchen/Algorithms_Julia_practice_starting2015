using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minimumLoss
{
    class Program
    {
        static void Main(string[] args)
        {
            Process();
            //Testcase2(); 
        }

        private static void Process()
        {
            int n = int.Parse(Console.ReadLine());

            Int64[] prices = new Int64[n];

            string[] arr = Console.ReadLine().Split(' ');
            prices = Array.ConvertAll(arr, Int64.Parse);

            Console.WriteLine(MinimumLossCal(n, prices));
        }

        private static void Testcase2()
        {
            Int64[] prices = new Int64[5] { 20, 7, 8, 2, 5 };

            Console.WriteLine(MinimumLossCal(5, prices));
        }

        private static void Testcase3()
        {
            Int64[] prices = new Int64[4] { 2, 3, 4, 1 };

            Console.WriteLine(MinimumLossCal(4, prices));
        }


        /*
         * minimum loss
         *  
         * 
         * read Java TreeSet floor method:
         * https://www.tutorialspoint.com/java/util/treeset_floor.htm
         * 
         * http://stackoverflow.com/questions/4872946/linq-query-to-select-top-five
         * 
         * http://stackoverflow.com/questions/11549580/find-key-with-max-value-from-sorteddictionary
         * 
         * http://stackoverflow.com/questions/1635497/orderby-descending-in-lambda-expression
         * 
         * timeout issue - try to find LINQ has a solution or not
         * http://stackoverflow.com/questions/14675108/sortedset-sortedlist-with-better-linq-performance
         * 
         * 
         */
        private static Int64 MinimumLossCal(int n, Int64[] prices)
        {
            SortedSet<Int64> data = new SortedSet<Int64>();

            Int64 minLoss = Int64.MaxValue;

            for (int i = n - 1; i >= 0; i--)
            {
                var smaller = data.Where(p => p < prices[i]).OrderByDescending(p => p).Take(1);
                if (smaller.Any())
                {
                    Int64 newDiff = prices[i] - smaller.Last();

                    minLoss = (newDiff < minLoss) ? newDiff : minLoss;
                }

                data.Add(prices[i]);
            }

            return minLoss;
        }
    }
}