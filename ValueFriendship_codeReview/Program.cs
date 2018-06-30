using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

class Solution
{
    /*
     * study the code
     * January 16, 2017
     * 
     */
    public class Group
    {
        public int totalNumberNodes = 1;
    }

    /*
     * GraphNode class
     */
    public class GraphNode
    {
        // Julia added the variable: name, so she can track GraphNode and work on the test case 
        // with 5 nodes: 0, 1, 2, 3, 4
        public string name; 

        // variables are defined by original author, study code from one of submissions with maximum score
        public Group group;
        public List<Edge> edges = new List<Edge>();

        public GraphNode(string s)
        {
            this.name = s; 
        }

        /*
         * Work on the test case, illustrate the process using specific examples. 
         * 
         * Use name to track with edge, which node in the test case: 
         * 1-2-3 4-5
         * 5 nodes in the graph, 4 connections, 1-3 is redundant. 
         * 
         * Go over each edge, and then remove friend node from nodes HashSet, edge from edges HashSet, 
         * and share the group to friend node, add one more to variable n, push friendNode to the stack. 
         */
        public void Connect(HashSet<GraphNode> nodes, HashSet<Edge> edges, Stack<GraphNode> neighbours)
        {
            foreach (var edge in this.edges)
            {
                var friendNode = (edge.id_1 != this) ? edge.id_1 : edge.id_2;

                if (friendNode.group == null)
                {
                    nodes.Remove(friendNode);
                    edges.Remove(edge);

                    friendNode.group = group;
                    group.totalNumberNodes++;        
                    neighbours.Push(friendNode);   
                }
            }
        }
    }

    public class Edge
    {
        public GraphNode id_1;
        public GraphNode id_2;
    }

    static void Main(string[] args)
    {
        ProcessInput();
        //RunSampleTestcase(); 
    }

    /*
     * Need to work on the sample test case
     * 1. student 1 and 2 become friends
     *  1-2 3 4 5,  we then sum the number of friends that each student has
     * to get 1 + 1 + 0 + 0 + 0 = 2. 
     * 2. Student 2 and 3 become friends:
     *  1-2-3 4 5, we then sum the number of friends that each student has to get
     * 2 + 2 + 2 + 0 + 0 = 6. 
     * 3. Student 4 and 5 become friends:
     * 1-2-3 4-5, we then sum the number of friends that each student has to get 
     * 2 + 2 + 2 + 1 + 1 = 8. 
     * 4. Student 1 and 3 become friends: (we hold to add 1 and 3 until 4 and 5 are added to maximize the value.)
     * 1-2-3 4-5, we then sum the number of friends that each student has to get
     * 2 + 2 + 2 + 1 + 1 = 8. 
     * Total is 2 + 6 + 8 + 8 = 24. 
     */
    public static void RunSampleTestcase()
    {
        int queries = 1; 
        string[][] datas = new string[1][]; 
        datas[0] = new string[2]; 
        datas[0][0] = "5"; 
        datas[0][1] = "4"; 

        string[][] allFriendships = new string[1][]; 
        allFriendships[0] = new string[4];

        allFriendships[0][0] = "1 2";
        allFriendships[0][1] = "2 3";
        allFriendships[0][2] = "1 3";
        allFriendships[0][3] = "4 5"; 

        IList<long> result = MaximizeValues( queries,   datas,  allFriendships);
        Debug.Assert(result[0] == 24); 
    }

    /*
     * code review:
     * Everything is in one function, should break down a few of pieces
     * 1. Input
     * 2. Set up multiple graphes
     * 3. minimum edges to connect the graph
     * 4. extra edges to hold for maximum output, added by descending order. 
     * 4. Output
     */
    public static void ProcessInput()
    {
        int queries = Convert.ToInt32(Console.ReadLine());
        string[][] graphData = new string[queries][];
        string[][] allFriendships = new string[queries][];        

        for (int query = 0; query < queries; query++)
        {
            string[] data = Console.ReadLine().Split(' ');
            int totalNodes  = Convert.ToInt32(data[0]);
            int friendships = Convert.ToInt32(data[1]);

            graphData[query] = new string[] { totalNodes.ToString(), friendships.ToString() };

            allFriendships[query] = new string[friendships]; 

            for (int i = 0; i < friendships; i++)
            {                
                allFriendships[query][i] = Console.ReadLine();                               
            }
        } // end of process input

        IList<long> result = MaximizeValues(queries, graphData, allFriendships); 
        foreach(long value in result)
        {
            Console.WriteLine(value); 
        }
    }

    /*
     * Maximum value to add the friendship 
     * 3 rules to follow - check editorial notes:
     *    The graph is comprised of several components. 
     *    Each component has its own size. 
     *   1. At first if a component has S nodes, you just need to add S - 1 edges to
     *   make the component connected (a subtree of the component), add the rest of the
     *   edges at the end when all the components are connected in themselves. 
     *   2. At the end, when all of the components are connected, add the extra edges. 
     *   3. But what about the order of the components? Its better to add larger components first
     *      so that larger numbers are repeated more. 
     *   4. What about a component in itself? Try to make a tree from that component. 
    */
    public static IList<long> MaximizeValues(int queries, string[][] datas, string[][] allFriendships)
    {
        IList<long> output = new List<long>(); 

        for (int query = 0; query < queries; query++)
        {
            string[] data = datas[query];
            int totalNodes = Convert.ToInt32(data[0]);
            int friendships = Convert.ToInt32(data[1]);

            var map = new GraphNode[totalNodes];
            var nodes = new HashSet<GraphNode>();

            for (int node = 0; node < totalNodes; node++)
            {
                map[node] = new GraphNode(node.ToString());
                nodes.Add(map[node]);
            }

            var edges = new HashSet<Edge>();

            var friendship = allFriendships[query];

            for (int i = 0; i < friendships; i++)
            {
                string[] relationship = friendship[i].Split(' '); 

                var edge = new Edge();

                edge.id_1 = map[Convert.ToInt32(relationship[0]) - 1];
                edge.id_2 = map[Convert.ToInt32(relationship[1]) - 1];

                edges.Add(edge);

                edge.id_1.edges.Add(edge);
                edge.id_2.edges.Add(edge);
            }
            // end of process input 

            var groups = new List<Group>();

            // use stack - how to understand the stack's functionality here? 
            // write down something here - go over a test case to understand the code
            while (nodes.Count > 0)
            {
                var node = nodes.First();
                nodes.Remove(node);

                groups.Add(node.group = new Group());

                var neighbours = new Stack<GraphNode>();
                node.Connect(nodes, edges, neighbours);

                while (neighbours.Count > 0)
                {
                    GraphNode current = neighbours.Pop();
                    current.Connect(nodes, edges, neighbours);
                }
            }

            long result = 0;
            long sum = 0;

            foreach (var edge in groups.OrderByDescending(g => g.totalNumberNodes))
            {
                for (int i = 1; i < edge.totalNumberNodes; i++)
                {
                    result += (i + 1) * (long)i + sum;
                }

                sum += (long)edge.totalNumberNodes * (edge.totalNumberNodes - 1);
            }

            output.Add(result + edges.Count * sum);
        }

        return output; 
    }
}