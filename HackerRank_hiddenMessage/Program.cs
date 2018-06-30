using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank_hiddenMessage
{
    /**
     source code:
     http://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/RabinKarp.java.html
    */
    public class RabinKarp
    {
        private String pat;      // the pattern  // needed only for Las Vegas
        private long patHash;    // pattern hash value
        private int m;           // pattern length
        private long q;          // a large prime, small enough to avoid long overflow
        private int R;           // radix
        private long RM;         // R^(M-1) % Q

        /**
         * Preprocesses the pattern string.
         *
         * @param pattern the pattern string
         * @param R the alphabet size
         */
        public RabinKarp(char[] pattern, int R)
        {
            this.pat = new string(pattern);
            this.R = R;
        }

        /**
         * Preprocesses the pattern string.
         *
         * @param pat the pattern string
         */
        public RabinKarp(String pat)
        {
            this.pat = pat;      // save pattern (needed only for Las Vegas)
            R = 256;
            m = pat.Length;
            q = LongRandomPrime();

            // precompute R^(m-1) % q for use in removing leading digit
            RM = 1;
            for (int i = 1; i <= m - 1; i++)
            {
                RM = (R * RM) % q;
            }

            patHash = hash(pat, m);
        }

        // Compute hash for key[0..m-1]. 
        private long hash(String key, int m)
        {
            long h = 0;

            for (int j = 0; j < m; j++)
            {
                h = (R * h + key[j]) % q;
            }

            return h;
        }

        // Las Vegas version: does pat[] match text[i..i-m+1] ?
        private bool Check(String text, int i)
        {
            for (int j = 0; j < m; j++)
            {
                if (pat[j] != text[i + j])
                {
                    return false;
                }
            }

            return true;
        }

        // Monte Carlo version: always return true
        // private boolean check(int i) {
        //    return true;
        //}

        /**
         * Returns the index of the first occurrrence of the pattern string
         * in the text string.
         *
         * @param  txt the text string
         * @return the index of the first occurrence of the pattern string
         *         in the text string; n if no such match
         */
        public int Search(String text)
        {
            int n = text.Length;
            if (n < m) return n;
            long txtHash = hash(text, m);

            // check for match at offset 0
            if ((patHash == txtHash) && Check(text, 0))
            {
                return 0;
            }

            // check for hash match; if hash match, check for exact match
            for (int i = m; i < n; i++)
            {
                // Remove leading digit, add trailing digit, check for match. 
                txtHash = (txtHash + q - RM * text[i - m] % q) % q;
                txtHash = (txtHash * R + text[i]) % q;

                // match
                int offset = i - m + 1;
                if ((patHash == txtHash) && Check(text, offset))
                {
                    return offset;
                }
            }

            // no match
            return n;
        }
        
        // a random 31-bit prime
        private static long LongRandomPrime()
        {
            //BigInteger prime = BigInteger.probablePrime(31, new Random());
            BigInteger prime = BigInteger.probablePrime(31, new Random());
            return prime.longValue();
        }

        /** 
         * Takes a pattern string and an input string as command-line arguments;
         * searches for the pattern string in the text string; and prints
         * the first occurrence of the pattern string in the text string.
         *
         * @param args the command-line arguments
         */
        public static void main(String[] args)
        {
            String pat = args[0];
            String txt = args[1];

            RabinKarp searcher = new RabinKarp(pat);
            int offset = searcher.Search(txt);

            // print results
            Console.WriteLine("text:    " + txt);

            // from brute force search method 1
            Console.WriteLine("pattern: ");
            for (int i = 0; i < offset; i++)
            {
                Console.WriteLine(" ");
            }

            Console.WriteLine(pat);
        }
    }
}
