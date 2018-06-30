using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minMaxSum
{
    class minMaxSum
    {
        static void Main(string[] args)
        {
            int[] arr = ToInt(Console.ReadLine().Split(' '));

            int min = Int32.MaxValue;
            int max = Int32.MinValue;
            long sum = 0; 

            foreach(int item in arr)
            {
                sum += item;
                min = (item < min) ? item : min;
                max = (item > max) ? item : max; 
            }

            Console.WriteLine((sum - max).ToString() + " " + (sum - min).ToString()); 
        }

        private static int[] ToInt(string[] arr)
        {
            int len = arr.Length;
            int[] res = new int[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = Convert.ToInt32(arr[i]);
            }

            return res;
        }
    }
}
