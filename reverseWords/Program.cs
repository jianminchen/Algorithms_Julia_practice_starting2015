using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reverseWord
{
    /// <summary>
    /// reverse words in the string 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var reversed = reverseWords(new char[] { 'f', 'u', 'n', ' ', 'i', 's', ' ', 'c', 'o', 'd', 'i', 'n', 'g' });

            Console.WriteLine(reversed);
        }

        // "", null, " ab"
        static char[] reverseWords(char[] arr)
        {
            // your code goes here
            if (arr == null || arr.Length == 0)
            {
                return new char[0]; // 
            }

            // assume that the string is not empty
            int length = arr.Length;
            Reverse(arr, 0, length - 1); // "a ", "ba "

            // find a word, and then reverse the word 
            int start = -1;

            for (int i = 0; i < length; i++)
            {
                var visit = arr[i]; //a  , b, a
                bool isAlphabetic = visit != ' '; // true, true
                bool isStart = isAlphabetic && (i == 0 || arr[i - 1] == ' '); // start, true
                bool isLastInWord = isAlphabetic && (i == (length - 1) || arr[i + 1] == ' '); // true, false

                if (isStart)
                {
                    start = i; // 0
                }

                if (isLastInWord)
                {
                    Reverse(arr, start, i); // start, (""ba ", 0, 1)                     
                }
            }

            return arr;
        }

        public static void Reverse(char[] arr, int start, int end)
        {
            for (int i = start; i <= end && i <= (end - start) / 2; i++)
            {
                var tmp = arr[i];
                var swapNode = start + end - i;

                arr[i] = arr[swapNode];
                arr[swapNode] = tmp;
            }
        }
    }
}