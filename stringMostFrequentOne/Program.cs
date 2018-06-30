using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringMostFrequentOne
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        /*
         * Ascii value is in the range from [-128, 127]
         * so, add +128 to fit in the range [0, 255]
         */
        public static void get_most(char[] s, ref char ch, ref int maxSize)
        {
            ch = '\0';
            maxSize = 0;

            if (null != s)
            {
                int[] n = new int[256];
                
                //memset(n, 0, sizeof(n));
                int count = 0; 
                while(s[count] != '\0')
                {
                    char c = s[count];
                    int index = c + 128;

                    n[index] += 1;

                    if ((n[index]) > maxSize)
                    {
                        maxSize = n[index];
                        ch = c;
                    }

                    count ++;
                }
            }
        }
    }
}
