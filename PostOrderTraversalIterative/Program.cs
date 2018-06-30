using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostOrderTraversalIterative
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

            List<int> result = postOrderTraversal(node1); 

        }

        public static List<int> postOrderTraversal(TreeNode root)
        {
            if (root == null)
                return null;
            TreeNode runner = root;

            List<int> postTraversal = new List<int>(); 
            Stack<TreeNode> stack = new Stack<TreeNode>(); 

            while (true)
            {
                while (runner != null)
                {
                    stack.Push(runner);
                    runner = runner.left; 
                }

                while(stack.Count > 0 && (stack.Peek().right == null || stack.Peek().right == runner))
                {
                    TreeNode node = (TreeNode)stack.Pop(); // bug - causing out of stack - should be runner = (TreeNode)stack.Pop()
                    postTraversal.Add(node.val); 
                }

                if(stack.Count == 0)
                    break; 
                else 
                    runner = stack.Peek().right; 
            }

            return postTraversal; 
        }

    }
}
