using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertTreeToList_codereview1
{
    /*
     * http://codereview.stackexchange.com/a/74017/123986
     * 
     * Julia is working on code review in January 22, 2017
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
            // This makes the structure of the tree you're creating more visible 
            // in the code and it also repeats itself less.
            TreeNode root = new TreeNode(0)
            {
                Left = new TreeNode(2)
                {
                    Left = new TreeNode(6),
                    Right = new TreeNode(8)
                },
                Right = new TreeNode(4)
                {
                    Right = new TreeNode(10)
                }
            };

            IEnumerable<int> res = ConvertTreeToList(root);
        }

        /* Empty list should be represented by an empty list, not by null. 
         * This would simplify your code, since you wouldn't have to check 
         * for nulls before invoking the method recursively.
         * 
         */
        private static IEnumerable<int> ConvertTreeToList(TreeNode root)
        {
            if (root == null)
                return Enumerable.Empty<int>();

            return new[] { root.Index }
                .Concat(ConvertTreeToList(root.Left))
                .Concat(ConvertTreeToList(root.Right));
        }
    }
}
