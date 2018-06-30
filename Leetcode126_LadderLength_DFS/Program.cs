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

       // private static List<List<String>> ladders = new List<List<string>>();

        /*
         * Write down this member variable List<string> - design 
         * 
         */
        //private static List<String> ladderHelper = new List<string>();

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
            int dist = getLadderLengthAndDictionary_BFS(beginWord, endWord, dict, dictionary);

            resetDistanceFromEnd(dictionary, dist);

            ISet<String> visited = new HashSet<string>();
            List<List<String>> ladders = new List<List<string>>();
            List<String> ladderHelper = new List<string>();

            getLadders_DFS_Backtracking(dist - 1, beginWord, endWord, dict, visited, dictionary, ladders, ladderHelper);

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

        /*
         * BFS - using Queue
         * get ladder minimum length and also prepare Dictionary to store the string and distance
         * 
         * use BFS to search the distance and find the one - minimum one, and then stop. 
         * 
         */
        private static int getLadderLengthAndDictionary_BFS(
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
         * using DFS search, and backtracking.          * 
         * 
         * 
         * ladderHelper - 
         * ladders - 
         * 
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
         */
        private static void getLadders_DFS_Backtracking(
            int                     dist,
            String                  word,
            String                  endWord,
            ISet<String>            dict,
            ISet<String>            visited,
            Dictionary<String, int> ladderDictionary,
            List<List<String>>      ladders,
            List<String>            ladderHelper )
        {
            visited.Add(word);
             
            ladderHelper.Add(word);

            bool isEndWord = word.CompareTo(endWord) == 0;
            bool isBeforeEndWord = dist > 0 && ladderDictionary[word] <= dist;

            if (isEndWord)
            {
                List<String> list = new List<string>();
                 
                list.AddRange(ladderHelper);
                ladders.Add(list);
            }
            else if (isBeforeEndWord)
            {
                char[] arr = word.ToCharArray();

                // ready to go through DFS backtracking process to find one 
                for (int i = 0; i < word.Length; ++i)
                {
                    char backtracking_char  = arr[i];  // for backtracking ... on line 243
                    char replace            = word[i]; 

                    for (int j = 'a'; j <= 'z'; ++j)
                    {
                        if (j == replace)
                            continue; 
                        
                        arr[i] = (char)j;
                        String ij_word = new String(arr);

                        if ( dict.Contains(ij_word) && 
                            !visited.Contains(ij_word))
                        {
                            getLadders_DFS_Backtracking(dist - 1, 
                                ij_word, 
                                endWord, 
                                dict, 
                                visited, 
                                ladderDictionary, 
                                ladders, 
                                ladderHelper);
                        }                   
                    }

                    arr[i] = backtracking_char; // restore.  backtracking  line 243 
                }
            }

            visited.Remove(word);  // back tracking 
             
            ladderHelper.RemoveAt(ladderHelper.Count - 1); 
        }
    }
}