using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _236LowestCommonAncestor_Warmup
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

            TreeNode node = lowestCommonAncestor(node1, node1.left, node1.left.right.left);
        }

        public static TreeNode lowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null || p == null || q == null)
                return null;

            List<TreeNode> pPath = new List<TreeNode>();
            List<TreeNode> qPath = new List<TreeNode>();

            TreeNode runner = root;
            Stack<TreeNode> stack = new Stack<TreeNode>();

            bool stopTraversal = false; 

            while (true)
            {
                while (runner != null)
                {
                    stack.Push(runner);

                    if (pPath.Count == 0 && p == runner)
                    {
                        pPath.AddRange(stack.ToArray());
                    }

                    if (qPath.Count == 0 && q == runner)
                    {
                        qPath.AddRange(stack.ToArray());
                    }

                    if (pPath.Count > 0 && qPath.Count > 0)
                    {
                        stopTraversal = true; 
                        break; // no longer traverse the tree in post order                        
                    }

                    runner = runner.left;
                }

                if (stopTraversal)
                    break; 

                while (stack.Count > 0 && (stack.Peek().right == null || stack.Peek().right == runner))
                {
                    stack.Pop();
                }

                if (stack.Count == 0)
                {
                    break;
                }
                else
                {
                    runner = stack.Peek().right;
                }
            }

            // get the last common node in two list, from end to start
            int indexP = pPath.Count - 1;
            int indexQ = qPath.Count - 1;

            while (indexP >= 0 && indexQ >= 0 && pPath[indexP] == qPath[indexQ])
            {
                indexP--;
                indexQ--;
            }

            return pPath[indexP + 1];
        }
    }
}
