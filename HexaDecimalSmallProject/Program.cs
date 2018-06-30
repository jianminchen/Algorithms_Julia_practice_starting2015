using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaDecimalSmallProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = convertHexaDecimalToBinaryList('F'); 
        }

        private static bool isHexaDecimal(char c)
        {
            int no = c - '0';
            int no2 = c - 'A';
            bool isOneDigitNumber = (no >= 0 && no <= 9);
            bool isinABCDEF = (no2 >= 0 && no2 <= 5);

            return (isOneDigitNumber || isinABCDEF);
        }
        /*
        * work on a small function ...
        * 
        */
        private static string convertHexaDecimalToBinaryList(char c)
        {
            int no = c - '0';
            int no2 = c - 'A';
            bool isOneDigitNumber = (no >= 0 && no <= 9);
            bool isinABCDEF = (no2 >= 0 && no2 <= 5);

            bool isHexaDecimalChar = (isOneDigitNumber || isinABCDEF);

            if (!isHexaDecimalChar)
                return "";

            string[] arr = new string[16]{
            "0","1","10","11","100","101","110","111","1000","1001","1010",
            "1011","1100","1101","1110","1111"
            };

            //int no = c -'0'; 

            if (isOneDigitNumber)
            {
                return arr[no];
            }
            else
            {
                return arr[10 + c - 'A'];
            }
        }

        /*
         * Hexadecimal char - 4 bits number, 
         * nth bit - 0 - 3 from least significant bit 0 to leftmost bit 
         * 
         * 
         */
        public static bool nthBitis1(char c, int n)
        {
            if (!isHexaDecimal(c) || (n<0 || n>3))
                return false;

            int value = hexaDecimalCharToInt(c);

            int newValue = value >> n;
            return (newValue & 1) == 1; 
        }

        /*
         * Convert HexaDecimal char to integer 
         */
        public static int hexaDecimalCharToInt(char c)
        {
            int number = c - '0';
            if (number >= 0 && number <= 9)
                return number;
            else
            {
                string charStr = "ABCDEF";
                char[] charArr = charStr.ToCharArray();
                int value = Array.IndexOf(charArr, c);
                return 10 + value;
            }
        }

        /*
         * Find first bit is 1 from leftmost to least significant bit 
        */
        public static bool first1InHexaDecimalNumber(char c, ref int n)
        {
            if (!isHexaDecimal(c))
                return false;
            int  value = hexaDecimalCharToInt(c); 
            int[] arr = {8, 4, 2, 1};

            for (int i = 0; i < arr.Length; i++ )
            {
                if ((value & arr[i]) == value)
                {
                    n = i; 
                    return true; 
                }
            }

            return false; 
        }
    }
}
