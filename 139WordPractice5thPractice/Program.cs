using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _139WordPractice5thPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "abcd";
            string[] dict = new string[] { "a", "bc", "d" };

            bool result = wordBreak(s, new HashSet<string>(dict.ToList())); 
        }

        /*
         * Leetcode 139 - word break 
         * 
         * Work on test case: "abc"
         */
        public static bool wordBreak(
            string s, 
            ISet<string>  wordDict
            )
        {
            if (s == null || s.Length == 0)
                return false;

            int len = s.Length;

            bool[] cache = new bool[len + 1];
            cache[0] = true;  // "" - empty string, it is constructable 

            for (int i = 0; i < len; i++)
            {
                for (int pos = i; pos >= 0; pos--)  // new word starting position 
                {
                    string existingWord = pos == 0 ? "" : s.Substring(0, pos);
                    string newWord = s.Substring(pos, i + 1 - existingWord.Length); // work on 0...i, word with i+1 chars

                    if(cache[pos] && wordDict.Contains(newWord))
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
