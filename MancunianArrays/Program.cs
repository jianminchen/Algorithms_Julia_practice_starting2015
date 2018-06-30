using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancunianArrays
{
    class Program
    {
        static void Main(string[] args)
        {

            int number = Convert.ToInt32(Console.ReadLine());

            int[] numbers = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int count = 0; 

            for(int i = 0; i < number; i++)
            {
                if(numbers[i] % 3 != 0)
                {
                    count++; 
                }
            }

            Console.WriteLine(count); 
        }
    }
}
