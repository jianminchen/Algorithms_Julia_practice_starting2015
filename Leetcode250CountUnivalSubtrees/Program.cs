using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode250_CountUniValueSubtrees
{
    /// <summary>
    /// Leetcode 250: Count univalue subtrees
    /// 
    /// </summary>
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int number)
        {
            Value = number;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase();
            RunTestcase2();
        }

        public static void RunTestcase()
        {
            var root1 = new Node(1);
            var root2B = new Node(1);
            root1.Left = root2B;
            root1.Right = new Node(2);
            
            var result = countUnivalSubtrees(root1);
            Debug.Assert(result == -2);
        }

        public static void RunTestcase2()
        {
            var root1 = new Node(1);
            var root2B = new Node(1);
            root1.Left = root2B;
            root1.Right = new Node(1);            
            
            var result = countUnivalSubtrees(root1);
            Debug.Assert(result == 3);
        }

        /// <summary>
        /// return negative if the tree does not have same value; absolute value
        /// is the count of unival subtrees. 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int countUnivalSubtrees(Node root)
        {
            if (root == null)
            {
                return 0;
            }

            int leftCheck  = countUnivalSubtrees(root.Left);
            int rightCheck = countUnivalSubtrees(root.Right);
            var hasSameValue = false;

            if(leftCheck >= 0 && rightCheck >=0)
            {
                if((root.Left  == null || root.Left.Value == root.Value) &&
                   (root.Right == null || root.Right.Value == root.Value))
                {
                    hasSameValue = true; 
                }
            }

            var childrenCount = Math.Abs(leftCheck) + Math.Abs(rightCheck);
            if (hasSameValue)
            {
                return 1 + childrenCount;
            }
            else
                return -1 * childrenCount;
        }
    }
}