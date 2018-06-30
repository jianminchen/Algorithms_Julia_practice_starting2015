using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{

    static void Main(String[] args)
    {
        RunSampleTestcase2();
        //ProcessInput(); 
    }

    static void ProcessInput()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        string[] inputStrings = Console.ReadLine().Split(' ');

        Console.WriteLine(SetupPrefixNeighbors(inputStrings));
    }

    /*
     * code review the algorithm here:
     * 
     */
    static long SetupPrefixNeighbors(string[] inputStrings)
    {
        int n = inputStrings.Length; 

        var dictionary = new Dictionary<string, int>();
        long   result = 0;
        string substring;
        List<string> substrings;

        for (int val = 11; val >= 1; val--)
        {
            substrings = new List<string>();

            foreach (var item in dictionary)
            {
                if (item.Key.Length == val + 1)
                {
                    substring = item.Key.Substring(0, val);
                    substrings.Add(substring);
                }
            }

            for (int i = 0; i < substrings.Count; i++)
            {
                if (!dictionary.ContainsKey(substrings[i]))
                {
                    dictionary[substrings[i]] = 1;
                }
            }

            for (int i = 0; i < n; i++)
            {
                if (val == 11)
                {
                    if (inputStrings[i].Length == 11)
                    {
                        substring = inputStrings[i].Substring(0, 10);
                        if (!dictionary.ContainsKey(substring))
                        {
                            dictionary[substring] = 1;
                        }

                        result += Calculate(inputStrings[i]);
                    }
                }
                else if (val == 1)
                {
                    if (inputStrings[i].Length == 1)
                    {
                        if (!dictionary.ContainsKey(inputStrings[i]))
                        {
                            result += Calculate(inputStrings[i]);
                        }
                    }
                }
                else
                {
                    if (inputStrings[i].Length == val)
                    {
                        if (dictionary.ContainsKey(inputStrings[i]))
                        {
                            dictionary.Remove(inputStrings[i]);
                        }
                        else
                        {
                            substring = inputStrings[i].Substring(0, val - 1);
                            if (!dictionary.ContainsKey(substring))
                            {
                                dictionary[substring] = 1;
                            }

                            result += Calculate(inputStrings[i]);
                        }
                    }
                }
            }
        }

        return result; 
    }

    static long Calculate(string s)
    {
        int length = s.Length;
        long result = 0;
        for (int i = 0; i < length; i++)
        {
            result += s[i];
        }
        return result;
    }

    // this test case helps to fix the design issue - how to update prefix neighbor's status
    // not necessary parent node, go up to the node in the orginal dictionary. 
    // string[] inputStrings = { "A", "AB", "ABCDEFGHIJK" };
    public static void RunSampleTestcase2()
    {
        //string[] inputStrings = { "A", "AB", "AC", "ABD", "B" };                        

        string[] inputStrings = {  "A", "AB", "ABCDEFGHIJK" };

        long benefitValue = SetupPrefixNeighbors(inputStrings);
    }

    public static void RunSampleTestcase1()
    {
        string[] inputStrings = { "AB", "AC", "A" };

        long benefitValue = SetupPrefixNeighbors(inputStrings);
    }

    public static void RunSampleTestcase3()
    {
        string[] inputStrings = { "A", "B" };

        long benefitValue = SetupPrefixNeighbors(inputStrings);
    }
}