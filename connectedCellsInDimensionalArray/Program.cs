using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectedCellMay11
{
    /*
     * Julia Chen practice - May 11, 2016
     */
    class Program
    {
        static void Main(string[] args)
        {

            int rows = Convert.ToInt32(Console.ReadLine());
            int cols = Convert.ToInt32(Console.ReadLine());

            int[,] arr = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string[] colA = Console.ReadLine().Split(' ');

                for (int j = 0; j < cols; j++)
                    arr[i, j] = Convert.ToInt32(colA[j]);
            }

            Console.WriteLine(countConnectedCell(arr, rows, cols));

            /*  testRoutine(); 
              Console.ReadLine();  */
        }

        static void testRoutine()
        {
            int rows = 5;
            int cols = 4;

            string[] strA = new string[5] { "0 0 1 1", "0 0 1 0", "0 1 1 0", "0 1 0 0", "1 1 0 0" };

            int[,] arr = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string[] colA = strA[i].Split(' ');

                for (int j = 0; j < cols; j++)
                    arr[i, j] = Convert.ToInt32(colA[j]);
            }

            Console.WriteLine(countConnectedCell(arr, rows, cols));
        }

        public static int countConnectedCell(int[,] arr, int rows, int cols)
        {
            int max = Int32.MinValue;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    if (arr[i, j] == 1)
                    {
                        int value = countConectedCellUsingQueue(arr, rows, cols, i, j);
                        max = value > max ? value : max;
                    }
                }

            return max;
        }

        private static int countConectedCellUsingQueue(int[,] arr, int rows, int cols, int startX, int startY)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(calculateKey(startX, startY));
            arr[startX, startY] = 0;

            int count = 0;
            while (queue.Count > 0)
            {
                int key = (int)queue.Dequeue();

                int tmpX = key / 10;
                int tmpY = key % 10;

                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                    {
                        int neighbor_X = i + tmpX;
                        int neighbor_Y = j + tmpY;

                        if (isInBound(neighbor_X, neighbor_Y, rows, cols) && arr[neighbor_X, neighbor_Y] == 1)
                        {
                            arr[neighbor_X, neighbor_Y] = 0;
                            queue.Enqueue(calculateKey(neighbor_X, neighbor_Y));
                        }
                    }

                count++;

                // Console.WriteLine("found: X=" + tmpX.ToString() + " Y=" + tmpY.ToString()); 
            }

            return count;
        }

        private static bool isInBound(int x, int y, int rows, int cols)
        {
            if ((x >= 0 && x < rows) && (y >= 0 && y < cols))
                return true;
            else
                return false;
        }

        private static int calculateKey(int x, int y)
        {
            return 10 * x + y;
        }
    }
}