using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreePostOrderTraversal
{
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

    class TreePostOrderTraversal
    {
        /*
         *    
         *         1
         *     /      \
         *    2        3
         *   / \      / \
         *  4   5    6   7
         *  
         * Post order traversal output:
         * Left, right, root
         * 
         * 4  5 2 6 7 3 1
         * 
         */
        static void Main(string[] args)
        {
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

            Console.WriteLine("PostorderTraversal");
            PostorderTraversal(node1);
 
            // June 2, 2015 
            Console.WriteLine("Post order traversal Iterative");
            postorderTraversal_Iterative(node1);

        }

        /*
         * 
         */
        private static void PostorderTraversal(Node node)
        {
            if (node == null)            
                return;            

            PostorderTraversal(node.left);

            PostorderTraversal(node.right);

            Console.WriteLine(node.value);
        }

        /** 
         * June 2, 2015 
         * https://github.com/yuzhangcmu/LeetCode/blob/master/tree/TreeDemo.java
         *  后序遍历迭代解法 
         *  
         *  从左到右的后序 与从右到左的前序的逆序是一样的，所以就简单喽！ 哈哈
         *  用另外一个栈进行翻转即可喽 
         *  
         * January 8, 2016
            1. The tip is to use two stacks, one is to push nodes in stack 1 using the order of root, right, left;
         *  2. When popping out stack 1, push node to stack 2, so the stack 2 order should be left, right, root. 
         *  One way to prove the correctness: 
         *    root, right, left   <-- the order of stack 1
         *    left, right, root   <-- reverse the above order of stack 2 
         *    
         *  The way to figure out the order of two stacks:
         *    second stack output order:
         *    left, right, root
         *    
         *    suppose that the output is from a stack, and then, the first stack should be in reverse order:
         *    root, right, left
         *    
         *    And it is easy to do when to traverse from root node of the binary tree
         *    
         *  More reading to help about using two stacks: (January 8, 2016) 
         * 1.  http://articles.leetcode.com/2010/10/binary-tree-post-order-traversal.html
         * 
         */
        public static void postorderTraversal_Iterative(Node root)
        {
            if (root == null)            
                return;            

            Stack s    = new Stack();
            Stack outS = new Stack();

            s.Push(root);
            while (s.Count > 0)
            {
                Node cur = (Node)s.Pop();
                outS.Push(cur);

                if (cur.left != null)                
                    s.Push(cur.left);
                
                if (cur.right != null)                
                    s.Push(cur.right);                
            }

            while (outS.Count > 0)
            {
                Node cur = (Node)outS.Pop();
                Console.WriteLine(cur.value + " ");
            }
        }
    }
}
