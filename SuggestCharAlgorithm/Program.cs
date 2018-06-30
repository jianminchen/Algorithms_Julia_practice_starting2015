using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuggestCharAlgorithm
{
    class Program
    {
        /*
         Suppose you are supplied with a file containing a list of words 
         like ABC, BCD , CAB ( say each word in new line ). now you have 
         to suggest algorithm for this problem - 
         When a user type some character, we have to suggest him next 
         character and basis of suggestion is that the character you are 
         going to suggest should have maximum occurrence at that position 
         among all these words. 

        For example , Let's say words are 
        ABC 
        BCD 
        CBA 
        Now if user types 'A' we have to suggest him 'B' as next character 
        because if you see at second position in all words 'B' is occurring 
        most number of times ( 2 times ). 
        similarly if he types 'AB' then we need to suggest him third character 
        as 'C' as in third index all words have same occurrence but 'C' comes first.

        KEYWORDS:
        maximum count -> list the character
        second criteria: first

        QUESTION:
        A, NEXT B 
        AB -> C 

        words: list of stream 

        A, -> B
        AC, -> B
        ABC 
        BC
        CB
        -> A - Z, 
        counting sort
        index -> count
        countSorting save each char at same position -> O(26) -> countingSort[index][char] 
        char occurrences in all word at the same position at index = i
        char[] maximumCount[index] 
        time complexity: 26 * n
        first occurence char at each index[] -> 
         Time spent: 11:04 PM - 12:29 AM 
        */
        static void Main(string[] args)
        {
            RunTestcases(); 
        }

        static void RunTestcases()
        {
            var stringMaxOccurrence = FindMaximumCountKeepOriginalOrder(new string[] { "ABC", "BCD", "CBA" });
            Debug.Assert(stringMaxOccurrence.ToString().CompareTo("ABC") == 0);
        }

        /// <summary>
        /// Save maximum count char at each index position of words. 
        /// If there are more than one char with the maximum value count, 
        /// then the one with smaller index in words argument will be selected. 
        /// Using less space as possible. 
        /// Julia wrote the code after the mock interview of May 19, 2018
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static char[] FindMaximumCountKeepOriginalOrder(string[] words)
        {
           if(words == null)
           {
                return new char[0];
           }

           var length    = words.Length;
           var maxLength = getMaxLength(words);

           var suggested = new char[maxLength];

           const int SIZE = 256;
           var iterated   = new char[SIZE];
           var firstOccurrence = new int[SIZE];

           for (int index = 0; index < maxLength; index++) // left to right each char
           {
               Array.Clear(iterated, 0, SIZE);
               Array.Clear(firstOccurrence, 0, SIZE); 

               var max = 0;
               var firstIndex = int.MaxValue;
               var maxIndex = 0;               

               // compare all words at the position of the ith char, i with value index
               // if the max value has more than one char, then check the first occurrence
               for (int wordIndex = 0; wordIndex < length; wordIndex++)
               {
                   var currentWord       = words[wordIndex];
                   var currentWordLength = currentWord.Length;

                   var skip = index >= currentWordLength;
                   if (skip)
                   {
                       continue;
                   }

                   var currentChar = (int)currentWord[index];

                   if (iterated[currentChar] == 0)
                   {
                       firstOccurrence[currentChar] = wordIndex;
                   }

                   iterated[currentChar]++;

                   var currentCount = iterated[currentChar];
                   var currentFirst = firstOccurrence[currentChar];

                   if(  currentCount < max || 
                       (currentCount == max && currentFirst > firstIndex))
                   {
                       continue; 
                   }
                   
                   // current one is beating the maximum one
                   firstIndex = currentFirst;
                   maxIndex   = wordIndex;
                   
                   if (currentCount > max)
                   {
                       max = currentCount;                       
                   }
               }

               suggested[index] = words[maxIndex][index];
           }

           return suggested; 
        }

        private static int getMaxLength(string[] words)
        {
            var max = 0; 
            foreach(var word in words)
            {
                var current = word == null ? 0 : word.Length;
                max = current > max ? current : max;
            }

            return max; 
        }
    }
}
