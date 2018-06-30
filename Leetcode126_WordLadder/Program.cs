using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _126WordLadderII_P1
{
    class Program
    {
        static void Main(string[] args)
        {
            string beginWord = "hit";
            string endWord = "cog";
            string[] arr = new string[5] { "hot", "dot", "dog", "lot", "log" };
            HashSet<string> wordList = new HashSet<string>(arr);

            List<List<string>> result = findLadders(beginWord, endWord, wordList);
        }

        private static List<List<String>> ladders = new List<List<string>>();

        /*
         * Write down this member variable List<string> - design 
         * 
         */
        private static List<String> ladderHelper = new List<string>();

        public static List<List<String>> findLadders(
            String beginWord,
            String endWord,
            ISet<String> wordList)
        {

            List<List<String>> res = new List<List<string>>();

            if (beginWord.CompareTo(endWord) == 0)
            {
                List<String> aL = new List<string>();

                aL.Add(beginWord);
                res.Add(aL);
                return res;
            }

            ISet<String> dict = new HashSet<string>();

            dict = new HashSet<string>(wordList);
            dict.Add(endWord);

            Dictionary<String, int> dictionary = new Dictionary<String, int>();
            int dist = getLadderLengthAndDictionary(beginWord, endWord, dict, dictionary);

            resetDistanceFromEnd(dictionary, dist); 

            ISet<String> visited = new HashSet<string>();

            getLadders(dist - 1, beginWord, endWord, dict, visited, dictionary);

            return ladders;
        }

        /*
         * Let us talk about a test case to help understand the work: 
         * hit -> cog 
         * Two transformation paths:
         *               dot -> dog 
         * hit -> hot ->            -> cog 
         *               lot -> log 
         *               
         * dist value is 5 
         * Dictionary<string, int> 
         * key    value      new value 
         * hit     0            4
         * hot     1            3
         * dot     2            2
         * lot     2            2
         * dog     3            1
         * log     3            1
         * cog     4            0
         * 
         * 
         */
        private static void resetDistanceFromEnd(Dictionary<string, int> dictionary, int dist)
        {
            List<string> keyArr = new List<string>(dictionary.Keys);
            foreach (String s in keyArr)
            {
                dictionary[s] = dist - 1 - dictionary[s];
            }
        }

        private static int getLadderLengthAndDictionary(
            String beginWord,
            String endWord,
            ISet<String> wordList,
            Dictionary<String, int> ladderDictionary)
        {
            int length = 0;

            ISet<String> visited = new HashSet<string>();

            Queue<String> q = new Queue<string>();
            Queue<int> qDist = new Queue<int>();

            q.Enqueue(beginWord);

            qDist.Enqueue(0);

            visited.Add(beginWord);

            while (q.Count > 0)
            {

                String w = q.Dequeue();
                int len = qDist.Dequeue();

                ladderDictionary[w] = len;

                if (w.CompareTo(endWord) == 0)
                {
                    length = len + 1;
                    break;
                }

                // get all neighbors of w, and add them to the queue, if they have not been visited.
                for (int i = 0; i < w.Length; ++i)
                {
                    for (int c = 'a'; c <= 'z'; ++c)
                    {
                        char[] cArr = w.ToCharArray();

                        if (c != cArr[i])
                        {
                            cArr[i] = (char)c;         // don't forget type conversion.
                            String nW = new String(cArr);

                            if (wordList.Contains(nW) && !visited.Contains(nW))
                            {
                                q.Enqueue(nW);

                                qDist.Enqueue(len + 1);

                                visited.Add(nW);
                            }
                        }
                    }
                }
            }

            return length;
        }

        /*
         * May 28, 2016 
         * using DFS search, and backtracking. 
         * 
         * 
         * 
         * ladderHelper - 
         * ladders - 
         * 
         */
        private static void getLadders(
            int dist,
            String word,
            String endWord,
            ISet<String> dict,
            ISet<String> visited,
            Dictionary<String, int> ladderDictionary)
        {
            visited.Add(word);
            ladderHelper.Add(word);

            if (word.CompareTo(endWord) == 0)
            {
                List<String> list = new List<string>();
                list.AddRange(ladderHelper);
                ladders.Add(list);
            }
            else if (dist == 0 || ladderDictionary[word] > dist)
            {
                // do nothing. 
            }
            else
            {
                char[] arr = word.ToCharArray();

                for (int i = 0; i < word.Length; ++i)
                {
                    char ithChar = arr[i];  // for backtracking ... on line 202 
                    for (int j = 'a'; j <= 'z'; ++j)
                    {
                        if (j != word[i])
                        {
                            arr[i] = (char)j;
                            String ij_word = new String(arr);

                            if (dict.Contains(ij_word) && !visited.Contains(ij_word))
                            {
                                getLadders(dist - 1, ij_word, endWord, dict, visited, ladderDictionary);
                            }
                        }
                    }

                    arr[i] = ithChar; // restore.  backtracking  line 202 
                }
            }

            visited.Remove(word);  // back tracking 
            ladderHelper.RemoveAt(ladderHelper.Count - 1);  // remove last one in the list 
        }
    }
}