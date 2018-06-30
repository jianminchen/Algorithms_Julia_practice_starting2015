
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject
{
    /// <summary>
    /// code review - July 18, 2017
    /// Maze - 
    /// Given an array, there is only one item is value of 9, all others are 0 or 1. 
    /// 0 stands for the exit is not availabe, 1 is ok to continue. Make a judgement 
    /// if starting from (0,0), 4 directions: upper, down, left and right, and see 
    /// if there is path to find 9.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var maze = new int[3][]{
                new int[]{1, 0, 0, 1}, 
                new int[]{1, 1, 1, 1}, 
                new int[]{1, 0, 0, 9}};
           
            Console.WriteLine(checkMaze(maze, 0, 0));
        }

        public static bool checkMaze(int[][] maze, int row, int col)
        {
            if (maze == null || maze.Length == 0 || maze[0].Length == 0 ||                      
                row < 0 || row >= maze.Length ||
                col < 0 || col >= maze[0].Length ||
                maze[row][col] == 0 )
            {
                return false;
            }

            if (maze[row][col] == 9)
            {
                return true;
            }

            // mark as visited, Julia forget to set it, ran into stack overflow.
            // It took her 10 minute to figure out the issue. 
            maze[row][col] = 0; 

            // depth first search, go over each recursive branch if need. 
            return checkMaze(maze, row + 1, col) ||
                   checkMaze(maze, row - 1, col) ||
                   checkMaze(maze, row, col + 1) ||
                   checkMaze(maze, row, col - 1); 
        }       
    }
}
 