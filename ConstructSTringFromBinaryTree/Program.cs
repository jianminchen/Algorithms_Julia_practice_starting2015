using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructSTringFromBinaryTree
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) { val = x; }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }

        public string Tree2str(TreeNode t)
        {
            if(t == null)
            {
                return string.Empty; 
            }

            bool leftIsNull = t.left == null;
            bool rightIsNull = t.right == null;
            bool noChild = leftIsNull && rightIsNull; 

            if(noChild)
            {
                return t.val.ToString();
            }
            else if(leftIsNull)
            {
                return t.val + "()" + "(" + Tree2str(t.right) + ")";
            }
            else if(rightIsNull)
            {
                return t.val + "(" + Tree2str(t.left) + ")";
            }
            else
            {
                return t.val + "(" + Tree2str(t.left) + ")" + "(" + Tree2str(t.right) + ")";
            }
        }
    }
}
