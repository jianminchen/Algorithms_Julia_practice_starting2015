using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeCandies
{
    /*
     * https://leetcode.com/contest/leetcode-weekly-contest-31/problems/distribute-candies/
     */
    class Program
    {
        static void Main(string[] args)
        {
        }

        public int DistributeCandies(int[] candies)
        {
            int length = candies.Length;

            var numbers = new HashSet<int>();

            foreach (var item in candies)
            {
                numbers.Add(item); 
            }

            int half = length / 2;
            int size = numbers.Count; 
            if (size >= half)
            {
                return half; 
            }
            else
            {
                return size; 
            }
        }
    }
}
