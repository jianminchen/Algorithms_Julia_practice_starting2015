using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMatching
{
    /// <summary>
    /// Leetcode 10: Regular Expression Matching
    /// Pass all test cases using memo - July 2, 2017 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var result = IsMatch("aa", "aa"); 
        }

        public static Dictionary<string, bool> memo = new Dictionary<string, bool>();

        public static bool IsMatch(string text, string pattern)
        {
            if( text == null || pattern == null)
            {
                return false; 
            }

            var key = text + " " + pattern;
            if(memo.ContainsKey(key))
            {
                return memo[key];
            }

            if (pattern.Length == 1 && text.Length == 0)
            {
                return false; // "", "a"  return false
            }
  
            if (pattern.Length == 1 && text.Length == 1 && pattern[0] == '.')
            {
                return true; //  # "a" "." return true
            }
  
            // test case: "" "a*"  
            if (pattern.Length <= 1) 
            {
                return text == pattern; // # "a" "a" pattern .
            }  

            // test case: "","a*"

            var test_pattern = pattern[0]; //#
            bool isStar = pattern[1] == '*';
            bool firstCharMatching = text.Length > 0 && 
                                    (test_pattern == '.' || text[0] == test_pattern);
            bool match = false;

            if (isStar)
            {
                match = IsMatch(text, pattern.Substring(2)); // repeat 0 times

                if (firstCharMatching)
                {
                    // repeat at least one time
                    match |=  IsMatch(text.Substring(1), pattern); 
                }
            }
            else if (firstCharMatching)
            {
                match |= IsMatch(text.Substring(1), pattern.Substring(1));
            }

            memo[key] = match; 

            return match;
        }
    }
}
