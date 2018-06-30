using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadsInHackerLand
{
    /*
         * N < 10^5    number of Cities
         * M < 2x10^5  number of Roads
         * City ID: 1 - N
         * length of road: Ci, 
         * road distance is different, 2^Ci
    */
    public class Distance{
        public int          oriDistance;          // orignal - directed connected
        public int          calculatedDistance;   // go over the graph to find shortest one
        public bool         usingDirected;        // directed edge is shortest distance 
        public List<int>    route;                // n1, n2, n3 - 3 nodes to a route

        public int[]        ID;                   // city id for the start and end. 
    }

    /*
     * Maintain a single linked list 
     * 
     */
    public class LinkedNode
    {
        public int ID;
        public LinkedNode prev; 
        public List<int> len;
        public int sum;  // sum of distance 
    }

    public class Graph{

        public int noCity; 
        public int noRoads;

        public Dictionary<Tuple<int, int>, Distance> edges;         
    }

    /*
     * Work on the graph 
     * - calculate minimum distance between any two nodes with directed connection
     * 
     * https://www.hackerrank.com/contests/june-world-codesprint/challenges/johnland
     */

    class Program
    {        
        static void Main(string[] args)
        {
            string[] arr = Console.ReadLine().Split(' ');

            int cities = Convert.ToInt32(arr[0]);
            int roads  = Convert.ToInt32(arr[1]);

            Graph graph = new Graph();

            graph.edges = new Dictionary<Tuple<int, int>, Distance>(); 

            for(int i=0; i< roads; i++)
            {
                string[] arr1 = Console.ReadLine().Split(' ');

                int[] arr2    = {   Convert.ToInt32(arr1[0]), 
                                    Convert.ToInt32(arr1[1]), 
                                    Convert.ToInt32(arr1[2])}; 

                Tuple<int, int> tuple = new Tuple<int, int>(arr2[0], arr2[1]);
                Distance dist = new Distance();
                dist.oriDistance = arr2[2];

                int smallerID = Math.Min(arr2[0], arr2[1]);
                int biggerId  = Math.Max(arr2[0], arr2[1]);
                dist.ID = new int[] { smallerID, biggerId };  

                graph.edges.Add(tuple, dist); 
            }

            calculateShortestDistance(graph); 
        }

        /*
         * The function design:
         * Need to know if the edge is shortest distance by connected directly, 
         * otherwise, get the route of shortest distance, and also mark the edge/ remove the edge
         * for future consideration - any two node shortest distance calculation
         * 
         * The idea is to using BFS - breadth first search 
         * -- need to avoid loops 
         * -- other concerns. 
         */
        public static void calculateShortestDistance(Graph graph)
        {
            List<Tuple<int, int>> list = graph.edges.Keys.ToList(); 

            foreach(Tuple<int,int> item in list)
            {
                // For example, edge 1-3
                int cityA = item.Item1; 
                int cityB = item.Item2;

                Distance dist = graph.edges[item];

                
                int directedDist = dist.oriDistance;

                HashSet<int> visited = new HashSet<int>();

                Queue<LinkedNode> queue = new Queue<LinkedNode>();
                LinkedNode node = new LinkedNode();
                node.ID = cityA;
                node.prev = null;
                node.len = new List<int>();
                node.sum = 0; 

                visited.Add(cityA);

                queue.Enqueue(node); 

                while(queue.Count > 0)
                {
                    LinkedNode tmpNode = (LinkedNode)queue.Dequeue();

                    // neighbors should not include itself, visited nodes
                    // avoid time out - need to come out O(logn) algorithm ? 
                    HashSet<int> neighbors = getNeighbors(tmpNode, cityB, visited, graph); 

                    foreach(int oneNeighbor in neighbors)
                    {
                        int currentLen = getDistance(graph, getTuple(oneNeighbor, tmpNode.ID))); 

                        if(oneNeighbor == cityB)
                        {
                            // set
                            //break; // cannot break the queue 
                        }
                        else if(currentLen + tmpNode.sum < directedDist)
                        {
                            LinkedNode selectedNode = new LinkedNode(); 
                            selectedNode.ID = oneNeighbor; 


                            queue.Enqueue(selectedNode); 
                        }
                    }
                }                
            }
        }
    }
}
