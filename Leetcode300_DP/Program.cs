using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode300_LongestIncreasingSubsequence_DP
{
    /// <summary>
    /// Leetcode 300 
    /// https://leetcode.com/problems/longest-increasing-subsequence/#/description
    /// Using dynamic programming 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            testcase2();
        }

        public static void testcase1()
        {
            var testcase = new int[] { 10, 9, 2, 5, 3, 7, 101, 18 };

            var length = lengthOfLIS(testcase);
            Debug.Assert(length == 3);
        }

        //10,9,2,5,3,7,101,18
        public static void testcase2()
        {
            var testcase = new int[] { 10, 9, 2, 5, 3, 7, 101, 18 };

            var length = lengthOfLIS(testcase);
            Debug.Assert(length == 4);
        }

        // There's a typical DP solution with O(N^2) Time and O(N) space 
        // DP[i] means the result ends at i
        // So for dp[i], dp[i] is max(dp[j]+1), for all j < i and nums[j] < nums[i]
        public static int lengthOfLIS(int[] nums)
        {
            int size = nums.Length;

            if (size == 0)
            {
                return 0;
            }

            var subsequence = new int[size];

            int length = 1;
            for (int i = 0; i < size; ++i)
            {
                var current = nums[i];
                subsequence[i] = 1;

                // get maximum value from all options
                for (int j = 0; j < i; ++j)
                {
                    var visit = nums[j];
                    if (visit < current)
                    {
                        subsequence[i] = Math.Max(subsequence[i], subsequence[j] + 1);
                    }
                }

                // update the length if need
                length = Math.Max(length, subsequence[i]);
            }

            return length;
        }
    }
}