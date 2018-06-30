using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanPlaceFlower
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = CanPlaceFlowers(new int[] { 1, 0, 0, 0, 1, 0, 0 }, 2);
        }

        public static bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            if (n == 0)
            {
                return true;
            }

            if (flowerbed == null || flowerbed.Length == 0)
            {
                return false;
            }

            int length = flowerbed.Length;

            bool found = false;
            int start = -1;
            int flowersCanPlant = 0;
            for (int i = 0; i < length; i++)
            {
                var visit = flowerbed[i];
                if (!found && visit == 0)
                {
                    found = true;
                    start = i;
                }

                bool visitLast = i == length -1; 
                if (found && (visit == 1 || visitLast))
                {
                    // length of zero
                    var lengthZero = (visit == 1)? (i - start) : (i - start + 1);
                    bool includingFirst = start == 0;
                    int lastPosition = (visit == 1) ? (i - 1) : i;
                    bool includingLast = lastPosition == length - 1;

                    if (!includingFirst)
                    {
                        lengthZero--;
                    }

                    if (!includingLast)
                    {
                        lengthZero--;
                    }

                    if (lengthZero > 0)
                    {
                        flowersCanPlant += (lengthZero + 1) / 2;
                    }

                    found = false;
                }
            }

            return (flowersCanPlant >= n);
        }
    }
}
