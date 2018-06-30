using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMatching
{
    /// <summary>
    /// Leetcode 10: Regular Expression Matching
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var result = IsMatch("aa", "aa");
        }

        public static bool IsMatch(string text, string pattern)
        {
            if (text == null || pattern == null)
            {
                return false;
            }

            if (pattern.Length == 0)
            {
                return text.Length == 0;
            }

            var test_pattern = pattern[0]; //#
            bool isStar = pattern.Length > 1 && pattern[1] == '*';
            bool firstCharMatching = text.Length > 0 &&
                                    (test_pattern == '.' || text[0] == test_pattern);

            if (pattern.Length > 1 && isStar)
            {
                return IsMatch(text, pattern.Substring(2)) ||
                (firstCharMatching && IsMatch(text.Substring(1), pattern));
            }

            return firstCharMatching && IsMatch(text.Substring(1), pattern.Substring(1));
        }
    }
}