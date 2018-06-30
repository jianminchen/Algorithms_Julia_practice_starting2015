using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    internal class Box
    {
        public int Id { get; set; }
        public HashSet<int> balls { get; set; } // cannot have same color more than 1
        public static Dictionary<int, int> candiesPerColor { get; set; }
        public int MaximumBalls { get; set; }

        public Box(int id)
        {
            Id = id; 
        }

        /// <summary>
        ///  one copy for all boxes 
        /// </summary>
        /// <param name="colorId"></param>
        /// <param name="candies"></param>
        public void AddCandiesPerColor(int colorId, int candies)
        {
            if(candiesPerColor == null)
            {
                candiesPerColor = new Dictionary<int,int>(); 
            }

            if(!candiesPerColor.ContainsKey(colorId))
            {
                candiesPerColor.Add(colorId, candies); 
            }
        }
    }

    static void Main(String[] args)
    {
        string[] tokens_n = Console.ReadLine().Split(' ');

        int colors = Convert.ToInt32(tokens_n[0]);  // different colors - n 
        int numberOfBoxes = Convert.ToInt32(tokens_n[1]);  // number of boxes 

        var rowNumbers = Console.ReadLine().Split(' ');
        int[] ballsPerColor = Array.ConvertAll(rowNumbers, Int32.Parse);  // balls for each color 

        var rowNumbers_2 = Console.ReadLine().Split(' ');
        int[] maximumBalls = Array.ConvertAll(rowNumbers_2, Int32.Parse); // maximumBalls for each box 

        var candiesCanEarn = new int[colors][]; // candies earned for each box 

        for (int i = 0; i < colors; i++)
        {
            var rowNumbers3 = Console.ReadLine().Split(' ');
            candiesCanEarn[i] = Array.ConvertAll(rowNumbers3, Int32.Parse);
        }

        // if the box added are bigger than maximumBalls by value x, she pays x * x candies              

        // 
        // Write Your Code Here
        IList<Box> boxes = new List<Box>(); 

        for(int i = 0; i < numberOfBoxes; i++)
        {
            var current = new Box(i);
            

            boxes.Add(current); 
        }
    }
}