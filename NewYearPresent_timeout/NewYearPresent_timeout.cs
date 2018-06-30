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
            int[] arr = new int[] { 4, 5, 1, 5, 1, 9, 4, 5 };

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
        private static long calCom(int n, int k)
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

            for (int i = 0; i < n; i++)
            {
                buckets[arr[i]]++;
            }

            long count = 0;
            for (int i = 2; i < SIZE; i++)
            {
                int valCount = buckets[i];
                int width = i;

                // Try 2 - 
                // example 1, n2 = 5, 1 4 1 4 5 5
                // 5 = 4 + 1, 2 of 4, 2 of 1 - need to find 4 numbers, 2 pairs
                // two cases: one pair 1+4, twice
                // two different pairs
                if (valCount >= 2)
                {
                    long m = calCom(valCount, 2);

                    long twoSum = 0;
                    // case 1: one pair - twice, 1, 4, 1, 4 for 2 of 5 
                    for (int n1 = 1; n1 <= width/2; n1++)
                    {
                        int b1 = buckets[n1];
                        if (b1 < 2)
                            continue;

                        int n2 = width - n1;                         
                        int b2 = buckets[n2];
                        if (b2 < 2)
                           continue;

                        twoSum += calCom(b1,2)*calCom(b2,2);                        
                    }

                    // case 2: two pair - 1, 4, 2, 3 for 2 of 5
                    int pairs = 0;
                    int pairCount = 0; 
                    for (int n1 = 1; n1 <= width / 2; n1++)
                    {
                        int b1 = buckets[n1];
                        if (b1 == 0)
                            continue;

                        int n2 = width - n1;
                        int b2 = buckets[n2];
                        if (b2 == 0)
                            continue;

                        pairCount++;
                        pairs = b1 + b2;
                    }

                    if(pairCount >= 2)
                        twoSum += pairs/2; 

                    count += m * twoSum;
                }

                // Try 3 - for example, n2 =5, 1 2 2 5 5 5
                // n1 < n2 < n3 - avoid duplication
                // choose one from each of them - b1 * b2 * b3, easy to verify
                if (valCount >= 3)
                {
                    long m = calCom(valCount, 3);

                    int sum1 = 0;
                    for (int n1 = 1; n1 < width; n1++)
                    {
                        int b1 = buckets[n1];
                        if (b1 == 0)
                            continue;

                        for (int n2 = n1 + 1; (n1+n2) < width; n2++)
                        {
                            int b2 = buckets[n2];
                            if (b2 == 0)
                                continue;

                            int n3 = width - n1 - n2; 
                            if(n3 < n2)
                                continue; 

                            int b3 = buckets[n3];                            

                            sum1 = b1 * b2 * b3;                            
                        }
                    }

                    count += m * sum1;
                }
            }

            return count;
        }
    }
}
