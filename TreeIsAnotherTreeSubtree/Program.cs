using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeIsAnotherTreeSubtree
{
    /// <summary>
    /// code review on June 17, 2017
    /// </summary>
    class Program
    {
        internal class Node{
            public int  Data { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        static void Main(string[] args)
        {
        }
        
        /// <summary>
        /// Function is designed to see if a tree is a subtree of the other tree.
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="subTree"></param>
        /// <returns></returns>
        public static bool IsSubtree(Node tree, Node subTree)
        {
            /* base cases */
            // Julia: subtree is null, assume that a null is valid subtree. 
            if (subTree == null)
            {                 
                return true;
            }

            // tree is empty, then, return false. 
            if (tree == null)
            {
                return false; 
            }

            // Now, let us check if subtree and tree are identical; if it is, then return true. 
            if (areIdentical(tree, subTree))
            { 
                return true;
            }

            // otherwise, continue to check subtree with tree's left and right child, ask same question. 
            return  IsSubtree(tree.Left, subTree) || IsSubtree(tree.Right, subTree);
        }
        
        /// <summary>
        /// Two trees are identical - check every node in the tree
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns></returns>
        private static bool areIdentical(Node node1, Node node2)
        {
            // base case: both trees are null, then true; 
            if (node1 == null && node2 == null)
            {                 
                return true;
            }

            // afterwards, if any one is null, then, return false; 
            if (node1 == null || node2 == null)
            { 
                
                return false;
            }
            
            // check 3 things: first, two nodes are having same value; 
            // and then, check both left child; 
            // and then, check both right child. 
            return (node1.Data == node2.Data && 
                    areIdentical(node1.Left, node2.Left) && 
                    areIdentical(node1.Right, node2.Right));
        }
    }
}
