using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minimumWindowSubstring_Shortversion
{
    class minimumWindowSubstring_Shortversion
    {
        static void Main(string[] args)
        {
            RunTestcase(); 
        }

        public static void RunTestcase()
        {
            var result = MinWindow("xxyz", "xyz");
        }

        public static string MinWindow(string s, string t)
        {
            if (s.Length < t.Length || t.Length == 0)
            {
                return "";
            }

            int length  = s.Length;
            int tLength = t.Length; 

            int minimumSubstring_left  = -1;
            int minimumSubstring_right = length - 1;                                 

            var dict = new Dictionary<char, int>();

            for (int i = 0; i < tLength; i++)
            {
                var visit = t[i];
                if (!dict.ContainsKey(visit))
                {
                    dict[visit] = 1;
                }
                else
                {
                    dict[visit]++;
                }
            }

            int left = 0; 
            int index = 0;
            int count = 0;
            var queue = new Queue<int>();
            while (index < length)
            {
                var visit = s[index];

                if(!dict.ContainsKey(visit))
                {
                    index++;
                    continue; 
                }
                
                queue.Enqueue(index);
                --dict[visit];

                if (dict[visit] >= 0)
                {
                    count++; // count is the number of chars visited required in search string
                }

                if (count == tLength) 
                {
                    left = queue.Dequeue();
                    if (left == index) // ?
                    {
                        return t;
                    }

                    while (count == tLength)
                    {
                        var leftChar = s[left];

                        ++dict[leftChar]; // ?
                        if (dict[leftChar] > 0) 
                        {
                            count--;
                        }
                        else
                        {
                            left = queue.Dequeue();
                        }
                    }

                    if (index - left < minimumSubstring_right - minimumSubstring_left)
                    { 
                        minimumSubstring_right = index; 
                        minimumSubstring_left  = left; 
                    }
                }                

                index++;
            }

            return minimumSubstring_left == -1 ? "" : s.Substring(minimumSubstring_left, minimumSubstring_right - minimumSubstring_left + 1);
        }
    }
}
