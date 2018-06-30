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
        /// Navigate the direction automatically by checking boudary and checking 
        /// the status of element visited status. 
        /// Only one loop, perfect idea to fit in mock interview or interview 
        /// 20 minutes setting         
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] MatrixSpiralPrint(int[,] array)
        {
            if ( array == null )
            {
                return new int[0];
            }

            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            var visited = new int[rows, columns];
            int index = 0;
            int totalNumbers = rows * columns;

            var directions = new List<int[]>();

            directions.Add(new int[] { 0, 1 });     // left to right - top row
            directions.Add(new int[] { 1, 0 });     // top to down   - last column
            directions.Add(new int[] { 0, -1 });    // right to left - bottom row
            directions.Add(new int[] { -1, 0 });    // bottom up     - first row

            int direction = 0;
            int row = 0;
            int col = -1;            

            var spiral = new int[totalNumbers];

            var nSteps = new int[] { rows, columns - 1 }; 

            while (nSteps[direction % 2] > 0)
            {
                for(int i = 0; i < nSteps[direction % 2]; i++)
                {
                    row += directions[direction][0];
                    col += directions[direction][1];

                    spiral[index++] = array[row, col];
                }

                nSteps[direction % 2]--;
                direction = (direction + 1) % 4; 
            }

            return spiral;
        }
    }
}