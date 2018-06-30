using System;
using System.Collections;
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
         * 
         * start: 5:34pm 
         * Use Ditionary - still out-of-memory
         * 
         * 5:57 start to bug fix
         * use BitArray to replace bool array - out-of-space bug is gone
         * But timeout issue needs to be fixed
         * 
         */
        static void Main(string[] args)
        {
            string[] numA = Console.ReadLine().Split(' ');
            int nR = Convert.ToInt32(numA[0]);
            int mC = Convert.ToInt32(numA[1]);

            int k = Convert.ToInt32(numA[2]);

            int[] map = new int[k];
            IList<int>  rows = new List<int>(); 
            IList<Tuple<int, int>> tuples = new List<Tuple<int, int>>();

            for (int i = 0; i < k; i++)
            {
                string[] arr = Console.ReadLine().Split(' ');

                int row    = Convert.ToInt32(arr[0]);
                int startC = Convert.ToInt32(arr[1]);
                int endC   = Convert.ToInt32(arr[2]);

                rows.Add(row); 
                tuples.Add(new Tuple<int, int>(startC, endC));
            }

            Console.WriteLine(calculate(rows, tuples, nR, mC));
        }

        /*
         * start: 5:11pm 
         * start to work on the function 
         * end: 5:28pm 
         * Walk through the code
         */
        private static string calculate(
            IList<int> rows,
            IList<Tuple<int, int>> tuples,
            int nR,
            int mC
            )
        {
            int[] sortedRows = rows.ToArray();

            Array.Sort(sortedRows);

            int prev = -1;
            long sum = 0;

            int SIZE = 1000 * 1000 * 1000;
            BitArray reserverd = new BitArray(SIZE);

            foreach (int row in sortedRows)
            {
                int runner = Array.IndexOf(rows.ToArray(), row);

                Tuple<int, int> colR = tuples[runner];
                int start = colR.Item1;
                int end = colR.Item2;

                if (prev == -1 ||
                    runner != prev)
                {
                    for (int i = 0; i < SIZE; i++)
                        reserverd[i] = false;

                    for (int j = start; j <= end; j++)
                        reserverd[j] = true;

                    sum += end - start + 1;
                }
                else
                {
                    for (int j = start; j <= end; j++)
                    {
                        if (reserverd[j])
                            continue;

                        sum++;
                        reserverd[j] = true;
                    }
                }
            }

            return ((long)(nR * mC) - sum).ToString();
        }
    }
}
