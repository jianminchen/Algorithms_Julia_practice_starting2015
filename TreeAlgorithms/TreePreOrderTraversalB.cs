using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreePreOrderTraversal
{
    class TreePreOrderTraversalB
    {
        static void Main(string[] args)
        {
            /**
             * test case: 
             *   Tree
             *      1
             *    2   3
             *  4  5 6  7
             *  Pre order Traversal: 1 2 4 5 3 6 7
             */
            var node1 = new Node();
            var node2 = new Node();
            var node3 = new Node();
            var node4 = new Node();
            var node5 = new Node();
            var node6 = new Node();
            var node7 = new Node();

            node1.value = 1;
            node2.value = 2;
            node3.value = 3;
            node4.value = 4;
            node5.value = 5;
            node6.value = 6;
            node7.value = 7;

            node1.left = node2;
            node1.right = node3;

            node2.left = node4;
            node2.right = node5;

            node3.left = node6;
            node3.right = node7;

            Console.WriteLine("PreorderTraversal");

            PreorderTraversal(node1);

            Console.WriteLine("PreorderTraversalIterative");
            IList<int> list = preorderTraversalIterative(node1);

            foreach (int s in list)                           
                Console.WriteLine(s);

            Console.WriteLine("Preorder Traversal using queue");
            traverse_bug(node1); 
        }

        private static void PreorderTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            Console.WriteLine(node.value.ToString());

            PreorderTraversal(node.left);
            PreorderTraversal(node.right);
        }

        /**
         * January 8, 2016
         * 
         * More readings:
         * 1. http://www.geeksforgeeks.org/iterative-preorder-traversal/
         * 2. http://articles.leetcode.com/2010/10/binary-tree-post-order-traversal.html
         * 3. https://dzone.com/articles/binary-tree-preorder-traversal
         * use queue to implement 
         */
        private static IList<int> preorderTraversalIterative(Node root)
        {
            IList<int> result = new List<int>();
            Node p = new Node();

            Stack s = new Stack();

            p = root;
            if (p != null) s.Push(p);

            while (s.Count > 0)
            {
                p = (Node)s.Peek();

                s.Pop();

                result.Add(p.value);

                if (p.right != null) 
                    s.Push(p.right);

                if (p.left != null) 
                    s.Push(p.left);
            }

            return result;
        }

        /*
         * January 8, 2016
         * 
         * Use queue instead of stack 
         *   https://dzone.com/articles/binary-tree-preorder-traversal
         *   
         * use queue to implement 
         * 
         * The code does not work, the output is 
         * 1 2 4 3 6 5 7 
         */
        public static void traverse_bug(Node root) {
            Queue<Node> queue = new Queue<Node>();

            pushAllLeft(queue, root);
            while (queue.Count > 0 ) {
                Node node = queue.Peek();

                queue.Dequeue(); 

                Console.WriteLine(node.value);

                pushAllLeft(queue, node.right);
            }
        }

        private static void pushAllLeft(Queue<Node> queue, Node node)
        {
            while (node != null)
            {
                queue.Enqueue(node);
                node = node.left;
            }
        }

        internal class Node
        {
            public int value { get; set; }
            public Node left { get; set; }
            public Node right { get; set; }
            public override string ToString()
            {
                return value.ToString();
            }
        }
    }
}
