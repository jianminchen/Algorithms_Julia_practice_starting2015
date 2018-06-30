using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresent
{
    class Program
    {
        static void Main(string[] args)
        {
            process();
            //testcase1(); 
        }

        private static void testcase1()
        {
            int n = 8;
            int[] arr = new int[]{4,5,1,5,1,9,4,5};

            long res = calculate(n, arr); 
        }

        private static void process()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            Console.WriteLine(calculate(n, arr)); 
        }
        /*
         * test c(6, 3) = 6 *5 *4/(3*2*1)
         */
        private static long calCombination(int n, int k)
        {
            long res = 1;
            for (int i = n; i >= (n - k + 1); i--)
                res *= i;

            for (int i = 1; i <= k; i++)
            {
                res /= i;
            }

            return res;
        }

        private static long calculate(int n, int[] arr)
        {
            int SIZE = 10000001; 

            int[] buckets = new int[SIZE]; 

            for(int i = 0;i < n; i++)
            {
                buckets[arr[i]]++; 
            }

            long count = 0; 
            for(int i=2; i< SIZE; i++)
            {
                int valCount = buckets[i];
                int width = i; 

                // Try 2 - for example, n2 = 5, 1 4 1 4 5 5
                //  4 = 2+2
                if (valCount >= 2)
                {
                    long m = calCombination(valCount, 2);

                    int sum1 = 0;
                    for (int n1 = 1; n1 < width; n1++)
                    {
                            int b1 = buckets[n1];
                            if (b1 == 0 )
                                continue;

                            for (int n2 = n1 + 1; n2 < width; n2++)
                            {                                
                                int b2 = buckets[n2];
                                if (b2 == 0 || Math.Abs(n1 + n2 - width) > 0)
                                    continue;

                                sum1++; 
                            }
                     }                     
                    
                     count += m * sum1;  
                }

                // Try 3 - for example, n2 =5, 1 2 2 5 5 5
                if(valCount>=3)
                {
                    long m = calCombination(valCount, 3);

                    int sum1 = 0;
                    for (int n1 = 1; n1 < width; n1++)
                    {
                        int b1 = buckets[n1]; 
                        if (b1 == 0 )
                            continue;

                        for (int n2 = n1 + 1; n2 < width; n2++)
                        {
                            int b2 = buckets[n2];
                            if (b2 == 0 || (n1 + n2) >= width)
                                continue;

                            for (int n3 = n2 + 1; n3 < width; n3++)
                            {
                                int b3 = buckets[n3]; 
                                if (buckets[n3] == 0 || 
                                    Math.Abs(n1+n2+n3-width) > 0)
                                    continue; 
                                
                                sum1++;
                            }
                        }
                    }

                    count += m * sum1; 
                }                
            }

            return count; 
        }
    }

    
}
