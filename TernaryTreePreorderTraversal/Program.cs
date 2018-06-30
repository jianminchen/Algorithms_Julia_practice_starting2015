    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace TernaryTreePreorderTraversal
    {
        class TernaryTree
        {
            static void Main(string[] args)
            {
                RunTestcase(); 
            }

            /*
             * Ternary tree: 
             *      1
             *    / | \
             *   3  2  5
             *  /|  |
             * 4 33 22
             */
            public static void RunTestcase()
            {
                var root = new TernaryTree(1);

                root.Middle = new TernaryTree(2);
                root.Left   = new TernaryTree(3);
                root.Right  = new TernaryTree(4);

                root.Middle.Middle = new TernaryTree(22);
                root.Left.Middle   = new TernaryTree(33);
                root.Right = new TernaryTree(5);

                TernaryTree.PreOrderTraversal(root); 

                // manually verify the console output is 1 2 22 3 33 4 5
            }

            public TernaryTree Left   { get; set; }
            public TernaryTree Right  { get; set; }
            public TernaryTree Middle { get; set; }
            public int Data { get; set; }

            public TernaryTree(int val)
            {
                Data = val;
            }
           
            public static void PreOrderTraversal(TernaryTree node)
            {
                if (node == null)
                {
                    return;
                }

                Console.WriteLine(node.Data);        

                PreOrderTraversal(node.Middle);     
                PreOrderTraversal(node.Left);       
                PreOrderTraversal(node.Right);     
            }
        }    
    }
