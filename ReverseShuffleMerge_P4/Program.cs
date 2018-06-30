using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseShuffleMerge_P4
{
    class Program
    {
        static void Main(string[] args)
        {
            runTestCases(); 
            //Console.WriteLine(reverseShuffleMerge(Console.ReadLine()));
        }

        public static void runTestCases()
        {
            string[] testCases = new string[4] { "abcacb", "aaccbb", "cacbab", "baabcc" };

            List<string> outputStrs = new List<string>();
            foreach (string s in testCases)
            {
                string s2 = reverseShuffleMerge(s);

                outputStrs.Add(s2);
            }

            string[] result = outputStrs.ToArray();
        }

        /*
         * practice - write code ready to be compiled and executed. 
         * 
         */
        public static string reverseShuffleMerge(string input)
        {
            if (input == null || input.Length == 0)
                return null;

            int TOTALCHARS = 26; 
            int len = input.Length; 

            int[] moreToAdd = new int[TOTALCHARS]; 
            int[] moreToSkip = new int[TOTALCHARS]; 

            foreach(char c in input)
            {               
                moreToAdd[getIndex(c)]++; 
            }

            for(int i=0; i< TOTALCHARS; i++)
            {
                moreToAdd[i] = moreToAdd[i] / 2;
                //moreToSkip[i] = moreToAdd[i] / 2;   // bug - 9:49pm - May 26/2016
                moreToSkip[i] = moreToAdd[i];
            }

            Stack<char> stack = new Stack<char>(); 

            for(int i = len - 1; i >= 0; i--)  // reverse order 
            {
                char runner = input[i];
                int index = getIndex(runner); 

                // stack is not empty, and need to add one more char - runner, 
                // top of stack - char > runner
                // top of stack - char - can be skipped if backtracked. 
                while(
                    stack.Count > 0     &&
                    moreToAdd[index] > 0 &&
                    ((char)stack.Peek() > runner)  &&
                    moreToSkip[getIndex((char)stack.Peek())] > 0
                    )
                {
                    char backTracked = (char)stack.Pop(); 
                    int  oldIndex = getIndex(backTracked);

                    moreToAdd[oldIndex]++;
                    moreToSkip[oldIndex]--; 
                }

                if(moreToAdd[index] > 0)
                {
                    stack.Push(runner);
                    moreToAdd[index]--; 
                }
                else
                {
                    moreToSkip[index]--; 
                }
            }

            char[] arr = stack.ToArray();
            Array.Reverse(arr);

            return new string(arr);
        }

        private static int getIndex(char c)
        {
            return c - 'a'; 
        }
    }
}
