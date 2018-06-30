using System;

class Solution
{
    // June 12, 2017
    // write the code in the mocking experience. 
    // brute force search - 9 * expontial 
    // DFS - backtrack
    // 9 backtrack -> n ^2, 81 cells - at most 81 emtpy cells
    // 
    static void Main(string[] args)
    {
        char[,] board = new char[9, 9];
        board = new char[9, 9]{
           {'5','3',' ',' ','7',' ',' ',' ',' '},
           {'6',' ',' ','1','9','5',' ',' ',' '},
           {' ','9','8',' ',' ',' ',' ','6',' '},
           {'8',' ',' ',' ','6',' ',' ',' ','3'},
           {'4',' ',' ','8',' ','3',' ',' ','1'},
           {'7',' ',' ',' ','2',' ',' ',' ','6'},
           {' ','6',' ',' ',' ',' ','2','8',' '},
           {' ',' ',' ','4','1','9',' ',' ','5'},
           {' ',' ',' ',' ','8',' ',' ','7','9'},
           
                              };
        Console.WriteLine(sudokuSolve(board));
    }

    static bool sudokuSolve(char[,] board)
    {
        // your code goes here
        return sudokuSolveHelper(board, 0, 0);
    }

    // 
    private static bool sudokuSolveHelper(char[,] board, int row, int col)
    {
        // base case 
        if (row > 8)  // 0 - 8 , row = 9
        {
            return true;
        }

        var current = board[row, col];
        bool isEmtpy = current == ' ';
        bool isNumber = (current - '0') >= 0 & (current - '0') <= 9;
        if (isNumber) // 3 
        {
            bool isLastColumn = col == 8;
            int nextRow = isLastColumn ? (row + 1) : row;
            int nextCol = isLastColumn ? 0 : col + 1;

            return sudokuSolveHelper(board, nextRow, nextCol); // 0, 1, (0, 2)
        }
        else // 
        {
            for (int number = 1; number <= 9; number++)
            {
                if (isAvailable(board, number, row, col))
                {
                    board[row, col] = (char)(number + '0');  // add '0' 
                    if (sudokuSolveHelper(board, row, col))
                    {
                        return true;
                    }
                    else
                    {
                        board[row, col] = ' ';
                    }
                }
            }

            return false;
        }

        // unreachable code
    }

    private static bool isAvailable(char[,] board, int number, int row, int col)
    {
        // check row 
        for (int column = 0; column < 9; column++)
        {
            if (board[row, column] - number == 0)
            {
                return false;
            }
        }

        // check column
        for (int rowIndex = 0; rowIndex < 9; rowIndex++)
        {
            if (board[rowIndex, col] - number == 0)
            {
                return false;
            }
        }

        // check 3 * 3 matrix 
        // row -> row/3
        // col -> col/3 
        int smallMatrixRow = row / 3; // 0
        int smallMatrixCol = col / 3; // 2 

        int startRow = smallMatrixRow * 3;
        int startCol = smallMatrixCol * 3;
        for (int r = startRow; r < startRow + 3; r++) // 5, 3, 6, 8, 9 - 1 avaiable 
        {
            for (int c = startCol; c < startCol + 3; c++)
            {
                if (board[r, c] - number == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }
}

//  5 3 ? - 7 -  - -  -
// 1 - 9, each row -> 3, 5, 7, 8 
// 1, 2, 3, 4, 6, 9
// 1, -> return true
// ?  -> 2, return true
// ? -> 3, ...
// ? -> 9, return true
// false 
// DFS -> backtracking 
// constraints - row checking, column check, 3 * 3 checking 
// base case row * col 9 * 9, 0 - 8, row = 9
// row, col -> row, col + 1, if it is not last column, increment row ++