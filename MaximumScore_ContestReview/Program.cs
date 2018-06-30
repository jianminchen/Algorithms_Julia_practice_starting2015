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
            ProcessInput();
            //RunTestcase(); 
        }

        public static void RunTestcase()
        {
            int n = 3;

            long[] a = new long[] { 4, 8, 5 };

            var used = new HashSet<int>();
            IList<int> sequence = new List<int>();
            long sum = 0;
            Dictionary<string, long> calculated = new Dictionary<string, long>();
            long maxScore = GetMaxScore(a, used, sum, 0);
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
            Dictionary<string, long> calculated = new Dictionary<string, long>();
            long maxScore = GetMaxScore(a, used, sum, 0);
            Console.WriteLine(maxScore);
        }

        static Dictionary<string, long> calculated = new Dictionary<string,long>(); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static long GetMaxScore(long[] array, HashSet<int> used, long sum, long score )
        {
            if (used.Count == array.Length)
            {
                return 0;
            }

            if (calculated.ContainsKey(EncodeKey(used)))
            {
                return calculated[EncodeKey(used)]; 
            }

            long maxScore = long.MinValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (used.Contains(i)) continue;

                long current = array[i];
                var currentScore = sum % current;
                var newCopy = new HashSet<int>(used);
                newCopy.Add(i);                              
                
                long value = GetMaxScore(array, newCopy, sum + current, score + currentScore);
                currentScore += value;                                  

                maxScore = Math.Max(currentScore, maxScore);
            }

            calculated[EncodeKey(used)] = maxScore; 

            return maxScore;
        }     
  
        public static string EncodeKey(HashSet<int> numbers)
        {            
            int[] sorted = numbers.ToArray(); 
            Array.Sort(sorted);
            return string.Join(" ", sorted); 
        }
    }
}