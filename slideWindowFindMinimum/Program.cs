using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slideWindowFindMinimum
{
          
    ///
    /// linear scan 
    //  brute force, O(n^2), any substring, find something
    //  
    // optimal solution: 
    // start, end = 0 -> scan the array 
    // end-> move, include all chars, find one
    // design scheme to move left point -> 
    // x, y, z ->  xyxx, this way, you can move left point to next one, first x 
    // yxxz -> all x, y, z are included, so I record length = 4
    //
    // yxxzy-> move left, remove y, check x, remove x, xzy => 
    // compared to existing length, min = 3, nothing can beat minimum length: 3, stop
    //
    // xzy
    // record count of x, y, z, this way, if I can move left point or not 
    // <- good design <- slide window 
    // only scan array once -> O(n), sliding windows -> dictionary <char, int>, 
    // add/min, avoid scan the window extra   
    //
    // edge case -> unique array, should not empty, alphabet, maximum 26, 
    // 
    class PracticeSlideWindow
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Practice Slide Window");

            var result = FindSmallestSubstringContainingUniqueChars_LinearScan("xyxxzy", "yxz");
        }  

        /// <summary>
        /// linear scan the array, keep two pointers, left/ right, sliding window,
        /// keep the slide window count of unqiure chars.
        /// Record minimum string, later just get the length
        /// </summary>
        /// <param name="s"></param>
        /// <param name="unique"></param>
        /// <returns></returns>
        public static string FindSmallestSubstringContainingUniqueChars_LinearScan(string s, string unique)
        {
            if (unique == null || unique.Length == 0)
            {
                return string.Empty;
            }

            // slide winodow
            int length = s.Length;

            var bookKeeping = new Dictionary<char, int>();                

            int minimumLength = Int32.MaxValue;

            string minimumStr = string.Empty;
                
            int start = 0;
            int end = 0; 
            for (int i = 0; i < length; i++)
            {
                char current = s[i];
                end = i; 

                if (!bookKeeping.ContainsKey(current)) // x
                {                       
                    bookKeeping.Add(current, 1);

                    // need to check if the string has everything
                    if (slidewindowContainsAllKey(bookKeeping, unique))
                    {
                        getNewMinimum(start, end, s, ref minimumLength, ref minimumStr);                            
                    }
                }
                else
                {
                    // check if the left point is the same char, move left pointer
                    // extra work - make it slide more than once if need                        
                        
                    bookKeeping[current]++;

                    bool moveStart = false;
                    while (start < length && bookKeeping[s[start]] > 1)
                    {
                        var removed = s[start];

                        bookKeeping[removed]--;
                        start++;
                        moveStart = true; 
                    }

                    if (moveStart && slidewindowContainsAllKey(bookKeeping, unique))
                    {
                        getNewMinimum(start, end, s, ref minimumLength, ref minimumStr);                            
                    }
                }
            }

            return minimumStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="s"></param>
        /// <param name="minimumLength"></param>
        /// <param name="minimumStr"></param>
        /// <returns></returns>
        private static bool getNewMinimum(int start, int end, string s, ref int minimumLength, ref string minimumStr)
        {
            int currentLength = end - start + 1;

            if (minimumLength > currentLength)
            {
                minimumLength = currentLength;
                minimumStr = s.Substring(start, end - start + 1);
                return true;
            }

            return false; 
        }

        /// <summary>
        /// time complexity: ? 
        /// </summary>
        /// <param name="bookKeeping"></param>
        /// <param name="unique"></param>
        /// <returns></returns>
        private static bool slidewindowContainsAllKey(IDictionary<char, int>bookKeeping, string unique)
        {
            foreach (var key in unique)
            {
                if (!bookKeeping.ContainsKey(key))
                {
                    return false; 
                }
            }

            return true; 
        }
    }    
}
