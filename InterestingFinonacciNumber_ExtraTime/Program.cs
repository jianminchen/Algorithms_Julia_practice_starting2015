using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestingFibonacciSum
{
    class InterestingFibonacciSum_OutofMemory
    {
        /*
         * https://www.hackerrank.com/contests/walmart-codesprint-algo/challenges/fibonacci-sum-1
         * Oct. 29, 2016
         * Spent two hours to read problem statement
         * 11:30am - 1:30am
         * 
         * 7:12pm - start to write some code
         * Figure out things in detail
         * 
         * 9:12pm complete the coding
         * 9:12pm put into HackerRank to test the code 
         * 
         */
        static void Main(string[] args)
        {
            //testing2(); 
            process(); 
        }

        private static void testing1()
        {
            int n = 2;
            int[] arr = {1,1 };

            int result = calculate(n, arr); 
        }

        private static void testing2()
        {
            int n = 3;
            int[] arr = { 1, 2, 3 };

            int result = calculate(n, arr);
        }

        /*
         * 9:33pm 
         * first, test the function is working 
         * ignore out-of-memory issue 
         */
        private static void process()
        {
            int q = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < q; i++)
            {
                int n = Convert.ToInt32(Console.ReadLine());   // 10^5 upper limit, size of array
                int[] arr = new int[n];

                arr = cToInt(Console.ReadLine().Split(' '));

                Console.WriteLine(calculate(n, arr));
            }
        }

        /*
         * start: 7:15pm 
         * end: 7:19pm
         */
        private static int[] cToInt(string[] arr)
        {
            if (arr == null || arr.Length == 0)
                return new int[0];

            int n = arr.Length;
            int[] res = new int[n];

            for (int i = 0; i < n; i++)
            {
                res[i] = Convert.ToInt32(arr[i]);
            }

            return res;
        }

        /*
         * Oct. 29, 2016
         * start: 7:20pm 
         * n - 10^5  upper limit
         * n^2 - 10^10 how many combination of sum -> n ? 
         * 
         * optimization II:
         * AL...R - subarray's sum - how many? n^2 
         * L...R - n^2 - 10^10 -> reduce to O(n), 
         * sum from 0 to n, denoted as sumC[i], 
         * then, sum(A) = sumC[R] - sumC[L]. 
         * 
         * 8:16pm 
         * think about "the sum of n over all queries does not exceed 10^5"
         * Guess what it means...
         * Each query, array size ni, 
         * n1 + n2 + ... + nq <= 10^5
         * 
         * 8:22pm 
         * 9:12pm completet the coding
         * 
         * 10:12pm pass sample test cases
         */
        private static int calculate(int n, int[] arr)
        {
            int count = 0;
            int module = 1000000000 + 7;

            int[] sum1 = sumC(n, arr);
            //int[] fiboA = fibo();

            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                {
                    int L = i;
                    int R = j;

                    int AL2R = (L == R) ? arr[L] : (sum1[R] - sum1[L] + arr[L]);
                    
                    count = (count + fibo2(AL2R)) % module;
                }

            return count;
        }

        /*
         * 8:25pm 
         * To reduce the variation of calculation from O(n^2) to O(n)
         * sum(A) = sumC[R] - sumC[L]
         * 
         * 8:40pm 
         * L, R two variable, each is O(n), so LR pair will be O(n^2)
         * but, we use sumC to get the difference based on O(n) size array
         * 
         * 8:55pm 
         * cut the size to 10^9 + 7
         * module calculation 
         */
        private static int[] sumC(int n, int[] arr)
        {
            int[] sum = new int[n];
            int module = 1000000000 + 7;

            sum[0] = arr[0];
            for (int i = 1; i < n; i++)
            {
                sum[i] = (sum[i - 1] + arr[i]) % module;
            }

            return sum;
        }

        /*
         * 8:29pm 
         * work on Finbonacci number calculation 
         * using Dynamic programming, bottom up 
         * using memorization 
         * C# int32 max value is 2147483647, 2*10^9 
         * 
         * 8:35pm 
         * Try to figure out the maximum number for Finbonacci's calculation
         * 1 <= ai <= 10^9 
         * 
         * Try to calculate once, from 0 to max value
         * 
         * Data type, timeout issues, and other things. 
         * Let us set the maximum size - 10^9 + 7
         * 0 - 10^9 + 6 
         * 
         * 9:02pm 
         * 
         * 9:21pm out-of-memory 
         * 512MB 
         * fib size will be 4000MB, 
         * maximum size is 512MB 
         * out-of-memory issue - fibo function design problem 
         */
        private static int[] fibo()
        {
            int SIZE = 1000000000 + 7;

            int[] fib = new int[SIZE];
            fib[0] = 0;
            fib[1] = 1;
            for (int i = 2; i < SIZE; i++)
            {
                fib[i] = (fib[i - 1] + fib[i - 2]) % SIZE;
            }

            return fib;
        }

        /*
         * 9:46pm 
         * avoid out-of-memory issue
         * calculate on-the-fly
         * no memorization at all
         * 
         */
        private static int fibo2(int n)
        {
            int SIZE = 1000000000 + 7;

            if (n == 0)
                return 0;
            if (n == 1)
                return 1; 

            int tmp0 = 0;
            int tmp1 = 1;
            int sum  = 0;
            for (int i = 2; i <= n; i++)
            {
                sum = (tmp0 + tmp1) % SIZE;
                tmp0 = tmp1;
                tmp1 = sum; 
            }

            return sum;

        }
    }
}
