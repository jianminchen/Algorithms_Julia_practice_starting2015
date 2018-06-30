using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriangleAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// Code review: 
        /// 10/13/2009 
        /// 
        /// Imagine you are given three integers, each representing a side of a triangle. 
        /// Write a function that determines whether the triangle is isoscoles, equilateral or scalene.
        /// 3 3 3    e    1
        /// 3 3 4  iso    2
        /// 3 4 5 scalens  3
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static int test(int x, int y, int z)
        {
            if (x <= 0 || y < 0 || z <= 0)
            {
                return -1;
            }

            if (x == y)
            {
                if (x == z)
                    return 1;    ///  3 3 3                               
                else
                {           // x not z
                    // if(y==z)  return 2;  //  delete  line 
                    return 2;   //  3 3 4  
                }
            }
            else
            {
                if (x == z) return 2;   //  3, 4, 3  4th  case 
                else if (y == z) return 2;  //  3 4 4
                else return 3;   //  3 4  5 }
            }

            // unreachable code
            //return -1;
        }
    }
}
