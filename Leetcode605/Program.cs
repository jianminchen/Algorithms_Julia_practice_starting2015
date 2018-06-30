using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantFlower
{
    /// <summary>
    /// Leetcode 506 - Can Place flowers
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// iterate the array, add first element with virtual left neigbhor with 0, and 
        /// last element with virtual right neighbor with 0. 
        /// The algorithm is to learn how to iterate the array, and work on previous, current
        /// and next 3 elements in the same time. 
        /// </summary>
        /// <param name="flowerbed"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            int count = 0;
            int length = flowerbed.Length;

            for (int i = 0; i < length && count < n; i++)
            {
                if (flowerbed[i] == 0)
                {
                    // get next and prev flower bed slot values. If i lies at the ends the next and 
                    // prev are considered as 0. 
                    int next = (i == length - 1) ? 0 : flowerbed[i + 1];
                    int previous = (i == 0) ? 0 : flowerbed[i - 1];

                    if (next == 0 && previous == 0)
                    {
                        flowerbed[i] = 1;
                        count++;
                    }
                }
            }

            return count == n;
        }
    }
}