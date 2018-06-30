using System;
using System.Collections.Generic;
// IMPORT LIBRARY PACKAGES NEEDED BY YOUR PROGRAM
// SOME CLASSES WITHIN A PACKAGE MAY BE RESTRICTED
// DEFINE ANY CLASS AND METHOD NEEDED
// CLASS BEGINS, THIS CLASS IS REQUIRED
public class Solution
{
    static void Main(string[] args)
    {
       // abdcghbaabcdij, bcda
        var result = getAnagramIndices("abdcghbaabcdij", "bcda");
        //var result2 = getAnagramIndices("bbbaba", "ba"); 

        // testing();            
    }

    // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
    // RETURN AN EMPTY LIST IF NO ANAGRAM FOUND
    public static List<int> getAnagramIndices(string haystack, string needle)
    {
        // WRITE YOUR CODE HERE
        if(haystack == null || haystack.Length == 0 || needle == null ||
            needle.Length == 0 || haystack.Length < needle.Length)
        {
            return new List<int>(); 
        }

        var dictionary = getDictionary(needle);

        int needleLength = needle.Length;
        int matches = 0; 
        for(int i = 0; i < needleLength; i++)
        {
            var visit = haystack[i]; 
            if(dictionary.ContainsKey(visit))
            {
                if(dictionary[visit] > 0)
                {
                    matches++; 
                }

                dictionary[visit]--;                
            }
        }

        // First possible candidate 
        var anagrams = new List<int>(); 
        if(matches == needleLength)
        {
            anagrams.Add(0); 
        }

        // iterate once 
        int removeIndex = 0; 
        for(int i = needleLength; i < haystack.Length; i++)
        {
            var remove = haystack[removeIndex];
            var visit  = haystack[i]; 

            // remove char first
            if(dictionary.ContainsKey(remove))
            {
                if(dictionary[remove] >= 0)
                {
                    matches--; 
                }

                dictionary[remove]++; 
            }

            // add char next 
            if(dictionary.ContainsKey(visit))
            {
                if (dictionary[visit] > 0)
                {
                    matches++;
                }

                dictionary[visit]--; 
            }

            bool foundMatch = matches == needleLength; 
            if(foundMatch)
            {
                anagrams.Add(i - needleLength + 1); 
            }

            removeIndex++; 
        }

        return anagrams; 
    }
    // METHOD SIGNATURE ENDS

    private static Dictionary<char, int> getDictionary(string s)
    {
        var dict = new Dictionary<char, int>(); 
        foreach(var item in s)
        {
            if(dict.ContainsKey(item))
            {
                dict[item]++; 
            }
            else
            {
                dict.Add(item, 1); 
            }
        }

        return dict; 
    }
}