using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _159LongestTwoDistinctCharacters
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] test = new string[2]{"eoeabc","aeooeoebc"};

            int result = lengthOfLongestSubstringTwoDistinct(test[1]); 
        }
        /*
         *  June 1, 2016
         *  Longest substring with two distince characters
         * Leetcode 159: 
         * Work on one test case: 
         * 
         * check Leetcode online judge
         */
        public static int lengthOfLongestSubstringTwoDistinct(String s) {
            if (s == null) 
                return 0;
            int len = s.Length; 
            if (len <= 2) 
                return len;

            Dictionary<char, int> hm = new Dictionary<char, int>();
            hm[s[0]] = 1;

            /* sliding window:  left point: left, right point: right
             * */
            int left = 0;
            int right = 0; // inclusive
            int maxLen = 1;
            while (right < len) {
                if (hm.Count <= 2) {
                
                    int distance = right - left + 1;
                    maxLen = Math.Max(maxLen, distance);
                    ++right;

                    if (right >= len) break;

                    char curr = s[right]; 
                    if (hm.ContainsKey(curr)) {
                        hm[curr] = hm[curr] + 1;
                    } else {
                        hm[curr] =  1;
                    }
                } else { 
                    // hm.size() > 2
                    // remove if curr char is only one in dictionary
                    // otherwise, decrement one. 
                    char curr = s[left]; 
                    if (hm[curr] == 1) {
                        hm.Remove(curr);
                    } else {
                        hm[curr] = hm[curr] - 1;
                    }

                    ++left;
                }
            }

            return maxLen;
        }
    }
}
