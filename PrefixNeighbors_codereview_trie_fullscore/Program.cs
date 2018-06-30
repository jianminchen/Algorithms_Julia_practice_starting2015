using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefixNeighbors_codereview_trie_fullscore
{
    class Program
    {
        static void Main(String[] args)
        { 
            int n = Convert.ToInt32(Console.ReadLine());

            String[] s = Console.ReadLine().Split(' ');
            
            /*
            int n = 3; 
            String[] s = new string[] {"A","B","AE" }; 
            */

            Trie trie = new Trie();
            int total = 0;
            var map = new Dictionary<Int32, List<Int32>>();

            for (int i = 0; i < n; ++i)
            {
                if (!map.ContainsKey(s[i].Length))
                {
                    var temp = new List<Int32>();
                    temp.Add(i);
                    map.Add(s[i].Length, temp);
                }
                else
                {
                    int length = s[i].Length;
                    var temp = map[length];
                    temp.Add(i);
                    map[length] =  temp;
                }
            }

            var keys = map.Keys.ToList();
            keys.Sort();

            foreach (Int32 len in keys)
            {
                var indices = map[len];
                foreach (Int32 index in indices)
                {
                    if (!trie.CheckPrefix(s[index]))
                    {
                        trie.Insert(s[index]);

                        total += CalculateBenefitValue(s[index]);
                    }
                }
            }

            Console.WriteLine(total);
        }

        public static int CalculateBenefitValue(String s)
        {
            int sum = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                sum += s[i];
            }
            return sum;
        }
    }

    public class Node
    {
        public Node[] Children = new Node[26];

        public bool HasWord;
        public int  NeighborLen;
        public bool BelongToResult;

        public Node()
        {
            for (int i = 0; i < 26; ++i)
            {
                Children[i] = null;
            }

            HasWord = false;
            NeighborLen = -1;
            BelongToResult = false;
        }
    }

    public class Trie
    {
        Node root;
        public Trie()
        {
            root = new Node();
        }

        public void Insert(String s)
        {
            Node current = root;
            for (int i = 0; i < s.Length; ++i)
            {
                if (current.Children[s[i] - 65] == null)
                {
                    current.Children[s[i] - 65] = new Node();
                }

                current = current.Children[s[i] - 65];
            }

            current.HasWord = true;
            current.BelongToResult = true;
        }

        public bool CheckPrefix(String s)
        {
            // check prefix
            Node current = root;
            for (int i = 0; i < s.Length; ++i)
            {
                if (current.Children[s[i] - 65] == null)
                {
                    return false;
                }
                current = current.Children[s[i] - 65];
            }
            current.HasWord = true;

            // update neighborlen for descendants
            for (int i = 0; i < 26; ++i)
            {
                if (current.Children[i] != null)
                {
                    UpdateNeighborLen(current.Children[i], s.Length);
                }
            }

            // check prefix neighbor
            for (int i = 0; i < 26; ++i)
            {
                if (current.Children[i] != null)
                {
                    if (CheckNeighbor(current.Children[i], s.Length) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void UpdateNeighborLen(Node node, int strlen)
        {
            if (node.HasWord == true)
            {
                if (node.NeighborLen == -1)
                {
                    node.NeighborLen = strlen;
                }
            }
            for (int i = 0; i < 26; ++i)
            {
                if (node.Children[i] != null)
                {
                    UpdateNeighborLen(node.Children[i], strlen);
                }
            }
        }

        public bool CheckNeighbor(Node node, int strlen)
        {
            if (node.HasWord == true)
            {
                if ((node.NeighborLen == strlen) && (node.BelongToResult == true))
                {
                    return true;
                }
            }

            for (int i = 0; i < 26; ++i)
            {
                if (node.Children[i] != null)
                {
                    if (CheckNeighbor(node.Children[i], strlen) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
