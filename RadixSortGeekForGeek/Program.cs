using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixSortGeekForGeek
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = {170, 45, 75, 90, 802, 24, 2, 66};
            int n = arr.Length;

            radixsort(arr, n);

            print(arr, n);
        }

        // A utility function to get maximum value in arr[]
        static int getMax(int[] arr, int n)
        {
            int mx = arr[0];
            for (int i = 1; i < n; i++)
                if (arr[i] > mx)
                    mx = arr[i];
            return mx;
        } 
        
        /*
         * exp - 10, rightmost second digit 
         * counting sort - using extra array to prepare sorting result. O(N). 
         */
        static void countSort(int[] arr, int n, int exp)
        {
            int N = 10; 
            int[] output = new int[n];

            // each digit, for example 1, position[1] is designed to serve multiple purposes:
            // 1. First, get count of digit 1 in the array in the nth digit
            // 2. Secondly, use summation of position[i] from 0 to up, 
            //    get the end position of each digit in the array 
            // 3. Thirdly, adjust the position by decrementing by one, 
            //   server next position of digit 1. 
            // 
            int[] helper = new int[N];

            // Store count of occurrences in position[]
            for (int i = 0; i < n; i++)
            {
                // nth digit (0, 1, ..9)'s number
                int index = getNthDigit(arr, i, exp); 
          
                helper[ index ]++;
            }

            // Change position[i] so that position[i] now contains
            // actual position of this digit in output[]
            // position[0], position[1], ..., position[9]
            // 0 end position: position[0] -1 
            // 1 end position: position[0] + position[1] -1 
            // 2 end position: position[0] + position[1] + position[2] -1  
            // 3 end position: position[0] + position[1] + position[2] + position[3] -1 
            // ...
            // each digit populates the position from end to start, decement one
            for (int i = 1; i < N; i++)
                helper[i] += helper[i - 1];
 
            //
            for (int i = n - 1; i >= 0; i--)
            {
                // nth digit (0, 1, ..9)'s number
                int index = getNthDigit(arr, i, exp); 

                output[helper[ index ] - 1] = arr[i];
                helper[index]--;  // still use the same variable 
            } 
            
            for (int i = 0; i < n; i++)
                arr[i] = output[i];
        }

        private static int getNthDigit(int[] arr, int i, int exp)
        {
            return  (arr[i]/exp)%10;
        }
 
        // The main function to that sorts arr[] of size n using
        // Radix Sort
        static void radixsort(int[] arr, int n)
        {
            // Find the maximum number to know number of digits
            int m = getMax(arr, n);
 
            // Do counting sort for every digit. Note that instead
            // of passing digit number, exp is passed. exp is 10^i
            // where i is current digit number
            for (int exp = 1; m/exp > 0; exp *= 10)
                countSort(arr, n, exp);
        }
 
         
        static void print(int[] arr, int n)
        {
            for (int i=0; i<n; i++)
                Console.Write(arr[i]+" ");
        }     
    }
}
