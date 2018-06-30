using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowestCommonAncestorWarmup
{
    public class TreeNode
    {
        public TreeNode left;
        public TreeNode right;
        public int val;

        public TreeNode(int v)
        {
            val = v;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TreeNode node1 = new TreeNode(9);
            node1.left = new TreeNode(5);
            node1.right = new TreeNode(8);
            node1.left.left = new TreeNode(1);
            node1.left.right = new TreeNode(4);
            node1.left.right.left = new TreeNode(2);
            node1.left.right.right = new TreeNode(3);

            node1.right.right = new TreeNode(8);
            node1.right.right.left = new TreeNode(6);

            TreeNode node = findLCA(node1, node1.left, node1.left.right.left);
            TreeNode nd2 = findLCA(node1, node1.left.right.right, node1.right.right);
            TreeNode nd3 = findLCA(node1, node1.left.left, node1.left.right.right); 
        }

        public static TreeNode findLCA(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null || p == null || q == null)
                return null;

            List<TreeNode> pPath = new List<TreeNode>();
            List<TreeNode> qPath = new List<TreeNode>();

            if(!findPath(root, p, pPath) ||
               !findPath(root, q, qPath))
                return null; 
            
            // Find last common node

            int index = 0; 
            while(index < pPath.Count && index < qPath.Count && pPath[index] == qPath[index])
            {
                index++; 
            }

            return pPath[index - 1]; 

        }

        /*
         * reasoning 4 return statements: 
         * 1. root == null, return false - why it is correct? 
         * 2. if(root == search) return true - why it is correct? 
         * 3. check left child or right child, if any one of them is true, then return true
         * 4. backtracking the path List variable at the last node, then return false. 
         * 
         */
        private static bool findPath(TreeNode root, TreeNode search, List<TreeNode> path)
        {
            if (root == null)
                return false;

            path.Add(root);

            if(root == search)                           
                return true;

            if (findPath(root.left, search, path) || findPath(root.right, search, path))
                return true;

            path.RemoveAt(path.Count -1);

            return false; 
        }
    }
}
