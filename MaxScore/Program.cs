using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxScore
{
    /// <summary>
    /// score 3.50 out of 35
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //ProcessInput(); 
            RunTestcase(); 
        }

        public static void RunTestcase()
        {
            int n = 7;
            
            long[] a = new long[]{1,2,3,4,5,6};

            var used = new HashSet<int>();
            IList<int> sequence = new List<int>(); 
            long sum = 0;
            
            long maxScore = GetMaxScore(a, used, sum, 0, sequence);
            Console.WriteLine(maxScore);
        }

        public static void ProcessInput()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            string[] a_temp = Console.ReadLine().Split(' ');
            long[] a = Array.ConvertAll(a_temp, Int64.Parse);

            var used = new HashSet<int>();
            IList<int> sequence = new List<int>(); 
            long sum = 0;
            
            long maxScore = GetMaxScore(a, used, sum, 0, sequence);
            Console.WriteLine(maxScore);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static long GetMaxScore(long[] array, HashSet<int> used, long sum, long score, IList<int> sequence)
        {
            if(used.Count == array.Length)
            {
                return 0; 
            }

            long maxScore = long.MinValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (used.Contains(i)) continue; 
                
                long current = array[i];
                var currentScore = sum % current;
                var newCopy = new HashSet<int>(used);                  
                newCopy.Add(i);

                var newSequence = new List<int>(sequence);
                newSequence.Add(i);
               
                long value = GetMaxScore(array, newCopy, sum + current, score + currentScore, newSequence);
                currentScore += value;                                  

                maxScore = (currentScore > maxScore) ? currentScore : maxScore;
            }

            return maxScore; 
        }       
    }    
}
