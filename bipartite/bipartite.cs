using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bipartite
{
    /// <summary>
    /// code review: 
    /// http://www.geeksforgeeks.org/bipartite-graph/
    /// May 10, 2017 code review 
    /// </summary>
    class Bipartite
    {      
        static int vertices = 4; // No. of Vertices

        /// <summary>
        /// Using color to determine if the graph is bipartite or not. 
        /// Test case: Graph with cycle, odd number of node is not bipartite. 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public bool IsBipartite(int[][] graph, int src)
        {
            // Create a color array to store colors assigned to all veritces.
            // Vertex number is used as index in this array. The value '-1'
            // of colorArr[i] is used to indicate that no color is assigned
            // to vertex 'i'. The value 1 is used to indicate first color
            // is assigned and value 0 indicates second color is assigned.
            var colors = new int[vertices];

            for (int i = 0; i < vertices; ++i)
            {
                colors[i] = -1;
            }

            // Assign first color to source
            colors[src] = 1;

            // Create a queue (FIFO) of vertex numbers and enqueue
            // source vertex for BFS traversal
            var queue = new LinkedList<Int32>();
            queue.AddLast(src);

            // Run while there are vertices in queue (Similar to BFS)
            while (queue.Count != 0)
            {
                // Dequeue a vertex from queue
                int visitVertex = queue.First();
                queue.RemoveFirst(); 
                int visitColor = colors[visitVertex];

                // Find all non-colored adjacent vertices
                for (int v = 0; v < vertices; ++v)
                {
                    // An edge from u to v exists and destination v is
                    // not colored
                    bool edgeIsExisted = graph[visitVertex][v] == 1;                    

                    if(!edgeIsExisted)
                    {
                        continue; 
                    }

                    int  currentColor = colors[v];
                    bool notColored   = currentColor == -1;                    
                    bool sameColor    = currentColor == visitColor; 

                    if (notColored)
                    {
                        // Assign alternate color to this adjacent v of u
                        colors[v] = 1 - visitColor;  // 0, 1 two colors

                        queue.AddLast(v);
                    }
                    else if (sameColor)
                    {
                        // An edge from u to v exists and destination v is
                        // colored with same color as u
                        return false;
                    }
                }
            }

            // If we reach here, then all adjacent vertices can
            // be colored with alternate color
            return true;
        }
        
        public static void Main(String[] args)
        {
            RunTestcase1(); 
        }

        /// <summary>
        ///  the graph is with a cycle but with even node, so it is bipartite
        /// </summary>
        public static void RunTestcase1()
        {
            var graph = new int[4][]; 

            graph[0] = new int[4]{0, 1, 0, 1}; 
			graph[1] = new int[4]{1, 0, 1, 0};
			graph[2] = new int[4]{0, 1, 0, 1};
			graph[3] = new int[4]{1, 0, 1, 0};		   

            Bipartite b = new Bipartite();

            Debug.Assert(b.IsBipartite(graph, 0));            
        }
    }
}
