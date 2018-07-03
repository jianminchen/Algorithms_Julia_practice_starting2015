using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawACircle
{
    class DrawACircle
    {
        static void Main(string[] args)
        {
        }

        /*
         * problem statement: 
        You have one help function:  Set(int x, int y) which turns on a pixel at (x,y). 
        The task at hand is to come up with an algorithm and code to draw a circle on screen. 
        You can use C/C++/C#/Java or similar languages.
        */

        public static void DrawCircle(int xCenter, int yCenter, int radius)
        {
            //your code here
            /* 
             * updated on Dec 2012 
             * since we only can show pixel on the integer, then, we do not have many options, 
               maximum number of points on the circle is 2 * radius, 
               Go through X anxis first, from  -radius, -radius +1, …, 0, …, radius, see if 
               the y value is integer or not; 
               We can relax the algorithm, define a variable double errorRange, so we can make 
               it more points on circle, but less good shape;  in the algorithm, set errorRange = 0, 
               but make the algorithm extendable
      
               In terms of algorithm time complexity, it is O(2n); 
               But if we find too less points on the circle, then we have to design the algorithm 
               to find the best error range to simulate  the circle
  
               In the last, I will start from small error range to increment the range to find best fit 
            */
            if (radius < 0) return;
            if (radius == 0)
            {
                Set(xCenter, yCenter);
            }

            //Double errorRange = 0; 
            // we may change it based on percentage, 0.1*radius for example
            Double errorRange = 0.1 * radius;
            int n = 2;
            int lookLikeCircle = 4 + 4 * n; // we may think about at 8 point will make it look likecircle, 
            // we have at least 4 point on integer


            int countPoint = 0;
            int tryTimeMaximum = 1000;
            int countTry = 0;

            for (; ; )
            {
                for (int i = 0; i < radius; i++)
                {
                    int x1 = xCenter - i;
                    int x2 = xCenter + i;

                    Double yValue = Math.Sqrt(radius * radius - i * i);

                    if (Math.Abs(yValue - Convert.ToInt16(yValue)) < errorRange)
                    {
                        //set(xCenter-i, int(yValue));
                        //set(Xcenter+i, int(yValue));
                        countPoint = countPoint + 2;
                    }
                }

                if (countPoint < lookLikeCircle)
                {
                    errorRange = errorRange * 2;
                }
                else
                {
                    for (int i = 0; i < radius; i++)
                    {
                        int x1 = xCenter - i;
                        int x2 = xCenter + i;

                        Double yValue = Math.Sqrt(radius * radius - i * i);

                        //yValueNegative ?
                        if (Math.Abs(yValue - toInt(yValue)) < errorRange)
                        {
                            Set(xCenter - i, toInt(yValue) + yCenter);
                            Set(xCenter - i, -1 * (toInt(yValue) + yCenter));
                            Set(xCenter + i, toInt(yValue) + yCenter);
                            Set(xCenter + i, -toInt(yValue) + yCenter);
                            //countpoint=countpoint+2; 
                        }

                        break;
                    }

                    countTry++;
                    // to avoid exhausting too much time
                    if (countTry < tryTimeMaximum)
                        continue;
                    else
                        break;  // break the loop

                }
            }
        }

        public static int toInt(double x)
        {
            return Convert.ToInt16(x);
        }


        public static void Set(int x, int y)
        {
        }
    }
}
