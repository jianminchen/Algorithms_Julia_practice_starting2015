using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode140_WordBreakII
{
    /// <summary>
    ///  Leetcode 140: word break 
    ///  source code: 
    ///  https://leetcode.com/problems/word-break-ii/discuss/44207/DFS+DP+Trie-beats-81
    /// </summary>   
    public class Solution
    {
        internal class Trie
        {
            class TrieNode
            {
                public TrieNode[] children = new TrieNode[26];
                public bool hasValue;
            }

            TrieNode root;

            public Trie(List<String> words)
            {
                root = new TrieNode();
                foreach (var item in words)
                {
                    Insert(item);
                }
            }

            public void Insert(String s)
            {
                var current = root;
                for (int i = 0; i < s.Length; i++)
                {
                    int index = s[i] - 'a';
                    var children = current.children;

                    if (children[index] == null)  // *
                    {
                        current.children[index] = new TrieNode();
                    }

                    current = current.children[index];
                }

                current.hasValue = true;
            }

            public List<String> FindNextPrefix(String s)
            {
                var prefixes = new List<string>();

                var iterator = root;
                for (int i = 0; i < s.Length; i++)
                {
                    int index = s[i] - 'a';

                    var children = iterator.children;
                    if (children == null || children[index] == null)
                    {
                        break;
                    }

                    if (children[index].hasValue)
                    {
                        prefixes.Add(s.Substring(0, i + 1));
                    }

                    iterator = children[index];
                }

                return prefixes;
            }
        }

        static void Main(string[] args)
        {
            string s = "catsanddog";
            var dict = new List<string> { "cat", "cats", "and", "sand", "dog" };

            // A solution is ["cats and dog", "cat sand dog"].

            var sentences = wordBreak(s, dict);
        }

        public static List<String> wordBreak(String s, List<String> wordDict)
        {
            var memo = new Dictionary<String, List<String>>();
            var trie = new Trie(wordDict);

            return helper(s, trie, memo);
        }

        /// <summary>
        /// code review: 
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="trie"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        private static List<String> helper(String s, Trie trie, Dictionary<String, List<String>> memo)
        {
            var words = new List<String>();

            if (memo.ContainsKey(s))
            {
                return memo[s];
            }

            if (s.Length == 0)
            {
                words.Add("");
                return words;
            }

            var prefixes = trie.FindNextPrefix(s);

            foreach (var next in prefixes)
            {
                if (!s.StartsWith(next))
                {
                    continue;
                }

                var tmp = helper(s.Substring(next.Length), trie, memo);
                foreach (var sub in tmp)
                {
                    words.Add(next + (string.IsNullOrEmpty(sub) ? "" : " ") + sub);
                }
            }

            memo.Add(s, words);

            return words;
        }
    }
}