﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode_15_3Sum
{
    /*
    * 
    * Work on this 3 sum algorithm
    * 
    * Leetcode 15: 3 sum
    * https://leetcode.com/problems/3sum/
    * 
    * Given an array S of n integers, are there elements a, b, c in S 
    * such that a + b + c = 0? Find all unique triplets in the array 
    * which gives the sum of zero.
    * 
      Note:
        Elements in a triplet (a,b,c) must be in non-descending order. (ie, a ≤ b ≤ c)
        The solution set must not contain duplicate triplets.  
    * 
    For example, given array S = {-1 0 1 2 -1 -4},
    A solution set is:
    (-1,  0, 1)
    (-1, -1, 2)
    *          
    */
    class Program
    {
        static void Main(string[] args)
        {
            // test 3 sum            
            // 2 lists, one is -1, 0, 1, second one is -1, -1, 2
            int[] array = new int[6] { -1, 0, 1, 2, -1, -4 };

            IList<IList<int>> triplets = ThreeSum(array);

            Debug.Assert(triplets.Count == 2);
            Debug.Assert(String.Join(",", triplets[0].ToArray()).CompareTo("-1,-1,2") == 0);
            Debug.Assert(String.Join(",", triplets[1].ToArray()).CompareTo("-1,0,1") == 0);
        }
        /*
         * @nums - the array containing the numbers
         * 
         * 3 sum can be solved using 2 sum algorithm, 
         * 2 sum algorithm - optimal solution is using two pointers, time complexity is O(nlogn), 
         *   sorting takes O(nlogn), and two pointer algorithm is O(n), so overall is O(nlogn).
         * Time complexity for 3 sum algorithm: 
         *   O(n*n)
         */
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> results = new List<IList<int>>();
            if (nums == null)
                return results;

            int length = nums.Length;
            if (length < 3)
                return results;
            
            int[] array = new int[nums.Length];
            Array.Copy(nums, 0, array, 0, nums.Length);

            Array.Sort(array);            

            int target = 0;
            int firstNo;
            int newTarget;
            int start;
            int end;
            for (int i = 0; i < length - 2; i++)
            {
                firstNo = array[i];

                if (i > 0 && firstNo == array[i - 1])
                    continue;

                // using two pointers to go through once the array, find two sum value 
                newTarget = target - firstNo;
                start = i + 1;
                end = length - 1;
                while (start < end)
                {
                    int twoSum = array[start] + array[end];
                    if (twoSum < newTarget)
                    {
                        StartMoveNext(array, ref start, end);                         
                    }
                    else if (twoSum > newTarget)
                    {
                        EndMoveNext(array, start, ref end);                         
                    }
                    else
                    {
                        results.Add(new List<int> { firstNo, array[start], array[end] });

                        StartMoveNext(array, ref start, end);
                        EndMoveNext(array, start, ref end);                         
                    }
                }
            }

            return results;
        }

        /*
         * start pointer moves to next one until it is a new value. 
         */
        private static void StartMoveNext(int[] nums, ref int start, int end)
        {
            start++;
            while (start < end && StartDuplicate(nums, start))
                start++;
        }

        /*
         * start pointer moves to next one until it is a new value. 
         */
        private static void EndMoveNext(int[] nums, int start, ref int end)
        {
            end--;
            while (start < end && EndDuplicate(nums, end))
                end--;  
        }

        /*
         * start pointer moves from left to right, 
         * then, checking is to compare nums in the index of start -1 and index
         */
        private static bool StartDuplicate(int[] nums, int start)
        {
            return nums[start - 1] == nums[start]; 
        }

        /*
         * end pointer moves from right to left, 
         * then, checking is to compare nums in the index of end + 1 and end
         */
        private static bool EndDuplicate(int[] nums, int end)
        {
            return nums[end+1] == nums[end]; 
        }
        /* 
         * -1, 0, 1 -> key string" "-1,0,1,"
         */
        private static string PrepareKey(int[] arr, int length)
        {
            string key = string.Empty;

            for (int j = 0; j < length; j++)
            {
                key += arr[j].ToString();
                key += ",";
            }

            return key;
        }
    }
}