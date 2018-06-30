using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringInterviews
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        bool contain_check()
        {
            string s1 = "AABCD";
            string d1 = "CDAA";

            char[] s = (s1+s1).ToArray<char>();
            char[] d = d1.ToArray<char>(); 

            int len = s.Length; ;

            for(int i = 0; i < len; ++i)
            {
               char temp = s[0];

               for(int j=0; j < len-1; ++j)
                 s[j] = s[j+1];

               s[len-1] = temp;
               if(s.ToString().IndexOf(d.ToString()) != -1)
                 return true;
            }

            return false;
        }
    }
}
