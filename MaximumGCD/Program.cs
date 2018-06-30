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
        var firstOne = new Dictionary<int, int>[lengthA];
        var secondOne = new Dictionary<int, int>[lengthB];

        for (int i = 0; i < lengthA; i++)
        {
            firstOne[i] = primeFactors(A[i]);
        }

        var maximumFirst = getMaximum(firstOne);

        for (int i = 0; i < lengthB; i++)
        {
            secondOne[i] = primeFactorsWithLimit(B[i], maximumFirst);
        }
        
        var maximumSecond = getMaximum(secondOne);

        var minimumValue = getMinimumValue(maximumFirst, maximumSecond);

        // go over each pair to find the maximum gcd
        var maximumGcd = int.MinValue;
        var sumValue = 0;
        for (int i = 0; i < lengthA; i++)
        {
            var first = A[i];
            var maximum = getMaximumBasedOnTheOther(firstOne[i], maximumSecond); 

            // pruning to avoid timeout
            if (first < maximumGcd || first < minimumValue ||
                maximum < maximumGcd || maximum < minimumValue)
            {
                continue;
            }

            for (int j = 0; j < lengthB; j++)
            {
                var second = B[j];                

                if (second < maximumGcd || second < minimumValue)
                {
                    continue;
                }

                var gcd = getCommonDivisor(firstOne[i], secondOne[j]);
                var currentSum = first + second;
                if (gcd > maximumGcd)
                {
                    maximumGcd = gcd;
                    sumValue = currentSum;
                }
                else if (gcd == maximumGcd && currentSum > sumValue)
                {
                    sumValue = currentSum;
                }
            }
        }

        return sumValue;
    }

    /// <summary>
    /// try to solve one more timeout test case
    /// </summary>
    /// <param name="one"></param>
    /// <param name="theOther"></param>
    /// <returns></returns>
    private static int getMaximumBasedOnTheOther(Dictionary<int, int> one, Dictionary<int, int> theOther)
    {
        int maximum = 1; 
        foreach(var key in one.Keys)
        {
            var value = one[key]; 
            if(theOther.ContainsKey(key))
            {
                maximum *= (int)Math.Pow(key, Math.Min(value, theOther[key])); 
            }
        }

        return maximum; 
    }

    private static Dictionary<int, int> getMaximum(Dictionary<int, int>[] firstOne)
    {
        var maximum = new Dictionary<int, int>(); 

        for(int i = 0 ; i < firstOne.Length; i++)
        {
            var item = firstOne[i]; 

            foreach(var key in item.Keys)
            {
                var value = item[key]; 
                if(!maximum.ContainsKey(key))
                {
                    maximum[key] = value; 
                }
                else if(maximum[key] < value)
                {
                    maximum[key] = value; 
                }
            }
        }

        return maximum; 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    private static int getMinimumValue(Dictionary<int, int> first, Dictionary<int, int> second)
    {
        int maxValue = 1; 

        foreach(var key in first.Keys)
        {
            if(second.ContainsKey(key))
            {
                var current = (int)Math.Pow(key, Math.Min(first[key], second[key])); 
                maxValue = (current > maxValue)? current : maxValue; 
            }
        }

        return maxValue; 
    }
    /// <summary>
    /// code review on July 22, 2017
    /// </summary>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <returns></returns>
    private static int getCommonDivisor(Dictionary<int, int> first, Dictionary<int, int> second)
    {
        var gcd = 1;
        foreach (var key in first.Keys)
        {
            var value = first[key];
            if (second.ContainsKey(key))
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
    public static Dictionary<int, int> primeFactors(int n)
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

        if (index > 0)
        {
            primeNumberFactors.Add(2, index);
        }

        // n must be odd at this point.  So we can
        // skip one element (Note i = i +2)
        for (int i = 3; i <= Math.Sqrt(n); i += 2)
        {
            // While i divides n, print i and divide n
            int count = 0;
            while (numberLeft % i == 0)
            {
                numberLeft /= i;
                count++;
            }

            if (count > 0)
            {
                primeNumberFactors.Add(i, count);
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

    public static Dictionary<int, int> primeFactorsWithLimit(int n, Dictionary<int,int> limit)
    {
        var primeNumberFactors = new Dictionary<int, int>();

        var numberLeft = n;
        // Print the number of 2s that divide n
        if (limit.ContainsKey(2))
        {
            int index = 0;
            while (numberLeft % 2 == 0)
            {
                numberLeft /= 2;
                index++;
            }

            if (index > 0)
            {
                primeNumberFactors.Add(2, index);
            }
        }

        // n must be odd at this point.  So we can
        // skip one element (Note i = i +2)
        for (int i = 3; i <= Math.Sqrt(n); i += 2)
        {
            if(!limit.ContainsKey(i))
            {
                continue; 
            }

            // While i divides n, print i and divide n
            int count = 0;
            while (numberLeft % i == 0 && count <= limit[i])
            {
                numberLeft /= i;
                count++;
            }

            if (count > 0)
            {
                primeNumberFactors.Add(i, count);
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
}