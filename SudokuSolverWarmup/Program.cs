using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverWarmup
{
    class Program
    {
        /*
         * May 22, 2016
         * Review the Sudoku algorithm
         * - using DFS, depth first search algorithm 
         * 
         */
        static void Main(string[] args)
        {
            char[,] board = new char[9,9];

            char[][] input = new char[9][]{
                new char[] { '.', '.', '9', '7', '4', '8', '.', '.', '.' }, 
                new char[] { '7', '.', '.', '.', '.', '.', '.', '.', '.' },
                new char[] { '.', '2', '.', '1', '.', '9', '.', '.', '.' },
                new char[] { '.', '.', '7', '.', '.', '.', '2', '4', '.' },
                new char[] { '.', '6', '4', '.', '1', '.', '5', '9', '.' },
                new char[] { '.', '9', '8', '.', '.', '.', '3', '.', '.' },
                new char[] { '.', '.', '.', '8', '.', '3', '.', '2', '.' },
                new char[] { '.', '.', '.', '.', '.', '.', '.', '.', '6' },
                new char[] { '.', '.', '.', '2', '7', '5', '9', '.', '.' }
            };

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = input[i][j];
                }
            }

            solveSudoku(board); 
        }

        public static void solveSudoku(char[,] board)
        {                                              
            bool result = dfs(board, 0, 0);
        }
        
        /*
         * Matrix 9x9, 
         * rows from 0 - 8
         * cols from 0 - 8
         */
        public static bool dfs(char[, ] board, int row, int col)
        {
            int SIZE = 8; 
            char Dot = '.'; 

            if (row > SIZE || col > SIZE)                  
                return true;            

            if (board[row, col] == Dot)
            {      
                for (int k = 1; k <= 9; k++)
                {
                    char kChar = (char)('0' + k);

                    if (isValid(board, row, col, kChar))
                    {    
                        board[row, col] = kChar;

                        int nextX = (col == SIZE) ? (row + 1) : row;
                        int nextY = (col == SIZE) ? 0 : (col + 1);                       
                        
                        if (dfs(board, nextX, nextY))                         
                            return true;                        

                        board[row, col] = '.';
                    }
                }

                return false;           
            }
            else
            {                      
                int nextX = (col == SIZE) ? (row + 1) : row;
                int nextY = (col == SIZE) ? 0 : (col + 1);   

                return dfs(board, nextX, nextY);
            }
        }

        /*
         * validation 
         * - every row
         * - ever  columns
         * - 9 matrixs 3x3 
         */
        public static bool isValid(char[,] board, int x, int y, char nextValue)
        {
             
            for (int i = 0; i < 9; i++)
            {
                // examine the column 
                if (board[i, y] == nextValue)
                {
                    return false;
                }

                // examine the row 
                if (board[x, i] == nextValue)
                {      
                    return false;
                }
            }

            // Examine 3x3 matrix 
            for (int i = 0; i < 3; i++)
            {  
                for (int j = 0; j < 3; j++)
                {
                    int row = 3 * (x / 3) + i;
                    int col = 3 * (y / 3) + j;

                    if (board[row, col] == nextValue)                    
                        return false;                    
                }
            }

            return true;
        }
    }
}
