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
         * 
         * Start: 3:06pm 
         * work on wrong answer, timeout issue 
         * Add memorization into the code 
         * Continue to more code more readable, less complicate 
         * 
         * start: 7:36pm
         * work on bug fix using module -> 11 point out of 40
         * continue to work on timeout 3 second issue
         * 
         * bug fix NO2: 11.03 -> 13.79
         * comment out debug code: 
         * 
         * push long -> int
         * 
         * special discussion in the function:
         * multiplicationWithCare
         * score from 13.79 -> 19.31
         * Ranking from 732 -> 692
         * 
         * How to get more from 19.31? 
         * Remove unnecessary module calculation 
        */
        static void Main(string[] args)
        {
            //test(); 
            program();
        }

        private static void test()
        {
            int n = 3;
            string[] arr = new string[3] { "1", "3", "6" };

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
         * IMPORTANT FIX:  
         * bug fix NO1: this fix -> point 4 -> point 11
         */
        private static long calculateUsingDP(int n, string[] arr)
        {
            int module = 1000 * 1000 * 1000 + 7;

            int[] input = convertInt(arr);
            int[] kCount = new int[n];
            int[] kSum = new int[n];
            int[] sumA = new int[n];

            kCount[0] = 1;
            kSum[0] = 1;

            sumA[0] = input[0];
            int inputFirst = input[0]; 
            for (int i = 1; i < n; i++)
            {
                int[] tmp = new int[i + 1];                

                int memo = 0;
                for (int j = i; j >= 0; j--)
                {
                    int start = j;
                    int end   = i;
                    int count = end - start + 1;

                    int leftEnd = j - 1;
                    if (leftEnd >= 0)
                    {
                        int tmpSum = (memo + input[start]) ;

                        tmp[start] = sumA[leftEnd] + mxn(kCount[leftEnd], mxn(tmpSum, count));                        

                        memo = tmpSum;
                    }
                    else
                    {                                                                                           
                        tmp[0] = mxn(
                                    memo + inputFirst, 
                                    i + 1);                                               
                    }
                }
                
                sumA[i] = arraySum(tmp, module);

                kCount[i] = (kSum[i - 1] + 1) % (int)module;         // bug fix NO1
                kSum[i]   = (kSum[i - 1] + kCount[i]) % (int)module; //bug fix NO1
            }

            return sumA[n - 1];
        }

        /*
         * start: 6:44pm
         * exit:  6:46pm 
         */
        private static int arraySum(int[] tmp, int module)
        {
            int result = 0;
            foreach (int item in tmp)
            {
                result = (result + item) % module;
            }

            return result;
        }

        /*
         * start: 3:26pm
         * Test case: 
         * sum * count 
         * Need to figure out how to shorten the time to do calculation
         * Test the funnction
         */
        private static int mxn(int sum, int count)
        {
            int module = 1000 * 1000 * 1000 + 7;
            long MAX = long.MaxValue;
            int step = (int)(MAX / sum) % module;

            int result = 0;
            long p = (long)sum * (long)count;
            int pre = (int)(p % module);

            if (p < MAX)
                result = pre;
            else
            {
                int no = count / step;

                int small = count - (no * step);

                result = (sum * step) % module;
                result = (result * no) % module;
                result += (sum * small);
                result = result % module;
            }

            return result;
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
