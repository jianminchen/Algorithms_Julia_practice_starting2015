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
        int m = Convert.ToInt32(tokens_n[1]);

        var list = new List<int[]>(); 
        for (int a0 = 0; a0 < m; a0++)
        {
            string[] tokens_a = Console.ReadLine().Split(' ');
            int a = Convert.ToInt32(tokens_a[0]);
            int b = Convert.ToInt32(tokens_a[1]);
            int w = Convert.ToInt32(tokens_a[2]);
            // Write Your Code Here

            list.Add(new int[]{a,b,w}); 
        }

        var result = getMinimum(n,m, list);
    }

    public static int[] getMinimum(int n, int m, List<int[]> list)
    {
        int sum = 0;
        int max = Int32.MinValue; 
        for(int i = 0; i < m; i ++)
        {
            var visit = list[i][2];
            max = visit > max ? visit : max;
            sum += visit; 
        }

        return new int[] { sum - max, Math.Max(m - n, 1) }; 
    }
}