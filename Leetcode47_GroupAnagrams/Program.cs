using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode49_GroupAnagrams
{
    public class HashKeys
    {
        /* O(n) solution 
         * abc 
         * hashed to key:
         * 01-11-11
         * 0 stands for a, 1 is count of a
         * further simplify the key:
         * 1-1-1
         * first 1 is count of a, 
         * second 1 is count of b, 
         * third 1 is count of c
         * 
         * In the end, "abc" hashed key will be 
         * "1-1-1-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0"
         */
        public static string ConvertHashKeys(string input)
        {
            if (input == null || input.Length == 0)
            {
                return string.Empty;
            }

            int[] countAlphabetic = new int[26];

            foreach (char c in input)
            {
                countAlphabetic[c - 'a']++;
            }

            return String.Join("-", countAlphabetic);
        }
    }
    class Program
    {                
        /*
         * Leetcode 49 - group anagrams
         * https://leetcode.com/problems/anagrams/
         * 
         */
        static void Main(string[] args)
        {
            RunTestcaseHashfunction();
            RunSampleTestcase();             
        }

        public static void RunTestcaseHashfunction()
        {
            var key = HashKeys.ConvertHashKeys("abc");
            Debug.Assert(key.CompareTo("1-1-1-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0") == 0);

            var key2 = HashKeys.ConvertHashKeys("xaba");
            Debug.Assert(key2.CompareTo("2-1-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-1-0-0") == 0);

            var key3 = HashKeys.ConvertHashKeys("xbaa");
            Debug.Assert(key3.CompareTo("2-1-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-1-0-0") == 0);
        }

        public static void RunSampleTestcase()
        {
            string[] input = new string[]{"ape","and","cat"}; 

            GroupAnagrams(input);
        }

        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var groupAnagrams = new List<IList<string>>(); 

            var groupAnagramsWithKeys = new Dictionary<string, IList<string>>(); 

            foreach(string s in strs)
            {
                string key = HashKeys.ConvertHashKeys(s); 
                if(groupAnagramsWithKeys.ContainsKey(key))
                {
                    var anagrams = groupAnagramsWithKeys[key]; 
                    anagrams.Add(s); 
                    groupAnagramsWithKeys[key] = anagrams; 
                }
                else{
                    var anagrams = new List<string>(); 
                    anagrams.Add(s); 
                    groupAnagramsWithKeys.Add(key, anagrams); 
                }
            }

            foreach(var value in groupAnagramsWithKeys.Values)
            {
                groupAnagrams.Add(value); 
            }

            return groupAnagrams; 
        }
    }
}
