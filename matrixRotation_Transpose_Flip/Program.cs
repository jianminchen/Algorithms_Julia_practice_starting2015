using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixRotation_Transpose_Flip
{
    /*
     * Source code reference:
     * https://discuss.leetcode.com/topic/22673/using-transpose-and-flip-operations
     * First practice: January 2, 2017
     */
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase(); 
        }
        
        public static void RunTestcase()
        {
            int[][] matrix = new int[3][]; 

            for(int i = 0; i < 3; i++)
            {
                matrix[i] = new int[3]; 
            }

            string[] data = {"1 2 3", "4 5 6", "7 8 9"};

            for (int i = 0; i < 3; i++)
            {
                matrix[i] = Array.ConvertAll(data[i].Split(' '), int.Parse);  
            }

            Rotate(matrix);
             
            Debug.Assert(String.Join(" ", matrix[0]).CompareTo("7 4 1") == 0);
            Debug.Assert(String.Join(" ", matrix[1]).CompareTo("8 5 2") == 0);
            Debug.Assert(String.Join(" ", matrix[2]).CompareTo("9 6 3") == 0);

        }

        /*
         * Transpose - flip only left diagonal cells
         *  i <-> n - j - 1
         *  j <-> n - i - 1 
         *  
         */
        private static void Transpose(int[][] matrix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i + j < n - 1)
                    {   
                        // flip only left diagonal cells
                        int temp = matrix[i][j];
                        int swap_row = n - j - 1;
                        int swap_col = n - i - 1;

                        matrix[i][j] = matrix[swap_row][swap_col];
                        matrix[swap_row][swap_col] = temp;
                    }
                }
            }
        }

        /*
         * row i and row n - i - 1, swap for two nodes on the same column. 
         * 
         */
        private static void FlipVertically(int[][] matrix, int n)
        {
            for (int i = 0; i < (n + 1) / 2; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int temp = matrix[i][j];
                    int swap_row = n - i - 1;
                    matrix[i][j] = matrix[swap_row][j];
                    matrix[swap_row][j] = temp;
                }
            }
        }

        /*
         * Rotate clockwise 90 degree
         * 
         */
        public static void Rotate(int[][] matrix)
        {
            int n = matrix.Length;
            Transpose(matrix, n);
            FlipVertically(matrix, n);
        }        
    }
}
