using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralMessage
{
    class Program
    {
        /*
         * https://www.hackerrank.com/contests/ncr-codesprint/challenges/spiral-message
         * 11:42am start to read problem statement
         * Write a code to work on the sample test case first, we do not need to store
         * result of string, just keep counting. 
         * 3 5 
         * a##ar
         * a#aa#
         * xxwsr
         * 
         * 12:21pm 
         * 4.49/ 20 
         * test cases: 0 - 13
         * pass: 0, 11, 12, 13
         */
        static void Main(string[] args)
        {
            int[] arr = ToInt(Console.ReadLine().Split(' '));
            int rows = arr[0], cols = arr[1];

            IList<string> input = new List<string>(); 
            for(int i=0; i< rows; i++)
            {
                input.Add(Console.ReadLine().Trim()); 
            }

            Console.WriteLine(calculate(input)); 
        }

        /*
         * start: 11:53am
         * exit: 12:15
         * static analysis the code
         */
        private static int calculate(IList<string> data)
        {
            int rows = data.Count;
            int cols = data[0].Length;

            int row = 0;
            int col = 0;
            int count = 0; 

            while(row < rows && col < cols)
            {
                int startX = row, endX = rows - 1 - row;
                int startY = col, endY = cols - 1 - col; 

                // go over 4 direction
                // to right, downward, to left, to up
                // 1. to right
                bool previous = false; // alpahbetic a-z
                for (int j = startY; j < endY; j++ )
                {
                    bool current = isAlphabetic(data[row][j]);

                    if (current && !previous)
                    {                     
                        count++;                          
                    }                    
                    previous = current; 
                }

                // 2. downward
                for (int i = startX; i < endX; i++ )
                {
                    bool current = isAlphabetic(data[i][endY]);

                    if (current && !previous)
                    {
                        count++;
                    }
                    previous = current; 
                }
                
                // 3. to left 
                for (int j = endY; j > startY; j--)
                {
                    bool current = isAlphabetic(data[endX][j]);

                    if (current && !previous)
                    {
                        count++;
                    }
                    previous = current;
                }
                
                // 4. to upward
                for (int i = endX; i > startX; i--)
                {
                    bool current = isAlphabetic(data[i][startY]);

                    if (current && !previous)
                    {
                        count++;
                    }
                    previous = current;
                }
                
                row++;
                col++; 
            }

            return count; 
        }

        private static bool isAlphabetic(char c)
        {
            if ((c - 'a') >= 0 && ('z' - c) >= 0)
                return true;
            else
                return false; 
        }

        private static int[] ToInt(string[] arr)
        {
            int len   = arr.Length;
            int[] res = new int[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = Convert.ToInt32(arr[i]);
            }

            return res;
        }
    }
}
