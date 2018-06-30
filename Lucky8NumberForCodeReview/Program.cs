    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Hackerrank_LuckyNumber8_DP_Studycode
    {
        /*
         * Problem statement:
         * 
         * https://www.hackerrank.com/contests/w28/challenges/lucky-number-eight
         * 
         * Study dynamic programming solution 
         * 
         * questions on two things: 
         * first value is 1, count[0][0] = 1, why? count 0 as the first digit. see the frequency table. 
         * second, count[n - 1][0] + Module - 1, why? Remove number 0 as the answer. 
         */
        class Program
        {
            private static long Module = 1000000007;

            static void Main(string[] args)
            {
                int n = Convert.ToInt32(Console.ReadLine());

                String number = Console.ReadLine().ToString();

                int[][] frequencyTable = new int[n][];  // subsequences 

                for (int i = 0; i < n; i++)
                {
                    frequencyTable[i] = new int[8];
                }

                BuildFrequencyTableFromBottomUp(frequencyTable, number, n);

                Console.WriteLine((frequencyTable[n - 1][0] - 1) % Module);
            }

            /*
             * Build frequency table using bottom up method - dynamic programming
             * Design of the algorithm: 
             * At any position of the sequence, you need to consider two cases:
                Concatenate the digit at the position with your current subsequence and move to next position.
                Leave the digit and move to next position.
                The idea can be coded with states: Current position and Remainder of the subsequence modulo 8.
             * 
             */
            public static void BuildFrequencyTableFromBottomUp(int[][] frequencyTable, string number, int n)
            {
                const int SIZE = 8;
                frequencyTable[0][0] = 1;  // ask why here? count 0 as the first digit
                frequencyTable[0][(number[0] - '0') % SIZE]++;

                // build a frequency table - 
                for (int i = 1; i < n; i++)
                {
                    // subseqences - just ignore the current elment
                    for (int remainder = 0; remainder < SIZE; remainder++)
                    {
                        frequencyTable[i][remainder] = frequencyTable[i - 1][remainder];
                    }

                    // iterate each element in the array - go over all possible remainders in ascending order
                    for (int remainder = 0; remainder < SIZE; remainder++)
                    {
                        long current = frequencyTable[i - 1][remainder];

                        // if the remainder's related count is 0, then no possible subsequences to the nextRemainder
                        // skip the remainder. 
                        if (current == 0)
                        {
                            continue;
                        }

                        int nextRemainder = (10 * remainder + (number[i] - '0')) % SIZE;

                        frequencyTable[i][nextRemainder] = (Int32)((frequencyTable[i][nextRemainder] + current) % Module);
                    }
                }
            }
        }
    }