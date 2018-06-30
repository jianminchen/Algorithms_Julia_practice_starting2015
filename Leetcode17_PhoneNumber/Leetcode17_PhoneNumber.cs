using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _17LetterCombinationOfAPhoneNUmber_usingDFS_Stack
{
    /*
     * Leetcode 17:
     * Given a digit string, return all possible letter combinations that 
     * the number could represent.
       A mapping of digit to letters (just like on the telephone buttons) is given below.
     * Input:Digit string "23"
       Output: ["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"].
     * 
     * Telephone dial pad:
     *                     0  1   2     3     4     5     6     7      8     9 
     * String[] keyboard={"","","abc","def","ghi","jkl","mno","pqrs","tuv","wxyz"};
     */
    class Solution
    {
        public class Node
        {
            public string PhoneNumber;
            public int    Length;

            public Node(string phoneNumber, int length)
            {
                this.PhoneNumber = phoneNumber;
                this.Length = length; 
            }
        }

        static void Main(string[] args)
        {
            RunSampleTestcase();             
        }

        /*
         * test result: "abc", "def", so 3x3 = 9 cases. 
         * ad, ae, af, bd, be, bf, cd, ce, cf
         */
        public static void RunSampleTestcase()
        {
            IList<string> phoneNumbers = letterCombination("234");
            Debug.Assert(phoneNumbers.Count == 9); 
        }

        /*
         * @digits - 
         */
        public static IList<string> letterCombination(string digitNumbers)
        {
            IList<string> list = new List<string>();
            if (digitNumbers == null || digitNumbers.Length == 0)
                return list;

            string[] keyboard = { "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };

            PhoneNumbersUsingDFS(list, keyboard, digitNumbers);  

            return list;
        }

        /*
         * @list
         * @keyboard
         * @numbers - such as "123"
         *  
         */
        private static void PhoneNumbersUsingDFS(
            IList<string> list,
            string[]      keyboard,
            string        numbers
            )   
        {            
            if (numbers == null || numbers.Length == 0)
                return;

            int phoneNumberLength = numbers.Length; 

            Stack<Node> stack = new Stack<Node>();
            int digit = numbers[0] - '0';

            foreach (char c in keyboard[digit])
            {
                Node node = new Node(digit.ToString(), 1);
                stack.Push(node);
            }

            while (stack.Count > 0)
            {
                Node partialNumbers = (Node)stack.Pop();

                string phoneNumber = partialNumbers.PhoneNumber;
                int    length      = partialNumbers.Length;

                if (length < phoneNumberLength)
                {
                    int nextDigit = numbers[length] - '0';
                    foreach (char c in keyboard[nextDigit])
                    {
                        string addOneMore = phoneNumber + c.ToString();

                        Node newNode = new Node(addOneMore, length + 1);

                        stack.Push(newNode);
                    }
                }
                else if (length == phoneNumberLength)
                {
                    list.Add(phoneNumber);
                }
            }

            return;
        }
    }
}