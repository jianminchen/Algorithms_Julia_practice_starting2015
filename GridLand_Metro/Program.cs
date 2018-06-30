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
         * 
         * start: 4:23pm
         * out-of-memory for array bool[size,size], 
         * so try to use bool[k,size]
         * 4:54 - out of memory too
         */
        static void Main(string[] args)
        {
            string[] numA = Console.ReadLine().Split(' ');
            int nR = Convert.ToInt32(numA[0]);
            int mC = Convert.ToInt32(numA[1]);

            int k = Convert.ToInt32(numA[2]);

            int SIZE = 1000000000;
            bool[,] shortList = new bool[k, SIZE];
            Dictionary<int, int> data = new Dictionary<int, int>();

            int rowCount = 0; 

            long count  = 0;
            long result = nR * mC;            
            
            for (int i = 0; i < k; i++)
            {
                string[] arr = Console.ReadLine().Split(' ');

                int row    = Convert.ToInt32(arr[0]);
                int startC = Convert.ToInt32(arr[1]);
                int endC   = Convert.ToInt32(arr[2]);

                int rShortList = rowCount;
                if (data.ContainsKey(row))
                    rShortList = data[row];
                else
                {
                    data.Add(row, rowCount); 
                    rowCount++;
                }

                for (int j = startC; j <= endC; j++)
                {
                    if (shortList[rShortList, j])
                        continue;

                    count++;
                    shortList[rShortList, j] = true;
                }                              
            }

            Console.WriteLine(result - count);
        }
    }
}
