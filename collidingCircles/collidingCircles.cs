using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace collidingCircles
{
    class collidingCircles
    {       
        static void Main(string[] args)
        {
            ProcessInput(); 
            //RunTestcase();
            //RunTestcase2(); 
        }

        public static void RunTestcase()
        {
            List<int> balls = new List<int>(new int[]{1,2,3});
            
            double expectedTotalArea = 0;
            CalculateExpectedTotalAreaAfterKSeconds(balls, 1, 1, ref expectedTotalArea); 
        }

        public static void RunTestcase2()
        {
            List<int> balls = new List<int>(new int[] { 1, 2, 3 });

            double expectedTotalArea = 0;
            CalculateExpectedTotalAreaAfterKSeconds(balls, 2, 1, ref expectedTotalArea);
        }
       
        public static void ProcessInput()
        {
            var firstRow = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int numbers = firstRow[0];
            int kSeconds = firstRow[1];

            var balls = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            double expectedTotalArea = 0;
            CalculateExpectedTotalAreaAfterKSeconds(balls.ToList(), kSeconds, 1, ref expectedTotalArea);

            Console.WriteLine(expectedTotalArea); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="balls"></param>
        /// <param name="kseconds"></param>
        /// <returns></returns>
        public static void CalculateExpectedTotalAreaAfterKSeconds(List<int> balls, int kseconds, double probability, ref double expectedTotalArea)
        {
            if (kseconds == 0 || (balls.Count == 1 ))
            {
                expectedTotalArea += CalculateTotalArea(balls) * probability; 
                return;
            }                     

            int length = balls.Count; 

            for(int i = 0; i < length - 1; i++)
            {
                for(int j = i+1; j < length; j++)
                {
                    var first  = balls[i];
                    var second = balls[j];
                    var collide = first + second;
                    
                    List<int> copy = new List<int>(balls); 
                    
                    // first update, and then remove, avoid out-of-index error
                    copy[i] = collide;
                    copy.RemoveRange(j, 1);
                    double nextProbablity = probability / (length * (length - 1) / 2);

                    CalculateExpectedTotalAreaAfterKSeconds(copy, kseconds - 1, nextProbablity, ref expectedTotalArea);                   
                }
            }

            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="balls"></param>
        /// <returns></returns>
        private static double  CalculateTotalArea(IList<int> balls)
        {
            double totalArea = 0; 

            for(int i = 0; i < balls.Count; i++)
            {
                totalArea += balls[i] * balls[i]; 
            }

            return Math.PI * totalArea; 
        }       
    }
}
