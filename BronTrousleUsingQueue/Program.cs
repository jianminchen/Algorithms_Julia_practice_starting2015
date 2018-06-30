
using System;
using System.Collections.Generic;
using System.IO;
using Number = System.Numerics.BigInteger;
using System.Linq;

class Solution {
    static void Main(String[] args) {
      var t = Number.Parse(Console.ReadLine());
            for (Number i = 0; i < t; i++)
            {
                var l = Console.ReadLine().Split(' ');
                var n = Number.Parse(l[0]);
                var k = Number.Parse(l[1]);
                var b = Number.Parse(l[2]);

                var r = Solve(n, k, b );
                if (r == null) Console.WriteLine(-1);
                else Console.WriteLine(string.Join(" ",r));
            }            
        }

        struct Cmd { public Number n,k,b; }
        static Queue<Cmd> Q;

        public static List<Number> Solve(Number n, Number k, Number b)
        {
            Q = new Queue<Cmd>();
            Q.Enqueue(new Cmd { n = n, k = k, b = b });

            var solution = new List<Number>();
            while (Q.Count > 0)
            {
                var cmd = Q.Dequeue();
                var r = SubSolve(cmd.n, cmd.k, cmd.b);
                if (r == null) return null;
                solution.AddRange(r);
            }
            return solution;
        }

        public static List<Number> SubSolve(Number n, Number k, Number b)
        {
            k = k < n ? k : n;
            if (n == 0 || k == 0 || b == 0) return new List<Number>();
            if (k * b < n) return null;
            var r = new List<Number>();

            var neededForOtherBoxes = LowerBound(b - 1);

            var availableThisBox = n - neededForOtherBoxes;
            if (availableThisBox < b) return null;

            var takeFromTop = availableThisBox / k;

            if (takeFromTop > k || availableThisBox <= 0) return null;

            if (takeFromTop > 0)
            {
                var newB = b - takeFromTop;
                var newN = n - UpperBound(k, takeFromTop);
                var newK = k - takeFromTop;
                Q.Enqueue(new Cmd { n = newN, k = newK, b = newB });
                
                for (int i = 0; i < takeFromTop; i++)
                {
                    r.Add(k - i);
                }
                return r;
            }
            
            Q.Enqueue(new Cmd { n = n - availableThisBox, k = availableThisBox - 1, b = b - 1 });
            r.Add(availableThisBox);
            return r;
        }

        public static Number LowerBound(Number b)
        {
            Number a1 = 1;
            Number a2 = b;
            var r = (a1 + a2) * b / 2;
            return r;
        }

        public static Number UpperBound(Number k, Number b)
        {
            var a1 = k - b + 1;
            var r = (a1 + k) * b / 2;
            return r;
        }
}
 