using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertTreeToList_codereview1
{
    /*
     * http://codereview.stackexchange.com/a/73642/123986
     */
    class TreeNode
    {
        public int Index; 
        public TreeNode Left, Right; 

        public TreeNode(int value)
        {
            this.Index = value; 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //            0 
            //        2         4
            //     6    8          10
            TreeNode root = new TreeNode(0);
            root.Left  = new TreeNode(2);
            root.Right = new TreeNode(4);
            root.Left.Left  = new TreeNode(6);
            root.Left.Right = new TreeNode(8);

            root.Right.Right = new TreeNode(10);

            List<int> res = ConvertTreeToList(root);
        }

        private static List<int> ConvertTreeToList(TreeNode root)
        {
            var result = new List<int>();
            ConvertTreeToList(root, result);
            return result;
        }

        private static void ConvertTreeToList(TreeNode root, List<int> result)
        {
            if (root == null)
            {
                return;
            }

            result.Add(root.Index);
            ConvertTreeToList(root.Left, result);
            ConvertTreeToList(root.Right, result);
        }
    }
}
