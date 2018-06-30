﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode48RotateImage
{
    /// <summary>
    /// Leetcode 48: Rotate Image
    /// https://leetcode.com/problems/rotate-image/#/description
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase(); 
        }

        public static void RunTestcase()
        {
            var matrix = new int[4, 4] {{1,2,3,4}, {12, 1, 2, 5}, {11, 4, 3, 6}, {10, 9, 8, 7} };
            Rotate(matrix); 
        }

        public static void Rotate(int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return;
            }

            int rows = matrix.GetLength(0);
            int cols = rows;
            var rotate90 = new int[rows, cols];

            int startRow = 0;
            int startCol = 0;
            int lastRow = rows - 1;
            int lastCol = cols - 1;

            while (startRow < rows && startCol < lastCol)
            {
                // put the number in the one dimension array
                int width = lastRow - startRow + 1;
                int circle = 4 * (width - 1);
                var numbers = new int[circle];
                int index = 0;

                // top row
                for (int col = startCol; col <= lastCol; col++)
                {
                    numbers[index++] = matrix[startRow, col];
                }

                // right col 
                for (int row = startRow + 1; row <= lastRow; row++)
                {
                    numbers[index++] = matrix[row, lastCol];
                }

                // bottom row 
                for (int col = lastCol - 1; col >= startCol; col--)
                {
                    numbers[index++] = matrix[lastRow, col];
                }

                // left row 
                for (int row = lastRow - 1; row > startRow; row--)
                {
                    numbers[index++] = matrix[row, startCol];
                }

                // -------------
                // Need to put numbers back to matrix                

                // right col
                int start = 0;
                for (int row = startRow; row <= lastRow; row++)
                {
                    matrix[row, lastCol] = numbers[start++];
                }

                // bottom row 
                for (int col = lastCol - 1; col >= startCol; col--)
                {
                    matrix[lastRow, col] = numbers[start++];
                }

                // left col 
                for (int row = lastRow - 1; row >= startRow; row--)
                {
                    matrix[row, startCol] = numbers[start++];
                }

                // top row 
                for (int col = startCol + 1; col < lastCol; col++)
                {
                    matrix[startRow, col] = numbers[start++];  
                }

                startRow++;
                startCol++;
                lastRow--;
                lastCol--; 
            }
        }
    }
}