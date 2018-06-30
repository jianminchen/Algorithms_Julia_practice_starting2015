using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestCommonPrefix_Trie
{
    public class StringComparer : IComparer<string>
    {
        public int Compare(string left, string right)
        {
            return left.CompareTo(right);
        }
    }

    class TrieNodeSelected
    {
        public TrieNode trieNode { get; set; }
        public bool isIncluding { get; set; }

        public TrieNodeSelected(TrieNode node, bool value)
        {
            trieNode = node;
            isIncluding = value;
        }
    }

    /*
     * First writing:
     * source code reference:
     * http://www.geeksforgeeks.org/longest-common-prefix-set-5-using-trie/
     * 
     * TrieNode class design: 
     * 6 APIs:
     * member variables:
     * 26 children node
     * isLeaf bool variable
     * TrieNode[] children
     * 
     * - TrieNode's bascis 
     * GetNode
     * 
     * - Construct a trie
     * AddOneStringToTrie -
     * AddStringsToTrie - 
     * 
     * - Query a trie  
     * 
     * - More advanced feature: 
     * build a trie first, and then query the trie. 
     * 
     * 
     */


    class TrieNode
    {
        private static readonly int ALPHABET_SZIE = 26;
        private static readonly char BASE = 'A';

        private TrieNode[] children = new TrieNode[ALPHABET_SZIE];

        private TrieNode parentNode { get; set; }
        private string name { get; set; }
        private bool isLeaf { get; set; }
        private bool isInDictionary { get; set; }

        // string's maximum length is 11
        private Dictionary<string, TrieNodeSelected>[] nodesByLevels = new Dictionary<string, TrieNodeSelected>[12];

        /*
         * Need to add more information for prefix neighbors processing
         */
        public void AddOneStringToTrie(TrieNode root, string input)
        {
            int length = input.Length;

            TrieNode runner = root;

            for (int i = 0; i < length; i++)
            {
                int index = CharToIndex(input[i]);

                if (runner.children[index] == null)
                {
                    runner.children[index] = PrepareTrieNode();
                    runner.children[index].parentNode = runner;
                    runner.children[index].name = input.Substring(0, i + 1);
                }

                // mark the string as a node in the dictionary
                if (i == length - 1)
                {
                    runner.children[index].isInDictionary = true;

                    nodesByLevels[i + 1].Add(input, new TrieNodeSelected(runner.children[index], true));
                }

                runner = runner.children[index];
            }

            // mark last node as leaf
            runner.isLeaf = true;
        }

        // A Function to construct trie
        public void AddStringsToTrie(string[] inputStrings, TrieNode root)
        {
            for (int i = 0; i < inputStrings.Length; i++)
            {
                AddOneStringToTrie(root, inputStrings[i]);
            }

            return;
        }

        /*
         * Need to do a few things:
         * 1. Set up a node 
         * 2. Set up nodesByLevels
         */
        public static TrieNode PrepareTrieNode()
        {
            TrieNode node = new TrieNode();

            node.isLeaf = false;
            for (int i = 0; i < ALPHABET_SZIE; i++)
            {
                node.children[i] = null;
            }

            // create dictionary for TrieNode
            for (int i = 0; i < 12; i++)
            {
                node.nodesByLevels[i] = new Dictionary<string, TrieNodeSelected>();
            }

            return node;
        }

            

        /*
         * start from 'A' - need to clarify 
         */
        public int CharToIndex(char c)
        {
            return c - BASE;
        }       
      
    }
}