using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    static bool[, ,] w;
    static void Main(String[] args)
    {
        int _tc_ = int.Parse(Console.ReadLine());
        while (_tc_-- > 0)
        {
            Array_Construction();
        }
    }

    static int n, s, k;
    static void Array_Construction()
    {
        var tmp = Console.ReadLine().Split(' ');
        n = int.Parse(tmp[0]);
        s = int.Parse(tmp[1]);
        k = int.Parse(tmp[2]);

        w = new bool[n, s + 1, k + 1];

        int[] A = new int[n];
        if (construct(A, 0, 0, 0) == 1)
            Console.WriteLine(string.Join(" ", A));
        else
            Console.WriteLine(-1);
    }

    static int construct(int[] A, int sum, int diffsum, int p)
    {
        if (p == n)
        {
            if (sum == s && diffsum == k) return 1;
            return 0;
        }

        if (w[p, sum, diffsum]) return -1; else w[p, sum, diffsum] = true;

        int i = 0;
        if (p != 0) i = A[p - 1];
        for (; i <= s; i++)
        {
            if (sum + i * (n - p) > s || diffsum + (i * p - sum) * (n - p) > k) return 0;

            A[p] = i;
            var z = construct(A, sum + i, diffsum + i * p - sum, p + 1);
            //if (z == -1) return 0;
            if (z == 1) return 1;
        }
        return 0;
    }
}