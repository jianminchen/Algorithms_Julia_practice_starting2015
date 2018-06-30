using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// code review on June 17, 2017
/// Hidden message 
/// http://juliachencoding.blogspot.ca/2016/09/hackerrank-stryker-code-sprint-grind_3.html
/// </summary>
public class ResultInfo
{
    public int Start { get; set; }

    public string Word { get; set; }
}

/// <summary>
/// code review on Jun 17, 2017
/// </summary>
public class TrieNode
{
    public int Words { get; private set; }

    public int Prefixes { get; private set; }

    TrieNode[] edges;

    public string Word { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="word"></param>
    /// <param name="start"></param>
    public void Add(string word, int start)
    {
        TrieNode node = this;

        while (start < word.Length)
        {
            node.Prefixes++;

            int index = word[start] - 'a';

            if (node.edges == null)
            {
                node.edges = new TrieNode[26];
            }

            if (node.edges[index] == null)
            {
                node.edges[index] = new TrieNode();
            }

            node = node.edges[index];
            start++;
        }

        node.Words++;
        node.Prefixes++;
        node.Word = word;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public List<ResultInfo> FindWords(string s)
    {
        List<ResultInfo> result = new List<ResultInfo>();
        List<string>    matches = new List<string>();

        for (int i = 0; i < s.Length; i++)
        {
            int count = result.Count;
            FindMatch(s, i, matches);

            for (int j = count; j < matches.Count; j++)
            {
                result.Add(new ResultInfo { Start = i, Word = matches[j] });
            }
        }

        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="i"></param>
    /// <param name="matches"></param>
    void FindMatch(string s, int i, List<string> matches)
    {
        var node = this;
        while (i < s.Length)
        {
            if (node.Word != null)
            {
                matches.Add(node.Word);
            }

            int index = s[i] - 'a';
            if (node.edges == null || node.edges[index] == null)
            {
                return;
            }

            node = node.edges[index];
            i++;
        }

        if (node.Word != null)
        {
            matches.Add(node.Word);
        }
    }
}

class Solution
{
    static void Main(String[] args)
    {
        ProcessInput(); 
    }

    public static void ProcessInput()
    {
        string t = Console.ReadLine();
        string p = Console.ReadLine();
        string[] phrase = p.Split(' ');
        TrieNode root = new TrieNode();
        foreach (string word in phrase)
        {
            root.Add(word, 0);
        }

        var matches = root.FindWords(t);
        var dictionary = new Dictionary<string, List<int>>();

        foreach (var resultInfo in matches)
        {
            if (dictionary.ContainsKey(resultInfo.Word))
            {
                dictionary[resultInfo.Word].Add(resultInfo.Start);
            }
            else
            {
                var l = new List<int> { resultInfo.Start };
                dictionary.Add(resultInfo.Word, l);
            }
        }

        var phraseMatch = new int[phrase.Length];
        for (int i = 0; i < phraseMatch.Length; i++)
        {
            phraseMatch[i] = -1;
        }

        int matchesCount = 0;
        int lastIndex = -1;
        for (int i = 0; i < phrase.Length; i++)
        {
            string word = phrase[i];
            bool containingWord = dictionary.ContainsKey(word);
            if (!containingWord)
            {
                break;
            }

            if (dictionary.ContainsKey(word))
            {
                var l = dictionary[word];
                bool found = false;
                for (int j = 0; j < l.Count; j++)
                {
                    if (l[j] > lastIndex)
                    {
                        lastIndex = l[j];
                        phraseMatch[i] = lastIndex;
                        matchesCount++;
                        found = true;
                        break;
                    }
                }

                if (found == false)
                {
                    break;
                }
            }
        }

        if (matchesCount == phrase.Length)
        {
            Console.WriteLine("YES");

            PrintMatches(phrase, phraseMatch);

            Console.WriteLine(LevenshteinDistance(t, p));
        }
        else
        {
            Console.WriteLine("NO");
            if (matchesCount == 0)
            {
                Console.WriteLine("0");
            }
            else
            {
                PrintMatches(phrase, phraseMatch);
            }

            Console.WriteLine("0");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    static int LevenshteinDistance(string s, string t)
    {
        if (s == t) return 0;
        if (s.Length == 0) return t.Length;
        if (t.Length == 0) return s.Length;

        int[] v0 = new int[t.Length + 1];
        int[] v1 = new int[t.Length + 1];

        for (int i = 0; i < v0.Length; i++)
            v0[i] = i;

        for (int i = 0; i < s.Length; i++)
        {
            v1[0] = i + 1;

            for (int j = 0; j < t.Length; j++)
            {
                var cost = (s[i] == t[j]) ? 0 : 2;
                v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
            }

            for (int j = 0; j < v0.Length; j++)
                v0[j] = v1[j];
        }

        return v1[t.Length];
    }

    static void PrintMatches(string[] phrase, int[] phraseMatch)
    {
        bool first = true;
        for (int i = 0; i < phraseMatch.Length; i++)
        {
            if (phraseMatch[i] >= 0)
            {
                Console.Write("{0}{1} {2} {3}", first ? "" : " ", phrase[i], phraseMatch[i],
                    phraseMatch[i] + phrase[i].Length - 1);

                first = false;
            }
        }

        Console.WriteLine();
    }
}