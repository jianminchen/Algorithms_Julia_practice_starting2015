using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    static void Main(String[] args)
    {
        string[] tokens_n = Console.ReadLine().Split(' ');
        int n = Convert.ToInt32(tokens_n[0]);
        int queries = Convert.ToInt32(tokens_n[1]);

        var data = Console.ReadLine().Split(' ');
        var numbers = Array.ConvertAll(data, Int32.Parse);

        for (int index = 0; index < queries; index++)
        {
            var tokens_x = Console.ReadLine().Split(' ');
            int x = Convert.ToInt32(tokens_x[0]);
            int y = Convert.ToInt32(tokens_x[1]);
            // Write Your Code Here

            Console.WriteLine(GetSameOccurrence(x, y, numbers)); 
        }
    }

    /// <summary>
    /// code review on July 22, 2017
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="numbers"></param>
    /// <returns></returns>
    public static int GetSameOccurrence(int x, int y, int[] numbers)
    {

    }
}
