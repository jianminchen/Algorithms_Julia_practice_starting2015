using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowestCommonAncestor
{
    public class TreeNode{
        public int val; 
        public TreeNode left; 
        public TreeNode right; 

        public TreeNode(int x)
        {
            val = x; 
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }

        /*
         * Julia Chen 
         * May 19, 2016
         * Work on post order traversal, using stack; and then, search for nodes p and q; 
         * when p or q is found, copy stack content to the list, and denote it as pPath or qPath. 
         * And then, find first common node in two lists. 
         * 1. Whatever it is, the root node of tree is the 
         * last choice if p and q both are found in the tree. 
         * 2. If no more than 1 node is found in the tree, then return null
         * 
         * Idea of implementation:
         * 1. First step, let us write a rountine to do post order traversal using stack; 
         * 2. Second, add the checking to do lowest common ancestor
         * 
         * Let us work on a test code when writing the code
         * 
         *               9
         *      5              8
         *   1     4             7
         *       2   3         6
         */
        public static TreeNode lowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
                return null;

            TreeNode runner = root;
            List<TreeNode> pPath = new List<TreeNode>();
            List<TreeNode> qPath = new List<TreeNode>();

            Stack<TreeNode> stack = new Stack<TreeNode>();

            // push node runner and its left children to the end into stack 

            // pop the 
        }
    }
}
