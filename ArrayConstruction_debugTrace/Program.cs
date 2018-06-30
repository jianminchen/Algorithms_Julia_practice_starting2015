using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    static bool[, ,] w;
    static void Main(String[] args)
    {
        //process(); 
        testing();
    }

    private static void testing()
    {
        n = 50;
        s = 20;
        k = 1860;

        w = new bool[n, s + 1, k + 1];

        int[] A = new int[n];
        IList<string> helper = new List<string>();

        if (construct(A, 0, 0, 0, helper) == 1)
            Console.WriteLine(string.Join(" ", A));
        else
            Console.WriteLine(-1);

        string s2 = String.Join("\r\n", helper.ToArray());
    }

    private static void process()
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
        IList<string> helper = new List<string>();

        if (construct(A, 0, 0, 0, helper) == 1)
            Console.WriteLine(string.Join(" ", A));
        else
            Console.WriteLine(-1);
    }

    /*
     * Nov. 14, 2016
     * 
     * Julia tried to figure out this dynamic programming formula
     * 
     * 1. change the code style to fit my reading style.
     * 
     */
    static int construct(
        int[] A,
        int sum,
        int diffsum,
        int p,
        IList<string> trace)
    {
        trace.Add("");

        if (p == n)
        {
            if (sum == s && diffsum == k)
            {
                trace.Add("sum == s && diffsum == k - find the solution! ");
                return 1;
            }

            trace.Add("p==n return 0 whereas p=" + p.ToString() + " n=" + n.ToString());
            trace.Add(" ");
            return 0;
        }

        if (w[p, sum, diffsum])
        {
            trace.Add("w[p, sum, diffsum] is true - w[" + p.ToString() + "," + sum.ToString() + "," + diffsum.ToString() + "] - return -1");
            return -1;
        }
        else
        {
            trace.Add("w[p, sum, diffsum] is not true w[" + p.ToString() + "," + sum.ToString() + "," + diffsum.ToString() + "] ");

            w[p, sum, diffsum] = true;
        }

        int i = 0;

        if (p != 0)
        {
            i = A[p - 1];
            trace.Add("i=A[p-1], p=" + p.ToString() + ", i=" + A[p - 1].ToString() + ";");
        }

        int startDebug = i;

        trace.Add("For loop (A - E): for(; i<=s;i++) i=" + i.ToString() + " s=" + s.ToString());
        trace.Add(" ");
        for (; i <= s; i++)
        {
            trace.Add("n-p=? " + (n - p).ToString());
            int newSum = sum + i * (n - p);
            int newDiffSum = diffsum + (i * p - sum) * (n - p);
            trace.Add("iterate on i =" + i.ToString());
            trace.Add("A: review sum, diffsum " + sum.ToString() + " " + diffsum.ToString());
            trace.Add("B: newSum, newDiffSum " + newSum.ToString() + " " + newDiffSum.ToString());
            trace.Add("C: i, n, p i=" + i.ToString() + " n=" + n.ToString() + " p=" + p.ToString());

            if (newSum > s ||
                newDiffSum > k)
            {
                trace.Add("D-Exception: newSum, newDiffSum are bigger than s, k " + newSum.ToString() + " " + newDiffSum.ToString() + " exit for loop.");
                trace.Add("return 0");
                trace.Add(" ");
                return 0;
            }

            A[p] = i;
            trace.Add("D-set A[p]=i, A[p]=" + i.ToString() + " where p=" + p.ToString() + " and i=" + i.ToString());

            // start to prepare debug text ... 
            string helper = "D-Normal: loop start to end: From " + startDebug.ToString();
            helper += " to " + s.ToString() + " current i = ";
            helper += i.ToString();
            helper += "  and arguments:";

            helper += (sum + i).ToString() + " ";
            helper += (diffsum + i * p - sum).ToString();
            helper += " " + (p + 1).ToString();
            trace.Add(helper);

            helper = "E: Array A is [" + string.Join(",", A) + "]";
            trace.Add(helper);
            // the end 

            trace.Add("F: recursive call construct - arguments: (A," + (sum + i).ToString() + ","
                + (diffsum + i * p - sum).ToString() + "," + (p + 1).ToString() + ")");

            var z = construct(A, sum + i, diffsum + i * p - sum, p + 1, trace);

            //if (z == -1) return 0;
            if (z == 1) return 1;
        }

        return 0;
    }
}