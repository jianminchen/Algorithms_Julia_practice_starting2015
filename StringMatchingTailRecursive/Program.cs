using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringMatchingTailRecursive
{
    class Program
    {
        /// <summary>
        /// source code link: 
        /// https://discuss.leetcode.com/topic/38077/my-concise-20-line-c-solution-using-recursion
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Debug.Assert(RunTestcase() == false); 
        }

        public static bool RunTestcase()
        {
            var search  = "aaaaaaaaaaaaab"; 
            var pattern = "a*a*a*a*a*a*a*a*a*a*c"; 

            return IsMatch(search,pattern, 0, 0); 
        }

        /// <summary>
        /// code review on July 2, 2-17
        /// 3:41 pm 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <param name="sIndex"></param>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        public static bool IsMatch(string s, string p, int sIndex, int pIndex)
        {
            //base case - reached end of pattern
            if (pIndex >= p.Length)
            {
                return sIndex >= s.Length && pIndex >= p.Length;
            }

            if (pIndex + 1 < p.Length && p[pIndex + 1] == '*')
            { //peek ahead for *
                while (sIndex < s.Length && (s[sIndex] == p[pIndex] || p[pIndex] == '.'))
                {
                    // * pattern - 0 times used, just skip .* or any char followed by *
                    if (IsMatch(s, p, sIndex, pIndex + 2))
                    {
                        return true;
                    }

                    // one time, instead of using recursive, using iterative. 
                    // Skip the first char
                    sIndex++; 
                }

                return IsMatch(s, p, sIndex, pIndex + 2);
            }
            else if (sIndex < s.Length && (s[sIndex] == p[pIndex] || p[pIndex] == '.'))
            { //direct 1-to-1 match
                return IsMatch(s, p, sIndex + 1, pIndex + 1);
            }

            return false;
        }
    }
}
