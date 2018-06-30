    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace substring_practice
    {
    
        class Program
        {
            static void Main(string[] args)
            {
                var testResult = GetShortestUniqueSubstring(new char[] { 'a', 'b', 'c' }, "aefbcgaxy");
                Debug.Assert(testResult.CompareTo("bcga") == 0);
            }
                  
            public static string GetShortestUniqueSubstring(char[] search, string source)
            {
                // your code goes here
                if (search == null || search.Length == 0)
                {
                    return "";
                }

                // assume that arr is not empty
                if (source == null || source.Length == 0)
                {
                    return "";
                }

                // put arr to the dictionary
                var map = new Dictionary<char, int>();
                foreach (var item in search)
                {
                    map.Add(item, 1);
                }

                var needChars = search.Length; // 'xyz' - 3, var match = needChars == 0 
                // iterate the string and find match, and also keep track of minimum 
                var left = 0;
                var length = source.Length;

                var smallestLength = length + 1;  //  
                var smallestSubstring = "";

                for (int index = 0; index < length; index++)   //  
                {
                    var visit = source[index];

                    // x - 1 
                    var inMap = map.ContainsKey(visit);
                    var needOne = inMap && map[visit] > 0; // add checking contains
                    if (inMap)
                    {
                        map[visit]--;
                    }

                    if (needOne)
                    {
                        needChars--; // take x off 
                    }

                    var findMatch = needChars == 0;
                    if (!findMatch)
                    {
                        continue;
                    }

                    // move left point forward - while loop                
                    while (left <= index && (!map.ContainsKey(source[left]) || (map.ContainsKey(source[left]) && map[source[left]] < 0)))
                    {
                        var removeChar = source[left];

                        // update the variable needChars xx -1                     
                        if (map.ContainsKey(source[left]))
                        {
                            map[removeChar]++;
                        }

                        left++;
                    }

                    var currentLength = index - left + 1;
                    var findSmallerOne = currentLength < smallestLength;
                    if (findSmallerOne)
                    {
                        smallestLength = currentLength;
                        smallestSubstring = source.Substring(left, currentLength);

                        needChars++;
                        map[source[left]]++;
                        left = left + 1;
                    }
                }

                // edge case
                if (smallestLength == length + 1)
                {
                    return "";
                }
                else
                {
                    return smallestSubstring;
                }
            }
        }
    }