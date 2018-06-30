using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestMask
{
    /// <summary>
    /// https://stackoverflow.com/questions/3912112/check-if-a-number-is-non-zero-using-bitwise-operators-in-c
    /// https://stackoverflow.com/questions/3160659/innovative-way-for-checking-if-number-has-only-one-on-bit-in-signed-int
    /// http://www.catonmat.net/blog/low-level-bit-hacks-you-absolutely-must-know/
    /// </summary>
    class Program
    {
        static void Main(String[] args)
        {
            //ProcessInput();     
            RunTestcase(); 
        }

        public static void RunTestcase()
        {
            //int length = 10; 
            //var numbers = new int[] { 1, 2, 4, 8, 16, 32,64,256, 512, 128 };

            int length = 3;
            var numbers = new int[] {9, 10,11 }; 

            int[][] memo = new int[length][];

            for (int i = 0; i < length; i++)
            {
                memo[i] = new int[27];
            }

            int[] foundMemo = new int[length];

            int result = getBestMask(numbers, memo, foundMemo);
        }

        public static void ProcessInput()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            string[] row = Console.ReadLine().Split(' ');
            int[] numbers = Array.ConvertAll(row, Int32.Parse);
            int length = numbers.Length;

            int[][] memo = new int[length][];

            for (int i = 0; i < length; i++)
            {
                memo[i] = new int[27];
            }

            int[] foundMemo = new int[length];


            int result = getBestMask(numbers, memo, foundMemo);
            Console.WriteLine(result);
        }

        /// <summary>
        /// http://www.catonmat.net/blog/low-level-bit-hacks-you-absolutely-must-know/
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int getBestMask(int[] numbers, int[][] memo, int[] foundMemo)
        {
            const int SIZE = 27; 

            int result = 0;
            int length = numbers.Length;            
            var memoSets = new HashSet<int>[SIZE];

            for(int i = 0; i < SIZE; i++)
            {
                memoSets[i] = new HashSet<int>(); 
            }

            var excludingSet = new HashSet<int>(); 

            for (int n = 26; n >= 0; n--)
            {
                bool foundOne = false;   
                int foundIndex = -1; 
               
                for(int i = 0; i < numbers.Length; i++)
                {
                    if(foundMemo[i] == 1)
                    {
                        continue; 
                    }

                    var visit = numbers[i];
                    if (checkNthBit(visit, n))
                    {
                        memoSets[n].Add(i); 
                    }

                    if (checkNthBit(visit, n) && (foundOne || (visit % (1 << n) == 0)))
                    {
                        if(!foundOne)
                        {
                            foundIndex = i; 
                        }

                        foundOne = true;                           
                    }
                }

                // mark visited 
                if(foundOne)
                {
                    int findMax = memoSets[n].Count;
                    int maxIndex = n; 
                    for (int previous = 26; previous > n; previous -- )
                    {
                        if(excludingSet.Contains(previous))
                        {
                            continue; 
                        }

                        var current = filter(memoSets[previous], foundMemo);
                        var count = current.Count;
                        if (current.Contains(n) && count > findMax)
                        {
                            maxIndex = previous;
                            findMax  = current.Count; 
                        }
                    }

                    foreach (var id in memoSets[maxIndex])
                    {
                        foundMemo[id] = 1;
                    }

                    result += 1 << maxIndex;

                    excludingSet.Add(maxIndex); 
                }
            }            

            return result; 
        }

        private static HashSet<int> filter(HashSet<int> original, int[] foundMemo)
        {
            var filtered = new HashSet<int>(); 

            foreach(var item in original)
            {
                if(foundMemo[item] == 1)
                {
                    continue;
                }

                filtered.Add(item); 
            }

            return filtered; 
        }

        private static bool checkFoundMemoNotComplete(int[] foundMemo)
        {
            foreach(var item in foundMemo)
            {
                if(item == 0)
                {
                    return true; 
                }
            }

            return false; 
        }

        private static bool checkNthBit(int number, int n)
        {
            return (number & ( 1 << n)) > 0; 
        }
    }
}
