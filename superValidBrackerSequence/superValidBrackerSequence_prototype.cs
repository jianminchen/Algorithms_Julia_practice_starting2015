using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace superValidBrackerSequence
{
    public class Node
    {
        public HashSet<Tuple<string, int>> setPair;
        public HashSet<string> set; 
    }
    class superValidBrackerSequence_prototype
    {
        static void Main(string[] args)
        {
            process(); 

            //IList<Node> data2 = new List<Node>();

            //bracketSequence_II(data2);            
        }

        /*
         * do some research, what is duplicate? 
         * How it is generated? 
         * What is recurrence formula? Can I make it more simple? 
         * 
         * Test the code to make sure that it is correct.
         * 
        */
        private static void process()
        {
            IList<HashSet<string>> data = new List<HashSet<string>>();
            bracketSequence(data);
            int result = calculateKSuper(data, 20, 5); 
        }
        /*
         * 11:09am
         * n = 10, 
         */
        private static int calculateKSuper(IList<HashSet<string>> data, int n, int k)
        {
            int index = (n - 2) / 2;

            int count = 0; 

            foreach(string s in data[index])
            {
                if (isSuperValid(s, k))
                    count++; 
            }
            return count; 
        }

        /*
         * Time complexity: O(n) - n is string length
         */
        private static bool isSuperValid(string s, int k)
        {
            int count = 0; 
            for (int i = 0; i<= s.Length- 2; i++)
                if (s[i] != s[i + 1])
                    count += 1;

            if (count >= k)
                return true;
            else
                return false;
        }
        /*
         * bracket sequence F(n)
         * F(2) - 0- n =  1  (length of string is 2, (), how many? n =1)
         * F(4) - 1- n =  2  "()()", "(())"
         * F(6) - 2- n =  5 
         * F(8) - 3- n = 13
         * F(10)- 4- n = 34
         * F(12)- 5- n = 89 
         * F(14)- 6- n = 233
         * F(16)- 7- n = 610
         * F(18)- 8- n = 1597
         * F(20)- 9- n = 4181
         * F(22)- 10-n = 10944
         * F(24)- 11-n = 28657
         * 
         * F(10) = 3 * F(8) - F(6)
         * 
         * 
         * Almost, 3^n, each time, the string length increases 2, triple the amount. 
         * so, n <=200, the amount of string is around 3^100
         * 
         */
        private static void bracketSequence(IList<HashSet<string>> data)
        {
            HashSet<string> kSuper = new HashSet<string>();
             
            HashSet<string> prev = new HashSet<string>();
            prev.Add("()");
            data.Add(prev);

            HashSet<string> duplicated = new HashSet<string>(); 

            for(int i=0; i<= 12; i++)
            {
                HashSet<string> cur = new HashSet<string>(); 
                foreach(string s in prev)
                {
                    string[] newArr = new string[3]{"(" + s + ")","()" + s, s + "()"};
                    foreach (string s2 in newArr)
                        if (!cur.Contains(s2))
                            cur.Add(s2);
                        else
                            duplicated.Add(s2); 
                }

                data.Add(cur);
                prev = copySet(cur); 
            }
        }

        /*
         * start: 11am
         * work on the code 12:05pm 
         * think about space complexity
         * 
         */
        private static void bracketSequence_II(IList<Node> data)
        {                        
            HashSet<Tuple<string, int>> prevPair = new HashSet<Tuple<string, int>>();
            HashSet<string> prevSet = new HashSet<string>();

            string s0 = "()";
            prevSet.Add(s0);
            prevPair.Add(new Tuple<string, int>(s0, 1));

            Node node    = new Node();
            node.set     = prevSet;
            node.setPair = prevPair; 

            data.Add(node);

            HashSet<string> duplicated = new HashSet<string>(); 

            for (int i = 0; i <= 6; i++)
            {
                HashSet<Tuple<string, int>> curPair = new HashSet<Tuple<string, int>>();
                HashSet<string> curSet = new HashSet<string>(); 

                foreach (Tuple<string, int> tmp2 in prevPair)
                {
                    string s   = tmp2.Item1;
                    int ori    = tmp2.Item2; 

                    string[] newArr = new string[3] { "(" + s + ")", "()" + s, s + "()" };
                    int[]    newInc = new int[3] { 0, 2, 2 };

                    for (int j = 0; j < newArr.Length; j++)
                    {
                        string s2 = newArr[j];
                        int    no = newInc[j];

                        if (!curSet.Contains(s2))
                        {
                            curSet.Add(s2);
                            curPair.Add(new Tuple<string, int>(s2, ori + no));
                        }
                        else
                            duplicated.Add(s2); 
                    }
                }

                Node newNode = new Node(); 
                newNode.set = curSet; 
                newNode.setPair = curPair; 
                data.Add(newNode);

                prevPair = copyPair(curPair);
                prevSet  = copySet(curSet); 
            }
        }

        /*
         * 
         */
        private static HashSet<string> copySet(  HashSet<string> copyFrom)
        {
            HashSet<string> res = new HashSet<string>();
            foreach (string s in copyFrom)
                res.Add(s);

            return res; 
        }

        /*
         * start: 12:33pm 
         * 
         */
        private static HashSet<Tuple<string, int>> copyPair(HashSet<Tuple<string, int>> copyFrom)
        {
            HashSet<Tuple<string, int>> res = new HashSet<Tuple<string, int>>();
            foreach (Tuple<string, int> s in copyFrom)
                res.Add(s);

            return res;
        }
    }
}
