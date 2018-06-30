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
        int q = Convert.ToInt32(tokens_n[1]);

        string[] c_temp = Console.ReadLine().Split(' ');
        int[] c = Array.ConvertAll(c_temp, Int32.Parse);

        for (int a0 = 0; a0 < n - 1; a0++)
        {
            string[] tokens_u = Console.ReadLine().Split(' ');
            int u = Convert.ToInt32(tokens_u[0]);
            int v = Convert.ToInt32(tokens_u[1]);
        }

        for (int a0 = 0; a0 < q; a0++)
        {
            string[] tokens_u = Console.ReadLine().Split(' ');
            int u = Convert.ToInt32(tokens_u[0]);
            int v = Convert.ToInt32(tokens_u[1]);
            int k = Convert.ToInt32(tokens_u[2]);
        }


    }
}