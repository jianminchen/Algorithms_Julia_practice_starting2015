using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFunction
{
    /*
     * Hackerearth.com simple function
     * https://www.hackerearth.com/problem/algorithm/simple-function/
     */
    class Program
    {
        /*
         * Use an integer array to express an integer from 1 to 1000, array size is 10. 
         * Store digit i from 1 to 9 if there is any digit i in the integer. 
         * 
         * For example, a number from 1 to 1000, 246 will be represented as int[]{0,0,1,0,1,0,1,0,0,0}.
         * 
         * Design of hashed integer: 
         * 1. Number of same digits in the integer will not be saved, the information will be losted, for example, 
         * integers 1, 11, 111 will be saved to same array {0,1,0,0,0,0,0,0,0,0}
         * 
         * 2. And also the order does not matter, 123, 213, 321 will be hashed to same array {0,1,1,1,0,0,0,0,0}
         */
        internal class HashedInteger
        {
            private static int SIZE = 10; 
            public int[] values; 

            public HashedInteger()
            {
                values = new int[SIZE]; 
            }

            /*
            * @number - a string representing an integer from 1 to 1000, 
            */
            public static HashedInteger Convert(string number)
            {                
                HashedInteger digits = new HashedInteger();
                foreach (char c in number)
                {
                    digits.values[ c- '0'] = 1;
                }

                return digits;
            }

            public static HashedInteger[] ConvertAll(string[] numbers)
            {
                if (numbers == null || numbers.Length == 0)
                    return new HashedInteger[] { };

                int length = numbers.Length;
                IList<HashedInteger> output = new List<HashedInteger>();
                foreach (string number in numbers)
                {
                    output.Add(HashedInteger.Convert(number));
                }

                return output.ToArray();
            }      

            /*              
            * Find largest digit in two numbers expressed in Digits
            * 
            * return: last digit
            */
            public static int GetLastDigit(HashedInteger firstOne, HashedInteger secondOne)
            {
                int lastDigit = 0;

                for (int i = 9; i >= 0; i--)
                {
                    if (firstOne.values[i] == 1 && secondOne.values[i] == 1)
                    {
                        lastDigit = i;
                        break;
                    }
                }

                return lastDigit;
            }
        }        

        static void Main(string[] args)
        {
            ProcessInputs();

           // RunSampleTestCase(); 
        }

        private static int RunSampleTestCase2()
        {
            return 0;
        }

        /*
         * Sample test case in problem description
         */
        private static void RunSampleTestCase()
        {
            int[] baskets = new int[] { 2, 2 };

            int numberInFirstBasket = baskets[0];
            int numberInSecondBaseket = baskets[1];

            string[] numbersInFirst = new string[] { "234526", "8345" };
            string[] numbersInSecond = new string[] { "333564", "98847675" };
            
            int count = CalculateSumOfEvenNumbers(numbersInFirst, numbersInSecond);
            int totalCount = CalculateTotalCount(numbersInFirst, numbersInSecond);

            double prob = count * 1.0 / totalCount;
            Debug.Assert((prob.ToString("#.000").CompareTo("0.750")) == 0);
        }

        /*
         * return value: probablity of even numbers compared to total pairs
         */
        private static void ProcessInputs()
        {
            int queries = Convert.ToInt32(Console.ReadLine());

            for (int no = 0; no < queries; no++)
            {
                int[] baskets = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                int numberInFirstBasket   = baskets[0];
                int numberInSecondBaseket = baskets[1];

                string[] numbersInFirst = new string[numberInFirstBasket];
                string[] numbersInSecond = new string[numberInSecondBaseket];

                for (int i = 0; i < numberInFirstBasket; i++)
                    numbersInFirst[i] = Console.ReadLine();

                for (int i = 0; i < numberInSecondBaseket; i++)
                    numbersInSecond[i] = Console.ReadLine();

                int count = CalculateSumOfEvenNumbers(numbersInFirst, numbersInSecond);

                int totalCount = CalculateTotalCount(numbersInFirst, numbersInSecond);

                double prob = count * 1.0 / totalCount;
                Console.WriteLine(prob.ToString("N3"));
            }
        }

        /*
         * 
         * Choose any number from each basket, compare two numbers to find largest digit in both numbers. 
         * If the largest digit is the even number, then add to the sum. Ruturn the value of sum of largest digit
         * of a pair number from two baskets. 
         */
        private static int CalculateSumOfEvenNumbers(string[] numbersA, string[] numbersB)
        {
            int count = 0;

            HashedInteger[] shortNumbers1 = HashedInteger.ConvertAll(numbersA);
            HashedInteger[] shortNumbers2 = HashedInteger.ConvertAll(numbersB);

            foreach (HashedInteger firstOne in shortNumbers1)
            {
                foreach (HashedInteger secondOne in shortNumbers2)
                {                    
                    int lastDigit = HashedInteger.GetLastDigit(firstOne, secondOne);

                    if (lastDigit % 2 == 0)
                        count++;
                }
            }

            return count;
        }                              
       
        private static int CalculateTotalCount(string[] numbersInFirst, string[] numbersInSecond)
        {
            return numbersInFirst.Length * numbersInSecond.Length;
        }
    }
}
