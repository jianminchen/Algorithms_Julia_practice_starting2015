using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindLargestSmallerBSTKey
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public class Node
        {
            public int  Key;
            public Node left;
            public Node right;
            public Node parent;
        }

        public static int findLargestSmallerKey(Node rootNode, int num)
        {           
            if (rootNode.right != null && rootNode.Key < num)
            {
                var r = findLargestSmallerKey(rootNode.right, num);
                if (r != -1)
                    return r;
                else
                    return rootNode.Key;
            }
            else
            {
                return findLargestSmallerKey(rootNode.left, num);
            }
        }
  
    }
}
