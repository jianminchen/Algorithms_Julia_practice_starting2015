using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeNSortedArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            int[][] test = new int[4][]{
                new int[4]{1,5, 9, 13}, 
                new int[4]{2,6, 10,14}, 
                new int[4]{3,7, 11, 15}, 
                new int[4]{4, 8, 12, 16}
            };

            int[][] test2 = new int[3][]{
                new int[4]{1,5, 9, 13}, 
                new int[4]{2,6, 10,14}, 
                new int[4]{3,7, 11, 15}
            };

            IList<int[]> input = new List<int[]>(test2);

            int[] output = mergeNSortedArray(input); 
        }

        /*
         * July 21, 2016
         * 
         * Divide and conquer 
         * Time complexity: 
         * 
         */
        public static int[] mergeNSortedArray(IList<int[]> list)
        {
            int[] res = new int[0];
            if (list == null || list.Count == 0)
                return res;

            while (list != null && list.Count > 1)
            {
                IList<int[]> tmp = new List<int[]>(); 

                int len = list.Count;
                for (int i = 0; i < len / 2; i++)
                {
                    int[] leftPart = list[i];
                    int[] rightPart = list[i + len / 2];

                    int[] merge = mergeTwoSortedArray(leftPart, rightPart);

                    tmp.Add(merge); 
                }

                //edge case
                if (len % 2 == 1)
                    tmp.Add(list[len-1]);

                list = new List<int[]>(tmp); 
            }

            return list[0]; 
        }

        public static int[] mergeTwoSortedArray(int[] arr1, int[] arr2)
        {
            if (arr1 == null || arr1.Length == 0)
                return arr2;
            if (arr2 == null || arr2.Length == 0)
                return arr1;

            int len1 = arr1.Length;
            int len2 = arr2.Length;

            int index = 0;

            int start1 = 0;
            int start2 = 0; 

            int[] merge = new int[len1+len2]; 

            while(start1 < len1 && start2 < len2)
            {
                if(arr1[start1] < arr2[start2])
                {
                    merge[index] = arr1[start1];
                    start1++;                   
                }
                else
                {
                    merge[index] = arr2[start2];
                    start2++;                    
                }

                index++; 
            }
            
            if(start1 < len1)
            {
                for(int i=start1; i< len1;i++)
                {
                    merge[index] = arr1[i];
                    index++;                  
                }                 
            }

            if(start2 < len2)
            {
                for (int i = start2; i < len2; i++)
                {
                    merge[index] = arr2[i];
                    index++; 
                }
            }

            return merge; 
        }
    }
}
