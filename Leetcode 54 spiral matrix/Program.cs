using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSpiralPrint
{
    class Program
    {
        /// <summary>
        /// Leetcode Spiral Matrix
        /// https://leetcode.com/problems/spiral-matrix/description/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var spiral = MatrixSpiralPrint(new int[,] { { 1, 2, 3 }, { 8, 9, 4 }, { 7, 6, 5 } });
            Debug.Assert(String.Join("", spiral).CompareTo("123456789") == 0);
        }

        /// <summary>            
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] MatrixSpiralPrint(int[,] array)
        {
            if (array == null || array.GetLength(0) == 0 || array.GetLength(1) == 0)
            {
                return new int[0];
            }

            int rows = array.GetLength(0);
            int columns = array.GetLength(1);
            
            int index = 0;
            int totalNumbers = rows * columns;
            
            var visited = new bool[rows,columns];
            
            int row = 0;
            int col = 0;

            int directionRow = 0;
            int directionCol = 1; 

            var spiral = new int[totalNumbers];

            while (index < totalNumbers)
            {
                var current = array[row, col];

                spiral[index++] = current;
                visited[row, col] = true; 

                var nextRow = (row + directionRow);
                var nextCol = (col + directionCol);

                var isOutOfRange = nextRow < 0 || nextCol < 0 || nextRow >= rows || nextCol >= columns;
                
                if (isOutOfRange || visited[nextRow, nextCol])
                {
                    var tmp = directionRow;
                    directionRow = directionCol;
                    directionCol = -1 * tmp;                    
                }

                row = row + directionRow;
                col = col + directionCol;                 
            }

            return spiral;
        }
    }
}