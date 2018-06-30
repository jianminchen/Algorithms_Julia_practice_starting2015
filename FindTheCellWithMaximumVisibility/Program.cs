using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheCellWithMaximumVisibility
{
    class Program
    {
        // 2d grid contains emtpy cell or flower cell or wall
        public const string Symbols = "FW ";

        static void Main(string[] args)
        {
            RunTestcase(); 
        }

        public static void RunTestcase()
        {
            var matrix = new char[4, 4];

            matrix[0, 0] = ' ';
            matrix[0, 1] = 'F';
            matrix[0, 2] = '0';
            matrix[0, 3] = 'F';

            matrix[1, 0] = 'W';
            matrix[1, 1] = ' ';
            matrix[1, 2] = 'F';
            matrix[1, 3] = ' ';

            matrix[2, 0] = 'F';
            matrix[2, 1] = 'W';
            matrix[2, 2] = ' ';
            matrix[2, 3] = 'F';

            matrix[3, 0] = ' ';
            matrix[3, 1] = 'F';
            matrix[3, 2] = 'W';
            matrix[3, 3] = ' ';

            var result = FindTheCellWithMaximumVisibility(matrix); 
        }

        /// <summary>
        /// using dynamic programming method, the time complexity should be O(n * n)
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static int FindTheCellWithMaximumVisibility(char[,] matrix)
        {
            if(matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return 0; 
            }

            var rows    = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var leftToRight = scanLeftToRight(matrix);
            var rightToLeft = scanRightToLeft(matrix);
            var topToBottom = scanTopToBottom(matrix);
            var bottomToUp  = scanBottomToUp(matrix);

            var maxCount = 0; 

            for(int row = 0; row < rows; row++)
            {
                for(int col = 0; col < columns; col++)
                {
                    var current = matrix[row, col];

                    var isFlower = current == Symbols[0];
                    var isWall   = current == Symbols[1];
                    var isEmpty  = current == Symbols[2];

                    var currentCount = 0;
                    if (isFlower || isEmpty)
                    {
                        currentCount = leftToRight[row, col] + 
                                       rightToLeft[row, col] + 
                                       topToBottom[row, col] + 
                                       bottomToUp[row, col];

                        if (isFlower)
                        {
                            currentCount = currentCount - 3;
                        }
                    }

                    maxCount = currentCount > maxCount ? currentCount : maxCount;
                }
            }

            return maxCount; 
        }

        private static int[,] scanLeftToRight(char[,] matrix)
        {
            var rows    = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var flowersCount = new int[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for(int col = 0; col < columns; col++)
                {
                    var current = matrix[row, col];
                    var isFlower = current == Symbols[0];
                    var isWall   = current == Symbols[1];                   

                    flowersCount[row, col] = 0;
                    if (!isWall)
                    {
                        int increment = 0; 
                        if (isFlower)
                        {
                            increment = 1; 
                        }

                        flowersCount[row, col] = col == 0 ? increment : (increment + flowersCount[row, col - 1]);
                    }
                }
            }

            return flowersCount;
        }

        private static int[,] scanRightToLeft(char[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var flowersCount = new int[rows, columns];
            var lastColumn = columns - 1; 

            for (int row = 0; row < rows; row++)
            {
                for (int col = columns - 1; col >=0; col--)
                {
                    var current = matrix[row, col];
                    var isFlower = current == Symbols[0];
                    var isWall = current == Symbols[1];

                    flowersCount[row, col] = 0;
                    if (!isWall)
                    {
                        int increment = 0;
                        if (isFlower)
                        {
                            increment = 1;
                        }

                        flowersCount[row, col] = col == lastColumn ? increment : (increment + flowersCount[row, col + 1]);
                    }
                }
            }

            return flowersCount;
        }

        private static int[,] scanTopToBottom(char[,] matrix)
        {
            var rows    = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var flowersCount = new int[rows, columns];
            var lastColumn   = columns - 1;

            for (int col = 0; col < columns; col++)
            { 
                for (int row = 0; row < rows; row++)
                {                                
                    var current  = matrix[row, col];
                    var isFlower = current == Symbols[0];
                    var isWall   = current == Symbols[1];

                    flowersCount[row, col] = 0;
                    if (!isWall)
                    {
                        int increment = 0;
                        if (isFlower)
                        {
                            increment = 1;
                        }

                        flowersCount[row, col] = row == 0 ? increment : (increment + flowersCount[row - 1, col]);
                    }
                }
            }

            return flowersCount;
        }

        private static int[,] scanBottomToUp(char[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var flowersCount = new int[rows, columns];
            var lastColumn = columns - 1;

            for (int col = 0; col < columns; col++)
            {
                for (int row = rows - 1; row >= 0; row--)
                {
                    var current = matrix[row, col];
                    var isFlower = current == Symbols[0];
                    var isWall = current == Symbols[1];

                    flowersCount[row, col] = 0;
                    if (!isWall)
                    {
                        int increment = 0;
                        if (isFlower)
                        {
                            increment = 1;
                        }

                        flowersCount[row, col] = row == (rows - 1) ? increment : (increment + flowersCount[row + 1, col]);
                    }
                }
            }

            return flowersCount;
        }
    }
}
