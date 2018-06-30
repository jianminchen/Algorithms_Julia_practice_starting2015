using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode76MinimumWindowSubstring_mocking
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase3(); 
        }

        public static void RunTestcase()
        {
            var result = GetSmallestSubstring("xyyzyyyyxz", "zyx");

            Debug.Assert(result.CompareTo("yxz") == 0); 
        }

        public static void RunTestcase2()
        {
            var result = GetSmallestSubstring("xyabczyyyyaxz", "zyx");

            Debug.Assert(result.CompareTo("yaxz") == 0);
        }

        public static void RunTestcase3()
        {
            var result = GetSmallestSubstring("xyabczyxyyyaxz", "zyx");

            Debug.Assert(result.CompareTo("zyx") == 0);
        }

        public static string GetSmallestSubstring(string input, string unique)
        {
            if (input == null || input.Length == 0 || unique == null || unique.Length == 0)
            {
                return string.Empty;
            }

            // assume that input is not empty, unique is not empty
            int minimumLength = Int32.MaxValue;
            var hashset = new HashSet<char>();

            foreach (var item in unique)
            {
                hashset.Add(item);
            }

            var dictionary = new Dictionary<char, int>();

            int left = 0;
            int length = input.Length;
            string minimumString = string.Empty;

            for (int i = 0; i < length; i++)
            {
                var visit = input[i];

                //put into dictionary
                if (dictionary.ContainsKey(visit))
                {
                    dictionary[visit]++;
                }
                else
                {
                    dictionary.Add(visit, 1);
                }

                // if all unique chars is in the dictionary
                bool isFound = checkFound(hashset, dictionary);

                if (!isFound)
                {
                    continue;
                }

                while (left < length &&
                      (!hashset.Contains(input[left]) ||
                      (dictionary.ContainsKey(input[left]) &&
                       dictionary[input[left]] > 1)))
                {
                    var current = input[left];
                    dictionary[current]--;
                    left++;
                }

                var currentLength = i - left + 1;
                if (currentLength < minimumLength)
                {
                    minimumLength = currentLength;
                    minimumString = input.Substring(left, currentLength);
                }

                if(left < length)
                {
                    dictionary[input[left]]--; // forget in mocking on June 29, 2017
                }

                left++;
            }

            return minimumString;
        }

        /// <summary>
        /// code review on June 30, 2017
        /// </summary>
        /// <param name="hashset"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>

        private static bool checkFound(HashSet<char> hashset, Dictionary<char, int> dictionary)
        {
            foreach (var item in hashset)
            {
                if (!dictionary.ContainsKey(item) || dictionary[item] == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}