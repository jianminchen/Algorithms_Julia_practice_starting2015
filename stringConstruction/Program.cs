using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringConstruction
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            for (int a0 = 0; a0 < n; a0++)
            {
                string s = Console.ReadLine();

                Console.WriteLine(getNo(s)); 
            }
        }

        public static int getNo(string s)
        {
            if (s == null || s.Length == 0)
                return 0;

            string arr = "abcdefghijklmnopqrstuvwxyz";
            int TOTAL = 26;
            bool[] found = new bool[TOTAL]; 

            int count = 0;
            char[] aA = arr.ToArray(); 
            
            for(int i = 0; i < s.Length; i++)
            {
                int index = s[i] - 'a'; 
                if(!found[index])
                {
                    found[index] = true;
                    count++; 
                }

                if (count == TOTAL)
                    break; 
            }

            return count; 
        }
    }
}
