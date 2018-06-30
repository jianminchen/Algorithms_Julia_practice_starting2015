using System;
using System.Collections.Generic;
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
            var result = GetSmallestSubstring("aa","aa"); 
        }

        public static void RunTestcase2()
        {
            var result = GetSmallestSubstring("a", "aa");
        }

        public static void RunTestcase3()
        {            
            var result = GetSmallestSubstring("cabefgecdaecf", "cae");
            // cabe, should be aec
        }

        public static string GetSmallestSubstring(string input, string search)
        {
          if(input == null || input.Length == 0 || search == null || search.Length == 0)
          {
            return string.Empty; 
          }
      
          // assume that input is not empty, unique is not empty
          int minimumLength = Int32.MaxValue; 
          var searchLookup = new Dictionary<char, int>(); 
      
          foreach(var item in search)
          {
            if(searchLookup.ContainsKey(item))
            {
                searchLookup[item]++;
            }
            else
            {
                searchLookup.Add(item, 1); 
            }
          }
      
          var dictionary = new Dictionary<char, int>(); 
      
          int left = 0; 
          int length = input.Length; 
          string minimumString = string.Empty; 
          char missingChar = ' ';
          bool findMissing = false; 
      
          for(int i = 0; i < length; i++)
          {
            var visit = input[i]; 
        
            //put into dictionary
            if(dictionary.ContainsKey(visit))
            {
              dictionary[visit] ++; 
            }
            else 
            {
              dictionary.Add(visit, 1); 
            }
        
            // if all unique chars is in the dictionary
            // need to prune the algorithm 
            if (findMissing && visit != missingChar)
            {                
                continue;                 
            }

            bool isFound = checkFound(searchLookup, dictionary, ref missingChar, ref findMissing); 
        
            if(!isFound)   
            {
              continue; 
            }                   
        
            while( left < length &&
                  (!searchLookup.ContainsKey(input[left]) ||
                  (dictionary.ContainsKey(input[left]) && 
                   dictionary[input[left]] > searchLookup[input[left]])))
            {
              var current = input[left];
              if (dictionary.ContainsKey(current))
              {
                  dictionary[current]--;
              }

              left ++; 
            }
           
            var currentLength = i - left + 1;
            if (currentLength >0 && currentLength < minimumLength)
            {
              minimumLength = currentLength;           
              minimumString = input.Substring(left, currentLength); 
            }

            // fail test case "aa", "aa"
            if (left < length)
            {
                findMissing = true;
                missingChar = input[left];

                dictionary[missingChar]--; // bug found by online judge
            }

            left ++;            
          }

          return minimumString; 
        }

        /// <summary>
        /// code review on June 30, 2017
        /// </summary>
        /// <param name="search"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>

        private static bool checkFound(Dictionary<char, int> search, Dictionary<char, int> dictionary, ref char missingChar, ref bool findMissing)
        {
            foreach (var item in search)
            {
                var key = item.Key;
                var value = item.Value; 

                if (!dictionary.ContainsKey(key) || dictionary[key] < value)
                {
                    findMissing = true; 
                    missingChar = key;
                    return false;
                }
            }

            findMissing = false;
            return true;
        }
    }
}
