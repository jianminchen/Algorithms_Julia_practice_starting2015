using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDistance
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase();
            RunTestcase2();
            RunTestcase3();
        }

        public static void RunTestcase()
        {
            int result1 = minDistance("abc", "abcd");
            Debug.Assert(result1 == 1);
        }

        public static void RunTestcase2()
        {
            int result1 = minDistance("abc", "bca");
            Debug.Assert(result1 == 2);
        }

        public static void RunTestcase3()
        {
            int result1 = minDistance("abc", "cba");
            Debug.Assert(result1 == 2);
        }

        /// <summary>
        /// June 22, 2017
        /// Code review again after 24 months
        /// 
        /// June 17, 2015
        /// source code reference: 
        /// http://blog.csdn.net/fightforyourdream/article/details/13169573
        /// there is a table to explain how to build this distance table:
        /// 
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        public static int minDistance(String word1, String word2)
        {
            int length1 = word1.Length;
            int length2 = word2.Length;

            // consider to add empty space string 
            var distance = new int[length1 + 1][];

            for (int i = 0; i < length1 + 1; i++)
            {
                distance[i] = new int[length2 + 1];
            }

            // base case: one thing is empty, then distance is another string's length  
            for (int i = 0; i < length1 + 1; i++)
            {
                distance[i][0] = i;
            }

            for (int j = 1; j < length2 + 1; j++)
            {
                distance[0][j] = j;
            }

            // recursive，[i][j] depends on left，top and left top 3 situations
            for (int i = 1; i < length1 + 1; i++)
            {
                var current1 = word1[i - 1];

                for (int j = 1; j < length2 + 1; j++)
                {
                    var current2 = word2[j - 1];

                    // If last characters of two words are the same, then it is 0. 
                    // If they are different, then it is 1. 
                    var distanceValue = (current1 == current2) ? 0 : 1;

                    var top = distance[i - 1][j];
                    var left = distance[i][j - 1];
                    var leftTop = distance[i - 1][j - 1];

                    // from top or from left, both needs insertion
                    var minimum = Math.Min(top + 1, left + 1);

                    // from top left, consider substitution                                       
                    distance[i][j] = Math.Min(minimum, leftTop + distanceValue);
                }
            }

            // return right bottom corner 
            return distance[length1][length2];
        }
    }
}