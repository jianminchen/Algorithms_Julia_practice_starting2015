using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        static IList<int> collectSprial(int[][] matrix) 
        {
           var spiral = new List<int>();

           if(matrix == null || matrix.Length == 0) 
           {
               return spiral;
           }

           int rows = matrix.Length;
           int cols = matrix[0].Length;    
    
           for (int x = 0; x <= rows/2; x++) 
           {
                var startCol = x;
                var endCol = cols - startCol; 
 
                // Collect Move from left to right
                for (int col = startCol; col < endCol; col++)
                {
                    spiral.Add(matrix[x][col]);
                }
     
                //collect last column  - 2, 3
                var startRow = x + 1; 
                var endRow   = rows - x;
                for (int row = startRow; row < endRow; row++)
                {
                    if(cols - 1 - x >= x) {
                        spiral.Add(matrix[row][cols - 1 - x]);
                    }
                }
    
                //collect bottom row;
                for (int col = cols - 2 - x; col >= x; col--) {
                    if(rows - 1 - x > x) {
                        spiral.Add(matrix[rows - 1 - x][col]);
                    }
                }
      
                //collect first col
                for (int row = rows - 2 - x; row >= x + 1; row --) {
                    if(cols - 1 - x != x) {
                        spiral.Add(matrix[row][x]);
                    }
                }
            }
   
            return spiral;
         }
    }
}
