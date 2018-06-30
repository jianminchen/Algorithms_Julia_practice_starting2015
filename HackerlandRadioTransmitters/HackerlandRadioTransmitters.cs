using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerlandRadioTransmitters
{
    /*
     * start from 8:29pm 
     * https://www.hackerrank.com/contests/university-codesprint/challenges/hackerland-radio-transmitters
     * 
     */
    class HackerlandRadioTransmitters
    {
        static void Main(string[] args)
        {
            process(); 
            //testCase1(); 
            //testCase2(); 
        }

        private static void process()
        {
            string[] arr = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(arr[0]);
            int k = Convert.ToInt32(arr[1]);

            int[] house = ToInt(Console.ReadLine().Split(' '));

            Console.WriteLine(calculateTransmitters(n, k, house)); 
        }

        private static void testCase1()
        {
            int[] house = new int[8]{7,2,4,6,5,9,12, 11}; 

            int test = calculateTransmitters(8,2,house); 
        }

        private static void testCase2()
        {
            int[] house = new int[7] { 9, 5, 4, 2, 6, 15, 12 };

            int test = calculateTransmitters(7, 2, house); 
        }

        /*
         * start: 8:33pm
         * exit: 9:33pm
         */
        private static int calculateTransmitters(int n, int k, int[] house)
        {
            int count = 0;

            Array.Sort(house);

            int transmiterPos = 0;
            int start = -1; 
            foreach(int item in house)
            {
                if (start < 0)
                {
                    start = item;
                    count++;
                    transmiterPos = item; 
                }

                int range = item - start;
                transmiterPos = (range <= k) ? item : transmiterPos;

               // if(range > 2 * k)
                if(item - transmiterPos > k)
                {
                    start = item;
                    transmiterPos = item; 
                    count++; 
                }
            }

            return count; 
        }

        private static int[] ToInt(string[] arr)
        {
            int len = arr.Length;
            int[] res = new int[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = Convert.ToInt32(arr[i]);
            }

            return res;
        }
    }
}
