using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvenTreeGraphAlgorithm
{
    class Graph
    {
        private int V;
        private string[] adj;

        public Graph(int V)
        {
            this.V = V;
            adj = new string[V+1]; // bug001 - index starting from 1, not 0, so array size V+1
        }

        public void addEdge(int v, int w)
        {
            adj[v] += ((adj[v]== null || adj[v].Length == 0) ? "" : ";" )+ w.ToString();  // string is null, run exception
            adj[w] += ((adj[w]== null || adj[w].Length == 0) ? "" : ";") + v.ToString();
        }

        public int countNoRemovedEdges()
        {
            int count = 0;
            for (int i = 1; i <= V; i++)  // include V, vertices are numbered from 1, 2, ..., V
            {
                string[] arr = adj[i].Split(';');
                int len = arr.Length; 

                if(len >= 1)
                {
                    for (int j = 0; j < arr.Length; j++)
                    {
                        int no = Convert.ToInt32(arr[j]);
                        if (haveMoreThanOneEdge(no) && !have3Edge(no))
                        {
                            // remove the edge 
                            removeEdge(i, no);
                            removeEdge(no, i);
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        private bool haveMoreThanOneEdge(int no)
        {
            string s = adj[no];
            return s.Split(';').Length > 1;
        }

        private bool have3Edge(int no)
        {
            string s = adj[no];
            return s.Split(';').Length  == 3;
        }

        private void removeEdge(int source, int removed)
        {
            string tmpS = adj[source];
            int len = tmpS.Length;

            int pos = tmpS.IndexOf(removed.ToString());
            if (pos > -1)
            {
                if ((len - pos) > 2)
                {
                    string s0 = removed.ToString() + ";";
                    adj[source] = tmpS.Replace(s0, "");
                }
                else
                {
                    adj[source] = tmpS.Substring(0, pos - 1);  // get rid of ; 
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] s = Console.ReadLine().Split(' ');
            int noVertices = Convert.ToInt32(s[0]);
            int noEdges = Convert.ToInt32(s[1]);

            Graph graph = new Graph(noVertices);
            for (int i = 0; i < noEdges; )
            {
                string[] s1 = Console.ReadLine().Split(' ');
                if (s1.Length >= 2)
                {
                    int start = Convert.ToInt32(s1[0]);
                    int end = Convert.ToInt32(s1[1]);

                    graph.addEdge(start, end);
                    i++; 
                }
            }

            Console.WriteLine(graph.countNoRemovedEdges());
        }
    }
}
