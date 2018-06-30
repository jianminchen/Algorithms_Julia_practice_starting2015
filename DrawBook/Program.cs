using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawBook
{
    class Program
    {
        static void Main(string[] args)
        {
            //TailorShop();
            RunSampleTestCase1(); 
        }

        private static void RunSampleTestCase1()
        {
            long sum = CalMinimumSumWithDistinctNoButtons(3, 2, new int[] { 4, 6, 6 });
            Debug.Assert(sum == 9); 
        }

        private static void TailorShop()
        {
            int[] data = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int colors = data[0];
            int cost = data[1];

            int[] minimumCosts = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            Console.WriteLine(CalMinimumSumWithDistinctNoButtons(colors, cost, minimumCosts)); 
        }
        /*
         * spec: 
         * 1. Distinct number of buttons of a certain color
         * each color 
         */
        private static long CalMinimumSumWithDistinctNoButtons(
            int colors, 
            int cost, 
            int[] minimumCosts
            )
        {
            int[] numbers = new int[colors];
            HashSet<int> data = new HashSet<int>();

            long sum = 0; 
            for(int i = 0; i < colors; i++)
            {
                int minimumValue = minimumCosts[i]; 
                int number = minimumValue / cost;

                number = (number * cost < minimumValue) ? (number + 1) : number;
                if (!data.Contains(number))
                {
                    numbers[i] = number;
                    data.Add(number);
                    sum += number; 
                }
                else
                {
                    for(int value = number+1; value <= 100000; value++)
                    {
                        if(!data.Contains(value))
                        {
                            numbers[i] = value;
                            data.Add(value);
                            sum += number; 
                            break; 
                        }
                    }
                }
            }           

            // add the sum of numbers
            return sum; 
        }
    }
}
