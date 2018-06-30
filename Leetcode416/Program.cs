using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode416_PartitionEqualSumSubset
{
    class Leetcode416_PartitionEqualSumSubset
    {
        /// <summary>
        /// Leetcode 416: Partition Equal Subset Sum
        /// https://leetcode.com/problems/partition-equal-subset-sum/#/description
        /// 
        /// Since the problem is a 0-1 backpack problem, we only have two choices which are take or not. 
        /// Thus in this problem, by using the sum value as the index of DP array, we transfer the problem 
        /// to "whether should we take the currently visited number into the sum or not".
        /// To construct the DP recurrence, when we are visiting nums[i] and to find partition of sum j: 
        /// if we do not take the nums[i], then the current iteration does not make any difference on 
        /// current DP value; if we take the nums[i], then we need to find whether the (new_sum = j - nums[i]) 
        /// can be constructed. If any of this two construction can work, the partition of sum == j can be reached.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RunTestcase();
        }

        public static void RunTestcase()
        {
            var result = CanPartition(new int[] { 1, 2, 3, 4 });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static bool CanPartition(int[] numbers)
        {
            // check edge case
            if (numbers == null || numbers.Length == 0)
            {
                return true;
            }

            // preprocess
            int volumn = 0;
            foreach (var num in numbers)
            {
                volumn += num;
            }

            if (volumn % 2 != 0)
            {
                return false;
            }

            int target = volumn /= 2;

            // dynamic programming defintion 
            bool[] dp = new bool[target + 1];

            // dynamic programming initialization
            dp[0] = true; // empty set, the sum is zero. 

            // dp transition
            for (int i = 0; i < numbers.Length; i++)
            {
                var visit = numbers[i];

                // for any sum bigger than visit value, current element in the array can contribute. 
                // if it is true, then continue; otherwise look up the value at sum - visit.
                for (int sum = target; sum >= visit; sum--)
                {
                    // 0 1 backpack problem, include visit number or exclude the number. 
                    dp[sum] = dp[sum] || dp[sum - visit];
                }
            }

            return dp[volumn];
        }
    }
}