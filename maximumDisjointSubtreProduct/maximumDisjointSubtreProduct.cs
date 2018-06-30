using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{   

    internal class Node
    {
        public int Id1 { get; set; }
        public int Id2 { get; set; }

        public Node(int id1, int id2)
        {
            Id1 = id1;
            Id2 = id2; 
        }
    }

    internal class TreeNode
    {
        public int Id { get; set; }
        public IList<TreeNode> Children { get; set; }
        public int Weight { get; set; }
        public static int[] Weights { get; set; }

        public TreeNode(int id)
        {
            Id = id;           
        }

        public void AddWeight(int[] weightsData)
        {
            Weights = weightsData; 
        }

        public bool IsInTree(int id1)
        {
            if(id1 == Id)
            {
                return true; 
            }

            if(Children == null)
            {
                return false; 
            }

            foreach(var child in Children)
            {
                if (child.IsInTree(id1))
                {
                    return true; 
                }
            }

            return false; 
        }        

        public void Add(int searchId, int addId)
        {
            if(Id == searchId)
            {
                if(this.Children == null)
                {
                    this.Children = new List<TreeNode>(); 
                }

                this.Children.Add(new TreeNode(addId)); 
            }
            else if(this.Children != null)
            {
                foreach(var child in this.Children)
                {
                    child.Add(searchId, addId); 
                }
            }
        }

        /// <summary>
        /// just cheat and see if I can make any points here - 8:50pm 4/29/2017
        /// Still missing something - not just children 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public long CalculateMaximumWeightEstimated(TreeNode node, HashSet<long> weightsInTraverse, long baseValue)
        {
            if(node == null)
            {
                return 0; 
            }

            int weightConsidered = TreeNode.Weights[node.Id];
            bool isPositive = weightConsidered > 0;
            long maximumSum = weightConsidered;

            weightsInTraverse.Add(maximumSum); //  1
            weightsInTraverse.Add(baseValue + maximumSum); 

            if (node.Children == null)
            {               
                return maximumSum; 
            }

            var addTogether = baseValue + maximumSum; 
            foreach(var child in node.Children)
            {
                weightsInTraverse.Add(TreeNode.Weights[child.Id]); // 2

                var sum = CalculateMaximumWeightEstimated(child, weightsInTraverse, baseValue + maximumSum);

                CalculateMaximumWeightEstimated(child, weightsInTraverse,  maximumSum);
                weightsInTraverse.Add(sum); // 3
                
                bool secondIsPositive = sum > 0; 
                
                maximumSum += sum;
                weightsInTraverse.Add(maximumSum); // 4
                addTogether += sum;

                weightsInTraverse.Add(addTogether); 
            }

            weightsInTraverse.Add(addTogether); 

            return maximumSum; 
        }
    }

    internal class WeightSet
    {
        public int ReprenstativeID { get; set; }
        public long NegativeMinimumWeight { get; set; }
        public long PositiveMaximumWeight { get; set; }

    }

    static void Main(String[] args)
    {
        ProcessInput(); 
        //RunTestcase(); 
    }

    public static void RunTestcase()
    {
        int n = 6;

        int[] weights = new int[] {-9, -6, -1, 9, -2, 0};

        IList<Node> edges = new List<Node>();
        int[][] edgeInfo = new int[5][];

        edgeInfo[0] = new int[] {6,1 };
        edgeInfo[1] = new int[] { 4, 5 };
        edgeInfo[2] = new int[] { 6, 3 };
        edgeInfo[3] = new int[] { 5, 2 };
        edgeInfo[4] = new int[] { 1, 2 };       

        for (int index = 0; index < n - 1; index++)
        {
            edges.Add(new Node(edgeInfo[index][0]-1, edgeInfo[index][1]-1));
        }

        var maximumProduct = CalculateMaximumProduct(n, weights, edges); 
    }

    public static void ProcessInput()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        // The respective weights of each node:
        string[] w_temp = Console.ReadLine().Split(' ');
        int[] weights = Array.ConvertAll(w_temp, Int32.Parse);

        IList<Node> edges = new List<Node>();

        for (int index = 0; index < n - 1; index++)
        {
            int[] edgeInfo = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            // Write Your Code Here
            edges.Add(new Node(edgeInfo[0]-1, edgeInfo[1]-1));
        }

        var maximumProduct = CalculateMaximumProduct(n, weights, edges);

        Console.WriteLine(maximumProduct); 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="weights"></param>
    /// <param name="edges"></param>
    /// <returns></returns>
    public static long CalculateMaximumProduct(int n, int[] weights, IList<Node> edges)
    {
        long maximumProduct = long.MinValue;         
        
        foreach(var edge in edges)
        {
            int id1 = edge.Id1;
            int id2 = edge.Id2; 

            // separate the graph to break the edge into two parts
            // from each node to do BFS graph search, find maximum weight sum and minimum weight sum 

            var weightSet1 = new WeightSet(); 
            weightSet1.ReprenstativeID = id1; 

            var weightSet2 = new WeightSet(); 
            weightSet2.ReprenstativeID = id2;

            RunBreathFirstSearch(edges, edge, id1, weights, ref weightSet1);

            RunBreathFirstSearch(edges, edge, id2, weights, ref weightSet2);

            long negativeOne = weightSet1.NegativeMinimumWeight * weightSet2.NegativeMinimumWeight;
            long positiveOne = weightSet1.PositiveMaximumWeight * weightSet2.PositiveMaximumWeight;

            maximumProduct = negativeOne > maximumProduct ? negativeOne : maximumProduct;
            maximumProduct = positiveOne > maximumProduct ? positiveOne : maximumProduct; 
        }

        return maximumProduct; 
    }

    /// <summary>
    /// Need to work on something, at least make a few points if possible
    /// Do not think about time complexity 
    /// Do not think about space complexity
    /// Do not think about too much 
    /// Naive solution 
    /// Brute force, get some points first
    /// </summary>
    /// <param name="edges"></param>
    /// <param name="edge"></param>
    /// <param name="representativeId"></param>
    /// <param name="weightInfo"></param>
    public static void RunBreathFirstSearch(IList<Node> edges, Node edge, int representativeId, int[] weight, ref WeightSet weightInfo )
    {        
        var rootNode = new TreeNode(representativeId);
        
        rootNode.AddWeight(weight); 

        int idSelected = representativeId;
        int idNotSelcted = (edge.Id1 == idSelected) ? edge.Id2 : edge.Id1;

        bool touchOnce = false;
        do
        {
            touchOnce = false;
            foreach (var item in edges)
            {
                int id1 = item.Id1;
                int id2 = item.Id2;

                if (rootNode.IsInTree(id1) != rootNode.IsInTree(id2) && cannotFind(id1, id2, idNotSelcted))
                {
                    var searchId = rootNode.IsInTree(id1) ? id1 : id2;
                    var addId = rootNode.IsInTree(id1) ? id2 : id1;

                    rootNode.Add(searchId, addId);
                    touchOnce = true;
                }
            }
        } while (touchOnce);

        var weightsInTraverse = new HashSet<long>();
        rootNode.CalculateMaximumWeightEstimated(rootNode, weightsInTraverse, 0);
        weightInfo.PositiveMaximumWeight = getPostiveMaximum(weightsInTraverse);
        weightInfo.NegativeMinimumWeight = getNegativeMaximum(weightsInTraverse);        
    }

    private static long getPostiveMaximum(HashSet<long> data)
    {
        long result = 0; 
        foreach(var item in data)
        {
            if (item <= 0) continue;
            result = (item > result) ? item : result; 
        }

        return result; 
    }

    private static long getNegativeMaximum(HashSet<long> data)
    {
        long result = 0;
        foreach (var item in data)
        {
            if (item >= 0) continue;
            result = (item < result) ? item : result;
        }

        return result;
    }

    private static bool cannotFind(int id1, int id2, int idNotSelcted)
    {
        return id1 != idNotSelcted && id2 != idNotSelcted; 
    }
}
