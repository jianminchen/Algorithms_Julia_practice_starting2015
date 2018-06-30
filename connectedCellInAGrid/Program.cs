using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectedCellInaGrid_May11_P2
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = Convert.ToInt32(Console.ReadLine());
            int cols = Convert.ToInt32(Console.ReadLine());

            int[][] arr = new int[rows][];

            for (int i = 0; i < rows; i++)
                arr[i] = new int[cols];

            for (int i = 0; i < rows; i++)
            {
                string[] colA = Console.ReadLine().Split(' ');

                for (int j = 0; j < cols; j++)
                    arr[i][j] = Convert.ToInt32(colA[j]);
            }

            Console.WriteLine(countConnectedCells(arr, rows, cols));
        }

        public static int countConnectedCells(int[][] arr, int rows, int cols)
        {
            int max = Int32.MinValue;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    if (arr[i][j] == 1)
                    {
                        int value = countConnectedCellsUsingQueue(arr, rows, cols, i, j);
                        max = value > max ? value : max;
                    }
                }

            return max;
        }

        private static int countConnectedCellsUsingQueue(int[][] arr, int rows, int cols, int startX, int startY)
        {
            Queue<int> queue = new Queue<int>();

            arr[startX][startY] = 0;
            queue.Enqueue(calculateKey(startX, startY));

            int count = 0;
            while (queue.Count > 0)
            {
                int key = Convert.ToInt32(queue.Dequeue());

                int tmpX = key / 10;
                int tmpY = key % 10;

                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                    {
                        int neighbor_X = tmpX + i;
                        int neighbor_Y = tmpY + j;

                        if (isInBound(neighbor_X, neighbor_Y, rows, cols) && arr[neighbor_X][neighbor_Y] == 1)
                        {
                            arr[neighbor_X][neighbor_Y] = 0;
                            queue.Enqueue(calculateKey(neighbor_X, neighbor_Y));
                        }
                    }

                count++;
            }

            return count;
        }

        private static bool isInBound(int x, int y, int rows, int cols)
        {
            return (x >= 0 && x < rows) && (y >= 0 && y < cols);
        }

        private static int calculateKey(int x, int y)
        {
            return 10 * x + y;
        }
    }
}