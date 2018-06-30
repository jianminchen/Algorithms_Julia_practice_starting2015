using System;
using System.Collections.Generic;

class Solution
{
    public static int[] FindArrayQuadruplet(int[] arr, int s) 
    {
        if (arr == null || arr.Length < 4) 
        {
            return new int[0];
        }

        Array.Sort(arr); 

        var dictionary = saveTwoSumToDictionary(arr); 

        int length = arr.Length;

        for (int first = 0; first < length - 3; first++) 
        {
            for (int second = first + 1; second < length - 2; second++) 
            {
                var firstTwoSum = arr[first] + arr[second]; 
                var no1 = arr[first]; 
                var no2 = arr[second]; 

                var search = s - firstTwoSum; 
                 
                if (!dictionary.ContainsKey(search))
                {
                    continue;
                }
                
                var options = dictionary[search];
                foreach (int[] pair in options)
                {                    
                    var no3 = arr[pair[0]];
                    var no4 = arr[pair[1]];

                    var index3 = pair[0];

                    var unique = second < index3; 
                    if (unique)
                    {
                        return new int[] { no1, no2, no3, no4 };  
                    }
                }
            }
        }

        return new int[0];
    }

    private static IDictionary<int, IList<int[]>> saveTwoSumToDictionary(int[] arr) 
    {
        var twoSum = new Dictionary<int, IList<int[]>>(); 

        int length = arr.Length; 
        for (int i = 0; i < length - 1; i++) 
        {
            for (int j = i + 1; j < length; j++) 
            {
                var no1 = arr[i];
                var no2 = arr[j];

                var sum = no1 + no2; 
                var thePair = new int[] {i,j}; 
                
                if(!twoSum.ContainsKey(sum))
                {
                    var newList = new List<int[]>();                    
                    twoSum.Add(sum, newList);
                }

                twoSum[sum].Add(thePair);
            }
        }

        return twoSum;
    }
}