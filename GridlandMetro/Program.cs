using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridlandMetro
{
    class Program
    {
        /*
         * start: 3:00pm 
         * read problem statement
         * 
         * start to code: 3:21pm 
         * write down the idea:
         * declare a two dimension array 10^9 x 10^9, bit
         * mark that it is reserved for track, 
         * avoid counting more than once. 
         * 100MB space - 10^18 bit
         * Let us give it try using the above idea
         * take 20 minutes break 
         * 
         * start to code: 3:55pm 
         * end of codeing: 4:11pm
         */
        static void Main(string[] args)
        {
            string[] numA = Console.ReadLine().Split(' ');
            int nR = Convert.ToInt32(numA[0]);
            int mC = Convert.ToInt32(numA[1]);

            int k = Convert.ToInt32(numA[2]);

            int SIZE = 1000000000;
            bool[,] reserved = new bool[SIZE, SIZE];

            long result = nR * mC;
            long count = 0; 
            for (int i = 0; i < k; i ++)
            {
                string[] arr = Console.ReadLine().Split(' ');

                int row    = Convert.ToInt32(arr[0]);
                int startC = Convert.ToInt32(arr[1]);
                int endC   = Convert.ToInt32(arr[2]); 

                for(int j= startC; j <= endC; j++)
                {
                    if (reserved[i, j])
                        continue;

                    count++; 
                    reserved[i, j] = true;                                        
                }
            }

            Console.WriteLine(result - count); 
        }
    }
}
