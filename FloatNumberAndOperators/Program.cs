    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace FindMaximumNumber
    {
        /*
        Keyword:
        Given a list of float number, and 
        four operator: +, -, *, / arithmetic opeators, 
        insert an operator between each consecutive pair of numbers    
        Ask you to solve the problem:
        Find maximimum value -> greedy algorithm    
        Constraints:
        equal precedence order - 1 + 2 * 3 = 1 + 6 = 7 - No
                                 1 + 2 * 3 = 3 * 3 = 9 - Yes
        evaluation order is from left to right 
        -> scan the float number from left to right 
    
        Major hint: 
        1, 12, -3, maximum number is 1 - 12 * (-3) = 33
                1     
           /  /  \    \
         -11 13  1/12 12  -> Maximum and Minimum
        */
        class Program
        {
            static void Main(string[] args)
            {
                RunTestcase();
            }        

            public static void RunTestcase()
            {
                var input = new double[] { 1, 12, -3 };

                var result = GetMaxNumber(input);
                Debug.Assert(result[0] == 33);
            }
            /// <summary>
            /// First number in the return array is the maximum value and
            /// the second one is the minimum value.  
            /// </summary>
            /// <param name="numbers"></param>
            /// <returns></returns>
            public static double[] GetMaxNumber(double[] numbers)
            {
                if (numbers == null || numbers.Length == 0)
                {
                    return new double[] { };
                }

                return GetMaxNumberHelper(numbers, 1, new double[] { numbers[0], numbers[0] });
            }

            /// <summary>
            /// Recursive function is designed to fit for flat preference, 
            /// in other words, the calculation is processed from left to right
            /// instead of right to left. 
            /// [1, 12, -3] will be handled 1 [+,-,*,/] 12 first, and then
            /// max/min [+,-,*,/] -3
            /// </summary>
            /// <param name="numbers"></param>
            /// <param name="startIndex"></param>
            /// <param name="firstKNumbers"></param>
            /// <returns></returns>
            public static double[] GetMaxNumberHelper(double[] numbers, int startIndex, double[] firstKNumbers)
            {
                if (startIndex >= numbers.Length)
                {
                    return firstKNumbers;
                }

                var length = numbers.Length;

                var visit = numbers[startIndex];

                var fourOperations = calculateFourOperations(firstKNumbers, visit);

                var current = new double[] { fourOperations.Max(), fourOperations.Min() };

                return GetMaxNumberHelper(numbers, startIndex + 1, current);
            }

            /// <summary>
            /// calculate using four operators +, -, *, /
            /// </summary>
            /// <param name="maxMin"></param>
            /// <param name="number2"></param>
            /// <returns></returns>
            private static double[] calculateFourOperations(double[] maxMin, double number2)
            {
                var addition  = maxMin[0] + number2;
                var addition2 = maxMin[1] + number2;

                var subtraction  = maxMin[0] - number2;
                var subtraction2 = maxMin[1] - number2;

                var multiplication  = maxMin[0] * number2;
                var multiplication2 = maxMin[1] * number2;

                var division  = maxMin[0] / number2;
                var division2 = maxMin[1] / number2;

                return new double[] { addition, addition2, subtraction, subtraction2, multiplication, multiplication2, division, division2 };
            }
        }
    }