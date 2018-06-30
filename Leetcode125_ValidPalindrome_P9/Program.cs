using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode125_IsPalindrome
{
    class Program
    {
        /*
         *  Leetcode 125: is palindrome 
         *  
         *  blog to read: 
         *  http://blog.csdn.net/nomasp/article/details/50623165
         */
        static void Main(string[] args)
        {
            bool test = isPalindrome("Aa");
            bool test2 = isPalindrome("Ab%Ba");
            bool test3 = isPalindrome("B%1*1b");
        }

        /*
         * August 19, 2016
         * 
         * define two functions: 
         * isAlnum()
         * toUpperCase()
         * 
         * highlights of first writing: 
         * 1. line 47 - work on positive case first, avoid too long discussion - let line 42 take care more things.
         * 2. static analysis: Runtime exception check, index-out-of-range
         * 3. line 51 - alternative: check Math.abs(c1-c2) = 26 or 0
         */
        public static bool isPalindrome(string s)
        {
            if (s == null || s.Length == 0)
                return true;

            int left = 0;
            int right = s.Length - 1;

            /*
             * practice goal: 
             * 1. No nested if statement 
             * 2. No else statement 
             * 3. let return case go first - line 61
             * 4. 3nd and 4rd if statements are the second if statement's else case, but hide the relationship. 
             *    logic thinking: if a case hits in second if and then one of if (3rd or 4th), then counting of left or right 
             *    will be twice. <- definitely wrong
             * 5. Also, follow guard clauses first idea 
             * http://www.refactoring.com/catalog/replaceNestedConditionalWithGuardClauses.html
             *   based on practice 8, put all guard clauses together, before the clause. 
             *   
             */
            while (left < right && left >= 0 && right >= 0)  // just extra checking... 
            {
                char c1 = s[left];
                char c2 = s[right];

                bool[] arr = new bool[2] { isAlnum(c1), isAlnum(c2) };

                bool isComparable = arr[0] && arr[1];           // left and right are comparable, cannot skip. 
                bool isSame = toUpper(c1) == toUpper(c2); // left and right are same - a=A

                if (!isComparable && !arr[0])
                    left++;

                if (!isComparable && !arr[1])
                    right--; 

                if (isComparable && !isSame)
                    return false;

                if (isComparable && isSame)
                {
                    left++;
                    right--;
                }                
            }

            return true;
        }

        /*
         * a-z
         * A-Z
         * 0-9
         */
        private static bool isAlnum(char c)
        {
            const int SIZE = 26;
            int[] arr = new int[] { c - 'a', c - 'A', c - '0' };

            // is A-Z or a-z or 0 -9
            if ((arr[0] >= 0 && arr[0] < SIZE) ||
               (arr[1] >= 0 && arr[1] < SIZE) ||
               (arr[2] >= 0 && arr[2] <= 9))
                return true;

            return false;
        }

        /*
         * 
         */
        private static char toUpper(char c)
        {
            int no = c - 'a';

            if (no >= 0 && no < 26)
                return (char)('A' + no);
            else
                return c;
        }
    }
}