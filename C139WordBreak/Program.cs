using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _139WordBreak
{
    class Program
    {
        static void Main(string[] args)
        {
            // test case: 
            // s = "leetcode",
            // dict = ["leet", "code"].

            string s = "abcd";
            string[] dict = new string[]{
                "a", "bc","d"
            };

            bool result = wordBreak(s, new HashSet<string>(dict.ToList()));

        }

        /*
         * Leetcode 139 Word Break 
         * 
         * 
         * source code from:
         * http://bangbingsyb.blogspot.ca/2014/11/leetcode-word-break-i-ii.html
         * 
         * Try to analyze the problem. 
         * 
         * Use DFS to do search, but to avoid duplication in calculation, 
         * use an array dp to save S[0:i] - break or not
           dp[i]：S[0:i-1] breakable or not。
           for example, dp[1] - s[0] is breakable or not.
         * 
            dp[0] = true
            dp[i] = true if and only if:
            1. there is one i-1 >= k >= 0，s[k:i-1] is in the dict。
            2. s[0: k-1] is breakable，so dp[k] = true。
         * 
         *  This practice is to work on test case:
         *  "abcd", 
         *  string[] dict = new string[]{
                "a", "bc","d"
            };
         * 
         *  We have to prepare an array dp[] to calculate 
         *  dp[i] value from 0 to 4. 
         *  And then, we can determine if dp[4] is true not
         *  dp[0] = true; 
         *  dp[1] = true, "a" can be constructed by dictionary
         *  dp[2] = false, "ab" cannot be constructed by dictionary
         *  dp[3] = true, "abc" can be constructed by dictionary "a"+"bc"
         *  dp[4] = true, "abcd" can be constructed by dictionary
         *  
         *     
         * 
         */
        public static bool wordBreak(
            string s,
            ISet<string> wordDict)
        {
            if (s == null || s.Length == 0)
                return false;

            int len = s.Length;

            bool[] cache = new bool[len + 1];
            cache[0] = true; // "" empty string 
           
            for (int i = 0; i < len; i++)
            {
                // "ab", "abc"
                // "a"
                // right string start position point index 
                for (int pos = i; pos >= 0; pos--)
                {
                    string left  = pos == 0 ? "" : s.Substring(0, pos);
                    //string right = s.Substring(pos, len - left.Length);   // bug001 - not len, should be i
                    string right = s.Substring(pos, i + 1 - left.Length);

                    if(cache[pos] && wordDict.Contains(right))
                    {
                        cache[i + 1] = true;
                        break;
                    }
                }
                
            }

            return cache[len]; 
        }

        
    }
}