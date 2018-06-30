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
            public static bool IsPalindrome_obsolete(string rawString)
            {
                if (rawString == null || rawString.Length == 0)
                {
                    return true;
                }

                // two pointers techniques 
                int start = 0;
                int end = rawString.Length - 1;
            
                while (start < end )   
                {               
                    if (!isAlphabeticAndDigit(rawString[start]))
                    {
                        start++;
                        continue;
                    }

                    if (!isAlphabeticAndDigit(rawString[end]))
                    {
                        end--;
                        continue;
                    }                                  

                    char left  = rawString[start];
                    char right = rawString[end];

                    if (toUpper(left) != toUpper(right))
                    {
                        return false;
                    }
                
                    start++;
                    end--;                
                }

                return true;
            }

            public static bool IsPalindrome_200Success(string s)
            {
                for (int start = 0, end = s.Length - 1; ; start++, end--)
                {
                    while (start < end && !isAlphabeticAndDigit(s[start]))
                    {
                        start++;
                    }
                    while (start < end && !isAlphabeticAndDigit(s[end]))
                    {
                        end--;
                    }

                    if (start >= end)
                    {
                        return true;
                    }

                    if (toUpper(s[start]) != toUpper(s[end]))
                    {
                        return false;
                    }
                }
            }

            private static bool IsPalindrome(string s)
            {

                if (string.IsNullOrWhiteSpace(s)) return true;
                if (s.Length == 1) return true;

                var left = 0;
                var right = s.Length - 1;

                while (left < right)
                {
                    if (!char.IsLetterOrDigit(s[left]))
                    {
                        left++;
                        continue;
                    }

                    if (!char.IsLetterOrDigit(s[right]))
                    {
                        right--;
                        continue;
                    }

                    var lhs = char.ToLower(s[left++]);
                    var rhs = char.ToLower(s[right--]);
                    if (lhs != rhs)
                    {
                        return false;
                    }
                }

                return true;
            }

            private static bool isPalindrome_112ms(string s)
            {
                if (string.IsNullOrWhiteSpace(s)) return true;
                if (s.Length == 1) return true;

                var left = 0;
                var right = s.Length - 1;

                while (left < right)
                {
                    while (left <= right && !char.IsLetterOrDigit(s[left]))
                    {
                        left++;
                    }

                    while (right >= left && !char.IsLetterOrDigit(s[right]))
                    {
                        right--;
                    }

                    if (left >= right)
                    {
                        return true;
                    }

                    var lhs = char.ToLower(s[left++]);
                    var rhs = char.ToLower(s[right--]);
                    if (lhs != rhs)
                    {
                        return false;
                    }
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
                   isLowerCaseAlphabetic(anyChar) ||
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
                return string.Concat(rawString.Where(c => isAlphabeticAndDigit(c) == true).ToArray());
            }
        }
    }