using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution
{
    /*
     * Study C#: 
     * how the code is written? public, Pascal case for public variables, get, set
     */
    class Index
    {
        public Dictionary<char, Index> Children { get; set; }
        public bool IsWord { get; set; }
        public string Word { get; set; }

        public Index()
        {
            Children = new Dictionary<char, Index>();
            IsWord = false;
            Word = "";
        }

        /*
         * code review: ?
         */
        public Tuple<string, string> Add(int index, char[] word, string wordy, string neighbour)
        {
            if (index == word.Length)
            {
                IsWord = true;
                Word = wordy;
                return new Tuple<string, string>(wordy, neighbour);
            }
            else
            {
                if (!Children.ContainsKey(word[index]))
                {
                    Children[word[index]] = new Index();
                }

                return Children[word[index]].Add(index + 1, word, wordy, IsWord ? Word : neighbour);
            }
        }
    }

    /*
     * code review by Jianmin Chen
     * Feb. 12, 2017
     */
    static long Process(string[] dict)
    {
        var points = 0L;
        var banned = new HashSet<string>();
        var stack  = new Stack<Tuple<string, string>>();
        var index  = new Index();
        var groupped = dict.GroupBy(x => x[0]);

        foreach (var g in groupped)
        {
            var sort = g.OrderBy(x => x.Length);
            index = new Index();
            stack = new Stack<Tuple<string, string>>();
            banned = new HashSet<string>();
            
            foreach (var word in sort)
            {
                stack.Push(index.Add(0, word.ToCharArray(), word, ""));
            }

            foreach (var tuple in stack)
            {
                if (!banned.Contains(tuple.Item1))
                {
                    points += tuple.Item1.ToCharArray().Aggregate(0L, (val, next) => val + (long)next);
                    banned.Add(tuple.Item2);
                }
            }
        }

        return points;
    }

    static void Main(String[] args)
    {
        //ProcessInput();
        RunSampleTestcase3(); 
    }

    public static void ProcessInput()
    {
        var n = Convert.ToInt32(Console.ReadLine());
        var dict = Console.ReadLine().Split(' ');
        var points = Process(dict);

        Console.WriteLine(points);
    }

    // this test case helps to fix the design issue - how to update prefix neighbor's status
    // not necessary parent node, go up to the node in the orginal dictionary. 
    // string[] inputStrings = { "A", "AB", "ABCDEFGHIJK" };
    public static void RunSampleTestcase2()
    {
        //string[] inputStrings = { "A", "AB", "AC", "ABD", "B" };                        
        
        string[] inputStrings = { "CA","A", "AB", "ABCDEFGHIJK" };

        long benefitValue = Process(inputStrings);
    }

    public static void RunSampleTestcase1()
    {        
        string[] inputStrings = {"AB","AC","A"};       

        long benefitValue = Process(inputStrings);
    }

    public static void RunSampleTestcase3()
    {
        string[] inputStrings = { "A", "B" };

        long benefitValue = Process(inputStrings);
    }
}