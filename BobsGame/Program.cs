using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BobsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessInput(); 
            //RunTestCase(); 
            //RunTestCase2();
        }

        public static void RunTestCase()
        {
            int[] moves = new int[4];             
            var result = CalculateWins(new string[] {"K.","XX"}, 2); 
        }

        public static void RunTestCase2()
        {
            int[] moves = new int[4];
            var result = CalculateWins(new string[] { "...", ".K.","..K" }, 2);
        }

        public static void ProcessInput()
        {
            int queries = Convert.ToInt32(Console.ReadLine());
            for (int index = 0; index < queries; index++)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                string[] board = new string[n];
                for (int board_i = 0; board_i < n; board_i++)
                {
                    board[board_i] = Console.ReadLine();
                }

                var numbers = CalculateWins(board, n);
                if (numbers == 0)
                {
                    Console.WriteLine("LOSE");
                }
                else
                {
                    Console.WriteLine("WIN " + numbers);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int CalculateWins(string[] board, int n)
        {
            int[] moves = new int[4];  // 0, 1, 2, 3 moves

            for(int row = 0; row < n; row ++)
            {
                for(int col = 0; col < n; col ++)
                {
                    var visit = board[row][col];
                    var isKing = visit == 'K'; 
                    if(!isKing)
                    {
                        continue; 
                    }

                    updateCount(board, row, col, n, moves);
                }
            }

            var sum = moves.Sum();
            var residue = moves[3] % 2;
            var winFive = residue == 0; 
            if (sum == 0 )
            {
                return 0; 
            }

            if (winFive)
            {
                return 5; 
            }

            int wins = 1;
            wins *= 2;

            return wins; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="n"></param>
        /// <param name="moves"></param>
        private static void updateCount(string[] board, int row, int col, int n, int[] moves)
        {
            int up = (row > 0 && col != 0) && (board[row - 1][col] != 'X') ? 1 : 0;
            int left = (col > 0) && (board[row][col - 1] != 'X') ? 1 : 0;
            int leftUp = (row > 0 && col > 0) && (board[row - 1][col - 1] != 'X') ? 1 : 0;
            var sum = up + left + leftUp; 
            if(sum == 0)
            {
                return; 
            }

            moves[up + left + leftUp]++; 
        }
    }
}
