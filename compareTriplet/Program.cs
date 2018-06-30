using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compareTriplet
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] alice = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[] bob = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] result = calculate(alice, bob);
            Console.WriteLine(result[0].ToString() +" "+result[1].ToString()); 
        }

        private static int[] calculate(int[] arr1, int[] arr2)
        {
            int n = arr1.Length; 

            int c1=0, c2=0;
            for (int i = 0; i < n; i++ )
            {
                if (arr1[i] > arr2[i])
                    c1++;
                else if (arr1[i] < arr2[i])
                    c2++;
            }

            return new int[]{c1, c2}; 
        }
    }
}
