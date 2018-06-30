using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamelCase
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();

            //string s1 = "saveChangesInTheEditor"; 
            Console.WriteLine(getNumberWords(s)); 
        }

        public static int getNumberWords(string s)
        {
            if (s == null || s.Length == 0)
                return 0; 
            string alphabetic = "ABCDEDFGHIJKLMNOPQRSTUVWXYZ";

            int count = 1; 
            for(int i=0; i < s.Length; i++)
            {
                char c = s[i]; 
                char[] arr = alphabetic.ToArray();

                if (Array.IndexOf(arr, c) != -1)
                    count++; 
            }

            return count; 
        }
    }
}
