using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    static bool[, ,] w;
    static void Main(String[] args)
    {
        Process(); 
        //Testing();
    }

    private static void Testing()
    {
        n = 3;
        s = 3;
        k = 4;

        w = new bool[n, s + 1, k + 1];

        int[] arr = new int[n];
        IList<string> helper = new List<string>();

        if (Construct(arr, 0, 0, 0) == 1)
            Console.WriteLine(string.Join(" ", arr));
        else
            Console.WriteLine(-1);

        string s2 = String.Join("\r\n", helper.ToArray());
    }

    private static void Process()
    {
        int _tc_ = int.Parse(Console.ReadLine());
        while (_tc_-- > 0)
        {
            ArrayConstruction();
        }
    }

    static int n, s, k;
    static void ArrayConstruction()
    {
        var tmp = Console.ReadLine().Split(' ');

        n = int.Parse(tmp[0]);
        s = int.Parse(tmp[1]);
        k = int.Parse(tmp[2]);

        w = new bool[n, s + 1, k + 1];

        int[] arr = new int[n];        

        if (Construct(arr, 0, 0, 0) == 1)
            Console.WriteLine(string.Join(" ", arr));
        else
            Console.WriteLine(-1);
    }

    /*     
     * Nov. 29, 2016
     * Add comment above the recursive detail to illustrate the idea
     */
    static int Construct(
        int[] arr,
        int   sum,
        int   diffsum,
        int   p
        )
    {       
        if (p == n)
        {
            if (sum == s && diffsum == k)
            {                
                return 1;
            }
            
            return 0;
        }

        // this pruning is very important, otherwise timeout! 
        // w[p, sum, diffsum] should only be processed once
        if (w[p, sum, diffsum])
        {            
            return -1;
        }
        else
        {
            w[p, sum, diffsum] = true;
        }

        int i = 0;

        if (p != 0)
        {
            i = arr[p - 1];            
        }
       
        for (; i <= s; i++)
        {
            // the array is in non-decreasing order, so, lower limit of newSum 
            // can be estimated: sum + i *(n-p), all elements in the array after p-1 will be 
            // not less than i, 
            // in other words, A[j] >=i, for any j in the range [p,n-1]. So newSum >= sum + i*(n-p)
            // use the value to prune. 
            int newSum = sum + i * (n - p);

            // similar to newSum, the newDiffSum will the same. 
            // Sum(A[p-1]-A[j]) where j>=0 and j< p-1, denote as addition, 
            // addition = Sum(A[p-1]) - Sum(A[j]) = (i*p - sum)
            int newDiffSum = diffsum + (i * p - sum) * (n - p);
            
            if (newSum > s ||
                newDiffSum > k)
            {
                return 0;
            }

            arr[p] = i;            

            var z = Construct(arr, sum + i, diffsum + i * p - sum, p + 1);
            
            if (z == 1) return 1;
        }

        return 0;
    }
}