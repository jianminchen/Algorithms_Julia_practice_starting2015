using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{   
    /// <summary>
    /// problem statement:
    /// https://www.hackerrank.com/contests/w34/challenges/maximum-gcd-and-sum
    /// time complexity: 
    /// 
    /// </summary>
    /// <param name="args"></param>
    static void Main(String[] args)
    {
        Process(); 
       // var factors = primeFactors(315); 
    }

    public static void Process()
    {
        int n = Convert.ToInt32(Console.ReadLine());

        var A_temp = Console.ReadLine().Split(' ');
        var A = Array.ConvertAll(A_temp, Int32.Parse);

        var B_temp = Console.ReadLine().Split(' ');
        var B = Array.ConvertAll(B_temp, Int32.Parse);
        int res = maximumGcdAndSum(A, B);

        Console.WriteLine(res);
    }

    /// <summary>
    /// code review on July 22, 2017
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    static int maximumGcdAndSum(int[] A, int[] B)
    {
        var lengthA = A.Length; 
        var lengthB = B.Length;

        // preprocessing two array once, make each number into prime number factors
        var firstOne  = new Dictionary<int, int>[lengthA];
        var secondOne = new Dictionary<int, int>[lengthB];
        var size = 5 * 100000 + 1;
        var memo = new Dictionary<int, int>[size]; 
        var memoLastPrime = new int[size]; 

        for (int i = 0; i < lengthA; i++ )
        {
            firstOne[i] = primeFactorsWithMemo(A[i], memo, memoLastPrime); 
        }

        for (int i = 0; i < lengthB; i++)
        {
            secondOne[i] = primeFactorsWithMemo(B[i], memo, memoLastPrime);
        }

        // go over each pair to find the maximum gcd
        var maximumGcd = int.MinValue;
        var sumValue = 0; 
        for (int i = 0; i < lengthA; i++ )
        {
            var first = A[i]; 

            // pruning to avoid timeout
            if(first < maximumGcd)
            {
                continue; 
            }

            for(int j = 0; j < lengthB; j++)
            {
                var second = B[j];

                if(second < maximumGcd)
                {
                    continue; 
                }

                var gcd = getCommonDivisor(firstOne[i], secondOne[j]);
                var currentSum = first + second; 
                if(gcd > maximumGcd)
                {
                    maximumGcd = gcd;
                    sumValue   = currentSum;
                }
                else if(gcd == maximumGcd && currentSum > sumValue)
                {
                    sumValue = currentSum; 
                }
            }
        }

        return sumValue; 
    }

    /// <summary>
    /// code review on July 22, 2017
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    private static int getCommonDivisor(
        Dictionary<int, int> first, Dictionary<int, int> second)
    {
        var gcd = 1; 
        foreach(var key in first.Keys)
        {
            var value = first[key]; 
            if(second.ContainsKey(key))
            {
                value = Math.Min(value, second[key]);
                gcd *= (int)Math.Pow(key, value); 
            }
        }

        return gcd; 
    }
    /// <summary>
    /// source code is from the blog:
    /// http://www.geeksforgeeks.org/print-all-prime-factors-of-a-given-number/
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static Dictionary<int, int> primeFactorsWithMemo(
        int n, Dictionary<int, int>[] memo, int[] memoLastPrime)
    {
        var primeNumberFactors = new Dictionary<int, int>(); 

        var numberLeft = n; 

        // Print the number of 2s that divide n
        int index = 0; 
        while (numberLeft % 2 == 0)
        {            
            numberLeft /= 2;
            index++; 
        }

        if(index > 0)
        {
            primeNumberFactors.Add(2, index); 
        }

        // try to solve timeout issue using memo
        var oddNumberFactors = getOddNumberPrimeFactors(n, ref numberLeft, memo, memoLastPrime);
        if (oddNumberFactors.Count > 0)
        {
            foreach (var pair in oddNumberFactors)
            {
                primeNumberFactors.Add(pair.Key, pair.Value);
            }
        }
        

        // This condition is to handle the case when
        // n is a prime number greater than 2
        if (numberLeft > 2)
        {
            primeNumberFactors.Add(numberLeft, 1); 
        }

        return primeNumberFactors; 
    }
    
    /// <summary>
    /// try to use memoization to save time
    /// need to return numberLeft
    /// </summary>
    /// <param name="number"></param>
    /// <param name="numberLeft"></param>
    /// <param name="memo"></param>
    /// <returns></returns>
    private static Dictionary<int, int> getOddNumberPrimeFactors(
        int     number, 
        ref int numberLeft, 
        Dictionary<int, int>[] memo, 
        int[] memoLastPrime)
    {
        var memoKey = numberLeft;
        if (memo[memoKey] != null && memo[memoKey].Count > 0)
        {
            numberLeft = memoLastPrime[memoKey];
            return memo[memoKey]; 
        }

        var primeNumberFactors = new Dictionary<int, int>();         
       
        // n must be odd at this point.  So we can
        // skip one element (Note i = i +2)
        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            // While i divides n, print i and divide n
            int count = 0; 
            while (numberLeft % i == 0)
            {                
                numberLeft /= i;
                count++; 
            }

            if(count > 0)
            {
                primeNumberFactors.Add(i, count); 
            }
        }

        memo[memoKey] = primeNumberFactors;
        memoLastPrime[memoKey] = numberLeft; 

        return primeNumberFactors; 
    }
}
