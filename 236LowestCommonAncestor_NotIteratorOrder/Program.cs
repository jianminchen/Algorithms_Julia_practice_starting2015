using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowestCommonAncestors
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

    /*
     * May 21, 2016
     * Julia's warmup on this algorithm - C# version 
     * Tips to share:
     * 1. Stack, always check stack is not empty before calling peek
     * 2. Peek method is good, you can retrieve the information then decide to Pop or not
     * 3. Try to make the design simple! 
     * 4. Stack.Array method keeps the order of iterator 
     */
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
            Stack<TreeNode> stack = new Stack<TreeNode>();

            TreeNode runner = root;
            List<TreeNode> pPath = new List<TreeNode>();
            List<TreeNode> qPath = new List<TreeNode>();

            bool stopTraversal = false; 

            while (true)
            {
                while (runner != null)
                {
                    stack.Push(runner);

                    if (runner == p && pPath.Count == 0)
                    {
                        pPath.AddRange(stack);
                    }

                    if (runner == q && qPath.Count == 0)
                    {
                        qPath.AddRange(stack);
                    }

                    if (pPath.Count > 0 && qPath.Count > 0)
                    {
                        stopTraversal = true; // warmup - added on May 21, 2016 - 9:42pm 
                        break; // terminate the traversal when both paths have been found                        
                    }

                    runner = runner.left;
                }

                if (stopTraversal)
                    break; 

                while (stack.Count > 0 && (stack.Peek().right == null || stack.Peek().right == runner))
                {
                    runner = stack.Pop();
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
            
            int pIndex = pPath.Count - 1;
            int qIndex = qPath.Count - 1;

            while (pIndex >= 0 && qIndex >= 0 && pPath[pIndex] == qPath[qIndex])
            {
                pIndex--;
                qIndex--; 
            }

            return pPath[pIndex + 1];
        }
    }
}
