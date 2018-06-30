    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Leetcode125_IsPalindrome
    {
        class ValidPalindrome
        {
            /*
             *  Leetcode 125: Valid Palindrome
             *  https://leetcode.com/problems/valid-palindrome/?tab=Description
             */
            static void Main(string[] args)
            {
                RunTestcases();             
            }

            public static void RunTestcases()
            {
                Debug.Assert(IsPalindrome("Aa"));
                Debug.Assert(IsPalindrome("Ab%Ba"));
                Debug.Assert(IsPalindrome("B%1*1b"));
                Debug.Assert(!IsPalindrome("B%a*1b"));
            }

            /* 
             * Given a string, determine if it is a palindrom, considering
             * only alphanumeric characters and ignoring cases
             * Time complexity: 
             * O(N)
             *  
             * empty string is valid palindrome 
             */
            public static bool IsPalindrome(string rawString)
            {
                if (rawString == null || rawString.Length == 0)
                {
                    return true;
                }

                string stringWithCases = fiterOutNonAlphanumbericaCharacters(rawString);

                // two pointers technique
                int start = 0;
                int end = stringWithCases.Length - 1;

                while (start < end)
                {
                    char left  = toUpper(stringWithCases[start]);
                    char right = toUpper(stringWithCases[end]);

                    if (left - right != 0)
                    {
                        return false;
                    }

                    start++;
                    end--;
                }

                return true;
            }

            /*
             * check if the char is alphabetic or digit
             * a-z
             * A-Z
             * 0-9
             */
            private static bool isAlphabeticAndDigit(char anyChar)
            {
                if (isCapitalCaseAlphabetic(anyChar) ||
                   isLowerCaseAlphabetic(anyChar)    ||
                   isDigit(anyChar))
                {
                    return true;
                }

                return false;
            }

            private static bool isCapitalCaseAlphabetic(char anyChar)
            {            
                var number = anyChar - 'A';
                return number >= 0 && number < 26;
            }

            private static bool isLowerCaseAlphabetic(char anyChar)
            {            
                var number = anyChar - 'a';
                return number >= 0 && number < 26;
            }

            private static bool isDigit(char anyChar)
            {            
                var number = anyChar - '0';
                return number >= 0 && number <= 9;
            }

            /*
             * assuming input char is alphabetic number, 
             * output the capitical case char
             */
            private static char toUpper(char alphabeticChar)
            {
                int number = alphabeticChar - 'a';

                if (number >= 0 && number < 26)
                {
                    return (char)('A' + number);
                }            
            
                return alphabeticChar;            
            }
        
            /*
             * Filter out non alphanumeric characters
             * and keep cases
             */
            private static string fiterOutNonAlphanumbericaCharacters(string rawString)
            {
                return string.Concat( rawString.Where(c => isAlphabeticAndDigit(c) == true).ToArray());            
            }
        }
    }