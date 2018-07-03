using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ternaryTreeTraversal
{
    /*
        C++ code - problem statement - August 2015 
     *  node* tnode {
           node* left;
           node* right;
           node* middle;
           int data;
        };
     
        void preOrderTraversal(node* rootNode)
        {
          // print the values of the ternary search tree by doing preorder traversal
  
        }
     */

    /*
     * Julia's comment: 
     * August 25, 2015 
     * Spend 10 minutes to write
     * 
     * Write a function to do a pre-order traversal of this ternary search tree (TST).
        /*
          < left
          = middle
          > right 

        Preorder:  root first, then, left, middle,  right
        1, 2,2, 3

        1 -> root
        1.right = 2
        1.right.middle =2 
        1.right.right = 3

        Now, I need to go traversal:

        Preordre traversal:
        1, 2, 2, 3

            1
               2 
               2  3
        preorder output: 1 2 2 3 

        test case:
        3, 2, 2, 1

        3-> root, 
        3.left = 2
        3.left.middle = 2
        3.left.left = 1; 

            3
          2 
        1 2

        preorder output: 3 3 1 2 
        3
     
     * root-> middle->left->right (order makes sense),  vs root ->left->middle->right (ternary tree)
       root->left->right
           
     */
    public class tNode
    {
        public tNode left;
        public tNode right;
        public tNode middle;
        public int data;

        public tNode(int val)
        {
            data = val;
        }

        /* 
        * print the values of the ternary search tree by doing preorder traversal
        * January 5,  2016
          C#
       
         * Review unfinished code written on August 25, 2015, and then, write a new one. 
         * 
         * common problem: 
         * 1. problem 1: 
         * in the function, only call print function once. 
         * Let recursive function handle the children. 
         * 2. problem 2: 
         * base case: discussion of null pointer, so 
         * no need to check if child pointer is not null 
         * if(r.middle !=null)   <-- problem 2
         * 
       */
        public static void preOrderTraversal(tNode r)
        {
            if (r == null)
                return;

            Console.WriteLine(r.data);      // fisrt node     

            preOrderTraversal(r.middle);    // Problem 2:
            preOrderTraversal(r.left);      // Problem 2: 
            preOrderTraversal(r.right);     // Problem 2: 
        }

        /*
         * August 25, 2015
         * The function was written with a few of problems: 
         * 
         * Problem 1: 
         * in the function, only call print function once for root node, and then, 
         * let recursive function handle the children. 
         * Ensure that any node is printed once, not more than once. 
         
         * Problem 2: 
         * base case: discussion of null pointer, so 
         * no need to check if child pointer is not null 
         * if(r.middle !=null)   <-- problem 2        
         * 
         * problem 3:                      
         *  redudant printout of nodes
         *  
         * problem 4: 
         * too many printout function calls
         * 
         * problem 5: 
         * recursive function is missing here! 
         * 
         * problem 6:      
         * So, time out! the function was left with a few problems:
         * 
         * Situation/ Task, Action, Result - SAR structure:
         * 1. Situation/ Task: under pressure, perform a recursive function 
         * 2. Action: Use test case to get the idea, and then, write code; 
         *    but, there are a few of problems in recursive function construction
         * 3. Result: 
         *    The recursive function with a few of serious bugs
         *    1. base case - ok
         *    2. recursive function calls are missing 
         *    3. action - output are duplicated, root node, and also child nodes are outputed in one function
         *    
         * Lessons learned about recursive solution (January 6, 2016): 
         *    1. Learn how to think using recursive solution 
         *    2. Always fully use "base case" checking, do not repeat the same checking after base case checking.               
         *    3. Only take action for root node, and then, let recursive function do the work for its children. 
         *    
         * But, remember that recursive solution is the most simple solution to write, short and concise; but 
         * may not be most efficient, maybe a lot of redundant recursive function calls.  
         * 
         */
        public static void preOrderTraversal_Trial(tNode r)
        {
            if (r == null)
                return;

            Console.WriteLine(r.data);

            if (r.middle != null)    // Problem 2:
            {
                Console.WriteLine(r.middle.data); // Problem 3
                preOrderTraversal_Trial(r.middle);
            }

            if (r.left != null)   // Problem 2:
                Console.WriteLine(r.left.data);  // Problem 3
            // problem 5

            if (r.right != null)    // Problem 2:
                Console.WriteLine(r.right.data); // Problem 3
            // Problem 5    
        }
    };

    public class ternaryTreeTraversal
    {
        static void Main(string[] args)
        {
            tNode n1 = new tNode(1);
            n1.right = new tNode(2);
            n1.right.middle = new tNode(2);
            n1.right.right = new tNode(3);

            tNode.preOrderTraversal(n1);

            Console.WriteLine("second test case");

            tNode n2 = new tNode(4);
            n2.left = new tNode(2);
            n2.left.left = new tNode(1);
            n2.left.middle = new tNode(2);
            n2.left.right = new tNode(3);
            n2.right = new tNode(5);

            tNode.preOrderTraversal(n2);
        }
    }
}
