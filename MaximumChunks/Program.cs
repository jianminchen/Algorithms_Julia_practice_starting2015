using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximumChunks
{
    /// <summary>
    /// jianmin chen January 9, 2018 8:07 PM
    /// Given an array with length N, the number is from 1 to N, please 
    /// calculate maximum number of chunks, so that each chunk is sorted, 
    /// and then the whole array is sorted
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {            
            Debug.Assert(GetNumberOfChunks(new int[] { 2, 1, 3, 5, 4 }) == 3); // [2,1],[3],[5,4]
            Debug.Assert(GetNumberOfChunks(new int[] { 2, 1, 4, 5, 3 }) == 2); // [2,1],[4,5,3]
            Debug.Assert(GetNumberOfChunks(new int[] { 5, 1, 4, 3, 2 }) == 1); // [5, 1,4, 3, 2]
        }

        public static int GetNumberOfChunks(int[] numbers)
        {
            var smallest = 1;
            int start = 0;
            int index = 0;
            while (smallest <= numbers.Length) // 3 < 5
            {
                int endIndex = FindSmallestChunkEndIndex(numbers, start, smallest); // 1
                smallest = endIndex + 2; // 3
                start = endIndex + 1; // 2

                index++;
            }

            return index; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="start"></param>
        /// <param name="smallest"></param>
        /// <returns></returns>
        public static int FindSmallestChunkEndIndex(int[] numbers, int start, int smallest) //{ 2, 1, 3, 5, 4 }
        {
            int length = numbers.Length;// 5

            int maxValue = 0; 
            var foundSmallest = false; 
            for(int index = start; index < length; index++) // 0
            {
                var current = numbers[index]; // 2, 1

                if (!foundSmallest) // false, true
                {
                    foundSmallest = current == smallest;// true
                }

                maxValue = current > maxValue ? current : maxValue; // 2, 2

                if(foundSmallest) // true
                {
                    var endFirstChunk = (index + 1) == maxValue; // 1 + 1 = 2
                    if(endFirstChunk)
                    {
                        return index; // 1
                    }
                }
            }

            return length - 1; 
        }
    }
}
