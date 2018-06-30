using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode76_minimumWindowSubstring
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = MinWindow("abc", "b");
        }

        public static string MinWindow(string search, string pattern) {          
            if(pattern == null || pattern.Length == 0 || search == null || search.Length == 0)
            {
                return string.Empty;
            }      
          
            int length = search.Length; 
      
            var dictionary = new Dictionary<char, int>();
            var hashset = new HashSet<char>(pattern);

            var patternDictionary = getDictionary(pattern); 

            int minimumLength = Int32.MaxValue;

            var result = string.Empty;
            int left = 0;            

            for(int i = 0; i < length; i++)
            {
                var visit = search[i]; 
        
                if(dictionary.ContainsKey(visit)) 
                {
                    dictionary[visit]++; 
                }
                else 
                {
                    dictionary.Add(visit, 1);
                }
        
                // check if I found the word 
                bool findCandidate = foundPattern(pattern, dictionary, patternDictionary); 
        
                if(!findCandidate)
                {
                    continue; 
                }
                
                // check that if left pointer can be moved. 
                while ( left < length &&
                    (!hashset.Contains(search[left]) ||
                         dictionary[search[left]] > patternDictionary[search[left]]))
                {
                    var current = search[left];
                    if (dictionary.ContainsKey(current))
                    {
                        var value = dictionary[current]; 
                        if( value == 1)
                        {
                            dictionary.Remove(current); 
                        }
                        else
                        {
                            dictionary[current]--; 
                        }
                    }

                    left ++;                   
                }
       
                var currentFound = search.Substring(left, i - left + 1); 
                var curentLength = currentFound.Length;

                if (curentLength < minimumLength)
                {
                    minimumLength = Math.Min(curentLength, minimumLength);
                    result = currentFound; 
                }            
  
                // move to next search
                if(left < length && dictionary.ContainsKey(search[left]))
                {
                    int value = dictionary[search[left]];

                    if (value == 1)
                    {
                        dictionary.Remove(search[left]);
                    }
                    else
                    {
                        dictionary[search[left]]--; 
                    }
                }

                left ++;   
                            
            }

            return result; 
        }

        private static Dictionary<char, int> getDictionary(string pattern)
        {
            var dictionary = new Dictionary<char, int>(); 

            foreach(var item in pattern)
            {
                if(!dictionary.ContainsKey(item))
                {
                    dictionary.Add(item, 1); 
                }
                else
                {
                    dictionary[item]++;
                }
            }

            return dictionary; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        private static bool foundPattern(string pattern, Dictionary<char, int> dictionary, Dictionary<char, int> patternDict)
        {
            foreach(char c in pattern)
            {
                if(!dictionary.ContainsKey(c))
                {
                    return false; 
                }

                int value = dictionary[c]; 
                if(value < patternDict[c])
                {
                    return false;
                }
            }

            return true; 
        }
    }
}
