using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefixNeighbors
{
    class TrieNode
    {
        private static readonly int ALPHABET_SIZE = 26; 
        public string content { get; set;}
        public TrieNode[] children = new TrieNode[ALPHABET_SIZE]; // 
        private bool isLeaf {get; set;}

        /*
         * 1. the string's array maximum length is 4 * 100000, 
         * minimum length is 1
         * 2. Each string's length is from 1 to 11. 
         * 3. Each string contains only uppercase letters. So in total there are 26 letters. 
         * ABCDEFGHIJKLMNOPQRSTUVQXYZ
         * A's ascii value is 65
         * 
         * Sorting algorithm should be O(N) level, taking adavantage of the string's length is smaller
         * than 11; Letters are size of 26. 
         * Sort by radix sorting. Sort by each letter from leftmost to right, in ascending order. 
         * 
         */
        public static IEnumerable<string> Sort(string[] input)
        {
            // sort by each letter from leftmost to right
        }

        public static 
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
