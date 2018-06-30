using System;
using System.Collections.Generic;

class Solution
{
    // -2, -1, 0, 1, 2 k = 2
    // [-2, 0], [-1, 1], [0, 2]  
    // -2, -1  1 < 2, move right pointer, 
    public static int FindPairsWithGivenDifference(int[] arr, int k)
    {
        // your code goes here
        if (arr == null || arr.Length == 0 || k < 0)
        {
            return 0;
        }

        Array.Sort(arr);  // ascending order

        var pairs = new List<int[]>();
        var length = arr.Length; //  5

        var left = 0;
        var leftValue = arr[0]; //-2
        for (int i = 1; i < length; )  // remove i++ after mocking 
        {
            var current = arr[i]; // -1, 0, 1

            leftValue = (left < length) ? arr[left] : leftValue; // possible issue

            if (left == length)
            {
                break;
            }

            var difference = current - leftValue;  // 1, 2
            var isEqual = difference == k;
            var isSmaller = difference < k;  // 1 < 2
            var isBigger = difference > k;

            if (isEqual)
            {

                pairs.Add(new int[] { current, leftValue }); // [-2, 0]

                // reset left 
                left++;          // 1     
                i++;   // added after mocking 
            }

            else if (isSmaller)
            {
                i++;  // added after mocking 
                continue;
            }

            else if (isBigger)
            {
                left++;
            }
        }

        return pairs.Count; 
    }

    static void Main(string[] args)
    {
        var result = FindPairsWithGivenDifference(new int[] { 1, 3, 1, 5, 4 }, 0); 
    }
}