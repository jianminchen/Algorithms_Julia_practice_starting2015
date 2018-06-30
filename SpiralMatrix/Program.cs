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
                Debug.Assert(String.Join(",", spiral).CompareTo("123456789") == 0);
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
                if (array == null || array.GetLength(0) == 0 || array.GetLength(1) == 0)
                {
                    return new int[0];
                }

                int rows = array.GetLength(0);
                int columns = array.GetLength(1);

                var visited = new int[rows, columns];
                int index = 0;
                int totalNumbers = rows * columns;

                var fourDirections = new List<int[]>();

                fourDirections.Add(new int[] { 0, 1 });     // left to right - top row
                fourDirections.Add(new int[] { 1, 0 });     // top to down   - last column
                fourDirections.Add(new int[] { 0, -1 });    // right to left - bottom row
                fourDirections.Add(new int[] { -1, 0 });    // bottom up     - first row

                int direction = 0;
                int row = 0;
                int col = 0;

                var spiral = new int[totalNumbers];

                while (index < totalNumbers)
                {
                    var current = array[row, col];

                    spiral[index++] = current;

                    visited[row, col] = 1; // mark visit

                    var nextRow = row + fourDirections[direction][0];
                    var nextCol = col + fourDirections[direction][1];

                    var isOutArrayBoundary = nextRow < 0 || nextRow >= rows || nextCol < 0 || nextCol >= columns;

                    if (isOutArrayBoundary || visited[nextRow, nextCol] == 1) // change the direction
                    {
                        direction = (direction + 1) % 4; // map to 0 to 3                    
                    }

                    row += fourDirections[direction][0];
                    col += fourDirections[direction][1];
                }

                return spiral;
            }
        }
    }