using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = CalculateDistance("923857614", "423692");
        }

        public static int CalculateDistance(string keyboard, string input)
        {
            if(input == null || input.Length <= 1)
                return 0;

            var dict = toDictionary(keyboard);

            var distance = 0;
            var length = input.Length;
            var previous = input[0] - '0';
            for(int i = 1; i < length; i++ )
            {
                var current = input[i] - '0';
                var currentStep = calculateStep(dict, current, previous);
                distance += currentStep;
                previous = current;
            }

            return distance; 
        }

        private static int calculateStep(Dictionary<int, int> dict, int current, int previous)
        {
            var start = dict[current];
            var end = dict[previous];

            var startRow = start / 3;
            var startCol = start % 3;

            var endRow = end / 3;
            var endCol = end % 3;

            var diff = new int[] { Math.Abs(startRow - endRow), Math.Abs(startCol - endCol) };
            var value = diff.Max();
            return value == 2 ? 2 : value == 1 ? 1 : 0; 
        }

        private static Dictionary<int, int> toDictionary(string keyboard)
        {
            var dict = new Dictionary<int, int>();

            for(int i = 0 ; i < keyboard.Length; i++)
            {
                var key = keyboard[i] - '0';
                dict.Add(key, i); 
            }

            return dict; 
        }
    }
}
