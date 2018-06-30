using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummingPieces
{
    class Program
    {
        /*
         * 
         * start: 11:09 
         * read problem statement
         * https://www.hackerrank.com/contests/world-codesprint-7/challenges/summing-pieces
         * work on DP formula 
         * from 11:00 -> 12:46pm 
         * work on two test cases 
         * Fully understand the fomula, let us give it a try using code
         * 
         * put code to run: 1:37pm 
         * debugging more 50+ minutes
         * put int testing on 3:02pm 
        */
        static void Main(string[] args)
        {
            //test(); 
            program(); 
        }

        private static void test()
        {
            int n = 3;
            string[] arr = new string[3]{"1","3","6"};

            Console.WriteLine(calculateUsingDP(n, arr)); 
        }
        /*
         * start: 1:39pm 
         * 
         */
        private static void program()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            string[] arr = Console.ReadLine().Split(' ');

            Console.WriteLine(calculateUsingDP(n, arr)); 
        }
        /*
         * work on: 1:20pm 
         * continue: 1:20pm - ? 
         */
        private static long calculateUsingDP(int n, string[] arr)
        {   
            long module = 1000 * 1000 * 1000 + 7;
         
            int[]  input  = convertInt(arr); 
            int[]  kCount = new int[n];
            int[]  kSum   = new int[n]; 
            long[] sumA   = new long[n];

            kCount[0] = 1;
            kSum[0] = 1; 

            sumA[0] = input[0];            
            for(int i = 1; i < n; i++)
            {
                long[] tmp = new long[i+1];
                string[] log = new string[i+1]; 
                for(int j = i; j >= 0; j--)
                {
                    int start   = j;
                    int end     = i;
 
                    int leftEnd = j - 1;
                    if (leftEnd >= 0)
                    {
                        long prod = calcProduction(input, start, end);
                        long v1 = sumA[leftEnd];
                        long v2 = kCount[leftEnd]; 

                        tmp[start] = (v1 + v2 * prod) % module;
                        log[start] = v1 + " " + v2 + "*" + prod; 
                    }
                    else
                    {                        
                        long v1 = calcProduction(input, 0, i); 
                        tmp[0] =  v1 % module;
                        log[0] =  v1 +""; 
                    }
                }

                sumA[i]  = tmp.Sum() % module;                 

                kCount[i] = kSum[i - 1] + 1;
                kSum[i]   = kSum[i - 1] + kCount[i]; 
            }

            return sumA[n - 1]; 
        }

        /*
         * start: 1:26pm 
         * exit: 1:27pm 
         * test case: 
         * 1 3
         * calculate: 2 (1+3)
         */
        private static long getProduction(
            int[] arr, 
            int start, 
            int end)
        {
            long module = 1000 * 1000 * 1000 + 7;

            long sum = 0; 
            for(int i = start; i <= end; i++)
            {
                sum += arr[i];
                sum = sum % module; 
            }


            return sum; 
        }

        /*
         * start: 1:06pm
         * exit: 1:20pm
         */
        private static long calcProduction(
            int[] input,            
            int   start, 
            int   end
            )
        {
            long module = 1000 * 1000 * 1000 + 7;

            int count = end - start + 1; 
            long sum = 0; 
            for(int i = start; i <= end; i++)
            {
                sum = (sum + input[i]) % module; 
            }

            // multiplcation of count 
            if (sum < long.MaxValue / count)
            {
                sum *= count;
                sum = sum % module;
            }
            else
            {
                long tmp = 0; 
                for (int i = 0; i < count; i++)
                {
                    tmp += sum;
                    tmp = tmp % module; 
                }
                sum = tmp; 
            }

            return sum; 
        }

        private static int[] convertInt(string[] arr)
        {
            int n = arr.Length;
            int[] res = new int[n];

            for (int i = 0; i < n; i++)
                res[i] = Convert.ToInt32(arr[i]);

            return res; 
        }
    }
}
