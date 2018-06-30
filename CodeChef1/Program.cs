using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChef1
{
    class Program
    {
        static void Main(string[] args)
        {
            int queries = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < queries; i++ )
            {
                var input = Console.ReadLine();

                Console.WriteLine(counting(input)); 
            }
        }

        public static int counting(string s)
        {
            if( s == null || s.Length == 0)
            {
                return 0; 
            }           

            var previous = s[0];
            var flip = 0;  
            foreach(var item in s)
            {
                if(item != previous)
                {
                    flip++;
                    previous = item; 
                }
            }

            return (flip + 1) / 2; 
        }
    }
}
