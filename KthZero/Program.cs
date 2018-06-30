using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KthZero
{
    class Program
    {
        /*
         * 3:46pm - 4:36pm
         * Read the question, and figure out the design:
         * Like database table using index, need to prepare index. 
         * Only update query - 
         *  1: case 1:replace the position which has value 0; 
         *  2. additional elment with value zero in the array
         *  Just put Hashtable to the array, and then, sort the array O(nlogn). 
         *  Maintain a hashtable and array for value 0 in the array 
         *  Otherwise, time complexity should be ok. 
         *  To make sort minimum - only sort when next query of kth zero comes in, 
         *  also set isDirty to track if update is need or not. 
         */
         /*
         * 4:36pm start to write code
          *5:30pm conduct testing. 
          *5:48pm, timeout, and score 26/40 points 
         */
        static void Main(string[] args)
        {
            string[] arr = Console.ReadLine().Split(' ');

            //int size = Convert.ToInt32(arr[0]);
            int queries = Convert.ToInt32(arr[1]);

            string[] table = Console.ReadLine().Split(' ');

            HashSet<int> zeroData = getZeroData(table);
            int[] zeroArray = zeroData.ToArray();
            Array.Sort(zeroArray);

            bool isDirty = false; 

            for (int i = 0; i < queries; i++)
            {
                string[] arr2 = Console.ReadLine().Split(' ');
                int symbol = Convert.ToInt32(arr2[0]);
                int kth    = Convert.ToInt32(arr2[1]);

                if (symbol == 1)
                {
                    Console.WriteLine(getKthZero(   
                        table,
                        zeroData, 
                        zeroArray, 
                        kth, 
                        ref isDirty)); 
                }
                else if (symbol == 2)
                {
                    updateQuery(
                        table,
                        zeroData, 
                        arr2,
                        ref isDirty); 
                }
            }
        }

        /*
         * 4:47pm - start coding
         * 4:50pm - exit the function 
         */
        private static HashSet<int> getZeroData(string[] arr)
        {
            HashSet<int> data = new HashSet<int>(); 

            for(int i=0; i< arr.Length; i++)
            {
                int no = Convert.ToInt32(arr[i]);
                if (no == 0)
                    data.Add(i); 
            }

            return data; 
        }

        /*
         * 4:53pm - start coding
         * 5:20pm - exit the function
         * 5:37pm testing, debug 
         *   kth should be 1, 2, .. zeroArray.Length
         */
        private static string getKthZero( 
            string[]     table,
            HashSet<int> zeroData,
            int[]        zeroArray,
            int          kth, 
            ref bool     isDirty)
        {
            string NO = "NO";             

            if (isDirty)
            {
                zeroArray = zeroData.ToArray();
                Array.Sort(zeroArray);
            }

            // Need to make sure the sync of zeroData and zeroArray
            if (kth >= 1 && 
                kth <= zeroArray.Length 
                )                          
                return zeroArray[kth-1].ToString();
            else             
                return NO; 
        }

        /*
         * 5:18pm - start coding
         * static analysis:
         * update 
         * 0 - add/ remove 1 in the array
         *    new value is 0 - 
         *    replaced position with value 0
         * 0 - update - do nothing
         * 
         * 5:25pm - exit coding 
         */
        private static void updateQuery(
            string[] table,
            HashSet<int> zeroData, 
            string[] para, 
            ref bool isDirty
            )
        {            
            int kth      = Convert.ToInt32(para[1]);
            int newValue = Convert.ToInt32(para[2]);

            if (kth < 0 || kth > table.Length)
                return;

            bool isIn = zeroData.Contains(kth) ; 
            if(isIn && newValue != 0)
            {
                isDirty = true;
                zeroData.Remove(kth); 
            }
            else if(!isIn && newValue == 0)
            {
                isDirty = true;
                zeroData.Add(kth); 
            }

            table[kth] = newValue.ToString(); 
        }
    }
}
