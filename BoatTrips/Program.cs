using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatTrips
{
    class Program
    {
        /*
         * https://www.hackerrank.com/contests/w28/challenges/boat-trip
         */
        static void Main(string[] args)
        {
            int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse); 
            int trips = input[0];
            int boats = input[1]; 
            int maximumCapacity = input[2]; // per boat

            int[] passengers = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            bool result = CaculateEachTrip(trips, boats, maximumCapacity, passengers); 
            if( result )
            {
                Console.WriteLine("Yes"); 
            }
            else
            {
                Console.WriteLine("No"); 
            }
        }

        public static bool CaculateEachTrip(int trips, int boats, int maximumCapacity, int[] passengers)
        {
            for(int i = 0; i < trips; i++)
            {
                int maximum = boats * maximumCapacity;
                if (passengers[i] > maximum)
                    return false; 
            }

            return true; 
        }
    }
}
