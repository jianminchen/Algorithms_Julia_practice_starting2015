using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowManySubstrings
{
    public class BoyerMoore
    {
        private int R;           // the radix
        private int[] right;     // the bad-character skip array

        private char[] pattern;  // store the pattern as a character array
        private String pat;      // or as a string

        /**
         * 
         * code source from Java code
         * 
         * http://algs4.cs.princeton.edu/53substring/BoyerMoore.java.html
         * Preprocesses the pattern string.
         *
         * @param pat the pattern string
         */
        public BoyerMoore(String pat)
        {
            this.R = 256;
            this.pat = pat;

            // position of rightmost occurrence of c in the pattern
            right = new int[R];
            for (int c = 0; c < R; c++)
                right[c] = -1;
            for (int j = 0; j < pat.Length; j++)
                right[pat[j]] = j;
        }

        /**
         * Preprocesses the pattern string.
         *
         * @param pattern the pattern string
         * @param R the alphabet size
         */
        public BoyerMoore(char[] pattern, int R)
        {
            this.R = R;
            this.pattern = new char[pattern.Length];
            for (int j = 0; j < pattern.Length; j++)
                this.pattern[j] = pattern[j];

            // position of rightmost occurrence of c in the pattern
            right = new int[R];
            for (int c = 0; c < R; c++)
                right[c] = -1;
            for (int j = 0; j < pattern.Length; j++)
                right[pattern[j]] = j;
        }

        /**
         * Returns the index of the first occurrrence of the pattern string
         * in the text string.
         *
         * @param  txt the text string
         * @return the index of the first occurrence of the pattern string
         *         in the text string; N if no such match
         */
        public int Search(String txt)
        {
            int M = pat.Length;
            int N = txt.Length;
            int skip;

            for (int i = 0; i <= N - M; i += skip)
            {
                skip = 0;
                for (int j = M - 1; j >= 0; j--)
                {
                    if (pat[j] != txt[i + j])
                    {
                        skip = Math.Max(1, j - right[txt[i + j]]);
                        break;
                    }
                }

                if (skip == 0) return i;    // found
            }

            return N;                       // not found
        }


        /**
         * Returns the index of the first occurrrence of the pattern string
         * in the text string.
         *
         * @param  text the text string
         * @return the index of the first occurrence of the pattern string
         *         in the text string; N if no such match
         */
        public int Search(char[] text)
        {
            int M = pattern.Length;
            int N = text.Length;
            int skip;
            for (int i = 0; i <= N - M; i += skip)
            {
                skip = 0;
                for (int j = M - 1; j >= 0; j--)
                {
                    if (pattern[j] != text[i + j])
                    {
                        skip = Math.Max(1, j - right[text[i + j]]);
                        break;
                    }
                }
                if (skip == 0) return i;    // found
            }
            return N;                       // not found
        }
    }   

    class Program
    {
        static Dictionary<string, bool> SearchedHistory = new Dictionary<string, bool>(); 
        static void Main(string[] args)
        {
            ProcessUserInput(); 
            //RunSampleTestcase1(); 
        }

        private static void RunSampleTestcase1()
        {
            int noSubstrings = CountSubstringsUsingDynamicProgramming("abaa");
            Debug.Assert(noSubstrings == 8); 
        }

        private static void ProcessUserInput()
        {
            int[] data = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int stringLength = data[0];
            int queries = data[1];

            string content = Console.ReadLine();

            for (int i = 0; i < queries; i++)
            {
                int[] positions = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                int start = positions[0];
                int end = positions[1];

                Console.WriteLine(CountSubstrings(content, start, end));
            }
        }

        /*
         * 4:24pm 
         * need to look into the issues
         * 
         */
        private static int CountSubstrings(string content, int start, int end)
        {
            string searchedFrom = content.Substring(start, end - start +1); 

            return CountSubstringsUsingDynamicProgramming(searchedFrom); 
        }

        /*
         * 
         */
        private static int CountSubstringsStartZero(string content, int end)
        {
            return 0; 
        }

        private static int CountSubstringsUsingDynamicProgramming(string content)
        {
            if (content == null || content.Length == 0)
                return 0;

            int len = content.Length;
            int count = 0; 
            for (int i = 0; i < len; i++ )
            {
                if (i == 0)
                {
                    count++;
                    continue; 
                }

                string searchFrom = content.Substring(0, i );  
                for(int start = 0; start <= i; start ++ )
                {
                    string substring = content.Substring(start, i - start + 1); 
                    int startIndex = 0;
                    string key = searchFrom + "," + substring; 
                    if(SearchedHistory.ContainsKey(key))
                    {
                        if (!SearchedHistory[key])
                            count++; 
                    }
                    else 
                    {
                        if (FindUsingBoyerAlgo(substring, searchFrom, ref startIndex))
                        {
                            SearchedHistory.Add(key, true);
                        }
                        else
                        {
                            SearchedHistory.Add(key, false);
                            count++; 
                        }
                    }
                }
            }

            return count;
        }
        
        private static bool FindUsingBoyerAlgo(
            string lookup, 
            string content, 
            ref int start)
        {
            int searchLen = lookup.Length;
            int len = content.Length;

            if (searchLen > len)
                return false;

            BoyerMoore bm = new BoyerMoore(lookup);

            int offset = bm.Search(content);
            if (offset < len)
            {
                start = offset;
                return true;
            }

            return false;
        }
    }
}
