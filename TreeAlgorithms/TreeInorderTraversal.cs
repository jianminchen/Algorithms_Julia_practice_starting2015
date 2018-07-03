using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeInorderTraversal
{
    /*
     * January 8, 2016
     *
     *  Binary tree in order traversal 
     *  
     *  Review the solution:
     */
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

    /*
     * 
     *         1
     *       /   \
     *    2        3
     *   / \      / \
     *  4   5    6   7
     *  
     * Inorder traversal output:
     *   *     1
     *       /   \
     *    2         3
     *   / \       / \
     * 4    5     6   7
     * 
     * |  | |  |  | | |
     * 4  2 5  1  6 3 7 
     * 
     */
    class TreeInorderTraversal
    {
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

            Console.WriteLine("InorderTraversal");

            InorderTraversal(node1);

            // June 3, 2015
            Console.WriteLine("In order traversal Iterative");
            inorderTraversalIterative(node1);

            // June 11, 2015
            Console.WriteLine("In order traversal Iterative - method B");
            inOrderIterative_B(node1);           
        }

        private static void InorderTraversal(Node node)
        {
            if (node == null)
            {
                return;
            }

            InorderTraversal(node.left);

            Console.WriteLine(node.value);

            InorderTraversal(node.right);
        }


        /** 
        * https://github.com/yuzhangcmu/LeetCode/blob/master/tree/TreeDemo.java
        * http://blog.csdn.net/fightforyourdream/article/details/16843303
       * 中序遍历迭代解法 ，用栈先把根节点的所有左孩子都添加到栈内， 
       * 然后输出栈顶元素，再处理栈顶元素的右子树 
       *  
       *  
       * 还有一种方法能不用递归和栈，基于线索二叉树的方法，较麻烦以后补上 
       * http://www.geeksforgeeks.org/inorder-tree-traversal-without-recursion-and-without-stack/ 
          * * *  Julia comment on June 1, 2015: 
          *  如何思考这个问题:
          *  1. 第一个点应该访问, 就是从根节点出发,　找左孩子,　直到最左边的点; 
          *  2. 如何找到第一个点 N1?
          *     2.1 从根节点出发，设为当前点;　     　　　 
          *     2.2 把非空的当前点入堆栈,　更新当前点为左的孩子,循环,　直到左边节点为空, 节点 N1; 
         *     2.3 出栈,　该点输出；　
          *     2.3 接下来, 把右孩子设为当前点; (对第一个点N1来说, 左孩子为空, 右孩子入栈; 所有的孩子都考虑了!)
        *    3. 如何想到设计一个栈,　一个当前节点，能够解决问题?
       */
        public static void inorderTraversalIterative(Node root)
        {
            if (root == null)
            {
                return;
            }

            Stack s = new Stack();

            Node cur = root;

            while (true)
            {
                // 把当前节点的左节点都push到栈中.
                while (cur != null)
                {
                    s.Push(cur);
                    cur = cur.left;
                }

                if (s.Count == 0)
                {
                    break;
                }

                // 因为此时已经没有左孩子了，所以输出栈顶元素 
                cur = (Node)s.Pop();
                Console.WriteLine(cur.value + " ");

                // 准备处理右子树  
                cur = cur.right;
            }
        }

        /**
         * Latest update: June 11, 2015
         * source code idea from the website: 
         * https://leetcodenotes.wordpress.com/2013/08/04/classic-%E6%A0%91%E7%9A%84%E5%89%8D%E5%BA%8F%E3%80%81%E4%B8%AD%E5%BA%8F%E3%80%81%E5%90%8E%E5%BA%8F%E7%9A%84iteration/
         * Very clear description in Chinese:
           中序 in order （左中右）：
           push root左支到死
           pop， 若pop的有right，则把right当root对待push它的左支到死。
           这样继续一边pop一边push。直到stack为空。
         */
        public static void inOrderIterative_B(Node root)
        {
            if (root == null)
                return;

            Stack s = new Stack();

            pushAllTheyWayAtLeft(s, root);

            while (s.Count > 0)
            {
                Node top = (Node)s.Pop();

                System.Console.WriteLine(top.value + ", ");

                if (top.right != null)
                    pushAllTheyWayAtLeft(s, top.right);
            }
        }

        /*
         * the function: 
         * pushAllTheyWayAtLeft
         * 
         * Easy to tell that there is no problem in the code
         * Just come out the name properly, use it; write it later. 
         */
        private static void pushAllTheyWayAtLeft(Stack s, Node root)
        {
            while (root != null)
            {
                s.Push(root);
                root = root.left;
            }
        }
    }
}
