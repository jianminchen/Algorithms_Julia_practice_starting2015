using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode312_burstBallon
{
    class Leetcode312_burstBallon
    {
        static void Main(string[] args)
        {
            RunTestcase(); 
        }

        public static void RunTestcase()
        {
            var numbers = new int[] {3,1,5,8}; 

            int result = GetMaximumCoins(numbers);
            Debug.Assert(result == 167); 
        }

        /// <summary>
        /// Leetcode 312: Burst Ballons
        /// https://leetcode.com/problems/burst-balloons/#/solutions
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int GetMaximumCoins(int[] numbers) {
            int length = numbers.Length; 

            int[] numbersExtra = new int[length + 2];

            int n = 1;
            foreach (var x in numbers)
            {
                if (x > 0)
                {
                    numbersExtra[n++] = x;
                }
            }

            numbersExtra[0] = numbersExtra[n++] = 1;

            int[][] memo = new int[n][];
            for (int i = 0; i < n; i++)
            {
                memo[i] = new int[n]; 
            }
            
            return BurstBallons_RecursiveMemorization(memo, numbersExtra, 0, n - 1);
        }

        /// <summary>
        /// code review on March 26, 2017
        /// Burst ballons using recursive solution 
        /// using memorization, and also using recurrence formula by the analysis:
        /// 
        /// </summary>
        /// <param name="memo"></param>
        /// <param name="numbers"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static int BurstBallons_RecursiveMemorization(int[][] memo, int[] numbers, int left, int right)
        {
            if (left + 1 == right)
            {
                return 0;
            }

            if (memo[left][right] > 0)
            {
                return memo[left][right];
            }

            int answer = 0;

            // work on the range [left, right], go over the last ballon to burst,
            // denote i, from left + 1 to right -1 
            // 3 cases 
            for (int i = left + 1; i < right; ++i)
            {
                int leftHand = BurstBallons_RecursiveMemorization(memo, numbers, left, i);
                int righHand = BurstBallons_RecursiveMemorization(memo, numbers, i, right);
                int defaultCase = numbers[left] * numbers[i] * numbers[right];

                // go over 3 cases
                // First case calculated using defaultCase, 
                // i is the last one to burst; 
                // Second case, leftHand,  a small range [left, i];
                // Third case,  rightHand, a small range [i, right];
                answer = Math.Max(answer, defaultCase + leftHand + righHand);
            }

            memo[left][right] = answer;
            return answer;
        }
    }
}
